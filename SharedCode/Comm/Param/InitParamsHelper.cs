//using Comm;
using Common;
using Database;
using DLMS;
using ReadSMS_AT_CS20;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Xml.Serialization;


namespace comm
{
    public class InitParamsHelper
    {
        private String _meterString;

        public String MeterString
        {
            get { return (_meterString == null) ? "x" : _meterString; }
            set { _meterString = value; }
        }

        public InitHDLCParam GetDefaultHDLCParams()
        {
            try
            {
                InitHDLCParam hdlcParam = new InitHDLCParam();

                hdlcParam.HDLCAddressLength = _HDLC.AddressLength.Four_Byte;
                hdlcParam.DeviceAddress = 0x0011;    /// Init HDLC Lower Destination Device Address
                hdlcParam.MaxInfoBufSizeTransmit = 0x80;
                hdlcParam.MaxInfoBufSizeReceive = 0x80;
                hdlcParam.TransmitWinSize = 1;
                hdlcParam.ReceiveWinSize = 1;
                hdlcParam.InactivityTimeout = new TimeSpan(0, 0, 0, 20, 0);
                hdlcParam.RequestResponseTimeout = new TimeSpan(0, 0, 0, 15, 0);
                hdlcParam.AvoidInActivityTimeout = true;
                hdlcParam.IsEnableRetrySend = true;

                return hdlcParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitTCPParams GetDefaultTCPParams()
        {
            try
            {
                InitTCPParams initTCPParams = new InitTCPParams();
                initTCPParams.ServerPort = 4059;
                initTCPParams.ServerIP = IPAddress.Any;
                initTCPParams.IsTCPTimeOut = false;
                initTCPParams.TCPIPTimeOut = new TimeSpan(0, 03, 00);
                return initTCPParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitCommuincationParams GetDefaultCommunicationParams()
        {
            try
            {
                InitCommuincationParams initCommParam = new InitCommuincationParams();
                initCommParam.DataReadTimeout = new TimeSpan(0, 1, 0);
                initCommParam.TCPInConnectionTimeout = new TimeSpan(0, 5, 0);
                string[] ComPorts = SerialPort.GetPortNames();
                Array.Sort<String>(ComPorts);
                
                if (ComPorts != null && ComPorts.Length > 0)
                    initCommParam.IRCOMPort = ComPorts[0];
                else
                    //Avoid Excetpional Error
                    initCommParam.IRCOMPort = "COM1";

                initCommParam.IRCOMBaudRate = 4800;

                if (ComPorts != null && ComPorts.Length > 0)
                    initCommParam.ModemCOMPort = ComPorts[ComPorts.Length - 1];
                else
                    initCommParam.ModemCOMPort = "COM1";

                initCommParam.MobileNumber = "00";
                initCommParam.ModemBaudRate = 4800;
                return initCommParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public MeterSAPConfiguration GetDefaultSAPConfiguration()
        //{
        //    try
        //    {
        //        #region Default SAPS
        //        MeterSAPConfiguration configs = new MeterSAPConfiguration();
        //        ///Define SAP Objects
        //        SAP_Object c1 = new SAP_Object();
        //        c1 = new SAP_Object("Public", 0x10);
        //        SAP_Object c2 = new SAP_Object("Management", 0x01);

        //        SAPConfig mSAPMngt = new SAPConfig();
        //        mSAPMngt.DefaultPassword = "accurate";
        //        mSAPMngt.FaceName = "Management";
        //        mSAPMngt.SAP = new SAP_Object("ACCACT34G1000001", 0x1);
        //        SAPConfig mSAPElectricity = new SAPConfig();
        //        mSAPElectricity.DefaultPassword = "accurate";
        //        mSAPElectricity.FaceName = "Electricity";
        //        mSAPElectricity.SAP = new SAP_Object("ACCACT34G2000002", 0x11);

        //        MeterConfig rConfig = new MeterConfig();
        //        rConfig.Model = "R326G";
        //        rConfig.Version = "0.0";
        //        rConfig.ServerSAP.Add(mSAPMngt);
        //        rConfig.ServerSAP.Add(mSAPElectricity);
        //        rConfig.ClientSAP.Add(c1);
        //        rConfig.ClientSAP.Add(c2);

        //        configs.SAPConfig.Add(rConfig);
        //        //configs.HashCode = configs.GetHashCode();

        //        return configs;
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public SMS_Params GetDefaultWakeUpSmsParams()
        {
            try
            {
                SMS_Params defaultSMSParams = new SMS_Params();
                defaultSMSParams.IP = IPAddress.Any;
                ///Define Default IP Params
                String localHostName = Dns.GetHostName();
                IPAddress[] localIPs = Dns.GetHostAddresses(localHostName);
                foreach (var item in localIPs)
                {
                    if (!IPAddress.IsLoopback(item) && item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        defaultSMSParams.IP = item;
                        break;
                    }
                }
                defaultSMSParams.IP_Port = 4059;    ///default TCP DLMS_COSEM Port
                defaultSMSParams.Password = "123456789";
                defaultSMSParams.HDLC_TCP_FLAG1 = false;
                defaultSMSParams.IP_and_Port_FLAG3 = true;
                defaultSMSParams.TCP_UDP = 0x01;
                defaultSMSParams.Use_Backup_IP_FLAG4 = false;
                defaultSMSParams.Password_FLAG0 = true;
                return defaultSMSParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveWakeupSmsParams(SMS_Params WakeUpSmsParams)
        {
            try
            {
                String wkUpSmsParamsFile = String.Format(@"{0}/{1}_WkupSMSParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextWriter WriteFileStream = new StreamWriter(wkUpSmsParamsFile, false))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(SMS_Params));
                    xmlSerl.Serialize(WriteFileStream, WakeUpSmsParams);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SMS_Params LoadWakeupSmsParams()
        {
            try
            {
                String wkUpSmsParamsFile = String.Format(@"{0}/{1}_WkupSMSParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextReader ReadFileStream = new StreamReader(wkUpSmsParamsFile))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(SMS_Params));
                    SMS_Params SmsParams = (SMS_Params)xmlSerl.Deserialize(ReadFileStream);
                    return SmsParams;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveTCPParams(InitTCPParams InitTCPParams)
        {
            try
            {
                String tcpParamsFile = String.Format(@"{0}/{1}_tcpParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextWriter WriteFileStream = new StreamWriter(tcpParamsFile, false))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(InitTCPParams));
                    xmlSerl.Serialize(WriteFileStream, InitTCPParams);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitTCPParams LoadTCPParams()
        {
            try
            {
                String tcpParamsFile = String.Format(@"{0}/{1}_tcpParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextReader ReadFileStream = new StreamReader(tcpParamsFile))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(InitTCPParams));
                    InitTCPParams TcpParams = (InitTCPParams)xmlSerl.Deserialize(ReadFileStream);
                    return TcpParams;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveCommuincationParams(InitCommuincationParams InitCommParams)
        {
            try
            {
                String commParamsFile = String.Format(@"{0}/{1}_commParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextWriter WriteFileStream = new StreamWriter(commParamsFile, false))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(InitCommuincationParams));
                    xmlSerl.Serialize(WriteFileStream, InitCommParams);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitCommuincationParams LoadCommunicationParams()
        {
            try
            {
                String commParamsFile = String.Format(@"{0}/{1}_commParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextReader ReadFileStream = new StreamReader(commParamsFile))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(InitCommuincationParams));
                    InitCommuincationParams CommParams = (InitCommuincationParams)xmlSerl.Deserialize(ReadFileStream);
                    return CommParams;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void SaveMeterSAPConfigurations(MeterSAPConfiguration SAPConfigs)
        //{
        //    try
        //    {
        //        String SAPParamsFile = String.Format(@"{0}/SAPConfigs.xml", App_Common.GetApplicationConfigsDirectory());
        //        using (TextWriter WriteFileStream = new StreamWriter(SAPParamsFile, false))
        //        {
        //            XmlSerializer xmlSerl = new XmlSerializer(typeof(MeterSAPConfiguration));
        //            xmlSerl.Serialize(WriteFileStream, SAPConfigs);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public MeterSAPConfiguration LoadMeterSAPConfigurations()
        //{
        //    try
        //    {
        //        String SAPParamsFile = String.Format(@"{0}/SAPConfigs.xml", App_Common.GetApplicationConfigsDirectory());
        //        using (TextReader ReadFileStream = new StreamReader(SAPParamsFile, false))
        //        {
        //            XmlSerializer xmlSerl = new XmlSerializer(typeof(MeterSAPConfiguration));
        //            MeterSAPConfiguration saps = (MeterSAPConfiguration)xmlSerl.Deserialize(ReadFileStream);
        //            return saps;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public MeterSAPConfiguration LoadMeterSAPConfigurations(Configs Configurator)
        //{
        //    try
        //    {
        //        ConfigsHelper ConfigGenerator = new ConfigsHelper(Configurator);
        //        return ConfigGenerator.GetAllMeterSAPConfiguration();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public void SaveHDLCParams(InitHDLCParam hdlcParams)
        {
            try
            {
                String hdlcParamsFile = String.Format(@"{0}/{1}_hdlcParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextWriter WriteFileStream = new StreamWriter(hdlcParamsFile, false))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(InitHDLCParam));
                    xmlSerl.Serialize(WriteFileStream, hdlcParams);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitHDLCParam LoadHDLCParams()
        {
            try
            {
                String hdlcParamsFile = String.Format(@"{0}/{1}_hdlcParams.xml", Commons_DB.GetApplicationConfigsDirectory(), MeterString);
                using (TextReader ReadFileStream = new StreamReader(hdlcParamsFile))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(InitHDLCParam));
                    InitHDLCParam hdlcParams = (InitHDLCParam)xmlSerl.Deserialize(ReadFileStream);
                    return hdlcParams;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveSAPAccessRights(SAP_Object meterLogicalDevice, SAP_Object clientSAP, List<OBISCodeRights> accessRights)
        {

            try
            {
                if (meterLogicalDevice == null || clientSAP == null)
                    throw new Exception("Invalid SAP Objects Passed");
                String AssociationName = meterLogicalDevice.SAP_Name + "_" + clientSAP.SAP_Name;
                using (TextWriter WriteFileStream = new StreamWriter(Commons_DB.GetAccessRightsFilePath(AssociationName), false))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(List<OBISCodeRights>));
                    xmlSerl.Serialize(WriteFileStream, accessRights);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<OBISCodeRights> LoadSAPAccessRights(SAP_Object meterLogicalDevice, SAP_Object clientSAP)
        {
            try
            {
                if (meterLogicalDevice == null || clientSAP == null)
                    throw new Exception("Invalid SAP Objects Passed");
                String AssociationName = meterLogicalDevice.SAP_Name + "_" + clientSAP.SAP_Name;
                using (TextReader ReaderFileStream = new StreamReader(Commons_DB.GetAccessRightsFilePath(AssociationName)))
                {
                    XmlSerializer xmlSerl = new XmlSerializer(typeof(List<OBISCodeRights>));
                    List<OBISCodeRights> accessRights = (List<OBISCodeRights>)xmlSerl.Deserialize(ReaderFileStream);
                    return accessRights;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
