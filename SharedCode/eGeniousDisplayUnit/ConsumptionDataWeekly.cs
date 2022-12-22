using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLMS;
using DLMS.Comm;

namespace SharedCode.eGeniousDisplayUnit
{
    public class ConsumptionDataWeekly : ICustomStructure
    {
        private const int DaysInWeek = 7;

        public List<ConsumptionData> consumptionDataWeeklyArr = null;

        #region Constructor
        public ConsumptionDataWeekly()
        {
            ReInit();
        }

        public void ReInit()
        {
            if (consumptionDataWeeklyArr == null)
            {
                consumptionDataWeeklyArr = new List<ConsumptionData>(DaysInWeek);
            }

            for (int i = 0 ; i < consumptionDataWeeklyArr.Count ;  i++)
            {
                //if (consumptionDataWeeklyArr[i] == null)
                //    consumptionDataWeeklyArr[i] = new ConsumptionData();
                //else
                    consumptionDataWeeklyArr[i].ReInit();
            }
        }

        #endregion


        #region Decode Data
        public void Decode_Data(byte[] Data)
        {
            int array_index = 0;
            Decode_Data(Data, ref array_index, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                foreach (var item in consumptionDataWeeklyArr)
                {
                    item.Decode_Data(Data, ref array_traverse, length);
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Consumption_Data_Weekly", "Decode_Data_Param_Consumption_Data_Weekly", ex);
            }

        }
        #endregion

        #region Encode Data

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>();
                foreach (var item in consumptionDataWeeklyArr)
                {
                    encodeRaw.AddRange(item.Encode_Data());
                }
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Consumption_Data_Weekly", "Encode_Data_Param_Consumption_Data_Weekly", ex);
            }
        }
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
