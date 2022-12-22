using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Register monitor (class_id: 21, version: 0) allows modeling the function of monitoring of values
    /// This Class allows modeling the function of monitoring of values modeled by Data, Register, Extended register or Demand register objects.
    /// It allows specifying thresholds, the value monitored, and a set of scripts that are executed when the value monitored crosses a threshold.
    /// The Class Register monitor requires an instantiation of the Class Script table in the same logical device.
    /// </summary>
    public class Class_21 : Base_Class
    {
        #region DataMembers

        internal object[] _Thresholds = null;
        internal Base_Class[] _baseClassThresholds = null;
        internal MonitoredValue _MonitoredValue = null;
        internal ActionSet[] _Actions = null;

        internal GetSAPEntryKeyIndex GetSAPEntryDlg;

        #endregion

        #region Properties

        /// <summary>
        /// Provides the threshold values to which the attribute of the referenced register is compared.
        /// </summary>
        public object[] Thresholds
        {
            get { return _Thresholds; }
            set { _Thresholds = value; }
        }

        public Base_Class[] BaseClassThresholds
        {
            get { return _baseClassThresholds; }
            set { _baseClassThresholds = value; }
        }

        /// <summary>
        /// Defines which attribute of an object is to be monitored. Only values with simple data types are allowed.
        /// </summary>
        public MonitoredValue MonitoredValue
        {
            get { return _MonitoredValue; }
            set { _MonitoredValue = value; }
        }

        /// <summary>
        /// Defines the scripts to be executed when the monitored attribute of the referenced object crosses the corresponding threshold. 
        /// The attribute actions has exactly the same number of elements as the attribute thresholds. 
        /// The ordering of the action_items corresponds to the ordering of the thresholds
        /// </summary>
        public ActionSet[] Actions
        {
            get { return _Actions; }
            set { _Actions = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes Representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_21(byte[] Obis_Code, byte Attribute_recieved)
            : base(21, 4, 0, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCode">OBIS for desire DLMS/COSEM class instance</param>
        public Class_21(StOBISCode OBISCodeStruct) : base(OBISCodeStruct, 4, 0) { }

        public Class_21(StOBISCode OBISCode, GetSAPEntryKeyIndex GetSAPFEntryDelegate)
            : this(OBISCode)
        {
            this.GetSAPEntryDlg = GetSAPFEntryDelegate;
        }

        /// <summary>
        /// Copy Constructor Class_21
        /// </summary>
        /// <param name="obj"></param>
        public Class_21(Class_21 obj)
            : base(obj)
        {
            if (obj.Thresholds == null ||
                obj.Thresholds.Length <= 0)
            {
                this.Thresholds = new object[0];
            }
            else
            {
                // Copy Thresholds
                this.Thresholds = new object[obj.Thresholds.Length];
                for (int indexer = 0; indexer < obj.Thresholds.Length; indexer++)
                    this.Thresholds[indexer] = obj.Thresholds[indexer];
            }

            // null Base_Class Struct
            if (obj.BaseClassThresholds == null ||
                obj.BaseClassThresholds.Length <= 0)
            {
                this.BaseClassThresholds = new Base_Class[0];
            }
            else
            {
                // Copy Base_Class
                this.BaseClassThresholds = new Base_Class[obj.BaseClassThresholds.Length];
                for (int indexer = 0; indexer < obj.BaseClassThresholds.Length; indexer++)
                    this.BaseClassThresholds[indexer] = obj.BaseClassThresholds[indexer];
            }

            // Copy Monitored Value
            if (obj.MonitoredValue != null)
                this.MonitoredValue = new MonitoredValue(obj.MonitoredValue);

            if (obj.Actions == null || obj.Actions.Length <= 0)
                this.Actions = new ActionSet[0];
            else
            {
                // Copy Action SET
                this.Actions = new ActionSet[obj.Actions.Length];
                for (int indexer = 0; indexer < obj.Actions.Length; indexer++)
                    this.Actions[indexer] = new ActionSet(obj.Actions[indexer]);
            }
        }

        #endregion

        #region Initialize_Support

        public Base_Class[] InitializeThresholds(GetSAPEntryKeyIndex GetSAPEntryDlg = null, int ThrsholdObjCount = 0)
        {
            try
            {
                if (MonitoredValue == null)
                    throw new DLMSException(String.Format("Error occurred while initializing Thresholds objects_Class_21_{0}", base.INDEX));

                if (Thresholds != null && Thresholds.Length > 0)
                {
                    ThrsholdObjCount = Thresholds.Length;
                }

                int captureListIndex = 0;

                Base_Class[] _baseClassThresholdObjectsList = new Base_Class[ThrsholdObjCount];

                Base_Class SapEntity = null;
                StOBISCode OBISCodeTmp = MonitoredValue.LogicalName;
                byte Attribute = Convert.ToByte(MonitoredValue.AttributeIndex);

                for (int ThItemCount = 1; ThItemCount <= ThrsholdObjCount; ThItemCount++)
                {
                    if (!OBISCodeTmp.IsValidate)
                    {
                        String OBIS_STR = DLMS_Common.ArrayToHexString(OBISCodeTmp.OBISCode);
                        throw new DLMSException(String.Format("OBIS Code {0} not found in SAP Table_InitializeBuffer_Class_21_{1}",
                                                               OBIS_STR, this.INDEX));
                    }

                    if (GetSAPEntryDlg != null)
                        this.GetSAPEntryDlg = GetSAPEntryDlg;
                    SapEntity = this.GetSAPEntryDlg.Invoke(new LRUCache.KeyIndexer(OBISCodeTmp.OBISIndex,
                                                           KeyIndexer.OwnerId, Convert.ToUInt16(ThItemCount)));

                    #region // Init SapEntity Properties

                    SapEntity.EncodingAttribute = SapEntity.DecodingAttribute = Attribute;
                    SapEntity.EncodingType = SapEntity.DecodingType = MonitoredValue.TargetDataType;
                    SapEntity.InitData_BaseClass();


                    #endregion
                    #region AccessRights For Dummy Objects

                    // OBIS_CODE Name
                    // string STR_Name = OBISCodeTmp.OBISIndex.ToString();

                    // if (OBISCodeTmp.ClassId == 1 &&
                    //     STR_Name.ToLower().Contains("dummy"))
                    // {

                    SapEntity.Grant_Attribute_Rights();

                    // }

                    #endregion

                    // Selective Access
                    _baseClassThresholdObjectsList[captureListIndex++] = SapEntity;
                }
                return _baseClassThresholdObjectsList;
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error initializing Thresholds objects_Class_21_{0},{1}", base.INDEX, ex.Message));
            }
        }

        public void LoadThresholds(Base_Class[] BaseClassThresholds)
        {
            try
            {
                if (MonitoredValue == null ||
                    BaseClassThresholds == null || BaseClassThresholds.Length <= 0)
                    throw new DLMSException(String.Format("Error occurred not initial BaseClassThresholds objects_Class_21_{0}", base.INDEX));

                int indexer = 0;
                foreach (var Data_Obj in BaseClassThresholds)
                {
                    Data_Obj.LoadData_BaseClass(Convert.ToByte(MonitoredValue.AttributeIndex),
                                                MonitoredValue.TargetDataType, Thresholds[indexer++]);
                    // Thresholds[indexer++] = DLMS_Common.StoreData_BaseClass(Data_Obj, Convert.ToByte(MonitoredValue.AttributeIndex));
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error LoadThresholds objects _Class_21_{0},{1}", base.INDEX, ex.Message));
            }
        }

        public void SaveThresholds(Base_Class[] BaseClassThresholds)
        {
            try
            {
                if (MonitoredValue == null ||
                    BaseClassThresholds == null || BaseClassThresholds.Length <= 0)
                    throw new DLMSException(String.Format("Error occurred not initial BaseClassThresholds objects_Class_21_{0}", base.INDEX));

                if (Thresholds == null ||
                    Thresholds.Length != BaseClassThresholds.Length)
                {
                    Thresholds = new object[BaseClassThresholds.Length];
                }

                int indexer = 0;
                foreach (var Data_Obj in BaseClassThresholds)
                {
                    Thresholds[indexer++] = Data_Obj.StoreData_BaseClass(Convert.ToByte(MonitoredValue.AttributeIndex));
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("Error Save Thresholds objects_Class_21_{0},{1}", base.INDEX, ex.Message));
            }
        }

        #endregion

        #region Decoders / Encoders

        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;
            if (DecodingAttribute == 0x00)
            {
                for (int index = 0; index < AccessResults.Length; index++)
                    AccessResults[index] = DecodingResult.DataNotPresent;
            }
            else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
            {
                SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
            }

            //------------------------------------------------------
            try
            {
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_21_Object_Array");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_21_Object_Array");

                #region Attribute 0x02

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check_Access_Rights
                        if (!IsAttribReadable(2))
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                    }
                    else
                    {
                        try
                        {
                            // Save in array
                            DataTypes Recieved_Obj_Type = (DataTypes)current_char;

                            if (Recieved_Obj_Type != DataTypes._A01_array)
                                throw new DLMSDecodingException("_A01_array Data Type Expected", "Decode_Data_Class_21");

                            int arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                            DataTypes arrayType = (DataTypes)Data[array_traverse + 1];
                            // Update Data Type
                            DecodingType = Recieved_Obj_Type;
                            DecodingSubType = arrayType;

                            if (_baseClassThresholds == null || _baseClassThresholds.Length != arrlength)
                            {
                                if (MonitoredValue != null)
                                {
                                    _baseClassThresholds = InitializeThresholds(this.GetSAPEntryDlg, arrlength);
                                }
                                else
                                    throw new DLMSDecodingException("_baseClassThresholds not initialize Properly", "Decode_Data_Class_21");
                            }

                            // Decode Each Threshold Object here
                            foreach (var baseObj in _baseClassThresholds)
                            {
                                baseObj.Decode_Data(ref Data, ref array_traverse, length);
                            }

                            try
                            {
                                SaveThresholds(_baseClassThresholds);
                            }
                            catch (Exception)
                            {
                                throw new DLMSDecodingException("_Thresholds not saved properly", "Decode_Data_Class_21");
                            }

                            SetAttributeDecodingResult(2, DecodingResult.Ready);
                        }
                        catch (Exception)
                        {
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                            throw;
                        }
                    }
                }

                #endregion
                #region Attribute 0x03

                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        //Check access rights
                        if (!IsAttribReadable(3))
                            SetAttributeDecodingResult(3, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(3, DecodingResult.DecodingError);
                    }
                    else
                    {
                        try
                        {
                            // Save in array
                            DataTypes Recieved_Obj_Type = (DataTypes)current_char;
                            current_char = Data[array_traverse++];

                            if (Recieved_Obj_Type != DataTypes._A02_structure || current_char != 03)
                                throw new DLMSDecodingException("_A02_structure With 3 Element is Expected", "Decode_Data_Class_21");

                            var Class_Id = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                            var OBISCode = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                            var attribId = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);

                            // StOBISCode
                            StOBISCode LogicalName = StOBISCode.ConvertFrom(OBISCode as byte[]);
                            LogicalName.ClassId = Convert.ToUInt16(Class_Id);

                            // Attribute Indexer
                            if (_MonitoredValue == null)
                                _MonitoredValue = new MonitoredValue();

                            _MonitoredValue.LogicalName = LogicalName;
                            _MonitoredValue.AttributeIndex = Convert.ToInt32(attribId);

                            SetAttributeDecodingResult(3, DecodingResult.Ready);
                        }
                        catch (Exception)
                        {
                            SetAttributeDecodingResult(3, DecodingResult.DecodingError);
                            throw;
                        }
                    }
                }

                #endregion
                #region Attribute 0x04

                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        //Check access rights
                        if (!IsAttribReadable(4))
                            SetAttributeDecodingResult(4, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(4, DecodingResult.DecodingError);
                    }
                    else
                    {
                        try
                        {
                            DataTypes Recieved_Obj_Type = (DataTypes)current_char;

                            if (Recieved_Obj_Type != DataTypes._A01_array)
                                throw new DLMSDecodingException("_A01_array Data Type Expected", "Decode_Data_Class_21");

                            int arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);

                            if (_Actions == null || _Actions.Length != arrlength)
                                _Actions = new ActionSet[arrlength];

                            ActionSet localActionSet = null;
                            ActionItem localActionItem = null;

                            for (int elmentcount = 1; elmentcount < arrlength; elmentcount++)
                            {
                                localActionSet = new ActionSet();
                                localActionItem = new ActionItem();
                                _Actions[elmentcount - 1] = localActionSet;

                                current_char = Data[array_traverse++];
                                Recieved_Obj_Type = (DataTypes)current_char;
                                current_char = Data[array_traverse++];

                                if (Recieved_Obj_Type != DataTypes._A02_structure || current_char != 02)
                                    throw new DLMSDecodingException("_A02_structure With 2 Element is Expected", "Decode_Data_Class_21");

                                // StOBISCode
                                StOBISCode LogicalName = Get_Index.Dummy_CLS01;
                                #region // Decode Action_Up

                                current_char = Data[array_traverse++];
                                Recieved_Obj_Type = (DataTypes)current_char;
                                current_char = Data[array_traverse + 1];

                                if (Recieved_Obj_Type == DataTypes._A02_structure && current_char == 02)
                                {
                                    array_traverse++;

                                    // var Class_Id = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                                    var OBISCode = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                                    var attribId = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);

                                    LogicalName = StOBISCode.ConvertFrom(OBISCode as byte[]);
                                    LogicalName.ClassId = 01;

                                    localActionItem.LogicalName = LogicalName;
                                    localActionItem.ScriptSelector = Convert.ToUInt16(attribId);
                                    localActionSet.ActionUp = localActionItem;
                                }
                                else if (Recieved_Obj_Type == DataTypes._A00_Null)
                                {
                                    localActionSet.ActionUp = localActionItem;
                                }
                                else
                                    throw new DLMSDecodingException("ActionItem _A02_structure With 2 Element is Expected", "Decode_Data_Class_21");

                                #endregion

                                // StOBISCode
                                LogicalName = Get_Index.Dummy_CLS01;
                                localActionItem = new ActionItem();
                                #region // Decode Action_Down

                                current_char = Data[array_traverse++];
                                Recieved_Obj_Type = (DataTypes)current_char;
                                current_char = Data[array_traverse + 1];

                                if (Recieved_Obj_Type == DataTypes._A02_structure && current_char == 02)
                                {
                                    array_traverse++;

                                    // var Class_Id = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);
                                    var OBISCode = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                                    var attribId = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse);

                                    LogicalName = StOBISCode.ConvertFrom(OBISCode as byte[]);
                                    LogicalName.ClassId = 01;

                                    localActionItem.LogicalName = LogicalName;
                                    localActionItem.ScriptSelector = Convert.ToUInt16(attribId);
                                    localActionSet.ActionDown = localActionItem;
                                }
                                else if (Recieved_Obj_Type == DataTypes._A00_Null)
                                {
                                    localActionSet.ActionDown = localActionItem;
                                }
                                else
                                    throw new DLMSDecodingException("ActionItem _A02_structure With 2 Element is Expected", "Decode_Data_Class_21");

                                #endregion
                            }

                            SetAttributeDecodingResult(4, DecodingResult.Ready);
                        }
                        catch (Exception)
                        {
                            SetAttributeDecodingResult(4, DecodingResult.DecodingError);
                            throw;
                        }
                    }
                }

                #endregion

                // make data array ready for upcoming objects
                // DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding data", "Decode_Data_Class_21_Object_Number", ex);
            }

        }

        public override byte[] Encode_Data()
        {
            try
            {
                EncodedRaw = new List<byte>(0x0A);
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------
                #region Attribute 0x02

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00
                        && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access", EncodingType), "EncodeData_Class_21");
                    }
                    else   // Encode Here Data
                    {
                        if (_baseClassThresholds == null || _baseClassThresholds.Length < 0)
                            throw new DLMSEncodingException(String.Format("Unable to encode,{0} Not Initialize BaseClassThresholds", EncodingType), "EncodeData_Class_21");

                        // Encode Array
                        byte[] ArrTLength = null;
                        BasicEncodeDecode.Encode_Length(ref ArrTLength, Convert.ToUInt16(_baseClassThresholds.Length));

                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        EncodedRaw.AddRange(ArrTLength);

                        foreach (var clsbaseTh in _baseClassThresholds)
                        {
                            if (clsbaseTh == null)
                                throw new DLMSEncodingException(String.Format("Unable to encode,{0} Not Initialize BaseClassThresholds", EncodingType), "EncodeData_Class_21");

                            EncodedRaw.AddRange(clsbaseTh.Encode_Data());
                        }
                    }
                }

                //------------------------------------------------------ 
                #endregion
                //------------------------------------------------------
                #region Attribute 0x03

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);

                    if (EncodingAttribute == 0x00
                        && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access", EncodingType), "EncodeData_Class_21");
                    }
                    else // Encode Here Data
                    {
                        if (MonitoredValue == null || !MonitoredValue.LogicalName.IsValidate)
                            throw new DLMSEncodingException(String.Format("Unable to encode,{0} Not Initialize MonitoredValue",
                                                            EncodingType), "EncodeData_Class_21");

                        EncodedRaw.Add((byte)DataTypes._A02_structure);
                        EncodedRaw.Add(0x03);

                        // Encode Class Id
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, MonitoredValue.ObjectType));
                        // Encode OBIS Code
                        EncodedRaw.AddRange(BasicEncodeDecode.Encode_OctetString(MonitoredValue.LogicalName.OBISCode, DataTypes._A09_octet_string));
                        // Encode Attribute Index
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, MonitoredValue.AttributeIndex));
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04

                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);

                    if (EncodingAttribute == 0x00
                        && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access", EncodingType), "EncodeData_Class_21");
                    }
                    else // Encode Here Data
                    {
                        if (Actions == null || Actions.Length <= 0)
                            throw new DLMSEncodingException(String.Format("Unable to encode,{0} Not Initialize ActionSet",
                                                            EncodingType), "EncodeData_Class_21");

                        // Encode Array
                        byte[] ArrTLength = null;
                        BasicEncodeDecode.Encode_Length(ref ArrTLength, Convert.ToUInt16(Actions.Length));

                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        EncodedRaw.AddRange(ArrTLength);

                        foreach (var actSET in Actions)
                        {
                            if (actSET == null ||
                                (actSET.ActionUp == null &&
                                 actSET.ActionDown == null))
                            {
                                EncodedRaw.Add((byte)DataTypes._A00_Null);
                            }
                            else
                            {
                                EncodedRaw.Add((byte)DataTypes._A02_structure);
                                EncodedRaw.Add(0x02);
                            }

                            #region Action Up Encode

                            if (actSET.ActionUp == null)
                                EncodedRaw.Add((byte)DataTypes._A00_Null);
                            else
                            {
                                // StOBISCode
                                StOBISCode LogicalName = Get_Index.Dummy_CLS01;

                                EncodedRaw.Add((byte)DataTypes._A02_structure);
                                EncodedRaw.Add(0x02);

                                // OBIS Code
                                EncodedRaw.AddRange(BasicEncodeDecode.Encode_OctetString(actSET.ActionUp.LogicalName.OBISCode, DataTypes._A09_octet_string));
                                // Action Selector
                                EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, actSET.ActionUp.ScriptSelector));
                            }

                            #endregion
                            #region Action Down Encode

                            if (actSET.ActionDown == null)
                                EncodedRaw.Add((byte)DataTypes._A00_Null);
                            else
                            {
                                // StOBISCode
                                StOBISCode LogicalName = Get_Index.Dummy_CLS01;

                                EncodedRaw.Add((byte)DataTypes._A02_structure);
                                EncodedRaw.Add(0x02);

                                // OBIS Code
                                EncodedRaw.AddRange(BasicEncodeDecode.Encode_OctetString(actSET.ActionDown.LogicalName.OBISCode, DataTypes._A09_octet_string));
                                // Action Selector
                                EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, actSET.ActionDown.ScriptSelector));
                            }

                            #endregion
                        }
                    }
                }

                #endregion

                byte[] dtBuf = EncodedRaw.ToArray<byte>();
                EncodedRaw = null;
                return dtBuf;
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}", "Error occurred while encoding data", "Encode_Data_Class_21"), ex);
            }
        }

        #endregion;

        #region Member Methods

        /// <summary>
        /// Returns the String representation of the Class_1 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            strVal.AppendFormat(",Monitor Value:{0}:{1}", MonitoredValue, GetAttributeDecodingResult(3));
            if (Thresholds != null && Thresholds.Length > 0)
            {
                foreach (var thItem in Thresholds)
                {
                    strVal.AppendFormat("{0},", thItem);
                }
                strVal.AppendFormat(":Thresholds {0}", GetAttributeDecodingResult(2));
            }

            return baseStr + strVal;
        }

        public override object Clone()
        {
            Class_21 cloned = new Class_21(this);
            return cloned;
        }

        #endregion
    }
}
