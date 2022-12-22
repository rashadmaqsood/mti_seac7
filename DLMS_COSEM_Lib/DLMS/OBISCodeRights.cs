using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// This Class Contains particular COSEM object with their class_id, version, logical name and
    /// the access rights to their attributes and methods within the given application association context.
    /// The OBISCodeRights class Instance could be used load Current Application Association (AAs) access rights from 
    /// DLMS_COSEM compliant Meter Device see IC <see cref="Class_15"/> then lator these objects are stored in
    /// SAPTable<see cref="DLMS.SAPTable"/> for future reference.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(StOBISCode))]
    public class OBISCodeRights : ICloneable
    {
        #region DataMembers
        private Get_Index obis_id;
        private StOBISCode oBISIndex;
        private byte _version;
        private List<byte[]> _attribRights;
        private List<byte[]> _methodRights;
        private List<byte[]> _AccessSelectors;

        private byte isHLSApplicable = 0;
        internal byte _CheckAccessRights = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OBISCodeRights()
        {
            Version = 0;
            AttributeRights = new List<byte[]>(1);
            MethodRights = new List<byte[]>(1);
            AccessSelectors = new List<byte[]>(1);
            OBISIndex = Get_Index.Dummy;
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="OBISCode">OBIS Code<see cref="StOBISCode"/>logical name for Instace</param>
        /// <param name="classId">Class Id for (IC)Interface Class</param>
        /// <param name="version">Version No for (IC)Interface Class</param>
        /// <param name="attribRights">The access rights List to their attributes</param>
        /// <param name="methodRights">The access rights List to their methods</param>
        public OBISCodeRights(byte[] OBISCode, ushort classId, byte version, List<byte[]> attribRights, List<byte[]> methodRights)
        {
            Version = version;
            AttributeRights = attribRights;
            MethodRights = methodRights;
            AccessSelectors = new List<byte[]>(1);
            StOBISCode tmp = StOBISCode.ConvertFrom(OBISCode);
            tmp.ClassId = classId;
            OBISIndex = tmp;
        }

        #endregion

        #region Properties
        [XmlElement("OBISLabel", Type = typeof(Get_Index))]
        public Get_Index OBISLabel
        {
            get { return obis_id; }
            set { obis_id = value; }
        }
        /// <summary>
        /// OBIS Code<see cref="StOBISCode"/>logical name for Instace of Interface Class
        /// </summary>
        [XmlElement("OBISIndex", Type = typeof(StOBISCode))]
        public StOBISCode OBISIndex
        {
            get { return oBISIndex; }
            set { oBISIndex = value; }
        }

        /// <summary>
        /// OBIS Code (bytes) logical name for Instance of Interface Class
        /// </summary>
        [XmlIgnore()]
        public byte[] OBISCode
        {
            get { return OBISIndex.OBISCode; }
            set { OBISIndex = StOBISCode.ConvertFrom(value); }
        }
        /// <summary>
        /// Class Id for (IC)Interface Class
        /// </summary>
        [XmlIgnore()]
        public ushort ClassId
        {
            get { return OBISIndex.ClassId; }
            set
            {
                StOBISCode t = (Get_Index)OBISIndex.OBIS_Value;
                t.ClassId = value;
                OBISIndex = t;
            }
        }
        /// <summary>
        /// Version No for (IC)Interface Class
        /// </summary>
        [XmlElement("DLMS_Version", Type = typeof(byte))]
        public byte Version
        {
            get { return _version; }
            set { _version = value; }
        }
        /// <summary>
        /// The access rights List to their related attributes
        /// </summary>
        [XmlArrayItem("Attrib_Right", Type = typeof(byte[]))]
        [XmlArray("Attributes_Rights")]
        public List<byte[]> AttributeRights
        {
            get { return _attribRights; }
            set { _attribRights = value; }
        }
        /// <summary>
        /// The Access_Selector List to their related attributes
        /// </summary>
        [XmlArray("Access_Selectors")]
        public List<byte[]> AccessSelectors
        {
            get { return _AccessSelectors; }
            set { _AccessSelectors = value; }
        }

        /// <summary>
        /// The access rights List to their methods
        /// </summary>
        [XmlArrayItem("Method_Right", Type = typeof(byte[]))]
        [XmlArray("Methods_Rights")]
        public List<byte[]> MethodRights
        {
            get { return _methodRights; }
            set { _methodRights = value; }
        }
        /// <summary>
        /// Get the Attribute count
        /// </summary>
        [XmlIgnore()]
        public uint TotalAttributes
        {
            get { return (uint)AttributeRights.Count; }

        }

        /// <summary>
        /// Get the method count
        /// </summary>
        [XmlIgnore()]
        public uint TotalMethods
        {
            get { return (uint)MethodRights.Count; }
        }

        public bool IsRightsLoaded
        {
            get
            {
                bool isRightsLoaded = false;

                try
                {
                    if ((oBISIndex != Get_Index.Dummy &&
                         AttributeRights != null && AttributeRights.Count > 0 &&
                         AccessSelectors != null && AccessSelectors.Count >= 0))
                    {
                        isRightsLoaded = true;
                    }

                    return isRightsLoaded;
                }
                catch
                {
                }

                return isRightsLoaded;
            }
        }

        [XmlIgnore()]
        public bool CheckAccessRights
        {
            get { return Convert.ToBoolean(_CheckAccessRights); }
            set { _CheckAccessRights = Convert.ToByte(value); }
        }

        [XmlIgnore()]
        public bool IsHLSApplicable
        {
            get { return Convert.ToBoolean(isHLSApplicable); }
            internal set { isHLSApplicable = Convert.ToByte(value); }
        }

        #endregion

        #region Member_Methods

        /// <summary>
        /// The method gives the access right for Specific attribute, <see cref="Attrib_Access_Modes"/> 
        /// </summary>
        /// <param name="attribId">Given Attribute Id</param>
        /// <returns><see cref="Attrib_Access_Modes"/></returns>
        public Attrib_Access_Modes GetAttribRight(int attribId)
        {
            try
            {
                byte[] AttribAccess = AttributeRights.Find((x => x[0] == attribId));
                if (AttribAccess == null || AttribAccess.Length != 2)
                    return Attrib_Access_Modes.No_Access;
                else
                    return (Attrib_Access_Modes)AttribAccess[1];
            }
            catch (Exception)
            {
                // Check Error
                return Attrib_Access_Modes.No_Access;
            }

            // if (attribId <= TotalAttributes)
            // {
            //     return (Attrib_Access_Modes)AttributeRights[attribId];
            // }
            // else
            //     return Attrib_Access_Modes.No_Access; ///Or Throw Exception
        }

        /// <summary>
        /// The method provide Access_Selector for Specific attribute
        /// </summary>
        /// <param name="attribId">Given Attribute Id</param>
        /// <returns>Access_Selector</returns>
        public byte GetAccessSelectorRight(int attribId)
        {
            try
            {
                byte[] AccessSelector = AccessSelectors.Find((x => x[0] == attribId));
                if (AccessSelector == null || AccessSelector.Length != 2)
                    return 0;
                else
                    return AccessSelector[1];
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// The method gives the access right for Specific method <see cref="Method_Access_Modes"/> 
        /// </summary>
        /// <param name="methodId">get right of this method</param>
        /// <returns><see cref="Method_Access_Modes"/></returns>
        public Method_Access_Modes GetMethodRight(int methodId)
        {
            try
            {
                byte[] MethodAccess = MethodRights.Find((x => x[0] == methodId));
                if (MethodAccess == null || MethodAccess.Length != 2)
                    return Method_Access_Modes.No_Access;
                else
                    return (Method_Access_Modes)MethodAccess[1];
            }
            catch (Exception)
            {
                return Method_Access_Modes.No_Access;
            }

            // if (methodId <= TotalMethods)
            // {
            //     return (Method_Access_Modes)MethodRights[methodId];
            // }
            // else
            //     return Method_Access_Modes.No_Access; ///Or Throw Exception
        }

        /// <summary>
        /// The method set the access right for Specific attribute <see cref="Attrib_Access_Modes"/> 
        /// </summary>
        /// <param name="attribId">specific attribute</param>
        /// <param name="accessLevel">Access Right enum<see cref="Attrib_Access_Modes"/></param>
        public void SetAttribRight(int attribId, Attrib_Access_Modes accessLevel)
        {
            byte[] AttribRight = new byte[2];
            AttribRight[0] = (byte)attribId;
            AttribRight[1] = (byte)accessLevel;
            bool flag = false;

            // Check Either attribute already existed
            for (int index = 0; index < AttributeRights.Count; index++)
            {
                if (AttributeRights[index][0] == attribId)
                {
                    flag = true;
                    AttributeRights[index] = AttribRight;      //Update Attribute Access Rights
                    break;
                }
            }
            if (!flag)
            {
                AttributeRights.Add(AttribRight);
            }
            // Sort List With Respect to IDs
            AttributeRights.Sort((x, y) => x[0].CompareTo(y[0]));
        }

        public void SetAccessSelectorRight(int attribId, SelectiveAccessType Val)
        {
            SetAccessSelectorRight(attribId, Convert.ToByte(Val));
        }

        /// <summary>
        /// The method provide Access_Selector for Specific attribute
        /// </summary>
        /// <param name="attribId">Given Attribute Id</param>
        /// <param name="Val">Access_Selector</param>
        public void SetAccessSelectorRight(int attribId, byte Val)
        {
            try
            {
                byte[] SelectorRight = new byte[2];
                SelectorRight[0] = (byte)attribId;
                SelectorRight[1] = Val;
                bool flag = false;
                // Check Either attribute already existed
                for (int index = 0; index < AccessSelectors.Count; index++)
                {
                    if (AccessSelectors[index][0] == attribId)
                    {
                        flag = true;
                        AccessSelectors[index] = SelectorRight;      // Update Attribute Access Rights
                        break;
                    }
                }
                if (!flag)
                {
                    AccessSelectors.Add(SelectorRight);
                }
                // Sort List With Respect to IDs
                AttributeRights.Sort((x, y) => x[0].CompareTo(y[0]));
            }
            finally
            {

            }
        }

        /// <summary>
        /// The method set access right for Specific method <see cref="Method_Access_Modes"/> 
        /// </summary>
        /// <param name="methodId">Given method Id</param>
        /// <param name="accessLevel">Access Right enum <see cref="Method_Access_Modes"/></param>
        public void SetMethodRight(int methodId, Method_Access_Modes accessLevel)
        {
            byte[] MethodRight = new byte[2];
            MethodRight[0] = (byte)methodId;
            MethodRight[1] = (byte)accessLevel;
            bool flag = false;
            ///Check Either attribute already existed
            for (int index = 0; index < MethodRights.Count; index++)
            {
                if (MethodRights[index][0] == methodId)
                {
                    flag = true;
                    MethodRights[index] = MethodRight;      //Update Attribute Access Rights
                    break;
                }

            }
            if (!flag)
            {
                MethodRights.Add(MethodRight);
            }
            // Sort Method Rights With Respect to Id
            MethodRights.Sort((x, y) => x[0].CompareTo(y[0]));
            // else Or Throw Exception
        }

        #endregion

        #region Support_Methods

        public bool IsAttribWritable(byte AttribNo)
        {
            if (!CheckAccessRights) return true;
            bool isWriteable = false;
            try
            {
                if (AttribNo > 0)
                {
                    Attrib_Access_Modes attribAccess = GetAttribRight(AttribNo);

                    if (attribAccess == Attrib_Access_Modes.Authenticated_Write_Only ||
                        attribAccess == Attrib_Access_Modes.Authenticated_Read_Write)
                    {
                        isWriteable = IsHLSApplicable && true;
                    }
                    else if (attribAccess == Attrib_Access_Modes.Write_Only ||
                             attribAccess == Attrib_Access_Modes.Read_Write)
                    {
                        isWriteable = true;
                    }
                    else
                        isWriteable = false;
                }
                else
                {
                    isWriteable = false;
                    for (int attribT = 1; attribT <= AttributeRights.Count; attribT++)
                    {
                        Attrib_Access_Modes attribAccess = GetAttribRight(attribT);
                        // evaluate Access Rights
                        if (attribAccess == Attrib_Access_Modes.Authenticated_Write_Only ||
                            attribAccess == Attrib_Access_Modes.Authenticated_Read_Write)
                        {
                            isWriteable = IsHLSApplicable && true;
                        }
                        else if (attribAccess == Attrib_Access_Modes.Write_Only ||
                                 attribAccess == Attrib_Access_Modes.Read_Write)
                        {
                            isWriteable = true;
                        }
                        else
                            isWriteable = false;

                        if (!isWriteable)
                            continue;
                        else
                            break;
                    }
                }
            }
            catch
            {
                // throw new DLMSException("Unable to determine access rights",ex);
                // Hard Coding & ByPass Access Rights
                return false;
            }
            return isWriteable;
        }

        public bool IsAttribReadable(byte AttribNo)
        {
            if (!CheckAccessRights) return true;
            bool isReadable = false;
            try
            {
                if (AttribNo > 0)
                {
                    Attrib_Access_Modes attribAccess = GetAttribRight(AttribNo);

                    if (attribAccess == Attrib_Access_Modes.Authenticated_Read_Only ||
                        attribAccess == Attrib_Access_Modes.Authenticated_Read_Write)
                    {
                        isReadable = IsHLSApplicable && true;
                    }
                    else if (attribAccess == Attrib_Access_Modes.Read_Only ||
                             attribAccess == Attrib_Access_Modes.Read_Write)
                    {
                        isReadable = true;
                    }
                    else
                        isReadable = false;
                }
                else
                {
                    isReadable = false;
                    for (int attribT = 1; attribT <= AttributeRights.Count; attribT++)
                    {
                        Attrib_Access_Modes attribAccess = GetAttribRight(attribT);
                        // evaluate Access Rights
                        if (attribAccess == Attrib_Access_Modes.Authenticated_Read_Only ||
                            attribAccess == Attrib_Access_Modes.Authenticated_Read_Write)
                        {
                            isReadable = IsHLSApplicable && true;
                        }
                        else if (attribAccess == Attrib_Access_Modes.Read_Only ||
                                 attribAccess == Attrib_Access_Modes.Read_Write)
                        {
                            isReadable = true;
                        }
                        else
                            isReadable = false;

                        if (!isReadable)
                            continue;
                        else
                            break;
                    }
                }
            }
            catch
            {
                // throw new DLMSException("Unable to determine access rights",ex);
                // Hard Coding & ByPass Access Rights
                return false;
            }
            return isReadable;
        }

        public bool IsMethodInvokable(byte MethodNo)
        {
            bool isMethodInvokable = false;
            try
            {
                if (!CheckAccessRights) return true;
                if (MethodNo > 0)
                {
                    Method_Access_Modes methodAccess = GetMethodRight(MethodNo);

                    if (methodAccess == Method_Access_Modes.Authenticated_Access)
                    {
                        isMethodInvokable = IsHLSApplicable && true;
                    }
                    else if (methodAccess == Method_Access_Modes.Access)
                    {
                        isMethodInvokable = true;
                    }
                    else
                        isMethodInvokable = false;
                }
            }
            catch
            {
                // throw new DLMSException("Unable to determine access rights",ex);
                // Hard Coding & ByPass Access Rights
                return false;
            }
            return isMethodInvokable;
        }

        public SelectiveAccessType IsAccessSelecterApplied(byte AttribNo)
        {
            try
            {
                if (AttribNo != 0)
                {
                    byte attribAccess = GetAccessSelectorRight(AttribNo);
                    return (SelectiveAccessType)attribAccess;
                }
                else
                {
                    return SelectiveAccessType.Not_Applied;
                }
            }
            catch (Exception ex)
            {
                return SelectiveAccessType.Not_Applied;
            }
        }

        internal void Grant_Attribute_Rights(int Attribs_No, int attributeNo = 0, Attrib_Access_Modes AccessRight = Attrib_Access_Modes.Read_Write)
        {
            try
            {
                if (attributeNo == 0 ||
                   !(attributeNo >= 1 && attributeNo <= Attribs_No))
                    attributeNo = 0;

                if (attributeNo == 0)
                    for (attributeNo = 1; attributeNo <= Attribs_No; attributeNo++)
                        SetAttribRight(attributeNo, AccessRight);
                else
                    SetAttribRight(attributeNo, AccessRight);

                IsHLSApplicable = true;
            }
            catch { }
        }

        internal void Grant_Method_Rights(int Methods_No, int methodId = 0, Method_Access_Modes AccessRight = Method_Access_Modes.Access)
        {
            try
            {
                if (methodId == 0 ||
                   !(methodId >= 1 && methodId <= Methods_No))
                    methodId = 0;

                if (methodId == 0)
                    for (methodId = 1; methodId <= Methods_No; methodId++)
                        SetMethodRight(methodId, AccessRight);
                else
                    SetMethodRight(methodId, AccessRight);

                IsHLSApplicable = true;
            }
            catch { }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            byte[] _obis_code = new byte[6];
            Array.Copy(OBISCode, _obis_code, _obis_code.Length);

            List<byte[]> _attribR = new List<byte[]>((this.AttributeRights == null) ? new List<byte[]>() : this.AttributeRights);
            List<byte[]> _methodR = new List<byte[]>((this.MethodRights == null) ? new List<byte[]>() : this.MethodRights);
            List<byte[]> _accessSel = new List<byte[]>((this.AccessSelectors == null) ? new List<byte[]>() : AccessSelectors);
            OBISCodeRights clone = new OBISCodeRights(_obis_code, this.ClassId, Version, _attribR, _methodR);
            clone.AccessSelectors = _accessSel;
            return clone;
        }

        #endregion
    }
    public enum OBISCodeRightType : byte
    {
        Attribute = 1,
        Method = 2,
        AccessSelector = 3
    }
}
