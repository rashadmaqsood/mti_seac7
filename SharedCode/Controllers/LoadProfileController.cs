//  #define Enable_LoadTester_Mode

using comm.DataContainer;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Controllers
{
    public class LoadProfileController
    {

        #region Data_Members

        internal int Max_Sync_Iteration = 400;
        public static readonly int Extra_Iteration = 50;
        private bool updateChannelInfo = true;
        private ApplicationProcess_Controller _AP_Controller;
        private GenericProfileInfo LoadProfileInfo = null;
        private List<LoadProfileChannelInfo> ChannelInfo = null;
        private Configs configurations;
        private Configurator _Configurator;
        private ConnectionInfo connInfo;
        private const int MaxChunkSize = 30;
        private bool _isStop;
        const int MaxLP_Entries = 4632;

        #endregion

        #region Properties

        public bool IsStop
        {
            get { return _isStop; }
            set { _isStop = value; }
        }

        public bool UpdateChannelInfo
        {
            get { return updateChannelInfo; }
            set { updateChannelInfo = value; }
        }
        public LoadProfileType LPInfo_Data { get; set; }

        public ApplicationProcess_Controller AP_Controller
        {
            get { return _AP_Controller; }
            set
            {
                _AP_Controller = value;
                _AP_Controller.ApplicationProcess.SAPAssociation += new flagStatus(ApplicationProcess_SAPAssociation);
            }
        }

        private void ApplicationProcess_SAPAssociation(bool flag)
        {
            ///may check either it's last connected meter etc
            updateChannelInfo = true;
        }

        public Configs Configurations
        {
            get { return configurations; }
            set { configurations = value; }
        }

        public ConnectionInfo CurrentConnectionInfo
        {
            get { return connInfo; }
            set { connInfo = value; }
        }

        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }

        public GenericProfileInfo LoadProfileInformation
        {
            get { return LoadProfileInfo; }
            set
            {
                LoadProfileInfo = value;
            }
        }

        #endregion

        #region Constructur
        public LoadProfileController()
        {
        }
        #endregion

        #region GET_Objects

        public Base_Class GetSAPEntry(Get_Index ObjIdentifier)
        {
            return AP_Controller.GetSAPEntry(ObjIdentifier);
        }

        public Base_Class GET_Object(Get_Index ObjIdentifier)
        {
            try
            {
                return GET_Object(ObjIdentifier, 0);
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
        }

        public Base_Class GET_Object(Get_Index ObjIdentifier, byte attribArg)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = AP_Controller.GET(ObjIdentifier, attribArg);
                return objReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
            finally
            {
            }
        }

        public Base_Class GET_Object(Base_Class ObjIdentifier)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = AP_Controller.GET(ObjIdentifier);
                return objReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                //objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                //objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
            finally
            { }
        }

        #endregion

        #region SET_Objects
        public Data_Access_Result SET_Object(Base_Class arg)
        {
            try
            {
                return AP_Controller.SET(arg);
            }
            catch (DLMSEncodingException ex)    ///Error Type 1 (Encoding Type Errors)
            {
                //return Data_Access_Result.Read_Write_Denied;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                //return Data_Access_Result.Other_Reason;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
            finally
            {
            }
        }

        #endregion

        #region Load Profile Raw Data

        protected bool Read_LoadProfileDataRaw(LoadProfileScheme LP_Scheme, IAccessSelector Selector, List<ILValue[]> LoadProfileValues,
                                               Action<Exception> SetInnerException, CancellationTokenSource CancelToken = null)
        {
            Exception InnerExceptionEx = null;
            bool IsSuccess = false;
            List<ILValue[]> LoadProfileValues_T = null;
            Class_7 Load_Profile_CommObj = null;
            try
            {
                Get_Index LP_Index = GetLoadProfileIndex(LP_Scheme);
                Load_Profile_CommObj = (Class_7)GetSAPEntry(LP_Index);
                ///Getting LoadProfile Channels Info
                ///Reading Load Profile Data
                Load_Profile_CommObj.DecodingAttribute = 0x02;
                Load_Profile_CommObj.AccessSelector = Selector;
                try
                {
                    GET_Object(Load_Profile_CommObj);
                }
                catch (Exception ex)
                {
                    InnerExceptionEx = ex;
                    IsSuccess = false;
                }
                DecodingResult d = Load_Profile_CommObj.GetAttributeDecodingResult(0x02);
                if (d == DecodingResult.Ready)
                {
                    IsSuccess = true;
                }
                else
                {
                    if (InnerExceptionEx == null)
                        InnerExceptionEx = new Exception("Error occurred while reading load profile data" + d);
                }
                if (Load_Profile_CommObj.buffer != null && Load_Profile_CommObj.buffer.Count > 0)
                {
                    IDataGenerator LoadProfileDataGen = new LoadProfileDataGenerator();
                    LoadProfileValues_T = LoadProfileDataGen.GetData(Load_Profile_CommObj.buffer);
                }
                return IsSuccess;
            }
            catch (ThreadAbortException ex)
            {
                IsSuccess = false;
                InnerExceptionEx = ex;
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                InnerExceptionEx = new Exception("Error occurred reading load profile data_Read_LoadProfileDataRaw", ex);
                if (ex.HelpLink != null && ex.HelpLink.Contains("WR76"))
                {
                    InnerExceptionEx.HelpLink = "LP76";
                }
            }
            finally
            {
                if (InnerExceptionEx != null && SetInnerException != null)
                {
                    SetInnerException.Invoke(InnerExceptionEx);
                }
                if (LoadProfileValues != null && LoadProfileValues_T != null && LoadProfileValues_T.Count > 0)
                {
                    LoadProfileValues.Clear();
                    LoadProfileValues.AddRange(LoadProfileValues_T);
                }
            }
            return IsSuccess;
        }
        protected List<ILValue[]> Read_LoadProfileDataRaw(IAccessSelector Selector, LoadProfileScheme LP_Scheme)
        {
            try
            {
                //Class_7 Load_Profile_CommObj = (Class_7)GetSAPEntry(Get_Index.Load_Profile);

                Get_Index LP_Index = GetLoadProfileIndex(LP_Scheme);
                Class_7 Load_Profile_CommObj = (Class_7)GetSAPEntry(LP_Index);

                ///Getting LoadProfile Channels Info
                ///Reading Load Profile Data
                Load_Profile_CommObj.DecodingAttribute = 0x02;
                Load_Profile_CommObj.AccessSelector = Selector;
                GET_Object(Load_Profile_CommObj);
                ///Generate Load Profile Data
                List<ILValue[]> LoadProfileValues = null;
                IDataGenerator LoadProfileDataGen = new LoadProfileDataGenerator();
                if (Load_Profile_CommObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready &&
                    Load_Profile_CommObj.buffer != null)
                {
                    LoadProfileValues = LoadProfileDataGen.GetData(Load_Profile_CommObj.buffer);
                }
                else
                    throw new Exception(String.Format("Error occurred reading load profile data,{0}", Load_Profile_CommObj.GetAttributeDecodingResult(0x02)));
                return LoadProfileValues;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred reading load profile data\nDetails:{ex.Message}");
            }
        }

        #endregion

        #region Load Profile Data in Chunks
        public bool TryGet_LoadProfileDataInChunks(LoadProfileScheme LP_Scheme, Profile_Counter LP_Counters, LoadProfileData LPData, List<LoadProfileChannelInfo> _LoadProfileChannelsInfo,
                                                   Action<Exception> SetInnerException = null, uint MaxChunkSize = 30,
                                                   int MaxIteration = 15, CancellationTokenSource CancelToken = null)
        {
            Exception InnerException = null;
            bool IsSuccess = false;
            List<ILValue[]> EventRawData = new List<ILValue[]>();
            long I_diff_Counter = 0, IL_Counter = 0, IH_Counter = 0, start_Counter = 0;
            uint ct_minCount = 0;
            uint ct_maxCount = 0;
            uint ct_MaxCount_Main = 0;
            const int MaxLoadProfileChunkIterations = 2;

            /// Init Load Profile Data Containers
            LoadProfileData tempLoadProfileData = new LoadProfileData();
            LPData.ChannelsInstances = new List<LoadProfileItem>();

            try
            {
                // Read Counter again if internal count is less than or equal to zero
                if (LP_Counters.Meter_Counter <= 0)
                {
                    LP_Counters.Meter_Counter = Get_LoadProfileCounter_Internal(LP_Scheme);
                }
                long difference = LP_Counters.Meter_Counter - LP_Counters.DB_Counter;
                /// Load Profile Is Updated
                if (difference == 0 && LP_Counters.Meter_Counter > 0)
                {
                    IsSuccess = true;
                    return IsSuccess;
                }
                else
                {
                    /// Validate Channels Info
                    if (_LoadProfileChannelsInfo == null || _LoadProfileChannelsInfo.Count <= 0)
                        throw new Exception("Error occurred while getting Load Profile Channel info");
                    LPData.ChannelsInfo = _LoadProfileChannelsInfo;
                    tempLoadProfileData.ChannelsInfo = _LoadProfileChannelsInfo;
                }
                /// Update Difference
                difference = (long)LP_Counters.Meter_Counter - LP_Counters.DB_Counter;
                /// Init Inter-Load Profile Counters
                start_Counter = (LP_Counters.Meter_Counter > LP_Counters.Max_Size) ?
                    (LP_Counters.Meter_Counter - LP_Counters.Max_Size) : 0;
                IL_Counter = (start_Counter <= LP_Counters.DB_Counter) ? LP_Counters.DB_Counter : start_Counter;
                IH_Counter = (difference <= MaxChunkSize) ? LP_Counters.Meter_Counter : (IL_Counter + MaxChunkSize - 1);
                /// Init Max Iteration Counters
                int Iteration_Counter = 0;
                //Max_Sync_Iteration = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(difference / MaxChunkSize))) + Extra_Iteration;

                Max_Sync_Iteration = MaxIteration;

                List<ILValue[]> LoadProfileValues = new List<ILValue[]>();
                EntryDescripter event_entry = null;
                uint counter = 1;

                while (IL_Counter < LP_Counters.Meter_Counter &&
                    IL_Counter >= LP_Counters.DB_Counter &&
                    IL_Counter != IH_Counter &&
                    (Iteration_Counter++ < Max_Sync_Iteration) &&
                    (Iteration_Counter < MaxLoadProfileChunkIterations)
                    )
                {
                    try
                    {
                        #region Compute Entry Descriptor
                        I_diff_Counter = IH_Counter - IL_Counter;
                        event_entry = MakeEntry(LP_Counters.Meter_Counter, IL_Counter, MaxChunkSize, LP_Counters.Max_Size);
                        #endregion

                        IsSuccess = Read_LoadProfileDataRaw(LP_Scheme, event_entry, LoadProfileValues, (x) => InnerException = x, CancelToken);
                        if (LoadProfileValues.Count > 0)
                        {
                            tempLoadProfileData = FormatLoadProfileData(LoadProfileValues, ref counter);
                        }
                        else
                            break;
                        #region Raise Internal Error

                        if (!IsSuccess || InnerException != null)
                        {
                            if (InnerException != null)
                                throw InnerException;
                            else
                                throw new Exception("Error occurred while reading load profile data");
                        }

                        #endregion
                    }
                    finally
                    {
                        if (tempLoadProfileData != null &&
                            tempLoadProfileData.ChannelsInstances != null &&
                            tempLoadProfileData.ChannelsInstances.Count > 0)
                        {
                            ///Update In Main Data Container
                            LPData.ChannelsInstances.AddRange(tempLoadProfileData.ChannelsInstances);
                            ct_minCount = tempLoadProfileData.MinCounter;
                            ct_maxCount = tempLoadProfileData.MaxCounter;
                            ct_MaxCount_Main = LPData.MaxCounter;
                            tempLoadProfileData.ChannelsInstances.Clear();
                        }
                        if (tempLoadProfileData.CounterAvailable)
                        {
                            #region Perform Counters Analysis

                            ///Load Profile Counters Missing re-Request Old Data
                            if ((IL_Counter + 1) < ct_minCount && ct_minCount > (start_Counter + 1))
                            {
                                long diff_recv = Math.Abs(ct_minCount - IL_Counter);
                                ///Update Current Counter,Start_Counters Here
                                if (diff_recv > MaxChunkSize)
                                {
                                    LP_Counters.Meter_Counter = Get_LoadProfileCounter_Internal(LP_Scheme);
                                    ///Init Inter- Event Counters
                                    start_Counter = (LP_Counters.Meter_Counter > LP_Counters.Max_Size) ?
                                                    (LP_Counters.Meter_Counter - LP_Counters.Max_Size) : 0;
                                    IL_Counter = (start_Counter <= ct_minCount) ? ct_minCount : start_Counter;
                                    IH_Counter = (difference <= MaxChunkSize) ? ct_minCount : (IL_Counter + MaxChunkSize - 1);
                                }
                                else
                                {
                                    ///Update Here Current_Counter etc
                                    LP_Counters.Meter_Counter = (uint)(LP_Counters.Meter_Counter + diff_recv);
                                    ///Init Inter- Event Counters
                                    start_Counter = (LP_Counters.Meter_Counter > LP_Counters.Max_Size) ?
                                        (LP_Counters.Meter_Counter - LP_Counters.Max_Size) : 0;
                                    IL_Counter = (start_Counter <= ct_minCount) ? ct_minCount : start_Counter;
                                    IH_Counter = (difference <= MaxChunkSize) ? ct_minCount : (IL_Counter + MaxChunkSize - 1);
                                }
                                if (diff_recv > MaxChunkSize)
                                    LP_Counters.Meter_Counter = Get_LoadProfileCounter_Internal(LP_Scheme);
                                else
                                    ///Update Here Current_Counter etc
                                    LP_Counters.Meter_Counter = (uint)(LP_Counters.Meter_Counter + diff_recv);
                                ///Init Inter- LP Counters
                                start_Counter = (LP_Counters.Meter_Counter > LP_Counters.Max_Size) ?
                                                (LP_Counters.Meter_Counter - LP_Counters.Max_Size) : 0;
                                IL_Counter = (start_Counter <= IL_Counter) ? IL_Counter : start_Counter;
                                IH_Counter = (difference <= MaxChunkSize) ? ct_minCount : (IL_Counter + MaxChunkSize - 1);
                                ///Update Next Data Request
                                if (IL_Counter >= ct_minCount)
                                {
                                    ///Init Inter-Load Profile Counters
                                    IL_Counter = (ct_MaxCount_Main > ct_maxCount) ? ct_MaxCount_Main : ct_maxCount;
                                    long newCounter = IL_Counter + MaxChunkSize - 1;
                                    IH_Counter = (newCounter > LP_Counters.Meter_Counter) ? LP_Counters.Meter_Counter : (newCounter);
                                }
                            }
                            else if (ct_maxCount < IH_Counter)
                            {
                                ///Init Inter-Event Counters
                                IL_Counter = ct_maxCount;
                            }
                            ///Counters As Expected
                            ///Compute Next Chunk IL_Counter,IH_Coutner 
                            else
                            {
                                ///Init Inter-Event Counters
                                IL_Counter = (ct_MaxCount_Main > ct_maxCount) ? ct_MaxCount_Main : ct_maxCount;
                                long newCounter = IL_Counter + MaxChunkSize - 1;
                                IH_Counter = (newCounter > LP_Counters.Meter_Counter) ? LP_Counters.Meter_Counter : (newCounter);
                            }
                            #endregion
                        }
                        if (CancelToken != null && CancelToken.IsCancellationRequested)
                        {
                            IsSuccess = false;
                            throw new TaskCanceledException();
                        }
                    }
                }
                return IsSuccess;
            }
            catch (TaskCanceledException ex)
            {
                InnerException = ex;
            }

            catch (ThreadAbortException ex)
            {
                InnerException = ex;
            }
            catch (Exception ex)
            {
                InnerException = new Exception(String.Format("Error occurred while reading load Profile"), ex);
                if (ex.HelpLink != null && ex.HelpLink.Contains("LP76") ||
                    ex.InnerException != null && ex.InnerException.HelpLink != null && ex.InnerException.HelpLink.Contains("76")) InnerException.HelpLink = "LP76";
            }
            finally
            {
                if (tempLoadProfileData.CounterAvailable)
                {
                    #region  //Remove Duplicate LoadProfile Counters

                    if (LPData != null && LPData.ChannelsInstances != null && LPData.ChannelsInstances.Count > 0)
                    {

                        for (uint LP_Counter = LP_Counters.DB_Counter; LP_Counter < LP_Counters.Meter_Counter; LP_Counter++)
                        {
                            List<LoadProfileItem> t = LPData.ChannelsInstances.FindAll((x) => x != null && x.Counter == LP_Counter);
                            if (t.Count > 1)
                            {
                                t.RemoveAt(0);
                                foreach (var item in t)
                                {
                                    LPData.ChannelsInstances.Remove(item);
                                }
                            }
                        }
                    }

                    #endregion
                }
                LPData.CounterAvailable = tempLoadProfileData.CounterAvailable;
                LPData.IntervalAvailable = tempLoadProfileData.IntervalAvailable;
                LPData.ClockAvailable = tempLoadProfileData.ClockAvailable;
                LPData.StatusWordAvailable = tempLoadProfileData.StatusWordAvailable;

                LPData.ChannelGroupId = LP_Counters.GroupId;
                if (SetInnerException != null && InnerException != null)
                    SetInnerException(InnerException);
            }
            return IsSuccess;
        }
        public bool TryGet_LoadProfileDataByDateTime(LoadProfileScheme LP_Scheme, Profile_Counter LP_ProfileInfo, LoadProfileData LPData, List<LoadProfileChannelInfo> _LoadProfileChannelsInfo,
                                            Action<Exception> SetInnerException = null, CancellationTokenSource CancelToken = null)
        {
            #region Declaration and Initialization
            Exception InnerException = null;
            bool IsSuccess = true;
            LoadProfileData tempLoadProfileData = new LoadProfileData();
            LPData.ChannelsInstances = new List<LoadProfileItem>();
            DateTime FromTime = LP_ProfileInfo.LastReadTime;
            DateTime ToTime = (LP_ProfileInfo.ToTime == DateTime.MinValue ? DateTime.Now: LP_ProfileInfo.ToTime);
            TimeSpan TotalSpanToBeRead = ToTime - FromTime;
            TimeSpan ChunkSize = new TimeSpan(0, (int)(LP_ProfileInfo.Period.TotalMinutes * LP_ProfileInfo.ChunkSize), 0);
            LPData.ChannelsInfo = _LoadProfileChannelsInfo;
            tempLoadProfileData.ChannelsInfo = _LoadProfileChannelsInfo;
            #endregion
            try
            {
                if (FromTime >= ToTime)
                {
                    return IsSuccess;
                }
                if (_LoadProfileChannelsInfo == null && _LoadProfileChannelsInfo.Count <= 0)
                    throw new Exception("Error Occured While Getting Load Profile Channels Info.");

                //if (TotalSpanToBeRead > ChunkSize && FromTime.AddMinutes(ChunkSize.TotalMinutes)<DateTime.Now)
                //{
                //    ToTime = FromTime.AddMinutes(ChunkSize.TotalMinutes);
                //}
                List<ILValue[]> LoadProfileValues = new List<ILValue[]>();
                IAccessSelector descriptor = null;
                DateTime tmpToTime = DateTime.MinValue;
                uint counter = 1;
                //while ((FromTime < ToTime) && LPData.ChannelsInstances.Count <= LP_ProfileInfo.ChunkSize)
                while ((FromTime < ToTime) && LPData.ChannelsInstances.Count <= LP_ProfileInfo.ChunkSize)
                {
                    #region read Data from Meter
                    try
                    {
                        tmpToTime = FromTime.AddMinutes((double)LP_ProfileInfo.Period.TotalMinutes * LP_ProfileInfo.ChunkSize);
                        if (tmpToTime > ToTime) tmpToTime = ToTime;
                        descriptor = new RangeDescripter()
                        {
                            EncodingDataType = DataTypes._A19_datetime,
                            FromEntry = FromTime,
                            ToEntry = tmpToTime
                        };
                        IsSuccess = Read_LoadProfileDataRaw(LP_Scheme, descriptor, LoadProfileValues, (x) => InnerException = x, CancelToken);

                        if (LoadProfileValues.Count > 0)
                        {
                            tempLoadProfileData = FormatLoadProfileData(LoadProfileValues, ref counter);
                        }
                        //else
                        //    break;// Added by Sajid
                        FromTime = tmpToTime.AddSeconds(1);//to read from next entry
                        if (!IsSuccess || InnerException != null)
                        {
                            if (InnerException != null)
                                throw InnerException;
                            else
                                throw new Exception("Error Occured While reading Load Profile Data.");
                        }
                    }
                    finally
                    {
                        if (tempLoadProfileData != null &&
                            tempLoadProfileData.ChannelsInstances != null &&
                            tempLoadProfileData.ChannelsInstances.Count > 0)
                        {
                            //Updae in Main Data Container
                            LPData.ChannelsInstances.AddRange(tempLoadProfileData.ChannelsInstances);
                            tempLoadProfileData.ChannelsInstances.Clear();
                        }
                    }
                    #endregion
                }
            }
            catch (TaskCanceledException ex)
            {
                InnerException = ex;
            }
            catch (ThreadAbortException ex)
            {
                InnerException = ex;
            }
            catch (Exception ex)
            {
                InnerException = new Exception(string.Format("Error Occured while reading load profile"), ex);
                if (ex.HelpLink != null && ex.HelpLink.Contains("LP76") ||
                    ex.InnerException != null && ex.InnerException.HelpLink != null && ex.InnerException.HelpLink.Contains("LP76"))
                    InnerException.HelpLink = "LP76";
            }
            finally
            {
                LPData.CounterAvailable = tempLoadProfileData.CounterAvailable;
                LPData.IntervalAvailable = tempLoadProfileData.IntervalAvailable;
                LPData.ClockAvailable = tempLoadProfileData.ClockAvailable;
                LPData.StatusWordAvailable = tempLoadProfileData.StatusWordAvailable;

                LP_ProfileInfo.LastReadTime = FromTime;
                LPData.ChannelGroupId = LP_ProfileInfo.GroupId;
                if (SetInnerException != null && InnerException != null)
                    SetInnerException(InnerException);
            }
            return IsSuccess;
        }

        #endregion

        #region Get Load Profile Methods

        public GenericProfileInfo Get_LoadProfileInternal_Counter(LoadProfileScheme lp_scheme)
        {
            try
            {
                if (LoadProfileInfo == null)
                    LoadProfileInfo = new GenericProfileInfo();
                Get_Index LP_Index = GetLoadProfileCounterIndex(lp_scheme);

                Class_1 LoadProfile_CommObj = (Class_1)GetSAPEntry(LP_Index);
                ///Request Capture Object Buffer Read Only On UpdateChannelInfo or Not CaptureObjectList Init
                //if (LoadProfile_CommObj.IsAttribReadable(0x03) && (!LoadProfile_CommObj.IsCaptureObjectListIntialized || this.updateChannelInfo))

                if (LoadProfile_CommObj.IsAttribReadable(0x02))
                {
                    LoadProfile_CommObj = (Class_1)GET_Object(LP_Index, 0x02);
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                        LoadProfileInfo.EntriesInUse = Convert.ToUInt32(LoadProfile_CommObj.Value);
                    else
                        throw new Exception(String.Format("Error reading Entries In Use {0}",
                            LoadProfile_CommObj.GetAttributeDecodingResult(0x02)));
                }
                return LoadProfileInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading LoadProfile Info data", ex);
            }
        }
        public uint Get_LoadProfileInfo_Counter(LoadProfileScheme LP_Scheme)
        {
            uint LP_Current_Counter = 0;
            try
            {
                // LoadProfileInfo = new GenericProfileInfo();
                Get_Index LP_Profile_Counter = GetLoadProfileCounterIndex(LP_Scheme);
                Class_1 LoadProfile_CommObj = (Class_1)GetSAPEntry(LP_Profile_Counter);

                if (LoadProfile_CommObj.IsAttribReadable(0x02))
                {
                    LoadProfile_CommObj = (Class_1)GET_Object(LP_Profile_Counter, 0x02);

                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                        LP_Current_Counter = Convert.ToUInt32(LoadProfile_CommObj.Value);
                    else
                        throw new Exception(String.Format("Error reading Load Profile Counter {0}"
                                            , LoadProfile_CommObj.GetAttributeDecodingResult(0x02)));
                }
                else
                    throw new Exception(String.Format("Error reading Load Profile Counter,No Access"));

                return LP_Current_Counter;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading LoadProfile Info data", ex);
            }
            finally
            {
                if (LPInfo_Data != null)
                    LPInfo_Data.LoadProfileCounter = LP_Current_Counter;
            }
        }
        public static Get_Index GetLoadProfileIndex(LoadProfileScheme lpScheme)
        {
            Get_Index LP_Index = Get_Index.Load_Profile;
            switch (lpScheme)
            {
                case LoadProfileScheme.Load_Profile_Channel_2: LP_Index = Get_Index.Load_Profile_Channel_2; break;
                case LoadProfileScheme.Daily_Load_Profile: LP_Index = Get_Index.Daily_Load_Profile; break;
            }
            return LP_Index;
        }
        private Get_Index GetLoadProfileCounterIndex(LoadProfileScheme lpScheme)
        {
            Get_Index LP_Index = Get_Index.Load_Profile_Counter;
            switch (lpScheme)
            {
                case LoadProfileScheme.Load_Profile_Channel_2: LP_Index = Get_Index.Load_Profile_Counter_2; break;
                case LoadProfileScheme.Daily_Load_Profile: LP_Index = Get_Index.PQ_Load_Profile_Counter; break;
            }
            return LP_Index;
        }
        private List<CaptureObject> GetLoadProfileBufferInfo()
        {
            List<CaptureObject> Info = new List<CaptureObject>();
            //Meter_Clock Class id 8 Attribute 02  Data Index 00
            CaptureObject Obj_1 = new CaptureObject();
            StOBISCode OBis = Get_Index.Meter_Clock;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.DataIndex = 0;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);
            //LoadPrileCounter                      Class id 1   Attribut 02  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Load_Profile_Counter;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);
            ///LoadProfileCapturePeriod              Class id 1   Attribut 02  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Load_Profile_Capture_Period;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            //Cumulative_TariffTL_KwhAbsolute             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Energy_Absolute_TL;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            ///Cumulative_TariffTL_KvarhAbsolute             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Energy_Absolute_TL;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            ///MDI_Parameters             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.DEMAND_ACTIVE_IMPORT;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            ///Total_Current             Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Total_Current;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            Info.Add(Obj_1);

            return Info;
        }
        public TimeSpan Get_LoadProfileInterval(LoadProfileScheme LP_Scheme)
        {
            Class_7 Load_Profile = null;
            try
            {
                #region GET_LoadProfilePeriod
                Get_Index LP_Index = GetLoadProfileIndex(LP_Scheme);
                Load_Profile = (Class_7)GET_Object(LP_Index, 4);
                #endregion
                return ConvertTimeSpan(Load_Profile.capturePeriod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ///Restore Previous Load Profile Config Info
                if (Load_Profile != null)
                { }
            }
        }
        public LoadProfileData Get_LoadProfileData(IAccessSelector SelectiveAccess, LoadProfileScheme LP_Scheme, ref uint counter)
        {
            try
            {
                ///Generate Load Profile Data
                LoadProfileData LoadProfileData = null;
                IDataGenerator LoadProfileDataGen = new LoadProfileDataGenerator();
                List<ILValue[]> LoadProfileValues = Read_LoadProfileDataRaw(SelectiveAccess, LP_Scheme);
                LoadProfileData = FormatLoadProfileData(LoadProfileValues, ref counter);
                return LoadProfileData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading load profile data");
            }
        }
        public uint Get_LoadProfileCounter_Internal(LoadProfileScheme lp_scheme)
        {
            try
            {
                //*** Comment Code Temporary Till Load Profile Counter Vz Works
                Get_Index LP_Index = GetLoadProfileCounterIndex(lp_scheme);
                Class_1 LoadProfile_Internal_Counter = (Class_1)GetSAPEntry(LP_Index);
                LoadProfile_Internal_Counter.DecodingAttribute = 0x02;
                // resetting value to 0 after getting object from cache
                LoadProfile_Internal_Counter.Value = 0;
                LoadProfile_Internal_Counter = (Class_1)GET_Object(LoadProfile_Internal_Counter);

                if (LoadProfile_Internal_Counter.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    return Convert.ToUInt32(LoadProfile_Internal_Counter.Value);
                }
                else
                    // throw new Exception("Error getting monthly billing internal counter");
                    return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting monthly billing internal counter", ex);
            }
        }
        public List<LoadProfileChannelInfo> Get_LoadProfileChannels(LoadProfileScheme LP_Scheme)
        {
            try
            {
                updateChannelInfo = true;
                try
                {
                    LoadProfileInfo = Get_LoadProfileInfo(LP_Scheme);
                }
                catch
                {
                    // Log Exception 
                    // Raise Error
                }

                this.ChannelInfo = Get_LoadProfileChannelsData(LoadProfileInfo);

                foreach (var item in ChannelInfo) //Azeem
                    item.Scheme = LP_Scheme;

                return ChannelInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting LoadProfile Channels Info", ex);
            }
            //finally
            //{
            //    int chId = 1;
            //    foreach (var channel in ChannelInfo)
            //    {
            //        if (channel != null)
            //            channel.Channel_id = chId++;
            //    }
            //}
        }
        public List<LoadProfileChannelInfo> Get_FixedChannels()
        {
            List<LoadProfileChannelInfo> listChannels = new List<LoadProfileChannelInfo>();
            LoadProfileChannelInfo channelToAdd = null;
            #region Date Time
            channelToAdd = new LoadProfileChannelInfo()
            {
                OBIS_Index = Get_Index.Meter_Clock,
                Quantity_Name = Enum.GetName(typeof(Get_Index), Get_Index.Meter_Clock),
                SelectedAttribute = 2,
                Unit = Unit.UnitLess
            };
            listChannels.Add(channelToAdd);
            #endregion

            #region Load Profile Counter
            channelToAdd = new LoadProfileChannelInfo()
            {
                OBIS_Index = Get_Index.Load_Profile_Counter,
                Quantity_Name = Enum.GetName(typeof(Get_Index), Get_Index.Load_Profile_Counter),
                SelectedAttribute = 2,
                Unit = Unit.UnitLess
            };
            listChannels.Add(channelToAdd);
            #endregion

            #region Load Profile 2 Counter
            channelToAdd = new LoadProfileChannelInfo()
            {
                OBIS_Index = Get_Index.Load_Profile_Counter_2,
                Quantity_Name = Enum.GetName(typeof(Get_Index), Get_Index.Load_Profile_Counter_2),
                SelectedAttribute = 2,
                Unit = Unit.UnitLess
            };
            listChannels.Add(channelToAdd);
            #endregion

            #region Load Profile Capture Period
            channelToAdd = new LoadProfileChannelInfo()
            {
                OBIS_Index = Get_Index.Load_Profile_Capture_Period,
                Quantity_Name = Enum.GetName(typeof(Get_Index), Get_Index.Load_Profile_Capture_Period),
                SelectedAttribute = 2,
                Unit = Unit.UnitLess
            };
            listChannels.Add(channelToAdd);
            #endregion

            #region Load Profile2 Capture Period
            channelToAdd = new LoadProfileChannelInfo()
            {
                OBIS_Index = Get_Index.Load_Profile_Capture_Period_2,
                Quantity_Name = Enum.GetName(typeof(Get_Index), Get_Index.Load_Profile_Capture_Period_2),
                SelectedAttribute = 2,
                Unit = Unit.UnitLess
            };
            listChannels.Add(channelToAdd);
            #endregion

            #region Load Profile Status Word
            channelToAdd = new LoadProfileChannelInfo()
            {
                OBIS_Index = Get_Index.OT_STATUS_WORD_LP1,
                Quantity_Name = Enum.GetName(typeof(Get_Index), Get_Index.OT_STATUS_WORD_LP1),
                SelectedAttribute = 2,
                Unit = Unit.UnitLess
            };
            listChannels.Add(channelToAdd);
            #endregion

            #region Status Word 2

            channelToAdd = new LoadProfileChannelInfo()
            {
                OBIS_Index = Get_Index.OT_STATUS_WORD_LP2,
                Quantity_Name = Enum.GetName(typeof(Get_Index), Get_Index.OT_STATUS_WORD_LP2),
                SelectedAttribute = 2,
                Unit = Unit.UnitLess
            };
            listChannels.Add(channelToAdd);

            #endregion

            return listChannels;
        }
        public GenericProfileInfo Get_LoadProfileInfo(LoadProfileScheme LP_Scheme)
        {
            try
            {
                LoadProfileInfo = new GenericProfileInfo();

                Get_Index LP_OBIS = GetLoadProfileIndex(LP_Scheme);

                Class_7 LoadProfile_CommObj = (Class_7)GetSAPEntry(LP_OBIS);
                // Request Capture Object Buffer Read Only On UpdateChannelInfo or Not CaptureObjectList Init
                // if (LoadProfile_CommObj.IsAttribReadable(0x03) && (!LoadProfile_CommObj.IsCaptureObjectListIntialized || this.updateChannelInfo))
                if (LoadProfile_CommObj.IsAttribReadable(0x03))
                {
                    ///Request Capture Object List
                    LoadProfile_CommObj = (Class_7)GET_Object(LP_OBIS, 3);
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready
                        && LoadProfile_CommObj.captureObjectsList != null)
                    {
                        if (!LoadProfile_CommObj.IsCaptureObjectListIntialized)
                            LoadProfile_CommObj.InitializeCaptureBuffer(1);
                        LoadProfileInfo.CaptureBufferInfo.AddRange(LoadProfile_CommObj.BaseClassCaptureObjectsList);
                    }
                }
                else if (LoadProfile_CommObj.IsCaptureObjectListIntialized)
                {
                    LoadProfile_CommObj.InitializeCaptureBuffer(1);
                    LoadProfileInfo.CaptureBufferInfo.AddRange(LoadProfile_CommObj.BaseClassCaptureObjectsList);
                }
                else
                    throw new Exception("Error occurred reading Load Profile Info,unable to read Capture ObjectInfo");
                ///Request Capture Period
                if (LoadProfile_CommObj.IsAttribReadable(0x04))
                {
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x04) != DecodingResult.Ready || updateChannelInfo)
                    ///Don't Request If Data Already Ready OR UpdateChannelInfo Request
                    {
                        LoadProfile_CommObj = (Class_7)GET_Object(LP_OBIS, 0x04);
                    }
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                        LoadProfileInfo.CapturePeriod = ConvertTimeSpan(LoadProfile_CommObj.capturePeriod);
                }
                /////Request Sort Method
                if (LoadProfile_CommObj.IsAttribReadable(0x05))
                {
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x05) != DecodingResult.Ready || updateChannelInfo)
                    ///Don't Request If Data Already Ready & UpdateChannelInfo Request is false
                    {
                        LoadProfile_CommObj = (Class_7)GET_Object(LP_OBIS, 0x05);
                    }
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                        LoadProfileInfo.SortMethod = (SortMethod)LoadProfile_CommObj.sortMethod;
                }
                ///Request entries_in_use
                if (LoadProfile_CommObj.IsAttribReadable(0x07))
                {
                    LoadProfile_CommObj = (Class_7)GET_Object(LP_OBIS, 0x07);
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                        LoadProfileInfo.EntriesInUse = LoadProfile_CommObj.entriesInUse;
                    else
                        throw new Exception(String.Format("Error reading Entries In Use {0}",
                            LoadProfile_CommObj.GetAttributeDecodingResult(0x07)));
                }
                /////Request Profile_Entries
                if (LoadProfile_CommObj.IsAttribReadable(0x08))
                {
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x08) != DecodingResult.Ready || updateChannelInfo)
                    ///Don't Request If Data Already Ready & UpdateChannelInfo Request is false
                    {
                        LoadProfile_CommObj = (Class_7)GET_Object(LP_OBIS, 0x08);
                    }
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                        LoadProfileInfo.MaxEntries = LoadProfile_CommObj.MaxProfileEntries;
                    else
                        throw new Exception(String.Format("Error reading Max Profile Entries {0}",
                            LoadProfile_CommObj.GetAttributeDecodingResult(0x08)));
                }
                ///Mark Channel Info Read
                this.updateChannelInfo = false;
                return LoadProfileInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading LoadProfile Info data", ex);
            }
        }

        public uint Get_LP_MaxEntries(LoadProfileScheme LP_Scheme)
        {
            try
            {
                uint entriesInUse = 0 ;
                Get_Index LP_OBIS = GetLoadProfileIndex(LP_Scheme);

                Class_7 LoadProfile_CommObj = (Class_7)GetSAPEntry(LP_OBIS);

                /////Request Profile_Entries
                if (LoadProfile_CommObj.IsAttribReadable(0x08))
                {
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x08) != DecodingResult.Ready || updateChannelInfo)
                    ///Don't Request If Data Already Ready & UpdateChannelInfo Request is false
                    {
                        LoadProfile_CommObj = (Class_7)GET_Object(LP_OBIS, 0x08);
                    }
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                        entriesInUse = LoadProfile_CommObj.MaxProfileEntries;
                    else
                        throw new Exception(String.Format("Error reading Max Profile Entries {0}",
                            LoadProfile_CommObj.GetAttributeDecodingResult(0x08)));
                }

                return entriesInUse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading LoadProfile Info data", ex);
            }
        }
        public List<LoadProfileChannelInfo> Get_LoadProfileChannels()
        {
            try
            {
                LoadProfileInfo = Get_LoadProfileInfo();
                this.ChannelInfo = Get_LoadProfileChannelsData(LoadProfileInfo);

                return ChannelInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting LoadProfile Channels Info", ex);
            }
        }
        public List<LoadProfileChannelInfo> Get_SelectableLoadProfileChannels(ConnectionInfo ConnInfo = null) //If Dummy Connection, then should be passed Externally
        {
            try
            {
                ILoadProfileDataFormatter dataFormatter = new LoadProfileDataFormatter();
                List<LoadProfileChannelInfo> AllSelectableValues = null;
                try
                {
                    if(ConnInfo == null)
                        AllSelectableValues = ((LoadProfileDataFormatter)dataFormatter).GetLoadProfileItemsFormat(Configurator, CurrentConnectionInfo);
                    else
                        AllSelectableValues = ((LoadProfileDataFormatter)dataFormatter).GetLoadProfileItemsFormat(Configurator, ConnInfo);
                }
                catch (Exception ex)                                                           
                {
                    AllSelectableValues = null;
                }
                if (AllSelectableValues == null || AllSelectableValues.Count == 0)
                    AllSelectableValues = dataFormatter.GetLoadProfileItemsFormatX();
                return AllSelectableValues;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading LoadProfile Channels Selectable", ex);
            }
        }

        public List<LoadProfileChannelInfo> Get_LoadProfileChannelsInfo()
        {
            try
            {
                LoadProfileInfo = Get_LoadProfileInfo();
                this.ChannelInfo = Get_LoadProfileChannelsData(LoadProfileInfo);
                return ChannelInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting LoadProfile Channels Info", ex);
            }
        }
        private List<LoadProfileChannelInfo> Get_LoadProfileChannelsData(GenericProfileInfo LoadProfileInfo, bool IncludeFixChannels = false)
        {
            try
            {
                //Class_7 Load_Profile_CommObj = (Class_7)GetSAPEntry(Get_Index.Load_Profile);
                List<LoadProfileChannelInfo> ChannelInfo = new List<LoadProfileChannelInfo>();
                if (LoadProfileInfo != null && LoadProfileInfo.CaptureBufferInfo.Count > 0)
                {
                    ILoadProfileDataFormatter dataFormatter = new LoadProfileDataFormatter();
                    List<LoadProfileChannelInfo> AllSelectableValues = null;
                    try
                    {
                        AllSelectableValues = ((LoadProfileDataFormatter)dataFormatter).GetLoadProfileItemsFormat(Configurator, CurrentConnectionInfo);
                    }
                    catch (Exception)
                    {
                        AllSelectableValues = null;
                    }
                    if (AllSelectableValues == null || AllSelectableValues.Count == 0)
                        AllSelectableValues = dataFormatter.GetLoadProfileItemsFormatX();
                    int channelId = 1;
                    var Meter_Clock = _AP_Controller.GetOBISCode(Get_Index.Meter_Clock);
                    var Load_Profile_Counter = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Counter);
                    var Load_Profile_Counter_2 = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Counter_2);
                    var Load_Profile_Counter_3 = _AP_Controller.GetOBISCode(Get_Index.PQ_Load_Profile_Counter);
                    var Load_Profile_Capture_Period = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Capture_Period);
                    var Load_Profile_Capture_Period2 = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Capture_Period_2);
                    var Load_Profile_Capture_Period3 = _AP_Controller.GetOBISCode(Get_Index.PQ_Load_Profile_Capture_Period);
                    ///Read Load Profile Channels
                    foreach (var item in LoadProfileInfo.CaptureObjectsInfo)
                    {
                        //int indexLoadChannel = AllSelectableValues.FindIndex((x) => x.OBIS_Index == item.StOBISCode.OBISIndex);
                        LoadProfileChannelInfo ChannelInfoT = null;
                        //if (indexLoadChannel != -1)
                        //{
                        //    ChannelInfoT = AllSelectableValues[indexLoadChannel];
                        //    ChannelInfoT.Channel_id = channelId;
                        //    ChannelInfo.Add(ChannelInfoT);
                        //}
                        //else 
                        if (
                            IncludeFixChannels ||
                            (item.StOBISCode.OBISIndex != Meter_Clock &&
                            item.StOBISCode.OBISIndex != Load_Profile_Counter &&
                            item.StOBISCode.OBISIndex != Load_Profile_Counter_2 &&
                            item.StOBISCode.OBISIndex != Load_Profile_Counter_3 &&
                            item.StOBISCode.OBISIndex != Load_Profile_Capture_Period &&
                            item.StOBISCode.OBISIndex != Load_Profile_Capture_Period2 &&
                            item.StOBISCode.OBISIndex != Load_Profile_Capture_Period3
                            ))
                        {
                            ChannelInfoT = new LoadProfileChannelInfo();
                            ChannelInfoT.SelectedAttribute = item.AttributeIndex;
                            ChannelInfoT.OBIS_Index = item.StOBISCode.OBISIndex;
                            ChannelInfoT.Quantity_Name = Enum.GetName(typeof(Get_Index), item.StOBISCode.OBISIndex);
                            ChannelInfoT.Unit = Unit.UnitLess;
                            ChannelInfoT.Multiplier = item.Multiplier;
                            ChannelInfoT.Format = "f3";
                            ChannelInfoT.Channel_id = channelId;
                            ChannelInfoT.DbColumnName = item.DatabaseFieldName;
                            ChannelInfo.Add(ChannelInfoT);
                        }
                        channelId++;
                    }
                }
                else
                    throw new Exception("Error occurred while getting LoadProfile Channels Info,parameter object null reference");
                TimeSpan t = LoadProfileInfo.CapturePeriod;
                ///Request Capture Period
                foreach (var item in ChannelInfo)
                {
                    item.CapturePeriod = t;
                }
                return ChannelInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting LoadProfile Channels Info", ex);
            }

        }
        public GenericProfileInfo Get_LoadProfileInfo()
        {
            try
            {
                if (LoadProfileInfo == null)
                    LoadProfileInfo = new GenericProfileInfo();
                Class_7 LoadProfile_CommObj = (Class_7)GetSAPEntry(Get_Index.Load_Profile);
                // Request Capture Object Buffer Read Only On UpdateChannelInfo or Not CaptureObjectList Init
                // if (LoadProfile_CommObj.IsAttribReadable(0x03) && 
                // (!LoadProfile_CommObj.IsCaptureObjectListIntialized || this.updateChannelInfo))
                if (LoadProfile_CommObj.IsAttribReadable(0x03) || updateChannelInfo)
                {
                    // Request Capture Object List
                    LoadProfile_CommObj.captureObjectsList = null;
                    LoadProfile_CommObj = (Class_7)GET_Object(Get_Index.Load_Profile, 3);

                }
                else if (!LoadProfile_CommObj.IsAttribReadable(0x03))
                    throw new Exception(String.Format("Error occurred reading LoadProfile Info,No Channel Info read access,unable to read Capture ObjectInfo"));
                if (LoadProfile_CommObj.IsCaptureObjectListInit)
                {
                    LoadProfile_CommObj.BaseClassCaptureObjectsList = LoadProfile_CommObj.InitializeCaptureBuffer(1);
                    LoadProfileInfo.CaptureBufferInfo = new List<Base_Class>(LoadProfile_CommObj.BaseClassCaptureObjectsList);
                    LoadProfileInfo.CaptureObjectsInfo = new List<CaptureObject>(LoadProfile_CommObj.captureObjectsList);
                }
                else
                    throw new Exception(String.Format("Error occurred reading LoadProfile Info,unable to read Capture ObjectInfo"));
#if Enable_LoadTester_Mode
                if (!LoadProfile_CommObj.IsCaptureObjectListIntialized)
                {
                    throw new Exception("Error occurred reading Load Profile Info,unable to read Capture ObjectInfo");
                }
                ///Request Capture Period
                if (LoadProfile_CommObj.IsAttribReadable(0x04) || updateChannelInfo)
                {
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x04) != DecodingResult.Ready || updateChannelInfo)
                    ///Don't Request If Data Already Ready OR UpdateChannelInfo Request
                    {
                        LoadProfile_CommObj = (Class_7)GET_Object(Get_Index.Load_Profile, 0x04);
                    }
                    //if (LoadProfile_CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                    LoadProfileInfo.CapturePeriod = ConvertTimeSpan(LoadProfile_CommObj.capturePeriod);
                }
                /////Request Sort Method
                //   if (LoadProfile_CommObj.IsAttribReadable(0x05))
                //{
                //    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x05) != DecodingResult.Ready || updateChannelInfo)
                //    ///Don't Request If Data Already Ready & UpdateChannelInfo Request is false
                //    {
                //        LoadProfile_CommObj = (Class_7)GET_Object(Get_Index.Load_Profile, 0x05);
                //    }
                //    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                //        LoadProfileInfo.SortMethod = (SortMethod)LoadProfile_CommObj.sortMethod;
                //}
                ///Request entries_in_use
                if (LoadProfile_CommObj.IsAttribReadable(0x07) || updateChannelInfo)
                {
                    LoadProfile_CommObj = (Class_7)GET_Object(Get_Index.Load_Profile, 0x07);
                    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                        LoadProfileInfo.EntriesInUse = LoadProfile_CommObj.entriesInUse;
                    else
                        throw new Exception(String.Format("Error reading Entries In Use {0}",
                            LoadProfile_CommObj.GetAttributeDecodingResult(0x07)));
                }
                /////Request Profile_Entries
                //if (LoadProfile_CommObj.IsAttribReadable(0x08))
                //{
                //    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x08) != DecodingResult.Ready || updateChannelInfo)
                //    ///Don't Request If Data Already Ready & UpdateChannelInfo Request is false
                //    {
                //        LoadProfile_CommObj = (Class_7)GET_Object(Get_Index.Load_Profile, 0x08);
                //    }
                //    if (LoadProfile_CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                //        LoadProfileInfo.MaxEntries = LoadProfile_CommObj.MaxProfileEntries;
                //    else
                //        throw new Exception(String.Format("Error reading Max Profile Entries {0}",
                //            LoadProfile_CommObj.GetAttributeDecodingResult(0x08)));
                //}
#endif
                this.updateChannelInfo = false;
                return LoadProfileInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading LoadProfile Info data", ex);
            }
        }
        public List<LoadProfileChannelInfo> Get_LoadProfileInfoByChannelGroup(long? LP_ChannelGroupId, LoadProfileScheme LP_Scheme, bool IncludeFixChannels = false)
        {
            List<LoadProfileChannelInfo> LP_ChannelInfo = null;
            try
            {
                if ((LP_ChannelGroupId == null || LP_ChannelGroupId == 0))
                    throw new Exception("Invalid Load Profile Channels Group Id");
                StOBISCode LP_Index = GetLoadProfileIndex(LP_Scheme);
                Class_7 LP_CaptureObject = (Class_7)GetSAPEntry(LP_Index.OBISIndex);
                List<CaptureObject> LP_Capture_Objects = Configurator.GetProfileCaptureObjectList(CurrentConnectionInfo, LP_Index, LP_ChannelGroupId);
                LP_CaptureObject.captureObjectsList = new List<CaptureObject>(LP_Capture_Objects);
                LP_CaptureObject.BaseClassCaptureObjectsList = LP_CaptureObject.InitializeCaptureBuffer(1);

                // Update Load Profile Info
                if (LoadProfileInfo == null)
                {
                    LoadProfileInfo = new GenericProfileInfo();
                }

                LoadProfileInfo.CaptureBufferInfo = new List<Base_Class>(LP_CaptureObject.BaseClassCaptureObjectsList);
                LoadProfileInfo.CaptureObjectsInfo = LP_Capture_Objects;
                LoadProfileInfo.CapturePeriod = TimeSpan.FromMinutes(15.0);

                if (LoadProfileInfo.CaptureObjectsInfo != null &&
                    LoadProfileInfo.CaptureObjectsInfo.Count > 0)
                {
                    LP_ChannelInfo = Get_LoadProfileChannelsData(LoadProfileInfo, IncludeFixChannels);
                    ChannelInfo = LP_ChannelInfo;
                }

                return LP_ChannelInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting LoadProfile Channels Info", ex);
            }
        }
        public List<LoadProfileChannelInfo> GetChannelsInfoList(Profile_Counter LP_Counters, LoadProfileScheme LP_Scheme)
        {
            return Get_LoadProfileChannelsInfo(LP_Counters, LP_Scheme);
        }
        public List<LoadProfileChannelInfo> Get_LoadProfileChannelsInfo(Profile_Counter LP_Counters, LoadProfileScheme LP_Scheme)
        {
            List<LoadProfileChannelInfo> LoadProfileChannelsInfo = null;
            try
            {
                //uint difference = LP_Counters.Meter_Counter - LP_Counters.DB_Counter;
                //if (difference == 0 && LP_Counters.Meter_Counter > 0)
                //{
                //    return null;
                //}
                //else if (difference > 0)
                {
                    try
                    {
                        // Get_Load_Profile_Channels
                        LoadProfileChannelsInfo = Get_LoadProfileInfoByChannelGroup(LP_Counters.GroupId, LP_Scheme);
                    }
                    catch (Exception ex)
                    {

                        ex.HelpLink = "Error:LP76";
#if Enable_DEBUG_RunMode
                        LoadProfileChannelsInfo = null;
#endif
#if !Enable_DEBUG_RunMode
                        throw ex;
#endif
                    }
                    // Read Load Profile Channels From Meter 
                    if (LoadProfileChannelsInfo == null || LoadProfileChannelsInfo.Count <= 0)
                    {
                        LoadProfileChannelsInfo = Get_LoadProfileChannelsInfo();
                        uint? GroupId = 0;
                        Update_LoadProfileChannelsInfo(LoadProfileInformation.CaptureObjectsInfo, LoadProfileChannelsInfo, out GroupId, LP_Scheme);
                        LP_Counters.GroupId = (uint)GroupId;
                    }
                }
                //                // Validate If Load_Profile_Channels 
                //                // Re-programmed (difference < 0)
                //                else if (difference < 0)
                //                {
                //                    try
                //                    {
                //                        ///Get_Load_Profile_Channels
                //                        LoadProfileChannelsInfo = Get_LoadProfileInfoByChannelGroup(LP_Counters.GroupId, LP_Scheme);
                //                    }
                //                    catch (Exception ex)
                //                    {
                //                        ex.HelpLink = "Error:LP76";
                //#if Enable_DEBUG_RunMode
                //                        LoadProfileChannelsInfo = null;
                //#endif
                //#if !Enable_DEBUG_RunMode
                //                        throw ex;
                //#endif
                //                    }
                //                    List<LoadProfileChannelInfo> LoadProfileChannelsInfo_t = Get_LoadProfileChannelsInfo();
                //                    // Load Profile Channels Reprogrammed
                //                    if (LoadProfileChannelsInfo == null || LoadProfileChannelsInfo.Count <= 0 ||
                //                        !IsLoadProfileChannelsEqual(LoadProfileChannelsInfo, LoadProfileChannelsInfo_t))
                //                    {
                //                        uint? GroupId = 0;
                //                        Update_LoadProfileChannelsInfo(LoadProfileInformation.CaptureObjectsInfo, LoadProfileChannelsInfo_t, out GroupId);
                //                        LP_Counters.DB_Counter = 0;
                //                        LP_Counters.GroupId = (uint)GroupId;
                //                        // Update Load Profile Channel Info
                //                        LoadProfileChannelsInfo = LoadProfileChannelsInfo_t;
                //                    }
                //                    return LoadProfileChannelsInfo;
                //                }
            }
            catch (Exception ex)
            {
#if Enable_DEBUG_RunMode
                throw new Exception("Error occurred while Getting Load Profile Channels Info_Get_LoadProfileChannelsInfo", ex);
#endif
#if !Enable_DEBUG_RunMode
                throw ex;
#endif
            }
            return LoadProfileChannelsInfo;
        }

        #endregion

        #region Set Load Profile Methods

        public Data_Access_Result Set_LoadProfileChannels(long LPChannelsGroupId, LoadProfileScheme lpScheme)
        {
            List<LoadProfileChannelInfo> LoadProfileChannelsList = null;
            try
            {
                LoadProfileChannelsList = Get_LoadProfileInfoByChannelGroup(LPChannelsGroupId, lpScheme, true);
                return Set_LoadProfileChannels(LoadProfileChannelsList, lpScheme);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Data_Access_Result Set_LoadProfileChannels(List<LoadProfileChannelInfo> LoadProfileChannelsList, LoadProfileScheme lpScheme)
        {
            // To Store Load_Profile Last State On Error
            Class_7 Load_Profile = null;
            List<CaptureObject> _CaptureList = null;
            Base_Class[] captureList = null;
            bool LoadProfileInitialize = false;
            Data_Access_Result Result = Data_Access_Result.Scope_of_Access_Violated;
            try
            {
                // Verify List Of Channels Info Correct
                #region Load Dynamic Load Profile Channels
                List<Base_Class> SelectedChannels = new List<Base_Class>();
                foreach (var item in LoadProfileChannelsList)
                {
                    Base_Class Obj = GetSAPEntry(item.OBIS_Index);
                    Obj.DecodingAttribute = item.SelectedAttribute;
                    SelectedChannels.Add(Obj);
                }
                #endregion
                #region Set Static Load Profile Channels
                /////Add Meter Clock OBIS Object
                //Base_Class MeterClock = GetSAPEntry(Get_Index.Meter_Clock);
                //MeterClock.DecodingAttribute = 0x02;
                //SelectedChannels.Insert(0, MeterClock);
                /////Add LoadProfile Counter
                //Base_Class Load_Profile_Counter = GetSAPEntry(Get_Index.Load_Profile_Counter);
                //Load_Profile_Counter.DecodingAttribute = 0x02;
                //SelectedChannels.Insert(1, Load_Profile_Counter);
                /////Add LoadProfile Capture Period
                //Base_Class Load_Profile_Capture_Period = GetSAPEntry(Get_Index.Load_Profile_Capture_Period);
                //Load_Profile_Capture_Period.DecodingAttribute = 0x02;
                //SelectedChannels.Insert(2, Load_Profile_Capture_Period);
                #endregion
                #region Save Previous Load Profile Config Info
                Load_Profile = (Class_7)GetSAPEntry(GetLoadProfileIndex(lpScheme));
                LoadProfileInitialize = Load_Profile.IsCaptureObjectListIntialized;
                if (LoadProfileInitialize)
                {
                    _CaptureList = Load_Profile.captureObjectsList;
                    captureList = Load_Profile.BaseClassCaptureObjectsList;
                }
                #endregion
                #region Set Capture Object
                #region SET_LoadProfilePeriod
                //TimeSpan t = new TimeSpan();
                //if (LoadProfileChannelsList.Count > 0)
                //{
                //    t = LoadProfileChannelsList[0].CapturePeriod;
                //}
                //if (t != TimeSpan.MinValue || t != TimeSpan.MaxValue)
                //{
                //    Load_Profile.capturePeriod = ConvertTimeSpan(t);
                //    Load_Profile.EncodingAttribute = 0x04;

                //    Result = SET_Object(Load_Profile);
                //    if (Result != Data_Access_Result.Success)
                //    {
                //        return Result;
                //    }
                //}
                #endregion
                #region SET_Capture Buffer
                Load_Profile.LoadCaptureBuffer(SelectedChannels.ToArray());
                Load_Profile.BaseClassCaptureObjectsList = Load_Profile.InitializeCaptureBuffer(1);
                Load_Profile.EncodingAttribute = 0x03;
                Result = SET_Object(Load_Profile);
                if (Result == Data_Access_Result.Success)
                {
                    if (Load_Profile.IsCaptureObjectListIntialized)
                    {
                        captureList = Load_Profile.BaseClassCaptureObjectsList;
                        _CaptureList = Load_Profile.captureObjectsList;
                        ///Mark Read Update Channel Info
                        updateChannelInfo = true;
                    }
                }
                #endregion
                #endregion
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Restore Previous Load Profile Config Info
                if (Load_Profile != null && LoadProfileInitialize)
                {
                    Load_Profile.BaseClassCaptureObjectsList = captureList;
                    Load_Profile.captureObjectsList = _CaptureList;
                }
            }
        }
        public Data_Access_Result Set_LoadProfileInterval(TimeSpan t_Interval, LoadProfileScheme lpScheme)
        {
            // To Store Load_Profile Last State On Error
            Class_7 Load_Profile = null;
            Data_Access_Result Result = Data_Access_Result.Scope_of_Access_Violated;
            try
            {
                #region SET_LoadProfilePeriod
                Load_Profile = (Class_7)GetSAPEntry(GetLoadProfileIndex(lpScheme));

                if (t_Interval != TimeSpan.MinValue || t_Interval != TimeSpan.MaxValue)
                {
                    Load_Profile.capturePeriod = ConvertTimeSpan(t_Interval);
                    Load_Profile.EncodingAttribute = 0x04;

                    Result = SET_Object(Load_Profile);
                    if (Result != Data_Access_Result.Success)
                    {
                        return Result;
                    }
                }
                #endregion
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Restore Previous Load Profile Configuration Info
                if (Load_Profile != null)
                { }
            }
        }

        #endregion

        #region Compute Access Selector

        protected IAccessSelector ComputeEntryDescripterSelector(uint LP_Counter, uint Current_MAXLP_lCounter, uint blockSize = 1)
        {
            try
            {
                EntryDescripter Selecter = new EntryDescripter();

                Selecter.FromEntry = LP_Counter;
                Selecter.ToEntry = LP_Counter + ((blockSize == 1 || blockSize <= 0) ? 0 : blockSize);
                uint MaxLP_Entries_Local = MaxLP_Entries;

                // Update MaxEntries
                if (LPInfo_Data != null &&
                    LPInfo_Data.MaxEntries > 256)
                {
                    MaxLP_Entries_Local = LPInfo_Data.MaxEntries;
                }
                // When Current_Counter_MAX Exceed  
                if (Current_MAXLP_lCounter > MaxLP_Entries_Local ||
                    Selecter.FromEntry > MaxLP_Entries_Local)
                {
                    Selecter.FromEntry = MaxLP_Entries_Local - (Current_MAXLP_lCounter - LP_Counter);
                    if (Selecter.FromEntry <= 0)
                    {
                        Selecter.FromEntry = 1;
                    }
                    Selecter.ToEntry = Selecter.FromEntry + ((blockSize == 1 || blockSize <= 0) ? 0 : blockSize);
                }

                return Selecter;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to compute Entry Descripter Selector for Load Profile data", ex);
            }
        }
        protected IAccessSelector ComputeAccessSelector(uint Last_LP_Counter, uint Current_LP_lCounter)
        {
            try
            {
                if (LoadProfileInfo == null)
                    throw new Exception("Unable to compute Entry Access Selecter for Load Profile Data,Load Profile Info not updated");
                EntryDescripter Selecter = new EntryDescripter();
                long DeltaLPCount = (Current_LP_lCounter - Last_LP_Counter);
                // Very First Request
                // if (DeltaLPCount == 0 && Last_LP_Counter == 0)
                //     return Selecter;
                // Data Upto date,don't request
                if (DeltaLPCount == 0)
                {
                    return null;
                    // throw new Exception("Unable to compute Entry Access Selecter for Load Profile Data,records are updated");
                }
                LoadProfileInfo.SortMethod = SortMethod.FIFO;
                switch (LoadProfileInfo.SortMethod)
                {
                    case SortMethod.FIFO:
                        {
                            Selecter.ToEntry = EntryDescripter.MaxPossibleValue;
                            long StartIndex = LoadProfileInfo.EntriesInUse - DeltaLPCount;
                            if (StartIndex <= 0)
                            {
                                Selecter.FromEntry = 1;
                            }
                            else
                                Selecter.FromEntry = (uint)StartIndex + 1;
                            break;
                        }
                    case SortMethod.LIFO:
                        {
                            Selecter.FromEntry = 1;
                            long LastIndex = Selecter.FromEntry + DeltaLPCount;
                            if (LastIndex > LoadProfileInfo.EntriesInUse)
                            {
                                Selecter.ToEntry = LoadProfileInfo.EntriesInUse;
                            }
                            else
                                Selecter.ToEntry = (uint)(LastIndex - 1);
                            break;
                        }
                    default:
                        throw new Exception(String.Format("Sort Method is {0} not expected", LoadProfileInfo.SortMethod));
                        break;
                }
                return Selecter;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to compute Entry Access Selector for billing data");
            }

        }

        #endregion

        #region Read/Load/Save_Load_Profile_Data/Channels

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update_LoadProfileChannelsInfo(List<CaptureObject> CaptureBufferObjects, List<LoadProfileChannelInfo> LP_Channel, out uint? ChannelGroupId, LoadProfileScheme lpScheme)
        {
            ChannelGroupId = null;
            uint? ChannelGroupId_T = null;
            StOBISCode OBIS_Index_PowerProfile = Get_Index.Dummy;

            try
            {
                if (LP_Channel == null || LP_Channel.Count <= 0 || CaptureBufferObjects == null || CaptureBufferObjects.Count <= 0)
                    throw new ArgumentException("Invalid Load profile Channel Info Provided");
                if (CurrentConnectionInfo == null || !CurrentConnectionInfo.IsInitialized)
                    throw new ArgumentException("Invalid Current Connection Info");

                try
                {
                    OBIS_Index_PowerProfile = LoadProfileController.GetLoadProfileIndex(lpScheme);
                    // _Configurator.GetMeterGroupByLoadProfileChannels(CurrentConnectionInfo, LP_Channel, out ChannelGroupId_T);
                    // Do Capture Objects Exists For Current LP Group
                    List<CaptureObject> T = _Configurator.GetProfileCaptureObjectList(CurrentConnectionInfo, OBIS_Index_PowerProfile, ChannelGroupId_T);
                }
                catch (Exception)
                {
                    ChannelGroupId_T = null;
                }
                if (ChannelGroupId_T != null)
                {
                    ChannelGroupId = ChannelGroupId_T;
                    return;
                }

                // if (ChannelGroupId_T == null)
                //     throw new Exception("Error occurred while updating load profile channel group");
                #region // Save LoadProfile Capture Buffer Info

                try
                {
                    if (ChannelGroupId_T == ChannelGroupId)
                        return;
                    // Update Channel Group Id
                    ChannelGroupId = ChannelGroupId_T;
                    // Validate Either Capture Objects Already Exists
                    // StOBISCode OBIS_Index_PowerProfile = Get_Index.Load_Profile;
                    List<CaptureObject> T = null;
                    try
                    {
                        T = Configurator.GetProfileCaptureObjectList(CurrentConnectionInfo, OBIS_Index_PowerProfile, ChannelGroupId);
                    }
                    catch (Exception) { T = null; }
                    // Save Capture Objects
                    if (T == null || T.Count <= 0)
                    {
                        Configurator.SaveProfileCaptureObjectList(CurrentConnectionInfo, CaptureBufferObjects,
                            OBIS_Index_PowerProfile, ChannelGroupId_T);
                    }
                }
                catch (Exception ex)
                {
                    String Error_message = String.Format("Error occurred while saving LoadProfile Capture List Info,Error Details {0}", ex.Message);
                    throw new Exception(Error_message, ex);
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving/updating LoadProfileChannel Group", ex);
            }
        }
        public Load_Profile saveToClass(LoadProfileData data, string msn)
        {
            try
            {
                Load_Profile obj = new Load_Profile();
                obj.MSN = msn;
                //obj.reference_no = reference_no;
                obj.channels.Clear();
                obj.DBColumns.Clear();
                for (int i = 0; i < data.ChannelsInfo.Count; i++)
                {
                    obj.channels.Add(data.ChannelsInfo[i].OBIS_Index.ToString());
                    obj.DBColumns.Add(data.ChannelsInfo[i].DbColumnName);
                }

                for (int j = 0; j < data.ChannelsInstances.Count; j++)
                {
                    L_Data l_data = new L_Data();
                    l_data.timeStamp = data.ChannelsInstances[j].DateTimeStamp;
                    l_data.counter = data.ChannelsInstances[j].Counter;
                    l_data.interval = data.ChannelsInstances[j].Interval;
                    l_data.statusWord = data.ChannelsInstances[j].StatusWord;
                    l_data.channelID = j + 1;
                    l_data.value.Clear();
                    int channelIndex = -1;
                    for (int k = 0; k < data.ChannelsInfo.Count; k++)
                    {

                        if(Get_FixedChannels().FindAll(y => y.OBIS_Index == data.ChannelsInfo[k].OBIS_Index).Count == 0)
                        {
                            channelIndex++;
                            l_data.value.Add(data.ChannelsInstances[j].LoadProfileInstance[channelIndex]);
                        }

                    }
                    obj.loadData.Add(l_data);
                }

                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Save/Load LoadProfile Data

        protected LoadProfileData FormatLoadProfileData(List<ILValue[]> RawLoadProfileData, ref uint counter)
        {
            try
            {
                ///Sort Values Based on Load_Profile_Counter
                //RawLoadProfileData.Sort((x, y) => (Convert.ToUInt32(Array.Find(x, (w) => w.OBIS_Index == Get_Index.Load_Profile_Counter).Value).
                //    CompareTo(Convert.ToUInt32(Array.Find(y, (v) => v.OBIS_Index == Get_Index.Load_Profile_Counter).Value))));

                // Format Event Data Object
                Func<Get_Index, StOBISCode> dlg = null;

                if (_AP_Controller != null && _AP_Controller.ApplicationProcessSAPTable != null)
                    dlg = new Func<Get_Index, StOBISCode>(_AP_Controller.GetOBISCode);

                ILoadProfileDataFormatter dtFormat = new LoadProfileDataFormatter() { Configurations = null, OBISLabelLookup = dlg };
                LoadProfileData formatted_LP_Data = new LoadProfileData();
                formatted_LP_Data.ChannelsInfo = new List<LoadProfileChannelInfo>(ChannelInfo);
                formatted_LP_Data.ChannelsInstances = new List<LoadProfileItem>();

                //var Meter_Clock = _AP_Controller.GetOBISCode(Get_Index.Meter_Clock);
                //var Load_Profile_Counter = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Counter);
                //var Load_Profile_Counter_2 = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Counter_2);
                //var Load_Profile_Counter_3 = _AP_Controller.GetOBISCode(Get_Index.PQ_Load_Profile_Counter);
                //var Load_Profile_Capture_Period = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Capture_Period);
                //var Load_Profile_Capture_Period2 = _AP_Controller.GetOBISCode(Get_Index.Load_Profile_Capture_Period_2);
                //var Load_Profile_Capture_Period3 = _AP_Controller.GetOBISCode(Get_Index.PQ_Load_Profile_Capture_Period);

                // FIXED Object_Count
                int fixedObjCount = 0;
                //bool counterObjectAvailable = false;
                if (RawLoadProfileData != null && RawLoadProfileData.Count > 0)
                {
                    List<ILValue> TList = new List<ILValue>(RawLoadProfileData[0]);
                    fixedObjCount = TList.Count<ILValue>((x) => x != null && Get_FixedChannels().FindAll(y => y.OBIS_Index == x.OBIS_Index).Count>0);
                                                          //x.OBIS_Index == Meter_Clock ||
                                                          //x.OBIS_Index == Load_Profile_Counter ||
                                                          //x.OBIS_Index == Load_Profile_Counter_2 ||
                                                          //x.OBIS_Index == Load_Profile_Counter_3 ||
                                                          //x.OBIS_Index == Load_Profile_Capture_Period ||
                                                          //x.OBIS_Index == Load_Profile_Capture_Period2 ||
                                                          //x.OBIS_Index == Load_Profile_Capture_Period3
                                                          //);
                    //counterObjectAvailable = TList.Count<ILValue>((x) => x != null && x.OBIS_Index == Load_Profile_Counter || x.OBIS_Index == Load_Profile_Counter_2) > 0;
                }

                if (formatted_LP_Data != null)
                {
                    formatted_LP_Data.FixedObjectCount = fixedObjCount;
                    //formatted_LP_Data.CounterObjectAvailable = counterObjectAvailable;
                }

                // Make LP Data In Order
                List<ILValue[]> tRawLoadProfileData = new List<ILValue[]>();
                var fixedChannels = Get_FixedChannels();
                switch (LoadProfileInfo.SortMethod)
                {
                    case SortMethod.FIFO:
                        {
                            for (int index = RawLoadProfileData.Count - 1; index >= 0; index--)
                            {
                                tRawLoadProfileData.Clear();
                                tRawLoadProfileData.Add(RawLoadProfileData[index]);
                                LoadProfileData LPData = dtFormat.MakeData(tRawLoadProfileData, this.ChannelInfo, fixedChannels, ref counter);
                                formatted_LP_Data.Add(LPData);
                            }
                            break;
                        }
                    case SortMethod.LIFO:
                        {
                            for (int index = 0; index < RawLoadProfileData.Count; index++)
                            {
                                tRawLoadProfileData.Clear();
                                tRawLoadProfileData.Add(RawLoadProfileData[index]);
                                LoadProfileData LPData = dtFormat.MakeData(tRawLoadProfileData, this.ChannelInfo, fixedChannels, ref counter);
                                formatted_LP_Data.Add(LPData);

                            }
                            break;
                        }
                    default:
                        throw new Exception(String.Format("Sort Method {0} is not expected", LoadProfileInfo.SortMethod));

                }
                return formatted_LP_Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while formatting raw load profile data", ex);
            }
        }

        #endregion

        protected uint MaxInternalLoadProfileCounter(List<ILValue[]> RawLoadProfileData)
        {
            try
            {
                uint Count = 0;
                foreach (var LPInstance in RawLoadProfileData)
                {
                    ILValue Internal_LP_Counter = Array.Find<ILValue>(LPInstance, (x) => x.OBIS_Index == Get_Index.Load_Profile_Counter);
                    if (Internal_LP_Counter != null)
                    {
                        uint t = Convert.ToUInt32(Internal_LP_Counter.Value);
                        if (Count < t)
                            Count = t;
                    }
                }
                return Count;

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to calculate max Internal Load Profile Counter");
            }
        }

        protected void SortAlignLoadProfile_Counter(List<ILValue[]> RawLoadProfileData)
        {
            try
            {
                // Remove Duplicate Entries with Load Profie Counter
                for (int index = 0; index < RawLoadProfileData.Count; index++)
                {
                    ILValue LPCounter = Array.Find(RawLoadProfileData[index], (w) => w.OBIS_Index == Get_Index.Load_Profile_Counter);
                    if (LPCounter != null)
                    {
                        for (int indexInner = 0; indexInner < RawLoadProfileData.Count; indexInner++)
                        {
                            ILValue LPOther = Array.Find(RawLoadProfileData[indexInner], (w) => w.OBIS_Index == Get_Index.Load_Profile_Counter);
                            if (LPOther != null && LPCounter != LPOther && LPCounter.Value.Equals(LPOther.Value))
                                RawLoadProfileData.RemoveAt(indexInner);
                        }
                    }
                }
                if (RawLoadProfileData.Count > 0)
                {
                    // Sort Values Based on Load_Profile_Counter
                    RawLoadProfileData.Sort((x, y) => (Convert.ToUInt32(Array.Find(x, (w) => w.OBIS_Index == Get_Index.Load_Profile_Counter).Value).
                        CompareTo(Convert.ToUInt32(Array.Find(y, (v) => v.OBIS_Index == Get_Index.Load_Profile_Counter).Value))));

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Sorting & Aligning the Load Profile Internal Counter");
            }
        }



        #endregion

        #region Support Function

        public bool IsLoadProfileChannelsEqual(List<LoadProfileChannelInfo> LP_Channel, List<LoadProfileChannelInfo> LP_ChannelOther)
        {
            try
            {
                if (LP_Channel.Count > 0 && LP_Channel.Count == LP_ChannelOther.Count)
                {
                    for (int index = 0; index < LP_Channel.Count; index++)
                    {
                        if (LP_Channel[index].CompareTo(LP_ChannelOther[index]) != 0)
                            return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ulong ConvertTimeSpan(TimeSpan TimeValue)
        {
            return (ulong)(TimeValue.Ticks / Math.Pow(10, 7));
        }
        public TimeSpan ConvertTimeSpan(ulong CapturePeriod)
        {
            return new TimeSpan((long)(CapturePeriod * Math.Pow(10, 7)));
        }
        public EntryDescripter MakeEntry(long CurrentCounter, long OldCounter, uint MaxChunkSize = 4632, uint Max_LoadProfile_Count_Limit = 4632)
        {
            long Difference = CurrentCounter - OldCounter;
            Difference = Math.Abs(Difference);
            EntryDescripter event_entry = new EntryDescripter();

            if (Difference > Max_LoadProfile_Count_Limit && CurrentCounter > Max_LoadProfile_Count_Limit)
            {
                event_entry.FromEntry = 1;
                event_entry.ToEntry = (uint)MaxChunkSize;
            }
            else if (Difference <= Max_LoadProfile_Count_Limit && CurrentCounter > Max_LoadProfile_Count_Limit)
            {
                event_entry.FromEntry = (uint)(Max_LoadProfile_Count_Limit - Difference);
                event_entry.ToEntry = ((Difference < MaxChunkSize) ? (uint)Max_LoadProfile_Count_Limit : event_entry.FromEntry + MaxChunkSize);
                event_entry.FromEntry++;
            }
            else if (CurrentCounter > OldCounter && CurrentCounter <= Max_LoadProfile_Count_Limit)
            {
                event_entry.FromEntry = Convert.ToUInt32(OldCounter);
                event_entry.ToEntry = (uint)((Difference < MaxChunkSize) ? CurrentCounter : event_entry.FromEntry + MaxChunkSize);
                event_entry.FromEntry++;
            }
            if (event_entry.FromEntry == 1 && event_entry.ToEntry == 0)
            {
                throw new DLMSException("LoadProfileController->MakeEntry->From entry=1 AND ToEntry=0");
            }
            else
            {
                return event_entry;
            }
        }

        #endregion

    }
}
