using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// For-each visible COSEM objects within the given Application Association(AAs), <see cref="OBISCodeRights"/> object is stored against each
    /// <see cref="Get_Index"/> as Index KEY.
    /// The OBISCodeRights object store with their class_id,version, logical name and the access rights to their attributes and methods
    /// </summary>
    public class SAPTable
    {
        #region Data_Members
        private Dictionary<StOBISCode, StOBISCode> obisLabelLookup;

        //private Dictionary<StOBISCode, StOBISCode> obisLabelLookup;
        private Dictionary<StOBISCode, OBISCodeRights> sapTable;

        #endregion

        public SAPTable()
        {
            obisLabelLookup = new Dictionary<StOBISCode, StOBISCode>(1024);
            this.SapTable = new Dictionary<StOBISCode, OBISCodeRights>(512);
        }

        #region Properties
        
        /// <summary>
        /// Get/Set the Dictionary represent a SAP table
        /// </summary>
        /// <remarks>that particular attribute of a particular class object represents through a <see cref="Get_Index"/> and their respective rights are represent through
        /// <see cref="OBISCodeRights"/> so this structure map <see cref="OBISCodeRights"/> to its respective owner <see cref="Get_Index"/>
        /// </remarks>
        public Dictionary<StOBISCode, StOBISCode> OBISLabelLookup
        {
            get { return obisLabelLookup; }
            set { obisLabelLookup = value; }
        }

        public Dictionary<StOBISCode, OBISCodeRights> SapTable
        {
            get { return sapTable; }
            set { sapTable = value; }
        }

        #endregion

        #region Members

        #region OBIS Code Rights

        /// <summary>
        /// Provides <see cref="OBISCodeRights"/> object for specific <see cref="Get_Index"/> as Index KEY
        /// </summary>
        /// <param name="OBIS_Index"><see cref="Get_Index"/> as Index KEY</param>
        /// <returns><see cref="OBISCodeRights"/> object</returns>
        public OBISCodeRights GetOBISCodeRights(Get_Index OBIS_Index)
        {
            try
            {
                OBISCodeRights Rights = null;
                StOBISCode obisVal = Get_Index.Dummy;
                try
                {
                    obisVal = OBIS_Index;
                    Rights = sapTable[obisVal];
                }
                catch (Exception)
                {
                    Rights = null;
                }
                if (Rights == null)
                {
                    Rights = new OBISCodeRights();
                    Rights.OBISIndex = obisVal;
                }
                return Rights;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to compute access rights for object {0}", OBIS_Index));
            }
        }

        /// <summary>
        /// Provides <see cref="OBISCodeRights"/> object from specific <see cref="StOBISCode"/> 
        /// </summary>
        /// <param name="OBIS_Index"><see cref="Get_Index"/> as Index KEY</param>
        /// <returns><see cref="OBISCodeRights"/> object</returns>
        public OBISCodeRights GetOBISCodeRights(StOBISCode OBIS_Index)
        {
            try
            {
                OBISCodeRights Rights = null;
                try
                {
                    if (SapTable.Keys.Contains<StOBISCode>(OBIS_Index))
                        Rights = sapTable[OBIS_Index];
                }
                catch (Exception)
                {
                    Rights = null;
                }

                if (Rights == null)
                {
                    Rights = new OBISCodeRights();
                    Rights.OBISIndex = OBIS_Index;
                }

                return Rights;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to compute access rights for object {0}", OBIS_Index));
            }
        }

        /// <summary>
        /// Set/Map <see cref="OBISCodeRights"/> to its respective <see cref="Get_Index"/>
        /// </summary>
        /// <param name="Rights"><see cref="OBISCodeRights"/> object</param>
        public void AddOBISRights(OBISCodeRights Rights)
        {
            try
            {
                sapTable.Add(Rights.OBISIndex, Rights);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to save access rights for object"));
            }
        }

        /// <summary>
        /// Set/Map <see cref="OBISCodeRights"/> objects to their respective <see cref="Get_Index"/> KEYs
        /// </summary>
        /// <param name="Rights"><see cref="OBISCodeRights"/> object</param>
        public void AddRangeOBISRights(List<OBISCodeRights> Rights)
        {
            try
            {
                foreach (var OBISItem in Rights)
                {
                    if (!sapTable.ContainsKey(OBISItem.OBISIndex))
                        sapTable.Add(OBISItem.OBISIndex, OBISItem);
                    else
                    {
                        sapTable.Remove(OBISItem.OBISIndex);
                        sapTable.Add(OBISItem.OBISIndex, OBISItem);
                    }
                    // sapTable.Add(OBISItem.OBISIndex, OBISItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save access rights for objects");
            }
        }

        #endregion

        #region OBIS Labels

        public StOBISCode GetOBISCode(Get_Index OBIS_Index)
        {
            try
            {
                StOBISCode obisVal = Get_Index.Dummy;
                try
                {
                    obisVal = OBISLabelLookup[OBIS_Index];
                }
                catch (Exception)
                {
                    obisVal = Get_Index.Dummy;
                }
                return obisVal;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to find OBIS Code for object {0}", OBIS_Index));
            }
        }

        public void AddOBISCode(Get_Index OBIS_Index, StOBISCode obisVal)
        {
            Get_Index DicKey = Get_Index.Dummy;
            try
            {
                DicKey = OBIS_Index;//Convert.ToUInt64(OBIS_Index);

                if (!OBISLabelLookup.ContainsKey(DicKey))
                    OBISLabelLookup.Add(DicKey, obisVal);
                else
                {
                    OBISLabelLookup.Remove(DicKey);
                    OBISLabelLookup.Add(DicKey, obisVal);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to Save OBIS Code for object {0}", OBIS_Index), ex);
            }
        }

        public void AddRangeOBISCode(List<KeyValuePair<StOBISCode, StOBISCode>> obisValues)
        {
            StOBISCode Obis_Index = Get_Index.Dummy;
            StOBISCode DicKey = Get_Index.Dummy;

            try
            {
                foreach (var OBISValue in obisValues)
                {
                    try
                    {
                        Obis_Index = OBISValue.Key;
                    }
                    catch
                    { }

                    DicKey = OBISValue.Key;

                    if (!OBISLabelLookup.ContainsKey(DicKey))
                        OBISLabelLookup.Add(DicKey, OBISValue.Value);
                    else
                    {
                        OBISLabelLookup.Remove(DicKey);
                        OBISLabelLookup.Add(DicKey, OBISValue.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to Save OBIS Code for object {0}", Obis_Index), ex);
            }
        }

        #endregion

        #endregion
    }

}
