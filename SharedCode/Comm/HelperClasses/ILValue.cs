using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.HelperClasses
{
    [XmlInclude(typeof(StDateTime))]
    [XmlInclude(typeof(ILValue))]
    public class ILValue
    {
        [XmlElement("OBIS_Code",Type = typeof(Get_Index))]
        public Get_Index OBIS_Index;
        [XmlElement("Value", Type = typeof(Object))]
        public Dictionary<byte?, Object> value;
        public string dbField;

        public ILValue()
        {
            OBIS_Index = Get_Index.Dummy;
            ValueCollection = new Dictionary<byte?, object>(1);
            dbField = "";
        }

        /// <summary>
        /// To Store All Values Collection
        /// </summary>
        public Dictionary<byte?, Object> ValueCollection
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Object GetDataItemValue(byte? KeyVal)
        {
            Object val = null;
            try
            {
                val = ValueCollection[KeyVal];
            }
            catch (Exception ex)
            { 
            }
            return val;
        }

        public void SetDataItemValue(byte? KeyVal, Object Value)
        {
            try
            {
                ValueCollection[KeyVal] = Value;
            }
            catch (Exception ex)
            {
            }
        }

        public Object Value
        {
            get
            {
                return GetDataItemValue(0);
            }
            set
            {
                try
                {
                    SetDataItemValue(0, value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to save data", ex);
                }
            }
        }
    }

    public interface IDataGenerator
    {
        List<ILValue[]> GetData(List<DLMS.Base_Class> dataList);
    }

    public class BillingDataGenerator : IDataGenerator
    {
        #region IDataGenerator Members

        public List<ILValue[]> GetData(List<DLMS.Base_Class> CommObjs)
        {
            int captureSize = 0;
            if (CommObjs != null && CommObjs.Count > 0)
            {
                int index = CommObjs.FindIndex(1, (x) => x.INDEX == CommObjs[0].INDEX);
                if (index != -1)
                    captureSize = index;
                else
                    captureSize = CommObjs.Count;
            }
            if (captureSize == 0)
                throw new Exception("Billing Data not present,unable to make ");
            List<ILValue[]> captureObjs = new List<ILValue[]>();
            int Indexer = 0;
            for (int index = 0; index < CommObjs.Count / captureSize; index++)
            {
                ILValue[] tArray = new ILValue[captureSize];
                for (int indexI = 0; indexI < captureSize; indexI++, Indexer++)
                {
                    tArray[indexI] = GetCommObjectValue(CommObjs[Indexer]);
                }
                captureObjs.Add(tArray);
            }
            return captureObjs;

        }

        private ILValue GetCommObjectValue(DLMS.Base_Class CommObj)
        {
            try
            {
                ILValue extValue = new ILValue();
                extValue.OBIS_Index = CommObj.INDEX;
                extValue.Value = null;
                #region Class_1
                if (CommObj is DLMS.Class_1)
                {
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        Class_1 _CommObj = (Class_1)CommObj;
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        else
                            extValue.Value = _CommObj.Value_Obj;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;

                }
                #endregion
                #region Class_3
                else if (CommObj is DLMS.Class_3)
                {
                    Class_3 _CommObj = (Class_3)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_4
                else if (CommObj is DLMS.Class_4)
                {
                    Class_4 _CommObj = (Class_4)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x05, _CommObj.Date_Time_Stamp);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_5
                else if (CommObj is DLMS.Class_5)
                {
                    Class_5 _CommObj = (Class_5)CommObj;
                    ///Store Current Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.CValue != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue);
                        else if (_CommObj.CValue_Array != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue_Array);

                    }
                    ///Store Last Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(0x03) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.LValue != null)
                            extValue.Value = _CommObj.LValue;
                        else if (_CommObj.LValue_Array != null)
                            extValue.Value = _CommObj.LValue_Array;
                        extValue.SetDataItemValue(0x03, extValue.Value);
                    }
                    ///Store Scaler Unit Structure
                    if (CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x04, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Capture Period
                    if (CommObj.GetAttributeDecodingResult(0x06) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x06, _CommObj.capture_time);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Start Time Current
                    if (CommObj.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x07, _CommObj.start_time_Current);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Register Period Values
                    if (CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x08, _CommObj.period);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Size Of Sliding Windows
                    if (CommObj.GetAttributeDecodingResult(0x09) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x09, _CommObj.periodCount);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_8
                else if (CommObj.GetType() == typeof(DLMS.Class_8))
                {
                    if (CommObj.DecodingAttribute == 2 &&
                        CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                        extValue.Value = ((DLMS.Class_8)(CommObj)).Date_Time_Value;
                    //else
                    //    extValue.Value = null;

                }
                #endregion
                else
                    throw new Exception(String.Format("Unable to GetData from type {0}", CommObj.GetType()));
                return extValue;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error extracting data from Communication {0} Object_GetCommObjectValue", CommObj.INDEX), ex);
            }
        }

        #endregion
    }

    public class EventDataGenerator : IDataGenerator
    {
        #region IDataGenerator Members

        public List<ILValue[]> GetData(List<DLMS.Base_Class> CommObjs)
        {
            int captureSize = 0;
            if (CommObjs != null && CommObjs.Count > 0)
            {
                int index = CommObjs.FindIndex(1, (x) => x.INDEX == CommObjs[0].INDEX);
                if (index != -1)
                    captureSize = index;
                else
                    captureSize = CommObjs.Count;
            }
            
            List<ILValue[]> captureObjs = new List<ILValue[]>();
            if (captureSize == 0)
            {
                return captureObjs;
                ///throw new Exception("Event Data not present,unable to make ");
            }
            int Indexer = 0;
            for (int index = 0; index < CommObjs.Count / captureSize; index++)
            {
                ILValue[] tArray = new ILValue[captureSize];
                for (int indexI = 0; indexI < captureSize; indexI++, Indexer++)
                    tArray[indexI] = GetCommObjectValue(CommObjs[Indexer]);
                captureObjs.Add(tArray);
            }
            return captureObjs;

        }

        private ILValue GetCommObjectValue(DLMS.Base_Class CommObj)
        {
            try
            {
                ILValue extValue = new ILValue();
                extValue.OBIS_Index = CommObj.INDEX;
                extValue.Value = null;
                #region Class_1
                if (CommObj is DLMS.Class_1)
                {
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        Class_1 _CommObj = (Class_1)CommObj;
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        else
                            extValue.Value = _CommObj.Value_Obj;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;

                }
                #endregion
                #region Class_3
                else if (CommObj is DLMS.Class_3)
                {
                    Class_3 _CommObj = (Class_3)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_4
                else if (CommObj is DLMS.Class_4)
                {
                    Class_4 _CommObj = (Class_4)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x05, extValue.Value);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_5
                else if (CommObj is DLMS.Class_5)
                {
                    Class_5 _CommObj = (Class_5)CommObj;
                    ///Store Current Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.CValue != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue);
                        else if (_CommObj.CValue_Array != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue_Array);

                    }
                    ///Store Last Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(0x03) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.LValue != null)
                            extValue.Value = _CommObj.LValue;
                        else if (_CommObj.LValue_Array != null)
                            extValue.Value = _CommObj.LValue_Array;
                        extValue.SetDataItemValue(0x03, extValue.Value);
                    }
                    ///Store Scaler Unit Structure
                    if (CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x04, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Capture Period
                    if (CommObj.GetAttributeDecodingResult(0x06) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x06, _CommObj.capture_time);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Start Time Current
                    if (CommObj.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x07, _CommObj.start_time_Current);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Register Period Values
                    if (CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x08, _CommObj.period);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Size Of Sliding Windows
                    if (CommObj.GetAttributeDecodingResult(0x09) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x09, _CommObj.periodCount);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_8
                else if (CommObj.GetType() == typeof(DLMS.Class_8))
                {
                    if (CommObj.DecodingAttribute == 2 &&
                        CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                        extValue.Value = ((DLMS.Class_8)(CommObj)).Date_Time_Value;
                    //else
                    //    extValue.Value = null;

                }
                #endregion
                else
                    throw new Exception(String.Format("Unable to GetData from type {0}", CommObj.GetType()));
                return extValue;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error extracting data from Communication {0} Object_GetCommObjectValue", CommObj.INDEX), ex);
            }
        }

        #endregion
    }

    public class LoadProfileDataGenerator : IDataGenerator
    {
        #region IDataGenerator Members

        public List<ILValue[]> GetData(List<DLMS.Base_Class> CommObjs)
        {
            int captureSize = 0;
            List<ILValue[]> captureObjs = new List<ILValue[]>();
            if (CommObjs != null && CommObjs.Count > 0)
            {
                int index = CommObjs.FindIndex(1, (x) => x.INDEX == CommObjs[0].INDEX);
                if (index != -1)
                    captureSize = index;
                else
                    captureSize = CommObjs.Count;
            }
            if (captureSize == 0)
                return captureObjs;
                //throw new Exception("LoadProfile Data not present,unable to make ");
            int Indexer = 0;
            for (int index = 0; index < CommObjs.Count / captureSize; index++)
            {
                ILValue[] tArray = new ILValue[captureSize];
                for (int indexI = 0; indexI < captureSize; indexI++, Indexer++)
                    tArray[indexI] = GetCommObjectValue(CommObjs[Indexer]);
                captureObjs.Add(tArray);
            }
            return captureObjs;
        }

        private ILValue GetCommObjectValue(DLMS.Base_Class CommObj)
        {
            try
            {
                ILValue extValue = new ILValue();
                extValue.OBIS_Index = CommObj.INDEX;
                extValue.Value = null;
                #region Class_1
                if (CommObj is DLMS.Class_1)
                {
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        Class_1 _CommObj = (Class_1)CommObj;
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        else
                            extValue.Value = _CommObj.Value_Obj;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;

                } 
                #endregion
                #region Class_3
                else if (CommObj is DLMS.Class_3)
                {
                    Class_3 _CommObj = (Class_3)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                } 
                #endregion
                #region Class_4
                else if (CommObj is DLMS.Class_4)
                {
                    Class_4 _CommObj = (Class_4)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x05, extValue.Value);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                } 
                #endregion
                #region Class_5
                else if (CommObj is DLMS.Class_5)
                {
                    Class_5 _CommObj = (Class_5)CommObj;
                    ///Store Current Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.CValue != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue);
                        else if (_CommObj.CValue_Array != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue_Array);

                    }
                    ///Store Last Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(0x03) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.LValue != null)
                            extValue.Value = _CommObj.LValue;
                        else if (_CommObj.LValue_Array != null)
                            extValue.Value = _CommObj.LValue_Array;
                        extValue.SetDataItemValue(0x03, extValue.Value);
                    }
                    ///Store Scaler Unit Structure
                    if (CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x04, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Capture Period
                    if (CommObj.GetAttributeDecodingResult(0x06) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x06, _CommObj.capture_time);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Start Time Current
                    if (CommObj.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x07, _CommObj.start_time_Current);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Register Period Values
                    if (CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x08, _CommObj.period);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Size Of Sliding Windows
                    if (CommObj.GetAttributeDecodingResult(0x09) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x09, _CommObj.periodCount);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_7
                else if(CommObj.GetType() == typeof(DLMS.Class_7))
                {
                    Class_7 _CommObj = (Class_7)CommObj;
                    ///Store Current Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(4) == DLMS.DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x04, _CommObj.capturePeriod);
                    }
                }
                #endregion
                #region Class_8
                else if (CommObj.GetType() == typeof(DLMS.Class_8))
                {
                    if (CommObj.DecodingAttribute == 2 &&
                        CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                        extValue.Value = ((DLMS.Class_8)(CommObj)).Date_Time_Value;
                    //else
                    //    extValue.Value = null;

                } 
                #endregion
                else
                    throw new Exception(String.Format("Unable to GetData from type {0}", CommObj.GetType()));
                return extValue;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error extracting data from Communication {0} Object_GetCommObjectValue",CommObj.INDEX), ex);
            }
        }

        #endregion
    }

    public class InstantaneousDataGenerator : IDataGenerator
    {
        #region IDataGenerator Members

        public List<ILValue[]> GetData(List<DLMS.Base_Class> CommObjs)
        {
            int captureSize = 0;
            if (CommObjs != null && CommObjs.Count > 0)
            {
                int index = CommObjs.FindIndex(1, (x) => x.INDEX == CommObjs[0].INDEX);
                if (index != -1)
                    captureSize = index;
                else
                    captureSize = CommObjs.Count;
            }
            if (captureSize == 0)
                throw new Exception("LoadProfile Data not present,unable to make ");
            List<ILValue[]> captureObjs = new List<ILValue[]>();
            int Indexer = 0;
            for (int index = 0; index < CommObjs.Count / captureSize; index++)
            {
                ILValue[] tArray = new ILValue[captureSize];
                for (int indexI = 0; indexI < captureSize; indexI++, Indexer++)
                    tArray[indexI] = GetCommObjectValue(CommObjs[Indexer]);
                captureObjs.Add(tArray);
            }
            return captureObjs;
        }

        private ILValue GetCommObjectValue(DLMS.Base_Class CommObj)
        {
            try
            {
                ILValue extValue = new ILValue();
                extValue.OBIS_Index = CommObj.INDEX;
                extValue.Value = null;
                #region Class_1
                if (CommObj is DLMS.Class_1)
                {
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        Class_1 _CommObj = (Class_1)CommObj;
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        else
                            extValue.Value = _CommObj.Value_Obj;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;

                }
                #endregion
                #region Class_3
                else if (CommObj is DLMS.Class_3)
                {
                    Class_3 _CommObj = (Class_3)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        else if (_CommObj.Value_Array != null)
                            extValue.Value = _CommObj.Value_Array;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_4
                else if (CommObj is DLMS.Class_4)
                {
                    Class_4 _CommObj = (Class_4)CommObj;
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.Value != null)
                            extValue.Value = _CommObj.Value;
                        extValue.SetDataItemValue(0x02, extValue.Value);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x03, scl);
                    }
                    if (CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x05, extValue.Value);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_5
                else if (CommObj is DLMS.Class_5)
                {
                    Class_5 _CommObj = (Class_5)CommObj;
                    ///Store Current Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.CValue != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue);
                        else if (_CommObj.CValue_Array != null)
                            extValue.SetDataItemValue(0x02, _CommObj.CValue_Array);

                    }
                    ///Store Last Average Value Feild
                    if (CommObj.GetAttributeDecodingResult(0x03) == DLMS.DecodingResult.Ready)
                    {
                        if (_CommObj.LValue != null)
                            extValue.Value = _CommObj.LValue;
                        else if (_CommObj.LValue_Array != null)
                            extValue.Value = _CommObj.LValue_Array;
                        extValue.SetDataItemValue(0x03, extValue.Value);
                    }
                    ///Store Scaler Unit Structure
                    if (CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                    {
                        Unit_Scaler scl = new Unit_Scaler();
                        scl.Unit = _CommObj.Unit;
                        scl.Scaler = _CommObj.scaler;
                        extValue.SetDataItemValue(0x04, scl);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Capture Period
                    if (CommObj.GetAttributeDecodingResult(0x06) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x06, _CommObj.capture_time);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Start Time Current
                    if (CommObj.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x07, _CommObj.start_time_Current);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Register Period Values
                    if (CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x08, _CommObj.period);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                    ///Store Size Of Sliding Windows
                    if (CommObj.GetAttributeDecodingResult(0x09) == DecodingResult.Ready)
                    {
                        extValue.SetDataItemValue(0x09, _CommObj.periodCount);
                    }
                    //else
                    //    extValue.Value = Double.PositiveInfinity;
                }
                #endregion
                #region Class_8
                else if (CommObj.GetType() == typeof(DLMS.Class_8))
                {
                    if (CommObj.DecodingAttribute == 2 &&
                        CommObj.GetAttributeDecodingResult(2) == DLMS.DecodingResult.Ready)
                        extValue.Value = ((DLMS.Class_8)(CommObj)).Date_Time_Value;
                    //else
                    //    extValue.Value = null;

                }
                #endregion
                else
                    throw new Exception(String.Format("Unable to GetData from type {0}", CommObj.GetType()));
                return extValue;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error extracting data from Communication {0} Object_GetCommObjectValue", CommObj.INDEX), ex);
            }
        }

        #endregion
    }
}
