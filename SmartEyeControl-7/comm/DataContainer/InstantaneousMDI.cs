using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using SmartEyeControl_7.Controllers;
using DLMS.Comm;

namespace comm
{

    public class InstantaneousMDI : ICustomStructure
    {
        #region DataMembers

        public ushort Time;
        public ushort Time_Elapsed;
        public ushort SlidesCount;
        public byte SlidesCounter;
        public uint RunningKW;
        public uint RunningKVar;
        public uint PreviousKW;
        public uint PrevoiusKVAR;
        public uint[] MDI_SlideKW = new uint[6];
        public uint[] MDI_SlideKVAR = new uint[6];
        public uint TimeLeft;

        #endregion

        #region ICustomStructure Members


        public void Decode_Data(byte[] Data)
        {
            int arrayTraverser = 0;
            Decode_Data(Data, ref arrayTraverser,Data.Length);
        }

        byte[] ICustomStructure.Encode_Data()
        {
            byte[] dummy = new byte[1];
            return dummy;
        }

        public void Decode_Data(byte[] Data, ref int array_traverser,int length)
        {
            try
            {
                byte current_Char = 0;
                current_Char = Data[array_traverser++];
                if (current_Char != (byte)DataTypes._A02_structure ||
                    Data[array_traverser++] != 9)
                    throw new DLMSDecodingException("Invalidnvalid Instantaneous MDI structure received", "Decode_Data ICustomStructure");
                this.Time = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Time_Elapsed = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.TimeLeft = (uint)(this.Time - this.Time_Elapsed);
                this.SlidesCount = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.SlidesCounter = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.RunningKW = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.RunningKVar = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.PreviousKW = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.PrevoiusKVAR = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.RunningKW = this.RunningKW / this.Time;
                this.RunningKVar = this.RunningKVar / this.Time;
              


                if (Data[array_traverser++] == (byte)DataTypes._A01_array && Data[array_traverser++] == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        //i = i;
                        if (Data[array_traverser++] != (byte)DataTypes._A02_structure || Data[array_traverser++] != 2)
                            throw new DLMSDecodingException("Invalid Instantaneous MDI structure received", "Decode_Data ICustomStructure");
                        this.MDI_SlideKW[i] = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                        ///Decode Retry TCP (byte)
                        this.MDI_SlideKVAR[i] = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                        ///Decode Retry UDP (byte)

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding Param_ModemLimtisAndTime", "Decode_Data", ex);
            }
        }
        #endregion


        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region ICustomStructure Members


        void ICustomStructure.Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse,Data.Length);
        }

        void ICustomStructure.Decode_Data(byte[] Data, ref int array_traverse,int length)
        {
            Decode_Data(Data, ref array_traverse,length);
        }

        #endregion


    }
}
