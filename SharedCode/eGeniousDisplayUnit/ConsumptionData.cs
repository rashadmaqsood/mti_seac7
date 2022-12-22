using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.eGeniousDisplayUnit
{
    public class ConsumptionData : ICustomStructure
    {
        /// <summary>
        /// MaxBytes = 12
        /// </summary>
        private const int MaxBytes = 12;
        const long Base_Date_Time_Ticks = 630822816000000000; //01/01/2000 00:00:00
        uint time = 0;

        public DateTime DateTime
        {
            get
            {
                return new DateTime(Base_Date_Time_Ticks + ((time) * ((Int64)1e7))); //Add base Date Time 01/01/2000 00:00:00
            }
            set
            {
                time = (uint)((value.Ticks - Base_Date_Time_Ticks) / (Int64)1e7); //Substract base Date Time 01/01/2000 00:00:00
            }
        }


        

        public int Energy { get; set; }
        public int Price { get; set; }

        #region Constructor
        public ConsumptionData(ConsumptionData data = null)
        {
            if (data != null)
            {
                this.DateTime = data.DateTime;
                this.Energy = data.Energy;
                this.Price = data.Price;
            }
            else
            {
                ReInit();
            }
        }

        public ConsumptionData(DateTime dt , int energy = 0, int price = 0)
        {
            this.DateTime = dt;
            this.Energy = energy;
            this.Price = price;
        }

        public void ReInit()
        {
            this.DateTime = DateTime.MinValue;
            this.Energy = 0;
            this.Price = 0;
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
                if (Data != null && length >= MaxBytes)
                {
                    this.DateTime = BasicEncodeDecode.Decode_Time(Data);
                    array_traverse += 4;

                    this.Energy = (int)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                    this.Price  = (int)BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                        
                    // array_traverse += 12; // will automatically increment
                }

            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Consumption_Data", "Decode_Data_Param_Consumption_Data", ex);
            }
        }
        #endregion

        #region Encode Data

        public byte[] Encode_Data()
        {
            try
            {
                byte[] encodeRaw = new byte[MaxBytes];
                //encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A19_datetime, this.DateTime));
                //encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A05_double_long, this.Energy));
                //encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A05_double_long, this.Price));

                byte[] tempTimeArr = null;
                tempTimeArr = BitConverter.GetBytes(time);
                //BasicEncodeDecode.Encode_Time(this.DateTime, ref tempTimeArr);
                
                Buffer.BlockCopy(tempTimeArr, 0, encodeRaw, 0, 4);

                var tempEnergyArr = BitConverter.GetBytes(this.Energy);
                Buffer.BlockCopy(tempEnergyArr, 0, encodeRaw, 4, 4);
                
                var tempPriceArr = BitConverter.GetBytes(this.Price);
                Buffer.BlockCopy(tempPriceArr, 0, encodeRaw, 8, 4);
            
                return encodeRaw;
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Consumption_Data", "Encode_Data_Param_Consumption_Data", ex);
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
