#define Enable_WriteDelay

using DLMS.Comm;
using DLMS.LRUCache;
using LogSystem.Shared.Common;
using Serenity.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DLMS
{
    #region Delegate

    /// <summary>
    /// GetData Delegate used to Pass an IO_Buffer received from lower level Transport Layers(TCPIP/HDCL) in
    /// DLMS_COSEM protocol stack
    /// </summary>
    /// <param name="data">IO_Buffer argument</param>
    public delegate void GetData(byte[] data);

    /// <summary>
    /// This method Begins a Synchronous write operation on Underlay Instance when it complete,
    /// it Begins a Synchronous read operation on the Underlay Instance
    /// </summary>
    /// <remarks>
    /// When write operation begins it copy data from Buffer Encoded_Packet after attach any 
    /// below layer Encapsulation(+AL COM_Wrapper).When read operation completes it copy data 
    /// to Buffer Received_Packet after removing any below layer Encapsulation(+AL COM_Wrapper) 
    /// </remarks>
    /// <exception cref="IOException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <param name="Encoded_Packet">Raw IO byte Buffer Encode_APDU</param>
    /// <param name="offsetTBF">IO Buffer start index</param>
    /// <param name="countTBF">IO buffer bytes length</param>
    /// <param name="Received_Packet">Raw IO byte Buffer</param>
    /// <param name="offSetRBF">IO Buffer start index</param>
    /// <param name="CountRBF">IO buffer bytes length</param>
    /// <returns>Number Of Bytes read In Received_Packet Buffer</returns>
    public delegate int SynCommData(byte[] Encoded_Packet, int offsetTBF, int countTBF, ref byte[] Received_Packet, int offSetRBF, int CountRBF);

    /// <summary>
    /// This method Begins a Synchronous write operation on Underlay Instance when it complete,
    /// it Begins an asynchronous read operation on the Underlay Instance
    /// </summary>
    /// <remarks>
    ///When write operation begins it copy data from Buffer Encoded_Packet after attach any 
    /// below layer Encapsulation(+AL COM_Wrapper).When asynchronous read completes it copy data 
    /// to Buffer Received_Packet after removing any below layer Encapsulation(+AL COM_Wrapper) 
    /// </remarks>
    /// <exception cref="IOException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <param name="Encoded_Packet">Raw IO byte Buffer Encode_APDU</param>
    /// <param name="offsetTBF">IO Buffer start index</param>
    /// <param name="countTBF">IO buffer bytes length</param>
    /// <param name="Received_Packet">Raw IO byte Buffer</param>
    /// <param name="offSetRBF">IO Buffer start index</param>
    /// <param name="CountRBF">IO buffer bytes length</param>
    /// <param name="callBack">The <see cref="AsyncCallback"/> delegate function that is exected whean pending IO read Complete</param>
    /// <returns>IAsyncResult<see cref="IAsyncResult"/></returns>
    public delegate IAsyncResult BeginCommunicateData(byte[] Encoded_Packet, int offsetTBF, int countTBF,
                            ref byte[] Received_Packet, int offSetRBF, int CountRBF, AsyncCallback callBack);

    /// <summary>
    /// Handles the end of an asynchronous read,
    /// see also<see cref="IConnection.BeginReceiveDataFromPhysicalLayer(byte[],int,int,ref byte[],int,int,AsyncCallback)"/>.
    /// </summary>
    /// <exception cref="IOException"/>
    /// <param name="Result">An <see cref="IAsyncResult"/> that represents an asynchronous call</param>
    /// <returns>The Number Of byte read in Received_Packet</returns>
    public delegate int EndCommunicateData(IAsyncResult Result);

    #endregion

    /// <summary>
    /// The ApplicationProcess_Controller is an Interface/Controller class to manage the flow and control DLMS_COSEM API
    /// The ApplicationProcess_Controller provides the DLMS_COSEM Protocol services which include
    /// Connect Service <see cref="AARQ(SAP_Object,SAP_Object, String)"/>,Disconnect Service<see cref="ARLRQ()"/>,
    /// GET_Service <see cref="GET(Base_Class)"/>,SET_Service <see cref="SET(Base_Class)"/>
    /// and Method_Invoke_Service<see cref="Method_Invoke(Base_Class)"/>
    /// </summary>
    public class ApplicationProcess_Controller : IDisposable
    {
        #region DataMembers

        internal bool PacketDroped = false;
        private bool _DLMSNotify = false;

        private IConnection ICommunicationObject;
        internal String ErrorMessage = null;
        internal Exception innerException = null;
        internal byte[] localBuffer;

        /// <summary>
        /// Local Receive Buffer Max_Size 
        /// </summary>
        public static readonly int DefaultMaxLocalBuffer = 1024;
        public static readonly int[] Max_AllowedBufferSize = new int[] { 256, 512, 1024, 2048 };

        private int _MaxLocalBuffer = DefaultMaxLocalBuffer;

        /// <summary>
        /// Max Number of IO_BLock can be transmitted/received in Get_WITH_BLOCK Service
        /// </summary>
        public static readonly int Max_GET_BLOCK_Count = 4096;
        /// <summary>
        /// Max Number of IO_BLock can be transmitted/received in Max_SET_BLOCK_Count Service
        /// </summary>
        public static readonly int Max_SET_BLOCK_Count = 4096;
        
        /// <summary>
        /// Max Number of IO_Request can be Re-Transmitted
        /// </summary>
        public static readonly int MAX_Request_Retry = 1;

        private ApplicationProcess_GET_Controller GetSubController;
        private ApplicationProcess_AARQ_Controller _AARQSubController;
        private ApplicationProcess_ARLRQ_Controller _ARLRQSubController;
        private ApplicationProcess_SET_Controller _SETSubController;
        private ApplicationProcess_Invoke_Controller _InvokeSubController;

        private DLMS_Application_Process Application_Process;
        internal ushort ownerThreadId;
        private SAPTable APSAPTable;
        internal GetSAPEntryKeyIndex _GetSAPEntryDlg;
        internal bool isResetIOStream = false;
        private Int32 isHandlerAttached = 0;

        private Security_Data _Security_Data;
        private Serenity.Crypto.ICrypto _Cypher;

        #region Event_Handler_Delegates

        /// <summary>
        /// It will provided particular DLMS_COSEM Class Instance/Object fully initialized,<see cref="GetSAPEntry"/>
        /// </summary>
        internal readonly GetSAPEntry GetSAPTableEntryDlg = null;
        /// <summary>
        /// This delegate will notify DLMS_COSEM Application Process Layer,when IO_Packet loss
        /// </summary>
        internal readonly Action<PacketType, string> Application_Layer_PacketDropDlg = null;

        private Event_Handler_EventNotify _EventNotify = null;

        /// <summary>
        /// EventNotify Alarm that DLMS Event Notification Service Received
        /// </summary>
        public event Event_Handler_EventNotify EventNotify
        {
            add
            {
                // Avoid Duplicate EventHandler Attachment
                if (_EventNotify != value)
                    _EventNotify += value;
                else ;    // Skip Already Event Handler Attached
            }
            remove
            {
                if (_EventNotify != null)
                    _EventNotify -= value;
            }
        }


        //private SynCommData _SynCommunicateData;
        //private BeginCommunicateData _AsyncCommunicateData;
        //private EndCommunicateData _EndAsyncCommunicateData;
        //private readonly AsyncCallback _EndAsyncCommunicateDataDummy;

        #endregion

        #endregion

        #region Properties

        //public SynCommData SynCommunicateData
        //{
        //    get { return _SynCommunicateData; }
        //    set
        //    {
        //        if (value == null || value.Target is IConnection)
        //            _SynCommunicateData = value;
        //        else
        //            throw new Exception("Not valid type class object");
        //    }
        //}

        //public BeginCommunicateData AsyncCommunicateData
        //{
        //    get { return _AsyncCommunicateData; }
        //    set
        //    {
        //        if (value == null || value.Target is IConnection)
        //            _AsyncCommunicateData = value;
        //        else
        //            throw new Exception("Not valid type class object");
        //    }
        //}

        //public EndCommunicateData EndAsyncCommunicateData
        //{
        //    get { return _EndAsyncCommunicateData; }
        //    set
        //    {
        //        if (value == null || value.Target is IConnection)
        //            _EndAsyncCommunicateData = value;
        //        else
        //            throw new Exception("Not valid type class object");
        //    }
        //}


        /// <summary>
        /// GET the local Buffer for DLMS_COSEM Transport Application Layer
        /// </summary>
        public byte[] LocalBuffer
        {
            get
            {
                try
                {
                    if (localBuffer == null || localBuffer.Length != MaxLocalBuffer)
                    {
                        localBuffer = new byte[MaxLocalBuffer];
                    }
                    return localBuffer;
                }
                catch (Exception) { throw; }
            }
        }

        /// <summary>
        /// Local Receive Buffer Max_Size 
        /// </summary>
        public int MaxLocalBuffer
        {
            get { return _MaxLocalBuffer; }
            set
            {
                if (Max_AllowedBufferSize.Contains<int>(value))
                    _MaxLocalBuffer = value;
                else
                    _MaxLocalBuffer = DefaultMaxLocalBuffer;
            }
        }

        /// <summary>
        /// Get/Set the <see cref="ApplicationProcess_GET_Controller"/> (Sub Controller)
        /// </summary>
        public ApplicationProcess_GET_Controller GETSubController
        {
            get { return GetSubController; }
            set { GetSubController = value; }
        }
        /// <summary>
        /// Get/Set the <see cref="ApplicationProcess_AARQ_Controller"/> (Sub Controller)
        /// </summary>
        public ApplicationProcess_AARQ_Controller AARQSubController
        {
            get { return _AARQSubController; }
            set { _AARQSubController = value; }
        }
        /// <summary>
        /// Get/Set the <see cref="ApplicationProcess_ARLRQ_Controller"/> (Sub Controller)
        /// </summary>
        public ApplicationProcess_ARLRQ_Controller ARLRQSubController
        {
            get { return _ARLRQSubController; }
            set { _ARLRQSubController = value; }
        }
        /// <summary>
        /// Get/Set the <see cref="ApplicationProcess_SET_Controller"/> (Sub Controller)
        /// </summary>
        public ApplicationProcess_SET_Controller SETSubController
        {
            get { return _SETSubController; }
            set { _SETSubController = value; }
        }
        /// <summary>
        /// Get/Set the <see cref="ApplicationProcess_Invoke_Controller"/> (Sub Controller)
        /// </summary>
        public ApplicationProcess_Invoke_Controller InvokeSubController
        {
            get { return _InvokeSubController; }
            set { _InvokeSubController = value; }
        }
        /// <summary>
        /// GET/SET delegate <see cref="GetSAPEntryKeyIndex"/>
        /// </summary>
        public GetSAPEntryKeyIndex GetSAPEntryDlg
        {
            get { return _GetSAPEntryDlg; }
            set { _GetSAPEntryDlg = value; }
        }

        /// <summary>
        /// GET <see cref="IConnection"/> Object associated with Current Instance
        /// </summary>
        public IConnection GetCommunicationObject
        {
            get
            {
                try
                {
                    var Event_Handler = new Action<ArraySegment<byte>>(GetIODataAsync);
                    // Assign DLMS Notification ASync Data Handler
                    if (ICommunicationObject != null &&
                        (ICommunicationObject.ReceiveDataFromPhysicalLayerASync == null))
                        // || ICommunicationObject.ReceiveDataFromPhysicalLayerASync != Event_Handler))
                    {
                        ICommunicationObject.ReceiveDataFromPhysicalLayerASync = Event_Handler;
                    }
                }
                catch
                { }

                return ICommunicationObject;
            }
            set
            {
                try
                {
                    ICommunicationObject = value;

                    var Event_Handler = new Action<ArraySegment<byte>>(GetIODataAsync);
                    // Assign DLMS Notification ASync Data Handler
                    if (ICommunicationObject != null &&
                        (ICommunicationObject.ReceiveDataFromPhysicalLayerASync == null))
                    // || ICommunicationObject.ReceiveDataFromPhysicalLayerASync != Event_Handler))
                    {
                        ICommunicationObject.ReceiveDataFromPhysicalLayerASync = null;
                        ICommunicationObject.ReceiveDataFromPhysicalLayerASync = Event_Handler;
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// GET <see cref="IConnection"/> Connect_Disconnect Status
        /// </summary>
        public bool IsConnected
        {
            get
            {
                try
                {
                    IConnection Conn = GetCommunicationObject;
                    return (Conn != null) && Conn.IsConnected() &&
                            Application_Process.Is_Association_Developed;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// SET/GET the <see cref="DLMS_Application_Process"/> Object
        /// </summary>
        public DLMS_Application_Process ApplicationProcess
        {
            get { return Application_Process; }
            set { Application_Process = value; }
        }

        /// <summary>
        /// GET <see cref="Base_Class"/> Previously Requested Object
        /// </summary>
        public Base_Class PreviousRequestedObject
        {
            get
            {
                return ApplicationProcess.Previous_Requested_Object;
            }
        }

        /// <summary>
        /// GET <see cref="SAPTable"/> Application Process SAP Table
        /// </summary>
        public SAPTable ApplicationProcessSAPTable
        {
            get { return APSAPTable; }
            set { APSAPTable = value; }
        }

        /// <summary>
        /// GET/SET Current Instance(DLMS_COSEM Protocol STACK) Identity
        /// </summary>
        public ushort OwnerThreadId
        {
            get { return ownerThreadId; }
            set { ownerThreadId = value; }
        }

        public uint SoftwareUserId
        {
            get
            {
                uint UserSoftwareId = Application_Process.User_Software_ID;
                return UserSoftwareId;
            }
            set
            {
                Application_Process.User_Software_ID = value;
            }
        }

        /// <summary>
        /// GET/SET <see cref="DLMS_Application_Process.User_Software_ID"/> Current User
        /// </summary>
        public ushort UserId //Used in All Reports //Current User Id
        {
            get
            {
                uint UserSoftwareId = Application_Process.User_Software_ID;
                return (ushort)(UserSoftwareId & 0xFFFF);
            }
            set
            {
                uint UserSoftwareId = (value);
                UserSoftwareId = (UserSoftwareId | (Application_Process.User_Software_ID & 0xFFFF0000));
                Application_Process.User_Software_ID = UserSoftwareId;
            }
        }

        /// <summary>
        /// GET/SET <see cref="DLMS_Application_Process.User_Software_ID"/> Current Software
        /// </summary>
        public ushort SoftWareId //Only used in About box
        {
            get
            {
                uint UserSoftwareId = Application_Process.User_Software_ID;
                return (ushort)(UserSoftwareId >> 16);
            }
            set
            {
                uint UserSoftwareId = (uint)(value << 16);
                UserSoftwareId = (UserSoftwareId | (Application_Process.User_Software_ID & 0xFFFF));
                Application_Process.User_Software_ID = UserSoftwareId;
            }
        }

        /// <summary>
        /// GET/SET Reset <see cref="IConnection"/>
        /// </summary>
        public bool IsResetIOStream
        {
            get { return isResetIOStream; }
            set
            {
                SetResetIOStream(value);
            }
        }

        /// <summary>
        /// GET/SET the internal state Variable <see cref="isHandlerAttached"/>        
        /// </summary>
        /// <remark>
        /// It will notify either function handlers linked with delegate variable or not
        /// </remark>
        internal bool IsHandlerAttached
        {
            get
            {
                return Interlocked.Equals(isHandlerAttached, 1);
            }
            set
            {
                Interlocked.Exchange(ref isHandlerAttached, Convert.ToInt16(value));
            }
        }

        public byte[] Client_ApplicationTitle
        {
            get
            {
                byte[] responding_AP_Title = null;
                try
                {
                    responding_AP_Title = Application_Process.Send_APPLICATION_ASSOCIATION.Calling_AP_Title.AP_Title;
                }
                catch
                {
                }
                return responding_AP_Title;
            }
            set
            {

                if (value != null && value.Length <= 01)
                    throw new ArgumentNullException("Calling AP Title", "Invalid Calling AP Title String");

                Application_Process.Send_APPLICATION_ASSOCIATION.Calling_AP_Title.AP_Title = value;
            }
        }

        public byte[] Responding_ApplicationTitle
        {
            get
            {
                byte[] responding_AP_Title = null;

                try
                {
                    responding_AP_Title = Application_Process.APPLICATION_ASSOCIATION_from_AL.Responding_AP_Title.AP_Title;
                }
                catch
                {
                }

                return responding_AP_Title;
            }
            set
            {
                if (value != null && value.Length <= 01)
                    throw new ArgumentNullException("Responding AP Title", "Invalid Responding AP Title String");

                Application_Process.Send_APPLICATION_ASSOCIATION.Responding_AP_Title.AP_Title = value;
                Application_Process.APPLICATION_ASSOCIATION_from_AL.Responding_AP_Title.AP_Title = value;
            }
        }

        public Security_Data Security_Data
        {
            get { return _Security_Data; }
            set
            {
                Application_Process.Security_Data = value;
                _Security_Data = value;
            }
        }

        public Serenity.Crypto.ICrypto Crypto
        {
            get { return _Cypher; }
            set
            {
                Application_Process.Crypto = value;
                _Cypher = value;
            }
        }

        public DLMSLogger Logger
        {
            get
            {
                return Application_Process.Logger;
            }
        }

        public ILogWriter ActivityLogger
        {
            get;
            set;
        }

        public static bool CheckAccessRights { get; set; }

        public TimeSpan Write_Delay { get; set; }

        public bool IsCompatibilityMode
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(Application_Process.IsCompatibilityMode);
                }
                catch { }
                return false;
            }
            set
            {
                try
                {
                    Application_Process.IsCompatibilityMode = Convert.ToByte(value);
                }
                catch { }
            }
        }

        public bool DLMSNotify
        {
            get { return _DLMSNotify; }
            internal set { _DLMSNotify = value; }
        }

        public bool RecvEventNotification { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ApplicationProcess_Controller()
        {
            Application_Process = new DLMS_Application_Process();
            // Initial Default Security_Data
            this.Security_Data = new Security_Data();

            // Initial ApplicationProcess_Controller Event_Handlers & Delegate
            GetSAPTableEntryDlg = new GetSAPEntry(GetSAPEntry);
            Attach_Handlers();
            RecvEventNotification = true;
            Init_ApplicationProcess_Controller();
            //CheckAccessRights = true;

        }

        static ApplicationProcess_Controller()
        {
        }

        #endregion

        #region Initial / DeInitial

        /// <summary>
        /// Initialize the Current Instance of an ApplicationProcess_Controller and its associated sub controllers and DLMS_Application_Process instance
        /// </summary>
        public void Init_ApplicationProcess_Controller()
        {
            try
            {
                Application_Process.Init_ApplicationProcess();

                // Reset Time Delay
                Write_Delay = new TimeSpan(0, 0, 0);
                // Initial Default Security_Data
                this.Security_Data = new Security_Data();
                #region ApplicationProcessSubController

                GetSubController = new ApplicationProcess_GET_Controller();
                AARQSubController = new ApplicationProcess_AARQ_Controller();
                ARLRQSubController = new ApplicationProcess_ARLRQ_Controller();
                SETSubController = new ApplicationProcess_SET_Controller();
                InvokeSubController = new ApplicationProcess_Invoke_Controller();

                #region Init_Controller

                ARLRQSubController.ApplicationProcess_Controller = this;
                AARQSubController.ApplicationProcess_Controller = this;
                GetSubController.ApplicationProcess_Controller = this;
                SETSubController.ApplicationProcess_Controller = this;
                InvokeSubController.ApplicationProcess_Controller = this;

                #endregion
                #endregion

                Application_Process.GetSAPTableEntryDelegate = GetSAPTableEntryDlg;
                Attach_Handlers();
                this.APSAPTable = new SAPTable();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Initializing DLMS_Application_Process", ex);
            }
        }

        /// <summary>
        /// De-Initialize the Current Instance of an ApplicationProcess_Controller and its associated sub controllers 
        /// </summary>
        public void DeInit_ApplicationProcess_Controller()
        {
            try
            {
                // Initial Default Security_Data
                this.Security_Data = new Security_Data();
                Crypto = null;

                #region ApplicationProcessSubController

                GetSubController = null;
                AARQSubController = null;
                ARLRQSubController = null;
                SETSubController = null;
                InvokeSubController = null;

                #endregion

                localBuffer = null;
                innerException = null;
                ErrorMessage = null;
                GetCommunicationObject = null;
                APSAPTable = null;
                _GetSAPEntryDlg = null;

                Dettach_Handlers();
                Application_Process.DeInit_ApplicationPorcess();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while DeIntializing DLMS_Application_Process", ex);
            }
        }

        #endregion

        #region GET_REQUEST_COMMAND

        public Data_Access_Result TryGETObject(StOBISCode ObisCode, byte attributeNo, out List<byte> rawCosemObject)
        {
            rawCosemObject = null;
            Base_Class Get_Object = null;
            Data_Access_Result ResultOpt = Data_Access_Result.Other_Reason;

            try
            {
                Get_Object = Application_Process.GetSAPTableEntryDelegate.Invoke(ObisCode);
                if (attributeNo < 0 && attributeNo > Get_Object.Attribs_No)
                    throw new DLMSException("Invalid attribute for GET Request_" + ObisCode);

                Get_Object.DecodingAttribute = attributeNo;
                Get_Object.EnableCOSEM_EncodeDecode_OPT = false;

                try
                {
                    GET(Get_Object);
                    rawCosemObject = Get_Object.EncodedRaw;
                }
                finally
                {
                    if (Application_Process != null &&
                        Application_Process.GET_from_AL != null)
                        ResultOpt = (Data_Access_Result)Application_Process.GET_from_AL.Data_Access_Result;
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                Logger.LogAPMessage(String.Format("Error occurred in GET Service {0}, \r\n Internal Error {1}",
                                    ObisCode, ex.Message), PacketType.GET);
            }
            finally
            {
                if (Get_Object != null)
                    Get_Object.EnableCOSEM_EncodeDecode_OPT = true;
            }

            return ResultOpt;
        }




        /// <summary>
        /// Read data of a particular attribute of a specific object from DLMS_COSEM device
        /// </summary>
        /// <remarks>
        /// This function is implemented using Synchronous IO Communication Model,see also 
        /// <see cref="ApplicationProcess_GET_Controller.GET_Helper(Base_Class)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObisCode">COSEM Object used to read data</param>
        /// <param name="attributeNo">Data attribute to be read</param>
        /// <returns>Base_Class</returns>
        public Base_Class GET(StOBISCode ObisCode, byte attributeNo)
        {
            Base_Class Get_Object = null;
            try
            {
                Get_Object = Application_Process.GetSAPTableEntryDelegate.Invoke(ObisCode);
                if (attributeNo < 0 && attributeNo > Get_Object.Attribs_No)
                    throw new DLMSException("Invalid attribute for GET Request_" + ObisCode);
                Get_Object.DecodingAttribute = attributeNo;
                GET(Get_Object);
                return Get_Object;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Read data of a specific object from DLMS_COSEM device
        /// </summary>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="ThreadAbortException">ThreadAbortException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="GetRequestObj">COSEM Object to be used to read data</param>
        /// <param name="TK">Data Read Cancellation Token during GET_WITH_BLOCKING blocking</param>
        /// <returns>Base_Class</returns>
        public Base_Class GET(Base_Class GetRequestObj, CancellationTokenSource TK = null)
        {
            try
            {
                if (GetRequestObj == null)
                    throw new ArgumentNullException("GetRequestObj");

                // Dettach_Handlers();
                GetSubController.GET_Helper(GetRequestObj, TK);

                // Validate data read completed Successfully
                // if (!GetRequestObj.IsReadSuccess)
                //     throw new DLMSException(String.Format("{0} {1} data read not completed successfully",
                //         GetRequestObj.OBISIndex, GetRequestObj.OBISIndex));

                return GetRequestObj;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Read data of a specific object from DLMS_COSEM device Asynchronously
        /// </summary>
        /// <remarks>
        /// This function is implemented using TPL(Task Parallel Library) communication model,see also 
        /// <see cref="DLMS.ApplicationProcess_GET_Controller.GET_HelperAsync(Base_Class,CancellationTokenSource)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="ThreadAbortException">ThreadAbortException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="GetRequestObj">COSEM Object to be used to read data</param>
        /// <param name="TK">Data Read Cancellation Token during GET_WITH_BLOCKING blocking</param>
        /// <returns>Task &lt;Base_Class&gt; ;Task that represent the pending IO to Complete</returns>
        public async Task<Base_Class> GETAsync(Base_Class GetRequestObj, CancellationTokenSource TK = null)
        {
            try
            {
                if (GetRequestObj == null)
                    throw new ArgumentNullException("GetRequestObj");
                /// Dettach_Handlers();
                await GetSubController.GET_HelperAsync(GetRequestObj, TK);
                /// Validate data read completed Successfully
                if (!GetRequestObj.IsReadSuccess)
                    throw new DLMSException(String.Format("{0} {1} data read not completed successfully",
                        GetRequestObj.OBISIndex, GetRequestObj.OBISIndex));

                return GetRequestObj;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Read data of a particular attribute of a specific object from DLMS_COSEM device Asynchronously 
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task based Async communication model,see also 
        /// <see cref="DLMS.ApplicationProcess_GET_Controller.GET_HelperAsync(Base_Class,CancellationTokenSource)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="ThreadAbortException">ThreadAbortException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObisCode">COSEM Object to be used to read data</param>
        /// <param name="attributeNo">Data attribute to be read</param>
        /// <param name="TK">Data Read Cancellation Token during GET_WITH_BLOCKING blocking</param>
        /// <returns>Task &lt;Base_Class&gt; ;Task that represent the pending IO to Complete</returns>
        public async Task<Base_Class> GETAsync(StOBISCode ObisCode, byte attributeNo, CancellationTokenSource TK = null)
        {
            Base_Class Get_Object = null;
            // byte[] Encoded_Packet = null;
            try
            {
                Get_Object = Application_Process.GetSAPTableEntryDelegate.Invoke(ObisCode);
                if (attributeNo < 0 && attributeNo > Get_Object.Attribs_No)
                    throw new DLMSException("Invalid attribute for GET Request_" + ObisCode);
                Get_Object.DecodingAttribute = attributeNo;
                await GETAsync(Get_Object, TK);
                return Get_Object;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Base_Class GET_Internal_Object(Get_Index ObisCode, byte attrib)
        {
            if (ApplicationProcessSAPTable != null)
            {
                OBISCodeRights rights = null;
                if (!ApplicationProcessSAPTable.SapTable.ContainsKey(ObisCode))
                {
                    ApplicationProcessSAPTable.SapTable.Add(ObisCode, null);
                }

                rights = ApplicationProcessSAPTable.SapTable[ObisCode];

                if (rights == null)
                    rights = new OBISCodeRights();
                if (rights.GetAttribRight(attrib) == Attrib_Access_Modes.No_Access)
                    rights.SetAttribRight(2, Attrib_Access_Modes.Read_Only);

                ApplicationProcessSAPTable.SapTable[ObisCode] = rights;
            }

            return GET_Internal_Object(ObisCode);
        }



        #endregion

        #region SET REQUEST COMMAND

        public Data_Access_Result TrySETObject(Base_Class ObjectToSet, byte EncodingAttribute, byte[] Data_To_Set)
        {
            Data_Access_Result ResultOpt = Data_Access_Result.Other_Reason;

            try
            {
                ResultOpt = SET(ObjectToSet, EncodingAttribute, Data_To_Set);
            }
            catch (Exception ex)
            {
                // throw ex;
                Logger.LogAPMessage(String.Format("Error occurred in SET Service {0}, \r\n Internal Error {1}",
                                    ObjectToSet.OBISIndex, ex.Message), PacketType.SET);
            }
            finally
            {
                if (Application_Process != null &&
                    Application_Process.SET_from_AL != null)
                    ResultOpt = (Data_Access_Result)Application_Process.SET_from_AL.Result;
            }
            return ResultOpt;
        }

        public Data_Access_Result SET(Base_Class ObjectToSet, byte EncodingAttribute, byte[] Data_To_Set)
        {
            byte[] Packet = null;
            ArraySegment<byte> Encoded_Packet;
            Data_Access_Result? _SetResult = Data_Access_Result.Other_Reason;
            try
            {
                if (!IsConnected)
                    throw new DLMSException("Unable to execute command,Application is not connected");

                // SET Base Class Object
                Packet = Application_Process.Encode_SET_Object(ObjectToSet, EncodingAttribute, Data_To_Set);
                // Access Rights Could be Checked
                Loop_WriteRead: Encoded_Packet = Packet.GetSegment<byte>();

                #region Apply_HighLevel_Security

                // Only For HLS Authentication Type And SecurityControl is not 'None'
                if (ApplicationProcess.
                    Send_APPLICATION_ASSOCIATION.Authentication_Mechanism >= HLS_Mechanism.HLS_Manufac &&
                    Security_Data != null &&
                    Security_Data.SecurityControl > SecurityControl.None)
                {
                    if (Crypto == null || _Security_Data == null || !_Security_Data.IsInitialized)
                    {
                        throw new CryptoException("Security Data not initialized properly to proceed for HLS Communication");
                    }

                    ArraySegment<byte> AuthenticatedEncrypted_Packet;

                    // Authenticated_Encryption
                    // General Protection Enable in Conformance
                    if (Application_Process.Negotiated_Conformance.GeneralProtection)
                    {
                        // Authenticated_Encryption
                        AuthenticatedEncrypted_Packet =
                                             Security_Context.Apply_GlobalGeneral_AuthenticatedEncryption(_Security_Data,
                                                                                                          _Cypher,
                                                                                                          Encoded_Packet);
                    }
                    else
                    {
                        // Service Specific AuthenticatedEncryption
                        _Security_Data.GloMessageTAG = (DLMSCommand)DLMS_Common.GetGloDLMSCommmandTAG(DLMSCommand.SetRequest);
                        AuthenticatedEncrypted_Packet =
                                             Security_Context.Apply_GlobalServiceSpecific_AuthenticatedEncryption(_Security_Data,
                                                                                                                  _Cypher,
                                                                                                                  Encoded_Packet);
                    }

                    Encoded_Packet = AuthenticatedEncrypted_Packet;
                }

                #endregion

                PacketDroped = false;
                DLMSNotify = false;

                #region Communicate On IO Channel

                if (IsResetIOStream && GetCommunicationObject != null)
                {
                    GetCommunicationObject.ResetStream();
                    SetResetIOStream(false);
                }

                //GetCommunicationObject.IsSyncComplete = true;
                    throw new IOException("Unable to write data on communication channel,IO delegates not initialized");
                byte[] localBuffer = null;

                #region WriteDelay
#if Enable_WriteDelay

                // Write_Delay
                if (Write_Delay.TotalMilliseconds > 0 &&
                    Write_Delay.TotalMilliseconds <= 60000)
                {
                    // Thread.Sleep(Write_Delay);
                    DLMS_Common.PreciseDelay(Write_Delay.TotalMilliseconds);
                }

#endif
                #endregion

                // Log Transmitted Data
                ApplicationProcess.Logger.LogALTraffic(Packet, 0, Packet.Length, DataStatus.Write);

                // Write IO Command To Meter
                GetCommunicationObject.SendRequestFromPhysicalLayer(Encoded_Packet.Array, Encoded_Packet.Offset, Encoded_Packet.Count);

                Loop_Read: int receiveBufLength = int.MaxValue;
                if (localBuffer == null)
                    receiveBufLength = int.MaxValue;
                else
                    receiveBufLength = localBuffer.Length;

                int length = -1;

                try
                {
                    length = GetCommunicationObject.ReceiveResponseFromPhysicalLayer(ref localBuffer, 0, receiveBufLength);
                }
                catch (IOException ex)
                {
                    if (ex.IsIODataTimeOut() &&
                        DLMSNotify)
                        goto Loop_WriteRead;
                    else
                        // Raise Error
                        throw ex;
                }

                bool isTagMatch = false;
                ArraySegment<byte> Resultant_Data = new ArraySegment<byte>(localBuffer, 0, length);

                if (Security_Context.IsSecurityApplicable(Resultant_Data.Array[0]))
                {
                    Resultant_Data = Security_Context.Process_HLS_APDU(_Security_Data, _Cypher, Resultant_Data, out isTagMatch);
                }
                GetIODataAsync(Resultant_Data);

                if (PacketDroped)
                {
                    if (innerException != null &&
                        innerException is IOException)
                        throw innerException;
                    if (_SetResult == null)
                        throw new IOException("SET Command Not Executed Successfuly" + ObjectToSet.INDEX);

                    throw new DLMSException(String.Format("{0}_{1}", ErrorMessage, ObjectToSet.INDEX), innerException);
                }
                // DLMS Notify
                else if (DLMSNotify)
                    goto Loop_Read;

                #endregion

                return (Data_Access_Result)_SetResult;
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                    throw ex;
                else if (ex is DLMSException)
                    throw ex;
                else if (ex is CryptoException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error occurred in SET Service {0}", ObjectToSet.INDEX), ex);
            }
            finally
            {
                //if (GetCommunicationObject != null) GetCommunicationObject.IsSyncComplete = false;
            }
        }



        /// <summary>
        /// Write data of a specific object to DLMS_COSEM device
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model,see also 
        /// <see cref="ApplicationProcess_SET_Controller.SET_Helper(Base_Class)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToSet">COSEM Object to be used to write data</param>
        /// <returns><see cref="Data_Access_Result"/></returns>
        public Data_Access_Result SET(Base_Class ObjectToSet, CancellationTokenSource SETAbortToken = null)
        {
            try
            {
                return SETSubController.SET_Helper(ObjectToSet, SETAbortToken);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Write data of a specific object to DLMS_COSEM device with Non Blocking IO
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task based ASync Communication model,see also 
        /// <see cref="DLMS.ApplicationProcess_SET_Controller.SET_HelperAsync(Base_Class, CancellationTokenSource)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToSet">COSEM Object to be used to write data</param>
        /// <returns>Task&lt;Data_Access_Result&gt;;Task that represent the pending IO to Complete</returns>
        public async Task<Data_Access_Result> SETAsync(Base_Class ObjectToSet, CancellationTokenSource SETAbortToken = null)
        {
            try
            {
                return await SETSubController.SET_HelperAsync(ObjectToSet, SETAbortToken);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Method REQUEST COMMAND

        /// <summary>
        /// This method invokes particular action/Method of particular object in DLMS_COSEM device
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model,See also
        /// <see cref="ApplicationProcess_Invoke_Controller.Method_InvokeHelper(Base_Class)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToInvoke">COSEM Object used to Invoke Action</param>
        /// <returns>Task&lt;Action_Result&gt;</returns>
        public Action_Result Method_Invoke(Base_Class ObjectToInvoke)
        {
            try
            {
                return InvokeSubController.Method_InvokeHelper(ObjectToInvoke);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This method invokes particular action/Method of particular object in DLMS_COSEM device
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task based Async communication model,
        /// <see cref="ApplicationProcess_Invoke_Controller.Method_InvokeHelperAsync(Base_Class)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="ObjectToInvoke">COSEM Object used to Invoke Action</param>
        /// <returns>Task&lt;Action_Result&gt;;Task that represent the pending IO to Complete</returns>
        public async Task<Action_Result> Method_InvokeAsync(Base_Class ObjectToInvoke)
        {
            try
            {
                return await InvokeSubController.Method_InvokeHelperAsync(ObjectToInvoke);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region AARQ

        /// <summary>
        /// Create an association with DLMS_COSEM device using AARQ Controller
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model,
        /// <seealso cref="ApplicationProcess_AARQ_Controller.AARQ_Helper(SAP_Object,SAP_Object,String)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="Password">Valid Password String for MeterSAP</param>
        /// <returns>True, False Either association is created or Not</returns>
        public bool AARQ(SAP_Object MeterSAP, SAP_Object ClientSAP, String Password)
        {
            try
            {
                return AARQSubController.AARQ_Helper(MeterSAP, ClientSAP, Password);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeterSAP"></param>
        /// <param name="ClientSAP"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool AARQ(ushort MeterSAP, ushort ClientSAP, String Password)
        {
            return AARQ(new SAP_Object(MeterSAP), new SAP_Object(ClientSAP), Password);
        }


        /// <summary>
        /// This helper method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ChallengeStr">Valid Challenge String for <see cref="SAP_Object"/></param>
        /// <param name="ChallengeOutStr">Returned Valid Challenge String from Meter <see cref="SAP_Object"/></param>  
        /// <param name="Authentication_Mechanism">HLS Authentication Mechanism</param>
        /// <returns>Either Is Connected or Not</returns>
        public bool AARQ(SAP_Object MeterSAP, SAP_Object ClientSAP, byte[] ChallengeStr, ref byte[] ChallengeOutStr,
                                HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC)
        {
            try
            {
                return AARQSubController.AARQ_Helper(MeterSAP, ClientSAP, ChallengeStr,
                                                     ref ChallengeOutStr, Authentication_Mechanism);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This helper method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model
        /// </remarks>
        ///<exception cref="IOException">IOException Class</exception>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        ///<exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        ///<exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ChallengeStr">Valid Challenge String for <see cref="SAP_Object"/></param>
        /// <param name="ChallengeOutStr">Returned Valid Challenge String from Meter <see cref="SAP_Object"/></param>  
        /// <param name="Authentication_Mechanism">HLS Authentication Mechanism</param>
        /// <returns>Either Is Connected or Not</returns>
        public bool AARQ(ushort MeterSAP, ushort ClientSAP, byte[] ChallengeStr,
                         ref byte[] ChallengeOutStr, HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC)
        {
            return AARQ(new SAP_Object(MeterSAP), new SAP_Object(ClientSAP), ChallengeStr, ref ChallengeOutStr, Authentication_Mechanism);
        }


        /// <summary>
        /// Create an association with DLMS_COSEM device using AARQ Controller Asynchronously
        /// </summary>
        /// <remarks>
        /// This function is implemented using ASync Communication Model,See also
        /// <see cref="ApplicationProcess_AARQ_Controller.AARQ_HelperAsync(SAP_Object,SAP_Object,String)"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="Password">Valid Password String for MeterSAP</param>
        /// <returns>Task&lt;bool&gt;Is Connected or not;Task that represent the pending IO to Complete</returns>
        public async Task<bool> AARQAsync(SAP_Object MeterSAP, SAP_Object ClientSAP, String Password)
        {
            try
            {
                bool isconnected = await AARQSubController.AARQ_HelperAsync(MeterSAP, ClientSAP, Password);
                return isconnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public async Task<bool> AARQAsync(ushort MeterSAP, ushort ClientSAP, String Password)
        {
            try
            {
                bool isconnected = await AARQSubController.AARQ_HelperAsync(new SAP_Object(MeterSAP), new SAP_Object(ClientSAP), Password);
                return isconnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        /// <summary>
        /// This method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task Based ASync Communication model
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ChallengeStr">Random Octet Challenge String of between 8 to 64 Characters long<see cref="SAP_Object"/></param>
        /// <param name="ChallengeOutStr">Random Octet Challenge String of between 8 to 64 Characters long<see cref="SAP_Object"/></param>
        /// <param name="Authentication_Mechanism">HLS Authentication Mechanism</param>
        /// <returns>Task&ls;bool&gt;Is Connected or not;Task that represent the pending IO to Complete</returns>
        public async Task<bool> AARQAsync(SAP_Object MeterSAP, SAP_Object ClientSAP, List<byte> ChallengeStr, List<byte> ChallengeOutStr,
                                HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC)
        {
            try
            {
                bool isconnected = await AARQSubController.AARQ_HelperAsync(MeterSAP, ClientSAP, ChallengeStr, ChallengeOutStr, Authentication_Mechanism);
                return isconnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method Connect to DLMS_COSEM device using DLMS_COSEM AARQ Service
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task Based ASync Communication model
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <param name="MeterSAP">Meter DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ClientSAP">Client DLMS_COSEM Service Access Point<see cref="SAP_Object"/></param>
        /// <param name="ChallengeStr">Random Octet Challenge String of between 8 to 64 Characters long<see cref="SAP_Object"/></param>
        /// <param name="ChallengeOutStr">Random Octet Challenge String of between 8 to 64 Characters long<see cref="SAP_Object"/></param>
        /// <param name="Authentication_Mechanism">HLS Authentication Mechanism</param>
        /// <returns>Task&ls;bool&gt;Is Connected or not;Task that represent the pending IO to Complete</returns>
        public async Task<bool> AARQAsync(ushort MeterSAP, ushort ClientSAP, List<byte> ChallengeStr, List<byte> ChallengeOutStr,
                                HLS_Mechanism Authentication_Mechanism = HLS_Mechanism.HLS_GMAC)
        {
            return await AARQAsync(new SAP_Object(MeterSAP), new SAP_Object(ClientSAP), ChallengeStr, ChallengeOutStr, Authentication_Mechanism);
        }

        #endregion

        #region ARLRQ

        /// <summary>
        /// Release association with DLMS_COSEM device using ARLRQ Controller
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model,See also
        /// <see cref="ApplicationProcess_ARLRQ_Controller.ARLRQ_Helper()"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <returns>Either Is Disconnected or Not</returns>
        public bool ARLRQ()
        {
            try
            {
                return ARLRQSubController.ARLRQ_Helper();
            }
            catch (Exception)
            {
                SetResetIOStream(true);
                throw;
            }
        }

        /// <summary>
        /// Release association with DLMS_COSEM device using ARLRQ Controller
        /// </summary>
        /// <remarks>
        /// This function is implemented using Sync Communication model,See also
        /// <see cref="ApplicationProcess_ARLRQ_Controller.ARLRQ_Helper()"/>
        /// </remarks>
        /// <param name="meterSap"> </param>
        /// <param name="clientSap"></param>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <returns>Either Is Disconnected or Not</returns>
        /// <returns></returns>
        public bool ARLRQ(ushort meterSap, ushort clientSap)
        {
            return ARLRQSubController.ARLRQ_Helper(new SAP_Object(meterSap), new SAP_Object(clientSap));
        }

        /// <summary>
        /// Release association with DLMS_COSEM device using ARLRQ Controller Asynchronously
        /// </summary>
        /// <remarks>
        /// This function is implemented using Task Based ASync Communication model,See also
        /// <see cref="ApplicationProcess_ARLRQ_Controller.ARLRQ_HelperAsync()"/>
        /// </remarks>
        /// <exception cref="IOException">IOException Class</exception>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <exception cref="DLMSEncodingException">DLMSEncodingException Class</exception>
        /// <exception cref="DLMSDecodingException">DLMSDecodingException Class</exception>
        /// <returns>Task&lt;bool&gt;;Either Is Disconnected or Not;Task that represent the pending IO to Complete</returns>
        public async Task<bool> ARLRQAsync()
        {
            try
            {
                return await ARLRQSubController.ARLRQ_HelperAsync();
            }
            catch (Exception)
            {
                SetResetIOStream(true);
                throw;
            }
        }
        #endregion

        #region Application_Process_Event Handlers

        private void EventNotification_Recieved(stEVENT_NOTIFICATION Recieved_Structure)
        {
            stEVENT_NOTIFICATION EVENT_NOTIFICATION_from_AL = null;
            St_EventNotify StEventNotify = null;

            Base_Class Object_To_Decode = null;
            StDateTime Capture_DateTime = null;
            StOBISCode ReceivedOBISCode = Get_Index.Dummy;
            byte Received_Attribute = 0;
            Object Data_Value = null;

            try
            {
                if (!RecvEventNotification)
                {
                    return;
                }

                EVENT_NOTIFICATION_from_AL = (stEVENT_NOTIFICATION)Recieved_Structure;
                Logger.LogAPMessage("Event Notification Request Received", PacketType.EventNotification);

                // association is not developed already
                if (!Application_Process.Is_Association_Developed)
                {
                    throw new DLMSException("Event Notification Received before Association is developed");
                }
                // if (!Application_Process.Negotiated_Conformance.EventNotification)
                // {
                //     throw new DLMSException("Event Notification Not Visible In Negotiated Conformance");
                // }

                // COSEM_Attribute_Descriptor
                ReceivedOBISCode = StOBISCode.ConvertFrom(EVENT_NOTIFICATION_from_AL.
                                                          COSEM_Attribute_Descriptor.COSEM_Object_Instance_Id);
                ReceivedOBISCode.ClassId = EVENT_NOTIFICATION_from_AL.
                                                          COSEM_Attribute_Descriptor.COSEM_Class_Id;
                Received_Attribute = EVENT_NOTIFICATION_from_AL.
                                                          COSEM_Attribute_Descriptor.COSEM_Object_Attribute_Id;
                // Decode Capture DateTime
                if (EVENT_NOTIFICATION_from_AL.Flg_Time != 0)
                {
                    Capture_DateTime = new StDateTime();
                    Capture_DateTime.DecodeDateTime(EVENT_NOTIFICATION_from_AL.Time);
                }

                // Process Event Notification Data
                if (EVENT_NOTIFICATION_from_AL.Attribute_Value != null &&
                    EVENT_NOTIFICATION_from_AL.Attribute_Value.Length > 0)
                {
                    // Create COSEM IC Instance
                    Object_To_Decode = GetSAPEntry(ReceivedOBISCode);
                    Object_To_Decode.DecodingAttribute = Received_Attribute;
                    Object_To_Decode.ResetAttributeDecodingResults(Received_Attribute);

                    byte[] TArray = EVENT_NOTIFICATION_from_AL.Attribute_Value;
                    int array_traverser = 0;
                    // Decode Attribute Value
                    Object_To_Decode.Decode_Data(ref TArray, ref array_traverser, TArray.Length);

                    // Process Value
                    double valDecoded = DLMS_Common.Decode_Any(Object_To_Decode, Convert.ToByte(ReceivedOBISCode.ClassId));
                    Data_Value = valDecoded;
                }

                StEventNotify = new St_EventNotify(ReceivedOBISCode, Received_Attribute, Capture_DateTime);
                StEventNotify.Flag_Time = EVENT_NOTIFICATION_from_AL.Flg_Time != 0;
                StEventNotify.Attribute_Value = Data_Value;

                // Logger.LogAPMessage("Event Notification:\r\n" + StEventNotify.ToString(), PacketType.EventNotification);
                DLMSNotify = true;
                if (_EventNotify != null)
                    _EventNotify.Invoke(StEventNotify);

                #region // ASync Event Notify Invoke

                // Delegate[] Handlers = null;
                // if (_EventNotify != null)
                // {
                //     Handlers = _EventNotify.GetInvocationList();
                //     foreach (Delegate evInvoke in Handlers)
                //     {
                //         if (evInvoke != null &&
                //             evInvoke is Event_Handler_EventNotify)
                //         {
                //             var AsynCallBack = new AsyncCallback((IAsyncResult Async_ResultLocal) =>
                //             {
                //                 try
                //                 {
                //                     ((Event_Handler_EventNotify)evInvoke).EndInvoke(Async_ResultLocal);
                //                 }
                //                 catch { }
                //             });
                //             ((Event_Handler_EventNotify)evInvoke).BeginInvoke(StEventNotify, AsynCallBack, this);
                //        }
                //     }
                // } 

                #endregion
            }
            catch (DLMSException ex)
            {
                byte[] TArray = null;
                int count = -1;

                if (Application_Process != null &&
                    Application_Process.ApplicationLayer != null)
                {
                    TArray = Application_Process.ApplicationLayer.Recieved_Packet;
                    if (TArray != null)
                        count = TArray.Length;
                }

                Logger.LogAPError(ex, TArray, 0, count, ReceivedOBISCode, PacketType.EventNotification);
            }
            catch (Exception ex)
            {
                // Extract Err Message
                String Error_message = ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : "NIL");
                Logger.LogAPMessage("Error Process Event Notification " + Error_message, PacketType.EventNotification);
            }
            finally
            {
            }
        }

        
        #region Packet Drop Handlers

        private void ApplicationLayer_PacketDrop(PacketType packetType, string errorMessage)
        {
            PacketDroped = true;
            ErrorMessage = errorMessage;
        }

        private void ApplicationLayer_PacketDrop(PacketType packetType, Exception ex)
        {
            PacketDroped = true;
            ErrorMessage = ex.Message;
            innerException = ex;
        }

        #endregion

        #endregion

        #region Member Methods

        /// <summary>
        /// provides particular DLMS_COSEM Class Instance/Object fully initialized,see also <see cref="GetSAPEntry"/> delegate
        /// </summary>
        /// <exception cref="DLMSException">Custom DLMS Exception Class</exception>
        /// <param name="ObisCode">OBIS Code for specific object to get<see cref="Get_Index"/></param>
        /// <returns>
        /// Base_Class Instance <see cref="Base_Class"/>
        /// </returns>       
        public Base_Class GET_Internal_Object(Get_Index ObisCode)
        {
            try
            {
                return Application_Process.GetSAPTableEntryDelegate.Invoke(ObisCode);
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Unable to retrieve object {0}", ObisCode), ex);
            }
        }

        /// <summary>
        /// GetOBISCode Function Returns The Mapped Actual OBIS Code Value 
        /// Against the Index referenced in Source Code
        /// </summary>
        /// <param name="OBIS_CODE"></param>
        /// <returns></returns>
        public StOBISCode GetOBISCode(Get_Index OBIS_CODE)
        {
            // Initialize
            StOBISCode Actual_OBIS_CODE = OBIS_CODE;

            #region OBIS LabelLookup

            try
            {
                // Skip Lookup Not Updated OBISLabelLookup Yet
                if (APSAPTable.OBISLabelLookup.Keys.Count <= 0)
                    Actual_OBIS_CODE = OBIS_CODE;
                else
                    Actual_OBIS_CODE = APSAPTable.OBISLabelLookup[OBIS_CODE];
            }
            catch
            {
                Actual_OBIS_CODE = OBIS_CODE;
                // Log Error Case
                // Logger.LogAPMessage(String.Format("{0} Actual OBIS Code Not GetOBISCode", OBIS_CODE.ToString()), PacketType.GET);
            }

            #endregion

            return Actual_OBIS_CODE;
        }


        /// <summary>
        /// provides particular DLMS_COSEM Class Instance/Object fully initialized,see also <see cref="GetSAPEntry"/> delegate
        /// </summary>
        /// <exception cref="DLMSException">DLMSException Class</exception>
        /// <param name="OBIS_CODE">COSEM Object OBISCode to be used<see cref="StOBISCode"/></param>
        /// <returns>
        /// Base_Class Instance <see cref="Base_Class"/>
        /// </returns>
        public Base_Class GetSAPEntry(StOBISCode OBIS_CODE)
        {
            // Initialize
            StOBISCode Actual_OBIS_CODE = OBIS_CODE;

            #region OBISLabelLookup

            try
            {
                // Skip Lookup Not Updated OBISLabelLookup Yet
                if (APSAPTable.OBISLabelLookup == null ||
                    APSAPTable.OBISLabelLookup.Keys.Count <= 0)
                    Actual_OBIS_CODE = OBIS_CODE;
                else
                    Actual_OBIS_CODE = APSAPTable.OBISLabelLookup[OBIS_CODE];
            }
            catch
            {
                Actual_OBIS_CODE = OBIS_CODE;
                // Log Error Case
                Logger.LogAPMessage(String.Format("{0}:Actual OBIS Code Not Found_GetSAPEntry", OBIS_CODE), PacketType.GET);
            }

            #endregion

            try
            {
                if (GetSAPEntryDlg != null)
                {
                    Base_Class obj = GetSAPEntryDlg.Invoke(new KeyIndexer(Actual_OBIS_CODE, OwnerThreadId));

                    // Base_Class obj = GetSAPEntryDlg.Invoke(new KeyIndexer(OBIS_CODE, OwnerThreadId));
                    if (obj != null)
                    {
                        try
                        {
                            obj.Rights = GetOBISAccessRights(Actual_OBIS_CODE);

                            #region // FIX LN 15 Access_Error

                            if (Actual_OBIS_CODE.ClassId == 15)
                            // &&  (obj.Rights.MethodRights.Count <= 0 ||
                            // obj.Rights.AttributeRights.Count <= 0))
                            {
                                obj.Grant_Attribute_Rights(0, Attrib_Access_Modes.Authenticated_Read_Write);
                                obj.Grant_Method_Rights(0, Method_Access_Modes.Authenticated_Access);
                            }

                            #endregion
                            #region // Process Rights

                            else if (obj.Rights != null)
                            {
                                // Either ByPass Access Rights
                                if (CheckAccessRights &&
                                    !obj.Rights.IsRightsLoaded)
                                    obj.Rights.CheckAccessRights = CheckAccessRights;
                                else if (!CheckAccessRights)
                                    obj.Rights.CheckAccessRights = CheckAccessRights;

                                // IsHLS Applicable
                                // Only For HLS Authentication Type
                                // & SecurityControl is not 'None'
                                if (ApplicationProcess.Send_APPLICATION_ASSOCIATION.
                                    Authentication_Mechanism >= HLS_Mechanism.HLS_Manufac &&
                                    Security_Data != null &&
                                    Security_Data.SecurityControl > SecurityControl.None)
                                    obj.Rights.IsHLSApplicable = true;
                                else
                                    obj.Rights.IsHLSApplicable = false;
                            }

                            #endregion

                        }
                        catch (Exception)
                        {
                            obj.Rights = new OBISCodeRights(obj.OBIS_CODE, obj.Class_ID, 0, new List<byte[]>(), new List<byte[]>());
                        }
                    }
                    return obj;
                }
                else
                    throw new DLMSException("Unable to create Base_Object,not initialized Properly_GetSAPEntry_ApplicationProcess_Controller");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// It will provide Access Rights<see cref="OBISCodeRights"/> for particular  Class Instance/Object
        /// <seealso cref="GetSAPRights"/> delegate
        /// </summary>
        ///<exception cref="DLMSException">DLMSException Class</exception>
        /// <param name="OBISCodeIndetifier">COSEM Object OBISCode to be used<see cref="StOBISCode"/></param>
        /// <returns>
        /// OBISCodeRights AccessRights Instance <see cref="OBISCodeRights"/>
        /// </returns>
        public OBISCodeRights GetOBISAccessRights(StOBISCode OBISCodeIndetifier)
        {
            try
            {
                if (ApplicationProcessSAPTable != null)
                    return this.ApplicationProcessSAPTable.GetOBISCodeRights(OBISCodeIndetifier);
                else
                {
                    return new OBISCodeRights();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unknown OBIS Code Rights Key {0}", OBISCodeIndetifier));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void SetResetIOStream(bool valueFlag)
        {
            isResetIOStream = valueFlag;
        }

        /// <summary>
        /// This Method will subscribe and attach all controllers to the Application Process Controller
        /// </summary>
        internal void Attach_Handlers()
        {
            try
            {
                if (Application_Process != null &&
                    !IsHandlerAttached)
                {
                    Application_Process.GetSAPTableEntryDelegate = GetSAPTableEntryDlg;      // GetSAPEntry;

                    if (ApplicationProcess.ApplicationLayer != null)
                    {
                        Application_Process.ApplicationLayer.PacketDrop += Application_Layer_PacketDropDlg;    // ApplicationLayer_PacketDrop;
                        Application_Process.ApplicationLayer.EventNotification_Packet_Decoded += EventNotification_Recieved; // EventNotification_Recieved;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while attaching handlers ApplicationProcess_Controller");
            }
            finally
            {
                IsHandlerAttached = true;
            }
        }

        /// <summary>
        /// This Method will Un-subscribe/detach all controllers 
        /// </summary>
        internal void Dettach_Handlers()
        {
            try
            {
                if (Application_Process != null && IsHandlerAttached)
                {
                    if (ApplicationProcess.ApplicationLayer != null)
                    {
                        Application_Process.ApplicationLayer.PacketDrop -= Application_Layer_PacketDropDlg;   // ApplicationLayer_PacketDrop;
                        Application_Process.ApplicationLayer.EventNotification_Packet_Decoded -= EventNotification_Recieved; // EventNotification_Recieved;
                    }
                }

                #region // Remove EventNotify Event Handlers

                Delegate[] Handlers = null;
                if (_EventNotify != null)
                {
                    Handlers = _EventNotify.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        EventNotify -= (Event_Handler_EventNotify)item;
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while detaching handlers ApplicationProcess_Controller", ex);
            }
            finally
            {
                IsHandlerAttached = false;
            }
        }

        #region ASync & Sync _IO_Related_Function

        #region Sync_IO_Related_Function

        /// <summary>
        /// This method send the Requested APDU on the physical layer
        /// </summary>
        /// <remarks>
        /// The Encoded APDU from COSEM Transport layer and to be transmitted on Physical Channel after any below layer Encapsulation required(AL Wrapper).
        /// This method is Sync Communication Model Implemented. 
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <param name="Encoded_Packet">Raw IO byte array encoded to be transmitted on channel</param>
        /// <param name="offsetTBF">IO Buffer start index</param>
        /// <param name="countTBF">IO buffer bytes length</param>
        public void SendRequestFromPhysicalLayer(byte[] Encoded_Packet, int offsetTBF, int countTBF)
        {
            try
            {
                if (GetCommunicationObject == null)
                    throw new ArgumentNullException("GetCommunicationObject");
                if (!GetCommunicationObject.IsConnected())
                {
                    // Reset Association Flag
                    Application_Process.Is_Association_Developed = false;
                   
                    throw new IOException(String.Format("Physical Channel is disconnected {0} (Error Code:{1})",
                        GetCommunicationObject, (int)DLMSErrors.IOChannel_Disconnected));
                }

                if (IsResetIOStream && GetCommunicationObject != null)
                {
                    GetCommunicationObject.ResetStream();
                    SetResetIOStream(false);
                }

                #region WriteDelay
#if Enable_WriteDelay

                // Write_Delay
                if (Write_Delay.TotalMilliseconds > 0 &&
                    Write_Delay.TotalMilliseconds <= 60000)
                {
                    // Thread.Sleep(Write_Delay);
                    DLMS_Common.Delay(Write_Delay);
                }

#endif
                #endregion

                GetCommunicationObject.SendRequestFromPhysicalLayer(Encoded_Packet, offsetTBF, countTBF);
            }
            catch (IOException)
            {
                SetResetIOStream(true);
                throw;
            }
            catch (Exception)
            {
                SetResetIOStream(true);
                // throw new Exception(String.Format("Error occurred while sending request on Physical Channel\r\nStack Trace: {0}", ex.StackTrace), ex);
                throw;
            }
        }

        /// <summary>
        /// Method receives Encoded APDU from physical layer and after removing encapsulation it transmit it to the above layer
        /// </summary>
        /// <remarks> 
        /// The Encoded_APDU from Physical Channel after removing AL COM Wrapper or any below layer Encapsulation, then APDU will pass to COSEM Application layer.
        /// This method is Sync Communication Model Implemented.
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <param name="Received_Packet">Raw IO byte array received from physical channel</param>
        /// <param name="offSetRBF">IO Buffer start index</param>
        /// <param name="CountRBF">IO buffer bytes length</param>
        /// <returns>int</returns>
        public int ReceiveResponseFromPhysicalLayer(byte[] Received_Packet, int offSetRBF, int CountRBF)
        {
            int byteReadCount = -1;
            try
            {
                if (GetCommunicationObject == null)
                    throw new ArgumentNullException("GetCommunicationObject");
                if (!GetCommunicationObject.IsConnected())
                {
                    // Reset Association Flag
                    Application_Process.Is_Association_Developed = false;

                    throw new IOException(String.Format("Physical Channel is disconnected {0} (Error Code:{1})",
                        GetCommunicationObject, (int)DLMSErrors.IOChannel_Disconnected));
                }

                byteReadCount = GetCommunicationObject.ReceiveResponseFromPhysicalLayer(ref Received_Packet, offSetRBF, CountRBF);
                return byteReadCount;
            }
            catch (IOException ex)
            {
                SetResetIOStream(true);
                throw ex;
            }
            catch (Exception ex)
            {
                SetResetIOStream(true);
                throw new Exception(String.Format("Error occurred while receiving response from Physical Channel\r\n"), ex);
            }
        }

        #endregion

        #region ASync_IO_Related_Function

        /// <summary>
        /// DLMS Event Notification + Data Notification Service Data Handler
        /// </summary>
        /// <param name="buf"></param>
        public void GetIODataAsync(ArraySegment<byte> buf)
        {
            bool isTagMatch = false;

            try
            {
                // Process Secure Packet Here
                if (Security_Context.IsSecurityApplicable(buf.Array[buf.Offset + 0]))
                {
                    if (_Security_Data == null || !_Security_Data.IsInitialized || Crypto == null)
                    {
                        throw new CryptoException("Message Security Not Available or Initialized Properly _GetIODataAsync");
                    }

                    buf = Security_Context.Process_HLS_APDU(_Security_Data, _Cypher, buf, out isTagMatch);
                }

                #region Debugging & Logging
#if Enable_IO_Logging

                if (ApplicationProcess.Logger != null)
                    // Log Transmitted Data
                    ApplicationProcess.Logger.LogALTraffic(buf, DataStatus.Read);
#endif
                #endregion

                Application_Process.ApplicationLayer.packetReceived(buf);
            }
            catch (Exception ex)
            {
                Logger.LogAPMessage("Error Processing DLMS Notification Service Message \r\n" + ex.Message, PacketType.EventNotification);
            }
        }

        /// <summary>
        /// This method send the Requested APDU on the physical layer Asynchronously
        /// </summary>
        /// <remarks>
        /// This method is Task Based Async Communication Model Implemented. 
        /// The "Encoded APDU" from COSEM Transport layer and to be transmitted
        /// on Physical Channel after any below layer Encapsulation required(AL Wrapper) 
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <param name="Encoded_Packet">Raw IO byte array encoded to be transmitted on channel</param>
        /// <param name="offsetTBF">IO Buffer start index</param>
        /// <param name="countTBF">IO buffer bytes length</param>
        /// <returns>Task&lt;int&gt;</returns>
        public async Task<int> SendRequestFromPhysicalLayerAsync(byte[] Encoded_Packet, int offsetTBF, int countTBF)
        {
            Task T = null;
            try
            {
                if (GetCommunicationObject == null)
                    throw new ArgumentNullException("GetCommunicationObject");
                if (!GetCommunicationObject.IsConnected())
                {
                    // Reset Association Flag
                    Application_Process.Is_Association_Developed = false;

                    throw new IOException(String.Format("Physical Channel is disconnected {0} (Error Code:{1})",
                                          GetCommunicationObject, (int)DLMSErrors.IOChannel_Disconnected));
                }

                if (IsResetIOStream && GetCommunicationObject != null)
                {
                    GetCommunicationObject.ResetStream();
                    SetResetIOStream(false);
                }

                #region WriteDelay
#if Enable_WriteDelay

                // Write_Delay
                if (Write_Delay.TotalMilliseconds > 0 &&
                    Write_Delay.TotalMilliseconds <= 60000)
                {
                    // Thread.Sleep(Write_Delay);
                    // DLMS_Common.PreciseDelay(Write_Delay.TotalMilliseconds);
                    await Task.Delay(Write_Delay);
                }

#endif
                #endregion

                T = GetCommunicationObject.SendRequestFromPhysicalLayerASync(Encoded_Packet, offsetTBF, countTBF);
                await T;
                return countTBF;
            }
            catch (IOException ex)
            {
                SetResetIOStream(true);
                throw ex;
            }
            catch (Exception ex)
            {
                SetResetIOStream(true);
                throw new Exception(String.Format("Error occurred while sending request on Physical Channel\r\nStack Trace: {0}", ex.StackTrace), ex);
            }
            finally
            {
                try
                {
                    if (T != null)
                        T.Dispose();
                }
                catch { }
            }
        }

        /// <summary>
        /// Method receives Encoded APDU from physical layer and after removing encapsulation it transmit it to the above layer Asynchronously
        /// </summary>
        /// <remarks>
        /// This method is Task Based Async Communication Model Implemented. 
        /// The "Encoded APDU" from Physical Channel after removing AL COM Wrapper or any below layer Encapsulation, 
        /// Then APDU will pass to COSEM Application layer
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <param name="Received_Packet">Raw IO byte array received from physical channel</param>
        /// <param name="offSetRBF">IO Buffer start index</param>
        /// <param name="CountRBF">IO buffer bytes length</param>
        /// <returns>IAsyncResult</returns>
        public async Task<int> ReceiveResponseFromPhysicalLayerAsync(byte[] Received_Packet, int offSetRBF, int CountRBF)
        {
            Task<int> AwaitTask = null;
            try
            {
                if (GetCommunicationObject == null)
                    throw new ArgumentNullException("GetCommunicationObject");
                if (!GetCommunicationObject.IsConnected())
                {
                    // Reset Association Flag
                    Application_Process.Is_Association_Developed = false;

                    throw new IOException(String.Format("Physical Channel is disconnected {0} (Error Code:{1})",
                                          GetCommunicationObject, (int)DLMSErrors.IOChannel_Disconnected));
                }

                return await GetCommunicationObject.ReceiveResponseFromPhysicalLayerASync(Received_Packet, offSetRBF, CountRBF);
            }
            catch (IOException ex)
            {
                SetResetIOStream(true);
                throw ex;
            }
            catch (Exception ex)
            {
                SetResetIOStream(true);
                throw new Exception(String.Format("Error occurred while receiving response from Physical Channel\r\nStack Trace: {0}", ex.StackTrace), ex);
            }
            finally
            {
                try
                {
                    if (AwaitTask != null)
                        AwaitTask.Dispose();
                }
                catch { }
            }
        }

        #endregion

        #endregion

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                Dettach_Handlers();

                GetCommunicationObject = null;
                if (Application_Process != null)
                {
                    Application_Process.Dispose();
                }

                GetSubController = null;
                _AARQSubController = null;
                _ARLRQSubController = null;
                _SETSubController = null;
                _InvokeSubController = null;
            }
            catch (Exception ex)
            { }
        }

        #endregion

        ~ApplicationProcess_Controller()
        {
            Dispose();
        }
    }
}
