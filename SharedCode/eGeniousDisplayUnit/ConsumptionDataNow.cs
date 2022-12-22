using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.eGeniousDisplayUnit
{
    public class ConsumptionDataNow : ICustomStructure
    {
        private const int MaxEntriesNow = 3;

        public List<ConsumptionData> consumptionDataNowArr = null;

        #region Constructor
        public ConsumptionDataNow()
        {
            ReInit();
        }

        public void ReInit()
        {
            if (consumptionDataNowArr == null)
            {
                consumptionDataNowArr = new List<ConsumptionData>(MaxEntriesNow);
            }
            else
                consumptionDataNowArr.Clear();
        }

        #endregion

        public bool AddInArray(ConsumptionData data)
        {
            try
            {
                if (this.consumptionDataNowArr.Count < MaxEntriesNow)
                {
                    if (data != null)
                    {
                        this.consumptionDataNowArr.Add(new ConsumptionData(data));
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                foreach (var item in consumptionDataNowArr)
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
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Consumption_Data_Now", "Decode_Data_Param_Consumption_Data", ex);
            }
        }
        #endregion

        #region Encode Data

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>();
                foreach (var item in consumptionDataNowArr)
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
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Consumption_Data_Now", "Encode_Data_Param_Consumption_Data_Now", ex);
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

    //public class  ConsumptionDataNowList : 
    //{
    //    public ConsumptionData[] consumptionDataNowArr = null;
    //
    //}
}
