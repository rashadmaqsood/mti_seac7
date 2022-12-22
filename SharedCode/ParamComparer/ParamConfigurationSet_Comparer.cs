using System.Collections.Generic;
using AccurateOptocomSoftware.comm.Param;
using SmartEyeControl_7.comm;

namespace comm
{
    public class ParamConfigurationSet_Comparer : EqualityComparer<ParamConfigurationSet>
    {
        public List<Params> Comparision_FailedList { get; internal set; }

        public override bool Equals(ParamConfigurationSet Obj_A, ParamConfigurationSet Obj_B)
        {
            bool isMatch = false;
            bool isMatch_All = true;
            Params Comparision_Failed = Params.ParamMonitoringTime;
            Comparision_FailedList = new List<Params>();

            //Compare ParamMonitoringTime
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamMonitoringTime;
                isMatch = new Param_Monitoring_time_EqualityComparer().Equals(Obj_A.ParamMonitoringTime, Obj_B.ParamMonitoringTime);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamLimits
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamLimits;
                isMatch = new Param_Limits_EqualityComparer().Equals(Obj_A.ParamLimits, Obj_B.ParamLimits);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamTariffication
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamTariffication;
                isMatch = new Param_ActivityCalendar_Comparer() { Compare_ActivationDateTime = false }.Equals(Obj_A.ParamTariffication, Obj_B.ParamTariffication);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamMDI
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamMDI;
                isMatch = new Param_MDI_parameters_Comparer().Equals(Obj_A.ParamMDI, Obj_B.ParamMDI);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamLoadProfileChannelInfo
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamLoadProfileChannelInfo;
                LoadProfileChannelInfo_PrgChannel_Comparer LP_Comparer = new LoadProfileChannelInfo_PrgChannel_Comparer();

                //Reference Match
                isMatch = Obj_A.ParamLoadProfileChannelInfo == Obj_B.ParamLoadProfileChannelInfo;

                //Compare Count
                isMatch = Obj_A.ParamLoadProfileChannelInfo.Count == Obj_B.ParamLoadProfileChannelInfo.Count;

                LoadProfileChannelInfo Ch_Info_A = null, Ch_Info_B = null;
                //Compare Each Channel
                //Compare LoadProfile Period
                for (int index = 0; isMatch && index < Obj_A.ParamLoadProfileChannelInfo.Count &&
                                   index < Obj_B.ParamLoadProfileChannelInfo.Count; index++)
                {
                    Ch_Info_A = Obj_A.ParamLoadProfileChannelInfo[index];
                    Ch_Info_B = Obj_B.ParamLoadProfileChannelInfo[index];

                    isMatch = LP_Comparer.Equals(Ch_Info_A, Ch_Info_B);
                    if (!isMatch)
                        break;
                }
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }
            DisplayWindows_Comparer DP_Comparer = new DisplayWindows_Comparer();

            //Compare ParamDisplayWindowsNormal
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamDisplayWindowsNormal;
                isMatch = DP_Comparer.Equals(Obj_A.ParamDisplayWindowsNormal, Obj_B.ParamDisplayWindowsNormal);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamDisplayWindowsAlternate
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamDisplayWindowsAlternate;

                isMatch = DP_Comparer.Equals(Obj_A.ParamDisplayWindowsAlternate, Obj_B.ParamDisplayWindowsAlternate);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamDisplayWindowsTestMode
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamDisplayWindowsTestMode;

                isMatch = DP_Comparer.Equals(Obj_A.ParamDisplayWindowsTestMode, Obj_B.ParamDisplayWindowsTestMode);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamCTPTRatio
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamCTPTRatio;
                isMatch = new Param_CTPT_ratio_Comparer().Equals(Obj_A.ParamCTPTRatio, Obj_B.ParamCTPTRatio);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamDecimalPoint
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamDecimalPoint;
                isMatch = new Param_decimal_point_Comparer().Equals(Obj_A.ParamDecimalPoint, Obj_B.ParamDecimalPoint);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamCustomerReferenceCode
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamCustomerReferenceCode;
                isMatch = new Param_Customer_Code_Comparer().Equals(Obj_A.ParamCustomerReferenceCode, Obj_B.ParamCustomerReferenceCode);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamPassword
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamPassword;
                isMatch = new Param_password_Comparer().Equals(Obj_A.ParamPassword, Obj_B.ParamPassword);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamEnergy
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamEnergy;
                isMatch = new Param_Energy_Parameter_Comparer().Equals(Obj_A.ParamEnergy, Obj_B.ParamEnergy);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamClockCalib
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamClockCalib;
                isMatch = new Param_clock_caliberation_Comparer().Equals(Obj_A.ParamClockCalib, Obj_B.ParamClockCalib);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamContactor
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamContactor;
                isMatch = new Param_Contactor_Comparer().Equals(Obj_A.ParamContactor, Obj_B.ParamContactor);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            Param_TimeBaseEvents_Comparer TBE_Comparer = new Param_TimeBaseEvents_Comparer();
            //Compare ParamTimeBaseEvent
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamTimeBaseEvent;

                isMatch = TBE_Comparer.Equals(Obj_A.ParamTimeBaseEvent_1, Obj_B.ParamTimeBaseEvent_1);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamTimeBaseEvent;

                isMatch = TBE_Comparer.Equals(Obj_A.ParamTimeBaseEvent_2, Obj_B.ParamTimeBaseEvent_2);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamTBPowerFail
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamTBPowerFail;
                isMatch = new TBE_PowerFail_Comparer().Equals(Obj_A.ParamTBPowerFail, Obj_B.ParamTBPowerFail);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamMajorAlarmProfile
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamMajorAlarmProfile;
                isMatch = new Param_MajorAlarmProfile_IsMajorAlarmComparer().Equals(Obj_A.ParamMajorAlarmProfile, Obj_B.ParamMajorAlarmProfile);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamEventsCaution
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamEventsCaution;
                isMatch = new Param_EventsCaution_Comparer().Equals(Obj_A.ParamEventsCaution, Obj_B.ParamEventsCaution);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare Param_DisplayPowerDown
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamDisplayWindowPowerDown;
                isMatch = new Param_DisplayPowerDown_Comparer().Equals(Obj_A.ParamDisplayPowerDown, Obj_B.ParamDisplayPowerDown);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare Param_DisplayPowerDown
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamGeneralProcess;
                isMatch = Obj_A.ParamGeneralProcess.IsSVS == Obj_B.ParamGeneralProcess.IsSVS;
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }


            //Compare ParamIPProfiles
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamIPProfiles;
                isMatch = new Param_IP_Profiles_Comparer().Equals(Obj_A.ParamIPProfiles, Obj_B.ParamIPProfiles);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamWakeUpProfile
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamWakeUpProfile;
                isMatch = new Param_WakeUp_Profile_Comparer().Equals(Obj_A.ParamWakeUpProfile, Obj_B.ParamWakeUpProfile);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamNumberProfile
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamNumberProfile;
                isMatch = new Param_Number_Profile_Comparer().Equals(Obj_A.ParamNumberProfile, Obj_B.ParamNumberProfile);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamCommunicationProfile
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamCommunicationProfile;
                isMatch = new Param_Communication_Profile_Comparer().Equals(Obj_A.ParamCommunicationProfile, Obj_B.ParamCommunicationProfile);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamKeepAliveIP
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamKeepAliveIP;
                isMatch = new Param_Keep_Alive_IP_Comparer().Equals(Obj_A.ParamKeepAliveIP, Obj_B.ParamKeepAliveIP);
            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamModemLimitsAndTime
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamModemLimitsAndTime;
                isMatch = new Param_ModemLimitsAndTime_Comparer().Equals(Obj_A.ParamModemLimitsAndTime, Obj_B.ParamModemLimitsAndTime);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamModemInitialize
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamModemInitialize;
                isMatch = new Param_Modem_Initialize_Comparer().Equals(Obj_A.ParamModemInitialize, Obj_B.ParamModemInitialize);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamModemBasicsNEW
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamModemBasicsNEW;
                isMatch = new Param_ModemBasics_NEW_Comparer().Equals(Obj_A.ParamModemBasicsNEW, Obj_B.ParamModemBasicsNEW);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            //Compare ParamTCPUDP
            try
            {
                isMatch = false;
                Comparision_Failed = Params.ParamTCPUDP;
                isMatch = new Param_TCP_UDP_Comparer().Equals(Obj_A.ParamTCPUDP, Obj_B.ParamTCPUDP);

            }
            catch { }
            finally
            {
                if (!isMatch)
                {
                    isMatch_All = isMatch;
                    Comparision_FailedList.Add(Comparision_Failed);
                }
            }

            return isMatch_All;
        }

        public override int GetHashCode(ParamConfigurationSet obj)
        {
            return obj.GetHashCode();
        }
    }
}
