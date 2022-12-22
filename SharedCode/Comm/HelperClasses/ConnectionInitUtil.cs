using System;
using SharedCode.Comm.DataContainer;

namespace SharedCode.Comm.HelperClasses
{
    public class ConnectionInitUtil
    {
        private static Random rand = null;
        private static uint meterSerialIdentifier = 0;

        static ConnectionInitUtil()
        {
            rand = new Random();
        }

        public static string GetSAPTableKey(string typeInfo, string meterLogical, string clientLogical)
        {
            try
            {
                return typeInfo + "_" + meterLogical + "_" + clientLogical;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String GetSAPTableKey(ConnectionInfo ConfInfo)
        {
            try
            {
                String TypeInfo = null, MeterLogical = null, ClientLogical = null, keySAP = null;
                TypeInfo = ConfInfo.MeterInfo.MeterModel;
                MeterLogical = ConfInfo.CurrentMeterSAP.ToString();
                ClientLogical = ConfInfo.CurrentClientSAP.ToString();

                keySAP = GetSAPTableKey(TypeInfo, MeterLogical, ClientLogical);
                return keySAP;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ConnectionInfo GetMeterConnectionInfo(MeterSerialNumber MSN, DatabaseConfiguration.DataSet.Configs LoadedConfigurations)
        {
            try
            {
                // Check Meter Profile Info From Databases against MSN
                ConfigsHelper ConfHelper = new ConfigsHelper(LoadedConfigurations);
                // Load Selected Meter SAP Configurations
                var sapConf = ConfHelper.GetAllMeterConfigurations();
                ConnectionInfo confInfo = new ConnectionInfo();
                confInfo.MeterInfo = sapConf[0];

                // Update Default Password Here
                // confInfo.CurrentSAPPassword = confInfo.CurrentMeterSAP.DefaultPassword;
                // confInfo.MeterSerialNumberObj = MSN;
                return confInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MeterSerialNumber GetRandomSerialNumber()
        {
            try
            {
                MeterSerialNumber Num = new MeterSerialNumber();
                uint Num_T = Convert.ToUInt32(rand.Next(1, Convert.ToInt32(11)));
                unchecked
                {
                    uint num = 0;
                    unchecked
                    {
                        num = (uint)((11) * (100000000))
                            + (uint)((0) * (1000000));
                        lock (rand)
                        {
                            num += (++meterSerialIdentifier);
                        }
                    }
                    Num.MSN = num;
                }
                return Num;
            }
            catch (Exception)
            {
                throw new Exception("Invalid Serial Number to generate");
            }
            finally
            {
                if (meterSerialIdentifier > MeterSerialNumber.HLMeterSerialNo)
                    meterSerialIdentifier = 1;
            }
        }

    }
}
