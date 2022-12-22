using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using DLMS.Comm;

namespace DLMS
{
    #region Class_7

    /// <summary>
    /// Profile generic (class_id: 7, version: 1) provides a generalized concept allowing to store, sort and access data groups or data series, called capture objects.
    /// Capture objects are appropriate attributes or elements of (an) attribute(s) of COSEM objects. 
    /// The capture objects are collected periodically or occasionally.
    /// A profile has a buffer to store the captured data.
    /// To retrieve only a part of the buffer, either a value range or an entry range may be specified, asking to retrieve all entries that fall within the range specified.
    /// The list of capture objects defines the values to be stored in the buffer (using auto capture or the method capture). 
    /// The list is defined statically to ensure homogeneous buffer entries (all entries have the same size and structure).
    /// If the list of capture objects is modified, the buffer is cleared. If the buffer is captured by other Profile generic objects, their buffer is cleared as well, to guarantee the homogeneity of their buffer entries.
    /// The buffer may be defined as sorted by one of the capture objects, e.g. the clock, or the entries are stacked in a last in first out order.
    /// For example, it is very easy to build a maximum demand register with a one entry deep sorted profile capturing and sorted by a Demand register last_average_value attribute. 
    /// It is just as simple to define a profile retaining the three largest values of some period.
    /// </summary>
    public class Class_7 : Base_Class
    {
        #region Data_Members

        /// <summary>
        /// Collection of DLMS/COSEM Class Objects Identified Class_1, Class_2, Class_3 etc... 
        /// </summary>
        public List<Base_Class> buffer;
        /// <summary>
        /// Collection of Capture Objects Object Specification
        /// </summary>
        public List<CaptureObject> captureObjectsList;

        private Base_Class[] baseClassCaptureObjectsList;
        /// <summary>
        /// Represents the Capturing time! on which Objects are captured
        /// </summary>
        public ulong capturePeriod;
        /// <summary>
        /// Sorting algorithm
        /// </summary>
        public SortMethod sortMethod;
        /// <summary>
        /// Object to sort
        /// </summary>
        public CaptureObject sortObject;
        /// <summary>
        /// no of with holding entries
        /// </summary>
        public uint entriesInUse;
        /// <summary>
        /// maximum no of data allowed for a single profile
        /// </summary>
        public uint MaxProfileEntries;
        /// <summary>
        /// Assign a Key to the particular object
        /// </summary>
        private readonly GetSAPEntryKeyIndex GetSAPEntryDlg;

        #endregion

        #region Properties

        /// <summary>
        /// GET or SET array of DLMS/COSEM Class Objects.
        /// </summary>
        /// <returns>Base_Class[]</returns>
        public Base_Class[] BaseClassCaptureObjectsList
        {
            get { return (Base_Class[])baseClassCaptureObjectsList.Clone(); }
            set { baseClassCaptureObjectsList = value; }
        }

        /// <summary>
        /// Returns True if captureObjectsList is initialized else return false
        /// </summary>
        public bool IsCaptureObjectListIntialized
        {
            get
            {
                if (captureObjectsList.Count <= 0 || (baseClassCaptureObjectsList != null &&
                     baseClassCaptureObjectsList.Length != captureObjectsList.Count)
                   ) // Read Capture Objects are not up-to date
                {
                    return false;
                }
                else
                {
                    ///Check each Corresponding Values Comparison
                    for (int index = 0; baseClassCaptureObjectsList != null &&
                        index < baseClassCaptureObjectsList.Length; index++)
                    {
                        CaptureObject t_Capture = captureObjectsList[index];
                        Base_Class base_Capture = baseClassCaptureObjectsList[index];

                        if (base_Capture == null || !base_Capture.OBIS_CODE.SequenceEqual<byte>(t_Capture.OBISCode))     ///Compare OBIS
                            return false;
                        if (base_Capture.DecodingAttribute != t_Capture.AttributeIndex)         ///Compare Decoding Attributes
                            return false;
                        //Compare DataIndex,Select Script                               
                    }
                    return true;
                }
            }
        }
        /// <summary>
        /// Returns True if captureObjectsList is initialized else return false
        /// </summary>
        public bool IsCaptureObjectListInit
        {
            get
            {
                try
                {
                    if (captureObjectsList == null || captureObjectsList.Count <= 0)
                    // Read Capture Objects are not upto date
                    {
                        return false;
                    }
                    else
                    {
                        foreach (var item in captureObjectsList)
                        {
                            if (item == null)
                                return false;
                            StOBISCode OBIS = StOBISCode.ConvertFrom(item.OBISCode);
                            OBIS.ClassId = item.ClassId;
                            if (!OBIS.IsValidate)
                                return false;
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// get or set the attribute type as per specified by the DLMS/COSEM protocol
        /// </summary>
        /// <returns>byte</returns>
        public byte DataRequestType
        {
            get
            {
                return OBISIndex.OBISCode_Feild_F;
            }
            set
            {
                OBISIndex.Set_OBISCode_Feild_F(value);
                for (int index = 0; AccessResults != null && index < AccessResults.Length; index++)
                {
                    AccessResults[index] = DecodingResult.DataNotPresent;
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_7(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(7, 8, 4, Index, Obis_Code, No_of_Associations)
        {
            captureObjectsList = new List<CaptureObject>();
            buffer = new List<Base_Class>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        /// <param name="GetSAPEntryDlg">Pass a method that with compute the Current Object SAP Key(OBIS Code)</param>
        public Class_7(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations, GetSAPEntryKeyIndex GetSAPEntryDlg)
            : this(Index, Obis_Code, No_of_Associations)
        {
            this.GetSAPEntryDlg = GetSAPEntryDlg;
            captureObjectsList = new List<CaptureObject>();
            buffer = new List<Base_Class>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCode">OBIS code for a specific Object</param>
        /// <param name="GetSAPFEntryDelegate">Pass a method that with compute the Current Object SAP Key(OBIS Code)</param>
        public Class_7(StOBISCode OBISCode, GetSAPEntryKeyIndex GetSAPFEntryDelegate)
            : base(OBISCode, 8, 4)
        {
            this.GetSAPEntryDlg = GetSAPFEntryDelegate;
            captureObjectsList = new List<CaptureObject>();
            buffer = new List<Base_Class>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCode">OBIS code for a specific Object</param>
        public Class_7(StOBISCode OBISCode)
            : this(OBISCode, null)
        {
            captureObjectsList = new List<CaptureObject>();
            buffer = new List<Base_Class>();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_7 Object</param>
        public Class_7(Class_7 obj)
            : base(obj)
        {
            if (obj.buffer != null && obj.buffer.Count > 0)
                buffer = new List<Base_Class>(obj.buffer);
            else
                buffer = new List<Base_Class>();

            if (obj.captureObjectsList != null && obj.captureObjectsList.Count > 0)
                captureObjectsList = new List<CaptureObject>(obj.captureObjectsList);
            else
                captureObjectsList = new List<CaptureObject>();

            if (obj.baseClassCaptureObjectsList != null)
                this.baseClassCaptureObjectsList = (Base_Class[])obj.baseClassCaptureObjectsList.Clone();

            this.capturePeriod = obj.capturePeriod;
            sortMethod = obj.sortMethod;

            if (obj.sortObject != null)
                sortObject = (CaptureObject)obj.sortObject.Clone();

            entriesInUse = obj.entriesInUse;
            MaxProfileEntries = obj.MaxProfileEntries;
            GetSAPEntryDlg = obj.GetSAPEntryDlg;
        }

        #endregion

        #region Decoders/Encoders

        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote Site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte[] Obis_code_recieved = null;
                byte current_char;
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                //------------------------------------------------------
                DecoderAttribute_0(ref Data, ref array_traverse, String.Format("Decode_Data_Class_7_{0}", base.INDEX));
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, String.Format("Decode_Data_Class_7_{0}", base.INDEX));
                //------------------------------------------------------
                #region Attribute_0x02

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        Repeat_BufferDecodnig: current_char = Data[array_traverse++];

                        // null Data
                        if (current_char == (byte)DataTypes._A00_Null)
                        {
                            if (!IsAttribReadable(0x02))
                                SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
                            else
                            {
                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("{0} First element should be A01 (Array) or A13 CompactArray,invalid identifier (Error Code:{1})",
                                OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
                            }
                        }
                        else
                        {
                            // Check Capture Object List Is Initialized Properly
                            buffer.Clear();

                            int No_Capture_Instances = -1;
                            #region _A01_array

                            if (current_char == (byte)DataTypes._A01_array)
                            {
                                // Store No Of Entries
                                // No Of Capture Instances
                                No_Capture_Instances = BasicEncodeDecode.Decode_Length(Data, ref array_traverse, Data.Length);
                            }

                            #endregion
                            #region _A13_compact_arry

                            else if (current_char == (byte)DataTypes._A13_compact_arry)
                            {
                                array_traverse--;
                                DLMS.Comm.TypeDescriptor ContentDescription = null;
                                try
                                {
                                    var TArray = BasicEncodeDecode.Parse_CompactArray(ref Data, ref array_traverse, ref ContentDescription, length);
                                    if (TArray == null || TArray.Length <= 0)
                                        throw new Exception("Invalid Array Decoding _A13_compact_arry");

                                    // Refresh Data and array_traverse
                                    Data = TArray;
                                    array_traverse = 0;

                                    // Jump For Array _A01_array Decoding Procedure
                                    goto Repeat_BufferDecodnig;
                                }
                                catch (Exception ex)
                                {
                                    if (ex is DLMSDecodingException)
                                        throw ex;
                                    else
                                        throw new DLMSDecodingException(String.Format("Error occurred while Decoding DataType {0} (Error Code:{1})",
                                                                        DataTypes._A13_compact_arry,
                                                                        (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}", base.INDEX));
                                }
                            }

                            #endregion
                            #region _A02_structure

                            else if (current_char == (byte)DataTypes._A02_structure)
                            {
                                No_Capture_Instances = 1;
                            }

                            #endregion
                            else
                            {
                                // Generate Error And Return
                                SetAttributeDecodingResult(2, DecodingResult.DecodingError);

                                throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array) (Error Code:{1})",
                                                                OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
                            }

                            bool isSimpleDataTypes = false;

                            #region Simple Data_Type Check

                            int array_traverse_T = 0;
                            try
                            {
                                array_traverse_T = array_traverse;

                                current_char = Data[array_traverse++];
                                if (current_char != (byte)DataTypes._A02_structure)
                                {
                                    throw new DLMSDecodingException(String.Format("{0} Invalid buffer entry structure, Element should be Structure (Error Code:{1})",
                                                                OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
                                }

                                int Struct_El_Count = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                                DataTypes dt_Type = (DataTypes)Data[array_traverse++];

                                // Simple DataType Condition 
                                if (Struct_El_Count == 1 &&
                                    dt_Type >= DataTypes._A03_boolean &&
                                    dt_Type <= DataTypes._A24_Float64)
                                {
                                    isSimpleDataTypes = true;
                                }
                                else // Complex DataType Expected
                                    isSimpleDataTypes = false;

                            }
                            catch { }
                            finally
                            {
                                array_traverse = array_traverse_T;
                            }

                            #endregion

                            if (isSimpleDataTypes &&
                                (this.captureObjectsList == null || this.captureObjectsList.Count <= 0))
                            {
                                var capture_ObjList = Initialize_SimpleDataTypes_ObjectList(Get_Index.Dummy_CLS01, 0x02);
                                if (capture_ObjList != null && capture_ObjList.Count > 0)
                                    this.captureObjectsList = capture_ObjList;
                            }

                            // No Of Capture Instances
                            InitializeBuffer(No_Capture_Instances);

                            int bufferIndex = 0;
                            for (int count = 1; count <= No_Capture_Instances; count++)
                            {
                                current_char = Data[array_traverse++];
                                if (current_char != (byte)DataTypes._A02_structure)
                                {
                                    SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                                    throw new DLMSDecodingException(String.Format("{0} Invalid buffer entry structure,Element should be Structure (Error Code:{1})",
                                    OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
                                }
                                int _length = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                                Base_Class Decode_Obj = null;

                                //DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
                                for (int index = 0; index < _length; index++, bufferIndex++)
                                {
                                    try
                                    {
                                        Decode_Obj = buffer[bufferIndex];
                                        Decode_Obj.Decode_Data(ref Data, ref array_traverse, Data.Length);
                                        // array_traverse = 0;              
                                        // Array Resized After Decoding
                                    }
                                    catch (DLMSDecodingException ex)
                                    {
                                        SetAttributeDecodingResult(2, DecodingResult.DecodingError);

                                        throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer object {0}_{1} (Error Code:{2}).Details {3}",
                                            buffer[bufferIndex].OBISIndex, buffer[bufferIndex].OBISIndex.OBISIndex,
                                            (int)DLMSErrors.ErrorDecoding_Type, ex.Message),
                                            String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);

                                    }
                                    catch (Exception ex)
                                    {

                                        SetAttributeDecodingResult(2, DecodingResult.DecodingError);

                                        throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer object {0}_{1} (Error Code:{2}).Details {3}",
                                        buffer[bufferIndex].OBISIndex, buffer[bufferIndex].OBISIndex.OBISIndex,
                                        (int)DLMSErrors.ErrorDecoding_Type, ex.Message),
                                        String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                                    }
                                }
                            }
                            SetAttributeDecodingResult(2, DecodingResult.Ready);
                        }
                    }
                    catch (Exception ex)
                    {
                        SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                        if (ex is DLMSDecodingException ||
                            ex is DLMSException)
                            throw ex;
                        else
                            throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer {0}_{1} (Error Code:{2}).",
                                               base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
                                               String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x03

                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        current_char = Data[array_traverse++];
                        // null Data
                        if (current_char == (byte)DataTypes._A00_Null)
                        {
                            if (!IsAttribReadable(0x03))
                                SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                            else
                            {
                                SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array),invalid identifier (Error Code:{1})",
                                    OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
                            }
                        }
                        else  // Decode Capture Objects
                        {
                            if (current_char != (byte)DataTypes._A01_array)
                            {
                                // Generate Error and return
                                SetAttributeDecodingResult(3, DecodingResult.DecodingError);

                                throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array) (Error Code:{1})",
                                    OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
                            }
                            if (captureObjectsList == null)
                                captureObjectsList = new List<CaptureObject>();
                            else
                                captureObjectsList.Clear();

                            // store no of entries
                            int No_Entries = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                            // Decode Each Capture Object
                            for (int count = 1; count <= No_Entries; count++)
                            {
                                CaptureObject captureObj = DecodeCaptureObject(Data, ref array_traverse);
                                captureObjectsList.Add(captureObj);
                            }
                            if (GetSAPEntryDlg != null)    // try to initialize buffer
                            {
                                BaseClassCaptureObjectsList = InitializeCaptureBuffer(1);
                            }
                            //DecodingAttribute = (byte)restoreDecodingAttribute;
                            SetAttributeDecodingResult(3, DecodingResult.Ready);
                        }
                    }
                    catch (Exception ex)
                    {
                        SetAttributeDecodingResult(3, DecodingResult.DecodingError);
                        if (ex is DLMSDecodingException ||
                            ex is DLMSException)
                            throw ex;
                        else
                            throw new DLMSDecodingException(String.Format("Error occurred while decoding capture objects {0}_{1} (Error Code:{2})",
                                                   base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
                                                   String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04

                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        capturePeriod = Convert.ToUInt64(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                        SetAttributeDecodingResult(4, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(0x04))
                            SetAttributeDecodingResult(4, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(4, DecodingResult.DecodingError);

                            if (ex is DLMSDecodingException)
                                throw ex;
                            else
                                throw new DLMSDecodingException(String.Format("Error occurred while decoding capture period {0}_{1} (Error Code:{2})",
                                                       base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
                                                       String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05

                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        sortMethod = (SortMethod)Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                        SetAttributeDecodingResult(0x05, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(0x05))
                            SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                            if (ex is DLMSDecodingException)
                                throw ex;
                            else
                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding Sort Method {0}_{1} (Error Code:{2})",
                                                                base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
                                                                String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x06
                if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        sortObject = DecodeCaptureObject(Data, ref array_traverse);
                        SetAttributeDecodingResult(0x06, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(0x06))
                            SetAttributeDecodingResult(0x06, DecodingResult.Ready);
                        else
                        {
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
                            if (ex is DLMSDecodingException)
                                throw ex;
                            else
                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding sort object {0}_{1} (Error Code:{2})",
                                                                base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
                                                                String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                        }
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x07
                if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        this.entriesInUse = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                        SetAttributeDecodingResult(0x07, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(0x07))
                            SetAttributeDecodingResult(0x07, DecodingResult.Ready);
                        else
                        {
                            SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
                            if (ex is DLMSDecodingException)
                                throw ex;
                            else
                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding EntriesInUse {0}_{1} (Error Code:{2})",
                                                                    base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
                                                                    String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                        }
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x08

                if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        this.MaxProfileEntries = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                        SetAttributeDecodingResult(0x08, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(0x08))
                            SetAttributeDecodingResult(0x08, DecodingResult.Ready);
                        else
                        {
                            SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);

                            if (ex is DLMSDecodingException)
                                throw ex;
                            else
                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding MaxProfileEntries {0}_{1} (Error Code:{2})",
                                                                        base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
                                                                        String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException || ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding (Error Code:{0})",
                                       (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}_{1}",
                                       OBISIndex, OBISIndex.OBISIndex), ex);
            }
            finally
            {
                Data = null;
            }
        }

        /// <summary>
        /// It decodes the Capture object return by the meter
        /// </summary>
        /// <param name="Data">Capture Object Received from network</param>
        /// <param name="array_traverse">off-set</param>
        /// <returns><see cref="CaptureObject"/>
        /// </returns>
        private CaptureObject DecodeCaptureObject(byte[] Data, ref int array_traverse)
        {
            try
            {
                byte[] toCompare = null;
                byte[] tArray = null;
                ///<DataType Structure, 4>
                toCompare = new byte[] { (byte)DataTypes._A02_structure, 4 };
                tArray = (byte[])Array.CreateInstance(typeof(byte), 2);
                Array.Copy(Data, array_traverse, tArray, 0, tArray.Length);
                array_traverse += 2;
                if (!tArray.SequenceEqual<byte>(toCompare))
                {
                    SetAttributeDecodingResult(3, DecodingResult.DecodingError);
                    throw new DLMSDecodingException(String.Format("Invalid Structure of capture objects structure,invalid identifier (Error Code:{1})",
                        (int)DLMSErrors.Invalid_Type_MisMatch), String.Format("Decode_Data_Class_7_{0}", base.INDEX));
                }
                ///Class ID
                CaptureObject captureObj = new CaptureObject();
                ValueType T = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length);
                captureObj.ClassId = Convert.ToUInt16(T);
                ///OBISCode
                captureObj.OBISCode = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                if (captureObj.OBISCode == null || captureObj.OBISCode.Length != 6)
                {
                    SetAttributeDecodingResult(3, DecodingResult.DecodingError);
                    throw new DLMSDecodingException(String.Format("Invalid OBISCode of capture object received (Error Code:{0})",
                        (int)DLMSErrors.Invalid_OBISCode), String.Format("Decode_Data_Class_7_{0}", base.INDEX));
                }
                ///Attribute Index
                T = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length);
                captureObj.AttributeIndex = Convert.ToByte(T);
                ///Data Index
                T = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length);
                captureObj.DataIndex = Convert.ToUInt16(T);
                return captureObj;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding Capture Object (Error Code:{0})",
                                           (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}_{1}",
                                           OBISIndex, OBISIndex.OBISIndex), ex);
            }
        }

        /// <summary>
        /// Encode Capture object to send to meter
        /// </summary>
        /// <param name="obj">Capture Object</param>
        /// <returns>Encoded Capture Object in Byte[] compatible to the network stream</returns>
        public static byte[] EncodeCaptureObject(CaptureObject obj)
        {
            try
            {
                List<byte> encodedRaw = new List<byte>(15);
                ///<DataType Structure, 4>
                encodedRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });
                ///Class_Id
                ///<DataType _A12_long_unsigned,Value>
                encodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, obj.ClassId));
                ///OBIS_CODE
                ///<DataType Octect_String, OBISCOde>
                encodedRaw.AddRange(BasicEncodeDecode.Encode_OctetString(obj.OBISCode, DataTypes._A09_octet_string));
                ///Attribute_Index
                ///<DataType _A12_integer,Value>
                encodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, obj.AttributeIndex));
                ///Data_Index
                ///<DataType _A12_long_unsigned,Value>
                encodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, obj.DataIndex));
                return encodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSException || ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error occurred while encoding capture object (Error Code:{0})",
                    (int)DLMSErrors.ErrorEncoding_Type), String.Format("Encode_CaptureObject_Class_7"));
            }
        }

        /// <summary>
        /// Set the Request Encoder
        /// </summary>
        /// <returns>byte[]</returns>
        public override byte[] Encode_Data()
        {
            try
            {
                EncodedRaw = new List<byte>(0x0A);
                byte[] t_Array = null;
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------
                #region Attribute 0x02 Buffer

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge),
                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,encoder not implemented yet (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded), String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x03 CaptureObjectList

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge),
                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x03)
                    {
                        if (!this.IsCaptureObjectListIntialized)
                            throw new DLMSEncodingException(String.Format("Unable to encode CaptureObject List,not initialized properly (Error Code:{0})"
                                , (int)DLMSErrors.ErrorEncoding_Type), String.Format("Encode_Data_Class_7_{0}", base.INDEX));

                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        byte[] lengthArray = null;
                        BasicEncodeDecode.Encode_Length(ref lengthArray, (ushort)captureObjectsList.Count);
                        EncodedRaw.AddRange(lengthArray);
                        foreach (var item in captureObjectsList)
                        {
                            EncodedRaw.AddRange(EncodeCaptureObject(item));
                        }
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x04 Capture Period
                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge),
                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x04)
                    {
                        ///<DataType double_long_unsigned, Value>
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, this.capturePeriod));
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x05 Sort Method
                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x05);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge),
                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x05)
                    {
                        ///<DataType enum, Value>
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, this.sortMethod));
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x06 Sort Object

                if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x06);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                                                        (int)DLMSErrors.Insufficient_Priviledge),
                                                        String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x06)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Sort object,Encoder Not Available (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded),
                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x07 entries_in_use
                if (EncodingAttribute == 0x07 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x07);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                                                        (int)DLMSErrors.Insufficient_Priviledge),
                                                        String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x07)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode entreis_in_use,Encoder Not Available (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded),
                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x08 profile_entries

                if (EncodingAttribute == 0x08 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x08);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                                                        (int)DLMSErrors.Insufficient_Priviledge),
                                                        String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x08)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Profile_Entries,Encoder Not Available (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded),
                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
                    }
                }

                #endregion
                //------------------------------------------------------
                byte[] dtBuf = EncodedRaw.ToArray<byte>();
                EncodedRaw = null;
                return dtBuf;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException(String.Format("Error occurred while encoding data (Error Code:{0})",
                                                   OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.ErrorEncoding_Type), "Encode_Data_Class_7_", ex);
            }
        }

        #endregion

        #region Initial Support

        /// <summary>
        /// This Method initialize the Capture Object Buffer with respect to specific Instance if the Class_7
        /// </summary>
        /// <param name="instanceId">specific object identified with an ID</param>
        /// <returns>Base_Class[] (Initialized buffer of the Capture Objects)</returns>
        public Base_Class[] InitializeCaptureBuffer(ushort instanceId)
        {
            try
            {
                if (captureObjectsList == null || captureObjectsList.Count <= 0)
                    throw new DLMSException(String.Format("Error occurred while initializing capture *(captureObjectsList not initialize) objects_Class_7_{0}", base.INDEX));
                int captureObjectCount = captureObjectsList.Count;
                int captureListIndex = 0;
                Base_Class[] _baseClassCaptureObjectsList = new Base_Class[captureObjectCount];
                foreach (var item in this.captureObjectsList)
                {
                    StOBISCode OBISCodeTmp = StOBISCode.ConvertFrom(item.OBISCode);
                    OBISCodeTmp.ClassId = item.ClassId;

                    if (!OBISCodeTmp.IsValidate)
                    {
                        String OBIS_STR = DLMS_Common.ArrayToHexString(item.OBISCode);

                        throw new DLMSException(String.Format("OBIS Code {0} not found in SAP Table_Initialize Buffer_Class_7_{1} (Error Code:{2})"
                            , OBIS_STR, this.INDEX, (int)DLMSErrors.Invalid_OBISCode));
                    }

                    Base_Class SapEntity = GetSAPEntryDlg.Invoke(new LRUCache.KeyIndexer(OBISCodeTmp.OBISIndex, KeyIndexer.OwnerId, instanceId));
                    SapEntity.DecodingAttribute = item.AttributeIndex;
                    ///Selective Access
                    _baseClassCaptureObjectsList[captureListIndex++] = SapEntity;
                }
                return _baseClassCaptureObjectsList;
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error initializing capture objects_Class_7_{0},{1}", base.INDEX, ex.Message), ex);
            }
        }

        /// <summary>
        /// Initialize the CaptureObjectList in CaptureObject List
        /// </summary>
        /// <param name="captureObjectList"></param>

        public void LoadCaptureBuffer(Base_Class[] captureObjects)
        {
            try
            {
                if (captureObjects == null || captureObjects.Length <= 0)
                    throw new DLMSException(String.Format("Error occurred while loading capture List  objects_Class_7_{0}", base.INDEX));
                captureObjectsList.Clear();
                foreach (var item in captureObjects)
                {
                    StOBISCode OBISCodeTmp = StOBISCode.ConvertFrom(item.OBIS_CODE);
                    OBISCodeTmp.ClassId = item.Class_ID;

                    if (!OBISCodeTmp.IsValidate)
                    {
                        String OBIS_STR = DLMS_Common.ArrayToHexString(item.OBIS_CODE);
                        throw new DLMSException(String.Format("OBIS Code {0} not found in SAP Table_Initialize Buffer_Class_7_{1} (Error Code:{2})"
                            , OBIS_STR, this.INDEX, (int)DLMSErrors.Invalid_OBISCode));
                    }

                    CaptureObject obj = new CaptureObject();
                    obj.ClassId = item.Class_ID;
                    obj.OBISCode = item.OBIS_CODE;
                    obj.AttributeIndex = item.DecodingAttribute;
                    obj.DataIndex = 0;      // Retrieve Whole Attribute
                    captureObjectsList.Add(obj);
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error loading capture objects_Class_7_{0}", base.INDEX));
            }
        }
        /// <summary>
        /// Initialize capture object list
        /// </summary>
        /// <param name="NoCaputeInstance">no of capture objects received</param>
        public void InitializeBuffer(int NoCaputeInstance)
        {
            try
            {
                //if (!IsCaptureObjectListIntialized)
                //    throw new DLMSException(String.Format("Unable to initialize the buffer,Capture Objects List not valid_Class_7_{0}", this.INDEX));
                if (AccessSelector != null && !AccessSelector.IsValid())
                    throw new DLMSException(String.Format("Unable to initialize the buffer,invalid access selection_Class_7_{0}", this.INDEX));
                int horizentalSelectionCount = 0;
                if (AccessSelector == null)
                {
                    buffer.Clear();
                    for (int captureInstanceId = 1; captureInstanceId <= NoCaputeInstance; captureInstanceId++)
                        buffer.AddRange(InitializeCaptureBuffer((ushort)captureInstanceId));

                    // for (int index = 0; index < horizentalSelectionCount; index++)
                    // {
                    //     buffer.AddRange(Init (Base_Class)baseClassCaptureObjectsList[index].Clone());
                    // }

                }
                else if (AccessSelector.GetSelectorType() == SelectiveAccessType.Entry_Descripter)
                {
                    EntryDescripter Selector = (EntryDescripter)AccessSelector;
                    int maxHorSelector = (Selector.ToSelectedValue == EntryDescripter.MaxPossibleValue) ? captureObjectsList.Count : Selector.ToSelectedValue;
                    horizentalSelectionCount = (maxHorSelector - Selector.ToSelectedValue);
                    buffer.Clear();
                    for (int captureInsatnceId = 1; captureInsatnceId <= NoCaputeInstance; captureInsatnceId++)
                    {
                        Base_Class[] tArray = InitializeCaptureBuffer((ushort)captureInsatnceId);
                        ///Add Only Selected Values Only
                        for (int index = Selector.FromSelectedValue - 1; index < (maxHorSelector); index++)
                        {
                            buffer.Add((Base_Class)tArray[index]);
                        }
                    }
                }
                else if (AccessSelector.GetSelectorType() == SelectiveAccessType.Range_Descripter)
                {
                    {
                        RangeDescripter Selector = (RangeDescripter)AccessSelector;
                        //int maxHorSelector = (Selector.ToSelectedValue == RangeDescripter.MaxPossibleValue) ? captureObjectsList.Count : Selector.ToSelectedValue;
                        //horizentalSelectionCount = (maxHorSelector - Selector.ToSelectedValue);
                        buffer.Clear();
                        for (int captureInsatnceId = 1; captureInsatnceId <= NoCaputeInstance; captureInsatnceId++)
                        {
                            Base_Class[] tArray = InitializeCaptureBuffer((ushort)captureInsatnceId);
                            ///Add Only Selected Values Only
                            for (int index = 0; index < (tArray.Length); index++)
                            {
                                buffer.Add((Base_Class)tArray[index]);
                            }
                        }
                        //throw new DLMSException(String.Format("Unable to initialize the buffer,RangeDescripter AccessSelector not supported_Class_7_{0}",
                        //                       this.INDEX));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error initializing Capture Buffer_Data_Class_7_{0}_{1}", base.INDEX, ex.Message));
            }
        }

        public List<CaptureObject> Initialize_SimpleDataTypes_ObjectList(StOBISCode OBISCodes, byte AttributeIndex = 0x02)
        {
            List<CaptureObject> listToReturn = new List<CaptureObject>();

            CaptureObject Obj_1 = new CaptureObject();
            // StOBISCode OBIS = OBISCodes;
            Obj_1.ClassId = OBISCodes.ClassId;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.DataIndex = 0;
            Obj_1.OBISCode = OBISCodes.OBISCode;
            listToReturn.Add(Obj_1);

            return listToReturn;
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_7 cloned = new Class_7(this);
            if (cloned.buffer != null && cloned.buffer.Count > 0)
                cloned.buffer.Clear();
            return cloned;
        }

        /// <summary>
        /// Returns the String Representation of the Class_7 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder(1000);

            if (buffer != null && buffer.Count > 0 && GetAttributeDecodingResult(2) == DecodingResult.Ready)    ///Buffer Decoded Info
            {
                // Compute buffer Capture Instance Size
                int captureInstanceSize = buffer.FindIndex(1, (x) => x.OBIS_CODE.SequenceEqual<byte>(buffer[0].OBIS_CODE));

                if (captureInstanceSize == -1)
                    captureInstanceSize = buffer.Count;
                strVal.AppendFormat(",Buffer Capture Instance Size:{0}", captureInstanceSize);
                strVal.AppendFormat(",Total Decoded Buffer Objects:{0}:{1},", buffer.Count, GetAttributeDecodingResult(2));

                // foreach (Base_Class obj in buffer)
                // {
                //     strVal.Append(obj.ToString());
                // }
            }

            if (IsCaptureObjectListIntialized && GetAttributeDecodingResult(3) == DecodingResult.Ready) // Buffer Decoded Info
            {
                strVal.AppendFormat(",Capture Objects List:{0}:{1},", baseClassCaptureObjectsList.Length, GetAttributeDecodingResult(3));
                foreach (Base_Class obj in baseClassCaptureObjectsList)
                {
                    strVal.Append(String.Format("{0},", obj.INDEX));
                }
            }
            return baseStr + strVal.ToString();
        }

        #endregion

    }

    #endregion
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Diagnostics;
//using System.ComponentModel;
//using DLMS.Comm;

//namespace DLMS
//{
//    #region Class_7

//    /// <summary>
//    /// Profile generic (class_id: 7, version: 1) provides a generalized concept allowing to store, sort and access data groups or data series, called capture objects.
//    /// Capture objects are appropriate attributes or elements of (an) attribute(s) of COSEM objects. 
//    /// The capture objects are collected periodically or occasionally.
//    /// A profile has a buffer to store the captured data.
//    /// To retrieve only a part of the buffer, either a value range or an entry range may be specified, asking to retrieve all entries that fall within the range specified.
//    /// The list of capture objects defines the values to be stored in the buffer (using auto capture or the method capture). 
//    /// The list is defined statically to ensure homogeneous buffer entries (all entries have the same size and structure).
//    /// If the list of capture objects is modified, the buffer is cleared. If the buffer is captured by other Profile generic objects, their buffer is cleared as well, to guarantee the homogeneity of their buffer entries.
//    /// The buffer may be defined as sorted by one of the capture objects, e.g. the clock, or the entries are stacked in a last in first out order.
//    /// For example, it is very easy to build a maximum demand register with a one entry deep sorted profile capturing and sorted by a Demand register last_average_value attribute. 
//    /// It is just as simple to define a profile retaining the three largest values of some period.
//    /// </summary>
//    public class Class_7 : Base_Class
//    {
//        #region Data_Members

//        /// <summary>
//        /// Collection of DLMS/COSEM Class Objects Identified Class_1, Class_2, Class_3 etc... 
//        /// </summary>
//        public List<Base_Class> buffer;
//        /// <summary>
//        /// Collection of Capture Objects Object Specification
//        /// </summary>
//        public List<CaptureObject> captureObjectsList;

//        private Base_Class[] baseClassCaptureObjectsList;
//        /// <summary>
//        /// Represents the Capturing time! on which Objects are captured
//        /// </summary>
//        public ulong capturePeriod;
//        /// <summary>
//        /// Sorting algorithm
//        /// </summary>
//        public SortMethod sortMethod;
//        /// <summary>
//        /// Object to sort
//        /// </summary>
//        public CaptureObject sortObject;
//        /// <summary>
//        /// no of with holding entries
//        /// </summary>
//        public uint entriesInUse;
//        /// <summary>
//        /// maximum no of data allowed for a single profile
//        /// </summary>
//        public uint MaxProfileEntries;
//        /// <summary>
//        /// Assign a Key to the particular object
//        /// </summary>
//        private readonly GetSAPEntryKeyIndex GetSAPEntryDlg;

//        #endregion

//        #region Properties

//        /// <summary>
//        /// GET or SET array of DLMS/COSEM Class Objects.
//        /// </summary>
//        /// <returns>Base_Class[]</returns>
//        public Base_Class[] BaseClassCaptureObjectsList
//        {
//            get { return (Base_Class[])baseClassCaptureObjectsList.Clone(); }
//            set { baseClassCaptureObjectsList = value; }
//        }

//        /// <summary>
//        /// Returns True if captureObjectsList is initialized else return false
//        /// </summary>
//        public bool IsCaptureObjectListIntialized
//        {
//            get
//            {
//                if (captureObjectsList.Count <= 0 || (baseClassCaptureObjectsList != null &&
//                     baseClassCaptureObjectsList.Length != captureObjectsList.Count)
//                   ) // Read Capture Objects are not up-to date
//                {
//                    return false;
//                }
//                else
//                {
//                    ///Check each Corresponding Values Comparison
//                    for (int index = 0; baseClassCaptureObjectsList != null &&
//                        index < baseClassCaptureObjectsList.Length; index++)
//                    {
//                        CaptureObject t_Capture = captureObjectsList[index];
//                        Base_Class base_Capture = baseClassCaptureObjectsList[index];

//                        if (base_Capture == null || !base_Capture.OBIS_CODE.SequenceEqual<byte>(t_Capture.OBISCode))     ///Compare OBIS
//                            return false;
//                        if (base_Capture.DecodingAttribute != t_Capture.AttributeIndex)         ///Compare Decoding Attributes
//                            return false;
//                        //Compare DataIndex,Select Script                               
//                    }
//                    return true;
//                }
//            }
//        }
//        /// <summary>
//        /// Returns True if captureObjectsList is initialized else return false
//        /// </summary>
//        public bool IsCaptureObjectListInit
//        {
//            get
//            {
//                try
//                {
//                    if (captureObjectsList == null || captureObjectsList.Count <= 0)
//                    // Read Capture Objects are not upto date
//                    {
//                        return false;
//                    }
//                    else
//                    {
//                        foreach (var item in captureObjectsList)
//                        {
//                            if (item == null)
//                                return false;
//                            StOBISCode OBIS = StOBISCode.ConvertFrom(item.OBISCode);
//                            OBIS.ClassId = item.ClassId;
//                            if (!OBIS.IsValidate)
//                                return false;
//                        }
//                    }
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    return false;
//                }
//            }
//        }
//        /// <summary>
//        /// get or set the attribute type as per specified by the DLMS/COSEM protocol
//        /// </summary>
//        /// <returns>byte</returns>
//        public byte DataRequestType
//        {
//            get
//            {
//                return OBISIndex.OBISCode_Feild_F;
//            }
//            set
//            {
//                OBISIndex.Set_OBISCode_Feild_F(value);
//                for (int index = 0; AccessResults != null && index < AccessResults.Length; index++)
//                {
//                    AccessResults[index] = DecodingResult.DataNotPresent;
//                }
//            }
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="Index">OBIS code for specific entity</param>
//        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
//        /// <param name="No_of_Associations">maximum no of association</param>
//        public Class_7(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
//            : base(7, 8, 4, Index, Obis_Code, No_of_Associations)
//        {
//            captureObjectsList = new List<CaptureObject>();
//            buffer = new List<Base_Class>();
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
//        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
//        /// <param name="GetSAPEntryDlg">Pass a method that with compute the Current Object SAP Key(OBIS Code)</param>
//        public Class_7(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations, GetSAPEntryKeyIndex GetSAPEntryDlg)
//            : this(Index, Obis_Code, No_of_Associations)
//        {
//            this.GetSAPEntryDlg = GetSAPEntryDlg;
//            captureObjectsList = new List<CaptureObject>();
//            buffer = new List<Base_Class>();
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="OBISCode">OBIS code for a specific Object</param>
//        /// <param name="GetSAPFEntryDelegate">Pass a method that with compute the Current Object SAP Key(OBIS Code)</param>
//        public Class_7(StOBISCode OBISCode, GetSAPEntryKeyIndex GetSAPFEntryDelegate)
//            : base(OBISCode, 8, 4)
//        {
//            this.GetSAPEntryDlg = GetSAPFEntryDelegate;
//            captureObjectsList = new List<CaptureObject>();
//            buffer = new List<Base_Class>();
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="OBISCode">OBIS code for a specific Object</param>
//        public Class_7(StOBISCode OBISCode)
//            : this(OBISCode, null)
//        {
//            captureObjectsList = new List<CaptureObject>();
//            buffer = new List<Base_Class>();
//        }

//        /// <summary>
//        /// Copy Constructor
//        /// </summary>
//        /// <param name="obj">Class_7 Object</param>
//        public Class_7(Class_7 obj)
//            : base(obj)
//        {
//            if (obj.buffer != null && obj.buffer.Count > 0)
//                buffer = new List<Base_Class>(obj.buffer);
//            else
//                buffer = new List<Base_Class>();

//            if (obj.captureObjectsList != null && obj.captureObjectsList.Count > 0)
//                captureObjectsList = new List<CaptureObject>(obj.captureObjectsList);
//            else
//                captureObjectsList = new List<CaptureObject>();

//            if (obj.baseClassCaptureObjectsList != null)
//                this.baseClassCaptureObjectsList = (Base_Class[])obj.baseClassCaptureObjectsList.Clone();

//            this.capturePeriod = obj.capturePeriod;
//            sortMethod = obj.sortMethod;

//            if (obj.sortObject != null)
//                sortObject = (CaptureObject)obj.sortObject.Clone();

//            entriesInUse = obj.entriesInUse;
//            MaxProfileEntries = obj.MaxProfileEntries;
//            GetSAPEntryDlg = obj.GetSAPEntryDlg;
//        }

//        #endregion

//        #region Decoders/Encoders

//        /// <summary>
//        /// Decode Data of this Class which is received in response of get data Request
//        /// </summary>
//        /// <param name="Data">Received data from Remote Site</param>
//        /// <param name="array_traverse">Off-Set</param>
//        /// <param name="length">Length to decode</param>
//        //       public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
//        //       {
//        //           try
//        //           {
//        //               byte[] Obis_code_recieved = null;
//        //               byte current_char;
//        //               if (DecodingAttribute == 0x00)
//        //               {
//        //                   for (int index = 0; index < AccessResults.Length; index++)
//        //                       AccessResults[index] = DecodingResult.DataNotPresent;
//        //               }
//        //               else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
//        //                   SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
//        //               //------------------------------------------------------
//        //               DecoderAttribute_0(ref Data, ref array_traverse, String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//        //               DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//        //               //------------------------------------------------------
//        //               #region Attribute_0x02

//        //               if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
//        //               {
//        //                   try
//        //                   {
//        //Repeat_BufferDecodnig: current_char = Data[array_traverse++];

//        //                       // null Data
//        //                       if (current_char == (byte)DataTypes._A00_Null)
//        //                       {
//        //                           if (!IsAttribReadable(0x02))
//        //                               SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
//        //                           else
//        //                           {
//        //                               SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
//        //                               throw new DLMSDecodingException(String.Format("{0} First element should be A01 (Array) or A13 CompactArray,invalid identifier (Error Code:{1})",
//        //                               OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//        //                           }
//        //                       }
//        //                       else
//        //                       {
//        //                           // Check Capture Object List Is Initialized Properly
//        //                           buffer.Clear();

//        //                           int No_Capture_Instances = -1;
//        //                           #region _A01_array

//        //                           if (current_char == (byte)DataTypes._A01_array)
//        //                           {
//        //                               // Store No Of Entries
//        //                               // No Of Capture Instances
//        //                               No_Capture_Instances = BasicEncodeDecode.Decode_Length(Data, ref array_traverse, Data.Length);
//        //                           }

//        //                           #endregion
//        //                           #region _A13_compact_arry

//        //                           else if (current_char == (byte)DataTypes._A13_compact_arry)
//        //                           {
//        //                               array_traverse--;
//        //                               DLMS.Comm.TypeDescriptor ContentDescription = null;
//        //                               try
//        //                               {
//        //                                   var TArray = BasicEncodeDecode.Parse_CompactArray(ref Data, ref array_traverse, ref ContentDescription, length);
//        //                                   if (TArray == null || TArray.Length <= 0)
//        //                                       throw new Exception("Invalid Array Decoding _A13_compact_arry");

//        //                                   // Refresh Data and array_traverse
//        //                                   Data = TArray;
//        //                                   array_traverse = 0;

//        //                                   // Jump For Array _A01_array Decoding Procedure
//        //                                   goto Repeat_BufferDecodnig;
//        //                               }
//        //                               catch (Exception ex)
//        //                               {
//        //                                   if (ex is DLMSDecodingException)
//        //                                       throw ex;
//        //                                   else
//        //                                       throw new DLMSDecodingException(String.Format("Error occurred while Decoding DataType {0} (Error Code:{1})",
//        //                                                                       DataTypes._A13_compact_arry,
//        //                                                                       (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//        //                               }
//        //                           } 

//        //                           #endregion
//        //                           #region _A02_structure

//        //                           else if (current_char == (byte)DataTypes._A02_structure)
//        //                           {
//        //                               No_Capture_Instances = 1;
//        //                           } 

//        //                           #endregion
//        //                           else
//        //                           {
//        //                               // Generate Error And Return
//        //                               SetAttributeDecodingResult(2, DecodingResult.DecodingError);

//        //                               throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array) (Error Code:{1})",
//        //                                                               OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//        //                           }

//        //                           bool isSimpleDataTypes = false;

//        //                           #region Simple Data_Type Check

//        //                           int array_traverse_T = 0;
//        //                           try
//        //                           {
//        //                               array_traverse_T = array_traverse;

//        //                               current_char = Data[array_traverse++];
//        //                               if (current_char != (byte)DataTypes._A02_structure)
//        //                               {
//        //                                   throw new DLMSDecodingException(String.Format("{0} Invalid buffer entry structure, Element should be Structure (Error Code:{1})",
//        //                                                               OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//        //                               }

//        //                               int Struct_El_Count = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
//        //                               DataTypes dt_Type = (DataTypes)Data[array_traverse++];

//        //                               // Simple DataType Condition 
//        //                               if (Struct_El_Count == 1 &&
//        //                                   dt_Type >= DataTypes._A03_boolean &&
//        //                                   dt_Type <= DataTypes._A24_Float64)
//        //                               {
//        //                                   isSimpleDataTypes = true;
//        //                               }
//        //                               else // Complex DataType Expected
//        //                                   isSimpleDataTypes = false;

//        //                           }
//        //                           catch { }
//        //                           finally
//        //                           {
//        //                               array_traverse = array_traverse_T;
//        //                           }

//        //                           #endregion

//        //                           if (isSimpleDataTypes &&
//        //                               (this.captureObjectsList == null || this.captureObjectsList.Count <= 0))
//        //                           {
//        //                               var capture_ObjList = Initialize_SimpleDataTypes_ObjectList(Get_Index.Dummy_CLS01, 0x02);
//        //                               if (capture_ObjList != null && capture_ObjList.Count > 0)
//        //                                   this.captureObjectsList = capture_ObjList;
//        //                           }

//        //                           // No Of Capture Instances
//        //                           InitializeBuffer(No_Capture_Instances);

//        //                           int bufferIndex = 0;
//        //                           for (int count = 1; count <= No_Capture_Instances; count++)
//        //                           {
//        //                               current_char = Data[array_traverse++];
//        //                               if (current_char != (byte)DataTypes._A02_structure)
//        //                               {
//        //                                   SetAttributeDecodingResult(2, DecodingResult.DecodingError);
//        //                                   throw new DLMSDecodingException(String.Format("{0} Invalid buffer entry structure,Element should be Structure (Error Code:{1})",
//        //                                   OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//        //                               }
//        //                               int _length = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
//        //                               Base_Class Decode_Obj = null;

//        //                               //DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
//        //                               for (int index = 0; index < _length; index++, bufferIndex++)
//        //                               {
//        //                                   try
//        //                                   {
//        //                                       Decode_Obj = buffer[bufferIndex];
//        //                                       Decode_Obj.Decode_Data(ref Data, ref array_traverse, Data.Length);
//        //                                       // array_traverse = 0;              
//        //                                       // Array Resized After Decoding
//        //                                   }
//        //                                   catch (DLMSDecodingException ex)
//        //                                   {
//        //                                       SetAttributeDecodingResult(2, DecodingResult.DecodingError);

//        //                                       throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer object {0}_{1} (Error Code:{2}).Details {3}",
//        //                                           buffer[bufferIndex].OBISIndex, buffer[bufferIndex].OBISIndex.OBISIndex,
//        //                                           (int)DLMSErrors.ErrorDecoding_Type, ex.Message),
//        //                                           String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);

//        //                                   }
//        //                                   catch (Exception ex)
//        //                                   {

//        //                                       SetAttributeDecodingResult(2, DecodingResult.DecodingError);

//        //                                       throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer object {0}_{1} (Error Code:{2}).Details {3}",
//        //                                       buffer[bufferIndex].OBISIndex, buffer[bufferIndex].OBISIndex.OBISIndex,
//        //                                       (int)DLMSErrors.ErrorDecoding_Type, ex.Message),
//        //                                       String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                                   }
//        //                               }
//        //                           }
//        //                           SetAttributeDecodingResult(2, DecodingResult.Ready);
//        //                       }
//        //                   }
//        //                   catch (Exception ex)
//        //                   {
//        //                       SetAttributeDecodingResult(2, DecodingResult.DecodingError);
//        //                       if (ex is DLMSDecodingException ||
//        //                           ex is DLMSException)
//        //                           throw ex;
//        //                       else
//        //                           throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer {0}_{1} (Error Code:{2}).",
//        //                                              base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//        //                                              String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                   }
//        //               }

//        //               #endregion
//        //               //------------------------------------------------------
//        //               #region Attribute 0x03

//        //               if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
//        //               {
//        //                   try
//        //                   {
//        //                       current_char = Data[array_traverse++];
//        //                       // null Data
//        //                       if (current_char == (byte)DataTypes._A00_Null)
//        //                       {
//        //                           if (!IsAttribReadable(0x03))
//        //                               SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
//        //                           else
//        //                           {
//        //                               SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
//        //                               throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array),invalid identifier (Error Code:{1})",
//        //                                   OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//        //                           }
//        //                       }
//        //                       else  // Decode Capture Objects
//        //                       {
//        //                           if (current_char != (byte)DataTypes._A01_array)
//        //                           {
//        //                               // Generate Error and return
//        //                               SetAttributeDecodingResult(3, DecodingResult.DecodingError);

//        //                               throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array) (Error Code:{1})",
//        //                                   OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//        //                           }
//        //                           if (captureObjectsList == null)
//        //                               captureObjectsList = new List<CaptureObject>();
//        //                           else
//        //                               captureObjectsList.Clear();

//        //                           // store no of entries
//        //                           int No_Entries = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
//        //                           // Decode Each Capture Object
//        //                           for (int count = 1; count <= No_Entries; count++)
//        //                           {
//        //                               CaptureObject captureObj = DecodeCaptureObject(Data, ref array_traverse);
//        //                               captureObjectsList.Add(captureObj);
//        //                           }
//        //                           if (GetSAPEntryDlg != null)    // try to initialize buffer
//        //                           {
//        //                               BaseClassCaptureObjectsList = InitializeCaptureBuffer(1);
//        //                           }
//        //                           //DecodingAttribute = (byte)restoreDecodingAttribute;
//        //                           SetAttributeDecodingResult(3, DecodingResult.Ready);
//        //                       }
//        //                   }
//        //                   catch (Exception ex)
//        //                   {
//        //                       SetAttributeDecodingResult(3, DecodingResult.DecodingError);
//        //                       if (ex is DLMSDecodingException ||
//        //                           ex is DLMSException)
//        //                           throw ex;
//        //                       else
//        //                           throw new DLMSDecodingException(String.Format("Error occurred while decoding capture objects {0}_{1} (Error Code:{2})",
//        //                                                  base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//        //                                                  String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                   }
//        //               }

//        //               #endregion
//        //               //------------------------------------------------------
//        //               #region Attribute 0x04

//        //               if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
//        //               {
//        //                   try
//        //                   {
//        //                       capturePeriod = Convert.ToUInt64(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
//        //                       SetAttributeDecodingResult(4, DecodingResult.Ready);
//        //                   }
//        //                   catch (Exception ex)
//        //                   {
//        //                       if (!IsAttribReadable(0x04))
//        //                           SetAttributeDecodingResult(4, DecodingResult.NoAccess);
//        //                       else
//        //                       {
//        //                           SetAttributeDecodingResult(4, DecodingResult.DecodingError);

//        //                           if (ex is DLMSDecodingException)
//        //                               throw ex;
//        //                           else
//        //                               throw new DLMSDecodingException(String.Format("Error occurred while decoding capture period {0}_{1} (Error Code:{2})",
//        //                                                      base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//        //                                                      String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                       }
//        //                   }
//        //               }

//        //               #endregion
//        //               //------------------------------------------------------
//        //               #region Attribute 0x05

//        //               if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
//        //               {
//        //                   try
//        //                   {
//        //                       sortMethod = (SortMethod)Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
//        //                       SetAttributeDecodingResult(0x05, DecodingResult.Ready);
//        //                   }
//        //                   catch (Exception ex)
//        //                   {
//        //                       if (!IsAttribReadable(0x05))
//        //                           SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
//        //                       else
//        //                       {
//        //                           SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
//        //                           if (ex is DLMSDecodingException)
//        //                               throw ex;
//        //                           else
//        //                               throw new DLMSDecodingException(String.Format("Error Occurred while decoding Sort Method {0}_{1} (Error Code:{2})",
//        //                                                               base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//        //                                                               String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                       }
//        //                   }
//        //               }

//        //               #endregion
//        //               //------------------------------------------------------
//        //               #region Attribute 0x06
//        //               if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
//        //               {
//        //                   try
//        //                   {
//        //                       sortObject = DecodeCaptureObject(Data, ref array_traverse);
//        //                       SetAttributeDecodingResult(0x06, DecodingResult.Ready);
//        //                   }
//        //                   catch (Exception ex)
//        //                   {
//        //                       if (!IsAttribReadable(0x06))
//        //                           SetAttributeDecodingResult(0x06, DecodingResult.Ready);
//        //                       else
//        //                       {
//        //                           SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
//        //                           if (ex is DLMSDecodingException)
//        //                               throw ex;
//        //                           else
//        //                               throw new DLMSDecodingException(String.Format("Error Occurred while decoding sort object {0}_{1} (Error Code:{2})",
//        //                                                               base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//        //                                                               String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                       }
//        //                   }
//        //               }
//        //               #endregion
//        //               //------------------------------------------------------
//        //               #region Attribute 0x07
//        //               if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
//        //               {
//        //                   try
//        //                   {
//        //                       this.entriesInUse = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
//        //                       SetAttributeDecodingResult(0x07, DecodingResult.Ready);
//        //                   }
//        //                   catch (Exception ex)
//        //                   {
//        //                       if (!IsAttribReadable(0x07))
//        //                           SetAttributeDecodingResult(0x07, DecodingResult.Ready);
//        //                       else
//        //                       {
//        //                           SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
//        //                           if (ex is DLMSDecodingException)
//        //                               throw ex;
//        //                           else
//        //                               throw new DLMSDecodingException(String.Format("Error Occurred while decoding EntriesInUse {0}_{1} (Error Code:{2})",
//        //                                                                   base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//        //                                                                   String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                       }
//        //                   }
//        //               }
//        //               #endregion
//        //               //------------------------------------------------------
//        //               #region Attribute 0x08

//        //               if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
//        //               {
//        //                   try
//        //                   {
//        //                       this.MaxProfileEntries = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
//        //                       SetAttributeDecodingResult(0x08, DecodingResult.Ready);
//        //                   }
//        //                   catch (Exception ex)
//        //                   {
//        //                       if (!IsAttribReadable(0x08))
//        //                           SetAttributeDecodingResult(0x08, DecodingResult.Ready);
//        //                       else
//        //                       {
//        //                           SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);

//        //                           if (ex is DLMSDecodingException)
//        //                               throw ex;
//        //                           else
//        //                               throw new DLMSDecodingException(String.Format("Error Occurred while decoding MaxProfileEntries {0}_{1} (Error Code:{2})",
//        //                                                                       base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//        //                                                                       String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//        //                       }
//        //                   }
//        //               }

//        //               #endregion
//        //           }
//        //           catch (Exception ex)
//        //           {
//        //               if (ex is DLMSDecodingException || ex is DLMSException)
//        //                   throw ex;
//        //               else
//        //                   throw new DLMSDecodingException(String.Format("Error occurred while decoding (Error Code:{0})",
//        //                                      (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}_{1}",
//        //                                      OBISIndex, OBISIndex.OBISIndex), ex);
//        //           }
//        //           finally
//        //           {
//        //               Data = null;
//        //           }
//        //       }

//        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
//        {
//            try
//            {
//                byte[] Obis_code_recieved = null;
//                byte current_char;
//                if (DecodingAttribute == 0x00)
//                {
//                    for (int index = 0; index < AccessResults.Length; index++)
//                        AccessResults[index] = DecodingResult.DataNotPresent;
//                }
//                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
//                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
//                //------------------------------------------------------
//                DecoderAttribute_0(ref Data, ref array_traverse, String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//                //------------------------------------------------------
//                #region Attribute_0x02

//                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
//                {
//                    try
//                    {
//                        Repeat_BufferDecodnig: current_char = Data[array_traverse++];

//                        // null Data
//                        if (current_char == (byte)DataTypes._A00_Null)
//                        {
//                            if (!IsAttribReadable(0x02))
//                                SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
//                            else
//                            {
//                                SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
//                                throw new DLMSDecodingException(String.Format("{0} First element should be A01 (Array) or A13 CompactArray,invalid identifier (Error Code:{1})",
//                                OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//                            }
//                        }
//                        else
//                        {
//                            // Check Capture Object List Is Initialized Properly
//                            buffer.Clear();

//                            int No_Capture_Instances = -1;
//                            #region _A01_array

//                            if (current_char == (byte)DataTypes._A01_array)
//                            {
//                                // Store No Of Entries
//                                // No Of Capture Instances
//                                No_Capture_Instances = BasicEncodeDecode.Decode_Length(Data, ref array_traverse, Data.Length);
//                            }

//                            #endregion
//                            #region _A13_compact_arry

//                            else if (current_char == (byte)DataTypes._A13_compact_arry)
//                            {
//                                array_traverse--;
//                                DLMS.Comm.TypeDescriptor ContentDescription = null;
//                                try
//                                {
//                                    var TArray = BasicEncodeDecode.Parse_CompactArray(ref Data, ref array_traverse, ref ContentDescription, length);
//                                    if (TArray == null || TArray.Length <= 0)
//                                        throw new Exception("Invalid Array Decoding _A13_compact_arry");

//                                    // Refresh Data and array_traverse
//                                    Data = TArray;
//                                    array_traverse = 0;

//                                    // Jump For Array _A01_array Decoding Procedure
//                                    goto Repeat_BufferDecodnig;
//                                }
//                                catch (Exception ex)
//                                {
//                                    if (ex is DLMSDecodingException)
//                                        throw ex;
//                                    else
//                                        throw new DLMSDecodingException(String.Format("Error occurred while Decoding DataType {0} (Error Code:{1})",
//                                                                        DataTypes._A13_compact_arry,
//                                                                        (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//                                }
//                            }

//                            #endregion
//                            #region _A02_structure

//                            else if (current_char == (byte)DataTypes._A02_structure)
//                            {
//                                No_Capture_Instances = 1;
//                            }

//                            #endregion
//                            else
//                            {
//                                // Generate Error And Return
//                                SetAttributeDecodingResult(2, DecodingResult.DecodingError);

//                                throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array) (Error Code:{1})",
//                                                                OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//                            }

//                            bool isSimpleDataTypes = false;

//                            #region Simple Data_Type Check

//                            int array_traverse_T = 0;
//                            try
//                            {
//                                array_traverse_T = array_traverse;

//                                current_char = Data[array_traverse++];
//                                if (current_char != (byte)DataTypes._A02_structure)
//                                {
//                                    throw new DLMSDecodingException(String.Format("{0} Invalid buffer entry structure, Element should be Structure (Error Code:{1})",
//                                                                OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//                                }

//                                int Struct_El_Count = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
//                                DataTypes dt_Type = (DataTypes)Data[array_traverse++];

//                                // Simple DataType Condition 
//                                if (Struct_El_Count == 1 &&
//                                    dt_Type >= DataTypes._A03_boolean &&
//                                    dt_Type <= DataTypes._A24_Float64)
//                                {
//                                    isSimpleDataTypes = true;
//                                }
//                                else // Complex DataType Expected
//                                    isSimpleDataTypes = false;

//                            }
//                            catch { }
//                            finally
//                            {
//                                array_traverse = array_traverse_T;
//                            }

//                            #endregion

//                            if (isSimpleDataTypes &&
//                                (this.captureObjectsList == null || this.captureObjectsList.Count <= 0))
//                            {
//                                var capture_ObjList = Initialize_SimpleDataTypes_ObjectList(Get_Index.Dummy_CLS01, 0x02);
//                                if (capture_ObjList != null && capture_ObjList.Count > 0)
//                                    this.captureObjectsList = capture_ObjList;
//                            }

//                            // No Of Capture Instances
//                            InitializeBuffer(No_Capture_Instances);

//                            int bufferIndex = 0;
//                            for (int count = 1; count <= No_Capture_Instances; count++)
//                            {
//                                current_char = Data[array_traverse++];
//                                if (current_char != (byte)DataTypes._A02_structure)
//                                {
//                                    SetAttributeDecodingResult(2, DecodingResult.DecodingError);
//                                    throw new DLMSDecodingException(String.Format("{0} Invalid buffer entry structure,Element should be Structure (Error Code:{1})",
//                                    OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//                                }
//                                int _length = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
//                                Base_Class Decode_Obj = null;

//                                //DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
//                                for (int index = 0; index < _length; index++, bufferIndex++)
//                                {
//                                    try
//                                    {
//                                        Decode_Obj = buffer[bufferIndex];
//                                        Decode_Obj.Decode_Data(ref Data, ref array_traverse, Data.Length);
//                                        // array_traverse = 0;              
//                                        // Array Resized After Decoding
//                                    }
//                                    catch (DLMSDecodingException ex)
//                                    {
//                                        SetAttributeDecodingResult(2, DecodingResult.DecodingError);

//                                        throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer object {0}_{1} (Error Code:{2}).Details {3}",
//                                            buffer[bufferIndex].OBISIndex, buffer[bufferIndex].OBISIndex.OBISIndex,
//                                            (int)DLMSErrors.ErrorDecoding_Type, ex.Message),
//                                            String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);

//                                    }
//                                    catch (Exception ex)
//                                    {

//                                        SetAttributeDecodingResult(2, DecodingResult.DecodingError);

//                                        throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer object {0}_{1} (Error Code:{2}).Details {3}",
//                                        buffer[bufferIndex].OBISIndex, buffer[bufferIndex].OBISIndex.OBISIndex,
//                                        (int)DLMSErrors.ErrorDecoding_Type, ex.Message),
//                                        String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                                    }
//                                }
//                            }
//                            SetAttributeDecodingResult(2, DecodingResult.Ready);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        SetAttributeDecodingResult(2, DecodingResult.DecodingError);
//                        if (ex is DLMSDecodingException ||
//                            ex is DLMSException)
//                            throw ex;
//                        else
//                            throw new DLMSDecodingException(String.Format("Error occurred while decoding buffer {0}_{1} (Error Code:{2}).",
//                                               base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//                                               String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                    }
//                }

//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x03

//                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
//                {
//                    try
//                    {
//                        current_char = Data[array_traverse++];
//                        // null Data
//                        if (current_char == (byte)DataTypes._A00_Null)
//                        {
//                            if (!IsAttribReadable(0x03))
//                                SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
//                            else
//                            {
//                                SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
//                                throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array),invalid identifier (Error Code:{1})",
//                                    OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//                            }
//                        }
//                        else  // Decode Capture Objects
//                        {
//                            if (current_char != (byte)DataTypes._A01_array)
//                            {
//                                // Generate Error and return
//                                SetAttributeDecodingResult(3, DecodingResult.DecodingError);

//                                throw new DLMSDecodingException(String.Format("{0} First element should be 01 (Array) (Error Code:{1})",
//                                    OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_7");
//                            }
//                            if (captureObjectsList == null)
//                                captureObjectsList = new List<CaptureObject>();
//                            else
//                                captureObjectsList.Clear();

//                            // store no of entries
//                            int No_Entries = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
//                            // Decode Each Capture Object
//                            for (int count = 1; count <= No_Entries; count++)
//                            {
//                                CaptureObject captureObj = DecodeCaptureObject(Data, ref array_traverse);
//                                captureObjectsList.Add(captureObj);
//                            }
//                            if (GetSAPEntryDlg != null)    // try to initialize buffer
//                            {
//                                BaseClassCaptureObjectsList = InitializeCaptureBuffer(1);
//                            }
//                            //DecodingAttribute = (byte)restoreDecodingAttribute;
//                            SetAttributeDecodingResult(3, DecodingResult.Ready);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        SetAttributeDecodingResult(3, DecodingResult.DecodingError);
//                        if (ex is DLMSDecodingException ||
//                            ex is DLMSException)
//                            throw ex;
//                        else
//                            throw new DLMSDecodingException(String.Format("Error occurred while decoding capture objects {0}_{1} (Error Code:{2})",
//                                                   base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//                                                   String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                    }
//                }

//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x04

//                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
//                {
//                    try
//                    {
//                        capturePeriod = Convert.ToUInt64(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
//                        SetAttributeDecodingResult(4, DecodingResult.Ready);
//                    }
//                    catch (Exception ex)
//                    {
//                        if (!IsAttribReadable(0x04))
//                            SetAttributeDecodingResult(4, DecodingResult.NoAccess);
//                        else
//                        {
//                            SetAttributeDecodingResult(4, DecodingResult.DecodingError);

//                            if (ex is DLMSDecodingException)
//                                throw ex;
//                            else
//                                throw new DLMSDecodingException(String.Format("Error occurred while decoding capture period {0}_{1} (Error Code:{2})",
//                                                       base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//                                                       String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                        }
//                    }
//                }

//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x05

//                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
//                {
//                    try
//                    {
//                        sortMethod = (SortMethod)Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
//                        SetAttributeDecodingResult(0x05, DecodingResult.Ready);
//                    }
//                    catch (Exception ex)
//                    {
//                        if (!IsAttribReadable(0x05))
//                            SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
//                        else
//                        {
//                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
//                            if (ex is DLMSDecodingException)
//                                throw ex;
//                            else
//                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding Sort Method {0}_{1} (Error Code:{2})",
//                                                                base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//                                                                String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                        }
//                    }
//                }

//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x06
//                if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
//                {
//                    try
//                    {
//                        sortObject = DecodeCaptureObject(Data, ref array_traverse);
//                        SetAttributeDecodingResult(0x06, DecodingResult.Ready);
//                    }
//                    catch (Exception ex)
//                    {
//                        if (!IsAttribReadable(0x06))
//                            SetAttributeDecodingResult(0x06, DecodingResult.Ready);
//                        else
//                        {
//                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
//                            if (ex is DLMSDecodingException)
//                                throw ex;
//                            else
//                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding sort object {0}_{1} (Error Code:{2})",
//                                                                base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//                                                                String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                        }
//                    }
//                }
//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x07
//                if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
//                {
//                    try
//                    {
//                        this.entriesInUse = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
//                        SetAttributeDecodingResult(0x07, DecodingResult.Ready);
//                    }
//                    catch (Exception ex)
//                    {
//                        if (!IsAttribReadable(0x07))
//                            SetAttributeDecodingResult(0x07, DecodingResult.Ready);
//                        else
//                        {
//                            SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
//                            if (ex is DLMSDecodingException)
//                                throw ex;
//                            else
//                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding EntriesInUse {0}_{1} (Error Code:{2})",
//                                                                    base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//                                                                    String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                        }
//                    }
//                }
//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x08

//                if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
//                {
//                    try
//                    {
//                        this.MaxProfileEntries = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
//                        SetAttributeDecodingResult(0x08, DecodingResult.Ready);
//                    }
//                    catch (Exception ex)
//                    {
//                        if (!IsAttribReadable(0x08))
//                            SetAttributeDecodingResult(0x08, DecodingResult.Ready);
//                        else
//                        {
//                            SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);

//                            if (ex is DLMSDecodingException)
//                                throw ex;
//                            else
//                                throw new DLMSDecodingException(String.Format("Error Occurred while decoding MaxProfileEntries {0}_{1} (Error Code:{2})",
//                                                                        base.OBISIndex, base.INDEX, (int)DLMSErrors.ErrorDecoding_Type),
//                                                                        String.Format("Decode_Data_Class_7_{0}", base.INDEX), ex);
//                        }
//                    }
//                }

//                #endregion
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException || ex is DLMSException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error occurred while decoding (Error Code:{0})",
//                                       (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}_{1}",
//                                       OBISIndex, OBISIndex.OBISIndex), ex);
//            }
//            finally
//            {
//                Data = null;
//            }
//        }
//        /// <summary>
//        /// It decodes the Capture object return by the meter
//        /// </summary>
//        /// <param name="Data">Capture Object Received from network</param>
//        /// <param name="array_traverse">off-set</param>
//        /// <returns><see cref="CaptureObject"/>
//        /// </returns>
//        private CaptureObject DecodeCaptureObject(byte[] Data, ref int array_traverse)
//        {
//            try
//            {
//                byte[] toCompare = null;
//                byte[] tArray = null;
//                ///<DataType Structure, 4>
//                toCompare = new byte[] { (byte)DataTypes._A02_structure, 4 };
//                tArray = (byte[])Array.CreateInstance(typeof(byte), 2);
//                Array.Copy(Data, array_traverse, tArray, 0, tArray.Length);
//                array_traverse += 2;
//                if (!tArray.SequenceEqual<byte>(toCompare))
//                {
//                    SetAttributeDecodingResult(3, DecodingResult.DecodingError);
//                    throw new DLMSDecodingException(String.Format("Invalid Structure of capture objects structure,invalid identifier (Error Code:{1})",
//                        (int)DLMSErrors.Invalid_Type_MisMatch), String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//                }
//                ///Class ID
//                CaptureObject captureObj = new CaptureObject();
//                ValueType T = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length);
//                captureObj.ClassId = Convert.ToUInt16(T);
//                ///OBISCode
//                captureObj.OBISCode = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
//                if (captureObj.OBISCode == null || captureObj.OBISCode.Length != 6)
//                {
//                    SetAttributeDecodingResult(3, DecodingResult.DecodingError);
//                    throw new DLMSDecodingException(String.Format("Invalid OBISCode of capture object received (Error Code:{0})",
//                        (int)DLMSErrors.Invalid_OBISCode), String.Format("Decode_Data_Class_7_{0}", base.INDEX));
//                }
//                ///Attribute Index
//                T = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length);
//                captureObj.AttributeIndex = Convert.ToByte(T);
//                ///Data Index
//                T = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length);
//                captureObj.DataIndex = Convert.ToUInt16(T);
//                return captureObj;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error occurred while decoding Capture Object (Error Code:{0})",
//                                           (int)DLMSErrors.ErrorDecoding_Type), String.Format("Decode_Data_Class_7_{0}_{1}",
//                                           OBISIndex, OBISIndex.OBISIndex), ex);
//            }
//        }

//        /// <summary>
//        /// Encode Capture object to send to meter
//        /// </summary>
//        /// <param name="obj">Capture Object</param>
//        /// <returns>Encoded Capture Object in Byte[] compatible to the network stream</returns>
//        public static byte[] EncodeCaptureObject(CaptureObject obj)
//        {
//            try
//            {
//                List<byte> encodedRaw = new List<byte>(15);
//                ///<DataType Structure, 4>
//                encodedRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });
//                ///Class_Id
//                ///<DataType _A12_long_unsigned,Value>
//                encodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, obj.ClassId));
//                ///OBIS_CODE
//                ///<DataType Octect_String, OBISCOde>
//                encodedRaw.AddRange(BasicEncodeDecode.Encode_OctetString(obj.OBISCode, DataTypes._A09_octet_string));
//                ///Attribute_Index
//                ///<DataType _A12_integer,Value>
//                encodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, obj.AttributeIndex));
//                ///Data_Index
//                ///<DataType _A12_long_unsigned,Value>
//                encodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, obj.DataIndex));
//                return encodedRaw.ToArray();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSException || ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error occurred while encoding capture object (Error Code:{0})",
//                    (int)DLMSErrors.ErrorEncoding_Type), String.Format("Encode_CaptureObject_Class_7"));
//            }
//        }

//        /// <summary>
//        /// Set the Request Encoder
//        /// </summary>
//        /// <returns>byte[]</returns>
//        public override byte[] Encode_Data()
//        {
//            try
//            {
//                EncodedRaw = new List<byte>(0x0A);
//                byte[] t_Array = null;
//                //------------------------------------------------------
//                EncoderAttribute_0();
//                EncoderLogicalName();
//                //------------------------------------------------------
//                #region Attribute 0x02 Buffer

//                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
//                {
//                    bool IsWriteable = IsAttribWritable(0x02);

//                    if (EncodingAttribute == 0x00 && !IsWriteable)
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    }
//                    else if (!IsWriteable)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
//                            (int)DLMSErrors.Insufficient_Priviledge),
//                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                    // Encode Here Data
//                    else if (EncodingAttribute == 0x02)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,encoder not implemented yet (Error Code:{0})",
//                            (int)DLMSErrors.Invalid_EncoderNotIncluded), String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                }

//                #endregion
//                //------------------------------------------------------ 
//                #region Attribute 0x03 CaptureObjectList

//                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
//                {
//                    bool IsWriteable = IsAttribWritable(0x03);

//                    if (EncodingAttribute == 0x00 && !IsWriteable)
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    }
//                    else if (!IsWriteable)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
//                            (int)DLMSErrors.Insufficient_Priviledge),
//                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                    // Encode Here Data
//                    else if (EncodingAttribute == 0x03)
//                    {
//                        if (!this.IsCaptureObjectListIntialized)
//                            throw new DLMSEncodingException(String.Format("Unable to encode CaptureObject List,not initialized properly (Error Code:{0})"
//                                , (int)DLMSErrors.ErrorEncoding_Type), String.Format("Encode_Data_Class_7_{0}", base.INDEX));

//                        EncodedRaw.Add((byte)DataTypes._A01_array);
//                        byte[] lengthArray = null;
//                        BasicEncodeDecode.Encode_Length(ref lengthArray, (ushort)captureObjectsList.Count);
//                        EncodedRaw.AddRange(lengthArray);
//                        foreach (var item in captureObjectsList)
//                        {
//                            EncodedRaw.AddRange(EncodeCaptureObject(item));
//                        }
//                    }
//                }

//                #endregion
//                //------------------------------------------------------ 
//                #region Attribute 0x04 Capture Period
//                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
//                {
//                    bool IsWriteable = IsAttribWritable(0x04);

//                    if (EncodingAttribute == 0x00 && !IsWriteable)
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    }
//                    else if (!IsWriteable)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
//                            (int)DLMSErrors.Insufficient_Priviledge),
//                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                    ///Encode Here Data
//                    else if (EncodingAttribute == 0x04)
//                    {
//                        ///<DataType double_long_unsigned, Value>
//                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, this.capturePeriod));
//                    }
//                }
//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x05 Sort Method
//                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
//                {
//                    bool IsWriteable = IsAttribWritable(0x05);

//                    if (EncodingAttribute == 0x00 && !IsWriteable)
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    }
//                    else if (!IsWriteable)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
//                            (int)DLMSErrors.Insufficient_Priviledge),
//                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                    ///Encode Here Data
//                    else if (EncodingAttribute == 0x05)
//                    {
//                        ///<DataType enum, Value>
//                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, this.sortMethod));
//                    }
//                }
//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x06 Sort Object

//                if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
//                {
//                    bool IsWriteable = IsAttribWritable(0x06);

//                    if (EncodingAttribute == 0x00 && !IsWriteable)
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    }
//                    else if (!IsWriteable)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
//                                                        (int)DLMSErrors.Insufficient_Priviledge),
//                                                        String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                    ///Encode Here Data
//                    else if (EncodingAttribute == 0x06)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode Sort object,Encoder Not Available (Error Code:{0})",
//                            (int)DLMSErrors.Invalid_EncoderNotIncluded),
//                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                }

//                #endregion
//                //------------------------------------------------------ 
//                #region Attribute 0x07 entries_in_use
//                if (EncodingAttribute == 0x07 || EncodingAttribute == 0x00)
//                {
//                    bool IsWriteable = IsAttribWritable(0x07);

//                    if (EncodingAttribute == 0x00 && !IsWriteable)
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    }
//                    else if (!IsWriteable)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
//                                                        (int)DLMSErrors.Insufficient_Priviledge),
//                                                        String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                    // Encode Here Data
//                    else if (EncodingAttribute == 0x07)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode entreis_in_use,Encoder Not Available (Error Code:{0})",
//                            (int)DLMSErrors.Invalid_EncoderNotIncluded),
//                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                }
//                #endregion
//                //------------------------------------------------------
//                #region Attribute 0x08 profile_entries

//                if (EncodingAttribute == 0x08 || EncodingAttribute == 0x00)
//                {
//                    bool IsWriteable = IsAttribWritable(0x08);

//                    if (EncodingAttribute == 0x00 && !IsWriteable)
//                    {
//                        EncodedRaw.Add((byte)DataTypes._A00_Null);
//                    }
//                    else if (!IsWriteable)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
//                                                        (int)DLMSErrors.Insufficient_Priviledge),
//                                                        String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                    // Encode Here Data
//                    else if (EncodingAttribute == 0x08)
//                    {
//                        throw new DLMSEncodingException(String.Format("Unable to encode Profile_Entries,Encoder Not Available (Error Code:{0})",
//                            (int)DLMSErrors.Invalid_EncoderNotIncluded),
//                            String.Format("Encode_Data_Class_7_{0}", base.INDEX));
//                    }
//                }

//                #endregion
//                //------------------------------------------------------
//                byte[] dtBuf = EncodedRaw.ToArray<byte>();
//                EncodedRaw = null;
//                return dtBuf;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                {
//                    throw ex;
//                }
//                else
//                    throw new DLMSEncodingException(String.Format("Error occurred while encoding data (Error Code:{0})",
//                                                   OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.ErrorEncoding_Type), "Encode_Data_Class_7_", ex);
//            }
//        }

//        #endregion

//        #region Initial Support

//        /// <summary>
//        /// This Method initialize the Capture Object Buffer with respect to specific Instance if the Class_7
//        /// </summary>
//        /// <param name="instanceId">specific object identified with an ID</param>
//        /// <returns>Base_Class[] (Initialized buffer of the Capture Objects)</returns>
//        public Base_Class[] InitializeCaptureBuffer(ushort instanceId)
//        {
//            try
//            {
//                if (captureObjectsList == null || captureObjectsList.Count <= 0)
//                    throw new DLMSException(String.Format("Error occurred while initializing capture *(captureObjectsList not initialize) objects_Class_7_{0}", base.INDEX));
//                int captureObjectCount = captureObjectsList.Count;
//                int captureListIndex = 0;
//                Base_Class[] _baseClassCaptureObjectsList = new Base_Class[captureObjectCount];
//                foreach (var item in this.captureObjectsList)
//                {
//                    StOBISCode OBISCodeTmp = StOBISCode.ConvertFrom(item.OBISCode);
//                    OBISCodeTmp.ClassId = item.ClassId;

//                    if (!OBISCodeTmp.IsValidate)
//                    {
//                        String OBIS_STR = DLMS_Common.ArrayToHexString(item.OBISCode);

//                        throw new DLMSException(String.Format("OBIS Code {0} not found in SAP Table_Initialize Buffer_Class_7_{1} (Error Code:{2})"
//                            , OBIS_STR, this.INDEX, (int)DLMSErrors.Invalid_OBISCode));
//                    }

//                    Base_Class SapEntity = GetSAPEntryDlg.Invoke(new LRUCache.KeyIndexer(OBISCodeTmp.OBISIndex, KeyIndexer.OwnerId, instanceId));
//                    SapEntity.DecodingAttribute = item.AttributeIndex;
//                    ///Selective Access
//                    _baseClassCaptureObjectsList[captureListIndex++] = SapEntity;
//                }
//                return _baseClassCaptureObjectsList;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSException)
//                    throw ex;
//                else
//                    throw new DLMSException(String.Format("Error initializing capture objects_Class_7_{0},{1}", base.INDEX, ex.Message), ex);
//            }
//        }

//        /// <summary>
//        /// Initialize the CaptureObjectList in CaptureObject List
//        /// </summary>
//        /// <param name="captureObjectList"></param>

//        public void LoadCaptureBuffer(Base_Class[] captureObjects)
//        {
//            try
//            {
//                if (captureObjects == null || captureObjects.Length <= 0)
//                    throw new DLMSException(String.Format("Error occurred while loading capture List  objects_Class_7_{0}", base.INDEX));
//                captureObjectsList.Clear();
//                foreach (var item in captureObjects)
//                {
//                    StOBISCode OBISCodeTmp = StOBISCode.ConvertFrom(item.OBIS_CODE);
//                    OBISCodeTmp.ClassId = item.Class_ID;

//                    if (!OBISCodeTmp.IsValidate)
//                    {
//                        String OBIS_STR = DLMS_Common.ArrayToHexString(item.OBIS_CODE);
//                        throw new DLMSException(String.Format("OBIS Code {0} not found in SAP Table_Initialize Buffer_Class_7_{1} (Error Code:{2})"
//                            , OBIS_STR, this.INDEX, (int)DLMSErrors.Invalid_OBISCode));
//                    }

//                    CaptureObject obj = new CaptureObject();
//                    obj.ClassId = item.Class_ID;
//                    obj.OBISCode = item.OBIS_CODE;
//                    obj.AttributeIndex = item.DecodingAttribute;
//                    obj.DataIndex = 0;      // Retrieve Whole Attribute
//                    captureObjectsList.Add(obj);
//                }
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSException)
//                    throw ex;
//                else
//                    throw new DLMSException(String.Format("Error loading capture objects_Class_7_{0}", base.INDEX));
//            }
//        }
//        /// <summary>
//        /// Initialize capture object list
//        /// </summary>
//        /// <param name="NoCaputeInstance">no of capture objects received</param>
//        public void InitializeBuffer(int NoCaputeInstance)
//        {
//            try
//            {
//                //if (!IsCaptureObjectListIntialized)
//                //    throw new DLMSException(String.Format("Unable to initialize the buffer,Capture Objects List not valid_Class_7_{0}", this.INDEX));
//                if (AccessSelector != null && !AccessSelector.IsValid())
//                    throw new DLMSException(String.Format("Unable to initialize the buffer,invalid access selection_Class_7_{0}", this.INDEX));
//                int horizentalSelectionCount = 0;
//                if (AccessSelector == null)
//                {
//                    buffer.Clear();
//                    for (int captureInstanceId = 1; captureInstanceId <= NoCaputeInstance; captureInstanceId++)
//                        buffer.AddRange(InitializeCaptureBuffer((ushort)captureInstanceId));

//                    // for (int index = 0; index < horizentalSelectionCount; index++)
//                    // {
//                    //     buffer.AddRange(Init (Base_Class)baseClassCaptureObjectsList[index].Clone());
//                    // }

//                }
//                else if (AccessSelector.GetSelectorType() == SelectiveAccessType.Entry_Descripter)
//                {
//                    EntryDescripter Selector = (EntryDescripter)AccessSelector;
//                    int maxHorSelector = (Selector.ToSelectedValue == EntryDescripter.MaxPossibleValue) ? captureObjectsList.Count : Selector.ToSelectedValue;
//                    horizentalSelectionCount = (maxHorSelector - Selector.ToSelectedValue);
//                    buffer.Clear();
//                    for (int captureInsatnceId = 1; captureInsatnceId <= NoCaputeInstance; captureInsatnceId++)
//                    {
//                        Base_Class[] tArray = InitializeCaptureBuffer((ushort)captureInsatnceId);
//                        ///Add Only Selected Values Only
//                        for (int index = Selector.FromSelectedValue - 1; index < (maxHorSelector); index++)
//                        {
//                            buffer.Add((Base_Class)tArray[index]);
//                        }
//                    }
//                }
//                else if (AccessSelector.GetSelectorType() == SelectiveAccessType.Range_Descripter)
//                {
//                    {
//                        RangeDescripter Selector = (RangeDescripter)AccessSelector;
//                        //int maxHorSelector = (Selector.ToSelectedValue == RangeDescripter.MaxPossibleValue) ? captureObjectsList.Count : Selector.ToSelectedValue;
//                        //horizentalSelectionCount = (maxHorSelector - Selector.ToSelectedValue);
//                        buffer.Clear();
//                        for (int captureInsatnceId = 1; captureInsatnceId <= NoCaputeInstance; captureInsatnceId++)
//                        {
//                            Base_Class[] tArray = InitializeCaptureBuffer((ushort)captureInsatnceId);
//                            ///Add Only Selected Values Only
//                            for (int index = 0; index < (tArray.Length); index++)
//                            {
//                                buffer.Add((Base_Class)tArray[index]);
//                            }
//                        }
//                        //throw new DLMSException(String.Format("Unable to initialize the buffer,RangeDescripter AccessSelector not supported_Class_7_{0}",
//                        //                       this.INDEX));
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSException)
//                    throw ex;
//                else
//                    throw new DLMSException(String.Format("Error initializing Capture Buffer_Data_Class_7_{0}_{1}", base.INDEX, ex.Message));
//            }
//        }

//        public List<CaptureObject> Initialize_SimpleDataTypes_ObjectList(StOBISCode OBISCodes, byte AttributeIndex = 0x02)
//        {
//            List<CaptureObject> listToReturn = new List<CaptureObject>();

//            CaptureObject Obj_1 = new CaptureObject();
//            // StOBISCode OBIS = OBISCodes;
//            Obj_1.ClassId = OBISCodes.ClassId;
//            Obj_1.AttributeIndex = 0x02;
//            Obj_1.DataIndex = 0;
//            Obj_1.OBISCode = OBISCodes.OBISCode;
//            listToReturn.Add(Obj_1);

//            return listToReturn;
//        }

//        #endregion

//        #region Member Methods

//        public override object Clone()
//        {
//            Class_7 cloned = new Class_7(this);
//            if (cloned.buffer != null && cloned.buffer.Count > 0)
//                cloned.buffer.Clear();
//            return cloned;
//        }

//        /// <summary>
//        /// Returns the String Representation of the Class_7 object
//        /// </summary>
//        /// <returns>String</returns>
//        public override string ToString()
//        {
//            String baseStr = base.ToString();
//            StringBuilder strVal = new StringBuilder(1000);

//            if (buffer != null && buffer.Count > 0 && GetAttributeDecodingResult(2) == DecodingResult.Ready)    ///Buffer Decoded Info
//            {
//                // Compute buffer Capture Instance Size
//                int captureInstanceSize = buffer.FindIndex(1, (x) => x.OBIS_CODE.SequenceEqual<byte>(buffer[0].OBIS_CODE));

//                if (captureInstanceSize == -1)
//                    captureInstanceSize = buffer.Count;
//                strVal.AppendFormat(",Buffer Capture Instance Size:{0}", captureInstanceSize);
//                strVal.AppendFormat(",Total Decoded Buffer Objects:{0}:{1},", buffer.Count, GetAttributeDecodingResult(2));

//                // foreach (Base_Class obj in buffer)
//                // {
//                //     strVal.Append(obj.ToString());
//                // }
//            }

//            if (IsCaptureObjectListIntialized && GetAttributeDecodingResult(3) == DecodingResult.Ready) // Buffer Decoded Info
//            {
//                strVal.AppendFormat(",Capture Objects List:{0}:{1},", baseClassCaptureObjectsList.Length, GetAttributeDecodingResult(3));
//                foreach (Base_Class obj in baseClassCaptureObjectsList)
//                {
//                    strVal.Append(String.Format("{0},", obj.INDEX));
//                }
//            }
//            return baseStr + strVal.ToString();
//        }

//        #endregion

//    }

//    #endregion
//}
