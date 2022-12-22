using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DLMS;
using DLMS.Comm;
//using ReadSMS_AT_CS20;
using Serenity.Crypto;
using SharedCode.Comm.DataContainer;
using DatabaseConfiguration.DataSet;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SharedCode.Comm.Param;
using SharedCode.Others;
using SharedCode.TCP_Communication;
using _HDLC;
using System.Threading.Tasks;
using System.Security;

namespace SharedCode.Controllers
{

    public class ConnectionController
    {
        #region Data_Members

        public event Action<String> ProcessStatus = delegate { };
        private ApplicationProcess_Controller _AP_Controller;
        private IOConnection currentConnection;

        private HDLC _HDLCConnection;
        private NetworkStream networkStream;
        private InitHDLCParam _InitHDLC_Params;

        public ManualResetEvent wakeUpConnWait;
        private LogHelper connectionLog;

        internal SAP_Object meterlogicalDevice = null, clientSAP = null;

        public static readonly SAP_Object ManagementDevice = null;
        public static readonly SAP_Object Public = null;

        private ApplicationController _AplicationController;
        private ParameterController _Parameter_Controller;
        private ConnectionManager _connectionManager;
        private Configs configurations;

        private BindingList<IOConnection> meterSerial;
        private bool resetState = false;

        private InitParamsHelper _InitParam;
        private InitTCPParams _TCPParams;
        private MeterConfig selectedMeter;
        private Security_Data _securityData;
        private bool isConfigurationUpated = false;
        private St_FirmwareInfo st_meter_Info = null;
        private IOConnectionType connectionType;
        private InitCommuincationParams _CommParams;
        private InitHDLCParam _hdlcParam;
        private InitHDLCParam _directHdlcParam;
        private ConfigsHelper configurationHelper;

        #endregion

        #region Properties
        public ConfigsHelper ConfigurationHelper
        {
            get { return configurationHelper; }
            set { configurationHelper = value; }
        }

        public InitHDLCParam HdlcParam
        {
            get
            {
                try
                {
                    _hdlcParam = _InitParam.LoadHDLCParams();
                }
                catch (Exception)
                {
                    _hdlcParam = _InitParam.GetDefaultHDLCParams();
                }
                return _hdlcParam;
            }
            set { _hdlcParam = value; }
        }
        public InitHDLCParam DirectHdlcParam
        {
            get
            {
                try
                {
                    _directHdlcParam = _InitParam.LoadHDLCParams();
                }
                catch (Exception)
                {
                    _directHdlcParam = _InitParam.GetDefaultHDLCParams();
                }
                return _directHdlcParam;
            }
            set { _directHdlcParam = value; }
        }
        public InitCommuincationParams CommunicationParams
        {
            get
            {
                try
                {
                    _CommParams = _InitParam.LoadCommunicationParams();
                }
                catch (Exception)
                {
                    _CommParams = _InitParam.GetDefaultCommunicationParams();
                }
                return _CommParams;
            }
            set { _CommParams = value; }
        }

        public IOConnectionType ConnectionType
        {
            get { return connectionType; }
            set { connectionType = value; }
        }

        public bool IsConfigurationUpated
        {
            get { return isConfigurationUpated; }
        }

        public Security_Data SecurityData
        {
            get { return _securityData; }
            set { _securityData = value; }
        }
        public MeterConfig SelectedMeter
        {
            get { return selectedMeter; }
            set { selectedMeter = value; }
        }

        public DeviceAssociation CurrentAssociation
        {
            get { return SelectedMeter.Device_Association; }
        }

        public InitParamsHelper InitParam
        {
            get { return _InitParam; }
            set { _InitParam = value; }
        }

        public InitTCPParams TCPParams
        {
            get
            {
                try
                {
                    _TCPParams = _InitParam.LoadTCPParams();
                }
                catch (Exception)
                {
                    _TCPParams = _InitParam.GetDefaultTCPParams();
                }
                return _TCPParams;
            }
            set { _TCPParams = value; }
        }

        public bool ResetState
        {
            get { return resetState; }
            set { resetState = value; }
        }

        public Configs Configurations
        {
            get { return configurations; }
            set { configurations = value; }
        }

        public ApplicationController AplicationController
        {
            get { return _AplicationController; }
            set { _AplicationController = value; }
        }

        public ConnectionManager ConnectionManager
        {
            get { return _connectionManager; }
            set
            {
                _connectionManager = value;
                AttachHandler();
            }
        }

        public ParameterController Parameter_Controller
        {
            get { return _Parameter_Controller; }
            set { _Parameter_Controller = value; }
        }

        public ApplicationProcess_Controller AP_Controller
        {
            get { return _AP_Controller; }
            set { _AP_Controller = value; }
        }

        public IOConnection CurrentConnection
        {
            get { return currentConnection; }
            set { currentConnection = value; }
        }

        public InitHDLCParam InitHDLC_Params
        {
            get { return _InitHDLC_Params; }
            set { _InitHDLC_Params = value; }
        }

        public bool IsConnected
        {
            get
            {
                return (AP_Controller.IsConnected);
            }
        }

        public HDLC HDLCConnection
        {
            get { return _HDLCConnection; }
        }

        public LLCProtocolType ProtocolType
        {
            get;
            set;
        }

        public BindingList<IOConnection> MeterSerial
        {
            get { return meterSerial; }
            set { meterSerial = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Connection Controller Default Constructor
        /// </summary>
        public ConnectionController()
        {

            ProtocolType = LLCProtocolType.Not_Assigned;
            _InitParam = new InitParamsHelper();
            _InitHDLC_Params = _InitParam.GetDefaultHDLCParams();

            connectionLog = new LogHelper();
            wakeUpConnWait = new ManualResetEvent(false);
            ConnectionType = IOConnectionType.HDLC_MODE_E;
            selectedMeter = new MeterConfig();
            ///Load Init Params
            try
            {
                meterSerial = new BindingList<IOConnection>();
                _CommParams = _InitParam.LoadCommunicationParams();
                _hdlcParam = _InitParam.LoadHDLCParams();
                _TCPParams = _InitParam.LoadTCPParams();
            }
            catch (Exception)
            {
                _CommParams = _InitParam.GetDefaultCommunicationParams();
                _hdlcParam = _InitParam.GetDefaultHDLCParams();
                _TCPParams = _InitParam.GetDefaultTCPParams();
            }
        }

        static ConnectionController()
        {
            ManagementDevice = new SAP_Object("MTI0R326G0000001", 0x01);
            Public = new SAP_Object("Public", 0x10);
            // ManagementDevice.ClientSap = Public;
        }

        #endregion

        #region Disconnect Methods
        /// <summary>
        /// Try to disconnect application any ways
        /// </summary>
        /// <param name="forcedly"></param>
        public void Disconnect(bool forcedly)
        {
            try
            {
                AP_Controller.ApplicationProcess.Is_Association_Developed = false;
                if (ConnectionManager != null)
                {
                    ConnectionManager.TCP_CONController.DisConnectServer();
                    ConnectionManager.OpticalPortConnection.DisConnectHdlc();
                    ConnectionManager.Disconnect();
                    ConnectionManager.DisconnectedIOList.Clear();
                    if (MeterSerial != null)
                        MeterSerial.Clear();
                    ConnectionManager.LastHeartBeatReceived = null;
                    ConnectionManager.LastConnectionInfo = ConnectionManager.ConnectionInfo;
                    ResetState = true;
                }
                ///this.Disconnect();
            }
            catch (Exception ex)
            { }
            finally
            {
                ///Release All IO Resoucres Here
                try
                {
                    if (AplicationController.ConnectionManager != null)
                    {
                        AplicationController.ConnectionManager.LastConnectionInfo =
                            AplicationController.ConnectionManager.ConnectionInfo;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void Disconnect_Old()
        {
            IConnection Conn = AP_Controller.GetCommunicationObject;
            try
            {
                bool flag = false;
                #region AP_Controller.IsConnected

                if (AP_Controller.IsConnected)
                /// Normal Disconnect Call
                {
                    try
                    {
                        this.ProcessStatus.Invoke("Sending Application Association Release Request");
                        flag = AP_Controller.ARLRQ();
                        String LogTxt = String.Format("Application_Process release request Exec,{0} IO Channel Connection Disconnected", 1);
                        connectionLog.LogConnection(LogTxt);
                        if (Conn is IOConnection)
                        {
                            IOConnection Connection = (IOConnection)Conn;
                            if (Connection.CurrentConnection == PhysicalConnectionType.OpticalPort)
                                Conn.Disconnect();
                            else
                            {
                                if (Connection.ConnectionInfo != null)
                                    Connection.ConnectionInfo.IsConnected = false;
                            }
                        }
                        this.ProcessStatus.Invoke("Application is disconnected");
                    }
                    catch (Exception ex)
                    {
                        AP_Controller.ApplicationProcess.Is_Association_Developed = false; /// Forcefully Disconnect Server AP
                        if (Conn != null)
                            Conn.Disconnect();
                        throw ex;
                    }
                }

                #endregion // AP_Controller.IsConnected
                else
                {
                    try
                    {
                        AP_Controller.ApplicationProcess.Is_Association_Developed = false; // Forcefully Disconnect Server AP

                        if (Conn is IOConnection && Conn.IsConnected())
                        {
                            IOConnection Connection = (IOConnection)Conn;
                            if (Connection.CurrentConnection == PhysicalConnectionType.OpticalPort)
                                Conn.Disconnect();

                            this.ProcessStatus.Invoke("IO Channel disconnected");
                            String LogTxt = String.Format("IO Channel Disconnected");
                            connectionLog.LogConnection(LogTxt);
                        }
                    }
                    catch (Exception ex)
                    {
                        AP_Controller.ApplicationProcess.Is_Association_Developed = false; // Forcefully Disconnect Server AP
                        if (Conn != null)
                            Conn.Disconnect();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                if (AP_Controller.ApplicationProcess != null)
                    AP_Controller.ApplicationProcess.Is_Association_Developed = false;
                if (Conn != null)
                    Conn.Disconnect();
                throw ex;
            }
            finally
            {
                // Check Either Connected to same meter
                if (AplicationController.ConnectionManager != null)
                {
                    AplicationController.ConnectionManager.LastConnectionInfo =
                        AplicationController.ConnectionManager.ConnectionInfo;
                }
            }
        }
        public void Disconnect()
        {
            IConnection Conn = null;

            try
            {
                Conn = AP_Controller.GetCommunicationObject;

                bool flag = false;
                {
                    try
                    {
                        if (AP_Controller.IsConnected)
                            flag = AP_Controller.ARLRQ();

                        if (Conn != null)
                        {
                            if (ProtocolType == LLCProtocolType.Direct_HDLC)
                            {
                                (Conn as IOConnection).Disconnect();
                                // (Conn as IOConnection).OverlayStream = null;
                            }

                            // Remove All Previous ASyn Handlers
                            //  Conn.ReceiveDataFromPhysicalLayerASync = null;

                            // Release Resource
                            Conn.ResetStream();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is DLMSException)
                            throw new DLMSException(string.Format("Error Code:{0}", (int)MDCErrors.App_Association_Release));

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AP_Controller.ApplicationProcess.Is_Association_Developed = false;
                // Forcefully Disconnect Server AP
            }
        }
        public void Disconnect_Fast()
        {
            IConnection Conn = null;

            try
            {
                Conn = AP_Controller.GetCommunicationObject;

                if (AP_Controller.IsConnected)
                {
                    var tk = AP_Controller.ARLRQAsync();
                    // Wait Before Logout Transmits
                    tk.Wait(TimeSpan.FromSeconds(10.0d));
                }

                if (Conn != null)
                {
                    // Release Resource
                    Conn.ResetStream();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Forcefully Disconnect Server AP
                AP_Controller.ApplicationProcess.Is_Association_Developed = false;
            }
        } 
        #endregion

        #region Member_Methods

        #region Connect_IR_Link

        public ConnectionInfo ReadMeterInfo()
        {
            ConnectionInfo CurrentConnInfo = null;
            try
            {
                CurrentConnInfo = new ConnectionInfo();
                CurrentConnInfo.MeterInfo = SelectedMeter;
                ReadMeterInfo(CurrentConnInfo);
                return CurrentConnInfo;
            }
            catch (Exception ex)
            {
                CurrentConnInfo.IsConnected = false;
                throw ex;
            }
            finally
            {

            }
        }

        public void ReadMeterInfo(ConnectionInfo CurrentConnInfo)
        {
            try
            {
                MeterConfig CurrentMeterConfig = null;
                DeviceAssociation CurrentAssociation = null;

                if (CurrentConnInfo == null ||
                    CurrentConnInfo.MeterInfo == null)
                {
                    throw new ArgumentNullException("Meter_Config",
                                                    "Unable to read meter info,Current Connection Info not Initialize");
                }

                // Reset Previous IOMonitorLog
                if (this.ConnectionManager.ConnectionMonitor != null)
                {
                    this.ConnectionManager.ConnectionMonitor.Manual_Session_Reset();
                }

                ConfigsHelper Configurator = new ConfigsHelper(Configurations);
                MeterSerialNumber msn = null;

                #region // st_meter_Info

                st_meter_Info = null;

                try
                {
                    st_meter_Info = _Parameter_Controller.GetStFirmwareInfo();
                    msn = CurrentConnInfo.MeterSerialNumberObj = st_meter_Info.MSN_Info;
                    CurrentConnInfo.MeterInfo.Version = st_meter_Info.Version + "_" + st_meter_Info.Release_ID;
                    CurrentConnInfo.MeterInfo.MeterModelSTR = st_meter_Info.Model;
                    _AP_Controller.ApplicationProcess.Logger.Identifier = st_meter_Info.MSN_Info.ToString();
                }
                catch
                {
                    st_meter_Info = new St_FirmwareInfo();
                }

                try
                {
                    if (msn == null || this.st_meter_Info.MSN_Info == null ||
                        msn.MSN != this.st_meter_Info.MSN_Info.MSN)
                    {
                        ProcessStatus.Invoke("Reading meter serial no (MSN) ");
                        msn = ReadMeterSerialNumber();
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception("Error reading meter serial number(MSN)", ex);
                }

                #endregion
                #region // Meter Firmware Version

                try
                {
                    if (this.st_meter_Info == null || string.IsNullOrEmpty(this.st_meter_Info.Version))
                    {
                        ProcessStatus.Invoke("Reading meter firmware version ");

                        this.st_meter_Info.Version = ReadFirmwareVersion();
                        this.st_meter_Info.MSN_Info = msn;
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception("Error reading meter serial number(MSN)", ex);
                }

                #endregion

                int CurrentMeterSAP = -1;
                int CurrentClientSAP = -1;

                #region Loading Meter_Configuration

                connectionLog.LogConnection("Trying to load meter configuration");

                try
                {
                    CurrentMeterConfig = CurrentConnInfo.MeterInfo;
                    CurrentAssociation = CurrentMeterConfig.Device_Association;

                    if (CurrentMeterConfig == null || CurrentAssociation == null)
                    {
                        throw new Exception("Unable to read meter info,MeterConfig and CurrentAssociation Not Initiailze");
                    }

                    CurrentMeterSAP = CurrentAssociation.MeterSap;
                    CurrentClientSAP = CurrentAssociation.ClientSap;
                }
                catch (Exception ex)
                {
                    connectionLog.LogConnection("Error to load meter configuration," + ex.ToString());
                    throw ex;
                }

                #endregion

                CurrentMeterConfig.Version = st_meter_Info.Version;
                CurrentMeterConfig.MeterModelSTR = st_meter_Info.Model;

                // Save Info In Meter SAPs
                CurrentConnInfo.MeterInfo = CurrentMeterConfig;
                // Saving Connection Info
                CurrentConnInfo.MeterSerialNumberObj = msn;
                CurrentConnInfo.MeterInfo.Version = st_meter_Info.Version;
                CurrentConnInfo.MeterInfo.DLMSVersion = st_meter_Info.DLMS_Version;
                CurrentConnInfo.MeterInfo.MeterModelSTR = st_meter_Info.Model;
                ConnectionManager.ConnectionInfo = CurrentConnInfo;
                this.ProcessStatus.Invoke("Application Connected Successfuly");
                CurrentConnInfo.LastActivity = DateTime.Now;

            }
            catch (Exception ex)
            {
                CurrentConnInfo.IsConnected = false;
                throw ex;
            }
            finally
            {
                CurrentConnInfo.IsConnected = AP_Controller.ApplicationProcess.Is_Association_Developed;

                AplicationController.ConnectionManager.ConnectionInfo = CurrentConnInfo;
                AplicationController.Billing_Controller.CurrentConnectionInfo = CurrentConnInfo;
                AplicationController.EventController.CurrentConnectionInfo = CurrentConnInfo;
                AplicationController.Param_Controller.CurrentConnectionInfo = CurrentConnInfo;
                AplicationController.LoadProfile_Controller.CurrentConnectionInfo = CurrentConnInfo;
                AplicationController.ConnectionManager.ConnectionInfo = CurrentConnInfo;
                AplicationController.InstantaneousController.ConnectionInfo = CurrentConnInfo;

                if (AplicationController.ConnectToMeter == null)
                    AplicationController.ConnectToMeter = (IOConnection)AP_Controller.GetCommunicationObject;
            }
        }

        private void ConnectMeter(ConnectionInfo CurrentConnectionInfo, String Password, DeviceAssociation AssociationDetail, IOConnection Conn, bool isLogoutFlag = false)
        {
            WrapperLayer WPObj = new WrapperLayer(AssociationDetail.MeterSap, AssociationDetail.ClientSap);
            Conn.TCPWrapper = WPObj;
            currentConnection = Conn;
            #region Connect Application Process

            uint Software_UserId = 0;

            // Skip Software User Id
            if (SelectedMeter != null &&
                SelectedMeter.Device != null &&
                SelectedMeter.Device.SkipSoftwareUserID)
            {
                Software_UserId = AP_Controller.SoftwareUserId;
                // Skip Software User Id
                AP_Controller.SoftwareUserId = 0;
            }

            // Only Reconnect If Not Public Login
            if (AssociationDetail.AuthenticationType >= HLS_Mechanism.HLS_Manufac)
            {

                /// Load Security Configuration
                //if (AP_Controller.Security_Data == null || !AP_Controller.Security_Data.IsInitialized)
                //{
                //    this.SecurityData = InitSecurityData(CurrentConnectionInfo.MSN);
                //}

                /// Start Authentication Process
                //GMAC_Authentication(AP_Controller.Security_Data);

                AP_Controller.Security_Data = this.SecurityData;
                // Start Authentication Process
                ProcessStatus.Invoke(string.Format("{0} Association Requesting...",
                                     AssociationDetail.AuthenticationType));

                try
                {

                    if (isLogoutFlag)
                        AP_Controller.ARLRQ(AssociationDetail.MeterSap, AssociationDetail.ClientSap);
                    else
                       HLS_Authentication(AP_Controller.Security_Data, AssociationDetail);
                }
                finally
                {
                    if (Software_UserId != 0 && AP_Controller != null)
                        AP_Controller.SoftwareUserId = Software_UserId;
                }
            }
            else
            {
                AP_Controller.Security_Data = null;
                ProcessStatus.Invoke("Private Login Requesting...");

                bool flag = false;

                try
                {
                    if(isLogoutFlag)
                        flag = AP_Controller.ARLRQ(AssociationDetail.MeterSap, AssociationDetail.ClientSap);
                    else
                        flag = AP_Controller.AARQ(AssociationDetail.MeterSap, AssociationDetail.ClientSap, Password);

                }
                finally
                {
                    if (Software_UserId != 0 && AP_Controller != null)
                        AP_Controller.SoftwareUserId = Software_UserId;
                }

                if (!flag)
                {
                    if (Conn.CurrentConnection == PhysicalConnectionType.OpticalPort)
                        Conn.Disconnect();
                    throw new Exception("Unable to login into meter");
                }
            }

            #endregion

            if (AP_Controller.ApplicationProcess.Is_Association_Developed)
            {
                ProcessStatus.Invoke("Reading Meter Info...");
                ReadMeterInfo(CurrentConnectionInfo);
                // Log Application Connected 
                String txt = String.Format("Application Connected with Server Sap {0} and Client Sap {1}",
                                            AssociationDetail.MeterSap, AssociationDetail.ClientSap);

                connectionLog.LogConnection(txt);
                // Conn.ConnectionInfo = ReadMeterInfo();
                // ProcessStatus.Invoke("Application Connected Successfully");
            }
        }


        private void DownLoadRights(DeviceAssociation AssociationDetail, string connectionString)
        {
            #region Load Access Rights

            ConfigsHelper Configurator = new ConfigsHelper(Configurations);
            // Configurations.Configuration.CurrentConfiguration = AssociationDetail.Configuration;
            // Select Default Configuration
            Configurator.SelectDefaultConfiguration(AssociationDetail.ConfigurationId);
            List<OBISCodeRights> OBISCodeRightsList = Configurator.GetCurrentSAPOBISRightsIOP(AssociationDetail.RightGroupId, AssociationDetail.DeviceId);
            List<KeyValuePair<StOBISCode, StOBISCode>> OBISCodeMapper = null;

            Configs.Obis_Rights_GroupRow rightGroup = Configurator.GetObisRightGroup(AssociationDetail.RightGroupId);

            if (OBISCodeRightsList == null || OBISCodeRightsList.Count == 0 || rightGroup.Update_Rights)
            {
                ProcessStatus.Invoke("Starting meter configurations download process");
                Class_15 OBJSAP = (Class_15)AP_Controller.GET(Get_Index.Current_Association, 2);    ///temp Request Access Rights
                OBISCodeRightsList = OBJSAP.OBISCodesReceived;

                // Save Access Rights
                Configurator.SaveCurrentSAPOBISRights(AssociationDetail.RightGroupId, AssociationDetail.DeviceId,
                    OBISCodeRightsList, connectionString);

                Configurator.SaveConfigurations(connectionString);
                this.ProcessStatus.Invoke("Configurations being saved");

                rightGroup.Update_Rights = false;
                Configs Conf = Configurations;

                //this.SaveConfigurations(Conf);
            }

            OBISCodeMapper = Configurator.GetOBISCodesMapByDeviceId(AssociationDetail.DeviceId);

            _AplicationController.PopulateOBISCodesMap(OBISCodeMapper);
            _AplicationController.PopulateSAPTable(OBISCodeRightsList);
            isConfigurationUpated = true;

            #endregion
        }

        private bool verifyCOMPort(String comPort)
        {
            try
            {
                String[] ComPorts = SerialPort.GetPortNames();
                foreach (var item in ComPorts)
                {
                    if (item.Equals(comPort))
                        return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Connect_IRLinkWithRights(String Password, string COMPort,
            DeviceAssociation AssociationDetail, string connectionString, ushort hdlcAddress, bool isLogoutFlag = false)
        {
            try
            {
                MeterConnection(Password, COMPort, AssociationDetail,hdlcAddress, isLogoutFlag);
                DownLoadRights(AssociationDetail, connectionString);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Connect_IRLinkSimple(String Password, string COMPort, DeviceAssociation AssociationDetail,ushort hdlcAddress, string connectionString)
        {
            try
            {
                MeterConnection(Password, COMPort, AssociationDetail, hdlcAddress);
                DownLoadRights(AssociationDetail, connectionString);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        void MeterConnection(string Password, string COMPort, DeviceAssociation AssociationDetail,ushort hdlcAddress, bool isLogoutFlag = false)
        {
            IOConnection Conn = null;
            ConnectionInfo CurrentConnectionInfo = new ConnectionInfo();
            try
            {
                if (!verifyCOMPort(COMPort))
                {
                    throw new Exception(String.Format("Invalid COM Port {0} Selected", COMPort));
                }
                else
                {
                    _CommParams.IRCOMPort = COMPort;
                    _CommParams.IRCOMBaudRate = 9600;
                    InitParam.SaveCommuincationParams(_CommParams);
                }

                String ComPort = CommunicationParams.IRCOMPort;
                int baudRate = CommunicationParams.IRCOMBaudRate;

                #region Initialize HDLC Connection

                ProcessStatus.Invoke(".....................................");
                ProcessStatus.Invoke($"Initializing the IR Port configuration: {COMPort}");
                ConnectionManager.OpticalPortConnection.InitHDLCParams(HdlcParam);
                TimeSpan dataReadOutTime = CommunicationParams.DataReadTimeout;
                if (dataReadOutTime != null)
                    ConnectionManager.DataReadTimeOut = dataReadOutTime;

                InitHDLCParam tempParam;
                if (ConnectionType == IOConnectionType.HDLC_MODE_E)
                {
                    tempParam = HdlcParam;
                    tempParam.DeviceAddress = hdlcAddress;
                    _InitParam.SaveHDLCParams(tempParam);
                    ConnectionManager.OpticalPortConnection.InitHDLCParams(tempParam);


                    if(!isLogoutFlag)
                    {
                        ProcessStatus.Invoke("Connecting HDLC_Mode_E on IR Port");
                        ConnectionManager.ConnectHDLC_ModeE(ComPort, AssociationDetail.MeterSap, AssociationDetail.ClientSap);
                    }
                    else
                    {
                        ConnectionManager.OpticalPortConnection.InitPort(ComPort);
                    }

                    // Add Physical Connection Object
                    Conn = ConnectionManager.GetIOConnection(ConnectionManager.OpticalPortConnection.HDLCConnection_.BaseStream);
                    ConnectionManager.AddIOConnection(Conn);
                    Conn.IOTrafficLogger = AP_Controller.ApplicationProcess.Logger;
                }
                else if (ConnectionType == IOConnectionType.Direct_HDLC)
                {
                    ProcessStatus.Invoke(String.Format("Connecting Direct HDLC On COM Port {0} using BaudRate {1}",
                                                                        ComPort, CommunicationParams.IRCOMBaudRate));
                    tempParam = DirectHdlcParam;
                    tempParam.DeviceAddress = hdlcAddress;
                    _InitParam.SaveHDLCParams(tempParam);
                    ConnectionManager.ConnectDirectHDLC(ComPort, CommunicationParams.IRCOMBaudRate, AssociationDetail.MeterSap, AssociationDetail.ClientSap);

                    // Add Physical Connection Object
                    Conn = ConnectionManager.GetIOConnection(ConnectionManager.OpticalPortConnection.HDLCConnection_.BaseStream);
                    ConnectionManager.AddIOConnection(Conn);
                    Conn.IOTrafficLogger = AP_Controller.ApplicationProcess.Logger;
                }
                else if (ConnectionType == IOConnectionType.IP_Ov_Serial_Link)
                {
                    ProcessStatus.Invoke("Connecting IP over Serial Port Physical Link");
                    SerialPort LocalSPort = ConnectionManager.OpticalPortConnection.ConnectIPOverSerialLink(ComPort, baudRate);
                    Stream IOStream = new TCPOverSerial(LocalSPort);

                    // Add Physical Connection Object
                    Conn = ConnectionManager.GetIOConnection(IOStream);
                    Conn.DataReadTimeOut = dataReadOutTime;
                    ConnectionManager.AddIOConnection(Conn);

                    int tmpBufferSize = 1024;
                    // Initiate TCP Wrapper Object
                    Conn.IOTrafficLogger = AP_Controller.ApplicationProcess.Logger;
                    AP_Controller.GetCommunicationObject = Conn;
                    Conn.InitBuffer(tmpBufferSize, tmpBufferSize, this.ConnectionManager.CreateDataReaderBuffer);
                }

                #endregion
                #region Initial Application Process

                if (!Conn.IsConnected && !isLogoutFlag)
                    throw new Exception("Unable to connect on IR port");
                /// Initiate Application New Work
                AP_Controller.GetCommunicationObject = Conn;
                AP_Controller.MaxLocalBuffer = Conn.MaxReadBufferSize;
                // Initial Application New Work
                //AP_Controller.SynCommunicateData = new SynCommData(Conn.ReceiveDataFromPhysicalLayer);
                //AP_Controller.AsyncCommunicateData = new BeginCommunicateData(Conn.BeginReceiveDataFromPhysicalLayer);
                //AP_Controller.EndAsyncCommunicateData = new EndCommunicateData(Conn.EndReceiveDataFromPhysicalLayer);

                Conn.IOTrafficLogger = AP_Controller.ApplicationProcess.Logger;

                #endregion

                CurrentConnectionInfo.MeterInfo = new MeterConfig(SelectedMeter);
                Conn.ConnectionInfo = CurrentConnectionInfo;
                
                ConnectMeter(CurrentConnectionInfo, Password, AssociationDetail, Conn , isLogoutFlag );
            }
            catch (Exception ex)
            {
                if (Conn != null && Conn.IsConnected)
                {
                    Conn.Disconnect();
                }
                throw ex;
            }
            finally
            {
                CurrentConnectionInfo.IsConnected = AP_Controller.ApplicationProcess.Is_Association_Developed;
                AplicationController.ConnectionManager.ConnectionInfo = CurrentConnectionInfo;
            }
        }

        public void Connect_IRLink(String Password, string connectionString, ushort hdlcAddress)
        {
            IOConnection Conn = null;
            try
            {
                String ComPort = CommunicationParams.IRCOMPort;
                // int baudRate = CommunicationParams.IRCOMBaudRate;

                if (SelectedMeter == null || SelectedMeter.Device_Association == null)
                    throw new ArgumentNullException("DeviceAssociation");

                Connect_IRLinkWithRights(Password, ComPort, SelectedMeter.Device_Association,
                                         connectionString,hdlcAddress);
            }
            catch (Exception ex)
            {
                if (Conn != null && Conn.IsConnected)
                {
                    Conn.Disconnect();
                }
                throw ex;
            }
        }

        public void Connect_IRLink(String Password, String COMPort, string connectionString,ushort hdlcAddress)
        {
            try
            {
                ///Update Current Port
                if (verifyCOMPort(COMPort))
                {
                    _CommParams.IRCOMPort = COMPort;
                    _CommParams.IRCOMBaudRate = 9600;
                    InitParam.SaveCommuincationParams(_CommParams);

                    if (SelectedMeter == null || SelectedMeter.Device_Association == null)
                        throw new ArgumentNullException("DeviceAssociation");

                    Connect_IRLinkWithRights(Password, COMPort, SelectedMeter.Device_Association, connectionString, hdlcAddress);
                }
                else
                {
                    throw new Exception(String.Format("Invalid COM Port {0} Selected", COMPort));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Connect_IPLink

        public void Connect_IPLink(IOConnection ConnObj, string password, DeviceAssociation AssociationDetail)
        {
            try
            {
                #region Meter/Client Logical Devices

                if (ConnectionManager.ConnectionInfo == null)
                    ConnectionManager.ConnectionInfo = new ConnectionInfo();

                if (ConnObj.ConnectionInfo == null)
                    ConnObj.ConnectionInfo = new ConnectionInfo();

                if (ConnObj == null || !ConnObj.IsConnected ||
                    ConnObj.CurrentConnection == PhysicalConnectionType.OpticalPort)
                    throw new Exception("Unable to connect,invalid connectionInfo parameters provided");
                else if (AP_Controller.IsConnected)
                    throw new Exception("Application is already connected,please disconnect");

                ProcessStatus.Invoke("Authenticate Connection");
                ProcessStatus.Invoke(".....................................");

                ProcessStatus.Invoke("Initializing Application Connect Process");

                // Check Object Validity
                if (ConnObj.ConnectionInfo == null)
                    throw new Exception("Unable to connect over IP Link,ConnectionInfo parameter not initialized properly");

                ConnObj.ConnectionInfo.PasswordTxt = password;
                ConnObj.ConnectionInfo.MeterInfo = new MeterConfig(SelectedMeter);

                #endregion
                ///-----------------------------------------------------------------------
                #region // Locate TCPIP Params & Reconnect TCP Server

                ProcessStatus.Invoke("Starting Application Connection Process on TCP IP Link");
                #region // Listen On TCP Server if Not Server Started or ServerIP not matches with listening socket

                IPAddress serverIP = TCPParams.ServerIP;
                if (!ConnectionManager.TCP_CONController.IsServerListening ||
                    !ConnectionManager.TCP_CONController.TCPServer.LocalSocket.Address.Equals(serverIP))
                {
                    // Init Server Socket
                    int ServerPort = TCPParams.ServerPort;
                    IPEndPoint serverSocket = new IPEndPoint(serverIP, ServerPort);
                    if (ConnectionManager.TCP_CONController.IsServerListening)  ///Server Already Listening
                    {
                        // Disconnect Server
                        ConnectionManager.TCP_CONController.DisConnectServer();
                    }
                    ConnectionManager.TCP_CONController.InitTCPServer();
                    ConnectionManager.TCP_CONController.TCPServer.LocalSocket = serverSocket;
                    ConnectionManager.TCP_CONController.TCPTimeOut = TCPParams.TCPIPTimeOut;
                    ConnectionManager.TCP_CONController.IsTCPIpTimeOut = TCPParams.IsTCPTimeOut;

                    // Listen On Server
                    ConnectionManager.TCP_CONController.ConnectServer();
                }

                #endregion // Init New Parameters Change Settings

                if (ConnectionManager.DataReadTimeOut != CommunicationParams.DataReadTimeout)
                    ConnectionManager.DataReadTimeOut = CommunicationParams.DataReadTimeout;

                #endregion

                #region Initial Application Process & TCPIP Wrapper

                //AP_Controller.SynCommunicateData = new SynCommData(ConnObj.ReceiveDataFromPhysicalLayer);
                //AP_Controller.AsyncCommunicateData = new BeginCommunicateData(ConnObj.BeginReceiveDataFromPhysicalLayer);
                //AP_Controller.EndAsyncCommunicateData = new EndCommunicateData(ConnObj.EndReceiveDataFromPhysicalLayer);
                //int tmpBufferSize = 1024;
                // Initiate TCP Wrapper Object
                ConnObj.IOTrafficLogger = AP_Controller.ApplicationProcess.Logger;
                AP_Controller.GetCommunicationObject = ConnObj;
                //COmmented By Rashad
                //ConnObj.InitBuffer(tmpBufferSize, tmpBufferSize, this.ConnectionManager.CreateDataReaderBuffer);
                #endregion
                ConnectMeter(ConnObj.ConnectionInfo, password, AssociationDetail, ConnObj);

                // Log Application Connected 
                String txt = String.Format("Application Connected using IO Channel {0}",
                            ConnObj.IOStream.ToString());
                ProcessStatus.Invoke(txt);
            }
            catch (Exception ex)
            {
                try
                {
                    try
                    {
                        if (AP_Controller.IsConnected)
                        {
                            // New DLMS Library Change
                            AP_Controller.ARLRQ();
                            AP_Controller.ApplicationProcess.Is_Association_Developed = false;
                            Thread.Sleep(100);
                            AP_Controller.GetCommunicationObject.Disconnect();
                        }
                    }
                    catch (Exception) { }
                }
                catch (Exception)
                { }
                throw ex;
            }
            finally
            {

            }
        }

        public void Connect_IPLink(IOConnection ConnectionInfo, string password)
        {
            try
            {
                if (SelectedMeter == null || SelectedMeter.Device_Association == null)
                    throw new ArgumentNullException("DeviceAssociation");

                Connect_IPLink(ConnectionInfo, password, SelectedMeter.Device_Association);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        #endregion

        #region Other Methods
        public List<OBISCodeRights> GetCurrentOBISCodeRights(int rightGroupId, int deviceId)
        {
            try
            {
                List<OBISCodeRights> OBISCodeRightsList = null;
                ConfigsHelper Configurator = new ConfigsHelper(Configurations);
                // ConnectionInfo CurrentConnInfo = CurrentConnectionInfo;
                // OBISCodeRightsList = Configurator.GetCurrentSAPOBISRights(Configurations.Configuration.CurrentConfiguration.RightsGroupId);
                OBISCodeRightsList = Configurator.GetCurrentSAPOBISRightsIOP(rightGroupId, deviceId);
                return OBISCodeRightsList;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load OBIS Code Rights Configs from data source", ex);
            }
        }
        public void ARLQCalBck(bool calBck)
        {
            try
            {
                Console.Out.WriteLine("ARLQ Status " + calBck);
            }
            catch (Exception ex) { }
        }
        private void TCP_CONController_TCPClientConnected(Socket ClientSocket)
        {
            try
            {
                /// wakeUpConnWait.Set();
                /// Save Incoming TCPIP Client Connection
                string msgTxt = String.Format("TCP Client Connection received,details {0}",
                                                    ClientSocket.RemoteEndPoint);
                connectionLog.LogConnection(msgTxt);
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void _connectionManager_IOChannelDisconnect(IOConnection conn, string msg)
        {
            try
            {
                if (MeterSerial != null && MeterSerial.Count > 0)
                    MeterSerial.Remove(conn);
                ///Try To Disconnect AP & Below Layers
                if (conn == AP_Controller.GetCommunicationObject)
                {
                    Disconnect();
                }
            }
            catch
            {
            }
        }
        private void AttachHandler()
        {
            _connectionManager.IOChannelDisconnect += new Action<IOConnection, String>(_connectionManager_IOChannelDisconnect);
            _connectionManager.TCP_CONController.TCPClientConnected += new Action<Socket>(this.TCP_CONController_TCPClientConnected);
        }
        public void HLS_Authentication(Security_Data _Security_Data, DeviceAssociation AssociationDetail = null)
        {
            byte[] RandomInChallenge = null;
            byte[] RandomOutChallenge = null;

            int length = new Random().Next(08, 16);//new Random().Next(08, 64);
            RandomInChallenge = Commons.GetRandomOctectString(length);

            SecurityControl SC_Val = SecurityControl.None;
            HLS_Mechanism AuthenticationType = HLS_Mechanism.HLS_GMAC;

            bool flag = false;

            try
            {
                #region // Security Data Validation

                if (!_Security_Data.IsInitialized)
                {
                    if (_Security_Data.SystemTitle == null ||
                        _Security_Data.SystemTitle.Count <= 0)
                        throw new DLMSException(string.Format("Unable to proceed HLS GMAC Authentication, System Title (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_SystemTitle));

                    if (_Security_Data.AuthenticationKey == null ||
                        _Security_Data.AuthenticationKey.Value.Count <= 0)
                        throw new DLMSException(string.Format("Unable to proceed HLS GMAC Authentication, Invalid AK (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_AK));

                    if (_Security_Data.EncryptionKey == null ||
                        _Security_Data.EncryptionKey.Value.Count <= 0)
                        throw new DLMSException(string.Format("Unable to proceed HLS GMAC Authentication, Invalid EK (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_SecurityData_EK));
                }

                #endregion
                SC_Val = _Security_Data.SecurityControl;

                if (AssociationDetail != null &&
                    AssociationDetail.AuthenticationType >= HLS_Mechanism.HLS_Manufac)
                    AuthenticationType = AssociationDetail.AuthenticationType;

                // System Title Required For GMAC,SHA2,EC_DSA
                if (AuthenticationType >= HLS_Mechanism.HLS_GMAC)
                {
                    AP_Controller.Client_ApplicationTitle = _Security_Data.SystemTitle.ToArray();
                }
                else
                {
                    // Set Null Value
                    AP_Controller.Client_ApplicationTitle = null;
                    AP_Controller.Responding_ApplicationTitle = null;
                }

                AP_Controller.Security_Data = _Security_Data;

                if (AuthenticationType != HLS_Mechanism.HLS_GMAC)
                    // Disable Encryption Authentication For 
                    // Action Request In GMAC & AARQ Initiate Request
                    AP_Controller.Security_Data.SecurityControl = SecurityControl.AuthenticationOnly;

                // Start Authentication Process
                if (AP_Controller.Crypto == null ||
                    AP_Controller.Crypto.GetType() != typeof(AESGCM))
                {
                    AP_Controller.Crypto = new AESGCM();

                    ((AESGCM)AP_Controller.Crypto).KeySize = 128;
                    ((AESGCM)AP_Controller.Crypto).GMacBitSize = 96;
                }

                if (AssociationDetail != null)
                {
                    // Initialize TCP Wrapper Object
                    CurrentConnection.TCPWrapper = new WrapperLayer(AssociationDetail.MeterSap, AssociationDetail.ClientSap);

                    flag = AP_Controller.AARQ(AssociationDetail.MeterSap, AssociationDetail.ClientSap,
                                              RandomInChallenge, ref RandomOutChallenge, AssociationDetail.AuthenticationType);
                }
                else
                {
                    if (meterlogicalDevice == null)
                        throw new ArgumentException("Unable to initialize Meter Server SAP", "Meter Management Device");
                    if (clientSAP == null)
                        throw new ArgumentException("Unable to initialize Meter Client SAP", "Meter Client Device");

                    // Initialize TCP Wrapper Object
                    CurrentConnection.TCPWrapper = new WrapperLayer(meterlogicalDevice.SAP_Address, clientSAP.SAP_Address);

                    flag = AP_Controller.AARQ(meterlogicalDevice, clientSAP, RandomInChallenge, ref RandomOutChallenge, HLS_Mechanism.HLS_GMAC);
                }

                if (!flag || !AP_Controller.IsConnected)
                {
                    throw new DLMSException(string.Format("Unable to proceed HLS GMAC Authentication Error, AARQ failed (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_HLS_LoginStatus));
                }

                Key Authen_KEY = _Security_Data.AuthenticationKey;
                Key Encrypt_KEY = _Security_Data.EncryptionKey;
                Key HLS_Secret = _Security_Data.HLS_Secret;

                if ((HLS_Secret == null ||
                     HLS_Secret.Value == null ||
                     HLS_Secret.Value.Count < 8) && Encrypt_KEY != null)
                    HLS_Secret = Encrypt_KEY;

                byte[] Chanllenge_STR = RandomOutChallenge;
                byte[] TAG = null;

                string Data_1 = string.Empty;
                string Data_2 = string.Empty;

                // Update Client & Responding AP_Title
                if (AP_Controller.Client_ApplicationTitle != null)
                {
                    _Security_Data.SystemTitle = new List<byte>(AP_Controller.Client_ApplicationTitle);
                }
                if (AP_Controller.Responding_ApplicationTitle != null)
                {
                    _Security_Data.ServerSystemTitle = new List<byte>(AP_Controller.Responding_ApplicationTitle);
                }

                #region // Processing Authentication TAG CToS

                switch (AuthenticationType)
                {
                    case HLS_Mechanism.HLS_Manufac:
                        {
                            throw new InvalidOperationException("HLS_Manufacturer Specific Not Implemented");
                        }
                    case HLS_Mechanism.HLS_MD5:
                    case HLS_Mechanism.HLS_SHA1:
                        {
                            List<byte> Input_Data = new List<byte>(64);

                            #region UnWrapp_Key

                            byte[] UnWrapp_Key = null;
                            List<byte> Key_Val = new List<byte>(HLS_Secret.Value);
                            if (HLS_Secret.IsKeyWrapped)
                                Key.ProcessKey_ForUsage(Key_Val);
                            UnWrapp_Key = Key_Val.ToArray();
                            Key_Val = null;

                            #endregion

                            // Add Challenge STR
                            Input_Data.AddRange(Chanllenge_STR);
                            // Add Secret KEY
                            Input_Data.AddRange(UnWrapp_Key);

                            if (AuthenticationType == HLS_Mechanism.HLS_SHA1)
                                TAG = Serenity.Crypto.Digest.Hash.GetSHA1Hash(Input_Data.ToArray());
                            else if (AuthenticationType == HLS_Mechanism.HLS_MD5)
                                TAG = Serenity.Crypto.Digest.Hash.GetMD5Hash(Input_Data.ToArray());

                            Data_1 = DLMS_Common.ArrayToHexString(TAG);
                        }
                        break;
                    case HLS_Mechanism.HLS_GMAC:
                        {
                            _Security_Data.IV = Security_Data.GetInitializationVector(_Security_Data.SystemTitle.ToArray(),
                                                                                      Encrypt_KEY.EncryptionCounter);

                            var AAD = Security_Data.GetAadBytes(Authen_KEY,
                                                                Chanllenge_STR,
                                                                (byte)SecurityControl.AuthenticationOnly);

                            #region UnWrapp_Key

                            byte[] UnWrapp_Key = null;
                            List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                            if (Encrypt_KEY.IsKeyWrapped)
                                Key.ProcessKey_ForUsage(Key_Val);
                            UnWrapp_Key = Key_Val.ToArray();
                            Key_Val = null;

                            #endregion

                            TAG = AP_Controller.Crypto.CalculateGMAC(AAD.ToArray(),
                                                                     UnWrapp_Key,
                                                                     _Security_Data.IV.ToArray());
                            Array.Resize<byte>(ref TAG, 12);

                            List<byte> Input_Data = new List<byte>(17);
                            // Format Data1 = SC|FC|F = GMAC()
                            Input_Data.Add((byte)SecurityControl.AuthenticationOnly);

                            var FC = BitConverter.GetBytes(Encrypt_KEY.EncryptionCounter);
                            Array.Reverse(FC);

                            Input_Data.AddRange(FC.GetSegment<byte>(0).AsEnumerable<byte>());
                            Input_Data.AddRange(TAG);
                            Data_1 = DLMS_Common.ArrayToHexString(Input_Data.ToArray());
                        }
                        break;
                    case HLS_Mechanism.HLS_SHA2:
                        {
                            List<byte> Input_Data = new List<byte>(256);

                            #region UnWrapp_Key

                            byte[] UnWrapp_Key = null;
                            List<byte> Key_Val = new List<byte>(HLS_Secret.Value);
                            if (HLS_Secret.IsKeyWrapped)
                                Key.ProcessKey_ForUsage(Key_Val);
                            UnWrapp_Key = Key_Val.ToArray();
                            Key_Val = null;

                            #endregion

                            // Format Input Data For Digest
                            // Add Secret KEY
                            Input_Data.AddRange(UnWrapp_Key);

                            // Add System Title C
                            if (_Security_Data.SystemTitle != null &&
                               _Security_Data.SystemTitle.Count > 0)
                                Input_Data.AddRange(_Security_Data.SystemTitle.ToArray());

                            // Add System Title S
                            if (_Security_Data.ServerSystemTitle != null &&
                               _Security_Data.ServerSystemTitle.Count > 0)
                                Input_Data.AddRange(_Security_Data.ServerSystemTitle.ToArray());

                            // Add Challenge STR SToC
                            Input_Data.AddRange(RandomInChallenge);

                            // Add Challenge STR CToS
                            Input_Data.AddRange(Chanllenge_STR);

                            TAG = Serenity.Crypto.Digest.Hash.GetSHA256Hash(Input_Data.ToArray());

                            Data_1 = DLMS_Common.ArrayToHexString(TAG);
                        }
                        break;
                    case HLS_Mechanism.HLS_ECDSA:
                        {
                            throw new InvalidOperationException("HLS_ECDSA Not Implemented");
                        }
                    default:
                        throw new InvalidOperationException(string.Format("{0} Not Implemented", AuthenticationType));
                }

                #endregion

                // Invoke HLS_Authentication Action
                Base_Class LN_15 = AP_Controller.GetSAPEntry(Get_Index.Current_Association);
                LN_15.MethodInvokeId = Class_15.Reply_HLS_Authentication;
                ((Class_15)LN_15).Password = Data_1;

                var result = AP_Controller.Method_Invoke(LN_15);
                if (result != Action_Result.Success)
                {
                    throw new DLMSException(string.Format("Unable to proceed HLS GMAC Authentication Error, Meter Rejected Processed TAG (Error Code:{0})",
                                                          (int)DLMSErrors.Invalid_AuthenticationTAG));
                }

                // Process Challenge String 1
                Data_2 = ((Class_15)LN_15).Password;

                byte[] respondedTag = DLMS_Common.String_to_Hex_array(Data_2);

                #region // Processing Authentication TAG SToC

                switch (AuthenticationType)
                {
                    case HLS_Mechanism.HLS_Manufac:
                        {
                            throw new InvalidOperationException("HLS_Manufacturer Specific Not Implemented");
                        }
                    case HLS_Mechanism.HLS_MD5:
                    case HLS_Mechanism.HLS_SHA1:
                        {
                            List<byte> Input_Data = new List<byte>(64);

                            #region UnWrapp_Key

                            byte[] UnWrapp_Key = null;
                            List<byte> Key_Val = new List<byte>(HLS_Secret.Value);
                            if (HLS_Secret.IsKeyWrapped)
                                Key.ProcessKey_ForUsage(Key_Val);
                            UnWrapp_Key = Key_Val.ToArray();
                            Key_Val = null;

                            #endregion

                            // Add Challenge STR
                            Input_Data.AddRange(RandomInChallenge);
                            // Add Secret KEY
                            Input_Data.AddRange(UnWrapp_Key);

                            TAG = null;
                            if (AuthenticationType == HLS_Mechanism.HLS_SHA1)
                                TAG = Serenity.Crypto.Digest.Hash.GetSHA1Hash(Input_Data.ToArray());
                            else if (AuthenticationType == HLS_Mechanism.HLS_MD5)
                                TAG = Serenity.Crypto.Digest.Hash.GetMD5Hash(Input_Data.ToArray());

                        }
                        break;
                    case HLS_Mechanism.HLS_GMAC:
                        {
                            var SecurityControlOpt = (SecurityControl)respondedTag[0];

                            byte[] T_Array = new byte[04];
                            Array.Copy(respondedTag, 1, T_Array, 0, T_Array.Length);
                            Array.Reverse(T_Array);

                            uint DecryptionCounter = BitConverter.ToUInt32(T_Array, 0);

                            var AAD = Security_Data.GetAadBytes(Authen_KEY,
                                       RandomInChallenge,
                                       (byte)SecurityControlOpt);

                            _Security_Data.IV = Security_Data.GetInitializationVector(_Security_Data.ServerSystemTitle.ToArray(),
                                                                                      DecryptionCounter);

                            #region UnWrapp_Key

                            byte[] UnWrapp_Key = null;
                            List<byte> Key_Val = new List<byte>(Encrypt_KEY.Value);
                            if (Encrypt_KEY.IsKeyWrapped)
                                Key.ProcessKey_ForUsage(Key_Val);
                            UnWrapp_Key = Key_Val.ToArray();
                            Key_Val = null;

                            #endregion

                            TAG = AP_Controller.Crypto.CalculateGMAC(AAD.ToArray(),
                                                                     UnWrapp_Key,
                                                                     _Security_Data.IV.ToArray());
                        }
                        break;
                    case HLS_Mechanism.HLS_SHA2:
                        {
                            List<byte> Input_Data = new List<byte>(256);

                            #region UnWrapp_Key

                            byte[] UnWrapp_Key = null;
                            List<byte> Key_Val = new List<byte>(HLS_Secret.Value);
                            if (HLS_Secret.IsKeyWrapped)
                                Key.ProcessKey_ForUsage(Key_Val);
                            UnWrapp_Key = Key_Val.ToArray();
                            Key_Val = null;

                            #endregion

                            // Format Input Data For Digest

                            // Add Secret KEY
                            Input_Data.AddRange(UnWrapp_Key);

                            // Add System Title S
                            if (_Security_Data.ServerSystemTitle != null &&
                               _Security_Data.ServerSystemTitle.Count > 0)
                                Input_Data.AddRange(_Security_Data.ServerSystemTitle.ToArray());

                            // Add System Title C
                            if (_Security_Data.SystemTitle != null &&
                               _Security_Data.SystemTitle.Count > 0)
                                Input_Data.AddRange(_Security_Data.SystemTitle.ToArray());

                            // Add Challenge STR CToS
                            Input_Data.AddRange(Chanllenge_STR);

                            // Add Challenge STR SToC
                            Input_Data.AddRange(RandomInChallenge);

                            TAG = Serenity.Crypto.Digest.Hash.GetSHA256Hash(Input_Data.ToArray());
                        }
                        break;
                    case HLS_Mechanism.HLS_ECDSA:
                        {
                            throw new InvalidOperationException("HLS_ECDSA Not Implemented");
                        }
                    default:
                        throw new InvalidOperationException(string.Format("{0} Not Implemented", AuthenticationType));
                }

                #endregion

                #region Compare_TAGs

                bool isEqual = true;
                int sourceIndex = 0;
                int destinationIndex = (AuthenticationType == HLS_Mechanism.HLS_GMAC) ? 5 : 0;
                int DataLength = (AuthenticationType == HLS_Mechanism.HLS_GMAC) ? 12 : respondedTag.Length;

                ArraySegment<byte> RespondedTAG = new ArraySegment<byte>(respondedTag, destinationIndex, DataLength);
                ArraySegment<byte> Processed_TAG = new ArraySegment<byte>(TAG);

                // TAGs Must be Equal in Length
                if (RespondedTAG.Count != Processed_TAG.Count)
                    isEqual = false;
                else
                    for (sourceIndex = Processed_TAG.Offset, destinationIndex = RespondedTAG.Offset;
                        // Condition 
                        sourceIndex < (Processed_TAG.Offset + Processed_TAG.Count) &&
                        destinationIndex < (RespondedTAG.Offset + RespondedTAG.Count);
                        sourceIndex++, destinationIndex++)
                    {
                        if (RespondedTAG.Array[destinationIndex] != Processed_TAG.Array[sourceIndex])
                        {
                            isEqual = false;
                            break;
                        }
                    }

                #endregion

                if (!isEqual)
                {
                    throw new SecurityException(string.Format("Unable to proceed HLS GMAC Authentication Error,Received TAG Mismatch (Error Code:{0})",
                                                               (int)DLMSErrors.Invalid_AuthenticationTAG));
                }
            }
            catch (Exception ex)
            {
                if (AP_Controller != null)
                    AP_Controller.ApplicationProcess.Is_Association_Developed = false;

                if (ex is DLMSException)
                    throw ex;
                else if (ex is IOException)
                    throw ex;
                else if (ex is SecurityException)
                    throw ex;
                else
                    throw new DLMSException(string.Format("Unable to proceed HLS Authentication Error (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_HLS_LoginStatus));
            }
            finally
            {
                if (_Security_Data != null)
                    _Security_Data.SecurityControl = SC_Val;
            }
        } 
        #endregion

        #region Login Methods
        public bool PublicLogin(IOConnection ConnInfo)
        {
            try
            {
                if (ConnInfo == null) // || !ConnInfo.IsConnected)
                    throw new IOException("Unable to processed,IOConnnection object not Connected/Initialize");

                ConnectionInfo conInfo = ConnInfo.ConnectionInfo;

                if (ConnInfo.ConnectionInfo == null)
                    ConnInfo.ConnectionInfo = new ConnectionInfo();

                AP_Controller.GetCommunicationObject = ConnInfo;

                // Initialize TCP Wrapper Object
                ConnInfo.TCPWrapper = new WrapperLayer(ManagementDevice.SAP_Address, Public.SAP_Address);

                // Set Null Value
                AP_Controller.Client_ApplicationTitle = null;
                AP_Controller.Responding_ApplicationTitle = null;


                bool isConnected = AP_Controller.AARQ(ManagementDevice, Public, string.Empty);
                return isConnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PrivateLogin(IOConnection ConnInfo, string Password)
        {
            try
            {
                SAP_Object mngt = null;
                SAP_Object pri = null;

                if (ConnInfo != null && ConnInfo.ConnectionInfo != null)
                {
                    mngt = new SAP_Object((ushort)ConnInfo.ConnectionInfo.CurrentMeterSAP);
                    pri = new SAP_Object((ushort)ConnInfo.ConnectionInfo.CurrentClientSAP);
                }

                if (mngt == null)
                    throw new ArgumentException("Unable to initialize Meter Server SAP", "Meter Management Device");

                if (pri == null)
                    throw new ArgumentException("Unable to initialize Meter Client SAP", "Client");

                AP_Controller.GetCommunicationObject = ConnInfo;

                // Initialize TCP Wrapper Object
                ConnInfo.TCPWrapper = new WrapperLayer(mngt.SAP_Address, pri.SAP_Address);

                // Set Null Value
                AP_Controller.Client_ApplicationTitle = null;
                AP_Controller.Responding_ApplicationTitle = null;

                bool isConnected = AP_Controller.AARQ(mngt, pri, Password);
                return isConnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PrivateLoginAsync(IOConnection ConnInfo, string Password)
        {
            try
            {
                SAP_Object mngt = null;
                SAP_Object pri = null;

                if (ConnInfo != null && ConnInfo.ConnectionInfo != null)
                {
                    mngt = new SAP_Object((ushort)ConnInfo.ConnectionInfo.CurrentMeterSAP);
                    pri = new SAP_Object((ushort)ConnInfo.ConnectionInfo.CurrentClientSAP);
                }

                if (mngt == null)
                    throw new ArgumentException("Unable to initialize Meter Server SAP", "Meter Management Device");

                if (pri == null)
                    throw new ArgumentException("Unable to initialize Meter Client SAP", "Client");

                AP_Controller.GetCommunicationObject = ConnInfo;

                // Initialize TCP Wrapper Object
                ConnInfo.TCPWrapper = new WrapperLayer(mngt.SAP_Address, pri.SAP_Address);

                // Set Null Value
                AP_Controller.Client_ApplicationTitle = null;
                AP_Controller.Responding_ApplicationTitle = null;

                bool isConnected = await AP_Controller.AARQAsync(mngt, pri, Password);
                return isConnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PublicLogin()
        {
            return PublicLogin(CurrentConnection);
        } 
        #endregion

        #region Read Data from Meter
        public MeterSerialNumber GetMeterSerialNumber()
        {
            #region // Meter MSN

            try
            {
                MeterSerialNumber msn = null;
                if (CurrentConnection.ConnectionInfo.IsInitialized &&
                    CurrentConnection.MeterSerialNumberObj != null &&
                    CurrentConnection.MeterSerialNumberObj.IsMSN_Valid)
                {
                    msn = CurrentConnection.MeterSerialNumberObj;
                }
                else
                {
                    ProcessStatus.Invoke("Reading meter serial no(MSN)");
                    Base_Class CommObjT = AP_Controller.GetSAPEntry(Get_Index.Manufacturing_ID);
                    CommObjT.DecodingAttribute = 0x02;

                    if (CommObjT.IsAttribReadable(0x02) || true)
                    {
                        AP_Controller.GET(CommObjT);
                        if (CommObjT.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                        { 
                            if (((Class_1)CommObjT).Value_Array != null)  // if (((Class_1)CommObjT).Value_Array != null)
                            {
                                string msn_ = ASCIIEncoding.ASCII.GetString((byte[])((Class_1)CommObjT).Value_Array);
                                msn = MeterSerialNumber.ConvertFrom(msn_);
                            }
                            else
                            {
                                uint msn_ = Convert.ToUInt32(((Class_1)CommObjT).Value);
                                msn = new MeterSerialNumber();
                                msn.MSN = msn_;
                            }
                        }
                    }
                }

                return msn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading meter serial number(MSN)" + ex.Message, ex);
            }

            #endregion
        }

        public string GetFirmwareVersion()
        {
            string version = null;

            #region // Meter Firmware Version

            try
            {
                if (CurrentConnection.ConnectionInfo.IsInitialized &&
                    CurrentConnection.MeterSerialNumberObj != null &&
                    CurrentConnection.MeterSerialNumberObj.IsMSN_Valid)
                {
                    version = CurrentConnection.ConnectionInfo.MeterInfo.Version;
                }
                else
                {
                    ProcessStatus.Invoke("Reading Meter FirmwareVersion");

                    Base_Class CommObjT = AP_Controller.GetSAPEntry(Get_Index.Active_Firmware_ID);
                    CommObjT.DecodingAttribute = 0x02;

                    if (CommObjT.IsAttribReadable(0x02) || true)
                    {
                        AP_Controller.GET(CommObjT);
                        if (CommObjT.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                        {
                            if (((Class_1)CommObjT).Value_Array != null)
                            {
                                version = ASCIIEncoding.ASCII.GetString((byte[])((Class_1)CommObjT).Value_Array);
                            }
                        }
                    }
                }

                return version;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading Meter FirmwareVersion" + ex.Message, ex);
            }

            #endregion
        }

        public List<OBISCodeRights> ReadMeterAccessRights(IOConnection ConnInfo)
        {
            try
            {
                if (!AP_Controller.IsConnected)
                {
                    throw new Exception("Application is not connected");
                }
                List<OBISCodeRights> OBISCodeRightsList = null;
                Class_15 OBJSAP = (Class_15)AP_Controller.GetSAPEntry(Get_Index.Current_Association);
                OBJSAP.DecodingAttribute = 0x02;
                AP_Controller.GET(OBJSAP);
                if (OBJSAP.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    OBISCodeRightsList = OBJSAP.OBISCodesReceived;
                else
                    throw new Exception("Error occurred while reading OBIS Rights Code");
                return OBISCodeRightsList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading data access rights", ex);
            }
        }

        public async Task<List<OBISCodeRights>> ReadMeterAccessRightsAsync(IOConnection ConnInfo)
        {
            try
            {
                if (!AP_Controller.IsConnected)
                {
                    throw new Exception("Application is not Connected");
                }

                List<OBISCodeRights> OBISCodeRightsList = null;
                Class_15 OBJSAP = (Class_15)AP_Controller.GetSAPEntry(Get_Index.Current_Association);
                OBJSAP.DecodingAttribute = 0x02;
                await AP_Controller.GETAsync(OBJSAP);

                if (OBJSAP.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    OBISCodeRightsList = OBJSAP.OBISCodesReceived;
                else
                    throw new Exception("Error Occurred while reading OBISRights Code");

                return OBISCodeRightsList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred while reading data access rights", ex);
            }
        }

        public MeterSerialNumber ReadMeterSerialNumber()
        {
            MeterSerialNumber msn = null;
            Base_Class CommObjT = AP_Controller.GetSAPEntry(Get_Index.Manufacturing_ID);
            CommObjT.DecodingAttribute = 0x02;

            AP_Controller.GET(CommObjT);
            if (CommObjT.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
            {
                if (((Class_1)CommObjT).Value_Array != null)
                {
                    string msn_ = ASCIIEncoding.ASCII.GetString((byte[])((Class_1)CommObjT).Value_Array);
                    msn = MeterSerialNumber.ConvertFrom(msn_);
                }
                else
                {
                    uint msn_ = Convert.ToUInt32(((Class_1)CommObjT).Value);
                    msn = new MeterSerialNumber();
                    msn.MSN = msn_;
                }
            }
            return msn;
        }

        public string ReadFirmwareVersion()
        {
            string version = null;

            Base_Class CommObjT = AP_Controller.GET(Get_Index.Active_Firmware_ID, 02);

            if (CommObjT.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
            {
                if (((Class_1)CommObjT).Value_Array != null)
                {
                    version = ASCIIEncoding.ASCII.GetString((byte[])((Class_1)CommObjT).Value_Array);
                }
            }

            return version;
        }

        public void GetMeterInfo(IOConnection ConnObjInfo, bool release = true)
        {
            bool isErrorRaised = false;
            try
            {
                String firmWareVersion = "";
                MeterSerialNumber msn = null;
                String mtrModel = "";

                this.st_meter_Info = null;
                ConfigsHelper Configurator = new ConfigsHelper(Configurations);

                #region Declarations

                ProcessStatus.Invoke("Starting Meter Information read process");

                #endregion

                #region Check Already Connected

                try
                {
                    if (AP_Controller.IsConnected)
                    {
                        AP_Controller.ARLRQ();
                    }
                    else if (!ConnObjInfo.IsChannelConnected)
                        throw new IOException("Unable to read meter info,physical connection disconnected");
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to read meter info", ex);
                }

                #endregion

                #region Initial Application Process

                //AP_Controller.SynCommunicateData = new SynCommData(ConnObjInfo.ReceiveDataFromPhysicalLayer);
                //AP_Controller.AsyncCommunicateData = new BeginCommunicateData(ConnObjInfo.BeginReceiveDataFromPhysicalLayer);
                //AP_Controller.EndAsyncCommunicateData = new EndCommunicateData(ConnObjInfo.EndReceiveDataFromPhysicalLayer);

                /// Initial TCP Wrapper Object
                ConnObjInfo.IOTrafficLogger = AP_Controller.ApplicationProcess.Logger;

                #endregion

                #region // try to login into meter using management device/public

                if (ConnObjInfo.ConnectionInfo == null)
                    ConnObjInfo.ConnectionInfo = new ConnectionInfo();

                if (ConnObjInfo.ConnectionInfo.MeterInfo == null ||
                    ConnObjInfo.ConnectionInfo.MeterInfo.Device_Association == null)
                    if (SelectedMeter != null)
                        ConnObjInfo.ConnectionInfo.MeterInfo = new MeterConfig(SelectedMeter);

                this.meterlogicalDevice = ManagementDevice;
                this.clientSAP = Public;

                ConnObjInfo.TCPWrapper = new WrapperLayer(ManagementDevice.SAP_Address, Public.SAP_Address);

                #endregion
                #region AARQ

                ProcessStatus.Invoke("Trying to Log In meter using public/Anonymous login account");

                // Reset Security Settings
                AP_Controller.Security_Data = null;
                bool login = AP_Controller.AARQ(ManagementDevice, Public, string.Empty);

                if (!login)
                    throw new DLMSException("Unable to read meter info,unable to login into meter");

                #endregion

                #region /// Reading Necessary Meter Information

                #region Reading FirmWareVersion

                ProcessStatus.Invoke("Reading Meter Firmware Software Version");

                try
                {
                    st_meter_Info = _Parameter_Controller.GetStFirmwareInfo();

                    msn = st_meter_Info.MSN_Info;
                    firmWareVersion = st_meter_Info.Version;
                    ConnObjInfo.ConnectionInfo.MeterInfo.MeterModelSTR = st_meter_Info.Model;
                }
                catch
                {
                    ProcessStatus.Invoke("Error Reading st_meter_Info");
                }

                /*** Please Set this.st_meter_info object null if not read from meter ***/
                //this.st_meter_Info = null;

                #endregion
                #region // Meter MSN Number

                try
                {
                    if (msn == null || this.st_meter_Info == null || msn == this.st_meter_Info.MSN_Info)
                    {
                        ProcessStatus.Invoke("Reading meter serial no (MSN) ");
                        msn = ReadMeterSerialNumber();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error reading meter serial number(MSN)", ex);
                }
                finally
                {
                    // MSN Validity Check
                    if (msn != null && !msn.IsMSN_Valid)
                    {
                        throw new Exception(String.Format("Invalid Meter Serial Number {0}", msn));
                    }
                }

                #endregion
                #region // Meter Firmware Version

                try
                {
                    if (this.st_meter_Info == null || string.IsNullOrEmpty(this.st_meter_Info.Version) || true)
                    {
                        ProcessStatus.Invoke("Reading meter firmware version ");
                        if (this.st_meter_Info == null)
                            this.st_meter_Info = new St_FirmwareInfo();
                        firmWareVersion = this.st_meter_Info.Version = ReadFirmwareVersion();
                        this.st_meter_Info.MSN_Info = msn;
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception("Error reading meter serial number(MSN)", ex);
                }

                #endregion

                #endregion
                #region // Initialize Data Properties

                ConnObjInfo.ConnectionInfo.MeterSerialNumberObj = msn;
                mtrModel = ConnObjInfo.ConnectionInfo.MeterInfo.MeterModel;

                mtrModel = mtrModel.TrimEnd("-,_,,,;,:".ToCharArray());
                ConnObjInfo.ConnectionInfo.MeterInfo.MeterModelSTR = mtrModel;
                ConnObjInfo.ConnectionInfo.MeterInfo.Version = firmWareVersion;

                #endregion

                #region ARLRQ

                connectionLog.LogConnection("Trying to log out meter");
                if (AP_Controller.IsConnected && release)
                {
                    AP_Controller.ARLRQ();
                }

                #endregion
            }
            catch (Exception ex)
            {
                isErrorRaised = true;

                if (ex is DLMSException)
                    throw ex;
                else if (ex is IOException)
                    throw ex;
                else
                    throw new Exception("Unable to read meter Information", ex);
            }
            finally
            {
                try
                {
                    if (ConnObjInfo != null &&
                        ConnObjInfo.ConnectionInfo != null &&
                        (isErrorRaised || release))
                    {
                        ConnObjInfo.ConnectionInfo.IsConnected = false;
                    }
                    if (AP_Controller.IsConnected && (isErrorRaised || release))
                    {
                        AP_Controller.ARLRQ();
                        Thread.Sleep(250);
                        if (AP_Controller.IsConnected)
                        {
                            AP_Controller.ApplicationProcess.Is_Association_Developed = false;
                            AP_Controller.GetCommunicationObject.Disconnect();
                        }
                    }
                }
                catch (Exception) { }
            }
        } 
        #endregion

        #endregion

        #region HDLC Methods

        public bool ConnectDirectHDLC(NetworkStream tcpstreamLocal, ushort ServerSAP, ushort ClientSAP,
                                      InitHDLCParam hdlcParamLocal = null)
        {
            bool isConnected = false;

            try
            {
                if (tcpstreamLocal == null)
                    throw new ArgumentNullException("NetworkStream");

                // Set HDLC Connection Type
                if (ProtocolType == LLCProtocolType.Not_Assigned)
                    ProtocolType = LLCProtocolType.Direct_HDLC;

                if (_HDLCConnection == null)
                {
                    _HDLCConnection = new HDLC();
                }

                _HDLCConnection.TransmitFrame += new TransmitData(TransmitData);


                // Reset Already Connected
                HDLCConnection.ResetHDLC();
                // Re-Initial HDLC
                if (hdlcParamLocal != null)
                    InitHDLCParams(hdlcParamLocal);

                HDLCConnection.DestinationSAP = ServerSAP;
                HDLCConnection.SourceAddress = ClientSAP;

                if (tcpstreamLocal == null ||
                    tcpstreamLocal is TCPStream)
                {
                    // Remove All Previous ASyn Handlers
                    (tcpstreamLocal as TCPStream)._ReceiveDataFromPhysicalLayerASync = null;

                    var Data_Receiver_Handler = new Action<ArraySegment<byte>>(HDLC_Data_Receiver_Handler);
                    (tcpstreamLocal as TCPStream).ReceiveDataFromPhysicalLayerASync += Data_Receiver_Handler;
                }
                this.networkStream = tcpstreamLocal;

                HDLCConnection.Connect();
                isConnected = HDLCConnection.Connected;

                return isConnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InitHDLCParams(InitHDLCParam hdlcParam)
        {
            try
            {
                HDLCConnection.AvoidInactivityTimeOut = hdlcParam.AvoidInActivityTimeout;
                HDLCConnection.IsEnableRetrySend = hdlcParam.IsEnableRetrySend;
                HDLCConnection.IsSkipLoginParameter = hdlcParam.IsSkipLoginParameter;

                HDLCConnection.DestinationAddressLength = hdlcParam.HDLCAddressLength;
                HDLCConnection.DestinationAddress = hdlcParam.DeviceAddress;
                HDLCConnection.InactivityTimeOut = hdlcParam.InactivityTimeout;
                HDLCConnection.ReqResTimeOut = hdlcParam.RequestResponseTimeout;
                HDLCConnection.MaxInfoBufReceive = hdlcParam.MaxInfoBufSizeReceive;
                HDLCConnection.MaxInfoBufTransmit = hdlcParam.MaxInfoBufSizeTransmit;
                HDLCConnection.TransmitWinSize = hdlcParam.TransmitWinSize;
                HDLCConnection.ReceiveWinSize = hdlcParam.ReceiveWinSize;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DisConnectHdlc()
        {
            try
            {
                // Attach Serial Port Handlers
                if (HDLCConnection.Connected)
                {
                    HDLCConnection.Disconnect();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Support Method
        public ushort GetDirectHDLCAddress(IOConnectionType connType)
        {
            if (connType == IOConnectionType.HDLC_MODE_E)
                return HdlcParam.DeviceAddress;
            return DirectHdlcParam.DeviceAddress;
        }
        public void HDLC_Data_Receiver_Handler(ArraySegment<byte> rawData)
        {
            try
            {
                HDLCConnection.ReceiveRawData(rawData.Array, rawData.Offset, rawData.Count);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.ToString()); }
        }

        private void TransmitData(byte[] data)
        {
            bool transmitData = false;

            try
            {
                if (data != null)
                {
                    // Transmit Data Over Channel
                    if (networkStream != null)
                    {
                        networkStream.Write(data, 0, data.Length);
                        networkStream.Flush();

                        transmitData = true;
                    }

                    if (!transmitData)
                        throw new InvalidOperationException("Invalid Operation Error");
                }
            }
            catch (_HDLC.HDLCErrorException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
                System.Diagnostics.Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
            }
            catch (HDLCInValidFrameException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
                System.Diagnostics.Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
                System.Diagnostics.Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
            }
        }

        #endregion

    }
}

