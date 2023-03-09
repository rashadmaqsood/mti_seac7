using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharedCode.Comm.DataContainer;
using DatabaseConfiguration.DataSet;
using SharedCode.Comm.HelperClasses;
using Serenity.Crypto;
using System.Collections;
using SharedCode.Comm.Param;
using SharedCode.Common;
using System.Net;
using SharedCode.eGeniousDisplayUnit;

namespace SharedCode.Controllers
{
    public class ParameterController
    {
        #region Data_Members

        public event Action<Status> ParamGetStatus = delegate { };              // Used in QC
        public event Action<Status> ParamSetStatus = delegate { };              // Both QC and SEAC
        public event Action<StatusCollection> ParamStatusRange = delegate { };  // Both QC and SEAC
        public event Action<String> ParameterSetStatus = delegate { };          // SEAC
        public event Action<String> ParameterGetStatus = delegate { };          // SEAC
        public event Action<Param_St_EEPRawRead> EEPRomReadStatus = delegate { };

        private ConnectionInfo connInfo;
        private ApplicationProcess_Controller _AP_Controller;
        private CommandStatusHelper parametersSETStatus;
        private CommandStatusHelper parametersGETStatus;
        private DisplayWindowsHelper DisplayWindows;
        private Configs configurations;
        #endregion

        #region Properties


        public Configs Configurations
        {
            get { return configurations; }
            set { configurations = value; }
        }

        public DisplayWindowsHelper DisplayWindowsHelper_Obj
        {
            get { return DisplayWindows; }
            set { DisplayWindows = value; }
        }
        public ConnectionInfo CurrentConnectionInfo
        {
            get { return connInfo; }
            set { connInfo = value; }
        }
        public ApplicationProcess_Controller AP_Controller
        {
            get { return _AP_Controller; }
            set { _AP_Controller = value; }
        }
        public CommandStatusHelper ParametersGETStatus
        {
            get { return parametersGETStatus; }
            set { parametersGETStatus = value; }
        }

        public CommandStatusHelper ParametersSETStatus
        {
            get { return parametersSETStatus; }
            set { parametersSETStatus = value; }
        }
        private const int MaxParamSETStatus = 1000;
        private const int MaxParamGETStatus = 1000;

        #endregion

        #region Constructur

        public ParameterController()
        {
            if (!Commons.IsMDC)
            {
                ParametersSETStatus = new CommandStatusHelper();
                ParametersGETStatus = new CommandStatusHelper();

                //if (Commons.IsQc)
                //    ParamGetStatus  += new Action<Status>(ParameterController_ParamGETStatus);

                ParamSetStatus += new Action<Status>(ParameterController_ParamSetStatus);
                ParamStatusRange += new Action<StatusCollection>(ParameterController_ParamGETStatusRange);
                ///Instantiate Variables
                DisplayWindows = new DisplayWindowsHelper();
            }
        }


        #endregion

        private void ParameterController_ParamSetStatus(Status obj)
        {
            try
            {
                //if (obj != null)
                //{
                //    if (Commons.IsQc)
                //    {
                //        ParamSetStatus.Invoke(obj);
                //    }
                //    else //if (Commons.IsSeac)
                //    {
                //        String Msg = String.Format("{0}", obj.GetParameterSetStatus());
                //        ParameterSetStatus.Invoke(Msg);
                //    }
                //}


                if (obj != null && !Commons.IsQc)
                {
                    //String Msg = String.Format("{0}..{1}", this.ParametersSETStatus.Current.CategoryName, obj.GetParameterSetStatus());
                    String Msg = String.Format("{0}", obj.GetParameterSetStatus()); //By Azeem
                    ParameterSetStatus.Invoke(Msg);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ParameterController_ParamGETStatus(Status obj)
        {
            try
            {
                if (obj != null)
                {
                    if (Commons.IsQc)
                    {
                        ParamGetStatus.Invoke(obj);
                    }
                    else if (Commons.IsSeac)
                    {
                        String Msg = String.Format("{0} {1}", this.ParametersGETStatus.Current.CategoryName, obj.GetParameterGETStatus());
                        ParameterGetStatus.Invoke(Msg);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ParameterController_ParamGETStatusRange(StatusCollection objList)
        {
            try
            {
                if (objList != null)
                {
                    String Status = "";
                    String Msg = "";

                    //Commented by Azeem Inayat
                    //foreach (var item in objList.Param_Category)
                    //{
                    //    Status += String.Format("{0}\r\n", item.GetParameterGETStatus());
                    //}
                    // Msg = String.Format("Reading Parameters {0}\r\n{1}", objList.CategoryName, Status);


                    ////Implemented by Azeem Inayat //v10.0.2 for Accurate
                    if (Commons.IsSeac)
                    {
                        if (objList.Param_Category.Count > 1)
                        {
                            Status = "";
                            Msg = String.Format("{0}", objList.Param_Category[objList.Param_Category.Count - 1].GetParameterGETStatus());
                        }
                        else
                        {
                            Status = String.Format("{0}", objList.Param_Category[0].GetParameterGETStatus());
                            Msg = String.Format("\r\nReading Parameters {0}\r\n{1}", objList.CategoryName, Status);
                        }

                        ParameterGetStatus.Invoke(Msg);
                    }
                    else if (Commons.IsQc)
                    {
                        ParamGetStatus.Invoke(objList.Param_Category[objList.Param_Category.Count - 1]);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region Member Methods

        #region Timer Reset Single Phase
        public void GET_TimerReset_Parameters_SP(ref TimerReset_SinglePhase TimerReset_Params)
        {
            try
            {
                ///Read Min_Interval_Bw_Resets
                Class_1 TimerReset_Param = (Class_1)GetSAPEntry(Get_Index.Timer_Reset);
                TimerReset_Param.Value_Obj = TimerReset_Params;
                Get_Param(TimerReset_Param);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Timer_Reset_Parameters", ex);
            }
        }

        public Data_Access_Result SET_TimerReset_SP(TimerReset_SinglePhase timerReset)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 TimerReset_Param = (Class_1)GetSAPEntry(Get_Index.Timer_Reset);
                TimerReset_Param.Value_Obj = timerReset;
                result = SET_Param(TimerReset_Param);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Timer Reset Parameters", ex);
            }
        }
        #endregion

        #region GetModemStatusInfo

        public Param_ModemStatus GetModemStatus()
        {
            byte[] byte_Array = new byte[68];
            Param_ModemStatus status = new Param_ModemStatus();
            byte_Array = GETArray_Any(Get_Index.MODEM_STATUS, 0);
            int offset = 0;
            status.Decode_Data(byte_Array, ref offset, byte_Array.Length);
            return status;
        }

        #endregion

        #region Monitored Value For MDI Exceed
        public Data_Access_Result Set_MDI_ToMonitor(StOBISCode MonitoredValue)
        {
            try
            {
                Base_Class Param_MonitoredValue = GetSAPEntry(Get_Index.Limits_Demand_Over_Load_T1);

                // Param Limit Demand Over Load
                // FIX Monitering Time Object
                (Param_MonitoredValue as Class_21).MonitoredValue =
                              new MonitoredValue()
                              {
                                  LogicalName = MonitoredValue,
                                  AttributeIndex = 0x03,
                                  TargetDataType = DataTypes._A06_double_long_unsigned
                              };
                Param_MonitoredValue.EncodingAttribute = 3;


                return SET_Param(Param_MonitoredValue);

            }
            catch (Exception ex)
            {
                throw new Exception("Error Setting MDI to Monitor", ex);
            }
        }

        public StOBISCode GET_MDI_ToMonitor()
        {
            try
            {
                Base_Class Param_limit_demandOverload_CommObj = GetSAPEntry(Get_Index.Limits_Demand_Over_Load_T1);

                Param_limit_demandOverload_CommObj.DecodingAttribute = 3;
                Get_Param(Param_limit_demandOverload_CommObj);

                return ((Class_21)Param_limit_demandOverload_CommObj).MonitoredValue.LogicalName;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param MDI to Monitor", ex);
            }
        }

        #endregion

        public bool GET_Relay_Status()
        {
            try
            {
                Base_Class Contactor_Parameters_Commobj = GetSAPEntry(Get_Index.ContactorFlags_70);
                Contactor_Parameters_Commobj.DecodingAttribute = 0x02;
                Get_Param(Contactor_Parameters_Commobj);

                if (Contactor_Parameters_Commobj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    Class_70 obj = (Class_70)(Contactor_Parameters_Commobj);
                    return obj.Output_state;
                }
                else
                {
                    throw new Exception("Error occurred while getting Contactor state");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Relay state", ex);
            }
        }

        public Action_Result RelayConnectRequest()
        {
            try
            {
                Base_Class Contactor_Parameters_Commobj = GetSAPEntry(Get_Index.ContactorFlags_70);
                ((Class_70)Contactor_Parameters_Commobj).MethodInvokeId = Class_70.REMOTE_RECONNECT;
                return Invoke_Param(Contactor_Parameters_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while connecting relay", ex);
            }
        }

        public Action_Result RelayDisConnectRequest()
        {
            try
            {
                Base_Class Contactor_Parameters_Commobj = GetSAPEntry(Get_Index.ContactorFlags_70);
                ((Class_70)Contactor_Parameters_Commobj).MethodInvokeId = Class_70.REMOTE_DISCONNECT;
                return Invoke_Param(Contactor_Parameters_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        public Base_Class GetSAPEntry(Get_Index ObjIdentifier)
        {
            StOBISCode OBIS_Code = ObjIdentifier;
            return AP_Controller.GetSAPEntry(OBIS_Code);
        }

        public Base_Class GetSAPEntry(StOBISCode ObjIdentifier)
        {
            return AP_Controller.GetSAPEntry(ObjIdentifier);
        }

        public Data_Access_Result SET_Param(Base_Class arg)
        {
            Status SETCommandStatus = new Status();
            try
            {
                if (!Commons.IsMDC)
                {
                    SETCommandStatus.OBIS_Code = arg.INDEX;
                    SETCommandStatus.AttributeNo = arg.EncodingAttribute;
                    SETCommandStatus.AttributeLabel = Common.Class_ID_Structures.GetAttribute_Name(arg.Class_ID, arg.EncodingAttribute);
                    SETCommandStatus.MethodLabel = "Unknown";
                }
                SETCommandStatus.SETCommStatus = AP_Controller.SET(arg);
                return (Data_Access_Result)SETCommandStatus.SETCommStatus;
            }
            catch (DLMSEncodingException ex)    ///Error Type 1 (Encoding Type Errors)
            {
                if (!Commons.IsMDC)
                {
                    SETCommandStatus.SETCommStatus = Data_Access_Result.Read_Write_Denied;
                    return (Data_Access_Result)SETCommandStatus.SETCommStatus;
                }
                else
                    throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                if (!Commons.IsMDC)
                {
                    SETCommandStatus.SETCommStatus = Data_Access_Result.Read_Write_Denied;
                    return (Data_Access_Result)SETCommandStatus.SETCommStatus;
                }
                else
                    throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                if (!Commons.IsMDC)
                {
                    SETCommandStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                }

                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                if (!Commons.IsMDC)
                {
                    SETCommandStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                }
                throw ex;
            }
            finally
            {
                if (!Commons.IsMDC)
                {
                    ParametersSETStatus.Current.AddCommandStatus(SETCommandStatus);
                    ///Invoke Parameter Set Status Event
                    ParamSetStatus.Invoke(SETCommandStatus);
                }
            }
        }
        public Data_Access_Result SET_MeterClock_Date_Time(Param_Clock_Caliberation MeterClockParam)
        {
            try
            {
                Base_Class Clock_Calib_CommObj = MeterClockParam.Encode_Date_Time(GetSAPEntry);
                return SET_Param(Clock_Calib_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Date_Time", ex);
            }
        }

        public void GET_MeterClock_Date_Time(ref Param_Clock_Caliberation MeterClockParam)
        {
            try
            {
                ///Get RTC Time 0x02
                Base_Class MeterClock_CommObj = Get_Param(Get_Index.Meter_Clock, 0x02);
                MeterClockParam.Decode_Date_Time(MeterClock_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting RTC Date Time", ex);
            }
        }

        public Action_Result Invoke_Param(Base_Class arg)
        {
            //Status SETCommandStatus = new Status();
            try
            {
                Action_Result Result = (DLMS.Action_Result)AP_Controller.Method_Invoke(arg);
                //SETCommandStatus.OBIS_Code = arg.INDEX;
                //SETCommandStatus.MethodNo = arg.MethodInvokeId;
                //SETCommandStatus.AttributeLabel = "UnKnown";
                //SETCommandStatus.MethodLabel = SmartDebugUtility.Common.Class_ID_Structures.GetMethod_Name(arg.Class_ID, arg.MethodInvokeId);

                //SETCommandStatus.ActionCommStatus = Result;
                return Result;
            }
            catch (DLMSEncodingException ex)    ///Error Type 1 (Encoding Type Errors)
            {
                //SETCommandStatus.ActionCommStatus = Action_Result.Read_write_denied;
                //SETCommandStatus.SETCommStatus = Data_Access_Result.Read_Write_Denied;
                //return (Action_Result)SETCommandStatus.ActionCommStatus;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                //SETCommandStatus.ActionCommStatus = Action_Result.other_reason;
                //SETCommandStatus.SETCommStatus = Data_Access_Result.Read_Write_Denied;
                ///return (Action_Result)SETCommandStatus.ActionCommStatus;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                //SETCommandStatus.ActionCommStatus = Action_Result.other_reason;
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                //SETCommandStatus.ActionCommStatus = Action_Result.other_reason;
                throw ex;
            }
            finally
            {

            }
        }

        public Base_Class Get_Param(Get_Index ObjIdentifier)
        {
            try
            {
                return Get_Param(ObjIdentifier, 0);
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
        }

        public Base_Class Get_Param(Get_Index ObjIdentifier, byte attribArg)
        {
            try
            {
                Base_Class arg = GetSAPEntry(ObjIdentifier);
                arg.DecodingAttribute = attribArg;
                return Get_Param(arg);
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
            finally
            {
            }
        }

        public Base_Class Get_Param(Base_Class ObjIdentifier)
        {
            Base_Class paramReceived = null;
            List<Status> GETCommSubStatus = new List<Status>();
            Status GETCommandStatus = new Status();
            try
            {
                ///GETCommandStatus.GETCommStatus = paramReceived.GetAttributeDecodingResult(attribArg);
                if (!Commons.IsMDC)
                {
                    #region ///To Mark Data Received Decoding Results
                    if (ObjIdentifier.DecodingAttribute != 0x00)
                    {
                        GETCommandStatus.OBIS_Code = ObjIdentifier.INDEX;
                        GETCommandStatus.AttributeNo = ObjIdentifier.DecodingAttribute;
                        GETCommandStatus.AttributeLabel = Common.Class_ID_Structures.GetAttribute_Name(ObjIdentifier.Class_ID, ObjIdentifier.DecodingAttribute);
                        GETCommandStatus.GETCommStatus = DecodingResult.DataNotPresent;
                        GETCommSubStatus.Add(GETCommandStatus);
                    }
                    else
                    {
                        for (byte attribNo = 1; attribNo <= ObjIdentifier.Attribs_No; attribNo++)
                        {
                            GETCommandStatus = new Status();
                            GETCommandStatus.OBIS_Code = ObjIdentifier.INDEX;
                            GETCommandStatus.AttributeNo = attribNo;
                            GETCommandStatus.AttributeLabel = Common.Class_ID_Structures.GetAttribute_Name(ObjIdentifier.Class_ID, attribNo);
                            GETCommandStatus.GETCommStatus = DecodingResult.DataNotPresent;
                            GETCommSubStatus.Add(GETCommandStatus);
                        }
                    }
                    #endregion 
                }
                ///Request Data Read
                paramReceived = ObjIdentifier;
                paramReceived = AP_Controller.GET(ObjIdentifier);
                ///Record Attribute Data Access Results
                return paramReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                if (!Commons.IsMDC)
                {
                    paramReceived = ObjIdentifier;
                    return paramReceived;
                }
                else
                    throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                if (!Commons.IsMDC)
                {
                    paramReceived = ObjIdentifier;
                    return paramReceived;
                }
                else
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
                if (!Commons.IsMDC)
                {
                    if (ObjIdentifier.DecodingAttribute != 0x00)
                    {
                        GETCommandStatus = GETCommSubStatus[0];
                        GETCommandStatus.GETCommStatus = paramReceived.GetAttributeDecodingResult(ObjIdentifier.DecodingAttribute);
                    }
                    else
                    {
                        for (int attribNo = 1, indexer = 0; attribNo <= ObjIdentifier.Attribs_No; attribNo++, indexer++)
                        {
                            GETCommandStatus = GETCommSubStatus[indexer];
                            GETCommandStatus.GETCommStatus = paramReceived.GetAttributeDecodingResult(attribNo);
                        }
                    }
                    ParametersGETStatus.Current.AddCommandStatusRange(GETCommSubStatus);
                    //ParamStatus.Invoke(GETCommSubStatus[GETCommSubStatus.Count - 1]);
                    ParamStatusRange.Invoke(this.ParametersGETStatus.Current);
                }
            }
        }

        public Base_Class Get_ParamAsync(Base_Class ObjIdentifier)
        {
            Base_Class paramReceived = null;
            //List<Status> GETCommSubStatus = new List<Status>();
            //Status GETCommandStatus = new Status();
            try
            {
                ///GETCommandStatus.GETCommStatus = paramReceived.GetAttributeDecodingResult(attribArg);
                #region ///To Mark Data Received Decoding Results
                //if (ObjIdentifier.DecodingAttribute != 0x00)
                //{
                //    GETCommandStatus.OBIS_Code = ObjIdentifier.INDEX;
                //    GETCommandStatus.AttributeNo = ObjIdentifier.DecodingAttribute;
                //    GETCommandStatus.AttributeLabel = SmartDebugUtility.Common.Class_ID_Structures.GetAttribute_Name(ObjIdentifier.Class_ID, ObjIdentifier.DecodingAttribute);
                //    GETCommandStatus.GETCommStatus = DecodingResult.DataNotPresent;
                //    GETCommSubStatus.Add(GETCommandStatus);
                //}
                //else
                //{
                //    for (byte attribNo = 1; attribNo <= ObjIdentifier.Attribs_No; attribNo++)
                //    {
                //        GETCommandStatus = new Status();
                //        GETCommandStatus.OBIS_Code = ObjIdentifier.INDEX;
                //        GETCommandStatus.AttributeNo = attribNo;
                //        GETCommandStatus.AttributeLabel = SmartDebugUtility.Common.Class_ID_Structures.GetAttribute_Name(ObjIdentifier.Class_ID, attribNo);
                //        GETCommandStatus.GETCommStatus = DecodingResult.DataNotPresent;
                //        GETCommSubStatus.Add(GETCommandStatus);
                //    }
                //}
                #endregion
                ///Request Data Read
                AP_Controller.GET(ObjIdentifier);
                ///Record Attribute Data Access Results
                paramReceived = ObjIdentifier;
                return paramReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                paramReceived = ObjIdentifier;
                ///return paramReceived;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                paramReceived = ObjIdentifier;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                paramReceived = ObjIdentifier;
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                paramReceived = ObjIdentifier;
                throw ex;
            }
            finally
            {
                #region Mark GETCommandStatus

                //if (ObjIdentifier.DecodingAttribute != 0x00)
                //{
                //    GETCommandStatus = GETCommSubStatus[0];
                //    GETCommandStatus.GETCommStatus = paramReceived.GetAttributeDecodingResult(ObjIdentifier.DecodingAttribute);
                //}
                //else
                //{
                //    for (int attribNo = 1, indexer = 0; attribNo <= ObjIdentifier.Attribs_No; attribNo++, indexer++)
                //    {
                //        GETCommandStatus = GETCommSubStatus[indexer];
                //        GETCommandStatus.GETCommStatus = paramReceived.GetAttributeDecodingResult(attribNo);
                //    }
                //} 

                #endregion
            }
        }
        #endregion

        #region Remote Grid Status
        public void GET_RemoteGridInputsStatus(ref List<GridStatusItem> ObjGridStatus)
        {
            try
            {
                if (ObjGridStatus == null) ObjGridStatus = new List<GridStatusItem>();
                if (ObjGridStatus.Count == 0) ObjGridStatus.Add(new GridStatusItem());
                Class_1 GridStatus_CommObj = (Class_1)GetSAPEntry(Get_Index.Grid_Input_Status);
                GridStatus_CommObj.DecodingAttribute = 2;
                GridStatus_CommObj.Value_Array = ObjGridStatus.ToArray();
                Get_Param(GridStatus_CommObj);

                for (int i = 0; i < GridStatus_CommObj.Value_Array.Length; i++)
                {
                    GridStatusItem inputStatusItem = (GridStatusItem)GridStatus_CommObj.Value_Array.GetValue(i);
                    if (ObjGridStatus.Count < (i + 1)) ObjGridStatus.Add(inputStatusItem);
                    else
                    {
                        ObjGridStatus[i].Status = inputStatusItem.Status;
                        ObjGridStatus[i].Time = inputStatusItem.Time;
                    }
                }
                ObjGridStatus.RemoveRange(GridStatus_CommObj.Value_Array.Length, ObjGridStatus.Count - GridStatus_CommObj.Value_Array.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param_GridInputStatus", ex);
            }

        }

        #endregion

        #region MajorAlarmProfile
        public Data_Access_Result SET_MajorAlarmProfile_Filter(Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Base_Class MajorAlarmProfile_obj = GetSAPEntry(Get_Index.MajorAlarmFilter);
                MajorAlarmProfile_obj.EncodingAttribute = 0x02;
                MajorAlarmProfile_obj.EncodingType = DataTypes._A04_bit_string;
                int bitLength = 0;
                byte[] data = ObjMajorAlarmProfile.Encode_EventFilter(ref bitLength);
                ((Class_1)MajorAlarmProfile_obj).Value_Array = data;
                ((Class_1)MajorAlarmProfile_obj).BitLength = bitLength;
                return SET_Param(MajorAlarmProfile_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MajorAlarmProfile_Filter", ex);
            }

        }

        public Data_Access_Result SET_MajorAlarmProfile_Status(Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Base_Class MajorAlarmProfile_obj = GetSAPEntry(Get_Index.AlarmStatus);
                MajorAlarmProfile_obj.EncodingAttribute = 0x02;
                MajorAlarmProfile_obj.EncodingType = DataTypes._A04_bit_string;
                int bitLength = 0;
                byte[] data = ObjMajorAlarmProfile.Encode_AlarmStatus(ref bitLength);
                ((Class_1)MajorAlarmProfile_obj).Value_Array = data;
                ((Class_1)MajorAlarmProfile_obj).BitLength = bitLength;
                return SET_Param(MajorAlarmProfile_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MajorAlarmProfile_Status", ex);
            }

        }

        public Data_Access_Result SET_MajorAlarmProfile_FilterAsync(Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Base_Class MajorAlarmProfile_obj = GetSAPEntry(Get_Index.MajorAlarmFilter);
                MajorAlarmProfile_obj.EncodingAttribute = 0x02;
                MajorAlarmProfile_obj.EncodingType = DataTypes._A04_bit_string;
                int bitLength = 0;
                byte[] data = ObjMajorAlarmProfile.Encode_EventFilter(ref bitLength);
                ((Class_1)MajorAlarmProfile_obj).Value_Array = data;
                ((Class_1)MajorAlarmProfile_obj).BitLength = bitLength;
                return SET_Param(MajorAlarmProfile_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MajorAlarmProfile_Filter", ex);
            }

        }
        public Data_Access_Result SET_MajorAlarmProfile_UserStatus(Param_MajorAlarmProfile ObjMajorAlarmProfile, EnergyMizerAlarmStatus currentStatus)
        {
            try
            {
                StOBISCode obis = Get_Index.AlarmUserStatus;
                if (currentStatus == EnergyMizerAlarmStatus.TeamsDispatched)
                    obis = obis.Set_OBISCode_Feild_E(0x12);
                else if (currentStatus == EnergyMizerAlarmStatus.Processed)
                    obis = obis.Set_OBISCode_Feild_E(0x13);
                Base_Class MajorAlarmProfile_obj = GetSAPEntry(obis);
                MajorAlarmProfile_obj.EncodingAttribute = 0x02;
                MajorAlarmProfile_obj.EncodingType = DataTypes._A04_bit_string;
                int bitLength = 0;
                byte[] data = ObjMajorAlarmProfile.Encode_AlarmUserStatus(ref bitLength, currentStatus);
                ((Class_1)MajorAlarmProfile_obj).Value_Array = data;
                ((Class_1)MajorAlarmProfile_obj).BitLength = bitLength;
                return SET_Param(MajorAlarmProfile_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MajorAlarmProfile_Status", ex);
            }

        }
        public Data_Access_Result SET_MajorAlarmProfile_StatusAsync(Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Base_Class MajorAlarmProfile_obj = GetSAPEntry(Get_Index.AlarmStatus);
                MajorAlarmProfile_obj.EncodingAttribute = 0x02;
                MajorAlarmProfile_obj.EncodingType = DataTypes._A04_bit_string;
                int bitLength = 0;
                byte[] data = ObjMajorAlarmProfile.Encode_AlarmStatus(ref bitLength);
                ((Class_1)MajorAlarmProfile_obj).Value_Array = data;
                ((Class_1)MajorAlarmProfile_obj).BitLength = bitLength;
                return SET_Param(MajorAlarmProfile_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MajorAlarmProfile_Status", ex);
            }

        }

        public void GET_MajorAlarmProfile_Filter(ref Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Class_1 MajorAlarmProfile_obj = (Class_1)GetSAPEntry(Get_Index.MajorAlarmFilter);
                MajorAlarmProfile_obj.DecodingAttribute = 0x02;
                Get_Param(MajorAlarmProfile_obj);
                if (MajorAlarmProfile_obj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    ObjMajorAlarmProfile.Decode_EventFilter((byte[])MajorAlarmProfile_obj.Value_Array, (int)MajorAlarmProfile_obj.BitLength);
                }
                else
                    throw new Exception("Error occurred while reading Major Alarm Profile Fileter");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MajorAlarmProfiles", ex);
            }
        }

        public void GET_MajorAlarmProfile_AlarmStatus(ref Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Class_1 MajorAlarmProfile_obj = (Class_1)GetSAPEntry(Get_Index.AlarmStatus);
                MajorAlarmProfile_obj.DecodingAttribute = 0x02;
                Get_Param(MajorAlarmProfile_obj);
                if (MajorAlarmProfile_obj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    ObjMajorAlarmProfile.Decode_AlarmStatus((byte[])MajorAlarmProfile_obj.Value_Array, (int)MajorAlarmProfile_obj.BitLength);
                }
                else
                    throw new Exception("Error occurred while reading Major Alarm Profile Status");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MajorAlarmProfiles Status", ex);
            }
        }

        public void GET_MajorAlarmProfile_AlarmStatusAsync(Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Class_1 MajorAlarmProfile_obj = (Class_1)GetSAPEntry(Get_Index.AlarmStatus);
                MajorAlarmProfile_obj.DecodingAttribute = 0x02;
                Get_ParamAsync(MajorAlarmProfile_obj);
                if (MajorAlarmProfile_obj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    ObjMajorAlarmProfile.Decode_AlarmStatus((byte[])MajorAlarmProfile_obj.Value_Array, (int)MajorAlarmProfile_obj.BitLength);
                }
                else
                    throw new Exception("Error occurred while reading Major Alarm Profile Status");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MajorAlarmProfiles Status", ex);
            }
        }

        public void GET_MajorAlarmProfile_FilterAsync(Param_MajorAlarmProfile ObjMajorAlarmProfile)
        {
            try
            {
                Class_1 MajorAlarmProfile_obj = (Class_1)GetSAPEntry(Get_Index.MajorAlarmFilter);
                MajorAlarmProfile_obj.DecodingAttribute = 0x02;
                Get_ParamAsync(MajorAlarmProfile_obj);
                if (MajorAlarmProfile_obj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    ObjMajorAlarmProfile.Decode_EventFilter((byte[])MajorAlarmProfile_obj.Value_Array, (int)MajorAlarmProfile_obj.BitLength);
                }
                else
                    throw new Exception("Error occurred while reading Major Alarm Profile Fileter");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MajorAlarmProfiles", ex);
            }
        }

        #endregion

        #region  Communication_Parameters

        /// <summary>
        /// SET_IP_Profiles Parameters
        /// </summary>
        /// <param name="ObjIP_Profiles"></param>
        /// <returns></returns>
        public Data_Access_Result SET_IP_Profiles(Param_IP_Profiles[] ObjIP_Profiles)
        {
            try
            {
                if (ObjIP_Profiles == null || Array.TrueForAll<Param_IP_Profiles>(ObjIP_Profiles, (x) => x == null) || ObjIP_Profiles[0].IP == 0)
                    throw new Exception("Error occurred while setting Param_IP_Profiles,Invalid argument passed");

                ///Write IP Profiles
                Base_Class IP_Profile_CommObj = GetSAPEntry(Get_Index._IP_Profile);
                IP_Profile_CommObj.EncodingAttribute = 2;
                ((Class_1)IP_Profile_CommObj).Value_Array = ObjIP_Profiles;
                return SET_Param(IP_Profile_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_IP_Profiles", ex);
            }
        }

        public void GET_IP_Profiles(ref Param_IP_Profiles[] ObjIP_Profiles)
        {
            #region GET_IP_Profiles
            try
            {
                ///Initialize ObjIP_Profiles if Not Intialized Yet
                if (ObjIP_Profiles == null)
                {
                    ObjIP_Profiles = new Param_IP_Profiles[1];
                    ObjIP_Profiles[0] = new Param_IP_Profiles();  ///Atleast Single Param_IP_Profiles
                }
                if (Array.TrueForAll<Param_Standard_IP_Profile>(ObjIP_Profiles, (x) => x == null))
                {
                    ObjIP_Profiles = new Param_IP_Profiles[1];
                    ObjIP_Profiles[0] = new Param_IP_Profiles();  ///Atleast Single Param_IP_Profiles
                }
                ///Read IP Profiles
                Base_Class IP_Profile_CommObj = GetSAPEntry(Get_Index._IP_Profile);
                IP_Profile_CommObj.DecodingAttribute = 2;
                ((Class_1)IP_Profile_CommObj).Value_Array = ObjIP_Profiles;
                Get_Param(IP_Profile_CommObj);
                if (IP_Profile_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    ObjIP_Profiles = (Param_IP_Profiles[])((Class_1)IP_Profile_CommObj).Value_Array;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param_IP_Profile", ex);
            }
            #endregion
        }

        public void GET_Standard_IP_Profiles(ref Param_Standard_IP_Profile[] ObjIP_Profiles)
        {
            try
            {
                if (ObjIP_Profiles == null)
                {
                    ObjIP_Profiles = new Param_Standard_IP_Profile[1];
                    ObjIP_Profiles[0] = new Param_Standard_IP_Profile();  ///Atleast Single Param_Standard_IP_Profile
                }
                if (Array.TrueForAll<Param_Standard_IP_Profile>(ObjIP_Profiles, (x) => x == null))
                {
                    ObjIP_Profiles = new Param_Standard_IP_Profile[1];
                    ObjIP_Profiles[0] = new Param_Standard_IP_Profile();  ///Atleast Single Param_Standard_IP_Profile
                }

                // Write IP Profiles
                Class_29 IP_Profile_CommObj = (Class_29)GetSAPEntry(Get_Index.AutoConnect);
                IP_Profile_CommObj.DecodingAttribute = 06;

                Get_Param(IP_Profile_CommObj);

                if (IP_Profile_CommObj.Destinations == null ||
                    IP_Profile_CommObj.Destinations.Count <= 0)
                    ObjIP_Profiles = new Param_Standard_IP_Profile[0];
                else if (ObjIP_Profiles.Length != IP_Profile_CommObj.Destinations.Count)
                {
                    ObjIP_Profiles = new Param_Standard_IP_Profile[IP_Profile_CommObj.Destinations.Count];
                }

                for (int indexer = 0; indexer < IP_Profile_CommObj.Destinations.Count &&
                                        indexer < ObjIP_Profiles.Length; indexer++)
                {
                    if (ObjIP_Profiles[indexer] == null)
                        ObjIP_Profiles[indexer] = new Param_Standard_IP_Profile();

                    // Convert Encoded IP & Port
                    byte[] IP_Prof_Entry = IP_Profile_CommObj.Destinations[indexer];

                    var IP_Bytes = new byte[04];
                    var Port_Bytes = new byte[02];

                    Array.Copy(IP_Prof_Entry, IP_Bytes, 04);
                    Array.Copy(IP_Prof_Entry, 04, Port_Bytes, 00, 02);

                    Array.Reverse(IP_Bytes);
                    Array.Reverse(Port_Bytes);

                    // Format Param IP Profile 
                    // var IP_Bytes = BitConverter.GetBytes(ipProf.IP);
                    // var Port_Bytes = BitConverter.GetBytes(ipProf.Wrapper_Over_TCP_port);

                    ObjIP_Profiles[indexer].IP = BitConverter.ToUInt32(IP_Bytes, 0);
                    ObjIP_Profiles[indexer].Wrapper_Over_TCP_port = BitConverter.ToUInt16(Port_Bytes, 0);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading Number_Profiles Allowed Callers", ex);
            }
        }


        public void GET_IP_ProfilesAsync(List<Param_IP_Profiles> ObjIP_ProfilesList)
        {
            #region GET_IP_Profiles
            Param_IP_Profiles[] ObjIP_Profiles = null;
            try
            {
                ObjIP_Profiles = ObjIP_ProfilesList.ToArray<Param_IP_Profiles>();
                ///Initialize ObjIP_Profiles if Not Intialized Yet
                if (ObjIP_Profiles == null)
                {
                    ObjIP_Profiles = new Param_IP_Profiles[1];
                    ObjIP_Profiles[0] = new Param_IP_Profiles();  ///Atleast Single Param_IP_Profiles
                }
                if (Array.TrueForAll<Param_IP_Profiles>(ObjIP_Profiles, (x) => x == null))
                {
                    ObjIP_Profiles = new Param_IP_Profiles[1];
                    ObjIP_Profiles[0] = new Param_IP_Profiles();  ///Atleast Single Param_IP_Profiles
                }
                ///Read IP Profiles
                Base_Class IP_Profile_CommObj = GetSAPEntry(Get_Index._IP_Profile);
                IP_Profile_CommObj.DecodingAttribute = 2;
                ((Class_1)IP_Profile_CommObj).Value_Array = ObjIP_Profiles;
                Get_Param(IP_Profile_CommObj);
                if (IP_Profile_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    ObjIP_Profiles = (Param_IP_Profiles[])((Class_1)IP_Profile_CommObj).Value_Array;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param_IP_Profile", ex);
            }
            finally
            {
                if (ObjIP_ProfilesList != null)
                {
                    ObjIP_ProfilesList.Clear();
                    if (ObjIP_Profiles != null && ObjIP_Profiles.Length > 0)
                        ObjIP_ProfilesList.AddRange(ObjIP_Profiles);
                }
            }
            #endregion
        }

        /// <summary>
        /// SET_Standard_IP_Profiles Parameters
        /// </summary>
        /// <param name="ObjIP_Profiles"></param>
        /// <returns></returns>
        public Data_Access_Result SET_Standard_IP_Profiles(Param_Standard_IP_Profile[] ObjIP_Profiles)
        {
            try
            {
                if (ObjIP_Profiles == null || Array.TrueForAll<Param_Standard_IP_Profile>(ObjIP_Profiles, (x) => x == null))
                    throw new Exception("Error occurred while setting Param_Standard_IP_Profile,Invalid argument passed");
                // Write IP Profiles
                Class_29 IP_Profile_CommObj = (Class_29)GetSAPEntry(Get_Index.AutoConnect);
                IP_Profile_CommObj.EncodingAttribute = 06;

                if (IP_Profile_CommObj.Destinations == null)
                    IP_Profile_CommObj.Destinations = new List<byte[]>();
                else
                    IP_Profile_CommObj.Destinations.Clear();

                // Loading Number Profile Data
                byte[] IP_Prof_Entry = new byte[08];

                foreach (var ipProf in ObjIP_Profiles)
                {
                    IP_Prof_Entry = new byte[06];
                    // Format Param IP Profile 
                    var IP_Bytes = BitConverter.GetBytes(ipProf.IP);
                    Array.Reverse(IP_Bytes);

                    var Port_Bytes = BitConverter.GetBytes(ipProf.Wrapper_Over_TCP_port);
                    Array.Reverse(Port_Bytes);

                    Array.Copy(IP_Bytes, IP_Prof_Entry, 04);
                    Array.Copy(Port_Bytes, 0, IP_Prof_Entry, 04, 02);

                    IP_Profile_CommObj.Destinations.Add(IP_Prof_Entry);
                }

                return SET_Param(IP_Profile_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET Standard IP Profiles", ex);
            }
        }

        public Data_Access_Result SET_AutoConnect_Mode(AutoConnectMode mode)
        {
            try
            {
                // Write IP Profiles {AutoConnect Mode}
                Class_29 IP_Profile_CommObj = (Class_29)GetSAPEntry(Get_Index.AutoConnect);
                IP_Profile_CommObj.EncodingAttribute = 02;
                IP_Profile_CommObj.Mode = mode;

                return SET_Param(IP_Profile_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET AutoConnect Mode", ex);
            }
        }

        public void GET_AutoConnect_Mode(ref AutoConnectMode mode)
        {
            try
            {
                // Initialize IP_Profile_CommObj if Not Intialized Yet
                Class_29 IP_Profile_CommObj = (Class_29)GetSAPEntry(Get_Index.AutoConnect);
                IP_Profile_CommObj.DecodingAttribute = 2;

                Get_Param(IP_Profile_CommObj);
                if (IP_Profile_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    mode = IP_Profile_CommObj.Mode;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading AutoConnect Mode", ex);
            }
        }

        public Data_Access_Result SET_Keep_Alive_IP(Param_Keep_Alive_IP Obj_Keep_Alive_Params)
        {
            try
            {
                if (Obj_Keep_Alive_Params == null)
                    throw new Exception("Error occurred while setting Param_Keep_Alive_IP,Invalid argument passed");
                ///Write IP Profiles
                Base_Class KeepAliveParams_CommObj = GetSAPEntry(Get_Index.KEEPALIVE_PARAMS);
                KeepAliveParams_CommObj.EncodingAttribute = 2;
                KeepAliveParams_CommObj.EncodingType = DataTypes._A02_structure;
                ((Class_1)KeepAliveParams_CommObj).Value_Obj = Obj_Keep_Alive_Params;
                return SET_Param(KeepAliveParams_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Keep_Alive_IP", ex);
            }
        }

        public Data_Access_Result SET_Keep_Alive_IPAsync(Param_Keep_Alive_IP Obj_Keep_Alive_Params)
        {
            try
            {
                if (Obj_Keep_Alive_Params == null)
                    throw new Exception("Error occurred while setting Param_Keep_Alive_IP,Invalid argument passed");
                ///Write IP Profiles
                Base_Class KeepAliveParams_CommObj = GetSAPEntry(Get_Index.KEEPALIVE_PARAMS);
                KeepAliveParams_CommObj.EncodingAttribute = 2;
                KeepAliveParams_CommObj.EncodingType = DataTypes._A02_structure;
                ((Class_1)KeepAliveParams_CommObj).Value_Obj = Obj_Keep_Alive_Params;
                return SET_Param(KeepAliveParams_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting SET_Keep_Alive_IPAsync", ex);
            }
        }

        public void GET_Keep_Alive_IP(ref Param_Keep_Alive_IP Obj_Keep_Alive_Params)
        {
            try
            {
                if (Obj_Keep_Alive_Params == null)
                    throw new Exception("Error occurred while setting Param_Keep_Alive_IP,Invalid argument passed");
                ///Write IP Profiles
                Base_Class KeepAliveParams_CommObj = GetSAPEntry(Get_Index.KEEPALIVE_PARAMS);
                KeepAliveParams_CommObj.DecodingAttribute = 2;
                ((Class_1)KeepAliveParams_CommObj).Value_Obj = Obj_Keep_Alive_Params;
                Get_Param(KeepAliveParams_CommObj);
                if (KeepAliveParams_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    Obj_Keep_Alive_Params = (Param_Keep_Alive_IP)((Class_1)KeepAliveParams_CommObj).Value_Obj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_Keep_Alive_IP", ex);
            }
        }

        public void GET_Keep_Alive_IPAync(Param_Keep_Alive_IP Obj_Keep_Alive_Params)
        {
            try
            {
                if (Obj_Keep_Alive_Params == null)
                    throw new Exception("Error occurred while setting Param_Keep_Alive_IP,Invalid argument passed");
                ///Write IP Profiles
                Base_Class KeepAliveParams_CommObj = GetSAPEntry(Get_Index.KEEPALIVE_PARAMS);
                KeepAliveParams_CommObj.DecodingAttribute = 2;
                ((Class_1)KeepAliveParams_CommObj).Value_Obj = Obj_Keep_Alive_Params;
                Get_Param(KeepAliveParams_CommObj);
                if (KeepAliveParams_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    Obj_Keep_Alive_Params = (Param_Keep_Alive_IP)((Class_1)KeepAliveParams_CommObj).Value_Obj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_Keep_Alive_IP", ex);
            }
        }


        #endregion

        #region Miscelleanous_Getter_Setter

        public double GETDouble_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return DLMS_Common.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
                // return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double GETDouble_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GETArray_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return DataContainer_Class_obj.Decode_Any_ByteArray(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GETArray_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return DLMS_Common.Decode_Any_ByteArray(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public string GETString_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return DataContainer_Class_obj.Decode_Any_string(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public string GETString_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return DLMS_Common.Decode_Any_string(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public void GETValue_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, Get_Index arg, byte attribute)
        {
            bool isSuccess = false;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("DecodeAnyObject");

                Base_Class Comm_Obj = Get_Param(arg, attribute);
                isSuccess = DataContainer_Class_obj.TryDecode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), DataContainer_Class_obj, Data_Property);

                isSuccess = false;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                // throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));

                throw ex;
            }
        }

        public bool TryGETValue_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, Get_Index arg, byte attribute)
        {
            bool isSuccess = false;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("DecodeAnyObject");

                Base_Class Comm_Obj = Get_Param(arg, attribute);
                isSuccess = DataContainer_Class_obj.TryDecode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), DataContainer_Class_obj, Data_Property);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            return isSuccess;
        }


        public double GET_Any(ref Param_IP_Profiles Param_IP_Profiles_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_IP_Profiles_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //____________________________________________________________________________

        public byte[] Decode_Any(Base_Class arg, bool h)
        {
            try
            {

                Class_1 temp_obj = (Class_1)arg;
                return (byte[])(temp_obj.Value_Array);


                //if (Class_ID == 3)
                //{
                //    Class_3 temp_obj = (Class_3)arg;
                //    double temp = Convert.ToDouble(temp_obj.Value);
                //    return temp;
                //}
                //if (Class_ID == 4)
                //{
                //    Class_4 temp_obj = (Class_4)arg;
                //    double temp = Convert.ToDouble(temp_obj.Value);
                //    return temp;
                //}
                //return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public byte[] GET_Any(Get_Index arg, byte attribute, byte Class_ID, bool h)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return this.Decode_Any(Comm_Obj, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        //____________________________________________________________________________

        public Class_4 Decode_Any(Base_Class arg)
        {
            try
            {
                Class_4 temp_obj = (Class_4)arg;
                double temp = Convert.ToDouble(temp_obj.Value);
                if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                    temp = double.NaN;
                else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                    temp = double.PositiveInfinity;
                else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                    temp = double.NegativeInfinity;

                return temp_obj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public double GET_Any(Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return this.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public Class_4 GET_Any(Get_Index arg)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, 0);
                return this.Decode_Any(Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double GET_Any(ref Param_Communication_Profile Param_Communication_Profile_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_Communication_Profile_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public double GET_Any(ref Param_Number_Profile Param_Number_Profile_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_Number_Profile_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public double GET_Any(ref Param_ModemLimitsAndTime Param_ModemLimtisAndTime_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_ModemLimtisAndTime_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public double GET_Any(ref Param_Contactor Param_Contactor_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_Contactor_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public double GET_Any(ref Param_Keep_Alive_IP Param_Keep_Alive_IP_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_Keep_Alive_IP_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public string GET_Any(ref Param_Password Param_password_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_password_obj.Decode_string(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public string GET_Any(ref Param_Customer_Code Param_Customer_Code_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_Customer_Code_obj.Decode_string(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public double GET_Any(ref Param_CTPT_Ratio Param_CTPT_ratio_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_CTPT_ratio_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public Class_42 GET_class_42(ref Param_IPV4 Param_IPV4_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                Class_42 temp = (Class_42)Comm_Obj;
                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public Class_41 GET_class_41(ref Param_TCP_UDP Param_TCP_UDP_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                Class_41 temp = (Class_41)Comm_Obj;
                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public double GET_Any(ref Param_IPV4 Param_IPV4_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_IPV4_obj.Decode_Any(Comm_Obj, Class_ID, attribute);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public string GET_string(ref Param_Number_Profile Param_Number_Profile_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_Number_Profile_obj.Decode_string(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public string GET_string(ref Param_Modem_Initialize Param_Modem_Initialize_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_Modem_Initialize_obj.Decode_string(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public void GET_string(ref Param_Password Param_password_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                Param_password_obj.Decode_Device_Password(Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GET_byte_array(ref Param_IPV4 Param_IPV4_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_IPV4_obj.Decode_byte_array(Comm_Obj, Class_ID, attribute);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }
        public bool GET_Bool(ref Param_IPV4 Param_IPV4_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = Get_Param(arg, attribute);
                return Param_IPV4_obj.Decode_bool(Comm_Obj, Class_ID, attribute);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        #endregion

        #region ModemBasics
        public Data_Access_Result SET_ModemBasics(Param_Modem_Initialize ObjModemInit)
        {
            try
            {
                ///Write GPRS APN
                Base_Class Modem_LimitsAndTime_obj = ObjModemInit.Encode_APN(GetSAPEntry);
                SET_Param(Modem_LimitsAndTime_obj);

                ///Write GPRS PINCode
                Modem_LimitsAndTime_obj = ObjModemInit.Encode_PIN(GetSAPEntry);
                return SET_Param(Modem_LimitsAndTime_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_ModemBasic", ex);
            }
        }

        public Data_Access_Result SET_ModemBasicsAsync(Param_Modem_Initialize ObjModemInit)
        {
            try
            {
                ///Write GPRS APN
                Base_Class Modem_LimitsAndTime_obj = ObjModemInit.Encode_APN(GetSAPEntry);
                SET_Param(Modem_LimitsAndTime_obj);

                ///Write GPRS PINCode
                Modem_LimitsAndTime_obj = ObjModemInit.Encode_PIN(GetSAPEntry);
                Data_Access_Result T = SET_Param(Modem_LimitsAndTime_obj);
                return T;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_ModemBasic", ex);
            }
        }

        public DecodingResult GET_ModemBasics(ref Param_Modem_Initialize ObjModemInit)
        {
            try
            {
                /////Read Modem UserName
                //Base_Class ModemBasics = Get_Param(Get_Index.ModemBasics_User_Name, 2);
                //ObjModemInit.Decode_Username(ModemBasics);
                /////Read Modem Password
                //ModemBasics = Get_Param(Get_Index.ModemBasics_Password, 2);
                //ObjModemInit.Decode_Password(ModemBasics);

                ///Read Modem GPRS Configs
                Base_Class ModemBasics = Get_Param(Get_Index.GPRS_Modem_Configuration, 0);
                ObjModemInit.Decode_ModemGPRSConfigs(ModemBasics);
                if (ModemBasics.AccessResults[1] != DLMS.DecodingResult.Ready)
                {
                    ObjModemInit.APN = "";
                }
                if (ModemBasics.AccessResults[2] != DLMS.DecodingResult.Ready)
                {
                    ObjModemInit.PIN_code = 10000;
                }

                foreach (var item in ModemBasics.AccessResults)
                    if (item != DecodingResult.Ready)
                        return item;
                return DecodingResult.Ready;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_ModemLimitsAndTime", ex);
            }
        }

        public void GET_ModemBasicsAsync(Param_Modem_Initialize ObjModemInit)
        {
            try
            {

                ///Read Modem GPRS Configs
                Base_Class ModemBasics = GetSAPEntry(Get_Index.GPRS_Modem_Configuration);
                ModemBasics.DecodingAttribute = 0;
                Get_Param(ModemBasics);
                ObjModemInit.Decode_ModemGPRSConfigs(ModemBasics);
                if (ModemBasics.AccessResults[1] != DLMS.DecodingResult.Ready)
                {
                    ObjModemInit.APN = "";
                }
                if (ModemBasics.AccessResults[2] != DLMS.DecodingResult.Ready)
                {
                    ObjModemInit.PIN_code = 10000;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_ModemLimitsAndTime", ex);
            }
        }
        #endregion

        #region GetModemStatusInfo
        public void GetModemStatus(ref Param_ModemStatus statusObj)
        {
            Class_1 ObjModemBasicNew_obj = (Class_1)GetSAPEntry(Get_Index.MODEM_STATUS);
            //ObjModemBasicNew_obj.Value_Obj = statusObj;
            Get_Param(ObjModemBasicNew_obj);
            statusObj.Decode_Data((byte[])ObjModemBasicNew_obj.Value_Array);
        }
        #endregion

        #region ModemBasicsNew

        public Data_Access_Result SET_ModemBasicsNew(Param_ModemBasics_NEW ObjModemBasicNew)
        {
            try
            {
                ///Write GPRS Modem Basic New
                Class_1 ObjModemBasicNew_obj = (Class_1)GetSAPEntry(Get_Index.ModemBasics_User_Name);
                ObjModemBasicNew_obj.Value_Obj = ObjModemBasicNew;
                ObjModemBasicNew_obj.DecodingAttribute = 2;
                return SET_Param(ObjModemBasicNew_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_ModemBasicNew", ex);
            }
        }

        public void GET_ModemBasicsNew(ref Param_ModemBasics_NEW ObjModemBasicNew)
        {
            try
            {
                ///Write GPRS Modem Basic New
                Class_1 ObjModemBasicNew_obj = (Class_1)GetSAPEntry(Get_Index.ModemBasics_User_Name);
                ObjModemBasicNew_obj.Value_Obj = ObjModemBasicNew;
                Get_Param(ObjModemBasicNew_obj);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_ModemBasicNew", ex);
            }
        }

        #endregion

        #region TimeBaseEvents

        public Data_Access_Result SET_TimeBaseEvents(Param_TimeBaseEvents ObjTimeBaseEvents, Get_Index arg)
        {
            try
            {
                Base_Class TimeBaseEvents_obj = GetSAPEntry(arg);
                TimeBaseEvents_obj.EncodingAttribute = 0x02;
                TimeBaseEvents_obj.EncodingType = DataTypes._A02_structure;
                ((Class_1)TimeBaseEvents_obj).Value_Obj = ObjTimeBaseEvents;              ///Load ModemLimits_Params
                return SET_Param(TimeBaseEvents_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting TimeBaseEvents", ex);
            }

        }

        public Data_Access_Result SET_TimeBaseEventsAsync(Param_TimeBaseEvents ObjTimeBaseEvents, Get_Index arg)
        {
            try
            {
                Base_Class TimeBaseEvents_obj = GetSAPEntry(arg);
                TimeBaseEvents_obj.EncodingAttribute = 0x02;
                TimeBaseEvents_obj.EncodingType = DataTypes._A02_structure;
                ((Class_1)TimeBaseEvents_obj).Value_Obj = ObjTimeBaseEvents;              ///Load ModemLimits_Params
                return SET_Param(TimeBaseEvents_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting TimeBaseEvents", ex);
            }

        }

        public void GET_TimeBaseEvents(ref Param_TimeBaseEvents ObjTimeBaseEvents, Get_Index arg)
        {
            try
            {
                Base_Class TimeBaseEvents_obj = GetSAPEntry(arg);
                ((Class_1)TimeBaseEvents_obj).Value_Obj = ObjTimeBaseEvents;
                TimeBaseEvents_obj.DecodingAttribute = 0x02;
                Get_Param(TimeBaseEvents_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting TimeBaseEvents", ex);
            }
        }

        public void GET_TimeBaseEventsAsync(Param_TimeBaseEvents ObjTimeBaseEvents, Get_Index arg)
        {
            try
            {
                Base_Class TimeBaseEvents_obj = GetSAPEntry(arg);
                ((Class_1)TimeBaseEvents_obj).Value_Obj = ObjTimeBaseEvents;
                TimeBaseEvents_obj.DecodingAttribute = 0x02;
                Get_ParamAsync(TimeBaseEvents_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting TimeBaseEvents", ex);
            }
        }

        public void GET_TBE_PowerFAil(ref TBE_PowerFail obj_tbe_powerFail)
        {
            try
            {
                Base_Class tbe_powerFail_obj = GetSAPEntry(Get_Index.TBE_PowerFail);
                ((Class_1)tbe_powerFail_obj).Value_Obj = obj_tbe_powerFail;
                tbe_powerFail_obj.DecodingAttribute = 0x02;
                Get_Param(tbe_powerFail_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_ModemLimitsAndTime", ex);
            }
        }
        public Data_Access_Result SET_TBE_PowerFAil(TBE_PowerFail obj_tbe_powerFail)
        {
            try
            {
                Base_Class tbe_powerFail_obj = GetSAPEntry(Get_Index.TBE_PowerFail);
                tbe_powerFail_obj.EncodingAttribute = 0x02;
                tbe_powerFail_obj.EncodingType = DataTypes._A02_structure;
                ((Class_1)tbe_powerFail_obj).Value_Obj = obj_tbe_powerFail;
                return SET_Param(tbe_powerFail_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Disable TBEs on power fail", ex);
            }
        }

        #endregion

        #region DisplayWindows

        public List<DisplayWindowItem> Get_DisplayWindows()
        {
            return this.DisplayWindows.SelectedWindows;
        }

        public void Get_DisplayWindow_Normal(ref DisplayWindows DisplayWinodowNormal)
        {
            try
            {
                Class_1 DisplayWindowNormal_Obj = (Class_1)GetSAPEntry(Get_Index.DisplayWindows_NOR);
                St_DisplayWindows DispWindows = new St_DisplayWindows();
                DisplayWindowNormal_Obj.DecodingAttribute = 0x02;
                DisplayWindowNormal_Obj.Value_Obj = DispWindows;
                Get_Param(DisplayWindowNormal_Obj);
                ///Decoding Display_Windows_Normal Data Received
                if (DisplayWindowNormal_Obj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    DisplayWindowsHelper.InitialzeDisplayWindowsHelper(AP_Controller, configurations, ref DisplayWindows);
                    DisplayWinodowNormal = DisplayWindowsHelper_Obj.Decode_DisplayWindows(DispWindows);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Display Windows Normal", ex);
            }
        }

        public Data_Access_Result Set_DisplayWindow_Normal(DisplayWindows DisplayWindNormalObj)
        {
            try
            {
                DisplayWindowsHelper_Obj = new DisplayWindowsHelper();
                if (DisplayWindNormalObj == null || !DisplayWindNormalObj.IsValid)
                {
                    throw new Exception("Display Windows Normal are not programmed");
                }
                ///Verify Windows Normal Data
                Class_1 DisplayWindowNormal_Obj = (Class_1)GetSAPEntry(Get_Index.DisplayWindows_NOR);
                ///Encoding DisplayWindNormalObj
                DisplayWindowsHelper.InitialzeDisplayWindowsHelper(AP_Controller, configurations, ref DisplayWindows);
                St_DisplayWindows DisplayWindowsNormal = DisplayWindowsHelper_Obj.Encode_DisplayWindows(DisplayWindNormalObj);
                DisplayWindowNormal_Obj.Value_Obj = DisplayWindowsNormal;
                return SET_Param(DisplayWindowNormal_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Display Windows Normal", ex);
            }
        }

        public void Get_DisplayWindow_Alternate(ref DisplayWindows DisplayWinodowAlternate)
        {
            try
            {
                Class_1 DisplayWindowAlternate_Obj = (Class_1)GetSAPEntry(Get_Index.DisplayWIndows_ALT);
                St_DisplayWindows DispWindows = new St_DisplayWindows();
                DisplayWindowAlternate_Obj.DecodingAttribute = 0x02;
                DisplayWindowAlternate_Obj.Value_Obj = DispWindows;
                Get_Param(DisplayWindowAlternate_Obj);
                ///Decoding Display_Windows_Alternate Data Received
                if (DisplayWindowAlternate_Obj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    DisplayWindowsHelper.InitialzeDisplayWindowsHelper(AP_Controller, configurations, ref DisplayWindows);
                    DisplayWinodowAlternate = DisplayWindowsHelper_Obj.Decode_DisplayWindows(DispWindows);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Display Windows Alternate", ex);
            }
            finally
            {
                if (DisplayWinodowAlternate != null)
                    DisplayWinodowAlternate.WindowsMode = DispalyWindowsModes.Alternate;//Azeem
            }
        }

        public Data_Access_Result Set_DisplayWindow_Alternate(DisplayWindows DisplayWindAlternateObj)
        {
            try
            {
                DisplayWindowsHelper_Obj = new DisplayWindowsHelper();
                if (DisplayWindAlternateObj == null || !DisplayWindAlternateObj.IsValid)
                {
                    throw new Exception("Display Windows Alternate are not programmed");
                }
                ///Verify Windows Alternate Data
                Class_1 DisplayWindowAlternatel_Obj = (Class_1)GetSAPEntry(Get_Index.DisplayWIndows_ALT);
                ///Encoding DisplayWindNormalObj
                DisplayWindowsHelper.InitialzeDisplayWindowsHelper(AP_Controller, configurations, ref DisplayWindows);
                St_DisplayWindows DisplayWindowsAlternate = DisplayWindowsHelper_Obj.Encode_DisplayWindows(DisplayWindAlternateObj);
                DisplayWindowAlternatel_Obj.Value_Obj = DisplayWindowsAlternate;
                return SET_Param(DisplayWindowAlternatel_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Display Windows Alternate", ex);
            }
        }

        #endregion

        #region Password

        public Data_Access_Result Set_CurrentAssociationPassword(string password)
        {
            try
            {
                if (String.IsNullOrEmpty(password) || password.Length >= 30)
                    throw new Exception("Invalid password string to set for Current Logical Device");
                Class_15 Management_Comm_Obj = (Class_15)GetSAPEntry(Get_Index.Current_Association);
                Management_Comm_Obj.Password = password;
                Management_Comm_Obj.EncodingAttribute = 0x07;
                return SET_Param(Management_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MTI0R32600000786 Device Password", ex);
            }
        }
        #endregion

        #region Modem

        public void GET_ModemLimitsAndTime(ref Param_ModemLimitsAndTime ObjModemLimits_Params)
        {
            try
            {
                Base_Class Modem_LimitsAndTime_obj = GetSAPEntry(Get_Index.MODEM_LIMITS_AND_TIME);
                ((Class_1)Modem_LimitsAndTime_obj).Value_Obj = ObjModemLimits_Params;              ///Load ModemLimits_Params
                Modem_LimitsAndTime_obj.DecodingAttribute = 0x02;
                Get_Param(Modem_LimitsAndTime_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_ModemLimitsAndTime", ex);
            }
        }
        public bool SET_Modem_Limit_Time(Param_ModemLimitsAndTime obj_modem)
        {
            try
            {
                Base_Class obj_base = GetSAPEntry(Get_Index.MODEM_LIMITS_AND_TIME);
                obj_base.EncodingAttribute = 0x02;
                obj_base.EncodingType = DataTypes._A02_structure;
                ((Class_1)obj_base).Value_Obj = obj_modem;
                var result = SET_Param(obj_base);
                return (result == Data_Access_Result.Success);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Customer Reference

        public bool SET_Customer_Reference_no(string reference_no)
        {
            try
            {
                Base_Class obj_base = GetSAPEntry(Get_Index.Customer_Reference_No);
                obj_base.EncodingAttribute = 0x02;
                obj_base.EncodingType = DataTypes._A09_octet_string;
                if (!string.IsNullOrEmpty(reference_no))
                {
                    ((Class_1)obj_base).Value_Array = ASCIIEncoding.ASCII.GetBytes(reference_no);
                    var result = SET_Param(obj_base);
                    return (result == Data_Access_Result.Success);
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region MDI Parameters

        public void GET_MDI_Parameters(ref Param_MDI_parameters MDI_Params)
        {
            try
            {
                ///Read Min_Interval_Bw_Resets
                Base_Class MDIParam = Get_Param(Get_Index.MDIParams_MinTimeIntervalBetweenResetsIncaseofManualReset, 2);
                MDI_Params.Decode_Minimum_Interval_Manual_Reset(MDIParam);

                //// Read MDI Interval Demand Register
                MDIParam = Get_Param(Get_Index.DEMAND_ALL, 8);
                MDI_Params.Decode_MDI_Interval(MDIParam);

                MDIParam = Get_Param(Get_Index.DEMAND_ALL, 9);
                MDI_Params.Decode_Roll_slide_count(MDIParam);
                // /Read MDI Auto Reset DateTime
                MDIParam = Get_Param(Get_Index.MDIParams_Auto_Reset_Date, 0x04);
                 MDI_Params.Decode_Auto_Reset_Date(MDIParam);

                /// Read MDI Flags
                MDIParam = Get_Param(Get_Index.MDIParams_Flag, 0x02);
                MDI_Params.Decode_MDI_FLAGS(MDIParam);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MDI_Parameters", ex);
            }
        }

        public Data_Access_Result Set_MDI_Date_Time(StDateTime time)
        {
            try
            {
                time.Kind = StDateTime.DateTimeType.DateTime;
                Class_22 obj_mdi_date_time = (Class_22)GetSAPEntry(Get_Index.MDIParams_Auto_Reset_Date);
                obj_mdi_date_time.EncodingAttribute = 0x04;
                if (time != null)   ///Must be zero
                {
                    //this.Auto_reset_date.Second = 0x00;
                    //time.Hundred = 0x00;
                }
                if (obj_mdi_date_time.executionTimeList == null)
                    obj_mdi_date_time.executionTimeList = new List<StDateTime>();
                else if (obj_mdi_date_time.executionTimeList.Count > 0)
                    obj_mdi_date_time.executionTimeList.Clear();
                obj_mdi_date_time.executionTimeList.Add(time);

                return SET_Param(obj_mdi_date_time);
            }
            catch (Exception ex)
            {

                throw new Exception("Error Setting MDI Auto reset date", ex);
            }
        }

        public void GET_MDI_Auto_Reset_Date(ref Param_MDI_parameters MDI_Params)
        {
            try
            {
                // Read Min_Interval_Bw_Resets
                // Base_Class MDIParam = Get_Param(Get_Index.MDIParams_MinTimeIntervalBetweenResetsIncaseofManualReset, 2);
                // MDI_Params.Decode_Minimum_Interval_Manual_Reset(MDIParam);
                // Read MDI Interval Demand Register
                // MDIParam = Get_Param(Get_Index.MDI_Parameters, 8);
                // MDI_Params.Decode_MDI_Interval(MDIParam);
                // MDIParam = Get_Param(Get_Index.MDI_Parameters, 9);
                // MDI_Params.Decode_Roll_slide_count(MDIParam);
                ///Read MDI Auto Reset DateTime
                Base_Class MDIParam = Get_Param(Get_Index.MDIParams_Auto_Reset_Date, 0x04);
                MDI_Params.Decode_Auto_Reset_Date(MDIParam);
                /// Read MDI Flags
                // MDIParam = Get_Param(Get_Index.MDIParams_Flag, 0x02);
                // MDI_Params.Decode_MDI_FLAGS(MDIParam);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MDI_Parameters", ex);
            }
        }

        public void GET_MDI_Interval_Count(ref Param_MDI_parameters MDI_Params)
        {
            try
            {
                Base_Class MDIParam = Get_Param(Get_Index.DEMAND_ACTIVE_IMPORT, 8);
                MDI_Params.Decode_MDI_Interval(MDIParam);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MDI_Parameters", ex);
            }
        }

        public void GET_MDI_Slide_Count(ref Param_MDI_parameters MDI_Params)
        {
            try
            {
                Base_Class MDIParam = Get_Param(Get_Index.DEMAND_ACTIVE_IMPORT, 9);
                MDI_Params.Decode_MDI_Interval(MDIParam);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MDI_Parameters", ex);
            }
        }

        #endregion

        #region Contactor

        public void GET_ContactorParams(Param_ContactorExt ObjContactorParams)
        {
            try
            {
                Base_Class Contactor_Parameters_Commobj = GetSAPEntry(Get_Index.ContactorParameters);
                ((Class_1)Contactor_Parameters_Commobj).Value_Obj = ObjContactorParams;
                Contactor_Parameters_Commobj.DecodingAttribute = 0x02;
                ObjContactorParams.contactor_read_Status = null;
                Get_Param(Contactor_Parameters_Commobj);
                if (Contactor_Parameters_Commobj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    ObjContactorParams = (Param_ContactorExt)((Class_1)Contactor_Parameters_Commobj).Value_Obj;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Contactor Params", ex);
            }
        }
        public bool SET_Contactor_Params(Param_Contactor obj_contactor)
        {
            try
            {
                Base_Class obj_arg = GetSAPEntry(Get_Index.ContactorParameters);
                ((Class_1)obj_arg).Value_Obj = obj_contactor;
                obj_arg.EncodingAttribute = 0x02;
                obj_contactor.contactor_read_Status = null;
                var result = SET_Param(obj_arg);
                return (result == Data_Access_Result.Success);
            }
            catch (Exception ex)
            {

                throw new Exception("Error Setting Contactor Parameters", ex);
            }
        }

        public bool SET_Over_Load_MoniteringTime(Param_Monitoring_Time obj_MT)
        {
            try
            {
                Base_Class obj_arg = obj_MT.Encode_Over_Load(GetSAPEntry);
                var result = SET_Param(obj_arg);
                return (result == Data_Access_Result.Success);
            }
            catch (Exception ex)
            {

                throw new Exception("Error Setting Contactor Parameters OverLoad Monitoring Time", ex);
            }
        }

        public bool SET_Over_Load_Total_All(Param_Limits obj_limit)
        {
            try
            {
                Base_Class obj_arg = obj_limit.Encode_Limits_Over_Load_Total_T1_attrib2(GetSAPEntry);
                var result = SET_Param(obj_arg); if (result != Data_Access_Result.Success) return false;
                obj_arg = obj_limit.Encode_Limits_Over_Load_Total_T2_attrib2(GetSAPEntry);
                result = SET_Param(obj_arg); if (result != Data_Access_Result.Success) return false;
                obj_arg = obj_limit.Encode_Limits_Over_Load_Total_T3_attrib2(GetSAPEntry);
                result = SET_Param(obj_arg); if (result != Data_Access_Result.Success) return false;
                obj_arg = obj_limit.Encode_Limits_Over_Load_Total_T4_attrib2(GetSAPEntry);
                result = SET_Param(obj_arg); if (result != Data_Access_Result.Success) return false;
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Error Setting Contactor Parameters Over Load Limits", ex);
            }
        }
        #endregion

        #region Number Profiles
        public void GET_Number_Profiles_AllowedCallers(ref Param_Standard_Number_Profile[] ObjNumber_Profiles)
        {
            try
            {
                if (ObjNumber_Profiles == null)
                {
                    ObjNumber_Profiles = new Param_Standard_Number_Profile[1];
                    ObjNumber_Profiles[0] = new Param_Standard_Number_Profile();  /// Atleast Single Param_Number_Profiles
                }

                // Write IP Profiles
                Class_28 Number_Profile_CommObj = (Class_28)GetSAPEntry(Get_Index.AutoAnswer);
                Number_Profile_CommObj.DecodingAttribute = 07;

                Get_Param(Number_Profile_CommObj);

                if (Number_Profile_CommObj.Allowed_AutoAnswerCallers == null ||
                    Number_Profile_CommObj.Allowed_AutoAnswerCallers.Count <= 0)
                    ObjNumber_Profiles = new Param_Standard_Number_Profile[0];
                else if (ObjNumber_Profiles.Length != Number_Profile_CommObj.Allowed_AutoAnswerCallers.Count)
                {
                    ObjNumber_Profiles = new Param_Standard_Number_Profile[Number_Profile_CommObj.Allowed_AutoAnswerCallers.Count];
                }

                for (int indexer = 0; indexer < Number_Profile_CommObj.Allowed_AutoAnswerCallers.Count &&
                                        indexer < ObjNumber_Profiles.Length; indexer++)
                {
                    if (ObjNumber_Profiles[indexer] == null)
                        ObjNumber_Profiles[indexer] = new Param_Standard_Number_Profile();

                    ObjNumber_Profiles[indexer].Number = Number_Profile_CommObj.Allowed_AutoAnswerCallers[indexer].Value;
                    ObjNumber_Profiles[indexer].CallerType = Number_Profile_CommObj.Allowed_AutoAnswerCallers[indexer].Key;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading Number_Profiles Allowed Callers", ex);
            }
        }

        public void GET_Number_Profiles(ref Param_Number_Profile[] ObjNumber_Profiles)
        {
            try
            {
                ///Initialize ObjIP_Profiles if Not Intialized Yet
                if (ObjNumber_Profiles == null)
                {
                    ObjNumber_Profiles = new Param_Number_Profile[1];
                    ObjNumber_Profiles[0] = new Param_Number_Profile();  ///Atleast Single Param_Number_Profiles
                }
                if (Array.TrueForAll<Param_Standard_Number_Profile>(ObjNumber_Profiles, (x) => x == null))
                {
                    ObjNumber_Profiles = new Param_Number_Profile[1];
                    ObjNumber_Profiles[0] = new Param_Number_Profile();  ///Atleast Single Param_Number_Profiles
                }
                ///Read Param_Number_Profiles
                Base_Class Number_Profile_CommObj = GetSAPEntry(Get_Index._Number_Profile);
                Number_Profile_CommObj.DecodingAttribute = 2;
                ((Class_1)Number_Profile_CommObj).Value_Array = ObjNumber_Profiles;
                Get_Param(Number_Profile_CommObj);
                if (Number_Profile_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    ObjNumber_Profiles = (Param_Number_Profile[])((Class_1)Number_Profile_CommObj).Value_Array;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param_Number_Profile", ex);
            }
        }
        public bool SET_NumberProfiles(Param_Number_Profile[] obj_NumProfiles)
        {
            try
            {
                Base_Class obj_base = GetSAPEntry(Get_Index._Number_Profile);
                obj_base.EncodingAttribute = 0x02;
                ((Class_1)obj_base).Value_Array = obj_NumProfiles;
                var result = SET_Param(obj_base);
                return (result == Data_Access_Result.Success);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Data_Access_Result SET_Number_Profiles_AllowedCallers(Param_Standard_Number_Profile[] ObjNumber_Profiles)
        {
            try
            {
                if (ObjNumber_Profiles == null || Array.TrueForAll<Param_Standard_Number_Profile>(ObjNumber_Profiles, (x) => x == null))
                    throw new Exception("Error occurred while setting Param_Number_Profiles,Invalid argument passed");
                // Write IP Profiles
                Class_28 Number_Profile_CommObj = (Class_28)GetSAPEntry(Get_Index.AutoAnswer);
                Number_Profile_CommObj.EncodingAttribute = 07;

                if (Number_Profile_CommObj.Allowed_AutoAnswerCallers == null)
                    Number_Profile_CommObj.Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(04);
                else
                    Number_Profile_CommObj.Allowed_AutoAnswerCallers.Clear();

                // Loading Number Profile Data
                KeyValuePair<AutoAnswerCallerType, byte[]> Num_Prof_Entry;

                foreach (var numProf in ObjNumber_Profiles)
                {
                    Num_Prof_Entry = new KeyValuePair<AutoAnswerCallerType, byte[]>(numProf.CallerType, numProf.Number);
                    Number_Profile_CommObj.Allowed_AutoAnswerCallers.Add(Num_Prof_Entry);
                }

                return SET_Param(Number_Profile_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Number_Profiles Allowed Callers", ex);
            }
        }

        #endregion

        #region WakeUp Profiles
        public void GET_WakeUp_Profile(ref Param_WakeUp_Profile[] Obj_WakeUp_Params)
        {
            try
            {
                if (Obj_WakeUp_Params == null || Array.TrueForAll<Param_WakeUp_Profile>(Obj_WakeUp_Params, (x) => x == null))
                    throw new Exception("Error occurred while setting Param_WakeUp_Profile,Invalid argument passed");
                ///Write IP Profiles
                Base_Class WakeUpParams_CommObj = GetSAPEntry(Get_Index._WakeUp_Profile);
                WakeUpParams_CommObj.DecodingAttribute = 2;
                ((Class_1)WakeUpParams_CommObj).Value_Array = Obj_WakeUp_Params;
                Class_1 WakeUpParam_CommObj = (Class_1)Get_Param(WakeUpParams_CommObj);
                if (WakeUpParam_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    Obj_WakeUp_Params = new Param_WakeUp_Profile[WakeUpParam_CommObj.Value_Array.Length];
                    for (int index = 0; index < WakeUpParam_CommObj.Value_Array.Length; index++)
                    {
                        Obj_WakeUp_Params[index] = (Param_WakeUp_Profile)((ICustomStructure[])WakeUpParam_CommObj.Value_Array)[index];
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_WakeUp_Profile", ex);
            }
        }
        public bool SET_WakeUp_Profile(Param_WakeUp_Profile[] Obj_WakeUp_Profile)
        {
            try
            {
                if (Obj_WakeUp_Profile == null || Array.TrueForAll<Param_WakeUp_Profile>(Obj_WakeUp_Profile, (x) => x == null))
                    throw new Exception("Error occurred while setting Param_Wakeup_Profile,Invalid argument passed");
                ///Write IP Profiles
                Base_Class WakeUpParams_CommObj = GetSAPEntry(Get_Index._WakeUp_Profile);
                WakeUpParams_CommObj.EncodingAttribute = 2;
                ((Class_1)WakeUpParams_CommObj).Value_Array = Obj_WakeUp_Profile;
                return (SET_Param(WakeUpParams_CommObj) == Data_Access_Result.Success);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Wakeup_Profile", ex);
            }

        }
        #endregion

        #region Dispaly Power Down
        public void GET_Display_PowerDown(ref Param_Display_PowerDown obj_DispWindow)
        {
            try
            {
                Base_Class com_Obj_DispWindow = GetSAPEntry(Get_Index.Display_At_Power_Down_Mode);

                com_Obj_DispWindow.DecodingAttribute = 0x02;
                Get_Param(com_Obj_DispWindow);
                ///***modification
                obj_DispWindow.Decode_Param_Display_PowerDown(com_Obj_DispWindow);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_Display_PowerDown", ex);
            }
        }

        public void GET_Display_PowerDown(Param_Display_PowerDown obj_DispWindow)
        {
            try
            {
                Base_Class com_Obj_DispWindow = GetSAPEntry(Get_Index.Display_At_Power_Down_Mode);

                com_Obj_DispWindow.DecodingAttribute = 0x02;
                Get_Param(com_Obj_DispWindow);
                ///***modification
                obj_DispWindow.Decode_Param_Display_PowerDown(com_Obj_DispWindow);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_Display_PowerDown", ex);
            }
        }
        public bool SET_Display_Power_Down(Param_Display_PowerDown obj_Dips)
        {
            try
            {
                var objBase = obj_Dips.Encode_Param_Display_PowerDown(GetSAPEntry);
                var result = SET_Param(objBase);
                return (result == Data_Access_Result.Success);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region Limits

        public void GET_Limit_Over_Voltage(ref Param_Limits Over_Voltage_Limit)
        {
            try
            {
                Base_Class Over_Voltage_Limit_Comm_Obj = Get_Param(Get_Index.Over_Voltage_Limit, 0);
                Over_Voltage_Limit.Decode_Limits_Over_Voltage_Limit_attrib2(Over_Voltage_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Voltage_Limit", ex);
            }
        }

        public Data_Access_Result SET_Limit_Over_Voltage(Param_Limits Over_Voltage_Limit)
        {
            try
            {
                Base_Class Over_Voltage_Limit_obj = Over_Voltage_Limit.Encode_Limits_Over_Voltage_Limit_attrib2(GetSAPEntry);
                return SET_Param(Over_Voltage_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Over Voltage Limit", ex);
            }
        }

        public void GET_Limit_Under_Voltage(ref Param_Limits Under_Voltage_Limit)
        {
            try
            {
                Base_Class Under_Voltage_Limit_Comm_Obj = Get_Param(Get_Index.Under_Voltage_Limit, 0);
                Under_Voltage_Limit.Decode_Limits_Under_Voltage_Limit_attrib2(Under_Voltage_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Under_voltage_limit", ex);
            }
        }

        public Data_Access_Result SET_Limit_Under_Voltage(Param_Limits Under_Voltage_Limit)
        {
            try
            {
                Base_Class Under_Voltage_Limit_obj = Under_Voltage_Limit.Encode_Limits_Under_Voltage_Limit_attrib2(GetSAPEntry);
                return SET_Param(Under_Voltage_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Under Voltage Limit", ex);
            }
        }

        public void GET_Limit_Imbalance_Voltage(ref Param_Limits Imbalance_Voltage_Limit)
        {
            try
            {
                Base_Class Imbalance_Voltage_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Imbalance_Volt, 0);
                Imbalance_Voltage_Limit.Decode_Limits_Imbalance_Volt_attrib2(Imbalance_Voltage_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Imbalance Voltage Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_Imbalance_Voltage(Param_Limits Imbalance_Voltage_Limit)
        {
            try
            {
                Base_Class Imbalance_Voltage_Limit_obj = Imbalance_Voltage_Limit.Encode_Limits_Imbalance_Volt_attrib2(GetSAPEntry);
                return SET_Param(Imbalance_Voltage_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Imbalance Voltage Limit", ex);
            }
        }

        public void GET_Limit_High_Neutral_Current(ref Param_Limits High_Neutral_Current_Limit)
        {
            try
            {
                Base_Class High_Neutral_Current_Limit_Comm_Obj = Get_Param(Get_Index.Limits_High_Neutral_Current, 0);
                High_Neutral_Current_Limit.Decode_Limits_High_Neutral_Current_attrib2(High_Neutral_Current_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting High_Neutral_Current", ex);
            }
        }
        public Data_Access_Result SET_Limit_High_Neutral_Current(Param_Limits High_Neutral_Current_Limit)
        {
            try
            {
                Base_Class High_Neutral_Current_Limit_obj = High_Neutral_Current_Limit.Encode_Limits_High_Neutral_Current_attrib2(GetSAPEntry);
                return SET_Param(High_Neutral_Current_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting High_Neutral_Current", ex);
            }
        }


        public void GET_Limit_Reverse_Energy(ref Param_Limits Reverse_Energy_Limit)
        {
            try
            {
                Base_Class Reverse_Energy_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Reverse_Energy_, 0);
                Reverse_Energy_Limit.Decode_Limits_Reverse_Energy_attrib2(Reverse_Energy_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Reverse_Energy_Limit ", ex);
            }
        }
        public Data_Access_Result SET_Limit_Reverse_Energy_Limit(Param_Limits Reverse_Energy_Limit)
        {
            try
            {
                Base_Class Reverse_Energy_Limit_obj = Reverse_Energy_Limit.Encode_Limits_Reverse_Energy_attrib2(GetSAPEntry);
                return SET_Param(Reverse_Energy_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Reverse_Energy_Limit", ex);
            }
        }

        public void GET_Limit_Tamper_Energy(ref Param_Limits Tamper_Energy_Limit)
        {
            try
            {
                Base_Class Tamper_Energy_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Tamper_Energy, 0);
                Tamper_Energy_Limit.Decode_Limits_Tamper_Energy_attrib2(Tamper_Energy_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Limits_Tamper_Energy", ex);
            }
        }
        public Data_Access_Result SET_Limit_Tamper_Energy(Param_Limits Tamper_Energy_Limit)
        {
            try
            {
                Base_Class Tamper_Energy_Limit_obj = Tamper_Energy_Limit.Encode_Limits_Tamper_Energy_attrib2(GetSAPEntry);
                return SET_Param(Tamper_Energy_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limits_Tamper_Energy", ex);
            }
        }

        public void GET_Limit_CT_Fail_AMP(ref Param_Limits CT_Fail_AMP_Limit)
        {
            try
            {
                Base_Class Limits_CT_Fail__Amp_Limit_Comm_Obj = Get_Param(Get_Index.Limits_CT_Fail__Amp_Limit, 0);
                CT_Fail_AMP_Limit.Decode_Limits_CT_Fail__Amp_Limit_attrib2(Limits_CT_Fail__Amp_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting CT_Fail_AMP_Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_CT_Fail_AMP(Param_Limits CT_Fail_AMP_Limit)
        {
            try
            {
                Base_Class CT_Fail_AMP_Limit_obj = CT_Fail_AMP_Limit.Encode_Limits_CT_Fail__Amp_Limit_attrib2(GetSAPEntry);
                return SET_Param(CT_Fail_AMP_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting CT_Fail_AMP_Limit", ex);
            }
        }

        public void GET_Limit_PT_Fail_AMP(ref Param_Limits Limit_PT_Fail_AMP)
        {
            try
            {
                Base_Class GET_Limit_PT_Fail_AMP_ = Get_Param(Get_Index.Limits_PT_Fail_Amp_Limit, 0);
                Limit_PT_Fail_AMP.Decode_Limits_PT_Fail_Amp_Limit_attrib2(GET_Limit_PT_Fail_AMP_);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Limits_PT_Fail_Amp_Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_PT_Fail_AMP_or_OverLoad_1P(Param_Limits PT_Fail_AMP_Limit)
        {
            try
            {
                Base_Class PT_Fail_AMP_Limit_obj = PT_Fail_AMP_Limit.Encode_Limits_PT_Fail_Amp_Limit_attrib2(GetSAPEntry);
                return SET_Param(PT_Fail_AMP_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limits_PT_Fail_Amp_Limit", ex);
            }
        }

        public Data_Access_Result? TrySET_ContactorFailure_Limit(Param_Limits Param_Limits_object)
        {
            try
            {
                Base_Class Limit_obj = Param_Limits_object.Encode_Contactor_Failure_attrib2(GetSAPEntry);
                return SET_Param(Limit_obj);
            }
            catch (Exception ex)
            {
                var _ex = new Exception("Error occurred while setting Contactor_Failure_Limit", ex);
                // throw _ex;
            }

            return null;
        }

        public void GET_Limit_Contactor_Power_Failure(ref Param_Limits Param_Limits_object)
        {
            try
            {

                Base_Class ContactorPowerFailure_Limit_Comm_Obj = GetSAPEntry(Get_Index.CONTACTOR_FAILURE_POWER_LIMIT);

                if (true) // ContactorPowerFailure_Limit_Comm_Obj.IsAttribReadable(0x02))
                {
                    ContactorPowerFailure_Limit_Comm_Obj = Get_Param(Get_Index.CONTACTOR_FAILURE_POWER_LIMIT, 0);
                    Param_Limits_object.Decode_Limits_ContactorFailure_attrib2(ContactorPowerFailure_Limit_Comm_Obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting ContactorFailure_Limit", ex);
            }
        }

        public void GET_Limit_PT_Fail_Volt(ref Param_Limits Limit_PT_Fail_Volt)
        {
            try
            {
                Base_Class GET_Limit_PT_Fail_Volt = Get_Param(Get_Index.Limits_PT_Fail_Volt_Limit, 0);
                Limit_PT_Fail_Volt.Decode_Limits_PT_Fail_Volt_Limit_attrib2(GET_Limit_PT_Fail_Volt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Limits_PT_Fail_volt_Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_PT_Fail_Volt(Param_Limits PT_Fail_Volt_Limit)
        {
            try
            {
                Base_Class PT_Fail_Volt_Limit_obj = PT_Fail_Volt_Limit.Encode_Limits_PT_Fail_Volt_Limit_attrib2(GetSAPEntry);
                return SET_Param(PT_Fail_Volt_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limits_PT_Fail_Volt_Limit", ex);
            }
        }

        public void GET_Limit_Over_Current_by_Phase_T1(ref Param_Limits Over_Current_by_Phase_T1_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T1_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Current_By_Phase_T1, 0);
                Over_Current_by_Phase_T1_Limit.Decode_Limits_Over_Current_By_Phase_T1_attrib2(Over_Current_by_Phase_T1_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Limits_Over_Current_By_Phase_T1", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Current_by_Phase_T1(Param_Limits Over_Current_by_Phase_T1_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T1_Limit_obj = Over_Current_by_Phase_T1_Limit.Encode_Limits_Over_Current_By_Phase_T1_attrib2(GetSAPEntry);
                return SET_Param(Over_Current_by_Phase_T1_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limits_Over_Current_By_Phase_T1", ex);
            }
        }

        public void GET_Limit_Over_Current_by_Phase_T2(ref Param_Limits Over_Current_by_Phase_T2_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T2_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Current_By_Phase_T2, 0);
                Over_Current_by_Phase_T2_Limit.Decode_Limits_Over_Current_By_Phase_T2_attrib2(Over_Current_by_Phase_T2_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Limits_Over_Current_By_Phase_T2", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Current_by_Phase_T2(Param_Limits Over_Current_by_Phase_T2_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T2_Limit_obj = Over_Current_by_Phase_T2_Limit.Encode_Limits_Over_Current_By_Phase_T2_attrib2(GetSAPEntry);
                return SET_Param(Over_Current_by_Phase_T2_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limits_Over_Current_By_Phase_T2", ex);
            }
        }

        public void GET_Limit_Over_Current_by_Phase_T3(ref Param_Limits Over_Current_by_Phase_T3_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T3_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Current_By_Phase_T3, 0);
                Over_Current_by_Phase_T3_Limit.Decode_Limits_Over_Current_By_Phase_T3_attrib2(Over_Current_by_Phase_T3_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Limits_Over_Current_By_Phase_T3", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Current_by_Phase_T3(Param_Limits Over_Current_by_Phase_T3_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T3_Limit_obj = Over_Current_by_Phase_T3_Limit.Encode_Limits_Over_Current_By_Phase_T3_attrib2(GetSAPEntry);
                return SET_Param(Over_Current_by_Phase_T3_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limits_Over_Current_By_Phase_T3", ex);
            }
        }

        public void GET_Limit_Over_Current_by_Phase_T4(ref Param_Limits Over_Current_by_Phase_T4_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T4_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Current_By_Phase_T4, 0);
                Over_Current_by_Phase_T4_Limit.Decode_Limits_Over_Current_By_Phase_T4_attrib2(Over_Current_by_Phase_T4_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Limits_Over_Current_By_Phase_T4", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Current_by_Phase_T4(Param_Limits Over_Current_by_Phase_T4_Limit)
        {
            try
            {
                Base_Class Over_Current_by_Phase_T4_Limit_obj = Over_Current_by_Phase_T4_Limit.Encode_Limits_Over_Current_By_Phase_T4_attrib2(GetSAPEntry);
                return SET_Param(Over_Current_by_Phase_T4_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limits_Over_Current_By_Phase_T1", ex);
            }
        }

        public void GET_Limit_Over_Load_by_Phase_T1(ref Param_Limits Over_Load_by_Phase_T1_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T1_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_By_Phase1_T1, 0);
                Over_Load_by_Phase_T1_Limit.Decode_Limits_Over_Load_By_Phase1_T1_attrib2(Over_Load_by_Phase_T1_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_by_Phase_T1", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_by_Phase_T1(Param_Limits Over_Load_by_Phase_T1_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T1_Limit_obj = Over_Load_by_Phase_T1_Limit.Encode_Limits_Over_Load_By_Phase1_T1_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_by_Phase_T1_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limit_Over_Load_by_Phase_T1", ex);
            }
        }

        public void GET_Limit_Over_Load_by_Phase_T2(ref Param_Limits Over_Load_by_Phase_T2_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T2_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_By_Phase1_T2, 0);
                Over_Load_by_Phase_T2_Limit.Decode_Limits_Over_Load_By_Phase1_T2_attrib2(Over_Load_by_Phase_T2_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_by_Phase_T2", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_by_Phase_T2(Param_Limits Over_Load_by_Phase_T2_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T2_Limit_obj = Over_Load_by_Phase_T2_Limit.Encode_Limits_Over_Load_By_Phase1_T2_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_by_Phase_T2_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limit_Over_Load_by_Phase_T2", ex);
            }
        }

        public void GET_Limit_Over_Load_by_Phase_T3(ref Param_Limits Over_Load_by_Phase_T3_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T3_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_By_Phase1_T3, 0);
                Over_Load_by_Phase_T3_Limit.Decode_Limits_Over_Load_By_Phase1_T3_attrib2(Over_Load_by_Phase_T3_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_by_Phase_T3", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_by_Phase_T3(Param_Limits Over_Load_by_Phase_T3_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T3_Limit_obj = Over_Load_by_Phase_T3_Limit.Encode_Limits_Over_Load_By_Phase1_T3_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_by_Phase_T3_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limit_Over_Load_by_Phase_T3", ex);
            }
        }

        public void GET_Limit_Over_Load_by_Phase_T4(ref Param_Limits Over_Load_by_Phase_T4_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T4_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_By_Phase1_T4, 0);
                Over_Load_by_Phase_T4_Limit.Decode_Limits_Over_Load_By_Phase1_T4_attrib2(Over_Load_by_Phase_T4_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_by_Phase_T4", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_by_Phase_T4(Param_Limits Over_Load_by_Phase_T4_Limit)
        {
            try
            {
                Base_Class Over_Load_by_Phase_T4_Limit_obj = Over_Load_by_Phase_T4_Limit.Encode_Limits_Over_Load_By_Phase1_T4_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_by_Phase_T4_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Limit_Over_Load_by_Phase_T4", ex);
            }
        }

        public void GET_Limit_Over_Load_Total_T1(ref Param_Limits Over_Load_Total_T1_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T1_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_Total_T1, 0);
                Over_Load_Total_T1_Limit.Decode_Limits_Over_Load_Total_T1_attrib2(Over_Load_Total_T1_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_Total_T1_Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_Total_T1(Param_Limits Over_Load_Total_T1_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T1_Limit_obj = Over_Load_Total_T1_Limit.Encode_Limits_Over_Load_Total_T1_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_Total_T1_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Over_Load_Total_T1_Limit", ex);
            }
        }

        public void GET_Limit_Over_Load_Total_T2(ref Param_Limits Over_Load_Total_T2_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T2_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_Total_T2, 0);
                Over_Load_Total_T2_Limit.Decode_Limits_Over_Load_Total_T2_attrib2(Over_Load_Total_T2_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_Total_T2_Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_Total_T2(Param_Limits Over_Load_Total_T2_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T2_Limit_obj = Over_Load_Total_T2_Limit.Encode_Limits_Over_Load_Total_T2_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_Total_T2_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Over_Load_Total_T2_Limit", ex);
            }
        }

        public void GET_Limit_Over_Load_Total_T3(ref Param_Limits Over_Load_Total_T3_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T3_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_Total_T3, 0);
                Over_Load_Total_T3_Limit.Decode_Limits_Over_Load_Total_T3_attrib2(Over_Load_Total_T3_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_Total_T3_Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_Total_T3(Param_Limits Over_Load_Total_T3_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T3_Limit_obj = Over_Load_Total_T3_Limit.Encode_Limits_Over_Load_Total_T3_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_Total_T3_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Over_Load_Total_T3_Limit", ex);
            }
        }

        public void GET_Limit_Over_Load_Total_T4(ref Param_Limits Over_Load_Total_T4_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T4_Limit_Comm_Obj = Get_Param(Get_Index.Limits_Over_Load_Total_T4, 0);
                Over_Load_Total_T4_Limit.Decode_Limits_Over_Load_Total_T4_attrib2(Over_Load_Total_T4_Limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Over_Load_Total_T4_Limit", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Load_Total_T4(Param_Limits Over_Load_Total_T4_Limit)
        {
            try
            {
                Base_Class Over_Load_Total_T4_Limit_obj = Over_Load_Total_T4_Limit.Encode_Limits_Over_Load_Total_T4_attrib2(GetSAPEntry);
                return SET_Param(Over_Load_Total_T4_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Over_Load_Total_T4_Limit", ex);
            }
        }


        public double GET_Limit_Demand_OverLoad(StOBISCode OBIS_Limits_Demand_Over_Load)
        {
            double limit_VAL = double.NaN;
            try
            {
                Base_Class Param_limit_demandOverload_CommObj = GetSAPEntry(OBIS_Limits_Demand_Over_Load);

                // Parameter Limit Demand Over Load
                // FIX Monitoring Time Object
                (Param_limit_demandOverload_CommObj as Class_21).MonitoredValue =
                              new MonitoredValue()
                              {
                                  LogicalName = Get_Index.Active_Power_Total_Pos,
                                  AttributeIndex = 0x02,
                                  TargetDataType = DataTypes._A06_double_long_unsigned
                              };
                Param_limit_demandOverload_CommObj.DecodingAttribute = 2;
                (Param_limit_demandOverload_CommObj as Class_21).BaseClassThresholds =
                    (Param_limit_demandOverload_CommObj as Class_21).InitializeThresholds(null, 01);

                Get_Param(Param_limit_demandOverload_CommObj);

                if (Param_limit_demandOverload_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    limit_VAL = Convert.ToUInt32(((Class_21)Param_limit_demandOverload_CommObj).Thresholds[0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Parameter Limit Demand OverLoad", ex);
            }

            return limit_VAL;
        }



        public void GET_Limit_MDIExceedT1(ref Param_Limits MDI_Exceed_T1)
        {
            try
            {
                MDI_Exceed_T1.DemandOverLoadTotal_T1 = GET_Limit_Demand_OverLoad(Get_Index.Limits_Demand_Over_Load_T1);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while MDI Exceed T1", ex);
            }
        }

        public Data_Access_Result SET_Limit_Demand_OverLoad(Param_Limit_Demand_OverLoad ObjLimit_Demand_OverLoad, StOBISCode Limits_Demand_Over_Load_OBIS)
        {
            try
            {
                if (ObjLimit_Demand_OverLoad == null)
                    throw new ArgumentException("Invalid Function Arguments", "Param_Limit_Demand_OverLoad");

                Base_Class Param_limit_demandOverload_CommObj = GetSAPEntry(Limits_Demand_Over_Load_OBIS);
                // Param Limit Demand Over Load
                // FIX Monitering Time Object
                (Param_limit_demandOverload_CommObj as Class_21).MonitoredValue =
                              new MonitoredValue()
                              {
                                  LogicalName = Get_Index.Active_Power_Total_Pos,
                                  AttributeIndex = 0x02,
                                  TargetDataType = DataTypes._A06_double_long_unsigned
                              };
                Param_limit_demandOverload_CommObj.EncodingAttribute = 2;
                (Param_limit_demandOverload_CommObj as Class_21).Thresholds = new object[] { ObjLimit_Demand_OverLoad.Threshold };
                (Param_limit_demandOverload_CommObj as Class_21).BaseClassThresholds = (Param_limit_demandOverload_CommObj as Class_21).InitializeThresholds(null, 01);
                (Param_limit_demandOverload_CommObj as Class_21).LoadThresholds((Param_limit_demandOverload_CommObj as Class_21).BaseClassThresholds);

                return SET_Param(Param_limit_demandOverload_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Limit_Demand_OverLoad", ex);
            }
        }


        public Data_Access_Result SET_Limit_Demand_OverLoad_T1(Param_Limit_Demand_OverLoad ObjLimit_Demand_OverLoad)
        {
            try
            {
                return SET_Limit_Demand_OverLoad(ObjLimit_Demand_OverLoad, Get_Index.Limits_Demand_Over_Load_T1);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Limit_Demand_OverLoad", ex);
            }
        }

        public void GET_Limit_MDIExceedT2(ref Param_Limits MDI_Exceed_T2)
        {
            try
            {
                MDI_Exceed_T2.DemandOverLoadTotal_T2 = GET_Limit_Demand_OverLoad(Get_Index.Limits_Demand_Over_Load_T2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while MDI Exceed T2", ex);
            }
        }
        public Data_Access_Result SET_Limit_Demand_OverLoad_T2(Param_Limit_Demand_OverLoad ObjLimit_Demand_OverLoad)
        {
            try
            {
                return SET_Limit_Demand_OverLoad(ObjLimit_Demand_OverLoad, Get_Index.Limits_Demand_Over_Load_T2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Limit_Demand_OverLoad", ex);
            }
        }

        public void GET_Limit_MDIExceedT3(ref Param_Limits MDI_Exceed_T3)
        {
            try
            {
                MDI_Exceed_T3.DemandOverLoadTotal_T3 = GET_Limit_Demand_OverLoad(Get_Index.Limits_Demand_Over_Load_T3);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while MDI Exceed T3", ex);
            }
        }
        public Data_Access_Result SET_Limit_Demand_OverLoad_T3(Param_Limit_Demand_OverLoad ObjLimit_Demand_OverLoad)
        {
            try
            {
                return SET_Limit_Demand_OverLoad(ObjLimit_Demand_OverLoad, Get_Index.Limits_Demand_Over_Load_T3);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Limit_Demand_OverLoad", ex);
            }
        }

        public void GET_Limit_MDIExceedT4(ref Param_Limits MDI_Exceed_T4)
        {
            try
            {
                MDI_Exceed_T4.DemandOverLoadTotal_T4 = GET_Limit_Demand_OverLoad(Get_Index.Limits_Demand_Over_Load_T4);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while MDI Exceed T4", ex);
            }
        }
        public Data_Access_Result SET_Limit_Demand_OverLoad_T4(Param_Limit_Demand_OverLoad ObjLimit_Demand_OverLoad)
        {
            try
            {
                return SET_Limit_Demand_OverLoad(ObjLimit_Demand_OverLoad, Get_Index.Limits_Demand_Over_Load_T4);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Limit_Demand_OverLoad", ex);
            }
        }

        /* for Single Phase  // Azeem  */

        public void GET_Limit_OverPower(ref Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = Get_Param(Get_Index.Over_Power_Limit, 0);
                limit.Decode_OverPower_Limit(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Reverse_Energy_Limit ", ex);
            }
        }
        public Data_Access_Result SET_Limit_OverPower(Param_Limits limit)
        {
            try
            {
                Base_Class overPower_Limit_obj = limit.Encode_OverPower_Limit(GetSAPEntry);
                return SET_Param(overPower_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Reverse_Energy_Limit", ex);
            }
        }


        public void GET_Limit_Over_Current_Phase(ref Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = Get_Param(Get_Index.Over_Current_Limit, 0);
                limit.Decode_Limits_Over_Current_Phase_attrib2(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while GET_Limit_Over_Current_Phase", ex);
            }
        }
        public Data_Access_Result SET_Limit_Over_Current_Phase(Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = limit.Encode_Limits_Over_Current_Phase_attrib2(GetSAPEntry);
                return SET_Param(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET_Limit_Over_Current_Phase", ex);
            }
        }

        public void GET_Limit_Meter_On_Load(ref Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = Get_Param(Get_Index.Limits_Meter_ON_LOAD, 0);
                limit.Decode_MeterOnLoad_Limit(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while GET_Limit_Meter_On_Load", ex);
            }
        }
        public Data_Access_Result SET_Limit_Meter_On_Load(Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = limit.Encode_MeterOnLoad_Limit(GetSAPEntry);
                return SET_Param(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET_Limit_Meter_On_Load", ex);
            }
        }

        public void GET_Limit_Power_Factor_Change(ref Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = Get_Param(Get_Index.Limits_POWER_FACTOR_CHANGE, 0);
                limit.Decode_PowerFactor_Change_Limit(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while GET_Limit_Power_Factor_Change", ex);
            }
        }
        public Data_Access_Result SET_Limit_Power_Factor_Change(Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = limit.Encode_PowerFactor_Change_Limit(GetSAPEntry);
                return SET_Param(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET_Limit_Power_Factor_Change", ex);
            }
        }

        public void GET_Limit_Crest_Factor_Low(ref Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = Get_Param(Get_Index.Limits_Crest_Factor_Low, 0);
                limit.Decode_Crest_Factor_Low_Limit(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while GET_Limit_Crest_Factor_Low", ex);
            }
        }
        public Data_Access_Result SET_Limit_Crest_Factor_Low(Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = limit.Encode_Crest_Factor_Low_Limit(GetSAPEntry);
                return SET_Param(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET_Limit_Crest_Factor_Low", ex);
            }
        }

        public void GET_Limit_Crest_Factor_High(ref Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = Get_Param(Get_Index.Limits_Crest_Factor_High, 0);
                limit.Decode_Crest_Factor_High_Limit(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while GET_Limit_Crest_Factor_High", ex);
            }
        }
        public Data_Access_Result SET_Limit_Crest_Factor_High(Param_Limits limit)
        {
            try
            {
                Base_Class limit_Comm_Obj = limit.Encode_Crest_Factor_High_Limit(GetSAPEntry);
                return SET_Param(limit_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET_Limit_Crest_Factor_High", ex);
            }
        }

        public bool SET_Limits_All(Param_Limits obj_Limits)
        {
            #region Limits
            try
            {

                if (obj_Limits.WriteCTFail_AMP)
                    if (SET_Limit_CT_Fail_AMP(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteHighNeutralCurrent)
                    if (SET_Limit_High_Neutral_Current(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteImbalanceVolt)
                    if (SET_Limit_Imbalance_Voltage(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverCurrentByPhase_T1)
                    if (SET_Limit_Over_Current_by_Phase_T1(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverCurrentByPhase_T2)
                    if (SET_Limit_Over_Current_by_Phase_T2(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverCurrentByPhase_T3)
                    if (SET_Limit_Over_Current_by_Phase_T3(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverCurrentByPhase_T4)
                    if (SET_Limit_Over_Current_by_Phase_T4(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadByPhase_T1)
                    if (SET_Limit_Over_Load_by_Phase_T1(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadByPhase_T2)
                    if (SET_Limit_Over_Load_by_Phase_T2(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadByPhase_T3)
                    if (SET_Limit_Over_Load_by_Phase_T3(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadByPhase_T4)
                    if (SET_Limit_Over_Load_by_Phase_T4(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadTotal_T1)
                    if (SET_Limit_Over_Load_Total_T1(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadTotal_T2)
                    if (SET_Limit_Over_Load_Total_T2(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadTotal_T3)
                    if (SET_Limit_Over_Load_Total_T3(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverLoadTotal_T4)
                    if (SET_Limit_Over_Load_Total_T4(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteOverVolt)
                    if (SET_Limit_Over_Voltage(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WritePTFail_AMP)
                    if (SET_Limit_PT_Fail_AMP_or_OverLoad_1P(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WritePTFail_Volt)
                    if (SET_Limit_PT_Fail_Volt(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteReverseEnergy)
                    if (SET_Limit_Reverse_Energy_Limit(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteTamperEnergy)
                    if (SET_Limit_Tamper_Energy(obj_Limits) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteUnderVolt)
                    if (SET_Limit_Under_Voltage(obj_Limits) != Data_Access_Result.Success) return false;

                #region Init Param_Limit_Demand_Overload

                Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T1 = new Param_Limit_Demand_OverLoad()
                {
                    Threshold = obj_Limits.DemandOverLoadTotal_T1
                };

                Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T2 = new Param_Limit_Demand_OverLoad()
                {
                    Threshold = obj_Limits.DemandOverLoadTotal_T2
                };

                Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T3 = new Param_Limit_Demand_OverLoad()
                {
                    Threshold = obj_Limits.DemandOverLoadTotal_T3
                };

                Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T4 = new Param_Limit_Demand_OverLoad()
                {
                    Threshold = obj_Limits.DemandOverLoadTotal_T4
                };

                #endregion

                if (obj_Limits.WriteDemandOverLoadTotal_T1) if (SET_Limit_Demand_OverLoad_T1(Param_Limit_Demand_OverLoad_T1) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteDemandOverLoadTotal_T2) if (SET_Limit_Demand_OverLoad_T2(Param_Limit_Demand_OverLoad_T2) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteDemandOverLoadTotal_T3) if (SET_Limit_Demand_OverLoad_T3(Param_Limit_Demand_OverLoad_T3) != Data_Access_Result.Success) return false;
                if (obj_Limits.WriteDemandOverLoadTotal_T4) if (SET_Limit_Demand_OverLoad_T4(Param_Limit_Demand_OverLoad_T4) != Data_Access_Result.Success) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }


        #endregion

        #region Monitoring Times

        public void GET_MT_Power_Fail(ref Param_Monitoring_Time Power_Fail)
        {
            try
            {
                Base_Class MT_Power_Fail_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Power_Fail, 0);
                Power_Fail.Decode_Power_Fail(MT_Power_Fail_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Power_Fail", ex);
            }
        }
        public Data_Access_Result SET_MT_Power_Fail(Param_Monitoring_Time Power_Fail)
        {
            try
            {
                Base_Class Power_Fail_MT_obj = Power_Fail.Encode_Power_Fail_attrib2(GetSAPEntry);
                return SET_Param(Power_Fail_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Power_Fail", ex);
            }
        }

        public void GET_MT_Phase_Fail(ref Param_Monitoring_Time Phase_Fail)
        {
            try
            {
                Base_Class MT_Phase_Fail_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Phase_Fail, 0);
                Phase_Fail.Decode_Phase_Fail(MT_Phase_Fail_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Phase_Fail", ex);
            }
        }
        public Data_Access_Result SET_MT_Phase_Fail(Param_Monitoring_Time Phase_Fail)
        {
            try
            {
                Base_Class Phase_Fail_MT_obj = Phase_Fail.Encode_Phase_Fail_attrib2(GetSAPEntry);
                return SET_Param(Phase_Fail_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Phase_Fail", ex);
            }
        }

        public void GET_MT_Over_Volt(ref Param_Monitoring_Time Over_Volt)
        {
            try
            {
                Base_Class MT_Over_Volt_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Over_Volt_, 0);
                Over_Volt.Decode_Over_Volt(MT_Over_Volt_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Over_Volt", ex);
            }
        }
        public Data_Access_Result SET_MT_Over_Volt(Param_Monitoring_Time Over_Volt)
        {
            try
            {
                Base_Class Over_Volt_MT_obj = Over_Volt.Encode_Over_Volt_attrib2(GetSAPEntry);
                return SET_Param(Over_Volt_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Over_Volt", ex);
            }
        }

        public void GET_MT_Under_Volt(ref Param_Monitoring_Time Under_Volt)
        {
            try
            {
                Base_Class MT_Under_Volt_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Under_Volt, 0);
                Under_Volt.Decode_Under_Volt(MT_Under_Volt_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Under_Volt", ex);
            }
        }
        public Data_Access_Result SET_MT_Under_Volt(Param_Monitoring_Time Under_Volt)
        {
            try
            {
                Base_Class Under_Volt_MT_obj = Under_Volt.Encode_Under_Volt_attrib2(GetSAPEntry);
                return SET_Param(Under_Volt_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Under_Volt", ex);
            }
        }

        public void GET_MT_Imbalance_Volt(ref Param_Monitoring_Time Imbalance_Volt)
        {
            try
            {
                Base_Class MT_Imbalance_Volt_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Imbalance_Volt, 0);
                Imbalance_Volt.Decode_Imbalance_Volt(MT_Imbalance_Volt_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Imbalance_Volt", ex);
            }
        }
        public Data_Access_Result SET_MT_Imbalance_Volt(Param_Monitoring_Time Imbalance_Volt)
        {
            try
            {
                Base_Class Imbalance_Volt_MT_obj = Imbalance_Volt.Encode_Imbalance_Volt(GetSAPEntry);
                return SET_Param(Imbalance_Volt_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Imbalance_Volt", ex);
            }
        }

        public void GET_MT_High_Neutral_Current(ref Param_Monitoring_Time High_Neutral_Current)
        {
            try
            {
                Base_Class MT_High_Neutral_Current_Comm_Obj = Get_Param(Get_Index.MonitoringTime_High_Neutral_Current, 0);
                High_Neutral_Current.Decode_High_Neutral_Current(MT_High_Neutral_Current_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_High_Neutral_Current", ex);
            }
        }
        public Data_Access_Result SET_MT_High_Neutral_Current(Param_Monitoring_Time High_Neutral_Current)
        {
            try
            {
                Base_Class High_Neutral_Current_MT_obj = High_Neutral_Current.Encode_High_Neutral_Current(GetSAPEntry);
                return SET_Param(High_Neutral_Current_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_High_Neutral_Current", ex);
            }
        }

        public void GET_MT_Over_Current(ref Param_Monitoring_Time Over_Current)
        {
            try
            {
                Base_Class MT_Over_Current_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Over_Current, 0);
                Over_Current.Decode_Over_Current(MT_Over_Current_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Over_Current", ex);
            }
        }
        public Data_Access_Result SET_MT_Over_Current(Param_Monitoring_Time Over_Current)
        {
            try
            {
                Base_Class Over_Current_MT_obj = Over_Current.Encode_Over_Current(GetSAPEntry);
                return SET_Param(Over_Current_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Over_Current", ex);
            }
        }

        public void GET_MT_Over_Load(ref Param_Monitoring_Time Over_Load)
        {
            try
            {
                Base_Class MT_Over_Load_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Over_Load, 0);
                Over_Load.Decode_Over_Load(MT_Over_Load_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Over_Load", ex);
            }
        }
        public Data_Access_Result SET_MT_Over_Load(Param_Monitoring_Time Over_Load)
        {
            try
            {
                Base_Class Over_Load_MT_obj = Over_Load.Encode_Over_Load(GetSAPEntry);
                return SET_Param(Over_Load_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Over_Load", ex);
            }
        }

        public void GET_MT_Reverse_Energy(ref Param_Monitoring_Time Reverse_Energy)
        {
            try
            {
                Base_Class MT_Reverse_Energy_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Reverse_Energy, 0);
                Reverse_Energy.Decode_Reverse_Energy(MT_Reverse_Energy_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Reverse_Energy", ex);
            }
        }
        public Data_Access_Result SET_MT_Reverse_Energy(Param_Monitoring_Time Reverse_Energy)
        {
            try
            {
                Base_Class Reverse_Energy_MT_obj = Reverse_Energy.Encode_Reverse_Energy(GetSAPEntry);
                return SET_Param(Reverse_Energy_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Reverse_Energy", ex);
            }
        }

        public void GET_MT_Contactor_Failure(ref Param_Monitoring_Time MT_obj)
        {
            try
            {
                Base_Class MT_Comm_Obj = Get_Param(Get_Index.CONTACTOR_FAILURE_MONITORING_TIME, 0);
                MT_obj.Decode_Contactor_Failure(MT_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Reverse_Energy", ex);
            }
        }
        public Data_Access_Result SET_MT_Contactor_Failure(Param_Monitoring_Time MT_obj)
        {
            try
            {
                Base_Class contactor_failure_obj = MT_obj.Encode_Contactor_Failure(GetSAPEntry);
                return SET_Param(contactor_failure_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Contactor_Failure", ex);
            }
        }

        public void GET_MT_earth(ref Param_Monitoring_Time earth)
        {
            try
            {
                Base_Class MT_earth_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Earth, 0);
                earth.Decode_earth(MT_earth_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Reverse_Energy", ex);
            }
        }
        public Data_Access_Result SET_MT_earth(Param_Monitoring_Time earth)
        {
            try
            {
                Base_Class earth_MT_obj = earth.Encode_earth(GetSAPEntry);
                return SET_Param(earth_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Reverse_Energy", ex);
            }
        }

        public void GET_MT_Reverse_Polarity(ref Param_Monitoring_Time Reverse_Polarity)
        {
            try
            {
                Base_Class MT_Reverse_Polarity_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Reverse_Polarity, 0);
                Reverse_Polarity.Decode_Reverse_Polarity(MT_Reverse_Polarity_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Reverse_Polarity", ex);
            }
        }
        public Data_Access_Result SET_MT_Reverse_Polarity(Param_Monitoring_Time Reverse_Polarity)
        {
            try
            {
                Base_Class Reverse_Polarity_MT_obj = Reverse_Polarity.Encode_Reverse_Polarity(GetSAPEntry);
                return SET_Param(Reverse_Polarity_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Reverse_Polarity", ex);
            }
        }

        public void GET_MT_Phase_Sequence(ref Param_Monitoring_Time Phase_Sequence)
        {
            try
            {
                Base_Class MT_Phase_Sequence_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Phase_Sequence, 0);
                Phase_Sequence.Decode_Phase_Sequence(MT_Phase_Sequence_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Phase_Sequence", ex);
            }
        }
        public Data_Access_Result SET_MT_Phase_Sequence(Param_Monitoring_Time Phase_Sequence)
        {
            try
            {
                Base_Class Phase_Sequence_MT_obj = Phase_Sequence.Encode_Phase_Sequence(GetSAPEntry);
                return SET_Param(Phase_Sequence_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Phase_Sequence", ex);
            }
        }

        public void GET_MT_Tamper_Energy(ref Param_Monitoring_Time Tamper_Energy)
        {
            try
            {
                Base_Class MT_Tamper_Energy_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Tamper_Energy, 0);
                Tamper_Energy.Decode_Tamper_Energy(MT_Tamper_Energy_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Tamper_Energy", ex);
            }
        }
        public Data_Access_Result SET_MT_Tamper_Energy(Param_Monitoring_Time Tamper_Energy)
        {
            try
            {
                Base_Class Tamper_Energy_MT_obj = Tamper_Energy.Encode_Tamper_Energy(GetSAPEntry);
                return SET_Param(Tamper_Energy_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Tamper_Energy", ex);
            }
        }

        public void GET_MT_CT_Fail(ref Param_Monitoring_Time CT_Fail)
        {
            try
            {
                Base_Class MT_CT_Fail_Comm_Obj = Get_Param(Get_Index.MonitoringTime_CT_Fail, 0);
                CT_Fail.Decode_CT_Fail(MT_CT_Fail_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_CT_Fail", ex);
            }
        }
        public Data_Access_Result SET_MT_CT_Fail(Param_Monitoring_Time CT_Fail)
        {
            try
            {
                Base_Class CT_Fail_MT_obj = CT_Fail.Encode_CT_Fail(GetSAPEntry);
                return SET_Param(CT_Fail_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_CT_Fail", ex);
            }
        }

        public void GET_MT_PT_Fail(ref Param_Monitoring_Time PT_Fail)
        {
            try
            {
                Base_Class MT_PT_Fail_Comm_Obj = Get_Param(Get_Index.MonitoringTime_PT_Fail, 0);
                PT_Fail.Decode_PT_Fail(MT_PT_Fail_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_PT_Fail", ex);
            }
        }
        public Data_Access_Result SET_MT_PT_Fail(Param_Monitoring_Time PT_Fail)
        {
            try
            {
                Base_Class PT_Fail_MT_obj = PT_Fail.Encode_PT_Fail(GetSAPEntry);
                return SET_Param(PT_Fail_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_PT_Fail", ex);
            }
        }

        public void GET_MT_Power_Up_Delay_To_Monitor(ref Param_Monitoring_Time Power_Up_Delay_To_Monitor)
        {
            try
            {
                Base_Class MT_Power_Up_Delay_To_Monitor_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Power_Up_Delay_To_Monitor, 0);
                Power_Up_Delay_To_Monitor.Decode_Power_Up_Delay_To_Monitor(MT_Power_Up_Delay_To_Monitor_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Power_Up_Delay_To_Monitor", ex);
            }
        }
        public Data_Access_Result SET_MT_Power_Up_Delay_To_Monitor(Param_Monitoring_Time Power_Up_Delay_To_Monitor)
        {
            try
            {
                Base_Class Power_Up_Delay_To_Monitor_MT_obj = Power_Up_Delay_To_Monitor.Encode_Power_Up_Delay_To_Monitor(GetSAPEntry);
                return SET_Param(Power_Up_Delay_To_Monitor_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Power_Fail", ex);
            }
        }

        public void GET_MT_Power_Up_Delay_For_Energy_Recording(ref Param_Monitoring_Time Power_Up_Delay_For_Energy_Recording)
        {
            try
            {
                Base_Class MT_Power_Up_Delay_For_Energy_Recording_Comm_Obj = Get_Param(Get_Index.MonitoringTime_Power_Up_Delay_For_Energy_Recording, 0);
                Power_Up_Delay_For_Energy_Recording.Decode_Power_Up_Delay_For_Energy_Recording(MT_Power_Up_Delay_For_Energy_Recording_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_Power_Up_Delay_For_Energy_Recording", ex);
            }
        }
        public Data_Access_Result SET_MT_Power_Up_Delay_For_Energy_Recording(Param_Monitoring_Time Power_Up_Delay_For_Energy_Recording)
        {
            try
            {
                Base_Class Power_Up_Delay_For_Energy_Recording_MT_obj = Power_Up_Delay_For_Energy_Recording.Encode_Power_Up_Delay_For_Energy_Recording(GetSAPEntry);
                return SET_Param(Power_Up_Delay_For_Energy_Recording_MT_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_Power_Up_Delay_For_Energy_Recording", ex);
            }
        }

        public void GET_MT_HALL_Sensor(ref Param_Monitoring_Time Param_MT_Obj)
        {
            try
            {
                Base_Class MT_HALL_Sensor_Comm_Obj = Get_Param(Get_Index.MonitoringTime_HALL_Sensor, 0);
                Param_MT_Obj.Decode_HALL_Sensor(MT_HALL_Sensor_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting MonitoringTime_HALL_Sensor", ex);
            }
        }

        public Data_Access_Result SET_MT_HALL_Sensor(Param_Monitoring_Time Param_MT_Obj)
        {
            try
            {
                Base_Class MT_Comm_obj = Param_MT_Obj.Encode_HALL_Sensor(GetSAPEntry);
                return SET_Param(MT_Comm_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MonitoringTime_HALL_Sensor", ex);
            }
        }
        public bool SET_MonitoringTimeAll(Param_Monitoring_Time Param_MT_Obj)
        {
            try
            {
                if (Param_MT_Obj.WriteCTFail)
                {
                    if (SET_MT_CT_Fail(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteHighNeutralCurrent)
                {
                    if (SET_MT_High_Neutral_Current(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteImbalanceVolt)
                {
                    if (SET_MT_Imbalance_Volt(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteOverCurrent)
                {
                    // Data_Access_Result res =SET_MT_Over_Current(Param_MT_Obj);
                    if (SET_MT_Over_Current(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteOverLoad)
                {
                    if (SET_MT_Over_Load(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteOverVolt)
                {
                    if (SET_MT_Over_Volt(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WritePhaseFail)
                {
                    if (SET_MT_Phase_Fail(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WritePhaseSequence)
                {
                    if (SET_MT_Phase_Sequence(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WritePowerFail)
                {
                    if (SET_MT_Power_Fail(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WritePowerUpDelayEnergyRecording)
                {
                    if (SET_MT_Power_Up_Delay_For_Energy_Recording(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WritePowerUPDelay)
                {
                    if (SET_MT_Power_Up_Delay_To_Monitor(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WritePTFail)
                {
                    if (SET_MT_PT_Fail(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteReverseEnergy)
                {
                    if (SET_MT_Reverse_Energy(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteReversePolarity)
                {
                    if (SET_MT_Reverse_Polarity(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteTamperEnergy)
                {
                    if (SET_MT_Tamper_Energy(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteUnderVolt)
                {
                    if (SET_MT_Under_Volt(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }
                if (Param_MT_Obj.WriteHALLSensor)
                {
                    if (SET_MT_HALL_Sensor(Param_MT_Obj) != Data_Access_Result.Success) return false;
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region Energy param

        public void GET_EnergyParams(ref Param_Energy_Parameter ObjEnergyParams)
        {
            try
            {
                Base_Class EnergyParam_CommObj = Get_Param(Get_Index._EneryParams, 2);
                ObjEnergyParams.Decode_energy_parameters(EnergyParam_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_Energy_Parameter", ex);
            }
        }
        public Data_Access_Result SET_EnergyParams(Param_Energy_Parameter ObjEnergyParams)
        {
            try
            {
                Base_Class Energy_Parameters_obj = ObjEnergyParams.Encode_energy_parameters(GetSAPEntry);
                return SET_Param(Energy_Parameters_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Energy_Parameter", ex);
            }
        }

        #endregion

        #region Decimal Point

        public void GET_Decimal_Point(ref Param_Decimal_Point decimal_point)
        {
            try
            {
                Base_Class Decimal_Point_Comm_Obj = Get_Param(Get_Index._DecimalPoint, 0);
                decimal_point.Decode_DecimalPoint(Decimal_Point_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting decimal_point", ex);
            }
        }
        public Data_Access_Result SET_Decimal_Point(Param_Decimal_Point decimal_point)
        {
            try
            {
                Base_Class decimal_point_obj = decimal_point.Encode_DecimalPoint(GetSAPEntry);
                return SET_Param(decimal_point_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting decimal_point", ex);
            }
        }

        #endregion

        #region CT_PT_Params

        public bool SET_CTPT_Param(Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                var rslt = false;
                rslt = (SET_CTRatio_Numerator(CTPT_Params) == Data_Access_Result.Success);
                if (!rslt) return false;
                rslt = (SET_CTRatio_Denominator(CTPT_Params) == Data_Access_Result.Success);
                if (!rslt) return false;
                rslt = (SET_PTRatio_Numerator(CTPT_Params) == Data_Access_Result.Success);
                if (!rslt) return false;
                rslt = (SET_PTRatio_Denominator(CTPT_Params) == Data_Access_Result.Success);
                if (!rslt) return false;


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_CTRatio_Denominator(Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class CT_Denomm = CTPT_Params.Encode_CTRatio_Denominator(GetSAPEntry);
                return SET_Param(CT_Denomm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting PTRatio_Denominator", ex);
            }
        }

        public Data_Access_Result SET_PTRatio_Numerator(Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class PT_Numm = CTPT_Params.Encode_PTRatio_Numerator(GetSAPEntry);
                return SET_Param(PT_Numm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting PT_Ratio_Numerator", ex);
            }
        }

        public Data_Access_Result SET_PTRatio_Denominator(Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class PT_Denomm = CTPT_Params.Encode_PTRatio_Denominator(GetSAPEntry);
                return SET_Param(PT_Denomm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting PTRatio_Denominator", ex);
            }
        }

        public Data_Access_Result SET_CTRatio_Numerator(Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class CT_Numm = CTPT_Params.Encode_CTRatio_Numerator(GetSAPEntry);
                return SET_Param(CT_Numm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting CTRatio_Numerator", ex);
            }
        }

        public void GET_CTPT_Param(ref Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                GET_CTRatio_Numerator(ref CTPT_Params);
                GET_CTRatio_Denominator(ref CTPT_Params);
                GET_PTRatio_Numerator(ref CTPT_Params);
                GET_PTRatio_Denominator(ref CTPT_Params);
            }
            catch (Exception ex)
            {

            }
        }

        public void GET_PTRatio_Denominator(ref Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class PT_Numm = Get_Param(Get_Index.PT_Ratio_Denominator, 2);
                CTPT_Params.Decode_PTRatio_Denominator(PT_Numm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting PTRatio_Denominator", ex);
            }
        }

        public void GET_PTRatio_Numerator(ref Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class PT_Numm = Get_Param(Get_Index.CT_Ratio_Numerator, 2);
                CTPT_Params.Decode_CTRatio_Numerator(PT_Numm);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting PTRatio_Numerator", ex);
            }
        }

        public void GET_CTRatio_Denominator(ref Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class PT_Numm = Get_Param(Get_Index.CT_Ratio_Denominator, 2);
                CTPT_Params.Decode_CTRatio_Denominator(PT_Numm);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting CTRatio_Denominator", ex);
            }
        }

        public void GET_CTRatio_Numerator(ref Param_CTPT_Ratio CTPT_Params)
        {
            try
            {
                Base_Class PT_Numm = Get_Param(Get_Index.CT_Ratio_Numerator, 2);
                CTPT_Params.Decode_CTRatio_Numerator(PT_Numm);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting CT_Ratio_Numerator", ex);
            }
        }

        #endregion

        #region GeneralProcessParams

        public Data_Access_Result SET_General_Process(Param_Generel_Process obj_Gpp)
        {
            try
            {
                Base_Class com_Obj_DispWindow = obj_Gpp.Encode_Param_Generel_Process(GetSAPEntry);
                ///Load gerenal process
                return SET_Param(com_Obj_DispWindow);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Generel_Process", ex);
            }

        }

        public void GET_General_Process(Param_Generel_Process obj_Gpp)
        {
            try
            {
                Base_Class com_Obj_Gpp = GetSAPEntry(Get_Index.General_Process_Param);
                com_Obj_Gpp.DecodingAttribute = 0x02;
                Get_Param(com_Obj_Gpp);
                obj_Gpp.Decode_Param_Generel_Process(com_Obj_Gpp);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_Generel_Process", ex);
            }
        }

        #endregion

        #region Activity Calander

        public Data_Access_Result SET_Param_DayProfile(Param_DayProfile DayProfileTable)
        {
            try
            {
                if (!DayProfileTable.IsConsistent)   ///Consistency Errors In DayProfile Table
                    throw new Exception("Some errors,exist in DayProfile table");
                Base_Class ActivityCalendar = DayProfileTable.Encode_Param_DayProfile(GetSAPEntry);
                Data_Access_Result res = SET_Param(ActivityCalendar);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_Param_WeekProfile(Param_WeeKProfile WeekProfileTable)
        {
            try
            {
                if (!WeekProfileTable.IsConsistent)
                    throw new Exception("Some errors,exist in WeekProfile Table");
                Base_Class ActivityCalendar = WeekProfileTable.Encode_Week_Profile(GetSAPEntry);
                Data_Access_Result res = SET_Param(ActivityCalendar);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_Param_SeasonProfile(Param_SeasonProfile SeasonProfileTable)
        {
            try
            {
                //SeasonProfileTable.seasonProfile_Table.Sort((x, y) => x.Start_Date.Month.CompareTo(y.Start_Date.Month));

                if (!SeasonProfileTable.IsConsistent)
                    throw new Exception("Some errors,exist in SeasonProfile Table");
                Base_Class ActivityCalendar = SeasonProfileTable.Encode_Season_Profile(GetSAPEntry);
                Data_Access_Result res = SET_Param(ActivityCalendar);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_Param_SpecialDayProfile(Param_SpecialDay SpecialDayTable)
        {
            try
            {
                if (!SpecialDayTable.IsConsistent)   ///Consistency Errors In DayProfile Table
                    throw new Exception("Some errors,exist in Special DayProfile table");
                Base_Class SpecialDayTableComObj = SpecialDayTable.Encode_SpecialDay_Profile(GetSAPEntry);
                Data_Access_Result res = SET_Param(SpecialDayTableComObj);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_Param_ActivityCalenar_Name(Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class ActivityCalendarComObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ((Class_20)ActivityCalendarComObj).CalendarNamePassive = ActivityCalendar.CalendarName;
                ActivityCalendarComObj.EncodingAttribute = 6;
                Data_Access_Result res = SET_Param(ActivityCalendarComObj);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_Param_ActivityCalenar_ActivePassiveDate(Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class ActivityCalendarComObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ((Class_20)ActivityCalendarComObj).ActivatePassiveCalendarTime = ActivityCalendar.CalendarstartDate;
                ActivityCalendarComObj.EncodingAttribute = 10;
                Data_Access_Result res = SET_Param(ActivityCalendarComObj);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Action_Result Active_Passive_Calendar()
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                Calendar_CommObj.MethodInvokeId = Class_20.ACTIVATE_CALENDAR;
                return Invoke_Param(Calendar_CommObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_ActivityCalendar(Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                ///Check Activity Calendar Correctness
                if (!ActivityCalendar.IsConsistent)
                {
                    if (!ActivityCalendar.ParamDayProfile.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Day Profile Table Inconsistent");
                    if (!ActivityCalendar.ParamWeekProfile.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Week Profile Table Inconsistent");
                    if (!ActivityCalendar.ParamSeasonProfile.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Season Profile Table Inconsistent");
                    if (!ActivityCalendar.ParamSpecialDay.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Special Day Profile Table Inconsistent");

                }
                Data_Access_Result paramDayProfileW = SET_Param_DayProfile(ActivityCalendar.ParamDayProfile);
                if (paramDayProfileW != Data_Access_Result.Success)
                    return paramDayProfileW;
                Data_Access_Result paramWeekProfileW = SET_Param_WeekProfile(ActivityCalendar.ParamWeekProfile);
                if (paramWeekProfileW != Data_Access_Result.Success)
                    return paramWeekProfileW;
                Data_Access_Result paramSeasonProfileW = SET_Param_SeasonProfile(ActivityCalendar.ParamSeasonProfile);
                if (paramSeasonProfileW != Data_Access_Result.Success)
                    return paramSeasonProfileW;
                Data_Access_Result paramSpecialDayProfileProfileW = SET_Param_SpecialDayProfile(ActivityCalendar.ParamSpecialDay);
                if (paramSpecialDayProfileProfileW != Data_Access_Result.Success)
                    return paramSpecialDayProfileProfileW;

                if (ActivityCalendar.CalendarName != null)
                {
                    SET_Param_ActivityCalenar_Name(ActivityCalendar);

                }

                if (ActivityCalendar.ExecuteActivateCalendarAction)
                {
                    Action_Result result = Active_Passive_Calendar();
                    return (Data_Access_Result)result;
                }
                else
                {
                    ///Data_Access_Result paramActivityCalendarName = SET_Param_ActivityCalenar_Name(ActivityCalendar);
                    Data_Access_Result paramActivityCalendarStartDate = SET_Param_ActivityCalenar_ActivePassiveDate(ActivityCalendar);
                    return paramActivityCalendarStartDate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GET_ActivityCalendar(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ///Day Profile Table Active
                Calendar_CommObj.DecodingAttribute = 0x05;
                Get_Param(Calendar_CommObj);
                ///Week Profile Table Active
                Calendar_CommObj.DecodingAttribute = 0x04;
                Get_Param(Calendar_CommObj);
                ///Season Profile Table Active
                Calendar_CommObj.DecodingAttribute = 0x03;
                Get_Param(Calendar_CommObj);
                ///Activity Calendar Name
                Calendar_CommObj.DecodingAttribute = 0x02;
                Get_Param(Calendar_CommObj);
                ///Activity Calendar Name Passive
                //Calendar_CommObj.DecodingAttribute = 0x06;
                //Get_Param(Calendar_CommObj);                    //Azeem
                ///Activity Calendar Activation DateTime
                Calendar_CommObj.DecodingAttribute = 0x0A;
                Get_Param(Calendar_CommObj);

                Base_Class specialDayTable = Get_Param(Get_Index.Special_Days_Table, 0);

                ///Base_Class arg = Get_Param(Get_Index.Activity_Calendar, 0);
                //Base_Class arg_season = Get_Param(Get_Index.Activity_Calendar, 3);
                //Base_Class arg_week = Get_Param(Get_Index.Activity_Calendar, 4);
                //Base_Class arg_day = Get_Param(Get_Index.Activity_Calendar, 5);
                //Base_Class arg_CalendarName = Get_Param(Get_Index.Activity_Calendar, 2);

                ActivityCalendar.CalendarName = ((Class_20)Calendar_CommObj).CalendarNameActive;
                ActivityCalendar.CalendarNamePassive = ((Class_20)Calendar_CommObj).CalendarNamePassive;
                ActivityCalendar.CalendarstartDate = ((Class_20)Calendar_CommObj).ActivatePassiveCalendarTime;
                ActivityCalendar.ParamDayProfile.Decode_Param_DayProfile(Calendar_CommObj);
                ActivityCalendar.ParamWeekProfile.Decode_Week_Profile(Calendar_CommObj, ActivityCalendar.ParamDayProfile);
                ActivityCalendar.ParamSeasonProfile.Decode_Season_Profile(Calendar_CommObj, ActivityCalendar.ParamWeekProfile);
                ActivityCalendar.ParamSpecialDay.Decode_SpecialDay_Profile(specialDayTable, ActivityCalendar.ParamDayProfile);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Script Tables

        public Data_Access_Result SET_ScriptTable(StOBISCode obis_ScriptTable, Param_ScriptTable script_Table)
        {
            try
            {
                return SET_ScriptTable(obis_ScriptTable, script_Table.Scripts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_ScriptTable(StOBISCode obis_ScriptTable, List<Script> scriptList)
        {
            try
            {
                if (scriptList == null || scriptList.Count <= 0)
                    throw new ArgumentException("Invalid Script List");

                if (obis_ScriptTable == Get_Index.Dummy ||
                    obis_ScriptTable.ClassId != 09)
                {
                    throw new ArgumentException("Script Table OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_9)Script_CommObj).Scripts = scriptList;

                // Set Encoding Attribute
                Script_CommObj.EncodingAttribute = 0x02;
                Script_CommObj.EncodingType = DataTypes._A00_Null;

                return SET_Param(Script_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET_ScriptTable", ex);
            }
        }

        public void GET_ScriptTable(StOBISCode obis_ScriptTable, Param_ScriptTable script_Table)
        {
            List<Script> scriptList = null;

            GET_ScriptTable(obis_ScriptTable, ref scriptList);
            script_Table.Scripts = scriptList;
        }

        public void GET_ScriptTable(StOBISCode obis_ScriptTable, ref List<Script> scriptList)
        {
            try
            {
                if (scriptList == null)
                    scriptList = new List<Script>();
                else
                    scriptList.Clear();

                if (obis_ScriptTable == Get_Index.Dummy || obis_ScriptTable.ClassId != 09)
                {
                    throw new ArgumentException("Script Table OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_9)Script_CommObj).Scripts = new List<Script>();

                // Set Decoding Attribute
                Script_CommObj.DecodingAttribute = 0x02;
                Script_CommObj.DecodingType = DataTypes._A00_Null;

                var result = Get_Param(Script_CommObj);

                if (Script_CommObj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                    throw new Exception("Unable to read Script Table " + Script_CommObj.GetAttributeDecodingResult(0x02));

                scriptList = (Script_CommObj as Class_9).Scripts;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while GET_ScriptTable", ex);
            }
        }

        public Action_Result Execute_Script(StOBISCode obis_ScriptTable, int Script_Id)
        {
            try
            {
                if (obis_ScriptTable == Get_Index.Dummy || obis_ScriptTable.ClassId != 09)
                {
                    throw new ArgumentException("Script Table OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_9)Script_CommObj).Scripts = new List<Script>();

                ((Class_9)Script_CommObj).ExecutedScriptSelector = Convert.ToUInt16(Script_Id);
                // Set Mehtod Invoke Id
                Script_CommObj.MethodInvokeId = Class_9.Execute_Script;
                Script_CommObj.Method_ParametersFlag = true;

                var result = Invoke_Param(Script_CommObj);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Invoke Execute Script", ex);
            }
        }

        #endregion

        #region Auto_Scheduler

        public Data_Access_Result SET_SchedulerTable(StOBISCode obis_ScriptTable, Param_Scheduler scheduler_Table)
        {
            try
            {
                return SET_SchedulerTable(obis_ScriptTable, scheduler_Table.SchedulerEntries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Data_Access_Result SET_SchedulerTable(StOBISCode obis_ScriptTable, List<ScheduleEntry> schedules)
        {
            try
            {
                if (schedules == null || schedules.Count <= 0)
                    throw new ArgumentException("Invalid Scheduler List");

                if (obis_ScriptTable == Get_Index.Dummy ||
                    obis_ScriptTable.ClassId != 10)
                {
                    throw new ArgumentException("Invalid Scheduler OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_10)Script_CommObj).Entries = schedules;

                // Set Encoding Attribute
                Script_CommObj.EncodingAttribute = 0x02;
                Script_CommObj.EncodingType = DataTypes._A00_Null;


                return SET_Param(Script_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET_Scheduler Entries Table", ex);
            }
        }

        public void GET_SchedulerTable(StOBISCode obis_ScriptTable, Param_Scheduler scheduler_Table)
        {
            List<ScheduleEntry> schEntriesList = null;

            GET_SchedulerTable(obis_ScriptTable, ref schEntriesList);
            scheduler_Table.SchedulerEntries = schEntriesList;
        }

        public void GET_SchedulerTable(StOBISCode obis_ScriptTable, ref List<ScheduleEntry> schedules)
        {
            try
            {
                if (schedules == null)
                    schedules = new List<ScheduleEntry>(0x0A);
                else
                    schedules.Clear();

                if (obis_ScriptTable == Get_Index.Dummy ||
                    obis_ScriptTable.ClassId != 10)
                {
                    throw new ArgumentException("Invalid Scheduler OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_10)Script_CommObj).Entries = schedules;

                // Set Decoding Attribute
                Script_CommObj.DecodingAttribute = 0x02;
                Script_CommObj.DecodingType = DataTypes._A00_Null;

                var result = Get_Param(Script_CommObj);

                // TODO:  0000 Yahan Error Aata hhy...
                if (Script_CommObj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                    throw new Exception("Unable to read Scheduler Table " + Script_CommObj.GetAttributeDecodingResult(0x02));

                schedules = (Script_CommObj as Class_10).Entries;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while GET_Scheduler Entries Table ", ex);
            }
        }

        public Action_Result Execute_InsertScheduleEntry(StOBISCode obis_ScriptTable, ScheduleEntry SchEntry)
        {
            try
            {
                if (obis_ScriptTable == Get_Index.Dummy || obis_ScriptTable.ClassId != 10)
                {
                    throw new ArgumentException("Invalid Scheduler OBIS Code");
                }

                if (SchEntry == null || SchEntry.Index <= 0 || SchEntry.Index > 9999)
                    throw new ArgumentException("Scheduler Entry");

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_10)Script_CommObj).Current_Entry = SchEntry;

                // Set Method Invoke Id
                Script_CommObj.MethodInvokeId = Class_10.Insert_Execute;
                Script_CommObj.Method_ParametersFlag = true;

                var result = Invoke_Param(Script_CommObj);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Invoke Execute_InsertScheduleEntry", ex);
            }
        }

        public Action_Result Execute_DeleteScheduleEntry(StOBISCode obis_ScriptTable, ScheduleEntryRange SchEntriesIndexer)
        {
            try
            {
                if (obis_ScriptTable == Get_Index.Dummy || obis_ScriptTable.ClassId != 10)
                {
                    throw new ArgumentException("Invalid Scheduler OBIS Code");
                }

                if (SchEntriesIndexer.FirstIndex == 0 && SchEntriesIndexer.FirstIndex == 0)
                    throw new ArgumentException("ScheduleEntryRange");

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_10)Script_CommObj).RangeA = SchEntriesIndexer;

                // Set Method Invoke Id
                Script_CommObj.MethodInvokeId = Class_10.Delete_Execute;
                Script_CommObj.Method_ParametersFlag = true;

                var result = Invoke_Param(Script_CommObj);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Invoke Execute Delete ScheduleEntry", ex);
            }
        }

        public Action_Result Execute_DisableEnableScheduleEntries(StOBISCode obis_ScriptTable,
                                                                  ScheduleEntryRange DisableSchEntriesIndexer,
                                                                  ScheduleEntryRange EnableSchEntriesIndexer)
        {
            try
            {
                if (obis_ScriptTable == Get_Index.Dummy || obis_ScriptTable.ClassId != 10)
                {
                    throw new ArgumentException("Invalid Scheduler OBIS Code");
                }

                if (DisableSchEntriesIndexer.FirstIndex == 0 &&
                    DisableSchEntriesIndexer.FirstIndex == 0)
                    throw new ArgumentException("DisableSchEntriesIndexer");

                if (EnableSchEntriesIndexer.FirstIndex == 0 &&
                    EnableSchEntriesIndexer.FirstIndex == 0)
                    throw new ArgumentException("EnableSchEntriesIndexer");

                Base_Class Script_CommObj = GetSAPEntry(obis_ScriptTable);
                ((Class_10)Script_CommObj).RangeA = DisableSchEntriesIndexer;
                ((Class_10)Script_CommObj).RangeA = EnableSchEntriesIndexer;

                // Set Method Invoke Id
                Script_CommObj.MethodInvokeId = Class_10.Enable_Execute;
                Script_CommObj.Method_ParametersFlag = true;

                var result = Invoke_Param(Script_CommObj);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(@"Error occurred while Invoke Execute Disable\Enable ScheduleEntries", ex);
            }
        }

        #endregion

        #region Consumption Data
        
        public Data_Access_Result SET_ConsumptionDataNow(StOBISCode obis, ConsumptionDataNow consumptionDataNow )
        {
            try
            {
                if (consumptionDataNow == null || consumptionDataNow.consumptionDataNowArr.Count <= 0)
                    throw new ArgumentException("Invalid Consumption Data Now");

                if (obis == Get_Index.Dummy ||
                    obis.ClassId != 1)
                {
                    throw new ArgumentException($"Invalid {Enum.GetName(typeof(Get_Index), Get_Index.CONSUMPTION_DATA_NOW)} OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis);
                ((Class_1)Script_CommObj).Value_Array = consumptionDataNow.Encode_Data();

                // Set Encoding Attribute
                Script_CommObj.EncodingAttribute = 0x02;
                Script_CommObj.EncodingType = DataTypes._A09_octet_string;


                return SET_Param(Script_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET Consumption_Data_Now", ex);
            }
        }

        public Data_Access_Result SET_ConsumptionDataWeekly(StOBISCode obis, ConsumptionDataWeekly consumptionDataWeekly)
        {
            try
            {
                if (consumptionDataWeekly == null || consumptionDataWeekly.consumptionDataWeeklyArr.Count <= 0)
                    throw new ArgumentException("Invalid Consumption Data Weekly ");

                if (obis == Get_Index.Dummy ||
                    obis.ClassId != 1)
                {
                    throw new ArgumentException($"Invalid {Enum.GetName(typeof(Get_Index), Get_Index.CONSUMPTION_DATA_WEEKLY)} OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis);
                ((Class_1)Script_CommObj).Value_Array = consumptionDataWeekly.Encode_Data();

                // Set Encoding Attribute
                Script_CommObj.EncodingAttribute = 0x02;
                Script_CommObj.EncodingType = DataTypes._A09_octet_string;
                
                return SET_Param(Script_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET Consumption_Data_Weekly ", ex);
            }
        }

        public Data_Access_Result SET_ConsumptionDataMonthly(StOBISCode obis, ConsumptionDataMonthly consumptionDataMonthly)
        {
            try
            {
                if (consumptionDataMonthly == null || consumptionDataMonthly.consumptionDataMonthlyArr.Count <= 0)
                    throw new ArgumentException("Invalid Consumption Data Monthly ");

                if (obis == Get_Index.Dummy ||
                    obis.ClassId != 1)
                {
                    throw new ArgumentException($"Invalid {Enum.GetName(typeof(Get_Index), obis)} OBIS Code");
                }

                Base_Class Script_CommObj = GetSAPEntry(obis);
                ((Class_1)Script_CommObj).Value_Array = consumptionDataMonthly.Encode_Data();

                // Set Encoding Attribute
                Script_CommObj.EncodingAttribute = 0x02;
                Script_CommObj.EncodingType = DataTypes._A09_octet_string;

                return SET_Param(Script_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET Consumption_Data_Monthly ", ex);
            }
        }

        #endregion

        #region Generator Start

        public void GET_Generator_Start(ref Param_Generator_Start generatorStart)
        {
            try
            {
                if (generatorStart == null)
                    generatorStart = new Param_Generator_Start();

                generatorStart.GeneratorStart_MonitoringTime = DECODE_Generator_Start_Monitoring_Time();
                generatorStart.GeneratorStart_Tariff = DECODE_Generator_Start_Tariff();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Generator_Start", ex);
            }

        }

        private byte DECODE_Generator_Start_Tariff()
        {
            byte tariff_index = 0x00;
            Base_Class Tariff_On_GeneratorStart_CommObj = Get_Param(Get_Index.TARIFF_ON_GENERATOR, 0x02);

            if (Tariff_On_GeneratorStart_CommObj.INDEX == Get_Index.TARIFF_ON_GENERATOR &&
                Tariff_On_GeneratorStart_CommObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
            {
                tariff_index = Convert.ToByte(((Class_1)Tariff_On_GeneratorStart_CommObj).Value);
            }

            return tariff_index;
        }

        private TimeSpan DECODE_Generator_Start_Monitoring_Time()
        {
            try
            {
                Base_Class MT_Generator_Start_Comm_Obj = Get_Param(Get_Index.MonitoringTime_GENERATOR_START, 0);

                Class_3 MonitoringTime_Start_Generator_MT = (Class_3)MT_Generator_Start_Comm_Obj;
                return Param_Monitoring_Time.UpdateMTObject(Convert.ToDouble(MonitoringTime_Start_Generator_MT.Value));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Decode Generator_Start ", ex); ;
            }

        }

        public Data_Access_Result SET_Generator_Start_Monitoring_Time(Param_Generator_Start Generator_Start)
        {
            try
            {
                Base_Class Generator_Start_MT_obj = Generator_Start.Encode_Generator_Start_MonitorTime(GetSAPEntry);
                Data_Access_Result result = SET_Param(Generator_Start_MT_obj);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Setting/Encoding GeneratorStart Monitoring Time", ex);
            }
        }

        public Data_Access_Result SET_Generator_Start_Tariff(Param_Generator_Start Generator_Start)
        {
            try
            {
                Base_Class Generator_Start_Tariff_obj = Generator_Start.Encode_Generator_Start_Tariff(GetSAPEntry);
                Data_Access_Result result = SET_Param(Generator_Start_Tariff_obj);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Setting/Encoding GeneratorStart Tariff", ex);
            }
        }

        #endregion

        #region HDLC Setup

        public ushort GetHDLCAddress()
        {
            try
            {
                Class_23 RAW_St = (Class_23)GetSAPEntry(Get_Index.HDLC_Setup);

                RAW_St.DecodingAttribute = 09;

                Get_Param(RAW_St);
                return RAW_St.DeviceAddress;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_HDLC_Address", ex);
            }
        }

        public Data_Access_Result SetHDLCAddress(ushort deviceAddress)
        {
            try
            {
                Class_23 RAW_St = (Class_23)GetSAPEntry(Get_Index.HDLC_Setup);
                RAW_St.EncodingAttribute = 09;
                RAW_St.DeviceAddress = deviceAddress;
                return SET_Param(RAW_St);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_HDLC_Address", ex);
            }
        }

        public Data_Access_Result SetInactivityTime(ushort inactivityTimeOut)
        {
            try
            {
                Class_23 RAW_St = (Class_23)GetSAPEntry(Get_Index.HDLC_Setup);
                RAW_St.EncodingAttribute = 08;
                RAW_St.InActivityTimeOut = inactivityTimeOut;
                return SET_Param(RAW_St);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_InactivityTimeOut", ex);
            }
        }

        public Class_23 GetHDLCSetup()
        {
            try
            {
                Class_23 RAW_St = (Class_23)GetSAPEntry(Get_Index.HDLC_Setup);
                RAW_St.DecodingAttribute = 00;
                Get_Param(RAW_St);
                return RAW_St;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_InactivityTimeOut", ex);
            }
        }

        public void Get_HDLCSetup(Param_HDLCSetup hdlcSetup)
        {
            try
            {
                Class_23 RAW_St = this.GetHDLCSetup();

                // Assign Data Properties
                // Param_HDLCSetup Object
                hdlcSetup.Comm_Speed = RAW_St.Comm_Speed;
                hdlcSetup.MaxInfoBufSizeTransmit = RAW_St.MaxInfoFieldLengthTransmit;
                hdlcSetup.MaxInfoBufSizeReceive = RAW_St.MaxInfoFieldLengthReceive;

                hdlcSetup.ReceiveWinSize = RAW_St.WindowSizeReceive;
                hdlcSetup.TransmitWinSize = RAW_St.WindowSizeTransmit;

                hdlcSetup.DeviceAddress = RAW_St.DeviceAddress;

                hdlcSetup.InterOctetInactivityTimeout = TimeSpan.FromMilliseconds(RAW_St.InterOctetTimeOut);
                hdlcSetup.InactivityTimeout = TimeSpan.FromMilliseconds(RAW_St.InActivityTimeOut);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Parameter HDLC Setup", ex);
            }
        }

        #endregion

        #region DateTime Synchronization Methods

        public Action_Result AdjustTimeToQuarter()
        {
            try
            {
                Base_Class Method_Commobj = GetSAPEntry(Get_Index.Meter_Clock);
                ((Class_8)Method_Commobj).MethodInvokeId = Class_8.ADJUST_TO_QUARTER;
                Method_Commobj.Method_ParametersFlag = true;
                return Invoke_Param(Method_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        public Action_Result AdjustTimeToMeasuringPeriod()
        {
            try
            {
                Base_Class Method_Commobj = GetSAPEntry(Get_Index.Meter_Clock);
                ((Class_8)Method_Commobj).MethodInvokeId = Class_8.ADJUST_TO_MEASURING_PERIOD;
                Method_Commobj.Method_ParametersFlag = true;
                return Invoke_Param(Method_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        public Action_Result AdjustTimeToMinute()
        {
            try
            {
                Base_Class Method_Commobj = GetSAPEntry(Get_Index.Meter_Clock);
                ((Class_8)Method_Commobj).MethodInvokeId = Class_8.ADJUST_TO_MINUTE;
                Method_Commobj.Method_ParametersFlag = true;
                return Invoke_Param(Method_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        public Action_Result AdjustTimeToPresetTime()
        {
            try
            {
                Base_Class Method_Commobj = GetSAPEntry(Get_Index.Meter_Clock);
                ((Class_8)Method_Commobj).MethodInvokeId = Class_8.ADJUST_TO_PRESET_TIME;
                Method_Commobj.Method_ParametersFlag = true;
                return Invoke_Param(Method_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        public Action_Result PresetAdjustingTime(DateTime presetTime, DateTime validityIntervalStart, DateTime validityIntervalEnd)
        {
            try
            {
                Class_8 Method_Commobj = (Class_8)GetSAPEntry(Get_Index.Meter_Clock);
                Method_Commobj.MethodInvokeId = Class_8.PRESET_ADJUSTING_TIME;
                Method_Commobj.Date_Time_Value = new StDateTime();
                Method_Commobj.Date_Time_Value.SetDateTime(presetTime);

                Method_Commobj.ValidityIntervalEnd = new StDateTime();
                Method_Commobj.ValidityIntervalEnd.SetDateTime(validityIntervalEnd);

                Method_Commobj.ValidityIntervalStart = new StDateTime();
                Method_Commobj.ValidityIntervalStart.SetDateTime(validityIntervalStart);

                Method_Commobj.Method_ParametersFlag = true;

                return Invoke_Param(Method_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        public Action_Result ShiftTime(short nSeconds)
        {
            try
            {
                Base_Class Method_Commobj = GetSAPEntry(Get_Index.Meter_Clock);
                ((Class_8)Method_Commobj).MethodInvokeId = Class_8.SHIFT_TIME;
                ((Class_8)Method_Commobj).SecondsToShift = nSeconds;
                Method_Commobj.Method_ParametersFlag = true;
                return Invoke_Param(Method_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        #endregion

        public St_FirmwareInfo GetStFirmwareInfo()
        {
            byte[] byte_Array = new byte[68];
            St_FirmwareInfo info_firmware = new St_FirmwareInfo();

            byte_Array = GETArray_Any(Get_Index.Firmware_ID_2, 2);
            info_firmware.Decode_St_FirmwareInfo(byte_Array, 0, byte_Array.Length);
            return info_firmware;
        }

        public Data_Access_Result SET_any_class1(double value, Get_Index Get_any_arg, DataTypes arg_DataTypes, byte encoding_attribute)
        {
            try
            {
                Param_Limits Over_Load_Total_T1_Limit = new Param_Limits();
                Base_Class Over_Load_Total_T1_Limit_obj = Over_Load_Total_T1_Limit.
                    Encode_any_class1(GetSAPEntry, Get_any_arg, encoding_attribute, value, arg_DataTypes);
                return SET_Param(Over_Load_Total_T1_Limit_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting obj_class1", ex);
            }
        }

        #region EventCautions

        public Data_Access_Result SET_EventsCaution(List<Param_EventsCaution> ObjEventCaution)
        {
            try
            {
                Param_EventsCaution[] List_EventsCautions_toSET = new Param_EventsCaution[ObjEventCaution.Count];
                int i = 0;
                for (i = 0; i < List_EventsCautions_toSET.Count(); i++)
                {
                    List_EventsCautions_toSET[i] = new Param_EventsCaution();
                }
                foreach (Param_EventsCaution item in ObjEventCaution)
                {
                    try
                    {
                        if (item.EventId != null)
                        {
                            int index = (int)item.EventId.Value;
                            index--;
                            i = item.Event_Code;
                            if (index >= 0 && index < List_EventsCautions_toSET.Length)
                            {
                                List_EventsCautions_toSET[index].eventCounter = item.eventCounter;
                                List_EventsCautions_toSET[index].Event_Name = item.Event_Name;
                                List_EventsCautions_toSET[index].EventId = item.EventId;
                                List_EventsCautions_toSET[index].Event_Code = item.Event_Code;
                                List_EventsCautions_toSET[index].CautionNumber = item.CautionNumber;
                                List_EventsCautions_toSET[index].Flag = item.Flag;
                                List_EventsCautions_toSET[index].FlashTime = item.FlashTime;

                            }
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                Data_Access_Result result = SET_EventsCaution(List_EventsCautions_toSET);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting EventsCaution", ex);
            }
        }

        public Data_Access_Result SET_EventsCaution(Param_EventsCaution[] ObjEventsCaution)
        {
            try
            {
                if (ObjEventsCaution == null || Array.TrueForAll<Param_EventsCaution>(ObjEventsCaution, (x) => x == null))
                    throw new Exception("Error occurred while setting Param_IP_Profiles,Invalid argument passed");
                ///Write Param Event Cautions
                Base_Class EventsCaution_CommObj = GetSAPEntry(Get_Index.EventsCautions_Caution_Number);
                EventsCaution_CommObj.EncodingAttribute = 2;
                ((Class_1)EventsCaution_CommObj).Value_Array = ObjEventsCaution;
                return SET_Param(EventsCaution_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting EventsCaution", ex);
            }
        }

        #endregion

        #region Customer_reference_#

        public void GET_Customer_Reference(ref Param_Customer_Code custRefCode)
        {
            try
            {
                Base_Class Cust_Ref_Num = Get_Param(Get_Index.Customer_Reference_No, 2);
                custRefCode.Decode_Customer_Reference(Cust_Ref_Num);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Customer_Reference", ex);
            }
        }

        public void GET_Customer_Name(ref Param_Customer_Code custRefCode)
        {
            try
            {
                Base_Class Cust = Get_Param(Get_Index.Customer_Name, 2);
                custRefCode.Decode_Customer_Name(Cust);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Customer_Reference", ex);
            }
        }

        public void GET_Customer_Address(ref Param_Customer_Code custRefCode)
        {
            try
            {
                Base_Class Cust = Get_Param(Get_Index.Customer_Address, 2);
                custRefCode.Decode_Customer_Address(Cust);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Customer_Reference", ex);
            }
        }

        public Data_Access_Result SET_Customer_Reference(Param_Customer_Code custRefCode)
        {
            try
            {
                Base_Class Cust_ref_obj = custRefCode.Encode_Customer_Reference(GetSAPEntry);
                return SET_Param(Cust_ref_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Customer_Reference", ex);
            }
        }

        //By Azeem 4.8.31
        public Data_Access_Result SET_Customer_Name(Param_Customer_Code custRefCode)
        {
            try
            {
                Base_Class Cust_ref_obj = custRefCode.Encode_Customer_Name(GetSAPEntry);
                return SET_Param(Cust_ref_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Customer_Reference", ex);
            }
        }

        public Data_Access_Result SET_Customer_Address(Param_Customer_Code custRefCode)
        {
            try
            {
                Base_Class Cust_ref_obj = custRefCode.Encode_Customer_Address(GetSAPEntry);
                return SET_Param(Cust_ref_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Customer_Reference", ex);
            }
        }

        public Data_Access_Result SET_Read_Raw_Data(ReadRawData raw_Data)
        {
            try
            {
                Base_Class raw_Data_obj = raw_Data.Encode_ReadRawData(GetSAPEntry);
                return SET_Param(raw_Data_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Customer_Reference", ex);
            }
        }
        #endregion

        public void Get_DisplayWindow_Test(ref DisplayWindows DisplayWindowTest)
        {
            try
            {
                Class_1 DisplayWindowTest_Obj = (Class_1)GetSAPEntry(Get_Index.Diplay_Windows_TestMode_GetSet);
                St_DisplayWindows DispWindows = new St_DisplayWindows();
                DisplayWindowTest_Obj.DecodingAttribute = 0x02;
                DisplayWindowTest_Obj.Value_Obj = DispWindows;
                Get_Param(DisplayWindowTest_Obj);
                ///Decoding Display_Windows_Alternate Data Received
                if (DisplayWindowTest_Obj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    if (DisplayWindowsHelper_Obj == null)
                        DisplayWindowsHelper_Obj = new DisplayWindowsHelper();
                    if (DisplayWindowsHelper_Obj.SelectedWindows == null || DisplayWindowsHelper_Obj.SelectedWindows.Count <= 0)
                        DisplayWindowsHelper_Obj.SelectedWindows = DisplayWindowsHelper_Obj.LoadSelectableDisplayWindows(Configurations);
                    DisplayWindowsHelper.InitialzeDisplayWindowsHelper(AP_Controller, configurations, ref DisplayWindows);
                    DisplayWindowTest = DisplayWindowsHelper_Obj.Decode_DisplayWindows(DispWindows);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Display Windows Alternate", ex);
            }
            finally
            {
                if (DisplayWindowTest != null)
                    DisplayWindowTest.WindowsMode = DispalyWindowsModes.Test;
            }
        }

        public void GET_Limit_Demand_OverLoad(ref Param_Limit_Demand_OverLoad Param_Arg, StOBISCode OBIS_Limits_Demand_Over_Load)
        {
            try
            {
                Base_Class Param_limit_demandOverload_CommObj = GetSAPEntry(OBIS_Limits_Demand_Over_Load);

                // Param Limit Demand Over Load
                // FIX Monitering Time Object
                (Param_limit_demandOverload_CommObj as Class_21).MonitoredValue =
                              new MonitoredValue()
                              {
                                  LogicalName = Get_Index.Active_Power_Total_Pos,
                                  AttributeIndex = 0x02,
                                  TargetDataType = DataTypes._A06_double_long_unsigned
                              };
                Param_limit_demandOverload_CommObj.DecodingAttribute = 2;
                (Param_limit_demandOverload_CommObj as Class_21).InitializeThresholds(null, 01);

                if (Param_Arg == null)
                    Param_Arg = new Param_Limit_Demand_OverLoad();

                Get_Param(Param_limit_demandOverload_CommObj);

                if (Param_limit_demandOverload_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    Param_Arg.Threshold = Convert.ToUInt32(((Class_21)Param_limit_demandOverload_CommObj).Thresholds[0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param Limit Demand OverLoad", ex);
            }
        }

        public void GET_Limit_Demand_OverLoad_T1(ref Param_Limit_Demand_OverLoad Limit_Demand_OverLoadArg)
        {
            GET_Limit_Demand_OverLoad(ref Limit_Demand_OverLoadArg, Get_Index.Limits_Demand_Over_Load_T1);
            Limit_Demand_OverLoadArg.Threshold = Limit_Demand_OverLoadArg.Threshold / 1000;
        }

        public void GET_Limit_Demand_OverLoad_T2(ref Param_Limit_Demand_OverLoad Limit_Demand_OverLoadArg)
        {
            GET_Limit_Demand_OverLoad(ref Limit_Demand_OverLoadArg, Get_Index.Limits_Demand_Over_Load_T2);
            Limit_Demand_OverLoadArg.Threshold = Limit_Demand_OverLoadArg.Threshold / 1000;
        }

        public void GET_Limit_Demand_OverLoad_T3(ref Param_Limit_Demand_OverLoad Limit_Demand_OverLoadArg)
        {
            GET_Limit_Demand_OverLoad(ref Limit_Demand_OverLoadArg, Get_Index.Limits_Demand_Over_Load_T3);
            Limit_Demand_OverLoadArg.Threshold = Limit_Demand_OverLoadArg.Threshold / 1000;
        }

        public void GET_Limit_Demand_OverLoad_T4(ref Param_Limit_Demand_OverLoad Limit_Demand_OverLoadArg)
        {
            GET_Limit_Demand_OverLoad(ref Limit_Demand_OverLoadArg, Get_Index.Limits_Demand_Over_Load_T4);
            Limit_Demand_OverLoadArg.Threshold = Limit_Demand_OverLoadArg.Threshold / 1000;
        }

        public Data_Access_Result SET_ContactorParams(Param_Contactor ObjContactorParams)
        {
            try
            {
                Base_Class Contactor_Parameters_Commobj = GetSAPEntry(Get_Index.ContactorParameters);
                ((Class_1)Contactor_Parameters_Commobj).Value_Obj = ObjContactorParams;
                Contactor_Parameters_Commobj.EncodingAttribute = 0x02;
                ObjContactorParams.contactor_read_Status = null;
                return SET_Param(Contactor_Parameters_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Contactor", ex);
            }
        }

        public bool SET_Meter_Clock_MISC(Param_Clock_Caliberation MeterClockParam)
        {
            try
            {
                //SET_MeterClock_Date_Time(MeterClockParam);
                //SET_MeterClock_DLS_Enable(MeterClockParam);
                //SET_MeterClock_DLS_BEGIN_Date_Time(MeterClockParam);
                //SET_MeterClock_DLS_END_Date_Time(MeterClockParam);
                //SET_MeterClock_DLS_Deviation(MeterClockParam);

                if (SET_Clock_Calib_Flags(MeterClockParam) == Data_Access_Result.Success)
                {
                    if (SET_Clock_Calib_PPM(MeterClockParam) == Data_Access_Result.Success)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting SET_Meter Clock", ex);
            }
        }

        public void GET_Clock_Calib_Flags(ref Param_Clock_Caliberation meterClockParam)
        {
            try
            {
                Base_Class ClockCalib_CommObj = Get_Param(Get_Index.Clock_Caliberation_Flags, 2);
                meterClockParam.Decode_Clock_Calib_Flags(ClockCalib_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Clock_Calib_Flags", ex);
            }
        }
        public Data_Access_Result SET_Clock_Calib_Flags(Param_Clock_Caliberation meterClockParam)
        {
            try
            {
                Base_Class meterClock_CommObj = meterClockParam.Encode_Clock_Calib_Flags(GetSAPEntry);
                return SET_Param(meterClock_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting SET_Clock_Calib_Flags", ex);
            }
        }

        public void GET_Clock_Calib_PPM(ref Param_Clock_Caliberation meterClockParam)
        {
            try
            {
                Base_Class ClockCalib_CommObj = Get_Param(Get_Index.Clock_Caliberation, 2);
                meterClockParam.Decode_Clock_Calib_PPM(ClockCalib_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Clock_Calib_PPM", ex);
            }
        }
        public Data_Access_Result SET_Clock_Calib_PPM(Param_Clock_Caliberation meterClockParam)
        {
            try
            {
                Base_Class meterClock_CommObj = meterClockParam.Encode_Clock_Calib_PPM(GetSAPEntry);
                return SET_Param(meterClock_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting SET_Clock_Calib_PPM", ex);
            }
        }

        public Data_Access_Result SET_Meter_Clock(Param_Clock_Caliberation MeterClockParam)
        {
            try
            {
                return SET_MeterClock_Date_Time(MeterClockParam);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting SET_Meter Clock", ex);
            }
        }

        #region InstantaneousMDI

        public void GET_Instantaneous_MDI(ref InstantaneousMDI obj_InstantaneousMDI)
        {
            try
            {
                // Base_Class InstantaneousMDI_obj = GetSAPEntry(Get_Index._IP_Profile);
                Base_Class InstantaneousMDI_obj = GetSAPEntry(Get_Index.Instantaneous_MDI);
                ((Class_1)InstantaneousMDI_obj).Value_Obj = obj_InstantaneousMDI;
                InstantaneousMDI_obj.DecodingAttribute = 0x02;
                Get_Param(InstantaneousMDI_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Instantaneous_MDI", ex);
            }
        }

        #endregion

        #region Single_Phase_EventErrorDetails

        public void Get_EventDetails(ref Param_ErrorDetail Param_Error_Details)
        {
            try
            {
                Base_Class EventErrorDetails_obj = Get_Param(Get_Index.Event_Detail_SP, 2);
                byte[] array = (byte[])((Class_1)EventErrorDetails_obj).Value_Array;
                Param_Error_Details.Text_EventDEtail = Encoding.ASCII.GetString(array);

                byte b = array[0];
                Param_Error_Details.Check_BRE = Convert.ToBoolean(b & 0x01);
                Param_Error_Details.Check_CMMDI = Convert.ToBoolean(b & 0x02);
                Param_Error_Details.Check_RFU1 = Convert.ToBoolean(b & 0x04);
                Param_Error_Details.Check_RFU2 = Convert.ToBoolean(b & 0x08);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading Event Details," + ex.Message, ex);
            }
        }

        #endregion

        #region Setting_Passwords

        public void Set_Passwords(Param_Password Password_Obj)
        {
            try
            {
                Set_CurrentAssociation1(Password_Obj.Management_Device);
                Set_CurrentAssociation2(Password_Obj.Electrical_Device);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Set_CurrentAssociation1(string password)
        {
            try
            {
                if (String.IsNullOrEmpty(password) || password.Length >= 30)
                    throw new Exception("Invalid password string to set for Association1 Logical Device");
                Class_15 Management_Comm_Obj = (Class_15)GetSAPEntry(Get_Index.Current_Association1);
                Management_Comm_Obj.Password = password;
                Management_Comm_Obj.EncodingAttribute = 0x07;
                SET_Param(Management_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Device Password", ex);
            }
        }

        public void Set_CurrentAssociation2(string password)
        {
            try
            {
                if (String.IsNullOrEmpty(password) || password.Length >= 30)
                    throw new Exception("Invalid password string to set for Association1 Logical Device");
                Class_15 Management_Comm_Obj = (Class_15)GetSAPEntry(Get_Index.Current_Association2);
                Management_Comm_Obj.Password = password;
                Management_Comm_Obj.EncodingAttribute = 0x07;
                SET_Param(Management_Comm_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Device Password", ex);
            }
        }

        #endregion

        #region DisplayWindowPowerDown

        public Data_Access_Result SET_Display_PowerDown(Param_Display_PowerDown obj_DispWindow)
        {
            try
            {
                Base_Class com_Obj_DispWindow = obj_DispWindow.Encode_Param_Display_PowerDown(GetSAPEntry);
                ///Load Param_TimeBaseEvents
                return SET_Param(com_Obj_DispWindow);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Display_PowerDown", ex);
            }
        }

        #endregion

        #region Param_KEY_Management
        public Action_Result Set_SecurityKey(byte[] keyTowrite, KEY_ID keyType)
        {
            try
            {
                //keyTowritebyte[] keyTowrite = DLMS_Common.PrintableStringToByteArray(key);

                if (AP_Controller.Crypto == null) AP_Controller.Crypto = new AESGCM();

                keyTowrite = AP_Controller.Crypto.WrapKey(keyTowrite, AP_Controller.Security_Data.MasterEncryptionKey.Value.ToArray());

                List<Key> KeysToTransfer = new List<Key>();
                Key _Key = new Key(keyTowrite, keyType);
                KeysToTransfer.Add(_Key);

                Base_Class Parameters_Commobj = GetSAPEntry(Get_Index.Security_Setup_CurrentAA);
                ((Class_64)Parameters_Commobj).MethodInvokeId = Class_64.Global_Key_Transfer;
                Parameters_Commobj.Method_ParametersFlag = true;
                ((Class_64)Parameters_Commobj).Key_Transfer_Arg = KeysToTransfer;
                return Invoke_Param(Parameters_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Security Key:" + keyType.ToString(), ex);
            }
        }
        public Action_Result Set_SecurityPolicy(Security_Policy securityPolicy)
        {
            try
            {


                Base_Class Parameters_Commobj = GetSAPEntry(Get_Index.Security_Setup_CurrentAA);
                ((Class_64)Parameters_Commobj).MethodInvokeId = Class_64.Security_Activate;
                Parameters_Commobj.Method_ParametersFlag = true;
                ((Class_64)Parameters_Commobj).SecurityPolicy = securityPolicy;

                return Invoke_Param(Parameters_Commobj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Security Policy", ex);
            }
        }
        #endregion

        public void GET_MDI_Parameters_SP(ref Param_MDI_parameters MDI_Params)
        {
            try
            {
                ///Read Min_Interval_Bw_Resets
                Base_Class MDIParam = Get_Param(Get_Index.MDIParams_Auto_Reset_Date, 0x04); ;
                MDI_Params.Decode_Auto_Reset_Date(MDIParam);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting MDI_Parameters", ex);
            }
        }

        public Data_Access_Result SET_MDI_Parameters_SP(Param_MDI_parameters MDI_Parameter)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                GetSAPEntry CommObjectGetter = new GetSAPEntry(this.GetSAPEntry);
                Base_Class MDI_Params = MDI_Parameter.Encode_Auto_Reset_Date(CommObjectGetter);
                result = SET_Param(MDI_Params);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MDI Parameters", ex);
            }
        }

        public void GET_ActivityCalendar_Complete(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                this.GET_ActivityCalendar_Name(ref ActivityCalendar);
                // this.GET_ActivityCalendar_NamePassive(ref ActivityCalendar); //Azeem
                this.GET_ActivityCalendar_ActivationDateTime(ref ActivityCalendar);

                this.GET_ActivityCalendar_DayProfile(ref ActivityCalendar);
                this.GET_ActivityCalendar_WeekProfile(ref ActivityCalendar);
                this.GET_ActivityCalendar_SeasonProfile(ref ActivityCalendar);
                this.GET_ActivityCalendar_SpecialDay(ref ActivityCalendar);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ActivityCalendar_DayProfile(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ///Day Profile Table Active
                Calendar_CommObj.DecodingAttribute = 0x05;
                Get_Param(Calendar_CommObj);

                ActivityCalendar.ParamDayProfile.Decode_Param_DayProfile(Calendar_CommObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ActivityCalendar_WeekProfile(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ///Week Profile Table Active
                Calendar_CommObj.DecodingAttribute = 0x04;
                Get_Param(Calendar_CommObj);

                //Param_WeeKProfile weakProfile = new Param_WeeKProfile();
                ActivityCalendar.ParamWeekProfile.Decode_Week_Profile(Calendar_CommObj, ActivityCalendar.ParamDayProfile);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ActivityCalendar_SeasonProfile(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ///Season Profile Table Active
                Calendar_CommObj.DecodingAttribute = 0x03;
                Get_Param(Calendar_CommObj);

                //Param_SeasonProfile seasonProfile = new Param_SeasonProfile();
                ActivityCalendar.ParamSeasonProfile.Decode_Season_Profile(Calendar_CommObj, ActivityCalendar.ParamWeekProfile);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ActivityCalendar_Name(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ///Activity Calendar Name
                Calendar_CommObj.DecodingAttribute = 0x02;
                Get_Param(Calendar_CommObj);

                ActivityCalendar.CalendarName = ((Class_20)Calendar_CommObj).CalendarNameActive;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ActivityCalendar_NamePassive(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ///Activity Calendar Name Passive
                if (Calendar_CommObj.IsAttribReadable(0x06))
                {
                    Calendar_CommObj.DecodingAttribute = 0x06;
                    Get_Param(Calendar_CommObj);
                }

                ActivityCalendar.CalendarNamePassive = ((Class_20)Calendar_CommObj).CalendarNamePassive;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ActivityCalendar_ActivationDateTime(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class Calendar_CommObj = GetSAPEntry(Get_Index.Activity_Calendar);
                ///Activity Calendar Activation DateTime
                Calendar_CommObj.DecodingAttribute = 0x0A;
                Get_Param(Calendar_CommObj);

                ActivityCalendar.CalendarstartDate = ((Class_20)Calendar_CommObj).ActivatePassiveCalendarTime;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ActivityCalendar_SpecialDay(ref Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                Base_Class specialDayTable = Get_Param(Get_Index.Special_Days_Table, 0);

                Param_SpecialDay specialDay = new Param_SpecialDay();
                specialDay.Decode_SpecialDay_Profile(specialDayTable, ActivityCalendar.ParamDayProfile);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_ContactorParams(ref Param_Contactor ObjContactorParams)
        {
            try
            {
                Base_Class Contactor_Parameters_Commobj = GetSAPEntry(Get_Index.ContactorParameters);
                ((Class_1)Contactor_Parameters_Commobj).Value_Obj = ObjContactorParams;
                Contactor_Parameters_Commobj.DecodingAttribute = 0x02;
                ObjContactorParams.contactor_read_Status = null;
                Get_Param(Contactor_Parameters_Commobj);
                if (Contactor_Parameters_Commobj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    ObjContactorParams = (Param_Contactor)((Class_1)Contactor_Parameters_Commobj).Value_Obj;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_Contactor_Parameter", ex);
            }
        }

        public void GET_Communication_Profiles(ref Param_Communication_Profile ObjCommunication_Profiles)
        {
            try
            {
                ///Initialize ObjCommunication_Profiles if Not Intialized Yet


                Base_Class CommunicationProfiles_CommObj = GetSAPEntry(Get_Index._Communication_Profile);
                CommunicationProfiles_CommObj.DecodingAttribute = 2;
                ((Class_1)CommunicationProfiles_CommObj).Value_Obj = ObjCommunication_Profiles;
                Get_Param(CommunicationProfiles_CommObj);
                if (CommunicationProfiles_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    ObjCommunication_Profiles = (Param_Communication_Profile)((Class_1)CommunicationProfiles_CommObj).Value_Obj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param_Communication_Profile", ex);
            }
        }

        //public void GET_Status_Word_Map(ref Param_StatusWordMap StatusWordList, StatusWordMapType statusWordMapType)
        //{
        //    try
        //    {
        //        Class_1 GetStatusWord = null;
        //        if (statusWordMapType == StatusWordMapType.StatusWordMap_1)
        //            GetStatusWord = (Class_1)Get_Param(Get_Index.Status_Word_Map, 2);
        //        else if (statusWordMapType == StatusWordMapType.StatusWordMap_2)
        //            GetStatusWord = (Class_1)Get_Param(Get_Index.Status_Word_Map2, 2);
        //        StatusWordList.Decode_Data((byte[])GetStatusWord.Value_Array);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while Getting status_word_map", ex);
        //    }
        //}

        //public Data_Access_Result SET_Status_Word_Map(Param_StatusWordMap StatusWordList, StatusWordMapType STW_Type)
        //{
        //    try
        //    {
        //        Class_1 GetStatusWord = null;
        //        if (STW_Type == StatusWordMapType.StatusWordMap_1)
        //            GetStatusWord = (Class_1)GetSAPEntry(Get_Index.Status_Word_Map);
        //        else if (STW_Type == StatusWordMapType.StatusWordMap_2)
        //            GetStatusWord = (Class_1)GetSAPEntry(Get_Index.Status_Word_Map2);
        //        GetStatusWord.Value_Obj = StatusWordList;
        //        return SET_Param(GetStatusWord);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while setting status_word_map", ex);
        //    }
        //}

        #region Status Word

        public void GET_Status_Word(ref Param_StatusWordMap StatusWordList, StatusWordMapType statusWordMapType)
        {
            try
            {
                GET_Status_Word_Map(ref StatusWordList, statusWordMapType);
                Class_1 GetStatusWord = null;

                if (statusWordMapType == StatusWordMapType.StatusWordMap_1)
                    GetStatusWord = (Class_1)Get_Param(Get_Index.OT_STATUS_WORD_LP1, 2);
                else if (statusWordMapType == StatusWordMapType.StatusWordMap_2)
                    GetStatusWord = (Class_1)Get_Param(Get_Index.OT_STATUS_WORD_LP2, 2);

                BitArray bArray = new BitArray((byte[])GetStatusWord.Value_Array);
                Param_StatusWordMap tempStatusList = new Param_StatusWordMap();
                for (int bitNo = 0; bitNo < bArray.Count; bitNo++)
                {
                    if (bArray[bitNo])
                        tempStatusList.StatusWordList.Add(StatusWordList.StatusWordList[bitNo]);
                }

                StatusWordList = tempStatusList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting status_word", ex);
            }
        }

        public void GET_Status_Word_Map(ref Param_StatusWordMap StatusWordList, StatusWordMapType statusWordMapType)
        {
            try
            {
                Class_1 GetStatusWord = null;

                if (statusWordMapType == StatusWordMapType.StatusWordMap_1)
                    GetStatusWord = (Class_1)Get_Param(Get_Index.Status_Word_Map, 2);
                else if (statusWordMapType == StatusWordMapType.StatusWordMap_2)
                    GetStatusWord = (Class_1)Get_Param(Get_Index.Status_Word_Map2, 2);

                StatusWordList.Decode_Data((byte[])GetStatusWord.Value_Array);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting status_word_map", ex);
            }
        }

        public Data_Access_Result SET_Status_Word_Map(Param_StatusWordMap StatusWordList, StatusWordMapType STW_Type)
        {
            try
            {
                Class_1 SetStatusWord = null;

                if (STW_Type == StatusWordMapType.StatusWordMap_1)
                    SetStatusWord = (Class_1)GetSAPEntry(Get_Index.Status_Word_Map);
                else if (STW_Type == StatusWordMapType.StatusWordMap_2)
                    SetStatusWord = (Class_1)GetSAPEntry(Get_Index.Status_Word_Map2);

                SetStatusWord.EncodingAttribute = 0x02;
                SetStatusWord.Value_Obj = StatusWordList;

                return SET_Param(SetStatusWord);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting status_word_map", ex);
            }
        }

        public List<StatusWord> DecodeStatusWordMap(List<StatusWord> WordMap, String status_word_string )
        {
            List<StatusWord> Word_MapLocal = null;
            byte[] encodeData = Commons.String_to_Hex_array(status_word_string);

            try
            {
                if (WordMap == null || WordMap.Count < 0 || encodeData == null || encodeData.Length <= 0)
                    return Word_MapLocal;

                Word_MapLocal = new List<StatusWord>(WordMap);

                BitArray binaryData = new BitArray(encodeData);
                for (int indexer = 0; indexer < binaryData.Length &&
                                      indexer < Word_MapLocal.Count; indexer++)
                {
                    // Update IsTrigger
                    if (Word_MapLocal[indexer] != null)
                        Word_MapLocal[indexer].IsTrigger = binaryData[indexer];
                }
            }
            catch (Exception ex)
            {
                Word_MapLocal = null;
                // Donot Raise Error
                // throw new Exception("Error occurred while setting status_word_map", ex);
            }

            return Word_MapLocal;
        }

        #endregion

        public Data_Access_Result Set_DisplayWindow_test(DisplayWindows DisplayWindTestObj)
        {
            try
            {
                ///Verify Windows Alternate Data
                Class_1 DisplayWindowTest_Obj = (Class_1)GetSAPEntry(Get_Index.Diplay_Windows_TestMode_GetSet);
                ///Encoding DisplayWindNormalObj
                DisplayWindowsHelper.InitialzeDisplayWindowsHelper(AP_Controller, configurations, ref DisplayWindows);
                St_DisplayWindows DisplayWindowsTest = DisplayWindowsHelper_Obj.Encode_DisplayWindows(DisplayWindTestObj);
                DisplayWindowTest_Obj.Value_Obj = DisplayWindowsTest;
                return SET_Param(DisplayWindowTest_Obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Display Windows Alternate", ex);
            }
        }

        public Data_Access_Result SET_ModemLimitsAndTime(Param_ModemLimitsAndTime ObjModemLimits_Params)
        {
            try
            {
                Base_Class Modem_LimitsAndTime_obj = GetSAPEntry(Get_Index.MODEM_LIMITS_AND_TIME);
                Modem_LimitsAndTime_obj.EncodingAttribute = 0x02;
                Modem_LimitsAndTime_obj.EncodingType = DataTypes._A02_structure;
                ((Class_1)Modem_LimitsAndTime_obj).Value_Obj = ObjModemLimits_Params;              ///Load ModemLimits_Params
                return SET_Param(Modem_LimitsAndTime_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_ModemLimitsAndTime", ex);
            }
        }

        public Data_Access_Result[] SET_MDI_Parameters(Param_MDI_parameters MDI_Parameter)
        {
            try
            {
                Data_Access_Result[] result = new Data_Access_Result[5];
                GetSAPEntry CommObjectGetter = new GetSAPEntry(this.GetSAPEntry);
                Base_Class MDI_Params = MDI_Parameter.Encode_Minimum_Interval_Manual_Reset_and_unit(CommObjectGetter);
                result[0] = SET_Param(MDI_Params);
                MDI_Params = MDI_Parameter.Encode_Roll_slide_count(CommObjectGetter);
                result[1] = SET_Param(MDI_Params);
                MDI_Params = MDI_Parameter.Encode_MDI_Interval(CommObjectGetter);
                result[2] = SET_Param(MDI_Params);
                MDI_Params = MDI_Parameter.Encode_Auto_Reset_Date(CommObjectGetter);
                result[3] = SET_Param(MDI_Params);
                MDI_Params = MDI_Parameter.Encode_MDI_FLAGS(CommObjectGetter);
                result[4] = SET_Param(MDI_Params);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting MDI Parameters", ex);
            }
        }

        public Data_Access_Result SET_Communication_Profiles(Param_Communication_Profile ObjCommunication_Profiles)
        {
            try
            {

                Base_Class CommunicationProfiles_CommObj = GetSAPEntry(Get_Index._Communication_Profile);
                ///CommunicationProfiles_CommObj.EncodingAttribute = 2;
                ((Class_1)CommunicationProfiles_CommObj).Value_Obj = ObjCommunication_Profiles;
                return SET_Param(CommunicationProfiles_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Communication_Profiles", ex);
            }
        }

        public Data_Access_Result SET_Number_Profiles(Param_Number_Profile[] ObjNumber_Profiles)
        {
            try
            {
                if (ObjNumber_Profiles == null || Array.TrueForAll<Param_Number_Profile>(ObjNumber_Profiles, (x) => x == null))
                    throw new Exception("Error occurred while setting Param_Number_Profiles,Invalid argument passed");
                ///Write IP Profiles
                Base_Class Number_Profile_CommObj = GetSAPEntry(Get_Index._Number_Profile);
                Number_Profile_CommObj.EncodingAttribute = 2;
                ((Class_1)Number_Profile_CommObj).Value_Array = ObjNumber_Profiles;
                return SET_Param(Number_Profile_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Param_Number_Profiles", ex);
            }
        }

        //public Data_Access_Result SET_Number_Profiles_AllowedCallers(Param_Standard_Number_Profile[] ObjNumber_Profiles)
        //{
        //    try
        //    {
        //        if (ObjNumber_Profiles == null || Array.TrueForAll<Param_Standard_Number_Profile>(ObjNumber_Profiles, (x) => x == null))
        //            throw new Exception("Error occurred while setting Param_Number_Profiles,Invalid argument passed");
        //        // Write IP Profiles
        //        Class_28 Number_Profile_CommObj = (Class_28)GetSAPEntry(Get_Index.AutoAnswer);
        //        Number_Profile_CommObj.EncodingAttribute = 07;

        //        if (Number_Profile_CommObj.Allowed_AutoAnswerCallers == null)
        //            Number_Profile_CommObj.Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(04);
        //        else
        //            Number_Profile_CommObj.Allowed_AutoAnswerCallers.Clear();

        //        // Loading Number Profile Data
        //        KeyValuePair<AutoAnswerCallerType, byte[]> Num_Prof_Entry;

        //        foreach (var numProf in ObjNumber_Profiles)
        //        {
        //            Num_Prof_Entry = new KeyValuePair<AutoAnswerCallerType, byte[]>(numProf.CallerType, numProf.Number);
        //            Number_Profile_CommObj.Allowed_AutoAnswerCallers.Add(Num_Prof_Entry);
        //        }

        //        return SET_Param(Number_Profile_CommObj);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error occurred while setting Number_Profiles Allowed Callers", ex);
        //    }
        //}

        public Data_Access_Result SET_ActivityCalendarName(Param_ActivityCalendar ActivityCalendar)
        {
            try
            {
                if (ActivityCalendar.CalendarName != null)
                {
                    SET_Param_ActivityCalenar_Name(ActivityCalendar);

                }

                if (ActivityCalendar.ExecuteActivateCalendarAction)
                {
                    Action_Result result = Active_Passive_Calendar();
                    return (Data_Access_Result)result;
                }
                else
                {
                    ///Data_Access_Result paramActivityCalendarName = SET_Param_ActivityCalenar_Name(ActivityCalendar);
                    Data_Access_Result paramActivityCalendarStartDate = SET_Param_ActivityCalenar_ActivePassiveDate(ActivityCalendar);
                    return paramActivityCalendarStartDate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GET_MeterClock(ref Param_Clock_Caliberation MeterClockParam)
        {
            try
            {
                Base_Class MeterClock_CommObj = Get_Param(Get_Index.Meter_Clock, 0);
                MeterClockParam.Decode_Date_Time(MeterClock_CommObj);
                MeterClockParam.Decode_Daylight_Saving_BEGIN_Date_Time(MeterClock_CommObj);
                MeterClockParam.Decode_Daylight_Saving_END_Date_Time(MeterClock_CommObj);
                MeterClockParam.Decode_Daylight_Saving_Enable(MeterClock_CommObj);
                MeterClockParam.Decode_Daylight_Saving_Deviation(MeterClock_CommObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting Meter Clock", ex);
            }
        }

        public void GET_EventsCaution(ref Param_EventsCaution[] ObjEventsCaution)
        {
            try
            {
                ///Initialize ObjIP_Profiles if Not Intialized Yet
                if (ObjEventsCaution == null)
                {
                    ObjEventsCaution = new Param_EventsCaution[1];
                    ObjEventsCaution[0] = new Param_EventsCaution();  ///Atleast Single Param_IP_Profiles
                }
                if (Array.TrueForAll<Param_EventsCaution>(ObjEventsCaution, (x) => x == null))
                {
                    ObjEventsCaution = new Param_EventsCaution[1];
                    ObjEventsCaution[0] = new Param_EventsCaution();  ///Atleast Single Param_IP_Profiles
                }

                ///Read IP Profiles
                Base_Class EventsCaution_CommObj = GetSAPEntry(Get_Index.EventsCautions_Caution_Number);
                EventsCaution_CommObj.DecodingAttribute = 2;
                ((Class_1)EventsCaution_CommObj).Value_Array = ObjEventsCaution;
                Get_Param(EventsCaution_CommObj);
                if (EventsCaution_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    var ev_Cautions = ((Class_1)EventsCaution_CommObj).Value_Array;

                    // Copy Ev_
                    if (ev_Cautions != ObjEventsCaution && ev_Cautions.Length != ObjEventsCaution.Length)
                    {
                        ObjEventsCaution = new Param_EventsCaution[ev_Cautions.Length];

                        int index = 0;
                        foreach (var ICustomeStruct in ev_Cautions)
                        {
                            if (ICustomeStruct is Param_EventsCaution)
                            {
                                ObjEventsCaution[index++] = (Param_EventsCaution)ICustomeStruct;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting Param_EventsCaution", ex);
            }
        }

        public Base_Class Encode_RW_ALL_IN_ONE_string(GetSAPEntry CommObjectGetter, byte[] s)
        {
            Class_1 RW_ALL_IN_ONE = (Class_1)CommObjectGetter.Invoke(Get_Index.RW_ALL_IN_ONE);
            RW_ALL_IN_ONE.EncodingAttribute = 2;
            RW_ALL_IN_ONE.EncodingType = DataTypes._A09_octet_string;
            RW_ALL_IN_ONE.Value_Array = s;
            return RW_ALL_IN_ONE;
        }

        public void GET_PrestTime(ref Param_PresetTime Obj_PresetTime_Params)
        {
            try
            {
                if (Obj_PresetTime_Params == null)
                    throw new Exception("Error occurred while setting Param_PresetTime,Invalid argument passed");
                ///Write IP Profiles
                Base_Class PresetTimeParams_CommObj = GetSAPEntry(Get_Index.Clock_Synchronization_Window);
                PresetTimeParams_CommObj.DecodingAttribute = 2;
                ((Class_1)PresetTimeParams_CommObj).Value_Obj = Obj_PresetTime_Params;
                Get_Param(PresetTimeParams_CommObj);
                Obj_PresetTime_Params = (Param_PresetTime)((Class_1)PresetTimeParams_CommObj).Value_Obj;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting Param_PresetTime", ex);
            }
        }

        #region SET/GET ICC Gate

        public Data_Access_Result SetIcclGate(bool enableIccGate)
        {
            try
            {
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.ICC_GATE);
                RAW_St.EncodingAttribute = 02;
                RAW_St.Value = enableIccGate ? 1 : 0;
                RAW_St.EncodingType = DataTypes._A16_enum;
                return SET_Param(RAW_St);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_ICC_Gateway", ex);
            }
        }

        public bool GetIccGate()
        {
            try
            {
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.ICC_GATE);

                RAW_St.DecodingAttribute = 02;

                Get_Param(RAW_St);
                bool enableIccGate = Convert.ToBoolean(RAW_St.Value);
                return enableIccGate;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_Icc_Gate", ex);
            }
        }

        #endregion

        #region Fusion IO States

        public void GetIOStates(ref Fuison_IO_StateList classObj)
        {
            try
            {
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.CONTROLLER_IO_STATE);

                RAW_St.DecodingAttribute = 02;
                RAW_St.Value_Obj = classObj;

                Get_Param(RAW_St);
                if (RAW_St.Value_Array != null && RAW_St.Value_Array.Length > 0)
                    classObj.Decode_Data((byte[])RAW_St.Value_Array);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_St_EEPRawRead Parameter", ex);
            }
        }
        #endregion

        #region SET/GET Protocol

        public Data_Access_Result SetProtocolGate(byte protocolType)
        {
            try
            {
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.PROTOCOL_GATEWAY);
                RAW_St.EncodingAttribute = 02;
                RAW_St.Value = protocolType;
                RAW_St.EncodingType = DataTypes._A16_enum;
                return SET_Param(RAW_St);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_Protocol_Gateway", ex);
            }
        }

        public Protocol_Gate GetProtocolGate()
        {
            try
            {
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.PROTOCOL_GATEWAY);

                RAW_St.DecodingAttribute = 02;

                Get_Param(RAW_St);
                Protocol_Gate CurrentProtocol = (Protocol_Gate)Enum.Parse(typeof(Protocol_Gate), RAW_St.Value.ToString());
                return CurrentProtocol;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_Protocol_Gate", ex);
            }
        }

        #endregion

        #region Param_StEEPRawRead

        public Data_Access_Result Set_Param_StEEPRawRead(Param_St_EEPRawRead _Param_St_EEPRawRead)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.Get_Set_Raw_Data_address);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A09_octet_string;
                RAW_St.Value_Array = _Param_St_EEPRawRead.Encode_Data();

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Param_St_EEPRawRead Parameter", ex);
            }
        }

        public Param_St_EEPRawRead Get_Param_StEEPRawRead(Param_St_EEPRawRead _Param_St_EEPRawRead = null)
        {
            //null-able Parameter
            if (_Param_St_EEPRawRead == null)
                _Param_St_EEPRawRead = new Param_St_EEPRawRead();

            try
            {
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.Get_Set_Raw_Data_address);
                RAW_St.DecodingAttribute = 02;

                Get_Param(RAW_St);
                if (RAW_St.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    byte[] Dt = (byte[])RAW_St.Value_Array;
                    _Param_St_EEPRawRead.Decode_Data(Dt);
                }

                return _Param_St_EEPRawRead;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading Param_St_EEPRawRead Parameter", ex);
            }

        }

        public byte[] GET_EEPRawData()
        {
            byte[] Raw_Data_Read = null;
            try
            {
                ///Read EEPROM ReadRawData
                Base_Class _EEPRawRead = Get_Param(Get_Index.Read_Raw_data, 0x02);
                if (_EEPRawRead.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    Raw_Data_Read = (byte[])((Class_1)_EEPRawRead).Value_Array;
                }
                return Raw_Data_Read;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading ReadRawData", ex);
            }
        }

        public EEPRawReadData GET_EEPRawData(Param_St_EEPRawRead _Param_St_EEPRawRead)
        {
            EEPRawReadData Raw_Data_Read = new EEPRawReadData();
            try
            {
                Raw_Data_Read.Param_St_EEPRawRead = _Param_St_EEPRawRead;
                Raw_Data_Read.RawData = null;

                //Parameterize Param_St_EEPRawRead
                Data_Access_Result Result = Set_Param_StEEPRawRead(_Param_St_EEPRawRead);
                if (Result == Data_Access_Result.Success)
                {
                    Base_Class _EEPRawRead = Get_Param(Get_Index.Read_Raw_data, 0x02);
                    if (_EEPRawRead.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    {
                        Raw_Data_Read.Capture_Stamp = DateTime.Now;
                        Raw_Data_Read.RawData = (byte[])((Class_1)_EEPRawRead).Value_Array;
                    }
                    else
                    {
                        Raw_Data_Read.Capture_Stamp = DateTime.MinValue;
                        Raw_Data_Read.RawData = null;
                    }
                }
                else
                    throw new Exception("Error occurred while reading ReadRawData: " + Result.ToString());

                return Raw_Data_Read;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading ReadRawData", ex);
            }
        }

        public List<EEPRawReadData> GET_EEPRawData(List<Param_St_EEPRawRead> _Param_St_EEPRawRead_List)
        {
            EEPRawReadData Raw_Data_Read = null;
            List<EEPRawReadData> Raw_Data_ReadAll = new List<EEPRawReadData>();

            foreach (var _Param_St_EEPRawRead in _Param_St_EEPRawRead_List)
            {
                try
                {
                    Raw_Data_Read = new EEPRawReadData();
                    Raw_Data_Read.Param_St_EEPRawRead = _Param_St_EEPRawRead;

                    Raw_Data_Read = GET_EEPRawData(_Param_St_EEPRawRead);
                }
                catch
                { }
                finally
                {
                    if (Raw_Data_Read != null &&
                        Raw_Data_Read.Param_St_EEPRawRead != null)
                        Raw_Data_ReadAll.Add(Raw_Data_Read);
                }

            }

            return Raw_Data_ReadAll;
        }

        #endregion

        #region EnergyMizer Configuration

        //public bool SET_RFChannel(int ch)
        //{
        //    return true;
        //}

        public Data_Access_Result Set_Param_RFChannel(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.RF_CHANNELS);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_RFChannel Parameter", ex);
            }
        }
        public uint GET_Param_RFChannel()
        {
            try
            {
                uint channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.RF_CHANNELS, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_RFChannel", ex);
            }
        }

        public Data_Access_Result Set_Param_Channel_Filter_BW(ulong ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.CHANNEL_FILTER_BW);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A06_double_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Channel_Filter_Bw Parameter", ex);
            }
        }
        public ulong GET_Param_Channel_Filter_BW()
        {
            try
            {
                ulong channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.CHANNEL_FILTER_BW, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt64(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Channel_Filter_BW", ex);
            }
        }

        public Data_Access_Result Set_Param_Transmit_Carrier_Frequency(ulong ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.TRANSMIT_CARRIER_FREQUENCY);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A06_double_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Transmit_Carrier_Frequency Parameter", ex);
            }
        }
        public ulong GET_Param_Transmit_Carrier_Frequency()
        {
            try
            {
                ulong channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.TRANSMIT_CARRIER_FREQUENCY, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt64(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Transmit_Carrier_Frequency", ex);
            }
        }

        public Data_Access_Result Set_Param_Receive_Carrier_Frequency(ulong ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.RECEIVE_CARRIER_FREQUENCY);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A06_double_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Receive_Carrier_Frequency Parameter", ex);
            }
        }
        public ulong GET_Param_Receive_Carrier_Frequency()
        {
            try
            {
                ulong channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.RECEIVE_CARRIER_FREQUENCY, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt64(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Receive_Carrier_Frequency", ex);
            }
        }

        public Data_Access_Result Set_Param_RF_Baud_Rate(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.RF_BAUD_RATE);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A12_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_RF_Baud_Rate Parameter", ex);
            }
        }
        public uint GET_Param_RF_Baud_Rate()
        {
            try
            {
                uint channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.RF_BAUD_RATE, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_RF_Baud_Rate", ex);
            }

        }

        public Data_Access_Result Set_Param_RF_Power(int ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.RF_POWER);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_RF_Baud_Rate Parameter", ex);
            }
        }
        public int GET_Param_RF_Power()
        {
            try
            {
                int channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.RF_POWER, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToInt32(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_RF_Power", ex);
            }
        }

        public Data_Access_Result Set_Param_Packet_Mode(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.PACKET_MODE);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Packet_Mode Parameter", ex);
            }
        }
        public PacketMode GET_Param_Packet_Mode()
        {
            try
            {
                PacketMode channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.PACKET_MODE, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (PacketMode)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Packet_Mode", ex);
            }
        }

        public Data_Access_Result Set_Param_Packet_format(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.PACKET_FORMAT);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Packet_format Parameter", ex);
            }
        }
        public PacketFormat GET_Param_Packet_format()
        {
            try
            {
                PacketFormat channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.PACKET_FORMAT, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (PacketFormat)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Packet_format", ex);
            }
        }

        public Data_Access_Result Set_Param_Packet_length(ushort ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.PACKET_LENGTH);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Packet_length Parameter", ex);
            }
        }
        public ushort GET_Param_Packet_length()
        {
            try
            {
                ushort channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.PACKET_LENGTH, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Packet_length", ex);
            }
        }

        public Data_Access_Result Set_Param_Modulation_Type(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.MODULATION_TYPE);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;  // _A16_enum;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Modulation_Type Parameter", ex);
            }
        }
        public ModulationType GET_Param_Modulation_Type()
        {
            try
            {
                ModulationType channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.MODULATION_TYPE, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (ModulationType)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Modulation_Type", ex);
            }
        }

        public Data_Access_Result Set_Param_Frequency_Deviation(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.FREQUENCY_DEVIATION);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A06_double_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Frequency_Deviation Parameter", ex);
            }
        }
        public uint GET_Param_Frequency_Deviation()
        {
            try
            {
                uint channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.FREQUENCY_DEVIATION, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Frequency_Deviation", ex);
            }
        }

        public Data_Access_Result Set_Param_Receiver_Bandwidth(ulong ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.RECEIVER_BANDWIDTH);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A06_double_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Receiver_Bandwidth Parameter", ex);
            }
        }
        public ulong GET_Param_Receiver_Bandwidth()
        {
            try
            {
                ulong channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.RECEIVER_BANDWIDTH, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt64(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Receiver_Bandwidth", ex);
            }
        }

        public Data_Access_Result Set_Param_Preamble(ushort ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.PREAMBLE);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Preamble Parameter", ex);
            }
        }
        public ushort GET_Param_Preamble()
        {
            try
            {
                ushort channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.PREAMBLE, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Preamble", ex);
            }
        }

        public Data_Access_Result Set_Param_SyncWord(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.SYNC_WORD);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A12_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_SyncWord Parameter", ex);
            }
        }
        public uint GET_Param_SyncWord()
        {
            try
            {
                uint channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.SYNC_WORD, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_SyncWord", ex);
            }
        }

        public Data_Access_Result Set_Param_Data_Whitening(bool ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.DATA_WHITENING);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum; // _A03_boolean;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Data_Whitening  Parameter", ex);
            }
        }
        public bool GET_Param_Data_Whitening()
        {
            try
            {
                bool channels = false;

                Base_Class base_Class = Get_Param(Get_Index.DATA_WHITENING, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToBoolean(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Data_Whitening", ex);
            }
        }

        public Data_Access_Result Set_Param_Packet_Encoding(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.PACKET_ENCODING);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum;  // _A03_boolean;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Packet_Encoding Parameter", ex);
            }
        }
        public Disable_Enable GET_Param_Packet_Encoding()
        {
            try
            {
                Disable_Enable channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.PACKET_ENCODING, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (Disable_Enable)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Packet_Encoding", ex);
            }
        }

        public Data_Access_Result Set_Param_Address_Filtering(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.ADDRESS_FILTERING);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum; // _A03_boolean;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Address_Filtering Parameter", ex);
            }
        }
        public Disable_Enable GET_Param_Address_Filtering()
        {
            try
            {
                Disable_Enable channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.ADDRESS_FILTERING, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (Disable_Enable)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Address_Filtering", ex);
            }
        }

        public Data_Access_Result Set_Param_Node_Address(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.NODE_ADDRESS);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Node_Address Parameter", ex);
            }
        }
        public uint GET_Param_Node_Address()
        {
            try
            {
                uint channels = -0; // false;

                Base_Class base_Class = Get_Param(Get_Index.NODE_ADDRESS, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Node_Address", ex);
            }
        }

        public Data_Access_Result Set_Param_Broadcast_Address(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.BROADCAST_ADDRESS);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Broadcast_Address Parameter", ex);
            }
        }
        public uint GET_Param_Broadcast_Address()
        {
            try
            {
                uint channels = 0;//  false;

                Base_Class base_Class = Get_Param(Get_Index.BROADCAST_ADDRESS, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Broadcast_Address", ex);
            }
        }

        public Data_Access_Result Set_Param_AES_Encryption(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.AES_ENCRYPTION);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum; // _A03_boolean;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_AES_Encryption  Parameter", ex);
            }
        }
        public Disable_Enable GET_Param_AES_Encryption()
        {
            try
            {
                Disable_Enable channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.AES_ENCRYPTION, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (Disable_Enable)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_AES_Encryption", ex);
            }
        }

        public Data_Access_Result Set_Param_AES_Encryption_Key_size(byte[] ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.AES_ENCRYPTION_KEY_SIZE);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A09_octet_string;
                RAW_St.Value_Array = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_AES_Encryption_Key_size Parameter", ex);
            }
        }
        public byte[] GET_Param_AES_Encryption_Key_size()
        {
            try
            {
                byte[] channels = null;

                Base_Class base_Class = Get_Param(Get_Index.AES_ENCRYPTION_KEY_SIZE, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (byte[])((Class_1)base_Class).Value_Array;

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_AES_Encryption_Key_size", ex);
            }
        }

        public Data_Access_Result Set_Param_RF_Command_Delay(ushort ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.RF_COMMAND_DELAY);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_RF_Command_Delay Parameter", ex);
            }
        }
        public ushort GET_Param_RF_Command_Delay()
        {
            try
            {
                ushort channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.RF_COMMAND_DELAY, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_RF_Command_Delay", ex);
            }
        }

        public Data_Access_Result Set_Param_RF_Command_Timeout(ushort ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.RF_COMMAND_TIMEOUT);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_RF_Command_Timeout Parameter", ex);
            }
        }
        public ushort GET_Param_RF_Command_Timeout()
        {
            try
            {
                ushort channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.RF_COMMAND_TIMEOUT, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_RF_Command_Timeout", ex);
            }
        }

        public Data_Access_Result Set_Param_Serial_Number(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.Manufacturing_ID);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A06_double_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Serial_Number Parameter", ex);
            }
        }
        // public uint Get_Param_Serial_Number()
        //{
        //** getting serial number direct 
        //from 'application_Controller.ConnectionController.GetMeterSerialNumber 
        //}

        public Data_Access_Result Set_Param_LCD_Contrast(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.LCD_CONTRAST);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A11_unsigned; // _A12_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_LCD_Contrast Parameter", ex);
            }
        }

        public byte GET_Param_LCD_Contrast()
        {
            try
            {
                byte channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.LCD_CONTRAST, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_LCD_Contrast", ex);
            }
        }
        public Data_Access_Result Set_Param_LDR_Setting(Param_Ldr_Setting param)
        {
            try
            {
                Base_Class base_obj = param.Encode_Data(GetSAPEntry);
                return SET_Param(base_obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while setting Ldr_Param", ex);
            }
        }

        public void Get_Param_LDR_Setting(ref Param_Ldr_Setting param)
        {

            try
            {
                Base_Class base_Class = Get_Param(Get_Index.Param_Ldr_Setting, 0);
                param.Decode_Data(base_Class);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting LDR_Setting", ex);
            }
        }
        public Data_Access_Result Set_Param_USB_Parameters(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.USB_PARAMETERS);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_USB_Parameters Parameter", ex);
            }
        }
        public USB_Parameter GET_Param_USB_Parameters()
        {
            try
            {
                USB_Parameter channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.USB_PARAMETERS, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (USB_Parameter)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_USB_Parameters", ex);
            }
        }

        public Data_Access_Result Set_Param_Cost_Parameters(double ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.COST_PARAMETERS);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A12_long_unsigned; // _A06_double_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Cost_Parameters Parameter", ex);
            }
        }
        public double GET_Param_Cost_Parameters()
        {
            try
            {
                double channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.COST_PARAMETERS, 0x00);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToDouble(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Cost_Parameters", ex);
            }
        }

        public Data_Access_Result Set_Param_Meter_To_Read(byte[] ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.METER_TO_READ);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A09_octet_string;
                RAW_St.Value_Array = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Meter_To_Read Parameter", ex);
            }
        }
        public byte[] GET_Param_Meter_To_Read()
        {
            try
            {
                byte[] channels = null;

                Base_Class base_Class = Get_Param(Get_Index.METER_TO_READ, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (byte[])((Class_1)base_Class).Value_Array;

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Meter_To_Read", ex);
            }
        }

        public Data_Access_Result Set_Param_Meter_Password(byte[] ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.METER_PASSWORD);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A09_octet_string;
                RAW_St.Value_Array = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Meter_Password Parameter", ex);
            }
        }
        public byte[] GET_Param_Meter_Password()
        {
            try
            {
                byte[] channels = null;

                Base_Class base_Class = Get_Param(Get_Index.METER_PASSWORD, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (byte[])((Class_1)base_Class).Value_Array;

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Meter_Password", ex);
            }
        }

        public Data_Access_Result Set_Param_Data_To_read(BitArray ch)
        {
            try
            {
                byte[] paramsToRead = new byte[(int)Math.Ceiling((double)ch.Length / 8)];
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.DATA_TO_READ);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A04_bit_string;
                RAW_St.Value_Array = paramsToRead;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Data_To_read Parameter", ex);
            }
        }
        public BitArray GET_Param_Data_To_read()
        {
            try
            {
                BitArray channels = null;

                Base_Class base_Class = Get_Param(Get_Index.DATA_TO_READ, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = new BitArray((byte[])((Class_1)base_Class).Value_Array);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Data_To_read", ex);
            }
        }

        public Data_Access_Result Set_Param_Buzzer_Settings(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.BUZZER_SETTINGS);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum; // _A03_boolean;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Buzzer_Settings Parameter", ex);
            }
        }
        public Disable_Enable GET_Param_Buzzer_Settings()
        {
            try
            {
                Disable_Enable channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.BUZZER_SETTINGS, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (Disable_Enable)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Buzzer_Settings", ex);
            }
        }

        public Data_Access_Result Set_Param_Read_humidity_setting(bool ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.HUMIDITY);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A12_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Read_humidity_setting Parameter", ex);
            }
        }
        public bool GET_Param_Read_humidity_setting()
        {
            try
            {
                bool channels = false;

                Base_Class base_Class = Get_Param(Get_Index.HUMIDITY, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToBoolean(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Read_humidity_setting", ex);
            }
        }

        public Data_Access_Result Set_Param_Humidity(bool ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.HUMIDITY);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A05_double_long;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Humidity Parameter", ex);
            }
        }
        public bool GET_Param_Humidity()
        {
            try
            {
                bool channels = false;

                Base_Class base_Class = Get_Param(Get_Index.HUMIDITY, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToBoolean(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Humidity", ex);
            }
        }

        public Data_Access_Result Set_Param_Temperature_setting(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.TEMPERATURE_SETTING);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Temperature_setting Parameter", ex);
            }
        }
        public Temperature_Settings GET_Param_Temperature_setting()
        {
            try
            {
                Temperature_Settings channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.TEMPERATURE_SETTING, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (Temperature_Settings)Convert.ToByte(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Temperature_setting", ex);
            }
        }

        public Data_Access_Result Set_Param_Temperature(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_3 RAW_St = (Class_3)GetSAPEntry(Get_Index.TEMPERATURE_SETTING);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A12_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Temperature Parameter", ex);
            }
        }
        public uint GET_Param_Temperature()
        {
            try
            {
                uint channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.TEMPERATURE_SETTING, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_3)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Temperature", ex);
            }
        }

        public Data_Access_Result Set_Param_Wifi_SSID(byte[] ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.WIFI_SSID);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A09_octet_string;// _A02_structure;
                RAW_St.Value_Array = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Wifi_SSID Parameter", ex);
            }
        }
        public byte[] GET_Param_Wifi_SSID()
        {
            try
            {
                byte[] channels = null;

                Base_Class base_Class = Get_Param(Get_Index.WIFI_SSID, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (byte[])((Class_1)base_Class).Value_Array;

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Wifi_SSID", ex);
            }
        }

        public Data_Access_Result Set_Param_Wifi_Username(int ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.WIFI_SSID);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A02_structure;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Wifi_Username Parameter", ex);
            }
        }
        public int GET_Param_Wifi_Username()
        {
            try
            {
                int channels = -1;

                Base_Class base_Class = Get_Param(Get_Index.WIFI_SSID, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Wifi_Username", ex);
            }
        }

        public Data_Access_Result Set_Param_Wifi_Password(int ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.WIFI_SSID);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A02_structure;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Wifi_Password Parameter", ex);
            }
        }
        public int GET_Param_Wifi_Password()
        {
            try
            {
                int channels = -1;

                Base_Class base_Class = Get_Param(Get_Index.WIFI_SSID, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Wifi_Password", ex);
            }
        }

        public Data_Access_Result Set_Param_Settings_Default_Wifi_Mode(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.SETTINGS_DEFAULT_WIFI_MODE);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Settings_Default_Wifi_Mode Parameter", ex);
            }
        }
        public WiFi_Mode GET_Param_Settings_Default_Wifi_Mode()
        {
            try
            {
                WiFi_Mode channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.SETTINGS_DEFAULT_WIFI_MODE, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (WiFi_Mode)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Settings_Default_Wifi_Mode", ex);
            }
        }

        public Data_Access_Result Set_Param_Settings_Wifi_DHCP(bool enable)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_42 RAW_St = (Class_42)GetSAPEntry(Get_Index.IPv4);

                RAW_St.EncodingAttribute = 08;
                RAW_St.flg_Use_DHCP = enable;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Settings_Wifi_DHCP Parameter", ex);
            }
        }
        public bool GET_Param_Settings_Wifi_DHCP()
        {
            try
            {
                bool channels = false;

                Base_Class base_Class = Get_Param(Get_Index.IPv4, 0x08);
                if (base_Class.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                    return ((Class_42)base_Class).flg_Use_DHCP;

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Settings_Wifi_DHCP", ex);
            }
        }

        public Data_Access_Result Set_Param_Settings_IP_Station_or_AP(IPAddress iPAddress)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_42 RAW_St = (Class_42)GetSAPEntry(Get_Index.IPv4);

                RAW_St.EncodingAttribute = 03;
                RAW_St.IP_Address = iPAddress;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Settings_IP_Station_or_AP Parameter", ex);
            }
        }
        public IPAddress GET_Param_Settings_IP_Station_or_AP()
        {
            try
            {
                Base_Class base_Class = Get_Param(Get_Index.IPv4, 0x03);
                if (base_Class.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    return ((Class_42)base_Class).IP_Address;
                else
                    throw new Exception("Error occurred while Getting GET_Param_Settings_IP_Station_or_AP");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Settings_IP_Station_or_AP", ex);
            }
        }

        public Data_Access_Result Set_Param_Settings_IP_gateway_Station_or_AP(IPAddress iPAddress)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_42 RAW_St = (Class_42)GetSAPEntry(Get_Index.IPv4);

                RAW_St.EncodingAttribute = 06;
                RAW_St.IP_Address = iPAddress;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Settings_IP_gateway_Station_or_AP Parameter", ex);
            }
        }
        public IPAddress GET_Param_Settings_IP_gateway_Station_or_AP()
        {
            try
            {
                Base_Class base_Class = Get_Param(Get_Index.IPv4, 0x06);
                if (base_Class.GetAttributeDecodingResult(0x06) == DecodingResult.Ready)
                    return ((Class_42)base_Class).IP_Address;
                else
                    throw new Exception("Error occurred while Getting GET_Param_Settings_IP_gateway_Station_or_AP");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Settings_IP_gateway_Station_or_AP", ex);
            }
        }

        public Data_Access_Result Set_Param_Settings_IP_network_mask_Station_or_AP(IPAddress ipAddress)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_42 RAW_St = (Class_42)GetSAPEntry(Get_Index.IPv4);

                RAW_St.EncodingAttribute = 07;
                RAW_St.IP_Address = ipAddress;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Settings_IP_network_mask_Station_or_AP Parameter", ex);
            }
        }
        public IPAddress GET_Param_Settings_IP_network_mask_Station_or_AP()
        {
            try
            {
                Base_Class base_Class = Get_Param(Get_Index.IPv4, 0x07);
                if (base_Class.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                    return ((Class_42)base_Class).IP_Address;
                else
                    throw new Exception("Error occurred while Getting GET_Param_Settings_IP_network_mask_Station_or_AP");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Settings_IP_network_mask_Station_or_AP", ex);
            }
        }

        public Data_Access_Result Set_Param_Auto_connect_object_IP_Port(List<byte[]> list)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_29 RAW_St = (Class_29)GetSAPEntry(Get_Index.AutoConnect);

                RAW_St.EncodingAttribute = 06;
                RAW_St.Destinations = list;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Auto_connect_object_IP_Port Parameter", ex);
            }
        }
        public List<byte[]> GET_Param_Auto_connect_object_IP_Port()
        {
            try
            {
                Base_Class base_Class = Get_Param(Get_Index.AutoConnect, 0x06);
                if (base_Class.GetAttributeDecodingResult(0x06) == DecodingResult.Ready)
                    return ((Class_29)base_Class).Destinations;
                else
                    throw new Exception("Error occurred while Getting GET_Param_Auto_connect_object_IP_Port");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Auto_connect_object_IP_Port", ex);
            }
        }

        public Data_Access_Result Set_Param_KEEPALIVE_params(int ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.KEEPALIVE_PARAMS);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A02_structure;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_KEEPALIVE_params Parameter", ex);
            }
        }
        public int GET_Param_KEEPALIVE_params()
        {
            try
            {
                int channels = -1;

                Base_Class base_Class = Get_Param(Get_Index.KEEPALIVE_PARAMS, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_KEEPALIVE_params", ex);
            }
        }

        public Data_Access_Result Set_Param_Modem_Limits_And_Time(int ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.MODEM_LIMITS_AND_TIME);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A02_structure;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_MODEM_LIMITS_AND_TIME  Parameter", ex);
            }
        }
        public int GET_Param_Modem_Limits_And_Time()
        {
            try
            {
                int channels = -1;

                Base_Class base_Class = Get_Param(Get_Index.MODEM_LIMITS_AND_TIME, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt16(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Modem_Limits_And_Time", ex);
            }
        }

        public Data_Access_Result Set_Param_Wifi_Modem_Mode(byte ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.WIFI_MODEM_MODE);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A16_enum;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_Wifi_Modem_Mode Parameter", ex);
            }
        }
        public WiFi_Modem_Mode GET_Param_Wifi_Modem_Mode()
        {
            try
            {
                WiFi_Modem_Mode channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.WIFI_MODEM_MODE, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = (WiFi_Modem_Mode)Convert.ToByte(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_Wifi_Modem_Mode", ex);
            }
        }

        public Data_Access_Result Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server(IPAddress ipAddress)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_42 RAW_St = (Class_42)GetSAPEntry(Get_Index.IPv4);

                RAW_St.EncodingAttribute = 02;
                RAW_St.IP_Address = ipAddress;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Parameter", ex);
            }
        }
        public IPAddress GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server()
        {
            try
            {
                Base_Class base_Class = Get_Param(Get_Index.IPv4, 0x02);

                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    return ((Class_42)base_Class).IP_Address;
                else
                    throw new Exception("Error occurred while Getting GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server", ex);
            }
        }

        public Data_Access_Result Set_Param_WiFi_Server_Port(ushort port)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_41 RAW_St = (Class_41)GetSAPEntry(Get_Index.TCP_UDP_Setup);

                RAW_St.EncodingAttribute = 02;
                RAW_St.TCP_UDP_Port = port;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_WiFi_Server_Port Parameter", ex);
            }
        }
        public ushort GET_Param_WiFi_Server_Port()
        {
            try
            {
                Base_Class base_Class = Get_Param(Get_Index.TCP_UDP_Setup, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    return ((Class_41)base_Class).TCP_UDP_Port;
                else
                    throw new Exception("Error occurred while Getting GET_Param_WiFi_Server_Port");

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_WiFi_Server_Port", ex);
            }
        }

        public Data_Access_Result Set_Param_WiFi_Web_Server_Port(uint ch)
        {
            try
            {
                Data_Access_Result result = new Data_Access_Result();
                Class_1 RAW_St = (Class_1)GetSAPEntry(Get_Index.WIFI_WEB_SERVER_PORT);

                RAW_St.EncodingAttribute = 02;
                RAW_St.EncodingType = DataTypes._A12_long_unsigned;
                RAW_St.Value = ch;

                result = SET_Param(RAW_St);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Parameterizing Set_Param_WiFi_Web_Server_Port Parameter", ex);
            }
        }
        public uint GET_Param_WiFi_Web_Server_Port()
        {
            try
            {
                uint channels = 0;

                Base_Class base_Class = Get_Param(Get_Index.WIFI_WEB_SERVER_PORT, 0x02);
                if (base_Class.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                    channels = Convert.ToUInt32(((Class_1)base_Class).Value);

                return channels;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting GET_Param_WiFi_Web_Server_Port", ex);
            }
        }

        #endregion // EnergyMizer Configuration

        #region Optical Port Access
        public Data_Access_Result SET_OpticalPortAccess(Param_OpticalPortAccess portAccess)
        {
            try
            {
                // Write IP Profiles {AutoConnect Mode}
                Class_1 PortAccessObj = (Class_1)GetSAPEntry(Get_Index.Param_Optical_Port_Access);
                PortAccessObj.EncodingAttribute = 02;
                PortAccessObj.Value_Obj = portAccess;

                return SET_Param(PortAccessObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while SET Optical Port Access", ex);
            }
        }
        #endregion

    }
}