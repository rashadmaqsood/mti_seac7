using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.Param
{
    public class GridStatusItem : ICustomStructure
    {
        #region Data_Members
        public Boolean Status;
        public DateTime Time;
        #endregion

        #region Encode Decode
        public byte[] Encode_Data()
        {
            throw new NotImplementedException();
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_Grid_Status Structure received", "Decode_Data_Param_Grid_Status");

                int StructureLength = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                byte[] OctetString;
                Status = Convert.ToBoolean(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                OctetString = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                Time = Convert.ToDateTime(BasicEncodeDecode.Decode_DateTime(OctetString));
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Grid_Input_Status", "Decode_Data_Param_Grid_Input_Status", ex);
            }
        }
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region Static Methods
        public static bool IsStatusChanged(List<GridStatusItem> originalList, List<GridStatusItem> newList)
        {
            int index = 0;
            bool Status = false;
            if (originalList == null || newList == null) return Status;
            
            if (originalList.Count != newList.Count) Status = true;
            else
            {
                Parallel.For(index, originalList.Count, ((x, loopState) =>
                    {
                        if (originalList[x].Status != newList[x].Status) { Status = true; loopState.Stop(); }
                    }));
            }
            return Status;
        }
        #endregion

    }
}
