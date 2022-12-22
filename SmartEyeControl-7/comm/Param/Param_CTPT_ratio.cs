using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using System.Windows.Forms;
using SmartEyeControl_7.Controllers;
using System.ComponentModel;
using GUI;
using System.Xml.Serialization;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    public struct OBIS_data_from_GUI
    {
        public Get_Index OBIS_code;
        public byte attribute;
        public DataTypes Type;
        public object Data_Array;
        public DataTypes Sub_type;
        public double Data;
        public Data_Access_Result Data_Result;
    }

    [Serializable]
    [XmlInclude(typeof(Param_CTPT_ratio))]
    public class Param_CTPT_ratio : IParam, ICloneable
    {
        [XmlElement("CTRatio_Numerator",typeof(ushort))]
        public ushort CTratio_Numerator;
        [XmlElement("CTRatio_Denominator", typeof(ushort))]
        public ushort CTratio_Denominator;
        [XmlElement("PTRatio_Numerator", typeof(ushort))]
        public ushort PTratio_Numerator;
        [XmlElement("PTRatio_Denominator", typeof(ushort))]
        public ushort PTratio_Denominator;

        #region Encode_CTRatio_Numerator

        public Base_Class Encode_CTRatio_Numerator(GetSAPEntry CommObjectGetter)
        {
            Class_1 CT_Numm = (Class_1)CommObjectGetter.Invoke(Get_Index.CT_Ratio_Numerator);
            CT_Numm.EncodingAttribute = 2;
            CT_Numm.EncodingType = DataTypes._A06_double_long_unsigned;
            CT_Numm.Value = CTratio_Numerator;
            return CT_Numm;
        }

        public void Decode_CTRatio_Numerator(Base_Class arg)
        {
            try
            {
                Class_1 CT_Numm = (Class_1)arg;
                ///Verify data Received/OBIS/ETC
                CTratio_Numerator = Convert.ToUInt16(CT_Numm.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region CTRatio_Denominator

        public Base_Class Encode_CTRatio_Denominator(GetSAPEntry CommObjectGetter)
        {
            Class_1 CT_Denomm = (Class_1)CommObjectGetter.Invoke(Get_Index.CT_Ratio_Denominator);
            CT_Denomm.EncodingAttribute = 2;
            CT_Denomm.EncodingType = DataTypes._A06_double_long_unsigned;
            CT_Denomm.Value = CTratio_Denominator;
            return CT_Denomm;
        }

        public void Decode_CTRatio_Denominator(Base_Class arg)
        {
            try
            {
                Class_1 CT_Denomm = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                CTratio_Denominator = Convert.ToUInt16(CT_Denomm.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region PT_Ratio_Numerator

        public Base_Class Encode_PTRatio_Numerator(GetSAPEntry CommObjectGetter)
        {
            Class_1 CT_Num = (Class_1)CommObjectGetter.Invoke(Get_Index.PT_Ratio_Numerator);
            CT_Num.EncodingAttribute = 2;
            CT_Num.EncodingType = DataTypes._A06_double_long_unsigned;
            CT_Num.Value = PTratio_Numerator;
            return CT_Num;
        }

        public void Decode_PTRatio_Numerator(Base_Class arg)
        {
            try
            {
                Class_1 CT_Numm = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                PTratio_Denominator = Convert.ToUInt16(CT_Numm.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region PT_Ratio_Denumerator

        public Base_Class Encode_PTRatio_Denominator(GetSAPEntry CommObjectGetter)
        {
            Class_1 CT_Num = (Class_1)CommObjectGetter.Invoke(Get_Index.PT_Ratio_Denominator);
            CT_Num.EncodingAttribute = 2;
            CT_Num.EncodingType = DataTypes._A06_double_long_unsigned;
            CT_Num.Value = PTratio_Denominator;
            return CT_Num;
        }

        public void Decode_PTRatio_Denominator(Base_Class arg)
        {
            try
            {
                Class_1 PT_Denomm = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                PTratio_Denominator = Convert.ToUInt16(PT_Denomm.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

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

        public object Clone()
        {
            return MemberwiseClone();
        }

    }

}
