using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.eGeniousDisplayUnit
{
    public class ConsumptionDataMonthly : ICustomStructure
    {
        private const int MaxMonths = 6;

        public List<ConsumptionData> consumptionDataMonthlyArr = null; //Made Public by Azeem //

        #region Constructor
        public ConsumptionDataMonthly()
        {
            ReInit();
        }

        public void ReInit()
        {
            if (consumptionDataMonthlyArr == null)
            {
                consumptionDataMonthlyArr = new List<ConsumptionData>(MaxMonths);
            }
           
            for (int i = 0; i < consumptionDataMonthlyArr.Count; i++)
            {
                if (consumptionDataMonthlyArr[i] == null)
                    consumptionDataMonthlyArr[i] = new ConsumptionData();
                else
                    consumptionDataMonthlyArr[i].ReInit();
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
                foreach (var item in consumptionDataMonthlyArr)
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
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Consumption_Data_Monthly", "Decode_Data_Param_Consumption_Data_Monthly", ex);
            }
        }
        #endregion

        #region Encode Data

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>();
                foreach (var item in consumptionDataMonthlyArr)
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
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Consumption_Data_Monthly", "Encode_Data_Param_Consumption_Data_Monthly", ex);
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
