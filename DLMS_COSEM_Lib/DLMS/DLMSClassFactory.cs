using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// This IDLMSClassFacotry interface provides interface for Factory Design Pattern Implementation, 
    /// this pattern provide DLMS_COSEM Interface Classes instances creation and initializing Properly.
    /// IDLMSClassFacotry is Interface for Factory Class and generic prototype function for Factory Method.
    /// </summary>
    public interface IDLMSClassFacotry
    {
        Base_Class DLMS_FactoryMethod(StOBISCode OBISCodeIdentifier);
    }

    /// <summary>
    /// This DLMSClassFactory Class implements Factory Design Pattern 
    /// this pattern provide DLMS_COSEM Interface Classes instances creation and initializing Properly.
    /// DLMSClassFactory implement IDLMSClassFacotry interface and provide generic prototype function for Factory Method.
    /// </summary>
    public class DLMSClassFactory : IDLMSClassFacotry
    {
        #region GetSAPEntryDelgegate

        private GetSAPEntryKeyIndex GetSAPEntryDlg;
        private GetSAPRights GetSAPAccessRightsDlg;

        /// <summary>
        /// Get/Set the delegate of type <see cref="GetSAPEntryKeyIndex"/>
        /// </summary>
        public GetSAPEntryKeyIndex GetSAPEntryDelegate
        {
            get { return GetSAPEntryDlg; }
            set { GetSAPEntryDlg = value; }
        }

        /// <summary>
        /// Get/Set the delegate of type <see cref="GetSAPRights"/>
        /// </summary>
        public GetSAPRights GetSAPAccessRightsDelegate
        {
            get { return GetSAPAccessRightsDlg; }
            set { GetSAPAccessRightsDlg = value; }
        }

        #endregion

        public DLMSClassFactory() { }

        /// <summary>
        /// This method implement Factory pattren and create instances of each DLSM/COSEM class through class id
        /// </summary>
        /// <param name="OBISCodes">Get the class id from <see cref="StOBISCode"/></param>
        /// <returns>Newly Created class packed in abstract <see cref="Base_Class"/></returns>
        public Base_Class DLMS_Classes_Factory(StOBISCode OBISCodes)
        {
            try
            {
                Base_Class Obj_ToRet = null;
                switch (OBISCodes.ClassId)
                {
                    #region Switch Cases
                    //DLSM_Class_1
                    case 1:
                        Obj_ToRet = DLMS_Class_1_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_3 
                    case 3:
                        Obj_ToRet = DLMS_Class_3_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_4 MDI's
                    case 4:
                        Obj_ToRet = DLMS_Class_4_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_5
                    case 5:
                        Obj_ToRet = DLMS_Class_5_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_6
                    case 6:
                        Obj_ToRet = DLMS_Class_6_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_7 Generic Profile
                    case 7:
                        Obj_ToRet = DLMS_Class_7_Factory(OBISCodes.OBISIndex, this.GetSAPEntryDelegate);
                        break;
                    //DLSM_Class_8 Meter Clock
                    case 8:
                        Obj_ToRet = DLMS_Class_8_Factory(OBISCodes.OBISIndex);
                        break;
                    // DLSM_Class_9 Script Table
                    case 9:
                        Obj_ToRet = DLMS_Class_9_Factory(OBISCodes.OBISIndex);
                        break;
                    // DLSM_Class_11
                    case 11:
                        Obj_ToRet = DLMS_Class_11_Factory(OBISCodes.OBISIndex);
                        break;
                    // DLSM_Class_15 OBIS Codes
                    case 15:
                        Obj_ToRet = DLMS_Class_15_Factory(OBISCodes.OBISIndex);
                        break;
                    // DLSM_Class_17 SAP Table
                    case 17:
                        Obj_ToRet = DLMS_Class_17_Factory(OBISCodes.OBISIndex);
                        break;
                    // DLSM_Class_20 
                    case 20:
                        Obj_ToRet = DLMS_Class_20_Factory(OBISCodes.OBISIndex);
                        break;
                    // DLSM_Class_21 
                    case 21:
                        Obj_ToRet = DLMS_Class_21_Factory(OBISCodes.OBISIndex);
                        break;
                    // DLSM_Class_22 
                    case 22:
                        Obj_ToRet = DLMS_Class_22_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_41 
                    case 41:
                        Obj_ToRet = DLMS_Class_41_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_42 
                    case 42:
                        Obj_ToRet = DLMS_Class_42_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_45 
                    case 45:
                        Obj_ToRet = DLMS_Class_45_Factory(OBISCodes.OBISIndex);
                        break;
                    //DLSM_Class_70 
                    case 70:
                        Obj_ToRet = DLMS_Class_70_Factory(OBISCodes.OBISIndex);
                        break;
                        #endregion
                }
                return Obj_ToRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region DLMS_Classes_Factory Methods

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_1 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_1"/>instance</returns>
        public Class_1 DLMS_Class_1_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 1;
                Class_1 OBj = new Class_1(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to create Class_1 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_3 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_3"/>instance</returns>
        public Class_3 DLMS_Class_3_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 3;
                Class_3 OBj = new Class_3(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to create Class_3 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_4 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_4"/>instance</returns>
        public Class_4 DLMS_Class_4_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 4;
                Class_4 OBj = new Class_4(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to create Class_4 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_5 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_5"/>instance</returns>
        public Class_5 DLMS_Class_5_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 5;
                Class_5 OBj = new Class_5(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to create Class_5 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_6 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_6"/>instance</returns>
        public Class_6 DLMS_Class_6_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 6;
                Class_6 OBj = new Class_6(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to create Class_6 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_7 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <param name="SapEntryDlg">The delegate of type <see cref="GetSAPEntryKeyIndex"/></param>
        /// <returns><see cref="Class_7"/>instance</returns>
        public Class_7 DLMS_Class_7_Factory(Get_Index OBISCode, GetSAPEntryKeyIndex SapEntryDlg)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 7;
                Class_7 OBj = new Class_7(newOBjCode, SapEntryDlg);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to create Class_7 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_7 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_7"/>instance</returns>
        public Class_7 DLMS_Class_7_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 7;
                Class_7 OBj = new Class_7(newOBjCode, this.GetSAPEntryDelegate);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_7 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_8 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_8"/>instance</returns>
        public Class_8 DLMS_Class_8_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 8;
                Class_8 OBj = new Class_8(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_8 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_9 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_9"/>instance</returns>
        public Class_9 DLMS_Class_9_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 9;
                Class_9 OBj = new Class_9(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_9 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_11 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_11"/>instance</returns>
        public Class_11 DLMS_Class_11_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 11;
                Class_11 OBj = new Class_11(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_11 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_15 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_15"/>instance</returns>
        public Class_15 DLMS_Class_15_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 15;
                Class_15 OBj = new Class_15(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_15 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_17 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_17"/>instance</returns>
        public Class_17 DLMS_Class_17_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 17;
                Class_17 OBj = new Class_17(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_17 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_20 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_20"/>instance</returns>
        public Class_20 DLMS_Class_20_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 20;
                Class_20 OBj = new Class_20(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_20 object {0}", OBISCode));
            }
        }

        public Class_21 DLMS_Class_21_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 7;
                Class_21 OBj = new Class_21(newOBjCode, this.GetSAPEntryDelegate);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_21 object {0}", OBISCode));
            }
        }

        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_22 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_22"/>instance</returns>
        public Class_22 DLMS_Class_22_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 22;
                Class_22 OBj = new Class_22(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_22 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_41 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_41"/>instance</returns>
        public Class_41 DLMS_Class_41_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 41;
                Class_41 OBj = new Class_41(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_41 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_42 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_42"/>instance</returns>
        public Class_42 DLMS_Class_42_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 42;
                Class_42 OBj = new Class_42(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_42 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_45 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_45"/>instance</returns>
        public Class_45 DLMS_Class_45_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 45;
                Class_45 OBj = new Class_45(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_45 object {0}", OBISCode));
            }
        }
        /// <summary>
        /// Methods Implements Factory Design Pattern to build DLMS_COSEM Interface class_70 Instance from StOBISCodes 
        /// </summary>
        /// <param name="OBISCode">The OBIS Code(LN) for IC Instance</param>
        /// <returns><see cref="Class_70"/>instance</returns>
        public Class_70 DLMS_Class_70_Factory(Get_Index OBISCode)
        {
            try
            {
                StOBISCode newOBjCode = OBISCode;
                newOBjCode.ClassId = 70;
                Class_70 OBj = new Class_70(newOBjCode);
                OBj.Rights = GetSAPAccessRightsDelegate.Invoke(newOBjCode);
                return OBj;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to create Class_70 object {0}", OBISCode));
            }
        }

        #endregion

        #region IDLMSClassFacotry Members



        /// <summary>
        /// This method implements Factory Pattern and reflection methodology to create an instance of each class specified by DLMS/COSEM
        /// </summary>
        /// <param name="OBISCodeIdentifier">The OBIS Code for IC Instance to create<see cref="StOBISCode"/></param>
        /// <returns><see cref="Base_Class"/>DLMS_COSEM Interface Class(IC) Instance</returns>
        Base_Class IDLMSClassFacotry.DLMS_FactoryMethod(StOBISCode OBISCodeIdentifier)
        {
            try
            {
                string className = String.Format("DLMS.Class_{0}", OBISCodeIdentifier.ClassId);
                Assembly DLMSLibInfo = Assembly.Load("DLMS_COSEM_Lib");
                Type ClassType = DLMSLibInfo.GetType(className);
                Base_Class OBJ = null;
                if (ClassType == typeof(Class_7) || ClassType == typeof(Class_21))
                {
                    OBJ = (Base_Class)Activator.CreateInstance(ClassType, new object[] { OBISCodeIdentifier, this.GetSAPEntryDelegate });
                }
                else
                {
                    OBJ = (Base_Class)Activator.CreateInstance(ClassType, new object[] { OBISCodeIdentifier });
                }
                // OBJ.Rights = this.GetSAPAccessRightsDelegate.Invoke(OBISCodeIdentifier.OBISIndex);
                return OBJ;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to build object of {0}", OBISCodeIdentifier), ex);
            }
        }

        /// <summary>
        /// This method implements Factory Pattern to create instances of each class specified in DLMS/COSEM protocol
        /// </summary>
        /// <param name="OBISCodeIndex">The OBIS Code for IC Instance to create<see cref="StOBISCode"/></param>
        /// <returns><see cref="Base_Class"/>;DLMS_COSEM Interface Class Instance</returns>
        public Base_Class DLMS_FactoryMethod(StOBISCode OBISCodeIndex)
        {
            try
            {
                StOBISCode OBISCodeIdentifier = OBISCodeIndex;
                return ((IDLMSClassFacotry)this).DLMS_FactoryMethod(OBISCodeIdentifier);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to build object of {0}", OBISCodeIndex), ex);
            }
        }

        #endregion
    }
}
