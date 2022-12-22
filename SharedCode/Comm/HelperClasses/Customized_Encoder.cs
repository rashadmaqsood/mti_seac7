using System.Text;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.HelperClasses
{
    public static class Customized_Encoder
    {
        public static Base_Class Encode_Any(GetSAPEntry CommObjectGetter, byte Class_id, Get_Index OBIS_code, byte Attribute, DataTypes Encoding_type, double Value, byte Multiplier)
        {
            switch (Class_id)
            {
                case 1:
                    Class_1 temp_class1 = (Class_1)CommObjectGetter.Invoke(OBIS_code);
                    temp_class1.EncodingAttribute = Attribute;
                    temp_class1.EncodingType = Encoding_type;
                    temp_class1.Value = Value * Multiplier;
                    return temp_class1;
                    break;
                case 3:
                    Class_3 temp_class3 = (Class_3)CommObjectGetter.Invoke(OBIS_code);
                    temp_class3.EncodingAttribute = Attribute;
                    temp_class3.EncodingType = Encoding_type;
                    temp_class3.Value = Value * Multiplier;
                    return temp_class3;
                    break;
            }

            return null;
        }
        public static Base_Class Encode_Any(GetSAPEntry CommObjectGetter, byte Class_id, Get_Index OBIS_code, byte Attribute, DataTypes Encoding_type, byte[] Data_Array)
        {
            switch (Class_id)
            {
                case 1:
                    Class_1 temp_class1 = (Class_1)CommObjectGetter.Invoke(OBIS_code);
                    temp_class1.EncodingAttribute = Attribute;
                    temp_class1.EncodingType = Encoding_type;
                    temp_class1.Value_Array = Data_Array;
                    return temp_class1;
                    break;
                case 3:
                    Class_3 temp_class3 = (Class_3)CommObjectGetter.Invoke(OBIS_code);
                    temp_class3.EncodingAttribute = Attribute;
                    temp_class3.EncodingType = Encoding_type;
                    temp_class3.Value_Array = Data_Array;
                    return temp_class3;
                    break;
            }

            return null;
        }
        public static Base_Class Encode_Any(GetSAPEntry CommObjectGetter, byte Class_id, Get_Index OBIS_code, byte Attribute, DataTypes Encoding_type, string ASCII_string)
        {
            switch (Class_id)
            {
                case 1:
                    Class_1 temp_class1 = (Class_1)CommObjectGetter.Invoke(OBIS_code);
                    temp_class1.EncodingAttribute = Attribute;
                    temp_class1.EncodingType = Encoding_type;
                    temp_class1.Value_Array = Encoding.ASCII.GetBytes(ASCII_string);
                    return temp_class1;
                    break;
                case 3:
                    Class_3 temp_class3 = (Class_3)CommObjectGetter.Invoke(OBIS_code);
                    temp_class3.EncodingAttribute = Attribute;
                    temp_class3.EncodingType = Encoding_type;
                    temp_class3.Value_Array = Encoding.ASCII.GetBytes(ASCII_string);
                    return temp_class3;
                    break;
                case 15:
                    Class_15 temp_class15 = (Class_15)CommObjectGetter.Invoke(OBIS_code);
                    temp_class15.EncodingAttribute = Attribute;
                    temp_class15.EncodingType = Encoding_type;
                    temp_class15.Password = ASCII_string;
                    return temp_class15;
                    break;
            }

            return null;
        }
    }
}
