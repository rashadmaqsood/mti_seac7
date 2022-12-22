using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// SAP assignment (class_id: 17, version: 0) allows modeling the logical structure of physical devices.
    /// This class allows modeling the logical structure of the physical devices by providing information on the assignment of the logical devices to their SAP's.
    /// </summary>
    public class Class_17 : Base_Class
    {
        #region DataMembers
        /// <summary>
        /// colloection of SAP_Objects (Service access point for the logical device) used by the ACSE (Assosication control of service element) to create associations.
        /// </summary>
        public List<SAP_Object> Server_Logical_Devices;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_17(Get_Index Index, byte[] Obis_Code, byte No_of_Associations)
            : base(17, 2, 1, Index, Obis_Code, No_of_Associations)
        {

            Server_Logical_Devices = new List<SAP_Object>(1);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_17(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 2, 1)
        {
            Server_Logical_Devices = new List<SAP_Object>(1);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="obj">Class_17 Object</param>
        public Class_17(Class_17 obj)
            : base(obj)
        {
            if (obj.Server_Logical_Devices != null && obj.Server_Logical_Devices.Count > 0)
                this.Server_Logical_Devices = new List<SAP_Object>(obj.Server_Logical_Devices);
            else
                Server_Logical_Devices = new List<SAP_Object>(1);


        }
        #endregion

        
        #region Decoders/Encoders
        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte Server_Logical_Devices_No;
            byte current_char = 0;
            byte[] Obis_code_recieved = null;
            
            try
            {
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Class_17_SAP_Assg_List");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Class_17_SAP_Assg_List");
               
                #region Attribute0x02
                
                //------------------------------------------------------
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!base.IsAttribReadable(02))
                        {
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        }
                        else
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class_17_SAP_Assg_List");
                        }
                    }
                    else
                    {
                        if (current_char != 0x01)
                        {
                            // Generate Error and return
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1}First element should be 01 (array), invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class_17_SAP_Assg_List");
                        }
                        // 2nd byte for number of logical devices
                        Server_Logical_Devices_No = Convert.ToByte(BasicEncodeDecode.Decode_Length(Data,
                            ref array_traverse, Data.Length));
                        // Create space for storing server logical device addresses
                        Server_Logical_Devices = new List<SAP_Object>(Server_Logical_Devices_No);
                        // create space for logical devices name
                        // packet traverse
                        for (UInt16 devices = 0; devices < Server_Logical_Devices_No; )
                        {
                            if ((Data[array_traverse++] != (byte)DataTypes._A02_structure) || (Data[array_traverse++] != 0x02))
                            {
                                throw new DLMSDecodingException(String.Format("{0}_{1} Invalid Structure Received, invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class_17_SAP_Assg_List");
                            }
                            Server_Logical_Devices.Add(new SAP_Object());
                            Server_Logical_Devices[devices]._SAP_Address = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                                ref array_traverse, Data.Length));
                            Server_Logical_Devices[devices].SAP_Name = BasicEncodeDecode.Decode_String(Data, ref array_traverse, Data.Length);
                            devices++;

                        }
                        SetAttributeDecodingResult(2, DecodingResult.Ready);
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("{0}_{1}_{2}_{3} (Error Code:{4})", "Error occurred while decoding data ",
                            OBISIndex, OBISIndex.OBISIndex, "Decode_Class_17_SAP_Assg_List", (int)DLMSErrors.ErrorDecoding_Type), ex);
            }
        }
        #endregion

        #region Member Methods
        public override object Clone()
        {
            Class_17 cloned = new Class_17(this);
            if (Server_Logical_Devices != null && Server_Logical_Devices.Count > 0)
                Server_Logical_Devices.Clear();
            return cloned;
        }
        /// <summary>
        /// Returns the String reprisentation of the Class_4 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            StringBuilder SAPList = new StringBuilder();

            int SAPCount = 0;
            if (this.Server_Logical_Devices != null && this.Server_Logical_Devices.Count > 0 && GetAttributeDecodingResult(2) == DecodingResult.Ready)
            {
                SAPCount = this.Server_Logical_Devices.Count;
                foreach (SAP_Object SAP in this.Server_Logical_Devices)//Append Codes in List & Code Name
                {
                    try
                    {
                        SAPList.AppendFormat(",{0}", SAP.SAP_Name);
                        SAPList.AppendFormat(" {0}", SAP.SAP_Address);
                    }
                    catch (Exception) { }

                }
            }
            strVal.AppendFormat(",Received SAPList Count:{0}:{1}", SAPCount, GetAttributeDecodingResult(2));
            strVal.Append(SAPList.ToString());
            return baseStr + strVal;
        }
        #endregion
    }
}
