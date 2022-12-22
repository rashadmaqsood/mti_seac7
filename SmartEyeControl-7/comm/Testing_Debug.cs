using System;
using System.Collections.Generic;
using System.Linq;
using DLMS;
using System.Windows.Forms;
using SharedCode.Common;
using SEAC.Common;
using DLMS.Comm;
using System.Text;
using System.Collections;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using System.ComponentModel;
using AccurateOptocomSoftware.Common;

namespace AccurateOptocomSoftware.comm
{
    public class Testing_Debug
    {
        private List<string> AttributeToClassId = null;
        private bool readWriteOPT;
        private bool onlyWritable = true;
        private BindingList<Test_DataObject> _ObjectsList = null;

        public bool ReadWriteOPT
        {
            get { return readWriteOPT; }
            set { readWriteOPT = value; }
        }

        public bool OnlyWritable
        {
            get { return onlyWritable; }
            set { onlyWritable = value; }
        }

        public BindingList<Test_DataObject> ObjectsList
        {
            get { return _ObjectsList; }
            set { _ObjectsList = value; }
        }

        public List<Test_DataObject> GetTestObjects(StOBISCode Obis)
        {
            List<Test_DataObject> TestDataObjs = new List<Test_DataObject>(5);

            try
            {
                foreach (var testDataObj in _ObjectsList)
                {
                    if (testDataObj.OBIS_Code == Obis &&
                       !TestDataObjs.Contains(testDataObj))
                        TestDataObjs.Add(testDataObj);
                }

                return TestDataObjs;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occurred while get Test_DataObject {0}", Obis), ex);
            }
        }

        public List<List<Test_DataObject>> GetAllTestObjects()
        {
            Dictionary<StOBISCode, List<Test_DataObject>> AllTestDataObjs = new Dictionary<StOBISCode, List<Test_DataObject>>(512);
            List<Test_DataObject> TestDataObjs = null;

            try
            {
                StOBISCode currentOBISCode = Get_Index.Dummy;

                foreach (var _testDataObj in _ObjectsList)
                {
                    currentOBISCode = _testDataObj.OBIS_Code;

                    if (!AllTestDataObjs.ContainsKey(currentOBISCode))
                    {
                        TestDataObjs = new List<Test_DataObject>(06);
                        AllTestDataObjs.Add(currentOBISCode, TestDataObjs);
                    }
                    else
                        TestDataObjs = AllTestDataObjs[currentOBISCode];

                    if (_testDataObj == null)
                        continue;

                    if (!TestDataObjs.Contains(_testDataObj))
                        TestDataObjs.Add(_testDataObj);
                }

                return AllTestDataObjs.Values.ToList<List<Test_DataObject>>();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occurred while GetAllTestObjects"), ex);
            }
        }


        public void AddTestObject(List<StOBISCode> SelectedObis, List<String> SelectObjectLabels)
        {
            if (_ObjectsList == null)
                _ObjectsList = new BindingList<Test_DataObject>();

            string LabelSTR = string.Empty;
            StOBISCode Object_ObisCode = Get_Index.Dummy;

            for (int indexer = 0; indexer < SelectedObis.Count &&
                                  indexer < SelectObjectLabels.Count; indexer++)
            {
                try
                {
                    LabelSTR = SelectObjectLabels[indexer];
                    Object_ObisCode = SelectedObis[indexer];

                    AddTestObject(Object_ObisCode, LabelSTR);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public void SortTestDataItems()
        {
            if (_ObjectsList != null && _ObjectsList.Count > 0)
                // Sort _ObjectList based on OBIS Code & Attribute Value
                _ObjectsList.Sort<Test_DataObject>((x, y) =>
                {
                    int return_Result = -1;
                    try
                    {
                        string x_CMP = string.Format("{0}_{1}", x.OBIS_Code.OBIS_Value, x.AttributeId);
                        string y_CMP = string.Format("{0}_{1}", y.OBIS_Code.OBIS_Value, y.AttributeId);

                        return_Result = string.CompareOrdinal(x_CMP, y_CMP);
                    }
                    catch
                    {
                        // Donot Raise Error
                    }

                    return return_Result;
                });
        }

        public void RemoveTestObject(StOBISCode Obis)
        {
            List<Test_DataObject> TestDataObjs = null;

            try
            {
                TestDataObjs = GetTestObjects(Obis);

                foreach (var testDataObj in TestDataObjs)
                {
                    _ObjectsList.Remove(testDataObj);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occurred while remove Test_DataObject {0}", Obis), ex);
            }
        }

        public void AddTestObject(StOBISCode Obis, string ObjectLabelArg = "")
        {
            AttributeToClassId = null;
            try
            {
                AttributeToClassId = Class_ID_Structures.Class_Attribute_Names(Obis.ClassId);

                if (AttributeToClassId == null || AttributeToClassId.Count < 0)
                {
                    throw new Exception("Invalid OBIS Code & Class Id to initiailze");
                }

                if (!string.Equals("Logical_Name", AttributeToClassId[0], StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Invalid OBIS Code & Class Id to initiailze");

                byte attributeId = 1;
                foreach (var attributeLabel in AttributeToClassId)
                {
                    Test_DataObject testData = new Test_DataObject()
                    {
                        OBIS_Code = Obis,
                        ObjectLabel = ObjectLabelArg,
                        AttributeId = attributeId,
                        Label = attributeLabel
                    };

                    if (_ObjectsList.Contains(testData, testData))
                    {
                        // _ObjectsList.Remove(testData);
                        continue;
                    }
                    _ObjectsList.Add(testData);

                    if (attributeId == 1 &&
                        attributeLabel.Contains("Logical_Name"))
                    {
                        testData.TestStatus = testData.TestStatus | TestStatus.UserCancel;
                    }

                    attributeId++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occurred add Test_DataObject {0}", Obis), ex);
            }
        }

        public void ResetTestObjects()
        {
            if (_ObjectsList != null)
                foreach (var testObj in _ObjectsList)
                {
                    testObj.Reset();
                }
        }

        public void ClearTestObjects()
        {
            if (_ObjectsList != null)
                _ObjectsList.Clear();
        }

        public void UpdateTestObject(StOBISCode Obis, byte AttributeId, double d, string s,
                                     List<byte> valueArray, TypeOfValue val_type)
        {
            Test_DataObject objToAdd = new Test_DataObject();

            var testObjs = GetTestObjects(Obis);

            if (testObjs != null && testObjs.Count > 0)
            {
                foreach (var tObj in testObjs)
                {
                    if (tObj.OBIS_Code == Obis && tObj.AttributeId == AttributeId)
                    {
                        objToAdd = tObj;
                    }
                }
            }

            objToAdd.TimeStamp = DateTime.Now;
            objToAdd.Double_Value = d;
            objToAdd.String_Value = s;
            objToAdd.Value_Array = valueArray;

            objToAdd.Value_Type = val_type;
        }

        public void showList(DataGridView grid)
        {
            int currentRow = 0;
            // Clear All Prev Data
            // DataGridViewRow dgvRow = null;

            try
            {
                grid.DataSource = null;
                grid.AutoGenerateColumns = false;
                // grid.Rows.Clear();
                grid.DataSource = ObjectsList;
                grid.Show();

                #region Commented__Code__Section

                // foreach (Test_DataObject item in ObjectsList)
                // {
                //     grid.Rows.Add();
                //     dgvRow = grid.Rows[grid.Rows.Count - 1];
                //   
                //     // Row Header
                //     dgvRow.HeaderCell.Value = ++currentRow;
                //     dgvRow.Cells["dtTimeStamp"].Value = item.TimeStamp.ToShortTimeString();
                //     dgvRow.Cells["stOBISCode"].Value = item.OBIS_Code.ToString(StOBISCode.FormatSpecifier.CompleteHexMode);
                //     dgvRow.Cells["OBISLabel"].Value = item.Label;
                //     dgvRow.Cells["AttributeId"].Value = item.AttributeId;
                //     dgvRow.Cells["testStatus"].Value = item.TestStatusSTR;
                //   
                //     dgvRow.Cells["Attribute_Value"].Value = "NA";
                //     if (item.Value_Type == TypeOfValue.Double_value)
                //     {
                //         string temp = App_Common.value_to_string(item.Double_Value);
                //         dgvRow.Cells["Attribute_Value"].Value = temp;
                //     }
                //     else if (item.Value_Type == TypeOfValue.string_value &&
                //              !string.IsNullOrEmpty(item.String_Value))
                //     {
                //         dgvRow.Cells["Attribute_Value"].Value = item.String_Value;
                //     }
                //   
                //     currentRow++;
                //     grid.FirstDisplayedScrollingRowIndex = grid.Rows.Count - 1;
                //   
                //     if (currentRow % 5 == 0 || currentRow % 7 == 0)
                //         Application.DoEvents();
                // } 

                #endregion
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Data Display Error", ex.Message);
            }
        }

        public enum TypeOfValue : byte
        {
            Double_value = 1,
            string_value,
            byte_array
        }

        /// <summary>
        /// To Perform COSEM Objects Decoding & Store Values In Test_DataObject 
        /// </summary>
        /// <param name="DataObj"></param>
        /// <param name="Data_Obj"></param>
        public bool TryDecode_TestDebugData(List<Test_DataObject> DataObj, Base_Class Data_Obj)
        {
            bool isValueRecovered = false;
            List<byte> data_Recovered = null;
            byte[] raw_dataRecovered = null;

            Test_DataObject Current_DataObj = null;

            #region Declare Attribute_Data

            Test_DataObject Attribute_2 = null;
            Test_DataObject Attribute_3 = null;
            Test_DataObject Attribute_4 = null;
            Test_DataObject Attribute_5 = null;
            Test_DataObject Attribute_6 = null;
            Test_DataObject Attribute_7 = null;
            Test_DataObject Attribute_8 = null;
            Test_DataObject Attribute_9 = null;
            Test_DataObject Attribute_10 = null;

            #endregion

            try
            {
                if (DataObj == null || DataObj.Count <= 0)
                    throw new ArgumentNullException("List<Test_DataObject>");

                if (Data_Obj == null)
                    throw new ArgumentNullException("Base_Class");

                // Argument\Parameter Validation
                if (!DataObj.Exists((x) => (x.TestStatus & TestStatus.ReadSuccess) != 0))
                    throw new InvalidOperationException("Object Decode Operation not supported,No Data To Support");

                #region Init_Attribute_X

                Attribute_2 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 2);

                Attribute_3 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 3);

                Attribute_4 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 4);

                Attribute_5 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 5);

                Attribute_6 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 6);

                Attribute_7 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 7);

                Attribute_8 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 8);

                Attribute_9 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                  x.AttributeId == 9);

                Attribute_10 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                   x.AttributeId == 10);

                #endregion
                #region // Init_Decode_Data

                Data_Obj.ResetAttributeDecodingResults();
                Data_Obj.InitData_BaseClass();

                #endregion

                #region // Decode Profile Generic Capture Object List

                try
                {
                    if (Data_Obj is Class_7 || Data_Obj is Class_21)
                    {
                        Data_Obj.DecodingAttribute = 0x03;
                        if ((Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0)
                        {
                            data_Recovered = Attribute_3.Value_Array;
                            if (data_Recovered != null && data_Recovered.Count > 0)
                            {
                                raw_dataRecovered = data_Recovered.ToArray();
                                Data_Obj.Decode_Data(ref raw_dataRecovered);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    String Message_Error = "Error Data Decoder Profile CaptureObject List TryDecode_TestDebugData,{0} {1}, \r\n {2}";

                    if (Attribute_3 != null)
                        System.Diagnostics.Debug.Write(string.Format(Message_Error,
                                                                     Attribute_3.ObjectLabel,
                                                                     Attribute_3.Label, ex.Message));
                    else
                        System.Diagnostics.Debug.Write(string.Format(Message_Error, null, null, ex.Message));
                }

                #endregion
                #region // Attribute Decode Procedure To Work


                for (int Indexer = 1; Indexer < DataObj.Count; Indexer++)
                {
                    try
                    {
                        Current_DataObj = DataObj[Indexer];

                        // Skip Data Decode If No Read Success
                        if ((Current_DataObj.TestStatus & TestStatus.ReadSuccess) == 0)
                            continue;

                        Data_Obj.DecodingAttribute = Current_DataObj.AttributeId;
                        data_Recovered = Current_DataObj.Value_Array;
                        if (data_Recovered != null && data_Recovered.Count > 0)
                        {
                            raw_dataRecovered = data_Recovered.ToArray();
                            Data_Obj.Decode_Data(ref raw_dataRecovered);
                        }
                    }
                    catch (Exception ex)
                    {
                        String Message_Error = "Error Data Decoder TryDecode_TestDebugData,{0} {1}, \r\n {2}";

                        if (Current_DataObj != null)
                            System.Diagnostics.Debug.WriteLine(string.Format(Message_Error,
                                                                         Current_DataObj.ObjectLabel,
                                                                         Current_DataObj.Label, ex.Message));
                        else
                            System.Diagnostics.Debug.WriteLine(string.Format(Message_Error, null, null, ex.Message));
                    }
                }

                #endregion

                #region // Store Value For Object Decoded

                #region Class_1 Data

                if (Data_Obj is Class_1)
                {
                    // Store Value Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        #region Integral_Value

                        if ((Data_Obj as Class_1).Value != null &&
                            Convert.ToDouble((Data_Obj as Class_1).Value) != double.NaN)
                        {
                            Attribute_2.Double_Value = Convert.ToDouble((Data_Obj as Class_1).Value);
                            Attribute_2.Value_Type = TypeOfValue.Double_value;
                        }

                        #endregion
                        #region Complex_Object

                        else if ((Data_Obj as Class_1).Value_Obj != null)
                        {
                            Attribute_2.String_Value = (Data_Obj as Class_1).Value_Obj.ToString();
                            Attribute_2.Value_Type = TypeOfValue.string_value;
                        }

                        #endregion
                        #region Value_Array

                        else if ((Data_Obj as Class_1).Value_Array != null &&
                                 (Data_Obj as Class_1).Value_Array.Length > 0)
                        {
                            StringBuilder builder = new StringBuilder(512);

                            if ((Data_Obj as Class_1).Value_Array is byte[] &&
                                (Data_Obj as Class_1).BitLength > 0)
                            {
                                byte[] flagsRaw = (Data_Obj as Class_1).Value_Array as byte[];
                                BitArray flags = new BitArray(flagsRaw) { Length = (Data_Obj as Class_1).BitLength };

                                foreach (var bitFlag in flags)
                                {
                                    builder.AppendFormat("[{0}]", Convert.ToByte(bitFlag));
                                }
                            }
                            else if ((Data_Obj as Class_1).Value_Array is byte[])
                            {
                                byte[] DataRaw = (Data_Obj as Class_1).Value_Array as byte[];

                                foreach (var itemVal in DataRaw)
                                {
                                    builder.AppendFormat("[{0:X2}]", Convert.ToByte(itemVal));
                                }
                            }
                            else
                            {
                                foreach (var itemVal in (Data_Obj as Class_1).Value_Array)
                                {
                                    builder.AppendFormat("[{0}]", itemVal.ToString());
                                }
                            }

                            Attribute_2.String_Value = builder.ToString();
                            Attribute_2.Value_Type = TypeOfValue.string_value;
                        }

                        #endregion
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_3 Register

                else if (Data_Obj is Class_3)
                {
                    // Store Value Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        #region Integral_Value

                        if ((Data_Obj as Class_3).Value != null &&
                            Convert.ToDouble((Data_Obj as Class_3).Value) != double.NaN)
                        {
                            Attribute_2.Double_Value = Convert.ToDouble((Data_Obj as Class_3).Value);
                            Attribute_2.Value_Type = TypeOfValue.Double_value;
                        }

                        #endregion
                        #region Value_Array

                        else if ((Data_Obj as Class_3).Value_Array != null &&
                                 (Data_Obj as Class_3).Value_Array.Length > 0)
                        {
                            StringBuilder builder = new StringBuilder(512);

                            if ((Data_Obj as Class_3).Value_Array is byte[])
                            {
                                byte[] DataRaw = (Data_Obj as Class_3).Value_Array as byte[];

                                foreach (var itemVal in DataRaw)
                                {
                                    builder.AppendFormat("[{0:X2}]", Convert.ToByte(itemVal));
                                }
                            }
                            else
                            {
                                foreach (var itemVal in (Data_Obj as Class_3).Value_Array)
                                {
                                    builder.AppendFormat("[{0}]", itemVal.ToString());
                                }
                            }

                            Attribute_2.String_Value = builder.ToString();
                            Attribute_2.Value_Type = TypeOfValue.string_value;
                        }

                        #endregion
                    }
                    // Store Scaler Unit
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        String Output_FRMT = "{0} Scl:{1}";
                        Attribute_3.String_Value = String.Format(Output_FRMT, (Data_Obj as Class_3).Unit, (Data_Obj as Class_3).scaler);
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_4 MDI Register

                else if (Data_Obj is Class_4)
                {
                    // Store Value Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        #region Integral_Value

                        if (Convert.ToDouble((Data_Obj as Class_4).Value) != double.NaN)
                        {
                            Attribute_2.Double_Value = Convert.ToDouble((Data_Obj as Class_4).Value);
                            Attribute_2.Value_Type = TypeOfValue.Double_value;
                        }

                        #endregion
                    }
                    // Store Scaler Unit
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        String Output_FRMT = "{0} Scl:{1}";
                        Attribute_3.String_Value = String.Format(Output_FRMT, (Data_Obj as Class_4).Unit, (Data_Obj as Class_4).scaler);
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }
                    // Store Status
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.String_Value = "NA";  // (Data_Obj as Class_4).Date_Time_Stamp.ToString();
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }
                    // Store Capture Date Time
                    if (Attribute_5 != null &&
                       (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.String_Value = (Data_Obj as Class_4).Date_Time_Stamp.ToString();
                        Attribute_5.Value_Type = TypeOfValue.string_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_5 Sliding MDI

                else if (Data_Obj is Class_5)
                {
                    // Store CAVG Value Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        #region Integral_Value

                        if (Convert.ToDouble((Data_Obj as Class_5).CValue) != double.NaN)
                        {
                            Attribute_2.Double_Value = Convert.ToDouble((Data_Obj as Class_5).CValue);
                            Attribute_2.Value_Type = TypeOfValue.Double_value;
                        }

                        #endregion
                        #region Value_Array

                        else if ((Data_Obj as Class_5).CValue_Array != null &&
                                 (Data_Obj as Class_5).CValue_Array.Length > 0)
                        {
                            StringBuilder builder = new StringBuilder(512);

                            if ((Data_Obj as Class_5).CValue_Array is byte[])
                            {
                                byte[] DataRaw = (Data_Obj as Class_5).CValue_Array as byte[];

                                foreach (var itemVal in DataRaw)
                                {
                                    builder.AppendFormat("[{0:X2}]", Convert.ToByte(itemVal));
                                }
                            }
                            else
                            {
                                foreach (var itemVal in (Data_Obj as Class_5).CValue_Array)
                                {
                                    builder.AppendFormat("[{0}]", itemVal.ToString());
                                }
                            }

                            Attribute_2.String_Value = builder.ToString();
                            Attribute_2.Value_Type = TypeOfValue.string_value;
                        }

                        #endregion
                    }
                    // Store LAVG Value Attribute
                    if (Attribute_3 != null &&
                       (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        #region Integral_Value

                        if (Convert.ToDouble((Data_Obj as Class_5).LValue) != double.NaN)
                        {
                            Attribute_3.Double_Value = Convert.ToDouble((Data_Obj as Class_5).LValue);
                            Attribute_3.Value_Type = TypeOfValue.Double_value;
                        }

                        #endregion
                        #region LValue_Array

                        else if ((Data_Obj as Class_5).LValue_Array != null &&
                                 (Data_Obj as Class_5).LValue_Array.Length > 0)
                        {
                            StringBuilder builder = new StringBuilder(512);

                            if ((Data_Obj as Class_5).LValue_Array is byte[])
                            {
                                byte[] DataRaw = (Data_Obj as Class_5).LValue_Array as byte[];

                                foreach (var itemVal in DataRaw)
                                {
                                    builder.AppendFormat("[{0:X2}]", Convert.ToByte(itemVal));
                                }
                            }
                            else
                            {
                                foreach (var itemVal in (Data_Obj as Class_5).LValue_Array)
                                {
                                    builder.AppendFormat("[{0}]", itemVal.ToString());
                                }
                            }

                            Attribute_3.String_Value = builder.ToString();
                            Attribute_3.Value_Type = TypeOfValue.string_value;
                        }

                        #endregion
                    }

                    // Store Scaler Unit
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        String Output_FRMT = "{0} Scl:{1}";
                        Attribute_4.String_Value = String.Format(Output_FRMT, (Data_Obj as Class_5).Unit, (Data_Obj as Class_5).scaler);
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Status
                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.String_Value = "NA"; //(Data_Obj as Class_5).capture_time.ToString();
                        Attribute_5.Value_Type = TypeOfValue.string_value;
                    }


                    // Store Capture DateTime
                    if (Attribute_6 != null &&
                       (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_6.String_Value = (Data_Obj as Class_5).capture_time.ToString();
                        Attribute_6.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Start CurrentTime
                    if (Attribute_7 != null &&
                        (Attribute_7.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_7.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_7.String_Value = (Data_Obj as Class_5).start_time_Current.ToString();
                        Attribute_7.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Period
                    if (Attribute_8 != null &&
                        (Attribute_8.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_8.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_8.Double_Value = Convert.ToDouble((Data_Obj as Class_5).period);
                        Attribute_8.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store No Of Periods
                    if (Attribute_9 != null &&
                        (Attribute_9.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_9.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_9.Double_Value = Convert.ToDouble((Data_Obj as Class_5).periodCount);
                        Attribute_9.Value_Type = TypeOfValue.Double_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_7 GenericProfile

                else if (Data_Obj is Class_7)
                {
                    #region // Attribute_2 Store Buffer

                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        var buffer = (Data_Obj as Class_7).buffer;
                        var captureObjectList = (Data_Obj as Class_7).captureObjectsList;

                        Base_Class Current_DLMS_OBJ = null;
                        string Base_TRIM_STR = "Attribute Request:";
                        string localObje_STR = string.Empty;
                        StringBuilder strVal = new StringBuilder(1000);
                        // Buffer Decoded Info
                        if (buffer != null && buffer.Count > 0)
                        {
                            // Compute buffer Capture Instance Size
                            int captureInstanceSize = buffer.FindIndex(1, (x) => x.OBIS_CODE.SequenceEqual<byte>(buffer[0].OBIS_CODE));
                            if (captureInstanceSize == -1)
                                captureInstanceSize = buffer.Count;
                            strVal.AppendFormat(",Capture Instance Size:{0}", captureInstanceSize);
                            strVal.AppendFormat(",Decoded Buffer Objects:{0}:{1},", buffer.Count
                                                                                  , Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId));

                            strVal.Append('[');
                            int captureSize = 1;
                            for (int indexer = 0; indexer < buffer.Count; indexer++, captureSize++)
                            {
                                Current_DLMS_OBJ = buffer[indexer];

                                if (Current_DLMS_OBJ is Class_8 &&
                                    (Current_DLMS_OBJ as Class_8).Date_Time_Value != null &&
                                     Current_DLMS_OBJ.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                                {
                                    localObje_STR = (Current_DLMS_OBJ as Class_8).Date_Time_Value.ToString();
                                }
                                else
                                    localObje_STR = buffer[indexer].ToString();

                                if (!string.IsNullOrEmpty(localObje_STR))
                                {
                                    // Trim Base_Class ToString Part
                                    int TrimIndex = localObje_STR.IndexOf(Base_TRIM_STR, 0, StringComparison.OrdinalIgnoreCase);
                                    if (TrimIndex != -1 &&
                                        TrimIndex < localObje_STR.Length)
                                        TrimIndex = localObje_STR.IndexOf(",", TrimIndex + 1, StringComparison.OrdinalIgnoreCase);

                                    if (TrimIndex != -1 &&
                                        TrimIndex < localObje_STR.Length)
                                        localObje_STR = localObje_STR.Substring(TrimIndex);

                                    strVal.AppendFormat("{0}|", localObje_STR);
                                }

                                if (captureSize % captureInstanceSize == 0 &&
                                    captureInstanceSize != -1)
                                    strVal.Append("][");
                            }
                        }
                        else
                        {
                            strVal.AppendFormat(",Capture Instance Size:{0}", captureObjectList.Count);
                            strVal.AppendFormat(",Decoded Buffer Objects:{0}:{1},", 0, Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId));
                        }

                        Attribute_2.String_Value = strVal.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute_3 Store Capture Object List Attribute

                    if (Attribute_3 != null &&
                       (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                        Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        StOBISCode CurrentOBISCode = Get_Index.Dummy;
                        var captureObjectList = (Data_Obj as Class_7).captureObjectsList;
                        String CaptureObjFRMT = ",[{0} {1} {2}]";
                        StringBuilder STRCapture = new StringBuilder();

                        // Process Capture Object List
                        foreach (var captureObj in captureObjectList)
                        {
                            // Current Object OBIS Code
                            CurrentOBISCode = StOBISCode.ConvertFrom(captureObj.OBISCode);
                            CurrentOBISCode.ClassId = captureObj.ClassId;

                            STRCapture.AppendFormat(CaptureObjFRMT
                                                    , CurrentOBISCode.OBISIndex
                                                    , CurrentOBISCode.ToString(StOBISCode.FormatSpecifier.CompleteHexMode)
                                                    , captureObj.AttributeIndex);
                        }

                        Attribute_3.String_Value = STRCapture.ToString();
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute_4 Store capturePeriod

                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.Value_Type = TypeOfValue.Double_value;
                        Attribute_4.Double_Value = Convert.ToDouble((Data_Obj as Class_7).capturePeriod);
                    }

                    #endregion
                    #region // Attribute_5 Store sortMethod

                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.Value_Type = TypeOfValue.Double_value;
                        Attribute_5.Double_Value = Convert.ToDouble((Data_Obj as Class_7).sortMethod);
                    }

                    #endregion
                    #region // Attribute 0x06 sortObject

                    if (Attribute_6 != null &&
                        (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {
                        var captureObj = (Data_Obj as Class_7).sortObject;

                        StOBISCode CurrentOBISCode = Get_Index.Dummy;
                        var captureObjectList = (Data_Obj as Class_7).captureObjectsList;
                        String CaptureObjFRMT = ",[{0} {1} {2}]";

                        Attribute_6.Value_Type = TypeOfValue.string_value;
                        Attribute_6.String_Value = string.Format(CaptureObjFRMT
                                                    , CurrentOBISCode.OBISIndex
                                                    , CurrentOBISCode.ToString(StOBISCode.FormatSpecifier.CompleteHexMode)
                                                    , captureObj.AttributeIndex);
                    }

                    #endregion
                    #region // Store Attribute 0x07 entriesInUse

                    if (Attribute_7 != null &&
                                    (Attribute_7.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_7.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_7.Value_Type = TypeOfValue.Double_value;
                        Attribute_7.Double_Value = Convert.ToDouble((Data_Obj as Class_7).entriesInUse);
                    }

                    #endregion
                    #region // Store Attribute 0x08 MaxProfileEntries

                    if (Attribute_8 != null &&
                                    (Attribute_8.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_8.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_8.Value_Type = TypeOfValue.Double_value;
                        Attribute_8.Double_Value = Convert.ToDouble((Data_Obj as Class_7).MaxProfileEntries);
                    }

                    #endregion

                    isValueRecovered = true;
                }

                #endregion
                #region Class_8 RTC

                else if (Data_Obj is Class_8)
                {
                    // Store Date_Time_Value Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = (Data_Obj as Class_8).Date_Time_Value.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }
                    // Store TimeZone Attribute
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.Double_Value = Convert.ToDouble((Data_Obj as Class_8).Time_Zone);
                        Attribute_3.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store Status Attribute
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.Double_Value = Convert.ToDouble((Data_Obj as Class_8).Status);
                        Attribute_3.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store Daylight_Savings_Begin DateTime Attribute
                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.String_Value = (Data_Obj as Class_8).Daylight_Savings_Begin.ToString();
                        Attribute_5.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Daylight_Savings_End DateTime
                    if (Attribute_6 != null &&
                        (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_6.String_Value = (Data_Obj as Class_8).Daylight_Savings_End.ToString();
                        Attribute_6.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Daylight_Savings_Deviation Attribute
                    if (Attribute_7 != null &&
                        (Attribute_7.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_7.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_7.Double_Value = Convert.ToDouble((Data_Obj as Class_8).Daylight_Savings_Deviation);
                        Attribute_7.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store flg_Daylight_Savings_Enabled Attribute
                    if (Attribute_8 != null &&
                        (Attribute_8.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_8.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_8.Double_Value = Convert.ToDouble((Data_Obj as Class_8).flg_Daylight_Savings_Enabled);
                        Attribute_8.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store ClockBase Attribute
                    if (Attribute_9 != null &&
                        (Attribute_9.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_9.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_9.String_Value = (Data_Obj as Class_8).Server_Clk_Base.ToString();
                        Attribute_9.Value_Type = TypeOfValue.string_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_11 Special Day Profile Table

                else if (Data_Obj is Class_11)
                {
                    // Store StSpecialDayProfile Table Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder strVal = new StringBuilder();
                        StringBuilder DayProfiles = new StringBuilder();
                        int DayProfileCount = 0;

                        List<StSpecialDayProfile> specialDayProfiles = (Data_Obj as Class_11).SpecialDayProfiles;


                        if (specialDayProfiles != null &&
                            specialDayProfiles.Count > 0)
                        {
                            DayProfileCount = specialDayProfiles.Count;
                            foreach (StSpecialDayProfile dyProfile in specialDayProfiles) // Append Codes in List & Code Name
                            {
                                try
                                {
                                    DayProfiles.AppendFormat(",[{0}]", dyProfile);
                                }
                                catch (Exception) { DayProfiles.Append("Error"); }
                            }
                        }

                        strVal.AppendFormat(",Count SP DayProfile:{0}:{1}",
                                             DayProfileCount, Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId));
                        strVal.Append(DayProfiles.ToString());

                        Attribute_2.String_Value = strVal.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_15 Association

                else if (Data_Obj is Class_15)
                {
                    #region // Attribute_2 Store ObjectList In Association Attribute

                    if (Attribute_2 != null &&
                                    (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = (Data_Obj as Class_15).ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute_3 Store Associated_Partners_Id Attribute

                    if (Attribute_3 != null &&
                                    (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {

                        Attribute_3.String_Value = String.Format("[{0},{1}]", (Data_Obj as Class_15).ClientSAP_Id,
                                                                              (Data_Obj as Class_15).ServerSAP_Id);
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute_4 Store ApplicationContext

                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        var applicationContext = (Data_Obj as Class_15).applicationContext;

                        string Formt_STR = "[{0:X2},{1:X2},{2:X2},{3:X2},{4:X2},{5:X2},{6:X2}]";

                        Attribute_4.Value_Type = TypeOfValue.string_value;
                        Attribute_4.String_Value = String.Format(Formt_STR,
                                                                applicationContext.ctt_element,
                                                                applicationContext.Country_element,
                                                                applicationContext.Country_name_element,
                                                                applicationContext.Organization_element,
                                                                applicationContext.DLMS_UA_element,
                                                                applicationContext.Application_Context_element,
                                                                applicationContext.Context_id_element);
                    }

                    #endregion
                    #region // Attribute_5 Store xDLMS_context_info

                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        var negoDLMSContext = (Data_Obj as Class_15).negotiatedDLMSContext;

                        string Formt_STR = "[({0:X2}),{1:X2},{2:X2},{3:X2},{4:X2}]";

                        string ConformanceData = (negoDLMSContext.Negotiated_DLMS_Conformance != null) ? LocalCommon.ArrayToHexString(negoDLMSContext.Negotiated_DLMS_Conformance) : "NIL";


                        Attribute_5.Value_Type = TypeOfValue.string_value;
                        Attribute_5.String_Value = String.Format(Formt_STR,
                                                                 ConformanceData,
                                                                 negoDLMSContext.Server_Max_Receive_PDU_Size,
                                                                 negoDLMSContext.Server_Max_Send_PDU_Size,
                                                                 negoDLMSContext.Negotiated_DLMS_Version_Number,
                                                                 // Decode QOS
                                                                 negoDLMSContext.VAA_Name);
                    }

                    #endregion
                    #region // Attribute 0x06 Authentication_Mechanism_Name

                    if (Attribute_6 != null &&
                        (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {
                        var AuthMechanismName = (Data_Obj as Class_15).authenticationMechanismName;

                        string Formt_STR = "[{0:X2},{1:X2},{2:X2},{3:X2},{4:X2},{5:X2},{6:X2}]";

                        Attribute_6.Value_Type = TypeOfValue.string_value;
                        Attribute_6.String_Value = String.Format(Formt_STR,
                                                                 AuthMechanismName.ctt_element,
                                                                 AuthMechanismName.Country_element,
                                                                 AuthMechanismName.Country_name_element,
                                                                 AuthMechanismName.Organization_element,
                                                                 AuthMechanismName.DLMS_UA_element,
                                                                 AuthMechanismName.Authen_mechanism_name_element,
                                                                 AuthMechanismName.mechanism_id_element);
                    }

                    #endregion
                    #region // Store Attribute 0x07 Secret

                    if (Attribute_7 != null &&
                                    (Attribute_7.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_7.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_7.String_Value = (Data_Obj as Class_15).Password;
                        Attribute_7.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Store Attribute 0x08 associationStatus

                    if (Attribute_8 != null &&
                                    (Attribute_8.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_8.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_8.String_Value = (Data_Obj as Class_15).associationStatus.ToString();
                        Attribute_8.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Store Attribute_9 SecurityObject_Reference

                    if (Attribute_9 != null && (Attribute_9.TestStatus & TestStatus.ReadSuccess) != 0 &&
                        Data_Obj.GetAttributeDecodingResult(Attribute_9.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_9.String_Value = (Data_Obj as Class_15).SecurityObject_Reference.ToString();
                        Attribute_9.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Store Attribute 0x0A UsersList

                    if (Attribute_10 != null &&
                        (Attribute_10.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_10.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder UserListSTR = new StringBuilder();
                        List<stUser> userLists = (Data_Obj as Class_15).usersList;

                        if (userLists != null &&
                            userLists.Count > 0)
                        {
                            foreach (stUser user in userLists)
                            {
                                try
                                {
                                    UserListSTR.AppendFormat(",[{0}{1}]", user.UserId, user.UserName);
                                }
                                catch (Exception) { UserListSTR.Append("Error"); }
                            }
                        }

                        Attribute_10.String_Value = UserListSTR.ToString();
                        Attribute_10.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion

                    var Attribute_11 = DataObj.Find((x) => x.OBIS_Code != Get_Index.Dummy &&
                                                     x.AttributeId == 11);

                    #region // Store Attribute 0x0B Current User

                    if (Attribute_11 != null &&
                        (Attribute_11.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_11.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder UserListSTR = new StringBuilder();
                        var currentUser = (Data_Obj as Class_15).currentUser;

                        UserListSTR.AppendFormat(",[{0}{1}]", currentUser.UserId, currentUser.UserName);

                        Attribute_11.String_Value = UserListSTR.ToString();
                        Attribute_11.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion

                    isValueRecovered = true;
                }

                #endregion
                #region Class_17 SAP List

                else if (Data_Obj is Class_17)
                {
                    #region // Attribute_2 Store SAPList

                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        var ServerLogicalLst = (Data_Obj as Class_17).Server_Logical_Devices;
                        StringBuilder logicalDevices = new StringBuilder();

                        if (ServerLogicalLst != null &&
                             ServerLogicalLst.Count > 0)
                        {
                            foreach (var sap in ServerLogicalLst)
                            {
                                try
                                {
                                    logicalDevices.AppendFormat(",[{0} {1:X2}]", sap.SAP_Name, sap.SAP_Address);
                                }
                                catch (Exception) { logicalDevices.Append("Error"); }
                            }
                        }

                        Attribute_2.String_Value = logicalDevices.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion

                    isValueRecovered = true;
                }

                #endregion
                #region Class_20 Activity Calender

                else if (Data_Obj is Class_20)
                {
                    #region // Attribute_02 Store calendarNameActive

                    if (Attribute_2 != null &&
                                    (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {

                        Attribute_2.String_Value = String.Format("{0}", (Data_Obj as Class_20).CalendarNameActive);
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 0x03 SeasonProfileActive Table

                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder strVal = new StringBuilder();
                        StringBuilder seasonProfilesSTR = new StringBuilder();

                        List<StSeasonProfile> StSeasonProfiles = (Data_Obj as Class_20).SeasonProfileActive;

                        if (StSeasonProfiles != null &&
                            StSeasonProfiles.Count > 0)
                        {
                            foreach (var seasonProfile in StSeasonProfiles)
                            {
                                try
                                {
                                    seasonProfilesSTR.AppendFormat(",[{0} {1} {2}]", seasonProfile.ProfileName, seasonProfile.WeekProfileName, seasonProfile.StartDate);
                                }
                                catch (Exception) { seasonProfilesSTR.Append("[Error Season Profile]"); }
                            }
                        }

                        Attribute_3.String_Value = seasonProfilesSTR.ToString();
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 0x04 WeekProfileActive Table

                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder strVal = new StringBuilder();
                        StringBuilder weekProfilesSTR = new StringBuilder();

                        List<StWeekProfile> StWeekProfiles = (Data_Obj as Class_20).WeekProfileActive;

                        if (StWeekProfiles != null &&
                            StWeekProfiles.Count > 0)
                        {
                            foreach (var wkProfile in StWeekProfiles)
                            {
                                try
                                {
                                    weekProfilesSTR.AppendFormat(",[{0} {1:X2} {2:X2} {3:X2} {4:X2} {5:X2} {6:X2} {7:X2} ]",
                                                                 wkProfile.ProfileName,
                                                                 wkProfile.DayProfileIdMon,
                                                                 wkProfile.DayProfileIdTue,
                                                                 wkProfile.DayProfileIdWed,
                                                                 wkProfile.DayProfileIdThu,
                                                                 wkProfile.DayProfileIdFri,
                                                                 wkProfile.DayProfileIdSat,
                                                                 wkProfile.DayProfileIdSun);
                                }
                                catch (Exception) { weekProfilesSTR.Append("[Error Week Profile]"); }
                            }
                        }

                        Attribute_4.String_Value = weekProfilesSTR.ToString();
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 0x05 DayProfileActive Table

                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder strVal = new StringBuilder();
                        StringBuilder dyProfilesSTR = new StringBuilder();

                        List<StDayProfile> StDayProfiles = (Data_Obj as Class_20).DayProfileActive;

                        if (StDayProfiles != null &&
                            StDayProfiles.Count > 0)
                        {
                            foreach (var dyProfile in StDayProfiles)
                            {
                                try
                                {
                                    dyProfilesSTR.AppendFormat(",[{0} ",
                                                                 dyProfile.DayId);


                                    foreach (var actionSch in dyProfile.DaySchedule)
                                    {
                                        dyProfilesSTR.AppendFormat(",({0} {1})",
                                                                 LocalCommon.ArrayToHexString(actionSch.Script_logicalName),
                                                                 actionSch.StartTime);
                                    }

                                    dyProfilesSTR.Append(']');


                                }
                                catch (Exception) { dyProfilesSTR.Append("[Error Day Profile]"); }
                            }
                        }

                        Attribute_5.String_Value = dyProfilesSTR.ToString();
                        Attribute_5.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute_06 Store calendarNamePassive

                    if (Attribute_6 != null &&
                       (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                        Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {

                        Attribute_6.String_Value = String.Format("{0}", (Data_Obj as Class_20).CalendarNamePassive);
                        Attribute_6.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 0x07 SeasonProfilePassive Table

                    if (Attribute_7 != null &&
                        (Attribute_7.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_7.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder strVal = new StringBuilder();
                        StringBuilder seasonProfilesSTR = new StringBuilder();

                        List<StSeasonProfile> StSeasonProfiles = (Data_Obj as Class_20).SeasonProfilePassive;

                        if (StSeasonProfiles != null &&
                            StSeasonProfiles.Count > 0)
                        {
                            foreach (var seasonProfile in StSeasonProfiles)
                            {
                                try
                                {
                                    seasonProfilesSTR.AppendFormat(",[{0} {1} {2}]", seasonProfile.ProfileName, seasonProfile.WeekProfileName, seasonProfile.StartDate);
                                }
                                catch (Exception) { seasonProfilesSTR.Append("[Error Season Profile]"); }
                            }
                        }

                        Attribute_7.String_Value = seasonProfilesSTR.ToString();
                        Attribute_7.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 0x08 WeekProfilePassive Table

                    if (Attribute_8 != null &&
                        (Attribute_8.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_8.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder strVal = new StringBuilder();
                        StringBuilder weekProfilesSTR = new StringBuilder();

                        List<StWeekProfile> StWeekProfiles = (Data_Obj as Class_20).WeekProfilePassive;

                        if (StWeekProfiles != null &&
                            StWeekProfiles.Count > 0)
                        {
                            foreach (var wkProfile in StWeekProfiles)
                            {
                                try
                                {
                                    weekProfilesSTR.AppendFormat(",[{0} {1:X2} {2:X2} {3:X2} {4:X2} {5:X2} {6:X2} {7:X2} ]",
                                                                 wkProfile.ProfileName,
                                                                 wkProfile.DayProfileIdMon,
                                                                 wkProfile.DayProfileIdTue,
                                                                 wkProfile.DayProfileIdWed,
                                                                 wkProfile.DayProfileIdThu,
                                                                 wkProfile.DayProfileIdFri,
                                                                 wkProfile.DayProfileIdSat,
                                                                 wkProfile.DayProfileIdSun);
                                }
                                catch (Exception) { weekProfilesSTR.Append("[Error Week Profile]"); }
                            }
                        }

                        Attribute_8.String_Value = weekProfilesSTR.ToString();
                        Attribute_8.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 0x09 DayProfilePassive Table

                    if (Attribute_9 != null &&
                        (Attribute_9.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_9.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder dyProfilesSTR = new StringBuilder();

                        List<StDayProfile> StDayProfiles = (Data_Obj as Class_20).DayProfilePassive;

                        if (StDayProfiles != null &&
                            StDayProfiles.Count > 0)
                        {
                            foreach (var dyProfile in StDayProfiles)
                            {
                                try
                                {
                                    dyProfilesSTR.AppendFormat(",[{0} ",
                                                                 dyProfile.DayId);


                                    foreach (var actionSch in dyProfile.DaySchedule)
                                    {
                                        dyProfilesSTR.AppendFormat(",({0} {1})",
                                                                 LocalCommon.ArrayToHexString(actionSch.Script_logicalName),
                                                                 actionSch.StartTime);
                                    }

                                    dyProfilesSTR.Append(']');


                                }
                                catch (Exception) { dyProfilesSTR.Append("[Error Day Profile]"); }
                            }
                        }

                        Attribute_9.String_Value = dyProfilesSTR.ToString();
                        Attribute_9.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 0x0A Store calendarActivation Time

                    if (Attribute_10 != null &&
                       (Attribute_10.TestStatus & TestStatus.ReadSuccess) != 0 &&
                        Data_Obj.GetAttributeDecodingResult(Attribute_10.AttributeId) == DecodingResult.Ready)
                    {

                        Attribute_10.String_Value = String.Format("{0}", (Data_Obj as Class_20).ActivatePassiveCalendarTime);
                        Attribute_10.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    isValueRecovered = true;
                }

                #endregion
                #region Class_21 Register Moniter

                else if (Data_Obj is Class_21)
                {
                    #region // Attribute 2 Thresholds

                    if (Attribute_2 != null &&
                       (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder builder = new StringBuilder(512);

                        if ((Data_Obj as Class_21).Thresholds != null)
                        {
                            foreach (var th in (Data_Obj as Class_21).Thresholds)
                            {
                                builder.AppendFormat("[{0}],", th);
                            }
                        }
                        else
                            builder.Append("NA");

                        Attribute_2.String_Value = builder.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute 3 Monitored Value

                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.String_Value = "NA";

                        if ((Data_Obj as Class_21).MonitoredValue != null)
                        {
                            Attribute_3.String_Value = (Data_Obj as Class_21).MonitoredValue.ToString();
                            Attribute_3.Value_Type = TypeOfValue.string_value;
                        }
                    }

                    #endregion

                    #region // Attribute 4 Action Set

                    if (Attribute_4 != null &&
                       (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                        Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder builder = new StringBuilder(512);

                        if ((Data_Obj as Class_21).Actions != null)
                        {
                            foreach (var acSET in (Data_Obj as Class_21).Actions)
                            {
                                builder.AppendFormat("[{0}][{1}],", acSET.ActionDown, acSET.ActionUp);
                            }
                        }
                        else
                            builder.Append("NA");

                        Attribute_4.String_Value = builder.ToString();
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion

                    isValueRecovered = true;
                }

                #endregion
                #region Class_22 Action Schedule

                else if (Data_Obj is Class_22)
                {
                    #region // Attribute_2 Executed_Script

                    if (Attribute_2 != null &&
                       (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                        Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = "NA";
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Attribute_03 Store type

                    if (Attribute_3 != null &&
                                    (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                                     Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {

                        Attribute_3.String_Value = "NA";
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion
                    #region // Store Attribute_04 Execution_Time

                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        StringBuilder ExecTimeListSTR = new StringBuilder();
                        List<StDateTime> ExecTimeList = (Data_Obj as Class_22).executionTimeList;

                        if (ExecTimeList != null &&
                            ExecTimeList.Count > 0)
                        {
                            foreach (StDateTime time in ExecTimeList)
                            {
                                try
                                {
                                    ExecTimeListSTR.AppendFormat(",[{0}]", time);
                                }
                                catch (Exception) { ExecTimeListSTR.Append("Error"); }
                            }
                        }

                        Attribute_4.String_Value = ExecTimeListSTR.ToString();
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    #endregion

                    isValueRecovered = true;
                }

                #endregion
                #region Class_41 TCP-UDP setup

                else if (Data_Obj is Class_41)
                {
                    // Store TCP_UDP_Port Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.Double_Value = Convert.ToDouble((Data_Obj as Class_41).TCP_UDP_Port);
                        Attribute_2.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store IP_Reference Attribute
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.String_Value = LocalCommon.ArrayToHexString((Data_Obj as Class_41).IP_Reference);
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Max_Segment_Size Attribute
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.Double_Value = Convert.ToDouble((Data_Obj as Class_41).Max_Segment_Size);
                        Attribute_4.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store Simultaneous_Conn_No Attribute
                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.Double_Value = Convert.ToDouble((Data_Obj as Class_41).Simultaneous_Conn_No);
                        Attribute_5.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store Inactivity_Time_Out_Secs Attribute
                    if (Attribute_6 != null &&
                        (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_6.Double_Value = Convert.ToDouble((Data_Obj as Class_41).Inactivity_Time_Out_Secs);
                        Attribute_6.Value_Type = TypeOfValue.Double_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_42 IPv4 setup

                else if (Data_Obj is Class_42)
                {
                    // Store DataLink_Reference Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = LocalCommon.ArrayToHexString((Data_Obj as Class_42).DataLink_Reference);
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    // Store IP_Address Attribute
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.String_Value = (Data_Obj as Class_42).IP_Address.ToString();
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    // Store multicast_IP_address Attribute
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.String_Value = "NA";
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    // Store IP_options Attribute
                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.String_Value = "NA";
                        Attribute_5.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Subnet_Mask Attribute
                    if (Attribute_6 != null &&
                        (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_6.String_Value = (Data_Obj as Class_42).Subnet_Mask.ToString();
                        Attribute_6.Value_Type = TypeOfValue.string_value;
                    }

                    // Store gateway_IP_address Attribute
                    if (Attribute_7 != null &&
                        (Attribute_7.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_7.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_7.String_Value = (Data_Obj as Class_42).Gateway_IP.ToString();
                        Attribute_7.Value_Type = TypeOfValue.string_value;
                    }

                    // Store flg_Use_DHCP Attribute
                    if (Attribute_8 != null &&
                        (Attribute_8.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_8.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_8.String_Value = (Data_Obj as Class_42).flg_Use_DHCP.ToString();
                        Attribute_8.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Primary_DNS_IP Attribute
                    if (Attribute_9 != null &&
                        (Attribute_9.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_9.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_9.String_Value = (Data_Obj as Class_42).Primary_DNS_IP.ToString();
                        Attribute_9.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Secondary_DNS_IP Attribute
                    if (Attribute_10 != null &&
                        (Attribute_10.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_10.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_10.String_Value = (Data_Obj as Class_42).Secondary_DNS_IP.ToString();
                        Attribute_10.Value_Type = TypeOfValue.string_value;
                    }


                    isValueRecovered = true;
                }

                #endregion
                #region Class_45 GPRS Modem Setup

                else if (Data_Obj is Class_45)
                {
                    // Store APN Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = (Data_Obj as Class_45).APN;
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Pin_Code Attribute
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.Double_Value = (Data_Obj as Class_45).Pin_Code;
                        Attribute_3.Value_Type = TypeOfValue.Double_value;
                    }

                    // Store quality_of_service Attribute
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.String_Value = "NA";
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region Class_64 Security setup

                else if (Data_Obj is Class_64)
                {
                    // Store SecurityPolicy Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = (Data_Obj as Class_64).SecurityPolicy.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    // Store SecuritySuite Attribute
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.String_Value = (Data_Obj as Class_64).SecuritySuite.ToString();
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    // Store ClientSystemTitle Attribute
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.String_Value = LocalCommon.ArrayToHexString((Data_Obj as Class_64).ClientSystemTitle.AP_Title);
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    // Store ServerSystemTitle Attribute
                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.String_Value = LocalCommon.ArrayToHexString((Data_Obj as Class_64).ServerSystemTitle.AP_Title);
                        Attribute_5.Value_Type = TypeOfValue.string_value;
                    }


                    isValueRecovered = true;
                }

                #endregion
                #region Class_70 Contactor Control

                else if (Data_Obj is Class_70)
                {
                    // Store Output_state Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = (Data_Obj as Class_70).Output_state.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Control_state Attribute
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.String_Value = (Data_Obj as Class_70).Control_state.ToString();
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }

                    // Store Control_Mode Attribute
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.String_Value = (Data_Obj as Class_70).Control_Mode.ToString();
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }

                    isValueRecovered = true;
                }

                #endregion
                #region ELSE
                else
                {
                    // Store Raw Object Value Attribute
                    if (Attribute_2 != null &&
                        (Attribute_2.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_2.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_2.String_Value = Data_Obj.ToString();
                        Attribute_2.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_3 != null &&
                        (Attribute_3.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_3.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_3.String_Value = Data_Obj.ToString();
                        Attribute_3.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_4 != null &&
                        (Attribute_4.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_4.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_4.String_Value = Data_Obj.ToString();
                        Attribute_4.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_5 != null &&
                        (Attribute_5.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_5.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_5.String_Value = Data_Obj.ToString();
                        Attribute_5.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_6 != null &&
                        (Attribute_6.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_6.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_6.String_Value = Data_Obj.ToString();
                        Attribute_6.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_7 != null &&
                        (Attribute_7.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_7.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_7.String_Value = Data_Obj.ToString();
                        Attribute_7.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_8 != null &&
                        (Attribute_8.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_8.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_8.String_Value = Data_Obj.ToString();
                        Attribute_8.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_9 != null &&
                        (Attribute_9.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_9.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_9.String_Value = Data_Obj.ToString();
                        Attribute_9.Value_Type = TypeOfValue.string_value;
                    }
                    if (Attribute_10 != null &&
                        (Attribute_10.TestStatus & TestStatus.ReadSuccess) != 0 &&
                         Data_Obj.GetAttributeDecodingResult(Attribute_10.AttributeId) == DecodingResult.Ready)
                    {
                        Attribute_10.String_Value = Data_Obj.ToString();
                        Attribute_10.Value_Type = TypeOfValue.string_value;
                    }

                    isValueRecovered = false;
                }
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                isValueRecovered = false;
            }
            return isValueRecovered;
        }

        [Flags]
        public enum TestStatus : byte
        {
            ReadNotPerform = 1,
            ReadSuccess = 2,
            ReadFailure = 4,
            WriteNotPerform = 8,
            WriteSuccess = 16,
            WriteFailure = 32,
            UserCancel = 64,
            Reserved2 = 128
        }

        public class Test_DataObject : NotifyProperyChangedBase, IEqualityComparer<Test_DataObject>
        {
            #region Data_Member

            private DateTime _TimeStamp;

            private StOBISCode _OBIS_code;
            private string _label;
            private string _ObjectLabel;
            private byte _attributeId;

            private double _Double_value;
            private string _string_value;
            private TypeOfValue _value_type;

            private List<byte> _byte_array;
            private TestStatus _testStatus;

            #endregion

            #region Property

            public DateTime TimeStamp
            {
                get { return _TimeStamp; }
                set
                {
                    base.CheckPropertyChanged<DateTime>("TimeStamp", ref _TimeStamp, ref value);
                    _TimeStamp = value;
                }
            }

            public StOBISCode OBIS_Code
            {
                get { return _OBIS_code; }
                set
                {
                    base.CheckPropertyChanged<StOBISCode>("OBIS_Code", ref _OBIS_code, ref value);
                    _OBIS_code = value;
                }
            }

            public string Label
            {
                get { return _label; }
                set
                {
                    base.CheckPropertyChanged<string>("Label", ref _label, ref value);
                    _label = value;
                }
            }

            public string ObjectLabel
            {
                get { return _ObjectLabel; }
                set
                {
                    base.CheckPropertyChanged<string>("ObjectLabel", ref _ObjectLabel, ref value);
                    _ObjectLabel = value;
                }
            }

            public byte AttributeId
            {
                get { return _attributeId; }
                set
                {
                    base.CheckPropertyChanged<byte>("AttributeId", ref _attributeId, ref value);
                    _attributeId = value;
                }
            }

            public string Attribute_Value
            {
                get
                {
                    string Value = "NA";

                    try
                    {
                        if (Value_Type == TypeOfValue.Double_value)
                        {
                            Value = LocalCommon.value_to_string(Double_Value);
                        }
                        else if (Value_Type == TypeOfValue.string_value &&
                                 !string.IsNullOrEmpty(String_Value))
                        {
                            Value = String_Value;
                        }
                    }
                    catch
                    {
                        Value = "NA";
                    }

                    return Value;
                }
            }


            public double Double_Value
            {
                get { return _Double_value; }
                set
                {
                    if (base.CheckPropertyChanged<double>("Double_Value", ref _Double_value, ref value))
                    {
                        base.FirePropertyChanged("Attribute_Value");
                    }
                    _Double_value = value;
                }
            }

            public string String_Value
            {
                get { return _string_value; }
                set
                {
                    if (base.CheckPropertyChanged<string>("String_Value", ref _string_value, ref value))
                    {
                        base.FirePropertyChanged("Attribute_Value");
                    }
                    _string_value = value;
                }
            }

            public TypeOfValue Value_Type
            {
                get { return _value_type; }
                set
                {
                    if (base.CheckPropertyChanged<TypeOfValue>("Value_Type", ref _value_type, ref value))
                    {
                        base.FirePropertyChanged("Attribute_Value");
                    }
                    _value_type = value;

                }
            }


            public List<byte> Value_Array
            {
                get { return _byte_array; }
                set
                {
                    _byte_array = value;
                }
            }

            public TestStatus TestStatus
            {
                get { return _testStatus; }
                set
                {
                    if (base.CheckPropertyChanged<TestStatus>("TestStatus", ref _testStatus, ref value))
                    {
                        base.FirePropertyChanged("TestStatusSTR");
                    }
                    _testStatus = value;
                }
            }

            public string TestStatusSTR
            {
                get
                {
                    string OPTStatusSTR = string.Empty;

                    if ((TestStatus & TestStatus.UserCancel) != 0)
                        OPTStatusSTR = "RW ABT";
                    else if ((TestStatus & TestStatus.ReadSuccess) != 0 &&
                        (TestStatus & TestStatus.WriteSuccess) != 0)
                        OPTStatusSTR = "RW S";
                    else if ((TestStatus & TestStatus.ReadFailure) != 0 &&
                             (TestStatus & TestStatus.WriteFailure) != 0)
                        OPTStatusSTR = "RW F";
                    else if ((TestStatus & TestStatus.ReadFailure) != 0)
                        OPTStatusSTR = "RF";
                    else if ((TestStatus & TestStatus.ReadSuccess) != 0 &&
                             (TestStatus & TestStatus.WriteFailure) != 0)
                        OPTStatusSTR = "RS WF";
                    else if ((TestStatus & TestStatus.ReadSuccess) != 0 &&
                             (TestStatus & TestStatus.WriteNotPerform) != 0)
                        OPTStatusSTR = "RS";
                    else if ((TestStatus & TestStatus.ReadNotPerform) != 0 &&
                             (TestStatus & TestStatus.WriteNotPerform) != 0)
                        OPTStatusSTR = "NA";
                    else
                        OPTStatusSTR = "NA";

                    return OPTStatusSTR;
                }
            }

            #endregion

            public void Reset()
            {
                _Double_value = double.NaN;
                _string_value = string.Empty;
                _value_type = TypeOfValue.byte_array;

                _byte_array = null;
                _testStatus = Testing_Debug.TestStatus.ReadNotPerform | Testing_Debug.TestStatus.WriteNotPerform;
            }

            /// <summary>
            /// Default Constructor
            /// </summary>
            /// <param name="dummyParam"></param>
            public Test_DataObject()
            {
                _TimeStamp = DateTime.MinValue;
                _OBIS_code = Get_Index.Dummy;
                _label = "Dummy";
                _attributeId = 1;

                _Double_value = double.NaN;
                _string_value = string.Empty;
                _value_type = TypeOfValue.byte_array;

                _byte_array = null;
                _testStatus = Testing_Debug.TestStatus.ReadNotPerform | Testing_Debug.TestStatus.WriteNotPerform;
            }

            /// <summary>
            /// Copy Construcutor
            /// </summary>
            /// <param name="dummyParam"></param>
            public Test_DataObject(Test_DataObject otherObj)
            {
                _TimeStamp = otherObj._TimeStamp;
                _OBIS_code = otherObj._OBIS_code;
                _label = otherObj._label;
                _attributeId = otherObj._attributeId;

                _Double_value = otherObj._Double_value;
                _string_value = otherObj._string_value;
                _value_type = otherObj._value_type;

                _byte_array = null;
                if (otherObj._byte_array != null)
                    otherObj._byte_array = new List<byte>(otherObj._byte_array);
                _testStatus = Testing_Debug.TestStatus.ReadNotPerform | Testing_Debug.TestStatus.WriteNotPerform;
            }

            #region Object Members

            public override bool Equals(object obj)
            {
                try
                {
                    return this.Equals(this, obj as Test_DataObject);
                }
                catch
                {
                }
                return false;
            }

            public bool Equals(Test_DataObject x, Test_DataObject y)
            {
                return x.OBIS_Code.Equals(y.OBIS_Code) &&
                       x.AttributeId == y.AttributeId;
            }

            public int GetHashCode(Test_DataObject obj)
            {
                return obj.GetHashCode();
            }

            #endregion
        }
    }

}
