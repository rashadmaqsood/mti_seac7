using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.Authentication;

namespace SharedCode.Others
{
    public static class XMLParamsProcessor
    {
        public static int Param_DisplayWindowsNormal = 0;
        public static int Param_DisplayWindowsAlternate = 1;
        public static int Param_DisplayWindowstest = 2;

        public static string URL_DemandOverloadT1 = "Param_Limit_Demand_OverLoad_T1.xml";
        public static string URL_DemandOverloadT2 = "Param_Limit_Demand_OverLoad_T2.xml";
        public static string URL_DemandOverloadT3 = "Param_Limit_Demand_OverLoad_T3.xml";
        public static string URL_DemandOverloadT4 = "Param_Limit_Demand_OverLoad_T4.xml";

        #region Param_Customer_Code

        public static Param_Customer_Code load_CustomerReference(string Dir)
        {
            Param_Customer_Code Param_customer_code_object = null;
            try
            {
                Dir += "Credentials\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "CustomerReference.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_customer_code_object = (Param_Customer_Code)XMLParamsProcessor.Load_ParamXML(Serialize_Stream,
                            typeof(Param_Customer_Code));
                    }
                }
                else
                {
                    //throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_customer_code_object;
            }
            catch (Exception Ex)
            {
                //throw new Exception("Error Loading ParamCustomerReference" + Ex.Message, Ex);
            }
            return new Param_Customer_Code();
        }

        public static Param_Customer_Code load_CustomerReference(Stream DeserlizerStream)
        {
            Param_Customer_Code Param_customer_code_object = null;
            try
            {
                Param_customer_code_object = (Param_Customer_Code)Load_ParamXML(DeserlizerStream, typeof(Param_Customer_Code));
                return Param_customer_code_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading ParamCustomerReference" + Ex.Message, Ex);
            }
        }

        public static Param_Customer_Code load_CustomerReferenceShowError(string Dir)
        {
            try
            {
                return load_CustomerReference(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading ParamCustomerReference" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading ParamCustomerReference", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_CustomerReference(string Dir, Param_Customer_Code Param_customer_code_object)
        {
            try
            {
                Dir += "\\Credentials\\";             /// Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                string FILE = "CustomerReference.xml";     /// For saving the items

                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_customer_code_object, typeof(Param_Customer_Code));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving ParamCustomerReference" + Ex.Message, Ex);
            }
        }

        public static void Save_CustomerReference(Stream SerlizerStream, Param_Customer_Code Param_customer_code_object)
        {
            try
            {
                Save_ParamXML(SerlizerStream, Param_customer_code_object, typeof(Param_Customer_Code));
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving ParamCustomerReference" + Ex.Message, Ex);
            }
        }

        public static void Save_CustomerReferenceShowError(string Dir, Param_Customer_Code Param_customer_code_object)
        {
            try
            {
                Save_CustomerReference(Dir, Param_customer_code_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving ParamCustomerReference" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving ParamCustomerReference", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Password

        public static Param_Password load_ParamPasswords(string Dir)
        {
            Param_Password Param_password_object = null;
            try
            {
                Dir += "Credentials\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "Credentials.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        //Param_password_object = (Param_password)XMLParamsProcessor.Load_ParamXML(Serialize_Stream,
                        //    typeof(Param_password));
                        Param_password_object = (Param_Password)XMLParamsProcessor.Load_ParamXML(Serialize_Stream);
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_password_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Param_password" + Ex.Message, Ex);
            }
        }

        public static Param_Password load_ParamPasswords(Stream DeserlizerStream)
        {
            Param_Password Param_password_object = null;
            try
            {
                Param_password_object = (Param_Password)Load_ParamXML(DeserlizerStream, typeof(Param_Password));
                return Param_password_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Param_password" + Ex.Message, Ex);
            }
        }

        public static Param_Password load_ParamPasswordsShowError(string Dir)
        {
            try
            {
                return load_ParamPasswords(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading ParamCustomerReference" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading ParamPasswords", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_ParamPasswords(string Dir, Param_Password Param_password_object)
        {
            try
            {
                Dir += "\\Credentials\\";             /// Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                string FILE = "Credentials.xml";     /// For saving the items

                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    //XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                    //    Param_password_object, typeof(Param_password));
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_password_object);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param_password" + Ex.Message, Ex);
            }
        }

        public static void Save_ParamPasswords(Stream SerlizerStream, Param_Password Param_password_object)
        {
            try
            {
                Save_ParamXML(SerlizerStream, Param_password_object, typeof(Param_Password));
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param_password" + Ex.Message, Ex);
            }
        }

        public static void Save_ParamPasswordsShowError(string Dir, Param_Password Param_password_object)
        {
            try
            {
                Save_ParamPasswords(Dir, Param_password_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving ParamCustomerReference" + Ex.Message, Ex);
                //PCL //PCL Notification notifier = new Notification("Error Saving ParamPasswordsShowError", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Clock_Caliberation

        public static Param_Clock_Caliberation load_Clock(string Dir)
        {
            Param_Clock_Caliberation Param_clock_caliberation_object = null;
            try
            {
                Dir += "Clock\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "Clock.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_clock_caliberation_object = (Param_Clock_Caliberation)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Clock_Caliberation));
                    }
                }
                else
                {
                    throw new Exception("The following folder doesnot exist :  " + Dir);
                }
                return Param_clock_caliberation_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Clock" + Ex.Message, Ex);
            }
        }

        public static Param_Clock_Caliberation load_Clock(Stream DeserlizerStream)
        {
            Param_Clock_Caliberation Param_clock_caliberation_object = null;
            try
            {
                Param_clock_caliberation_object = (Param_Clock_Caliberation)
                    Load_ParamXML(DeserlizerStream, typeof(Param_Clock_Caliberation));
                return Param_clock_caliberation_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Param_Clock_Caliberation" + Ex.Message, Ex);
            }
        }

        public static Param_Clock_Caliberation load_ClockShowError(string Dir)
        {
            try
            {
                return load_Clock(Dir);
            }
            catch (Exception ex)
            {
                //PCL Notification notifier = new Notification("Error loading Clock Param", ex.Message, 1500);
            }
            return null;
        }

        public static void Save_Clock(string Dir, Param_Clock_Caliberation Param_clock_caliberation_object)
        {
            try
            {
                Dir += "\\Clock\\";                      /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                string FILE = "Clock.xml";              /// For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_clock_caliberation_object, typeof(Param_Clock_Caliberation));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving Param Clock", ex);
            }
        }

        public static void Save_Clock(Stream SerlizerStream, Param_Clock_Caliberation Param_clock_caliberation_object)
        {
            try
            {
                Save_ParamXML(SerlizerStream, Param_clock_caliberation_object, typeof(Param_Clock_Caliberation));
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param Clock" + Ex.Message, Ex);
            }
        }

        public static void Save_ClockShowError(string Dir, Param_Clock_Caliberation Param_clock_caliberation_object)
        {
            try
            {
                Save_Clock(Dir, Param_clock_caliberation_object);
            }
            catch (Exception ex)
            {
                //PCL Notification notifier = new Notification("Error loading Clock Param", ex.Message, 1500);
            }
        }

        #endregion

        #region Param_CTPT_Ratio

        public static Param_CTPT_Ratio load_CTPT_Ratios(string Dir)
        {
            Param_CTPT_Ratio Param_CTPT_ratio_object = null;
            try
            {
                Dir += "CTPTRatio\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "CTPTRatio.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_CTPT_ratio_object = (Param_CTPT_Ratio)XMLParamsProcessor.Load_ParamXML(Serialize_Stream, typeof(Param_CTPT_Ratio));
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_CTPT_ratio_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading CTPT Ratio" + Ex.Message, Ex);
            }
            return null;
        }

        public static Param_CTPT_Ratio load_CTPT_RatiosShowError(string Dir)
        {
            Param_CTPT_Ratio Param_CTPT_ratio_object = null;
            try
            {
                Param_CTPT_ratio_object = load_CTPT_Ratios(Dir);
                return Param_CTPT_ratio_object;
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading CTPT Ratio" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading CTPT Ratio", Ex.Message, 1500);
            }
            return null;
        }

        public static void save_CTPT(string Dir, Param_CTPT_Ratio Param_CTPT_ratio_object)
        {
            try
            {
                Dir += "\\CTPTRatio\\";
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                /// Path for saving test files.
                string FILE = "CTPTRatio.xml";     // For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_CTPT_ratio_object, typeof(Param_CTPT_Ratio));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving CTPT Ratio", ex);
            }
        }

        public static void save_CTPTShowError(string Dir, Param_CTPT_Ratio Param_CTPT_ratio_object)
        {
            try
            {
                save_CTPT(Dir, Param_CTPT_ratio_object);
            }
            catch (Exception ex)
            {
                //PCL Notification notifier = new Notification("Error Saving CTPT Ratio", ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Decimal_Point

        public static Param_Decimal_Point load_DecimalPoint(string Dir)
        {
            Param_Decimal_Point Param_decimal_point_object = null;
            try
            {
                Dir += "DecimalPoint\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "DecimalPoint.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_decimal_point_object = (Param_Decimal_Point)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Decimal_Point));
                    }
                }
                else
                {
                    throw new Exception("The following folder  does not exist :  " + Dir);
                    ///MessageBox.Show("The following folder  does not exist :  " + Dir);
                }
                return Param_decimal_point_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Decimal Point", Ex);
                ///MessageBox.Show("Error Loading Decimal Point " + Ex.Message);
            }
        }

        public static Param_Decimal_Point load_DecimalPointShowError(string Dir)
        {
            try
            {
                return load_DecimalPoint(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Decimal Point", Ex);
                ///MessageBox.Show("Error Loading Decimal Point " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Decimal Point", Ex.Message, 1500);
            }
            return null;
        }

        public static void save_DecimalPoint(string Dir, Param_Decimal_Point Param_decimal_point_object)
        {
            try
            {
                Dir += "\\DecimalPoint\\";
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                // Path for saving test files.
                string FILE = "DecimalPoint.xml";     /// For Saving the items
                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_decimal_point_object, typeof(Param_Decimal_Point));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving Param DecimalPoint Profile ", ex);
            }
        }

        public static void save_DecimalPointShowError(string Dir, Param_Decimal_Point Param_decimal_point_object)
        {
            try
            {
                save_DecimalPoint(Dir, Param_decimal_point_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Param DecimalPoint Profile ", ex);
                //PCL Notification notifier = new Notification("Error Saving Param DecimalPoint Profile", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Status_Word_Map

        public static Param_StatusWordMap load_StatusWordMap(string Dir)
        {
            Param_StatusWordMap Param_status_word_object = null;
            try
            {
                Dir += "StatusWordMap\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "StatusWordMap.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_status_word_object = (Param_StatusWordMap)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_StatusWordMap));
                    }
                }
                else
                {
                    throw new Exception("The following folder  does not exist :  " + Dir);
                    ///MessageBox.Show("The following folder  does not exist :  " + Dir);
                }
                return Param_status_word_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Status Word Map", Ex);
                ///MessageBox.Show("Error Loading Decimal Point " + Ex.Message);
            }
        }

        public static Param_StatusWordMap load_StatusWordMapShowError(string Dir)
        {
            try
            {
                return load_StatusWordMap(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Decimal Point", Ex);
                ///MessageBox.Show("Error Loading Decimal Point " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Status Word Map", Ex.Message, 1500);
            }
            return null;
        }

        public static void save_StatusWordMap(string Dir, Param_StatusWordMap Param_status_word_object)
        {
            try
            {
                Dir += "\\StatusWordMap\\";
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                // Path for saving test files.
                string FILE = "StatusWordMap.xml";     /// For Saving the items
                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_status_word_object, typeof(Param_StatusWordMap));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving Param StatusWordMap Profile ", ex);
            }
        }

        public static void save_StatusWordMapShowError(string Dir, Param_StatusWordMap Param_status_word_object)
        {
            try
            {
                save_StatusWordMap(Dir, Param_status_word_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Param DecimalPoint Profile ", ex);
                //PCL Notification notifier = new Notification("Error Saving Param StatusWordMap Profile", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Energy_Parameter

        public static Param_Energy_Parameter load_EnergyParam(string Dir)
        {
            Param_Energy_Parameter Param_energy_parameters_object = null;
            try
            {
                Dir += "EnergyParam\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "EnergyParam" + ".xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_energy_parameters_object = (Param_Energy_Parameter)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Energy_Parameter));
                    }

                }
                else
                {
                    throw new Exception("The following folder doesnot exist :  " + Dir);
                }
                return Param_energy_parameters_object;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Energy Params" + Ex.Message);
                throw new Exception("Error Loading Energy Params", Ex);
            }
        }

        public static Param_Energy_Parameter load_EnergyParamShowError(string Dir)
        {
            try
            {
                return load_EnergyParam(Dir);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Energy Params" + Ex.Message);
                ///throw new Exception("Error Loading Energy Params", Ex);
                //PCL Notification notifier = new Notification("Error Loading Energy Params", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_EnergyParams(string Dir, Param_Energy_Parameter Param_energy_parameters_object)
        {
            try
            {
                Dir += "\\EnergyParam\\";                  /// Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                string FILE = "EnergyParam.xml";          /// For saving the items
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_energy_parameters_object, typeof(Param_Energy_Parameter));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving Energy Params", ex);
            }
        }

        public static void Save_EnergyParamsShowError(string Dir, Param_Energy_Parameter Param_energy_parameters_object)
        {
            try
            {
                Save_EnergyParams(Dir, Param_energy_parameters_object);
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Saving Energy Params", ex);
                //PCL Notification notifier = new Notification("Error Saving Energy Params", ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Contactor

        public static Param_Contactor load_Contactor(string Dir)
        {
            Param_Contactor Param_Contactor_object = null;
            try
            {
                Dir += "Contactor\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "Contactor.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_Contactor_object = (Param_ContactorExt)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_ContactorExt));
                    }
                }
                else
                {
                    throw new Exception("The following folder does not exist :  " + Dir);
                }
                return Param_Contactor_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Contactor", Ex);
            }
        }

        public static Param_Contactor load_ContactorShowError(string Dir)
        {
            try
            {
                return load_Contactor(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Contactor", Ex);
                //PCL Notification notifier = new Notification("Error Loading Contactor", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_Contactor(string Dir, Param_Contactor Param_Contactor_object)
        {
            try
            {
                Dir += "\\Contactor\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                string FILE = "Contactor.xml";     // For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_Contactor_object, typeof(Param_ContactorExt));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving Contactor", ex);
            }
        }

        public static void Save_ContactorShowError(string Dir, Param_Contactor Param_Contactor_object)
        {
            try
            {
                Save_Contactor(Dir, Param_Contactor_object);
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Saving Contactor", ex);
                //PCL Notification notifier = new Notification("Error Saving Contactor", ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Communication_Profile

        public static Param_Communication_Profile load_CommProfile(string Dir)
        {
            Param_Communication_Profile Param_Communication_Profile_object = null;
            try
            {
                Dir += "CommProfile\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "CommProfile.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_Communication_Profile_object = (Param_Communication_Profile)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Communication_Profile));
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_Communication_Profile_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Communication Profile ", Ex);
            }
        }

        public static Param_Communication_Profile load_CommProfileShowError(string Dir)
        {
            try
            {
                return load_CommProfile(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Communication Profile ", Ex);
                //PCL Notification notifier = new Notification("Error Loading Communication Profile", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_CommProfile(string Dir, Param_Communication_Profile Param_Communication_Profile_object)
        {
            try
            {
                Dir += "\\CommProfile\\";            /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                string FILE = "CommProfile.xml";    /// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_Communication_Profile_object, typeof(Param_Communication_Profile));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving Communication Profile ", ex);
            }
        }

        public static void Save_CommProfileShowError(string Dir, Param_Communication_Profile Param_Communication_Profile_object)
        {
            try
            {
                Save_CommProfile(Dir, Param_Communication_Profile_object);
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Saving Communication Profile ", ex);
                //PCL Notification notifier = new Notification("Error Saving Communication Profile", ex.Message, 1500);
            }
        }

        #endregion

        #region ParamDisplayWindows

        public static DisplayWindows[] load_DisplayWindows(string Dir)
        {
            DisplayWindows[] Display_Windows = new DisplayWindows[03];
            try
            {
                DisplayWindowsHelper dispDisplayWindowsHelper = null;
                dispDisplayWindowsHelper = new DisplayWindowsHelper();

                if (Directory.Exists(Dir))
                {
                    String FileUrl_1 = String.Format(@"{0}DisplayWindows\DisplayWindowsNormal.xml", Dir);
                    String FileUrl_2 = String.Format(@"{0}DisplayWindows\DisplayWindowsAlternate.xml", Dir);
                    String FileUrl_3 = String.Format(@"{0}DisplayWindows\DisplayWindowsTest.xml", Dir);
                    /// Path for saving test files.
                    String FileUrl_AllWin = Commons_DB.GetApplicationConfigsDirectory() + @"\AllDisplayWindows.xml";

                    ///Path for saving test files
                    //Display_Windows[Param_DisplayWindowsNormal] = dispDisplayWindowsHelper.LoadDisplayWindows(FileUrl_1);
                    //Display_Windows[Param_DisplayWindowsAlternate] = dispDisplayWindowsHelper.LoadDisplayWindows(FileUrl_2);
                    //Display_Windows[Param_DisplayWindowstest] = dispDisplayWindowsHelper.LoadDisplayWindows(FileUrl_3);
                    ///Param_Controller.DisplayWindowsHelper.LoadSelectableDisplayWindows(FileUrl_AllWin);

                    using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                    {
                        #region ///Load Param_DisplayWindowsNormal

                        Serialize_Stream.SetLength(0);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, FileUrl_1);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Display_Windows[Param_DisplayWindowsNormal] = (DisplayWindows)XMLParamsProcessor.Load_Param(Serialize_Stream);

                        #endregion
                        #region ///Load Param_DisplayWindowsAlternate

                        Serialize_Stream.SetLength(0);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, FileUrl_2);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Display_Windows[Param_DisplayWindowsAlternate] = (DisplayWindows)XMLParamsProcessor.Load_Param(Serialize_Stream);

                        #endregion
                        #region ///Save Param_DisplayWindowstest

                        Serialize_Stream.SetLength(0);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, FileUrl_3);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Display_Windows[Param_DisplayWindowstest] = (DisplayWindows)XMLParamsProcessor.Load_Param(Serialize_Stream);

                        #endregion
                    }
                }
                else
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                return Display_Windows;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading Display Windows", ex);
            }
        }

        public static DisplayWindows[] load_DisplayWindowsShowError(string Dir)
        {
            try
            {
                return load_DisplayWindows(Dir);
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Loading Display Windows", ex);
                //PCL Notification notifier = new Notification("Error Loading Display Windows", ex.Message, 1500);
            }
            return null;
        }

        public static void Save_DisplayWindows(string Dir, DisplayWindows[] Display_Windows)
        {
            DisplayWindowsHelper dispDisplayWindowsHelper = null;
            dispDisplayWindowsHelper = new DisplayWindowsHelper();
            try
            {
                Dir += @"\DisplayWindows";
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                String FileUrl_1 = String.Format(@"{0}\DisplayWindowsNormal.xml", Dir);
                String FileUrl_2 = String.Format(@"{0}\DisplayWindowsAlternate.xml", Dir);
                String FileUrl_3 = String.Format(@"{0}\DisplayWindowsTest.xml", Dir);
                String FileUrl_AllWin = Commons_DB.
                    GetApplicationConfigsDirectory() + @"\AllDisplayWindows.xml";
                /// Path for saving test files
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    #region ///Save Param_DisplayWindowsNormal
                    Serialize_Stream.SetLength(0);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Display_Windows[Param_DisplayWindowsNormal]);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, FileUrl_1);

                    #endregion
                    #region ///Save Param_DisplayWindowsAlternate
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Serialize_Stream.SetLength(0);
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Display_Windows[Param_DisplayWindowsAlternate]);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, FileUrl_2);
                    #endregion
                    #region ///Save Param_DisplayWindowstest

                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Serialize_Stream.SetLength(0);
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Display_Windows[Param_DisplayWindowstest]);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, FileUrl_3);

                    #endregion
                }
                ///dispDisplayWindowsHelper.SaveDisplayWindows(Display_Windows[Param_DisplayWindowsNormal], FileUrl_1);
                ///dispDisplayWindowsHelper.SaveDisplayWindows(Display_Windows[Param_DisplayWindowsAlternate], FileUrl_2);
                ///dispDisplayWindowsHelper.SaveDisplayWindows(Display_Windows[Param_DisplayWindowstest], FileUrl_3);

                ///Param_Controller.DisplayWindowsHelper.SaveSelectableWindows
                ///(Param_Controller.DisplayWindowsHelper.GetSelectableWindows(), FileUrl_AllWin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while saving display windows", ex);
            }
        }

        public static void Save_DisplayWindowsShowError(string Dir, DisplayWindows[] Display_Windows)
        {
            try
            {
                Save_DisplayWindows(Dir, Display_Windows);
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Loading Display Windows", ex);
                //PCL Notification notifier = new Notification("Error Saving Display Windows", ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Monitoring_Time

        public static Param_Monitoring_Time load_MonitoringTime(string Dir)
        {
            Param_Monitoring_Time Param_Monitoring_time_object = null;
            try
            {
                Dir += "MonitoringTime\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "MonitoringTime.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_Monitoring_time_object = (Param_Monitoring_Time)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream);
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_Monitoring_time_object;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading MonitoringTime" + Ex.Message);
                ///Notification notifier = new Notification("Error Loading MonitoringTime", Ex.Message, 1500);
                throw new Exception("Error Loading MonitoringTime" + Ex.Message);
            }
        }

        public static Param_Monitoring_Time load_MonitoringTimeShowError(string Dir)
        {
            try
            {
                return load_MonitoringTime(Dir);
            }
            catch (Exception Ex)
            {
                //PCL Notification notifier = new Notification("Error Loading MonitoringTime", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_MonitoringTime(string Dir, Param_Monitoring_Time Param_Monitoring_time_object = null)
        {
            try
            {
                Dir += "\\MonitoringTime\\";             /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                string FILE = "MonitoringTime.xml";     /// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_Monitoring_time_object);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving MonitoringTime" + Ex.Message);
            }
        }

        public static void Save_MonitoringTimeShowError(string Dir, Param_Monitoring_Time Param_Monitoring_time_object = null)
        {
            try
            {
                Save_MonitoringTime(Dir, Param_Monitoring_time_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving MonitoringTime" + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving MonitoringTime", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Limits

        public static Param_Limits load_Limits(string Dir, String CurrentMeterName)
        {
            Param_Limits Param_Limits_object = null;
            try
            {
                Dir += "Limits" + "_" + CurrentMeterName + "\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "Limits.xml";     /// For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_Limits_object = (Param_Limits)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Limits));
                    }
                    return Param_Limits_object;
                }
                else
                {
                    throw new Exception("The following folder does not exist: " + Dir);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Limits" + Ex.Message, Ex);
            }
        }

        public static Param_Limits load_LimitsShowError(string Dir, String CurrentMeterName)
        {
            try
            {
                return load_Limits(Dir, CurrentMeterName);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Limits" + Ex.Message, Ex);
                //PCL Notification noti = new Notification("Error Loading Param_Limits", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_Limits(string Dir, String CurrentMeterName, Param_Limits Param_Limits_object)
        {
            try
            {
                Dir += "\\Limits" + "_" + CurrentMeterName + "\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);
                string FILE = "Limits.xml";     /// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_Limits_object, typeof(Param_Limits));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error saving Limits" + Ex.Message, Ex);
            }
        }

        public static void Save_LimitsShowError(string Dir, String CurrentMeterName, Param_Limits Param_Limits_object)
        {
            try
            {
                Save_Limits(Dir, CurrentMeterName, Param_Limits_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error saving Limits" + Ex.Message, Ex);
                //PCL Notification noti = new Notification("Error saving Limits ParamLimits", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Limit_Demand_OverLoad

        public static Param_Limit_Demand_OverLoad load_Limits_Param_Limit_Demand_OverLoad(string Dir, String CurrentMeterName, string fileURL)
        {
            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad = null;
            try
            {
                Dir += "Limits" + "_" + CurrentMeterName + "\\";
                if (Directory.Exists(Dir))
                {
                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + fileURL);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_Limit_Demand_OverLoad = (Param_Limit_Demand_OverLoad)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Limit_Demand_OverLoad));
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_Limit_Demand_OverLoad;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Param_Limit_Demand_OverLoad" + Ex.Message, Ex);
            }
        }

        public static Param_Limit_Demand_OverLoad[] loadAll_Limits_Param_Limit_Demand_OverLoad(string Dir, String CurrentMeterName)
        {
            Param_Limit_Demand_OverLoad[] Param_Limit_Demand_OverLoad = null;
            try
            {
                Param_Limit_Demand_OverLoad = new Param_Limit_Demand_OverLoad[04];
                Param_Limit_Demand_OverLoad[0] = load_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT1);
                Param_Limit_Demand_OverLoad[1] = load_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT2);
                Param_Limit_Demand_OverLoad[2] = load_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT3);
                Param_Limit_Demand_OverLoad[3] = load_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT4);
                return Param_Limit_Demand_OverLoad;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Param_Limit_Demand_OverLoad" + Ex.Message, Ex);
            }
        }

        public static Param_Limit_Demand_OverLoad[] loadAll_Limits_Param_Limit_Demand_OverLoadShowError(string Dir, String CurrentMeterName)
        {
            try
            {
                return loadAll_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName);
            }
            catch (Exception Ex)
            {
                //throw new Exception("Error Loading Param_Limit_Demand_OverLoad" + Ex.Message, Ex);
                //PCL Notification noti = new Notification("Error Loading Param_Limit_Demand_OverLoad", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_Limits_Param_Limit_Demand_OverLoad(string Dir, String CurrentMeterName,
            string fileURL, Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad)
        {
            try
            {
                Dir += "\\Limits" + "_" + CurrentMeterName + "\\";         /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_Limit_Demand_OverLoad, typeof(Param_Limit_Demand_OverLoad));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + fileURL);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param_Limit_Demand_OverLoad" + Ex.Message, Ex);
            }
        }

        public static void SaveAll_Limits_Param_Limit_Demand_OverLoad(string Dir, String CurrentMeterName,
            Param_Limit_Demand_OverLoad[] Param_Limit_Demand_OverLoad)
        {
            try
            {
                Save_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT1, Param_Limit_Demand_OverLoad[0]);
                Save_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT2, Param_Limit_Demand_OverLoad[1]);
                Save_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT3, Param_Limit_Demand_OverLoad[2]);
                Save_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, URL_DemandOverloadT4, Param_Limit_Demand_OverLoad[3]);
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param_Limit_Demand_OverLoad" + Ex.Message, Ex);
            }
        }

        public static void SaveAll_Limits_Param_Limit_Demand_OverLoadShowError(string Dir, String CurrentMeterName,
            Param_Limit_Demand_OverLoad[] Param_Limit_Demand_OverLoad)
        {
            try
            {
                SaveAll_Limits_Param_Limit_Demand_OverLoad(Dir, CurrentMeterName, Param_Limit_Demand_OverLoad);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Limits" + Ex.Message, Ex);
                //PCL Notification noti = new Notification("Error Save Param_Limit_Demand_OverLoad", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_LoadProfileChannelInfo

        public static LoadProfileChannelInfo[] Load_LoadProfile(string Dir, LoadProfileScheme LP_Scheme)
        {
            try
            {
                LoadProfileChannelInfo[] channels = null;
                Dir += "LoadProfile\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "LoadProfileChannels_" + (int)LP_Scheme + ".xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        channels = (LoadProfileChannelInfo[])XMLParamsProcessor.
                            Load_Params(Serialize_Stream);
                    }
                }
                else
                {
                    throw new Exception("The following folder does not exist:" + Dir);
                }
                return channels;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading ParamLoadProfile" + Ex.Message, Ex);
            }
        }

        public static LoadProfileChannelInfo[] Load_LoadProfileShowError(string Dir, LoadProfileScheme LP_Scheme)
        {
            try
            {
                return Load_LoadProfile(Dir,LP_Scheme);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading ParamLoadProfile" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading ParamLoadProfile", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_LoadProfile(String Dir, LoadProfileChannelInfo[] channels, LoadProfileScheme LP_Scheme)
        {
            try
            {
                Dir += "\\LoadProfile\\";
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                // Path for saving test files.
                string FILE = "LoadProfileChannels_" + (int)LP_Scheme + ".xml";      /// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream,
                        channels);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while saving_ParamLoadProfile" + ex.Message, ex);
            }
        }

        public static void Save_LoadProfileShowError(String Dir, LoadProfileChannelInfo[] channels, LoadProfileScheme LP_Scheme)
        {
            try
            {
                Save_LoadProfile(Dir, channels, LP_Scheme);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading ParamLoadProfile" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving ParamLoadProfile", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_ModemLimitsAndTime

        public static Param_ModemLimitsAndTime load_ModemLimitAndTime(string Dir)
        {
            Param_ModemLimitsAndTime Param_ModemLimitsAndTime_Object = null;
            try
            {
                Dir += "ModemLimitAndTime\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "ModemLimitAndTime.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_ModemLimitsAndTime_Object = (Param_ModemLimitsAndTime)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_ModemLimitsAndTime));
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_ModemLimitsAndTime_Object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Modem Limts and Time" + Ex.Message, Ex);
            }
        }

        public static Param_ModemLimitsAndTime load_ModemLimitAndTimeShowError(string Dir)
        {
            try
            {
                return load_ModemLimitAndTime(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Modem Limts and Time" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading Modem Limts and Time", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_ModemLimitsAndTime(string Dir, Param_ModemLimitsAndTime Param_ModemLimitsAndTime_Object)
        {
            try
            {
                Dir += "\\ModemLimitAndTime\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                string FILE = "ModemLimitAndTime.xml";     // For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_ModemLimitsAndTime_Object, typeof(Param_ModemLimitsAndTime));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving ParamModemLimtsandTime" + Ex.Message, Ex);
            }
        }

        public static void Save_ModemLimitsAndTimeShowError(string Dir, Param_ModemLimitsAndTime Param_ModemLimitsAndTime_Object)
        {
            try
            {
                Save_ModemLimitsAndTime(Dir, Param_ModemLimitsAndTime_Object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading Modem Limts and Time" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving ParamModemLimtsandTime", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Modem_Initialize

        public static Param_Modem_Initialize load_ModemInitialize(string Dir)
        {
            Param_Modem_Initialize Param_Modem_Initialize_Object = null;
            try
            {
                Dir += "ModemInitialize\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "ModemInitialize.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_Modem_Initialize_Object = (Param_Modem_Initialize)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Modem_Initialize));
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_Modem_Initialize_Object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading ModemInitialize" + Ex.Message, Ex);
            }
        }

        public static Param_Modem_Initialize load_ModemInitializeShowError(string Dir)
        {
            try
            {
                return load_ModemInitialize(Dir);
            }
            catch (Exception Ex)
            {
                /// throw new Exception("Error Loading ModemInitialize" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading ModemInitialize", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_Modem_Initialize(string Dir, Param_Modem_Initialize Param_Modem_Initialize_Object)
        {
            try
            {
                Dir += "\\ModemInitialize\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                string FILE = "ModemInitialize.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_Modem_Initialize_Object, typeof(Param_Modem_Initialize));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving ModemInitialize" + ex.Message, ex);
            }
        }

        public static void Save_Modem_InitializeShowError(string Dir, Param_Modem_Initialize Param_Modem_Initialize_Object)
        {
            try
            {
                Save_Modem_Initialize(Dir, Param_Modem_Initialize_Object);
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Saving ModemInitialize" + ex.Message, ex);
                //PCL Notification notifier = new Notification("Error Saving ModemInitialize", ex.Message, 1500);
            }
        }

        #endregion

        #region Param_ModemBasics_NEW

        public static Param_ModemBasics_NEW load_ModemInitializeNew(string Dir)
        {
            Param_ModemBasics_NEW Param_ModemBasics_NEW_object = null;
            try
            {
                Dir += "ModemBasicsNEW\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "ModemBasicsNEW.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_ModemBasics_NEW_object = (Param_ModemBasics_NEW)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_ModemBasics_NEW));
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_ModemBasics_NEW_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading ModemInitializeNew" + Ex.Message, Ex);
            }
        }

        public static Param_ModemBasics_NEW load_ModemInitializeNewShowError(string Dir)
        {
            try
            {
                return load_ModemInitializeNew(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading ModemInitialize" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading ModemInitialize", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_Modem_Initialize(string Dir, Param_ModemBasics_NEW Param_ModemBasics_NEW_object)
        {
            try
            {
                Dir += "\\ModemBasicsNEW\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                string FILE = "ModemBasicsNEW.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_ModemBasics_NEW_object, typeof(Param_ModemBasics_NEW));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving ModemInitializeNew" + ex.Message, ex);
            }
        }

        public static void Save_Modem_InitializeShowError(string Dir, Param_ModemBasics_NEW Param_ModemBasics_NEW_object)
        {
            try
            {
                Save_Modem_Initialize(Dir, Param_ModemBasics_NEW_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading ModemInitialize" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving ModemInitialize", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_IP_Profiles

        public static Param_IP_Profiles[] load_IP_profile(string Dir)
        {
            Param_IP_Profiles[] Param_IP_Profiles_object = new Param_IP_Profiles[Param_IP_ProfilesHelper.Max_IP_Profile];
            try
            {
                Dir += "IPProfile\\";
                if (Directory.Exists(Dir))
                {
                    Type type_Param_IP_Profiles = typeof(Param_IP_Profiles);

                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        for (int count = 0; count < Param_IP_Profiles_object.Length; count++)
                        {
                            string FILE = String.Format("ProfileID_{0}.xml", (count + 1));     ///For Saving the items
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Serialize_Stream.SetLength(0);
                            XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);

                            Param_IP_Profiles_object[count] = (Param_IP_Profiles)XMLParamsProcessor.
                                Load_ParamXML(Serialize_Stream, type_Param_IP_Profiles);
                        }
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_IP_Profiles_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading IP Profile", Ex);
                ///MessageBox.Show("Error Loading IP Profile" + Ex.Message);
            }
        }
        public static Param_Standard_IP_Profile[] Load_Standard_IP_profile(string Dir)
        {
            Param_Standard_IP_Profile[] Param_IP_Profiles_object = new Param_Standard_IP_Profile[Param_IP_ProfilesHelper.Max_IP_Profile];
            try
            {
                Dir += "StandardIPProfile\\";
                if (Directory.Exists(Dir))
                {
                    Type type_Param_IP_Profiles = typeof(Param_Standard_IP_Profile);

                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        for (int count = 0; count < Param_IP_Profiles_object.Length; count++)
                        {
                            string FILE = String.Format("StandardProfileID_{0}.xml", (count + 1));     ///For Saving the items
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Serialize_Stream.SetLength(0);
                            XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);

                            Param_IP_Profiles_object[count] = (Param_Standard_IP_Profile)XMLParamsProcessor.
                                Load_ParamXML(Serialize_Stream, type_Param_IP_Profiles);
                        }
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_IP_Profiles_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Standard IP Profile", Ex);
                ///MessageBox.Show("Error Loading IP Profile" + Ex.Message);
            }
        }

        public static Param_IP_Profiles[] load_IP_profileShowError(string Dir)
        {
            try
            {
                return load_IP_profile(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading IP Profile", Ex);
                ///MessageBox.Show("Error Loading IP Profile" + Ex.Message);
                //PCL //PCL Notification notifier = new Notification("Error Loading IP Profile", Ex.Message, 1500);
            }
            return null;
        }
        public static Param_Standard_IP_Profile[] Load_Standard_IP_profileShowError(string Dir)
        {
            try
            {
                return Load_Standard_IP_profile(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading IP Profile", Ex);
                ///MessageBox.Show("Error Loading IP Profile" + Ex.Message);
                //PCL //PCL Notification notifier = new Notification("Error Loading IP Profile", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_IP_profile(string Dir, Param_IP_Profiles[] Param_IP_Profiles_object)
        {
            try
            {
                Dir += "\\IPProfile\\";           /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                Type type_Param_IP_Profiles = typeof(Param_IP_Profiles);
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    for (int count = 0; count < Param_IP_Profiles_object.Length; count++)
                    {
                        string FILE = String.Format("ProfileID_{0}.xml", (count + 1));     ///For Saving the item
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Serialize_Stream.SetLength(0);
                        XMLParamsProcessor.Save_ParamXML(Serialize_Stream, Param_IP_Profiles_object[count], type_Param_IP_Profiles);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                    }
                }
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("IP Profile save unsussessful");
                throw new Exception("IP Profile save unsussessful", ex);
            }
        }
        public static void Save_Standard_IP_profile(string Dir, Param_Standard_IP_Profile[] Param_IP_Profiles_object)
        {
            try
            {
                Dir += "\\StandardIPProfile\\";           /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                Type type_Param_IP_Profiles = typeof(Param_Standard_IP_Profile);
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    for (int count = 0; count < Param_IP_Profiles_object.Length; count++)
                    {
                        string FILE = String.Format("StandardProfileID_{0}.xml", (count + 1));     ///For Saving the item
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Serialize_Stream.SetLength(0);
                        XMLParamsProcessor.Save_ParamXML(Serialize_Stream, Param_IP_Profiles_object[count], type_Param_IP_Profiles);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                    }
                }
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("IP Profile save unsussessful");
                throw new Exception("Standard IP Profile save unsussessful", ex);
            }
        }

        public static void Save_IP_profileShowError(string Dir, Param_IP_Profiles[] Param_IP_Profiles_object)
        {
            try
            {
                Save_IP_profile(Dir, Param_IP_Profiles_object);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("IP Profile save unsussessful");
                ///throw new Exception("IP Profile save unsussessful", ex);
                //PCL Notification notifier = new Notification("IP Profile save unsussessful", Ex.Message, 1500);
            }
        }
        public static void Save_Standard_IP_profileShowError(string Dir, Param_Standard_IP_Profile[] Param_IP_Profiles_object)
        {
            try
            {
                Save_Standard_IP_profile(Dir, Param_IP_Profiles_object);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("IP Profile save unsussessful");
                ///throw new Exception("IP Profile save unsussessful", ex);
                //PCL Notification notifier = new Notification("IP Profile save unsussessful", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_IPV4

        public static Param_IPV4 load_IPV4(string Dir)
        {
            Param_IPV4 Param_IPV4_object = null;
            try
            {
                Dir += "IPV4\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "IPV4.xml";     /// For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(256))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_IPV4_object = (Param_IPV4)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_IPV4));
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_IPV4_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading IPv4", Ex);
            }
        }

        public static Param_IPV4 load_IPV4ShowError(string Dir)
        {
            try
            {
                return load_IPV4(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading IPv4", Ex);
                //PCL Notification noti = new Notification("Error Loading IPv4", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_IPV4(string Dir, Param_IPV4 Param_IPV4_object)
        {
            try
            {
                Dir += "\\IPV4\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                string FILE = "IPV4.xml";     // For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_IPV4_object, typeof(Param_IPV4));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while saving_IPV4", ex);
            }
        }

        public static void Save_IPV4ShowError(string Dir, Param_IPV4 Param_IPV4_object)
        {
            try
            {
                Save_IPV4(Dir, Param_IPV4_object);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("IP Profile save unsussessful");
                ///throw new Exception("IP Profile save unsussessful", ex);
                //PCL Notification notifier = new Notification("Error occured while saving_IPV4", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Keep_Alive_IP

        public static Param_Keep_Alive_IP load_KeepAlive(string Dir)
        {
            Param_Keep_Alive_IP Param_Keep_Alive_IP_object = null;
            try
            {
                Dir += "KeepAlive\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "KeepAlive.xml";     // For saving the items.
                    using (MemoryStream Serialize_Stream = new MemoryStream(50))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_Keep_Alive_IP_object = (Param_Keep_Alive_IP)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_Keep_Alive_IP));
                    }
                }
                return Param_Keep_Alive_IP_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading Keep Alive" + Ex.Message, Ex);
            }
        }

        public static Param_Keep_Alive_IP load_KeepAliveShowError(string Dir)
        {
            try
            {
                return load_KeepAlive(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading IPv4", Ex);
                //PCL Notification noti = new Notification("Error Loading ParamKeepAliveIP", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_KeepAlive(string Dir, Param_Keep_Alive_IP Param_Keep_Alive_IP_object)
        {
            try
            {
                Dir += "\\keepAlive\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                string FILE = "keepAlive.xml";              // For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_Keep_Alive_IP_object, typeof(Param_Keep_Alive_IP));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while save ParamKeepAliveIP" + ex.Message, ex);
            }
        }

        public static void Save_KeepAliveShowError(string Dir, Param_Keep_Alive_IP Param_Keep_Alive_IP_object)
        {
            try
            {
                Save_KeepAlive(Dir, Param_Keep_Alive_IP_object);
            }
            catch (Exception Ex)
            {
                //PCL Notification notifier = new Notification("Error occured while Save ParamKeepAlive", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_Number_Profile

        public static Param_Number_Profile[] load_NumberProfile(string Dir)
        {
            Param_Number_Profile[] Param_Number_Profile_object = new Param_Number_Profile[Param_Number_ProfileHelper.Max_Number_Profile + 1];
            try
            {
                Dir += "NumberProfile\\";
                if (Directory.Exists(Dir))
                {
                    Type Type_Param_Number_Profile = typeof(Param_Number_Profile);
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        for (int count = 0; count < Param_Number_Profile_object.Length; count++)
                        {
                            string FILE = String.Format("NumberProfile_{0}.xml", (count + 1));
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Serialize_Stream.SetLength(0);
                            XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Param_Number_Profile_object[count] = (Param_Number_Profile)XMLParamsProcessor.
                                Load_ParamXML(Serialize_Stream, Type_Param_Number_Profile);
                        }
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_Number_Profile_object;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Number Profile " + Ex.Message);
                throw new Exception("Error Loading Number Profile " + Ex.Message, Ex);
            }
        }
        public static Param_Standard_Number_Profile[] Load_Standard_NumberProfile(string Dir)
        {
            Param_Standard_Number_Profile[] Param_Number_Profile_object = new Param_Standard_Number_Profile[Param_Number_ProfileHelper.Max_Number_Profile]; // + 1];
            try
            {
                Dir += "StandardNumberProfile\\";
                if (Directory.Exists(Dir))
                {
                    Type Type_Param_Number_Profile = typeof(Param_Standard_Number_Profile);
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        for (int count = 0; count < Param_Number_Profile_object.Length; count++)
                        {
                            string FILE = String.Format("StandardNumberProfile_{0}.xml", (count + 1));
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Serialize_Stream.SetLength(0);
                            XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Param_Number_Profile_object[count] = (Param_Standard_Number_Profile)XMLParamsProcessor.
                                Load_ParamXML(Serialize_Stream, Type_Param_Number_Profile);
                        }
                    }
                }
                else
                {
                    throw new Exception("The following folder  doesnot exist :  " + Dir);
                }
                return Param_Number_Profile_object;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Number Profile " + Ex.Message);
                throw new Exception("Error Loading Standard Number Profile " + Ex.Message, Ex);
            }
        }

        public static Param_Number_Profile[] load_NumberProfileShowError(string Dir)
        {
            try
            {
                return load_NumberProfile(Dir);
            }
            catch (Exception Ex)
            {

                ///throw new Exception("Error Loading Number Profile " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading Number Profile", Ex.Message, 1500);
            }
            return null;
        }
        public static Param_Standard_Number_Profile[] Load_Standard_NumberProfileShowError(string Dir)
        {
            try
            {
                return Load_Standard_NumberProfile(Dir);
            }
            catch (Exception Ex)
            {

                ///throw new Exception("Error Loading Number Profile " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading Number Profile", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_NumberProfile(string Dir, Param_Number_Profile[] Param_Number_Profile_object)
        {
            try
            {
                Dir += "\\NumberProfile\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                Type Type_Param_Number_Profile = typeof(Param_Number_Profile);
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    for (int count = 0; count < Param_Number_Profile_object.Length; count++)
                    {
                        string FILE = String.Format("NumberProfile_{0}.xml", (count + 1));     ///For Saving the item
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Serialize_Stream.SetLength(0);
                        XMLParamsProcessor.Save_ParamXML(Serialize_Stream, Param_Number_Profile_object[count], Type_Param_Number_Profile);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                    }
                }
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Number Profile " + Ex.Message);
                throw new Exception("Error Saving Number Profile " + Ex.Message, Ex);
            }
        }

        public static void Save_Standard_NumberProfile(string Dir, Param_Standard_Number_Profile[] Param_Number_Profile_object)
        {
            try
            {
                Dir += "\\StandardNumberProfile\\";                      // Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                Type Type_Param_Number_Profile = typeof(Param_Standard_Number_Profile);
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    for (int count = 0; count < Param_Number_Profile_object.Length; count++)
                    {
                        string FILE = String.Format("StandardNumberProfile_{0}.xml", (count + 1));     ///For Saving the item
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Serialize_Stream.SetLength(0);
                        XMLParamsProcessor.Save_ParamXML(Serialize_Stream, Param_Number_Profile_object[count], Type_Param_Number_Profile);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                    }
                }
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Number Profile " + Ex.Message);
                throw new Exception("Error Saving Number Profile " + Ex.Message, Ex);
            }
        }

        public static void Save_NumberProfileShowError(string Dir, Param_Number_Profile[] Param_Number_Profile_object)
        {
            try
            {
                Save_NumberProfile(Dir, Param_Number_Profile_object);
            }
            catch (Exception Ex)
            {

                ///throw new Exception("Error Loading Number Profile " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving Number Profile ", Ex.Message, 1500);
            }
        }
        public static void Save_Standard_NumberProfileShowError(string Dir, Param_Standard_Number_Profile[] Param_Number_Profile_object)
        {
            try
            {
                Save_Standard_NumberProfile(Dir, Param_Number_Profile_object);
            }
            catch (Exception Ex)
            {

                ///throw new Exception("Error Loading Number Profile " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving Number Profile ", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_WakeUp_Profile

        public static Param_WakeUp_Profile[] load_WakeUpProfile(string Dir)
        {
            Param_WakeUp_Profile[] Param_Wakeup_Profile_object = new Param_WakeUp_Profile[Param_WakeUp_ProfileHelper.Max_WakeUp_Profile];
            try
            {
                Dir += "WakeUpProfile\\";
                if (Directory.Exists(Dir))
                {
                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        for (int count = 0; count < Param_Wakeup_Profile_object.Length; count++)
                        {
                            string FILE = String.Format("WakeUpProfile_{0}.xml", (count + 1));
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Serialize_Stream.SetLength(0);
                            XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                            Serialize_Stream.Seek(0, SeekOrigin.Begin);
                            Param_Wakeup_Profile_object[count] = (Param_WakeUp_Profile)XMLParamsProcessor.
                                Load_ParamXML(Serialize_Stream, typeof(Param_WakeUp_Profile));
                        }
                    }
                }
                else
                {
                    throw new Exception("The following folder doesnot exist:" + Dir);
                }
                return Param_Wakeup_Profile_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading WakeUpProfile " + Ex.Message, Ex);
            }
        }

        public static Param_WakeUp_Profile[] load_WakeUpProfileShowError(string Dir)
        {
            try
            {
                return load_WakeUpProfile(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading WakeUpProfile " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading WakeUpProfile ", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_WakeUpProfile(string Dir, Param_WakeUp_Profile[] Param_Wakeup_Profile_object)
        {
            try
            {
                Dir += "\\WakeUpProfile\\";                      /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                Type Type_Param_WakeUp_Profile = typeof(Param_WakeUp_Profile);
                using (MemoryStream Serialize_Stream = new MemoryStream(512))
                {
                    for (int count = 0; count < Param_Wakeup_Profile_object.Length; count++)
                    {
                        string FILE = String.Format("WakeUpProfile_{0}.xml", (count + 1));     ///For Saving the item
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Serialize_Stream.SetLength(0);
                        XMLParamsProcessor.Save_ParamXML(Serialize_Stream, Param_Wakeup_Profile_object[count], Type_Param_WakeUp_Profile);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving WakeUpProfile " + Ex.Message, Ex);
            }
        }

        public static void Save_WakeUpProfileShowError(string Dir, Param_WakeUp_Profile[] Param_Wakeup_Profile_object)
        {
            try
            {
                Save_WakeUpProfile(Dir, Param_Wakeup_Profile_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving WakeUpProfile " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving WakeUpProfile ", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_MDI_parameters

        public static Param_MDI_parameters load_MDIParams(string Dir)
        {
            Param_MDI_parameters Param_MDI_parameters_object = null;
            try
            {
                Dir += "MDIParams\\";
                if (Directory.Exists(Dir))
                {
                    string FILE = "MDIParams.xml";     ///For saving the items.

                    using (MemoryStream Serialize_Stream = new MemoryStream(512))
                    {
                        XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                        Serialize_Stream.Seek(0, SeekOrigin.Begin);
                        Param_MDI_parameters_object = (Param_MDI_parameters)XMLParamsProcessor.
                            Load_ParamXML(Serialize_Stream, typeof(Param_MDI_parameters));
                    }
                }
                else
                {
                    throw new Exception("The following folder does not exist:" + Dir);
                }
                return Param_MDI_parameters_object;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading MDIParams. " + Ex.Message, Ex);
            }
        }

        public static Param_MDI_parameters load_MDIParamsShowError(string Dir)
        {
            try
            {
                return load_MDIParams(Dir);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Loading MDIParams. " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading MDIParams. ", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_MDIParams(string Dir, Param_MDI_parameters Param_MDI_parameters_object)
        {
            try
            {
                Dir += "\\MDIParams\\";
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                /// Path for saving test files.
                string FILE = "MDIParams.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(256))
                {
                    XMLParamsProcessor.Save_ParamXML(Serialize_Stream,
                        Param_MDI_parameters_object, typeof(Param_MDI_parameters));
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving MDIParams" + Ex.Message, Ex);
            }
        }

        public static void Save_MDIParamsShowError(string Dir, Param_MDI_parameters Param_MDI_parameters_object)
        {
            try
            {
                Save_MDIParams(Dir, Param_MDI_parameters_object);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving MDIParams" + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving MDIParams", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_ActivityCalendar

        public static Param_ActivityCalendar Load_ActivityCalendar(String Dir)
        {
            Param_ActivityCalendar Calendar = null;
            try
            {
                Dir += "ActivityCalendar\\";                 /// Path for saving test files.
                Directory.CreateDirectory(Dir);
                string FILE = "Param_ActivityCalendar.xml";     // For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, Dir + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Calendar = (Param_ActivityCalendar)XMLParamsProcessor.
                        Load_Param(Serialize_Stream);
                }
                return Calendar;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Activity Calendar " + Ex.Message);
                throw new Exception("Error Loading Activity Calendar," + Ex.Message, Ex);
            }
        }

        public static Param_ActivityCalendar Load_ActivityCalendarShowError(String Dir)
        {
            try
            {
                return Load_ActivityCalendar(Dir);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Activity Calendar " + Ex.Message);
                ///throw new Exception("Error Loading Activity Calendar " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Loading Activity Calendar", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_ActivityCalendar(String Dir, Param_ActivityCalendar Calendar)
        {
            try
            {
                Dir += "\\ActivityCalendar\\";                 /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(Dir);

                string FILE = "Param_ActivityCalendar.xml";     /// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Calendar);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, Dir + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Activity Calendar " + Ex.Message, Ex);
            }
        }

        public static void Save_ActivityCalendarShowError(String Dir, Param_ActivityCalendar Calendar)
        {
            try
            {
                Save_ActivityCalendar(Dir, Calendar);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Activity Calendar " + Ex.Message, Ex);
                //PCL Notification notifier = new Notification("Error Saving Activity Calendar", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_TBEs

        public static TBE Load_TBEs(string path)
        {
            TBE tbe_obj = null;
            try
            {
                path += "TBEs\\";              /// Path for saving test files.
                Directory.CreateDirectory(path);
                string FILE = "TBEs.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    tbe_obj = (TBE)XMLParamsProcessor.
                    Load_Param(Serialize_Stream);
                }
                return tbe_obj;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                throw new Exception("Error Loading Time Based Profile " + Ex.Message);
            }
        }

        public static TBE Load_TBEsShowError(string path)
        {
            try
            {
                return Load_TBEs(path);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                ///throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Time Based Profile", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_TOFILE_TBEs(string path, TBE tbe_obj)
        {
            try
            {
                path += "\\TBEs\\";                 /// Path for saving test files.
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);
                string FILE = "TBEs.xml";     /// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, tbe_obj);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    XMLParamsProcessor.Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Time Based Profile " + Ex.Message);
            }
        }

        public static void Save_TOFILE_TBEsShowError(string path, TBE tbe_obj)
        {
            try
            {
                Save_TOFILE_TBEs(path, tbe_obj);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Time Based Profile", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_MajorAlarm

        public static Param_MajorAlarmProfile Load_MajorAlarm(string path, string currentMeterInfo)
        {
            Param_MajorAlarmProfile Param_MajorAlarmProfileObj = null;
            try
            {
                path += "\\MajorAlarmProfile\\";      ///Path for saving test files
                Directory.CreateDirectory(path);
                string FILE = $"MajorAlarms_{currentMeterInfo}.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Param_MajorAlarmProfileObj = (Param_MajorAlarmProfile)XMLParamsProcessor.Load_Param(Serialize_Stream);
                }
                return Param_MajorAlarmProfileObj;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                //Temporary commented
                //throw new Exception("Error Loading Param MajorAlarmProfile " + Ex.Message, Ex);
                //return Param_MajorAlarmProfileObj;
            }
            return new Param_MajorAlarmProfile();
        }

        public static Param_MajorAlarmProfile Load_MajorAlarmShowError(string path, string meterInfo)
        {
            //return Load_MajorAlarm(path);

            //temporary commented
            try
            {
                return Load_MajorAlarm(path, meterInfo);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                ///throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Param MajorAlarmProfile", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_MajorAlarm(string path, string currentMeterInfo, Param_MajorAlarmProfile Param_MajorAlarmProfileObj)
        {
            try
            {
                path += "\\MajorAlarmProfile\\";       ///Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);

                string FILE = $"MajorAlarms_{currentMeterInfo}.xml";/// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Param_MajorAlarmProfileObj);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param MajorAlarmProfile" + Ex.Message);
            }
        }

        public static void Save_MajorAlarmShowError(string path, string currentMeterInfo, Param_MajorAlarmProfile Param_MajorAlarmProfileObj)
        {
            try
            {
                Save_MajorAlarm(path, currentMeterInfo, Param_MajorAlarmProfileObj);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Param MajorAlarmProfile", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_EventCaution

        public static Param_EventsCaution[] Load_EventsCautions(string path)
        {
            Param_EventsCaution[] Param_EventCautions = null;
            try
            {
                path += "\\EventCautions\\";      ///Path for saving test files
                Directory.CreateDirectory(path);
                string FILE = "EventCautions.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Param_EventCautions = (Param_EventsCaution[])XMLParamsProcessor.Load_Params(Serialize_Stream);
                }
                return Param_EventCautions;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                throw new Exception("Error Loading Param EventCautions " + Ex.Message, Ex);
            }
        }

        public static Param_EventsCaution[] Load_EventsCautionsShowError(string path)
        {
            try
            {
                return Load_EventsCautions(path);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                ///throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Param EventCautions", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_EventsCautions(string path, Param_EventsCaution[] Param_EventCautions)
        {
            try
            {
                if (Param_EventCautions == null)
                    throw new ArgumentNullException("Param_EventCautions_Arg", "Invalid argument Param_EventCautions List");
                List<Param_EventsCaution> AlarmsItems = new List<Param_EventsCaution>(Param_EventCautions);
                Param_EventCautionHelper.Init_ParamEventCaution(AlarmsItems);

                path += "\\EventCautions\\";      ///Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);

                string FILE = "EventCautions.xml";/// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, AlarmsItems.ToArray());
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param EventCautions" + Ex.Message);
            }
        }

        public static void Save_EventsCautionsShowError(string path, Param_EventsCaution[] Param_EventCautions)
        {
            try
            {
                Save_EventsCautions(path, Param_EventCautions);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Param EventCautions", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_DisplayPowerDownMode

        public static Param_Display_PowerDown Load_DisplayPowerDown(string path)
        {
            Param_Display_PowerDown Param_DisplayPowerDownObj = null;
            try
            {
                path += "\\DisplayPowerDown\\";      ///Path for saving test files
                Directory.CreateDirectory(path);
                string FILE = "DisplayPowerDown.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Param_DisplayPowerDownObj = (Param_Display_PowerDown)XMLParamsProcessor.Load_Param(Serialize_Stream);
                }
                return Param_DisplayPowerDownObj;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                throw new Exception("Error Loading ParamDisplayPowerDown " + Ex.Message, Ex);
            }
        }

        public static Param_Display_PowerDown Load_DisplayPowerDownShowError(string path)
        {
            try
            {
                return Load_DisplayPowerDown(path);
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                ///throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Param ParamDisplayPowerDown", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_DisplayPowerDown(string path, Param_Display_PowerDown Param_DisplayPowerDownObj)
        {
            try
            {
                path += "\\DisplayPowerDown\\";      ///Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);

                string FILE ="DisplayPowerDown.xml";/// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Param_DisplayPowerDownObj);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param_DisplayPowerDownObj" + Ex.Message);
            }
        }

        public static void Save_DisplayPowerDownShowError(string path, Param_Display_PowerDown Param_DisplayPowerDownObj)
        {
            try
            {
                Save_DisplayPowerDown(path, Param_DisplayPowerDownObj);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Param Param_DisplayPowerDownObj", Ex.Message, 1500);
            }
        }

        #endregion

        #region Param_GeneralProcess

        public static Param_Generel_Process Load_GeneralProcess(string path)
        {
            Param_Generel_Process Param_GeneralProcessParam = null;
            try
            {
                path += "\\GeneralProcess\\";      //Path for saving test files
                Directory.CreateDirectory(path);
                string FILE = "GeneralProcess.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Param_GeneralProcessParam = (Param_Generel_Process)XMLParamsProcessor.Load_Param(Serialize_Stream);
                }
                return Param_GeneralProcessParam;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Loading ParamGeneralProcess " + Ex.Message, Ex);
            }
        }

        public static Param_Generel_Process Load_GeneralProcessShowError(string path)
        {
            try
            {
                return Load_GeneralProcess(path);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                //throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading ParamGeneralProcess", Ex.Message, 1500);
            }
            return null;
        }

        public static void Save_GeneralProcess(string path, Param_Generel_Process Param_GeneralProcessObj)
        {
            try
            {
                path += "\\GeneralProcess\\";      //Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);

                string FILE = "GeneralProcess.xml";// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Param_GeneralProcessObj);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param_Generel_Process" + Ex.Message);
            }
        }

        public static void Save_GeneralProcessShowError(string path, Param_Generel_Process Param_GeneralProcessObj)
        {
            try
            {
                Save_GeneralProcess(path, Param_GeneralProcessObj);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Param Param_Generel_Process", Ex.Message, 1500);
            }
        }

        #endregion


        #region Param_LoadSchedule
        
        public static void Save_LoadSheddingShowError(string path, Param_Load_Scheduling Param_LoadScheduleObj)
        {
            try
            {
                Save_LoadSchedule(path, Param_LoadScheduleObj);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Param MajorAlarmProfile", Ex.Message, 1500);
            }
        }

        public static void Save_LoadSchedule(string path, Param_Load_Scheduling Param_LoadScheduleObj)
        {
            try
            {
                path += "\\LoadShedding\\";      ///Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);

                string FILE = "LoadSheddings.xml";/// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Param_LoadScheduleObj);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param LoadShedding" + Ex.Message);
            }
        }

        public static Param_Load_Scheduling Load_LoadSchedule(string path)
        {
            Param_Load_Scheduling Param_LoadSheddingObj = null;
            try
            {
                path += "\\LoadShedding\\";      ///Path for saving test files
                Directory.CreateDirectory(path);
                string FILE = "LoadSheddings.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Param_LoadSheddingObj = (Param_Load_Scheduling)XMLParamsProcessor.Load_Param(Serialize_Stream);
                }
                return Param_LoadSheddingObj;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                //Temporary commented
                //throw new Exception("Error Loading Param LoadShedding " + Ex.Message, Ex);
                //return Param_MajorAlarmProfileObj;
            }
            return new Param_Load_Scheduling();
        }

        public static Param_Load_Scheduling Load_LoadSheddingShowError(string path)
        {
            //return Load_MajorAlarm(path);

            //temporary commented
            try
            {
                return Load_LoadSchedule(path);
            }
            catch (Exception Ex)
            {
                
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                ///throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Param MajorAlarmProfile", Ex.Message, 1500);
            }
            return new Param_Load_Scheduling();
        }

        #endregion
        
        //TODO: SaveToFile 07 - Add Region For Save and Get from File

        #region Param_EnergyMizer
        
        public static void Save_EnergyMizerShowError(string path, Param_Energy_Mizer Param_EnergyMizerObj)
        {
            try
            {
                Save_EnergyMizer(path, Param_EnergyMizerObj);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Param EnergyMizer", Ex.Message, 1500);
            }
            
        }

        public static void Save_EnergyMizer(string path, Param_Energy_Mizer Param_EnergyMizerObj)
        {
            try
            {
                path += "\\EnergyMizer\\";      ///Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);

                string FILE = "EnergyMizer.xml";/// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Param_EnergyMizerObj);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param EnergyMizer" + Ex.Message);
            }
        }

        public static Param_Energy_Mizer Load_EnergyMizer(string path)
        {
            Param_Energy_Mizer Param_EnergyMizerObj = null;
            try
            {
                path += "\\EnergyMizer\\";      ///Path for saving test files
                Directory.CreateDirectory(path);
                string FILE = "EnergyMizer.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Param_EnergyMizerObj = (Param_Energy_Mizer)XMLParamsProcessor.Load_Param(Serialize_Stream);
                }
                return Param_EnergyMizerObj;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                //Temporary commented
                throw new Exception("Error Loading Param EnergyMizer " + Ex.Message, Ex);
                //return Param_MajorAlarmProfileObj;
            }
        }

        public static Param_Energy_Mizer Load_EnergyMizerShowError(string path)
        {
            try
            {
                return Load_EnergyMizer(path);
            }
            catch (Exception Ex)
            {

                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                ///throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Param EnergyMizer ", Ex.Message, 1500);
            }
            return new Param_Energy_Mizer();
        }

        #endregion


        #region Param_GeneratorStart

        public static void Save_GeneratorStartShowError(string path, Param_Generator_Start Param_GeneratorStartObj)
        {
            try
            {
                Save_GeneratorStart(path, Param_GeneratorStartObj);
            }
            catch (Exception Ex)
            {
                ///throw new Exception("Error Saving Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Saving Param MajorAlarmProfile", Ex.Message, 1500);
            }
        }

        public static void Save_GeneratorStart(string path, Param_Generator_Start Param_GeneratorStartObj)
        {
            try
            {
                path += "\\GeneratorStart\\";      ///Path for saving test files
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(path);

                string FILE = "GeneratorStart.xml";/// For saving the items.
                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Save_Param(Serialize_Stream, Param_GeneratorStartObj);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Save_ParamToFile(Serialize_Stream, path + FILE);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Saving Param GeneratorStart" + Ex.Message);
            }
        }

        public static Param_Generator_Start Load_GeneratorStart(string path)
        {
            Param_Generator_Start Param_GeneratorStartObj = null;
            try
            {
                path += "\\GeneratorStart\\";      ///Path for saving test files
                Directory.CreateDirectory(path);
                string FILE = "GeneratorStart.xml";     // For saving the items.

                using (MemoryStream Serialize_Stream = new MemoryStream(1024))
                {
                    XMLParamsProcessor.Load_ParamFromFile(Serialize_Stream, path + FILE);
                    Serialize_Stream.Seek(0, SeekOrigin.Begin);
                    Param_GeneratorStartObj = (Param_Generator_Start)XMLParamsProcessor.Load_Param(Serialize_Stream);
                }
                return Param_GeneratorStartObj;
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                //Temporary commented
                throw new Exception("Error Loading Param GeneratorStart " + Ex.Message, Ex);
                //return Param_MajorAlarmProfileObj;
            }
        }

        public static Param_Generator_Start Load_GeneratorStartShowError(string path)
        {
            //return Load_MajorAlarm(path);

            //temporary commented
            try
            {
                return Load_GeneratorStart(path);
            }
            catch (Exception Ex)
            {

                ///MessageBox.Show("Error Loading Time Based Profile " + Ex.Message);
                ///throw new Exception("Error Loading Time Based Profile " + Ex.Message);
                //PCL Notification notifier = new Notification("Error Loading Param MajorAlarmProfile", Ex.Message, 1500);
            }
            return new Param_Generator_Start();
        }

        #endregion


        public static void Save_AllParameters(String fileURL, List<IParam> ParametersObjs, HeaderInfo hInof = null)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileURL);
                /// fileURL += "\\ParameterExport\\Parameters.conf";    /// Path for saving test files
                if (!fileInfo.Exists)
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                using (FileStream new_File = new FileStream(fileURL, FileMode.Create))
                {
                    Save_AllParametersHelper(new_File, ParametersObjs, hInof);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Save_AllParameters" + Ex.Message, Ex);
            }
        }

        public static void Save_AllParametersHelper(Stream OutStream, List<IParam> ParametersObjs, HeaderInfo hInof = null)
        {
            try
            {
                MemoryStream WriterStream = new MemoryStream(1024); ///Initial 1K Buffer 
                foreach (var obj in ParametersObjs)
                {
                    Save_Param(WriterStream, obj);
                }
                CryptoHelper EncryptStream = new CryptoHelper();
                ///Test Code Header
                if (hInof == null)
                {
                    hInof = new HeaderInfo();
                    hInof.ConfigName = "Default Config";
                    hInof.Description = "Default Config provide the default set of configuration for given set of meters";
                }
                ConfigFileHelper.Init_HeaderInfo_ParamList(ParametersObjs, hInof);
                ConfigFileHelper.Init_HeaderInfo_ParamCategoryList(hInof);
                if (hInof.HashFunction == HashAlgorithmType.None)
                {
                    ///Default Hash Function MD5
                    hInof.HashFunction = HashAlgorithmType.Md5;
                }
                ///Create Signature For Parameters
                WriterStream.Seek(0, SeekOrigin.Begin);
                string signature = EncryptStream.GetSignature(WriterStream, hInof.HashFunction);
                hInof.Signature = signature;
                ///Build Serialized Header Stream
                MemoryStream headerStream = new MemoryStream(512); ///Initial 1K Buffer 
                Save_Param(headerStream, hInof);

                ///Process Header Stream
                headerStream.Seek(0, SeekOrigin.Begin);
                headerStream = (MemoryStream)EncryptStream.GetEncryptedStream(headerStream);
                ///Write Header
                byte[] block = new byte[headerStream.Length - headerStream.Position];
                int length = -1;
                ///Write Header Length
                BinaryWriter writer = new BinaryWriter(OutStream);
                writer.Write((Int32)headerStream.Length);
                ///Write Header Into Memory
                length = headerStream.Read(block, 0, block.Length);
                if (length == 0)
                    throw new FormatException("Invalid headerInfo detected");
                OutStream.Write(block, 0, length);
                ///Write File Contents here
                WriterStream.Seek(0, SeekOrigin.Begin);
                ///For Fast File Process
                ///Disable FileContentEncryption Process 
                ///WriterStream = (MemoryStream)EncryptStream.GetEncryptedStream(WriterStream);
                block = new byte[512];
                while (WriterStream.CanRead)
                {
                    length = WriterStream.Read(block, 0, block.Length);
                    if (length == 0)
                        break;
                    OutStream.Write(block, 0, length);
                }
                OutStream.Flush();
                //Todo:Debug/Check Donot Disposal of OutSteram
                //writer.Dispose();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Save_AllParameters" + Ex.Message, Ex);
            }
        }

        public static List<IParam> Load_AllParameters(string fileURL)
        {
            List<IParam> Param_Deserialize = null;
            HeaderInfo hInfo = null;
            try
            {
                ///fileURL += "\\ParameterExport\\";   ///Path for saving test files.
                FileInfo fileInfo = new FileInfo(fileURL);
                /// fileURL += "\\ParameterExport\\Parameters.conf";    /// Path for saving test files
                if (!fileInfo.Exists)
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                ///string FILE = "Parameters_" + DateTime.UtcNow.ToFileTime() + ".xml";
                ///string FILE = "Parameters.conf";
                using (FileStream new_File = new FileStream(fileURL, FileMode.Open))
                {
                    Param_Deserialize = Load_AllParameters_Helper(new_File);
                    hInfo = (HeaderInfo)Param_Deserialize[0];
                    hInfo.FileURL = fileURL;
                }
                return Param_Deserialize;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Load_AllParameters" + Ex.Message, Ex);
            }
        }

        public static List<IParam> Load_AllParameters_Helper(Stream new_File)
        {
            List<IParam> Param_Deserialize = new List<IParam>();
            try
            {

                HeaderInfo hInfo = null;
                MemoryStream ReaderStream = null;  ///Initail 1K Buffer
                MemoryStream HeaderReaderStream = null;  ///Initail 1K Buffer
                CryptoHelper DecryptStream = new CryptoHelper();
                byte[] block = null;
                int length = -1;
                using (new_File)
                {
                    new_File.Seek(0, SeekOrigin.Begin);
                    ///Process Headers Before Content
                    HeaderReaderStream = new MemoryStream(128);
                    BinaryReader reader = new BinaryReader(new_File);
                    int header_Length = reader.ReadInt32();
                    block = new byte[header_Length];
                    length = new_File.Read(block, 0, header_Length);
                    ///EOF Stream marker;Check Error Conditions
                    if (length <= 0)
                    {
                        throw new FormatException("Invalid Header Format Received");
                    }
                    else
                    {
                        HeaderReaderStream = new MemoryStream(block);
                        HeaderReaderStream = (MemoryStream)DecryptStream.GetDecryptedStream(HeaderReaderStream);
                        HeaderReaderStream.Seek(0, SeekOrigin.Begin);
                        hInfo = (HeaderInfo)Load_Param(HeaderReaderStream);
                        //Process Header here
                        if (hInfo == null || hInfo.HashFunction == HashAlgorithmType.None)
                            throw new FormatException("Invalid Header Format Received");
                    }
                    //Init Reader Stream
                    ReaderStream = new MemoryStream(1024);
                    block = new byte[512];
                    while (new_File.CanRead)
                    {
                        length = new_File.Read(block, 0, block.Length);
                        if (length == 0)
                            break;
                        ReaderStream.Write(block, 0, length);
                    }
                    //For Fast File Processing
                    //Disable File Decryption
                    //ReaderStream = (MemoryStream)DecryptStream.GetDecryptedStream(new_File);
                    //Verify Signature here

                    ReaderStream.Seek(0, SeekOrigin.Begin);
                    hInfo.SignatureComputed = DecryptStream.GetSignature(ReaderStream, hInfo.HashFunction);
                    ///Verify Digital Signature here
                    if (!String.Equals(hInfo.SignatureComputed, hInfo.Signature))
                        throw new FormatException("Invalid Configuration file Format,signature not verified");
                    //Add HeaderInfo as first Parameters
                    Param_Deserialize.Add(hInfo);
                    //hInfo.FileURL = fileURL;
                }
                ReaderStream.Seek(0, SeekOrigin.Begin);
                Object DeserlObj = null;
                IParam ParamDeserlObj = null;
                while (ReaderStream.CanRead)
                {
                    ParamDeserlObj = null;
                    length = ReaderStream.Read(block, 0, 1);
                    //EOF Stream marker;Check Error Conditions
                    if (length == 0)
                    {
                        if (Param_Deserialize.Count <= 0)
                            throw new FormatException("Invalid Configuration file Format,Invalid file length");
                        else
                            break;
                    }
                    else
                    {
                        DeserlObj = ParamDeserlObj = null;
                        ReaderStream.Seek(-1, SeekOrigin.Current);
                        DeserlObj = Load_Param(ReaderStream);
                        ParamDeserlObj = (IParam)DeserlObj;
                        Param_Deserialize.Add(ParamDeserlObj);
                    }
                }
                return Param_Deserialize;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Load_AllParameters" + Ex.Message, Ex);
            }
        }

        public static List<HeaderInfo> Load_HeaderInfo(string fileURL)
        {
            List<HeaderInfo> hInfo_Deserialize = new List<HeaderInfo>();
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(fileURL);
                FileInfo[] files = dirInfo.GetFiles("*.conf");
                ///string FILE = "Parameters_" + DateTime.UtcNow.ToFileTime() + ".xml";
                HeaderInfo hInfo = null;
                MemoryStream HeaderReaderStream = null;  ///Initail 1K Buffer
                CryptoHelper DecryptStream = new CryptoHelper();
                byte[] block = null;
                int length = -1;
                int header_Length = -1;
                foreach (FileInfo info in files)
                {
                    if (!info.Exists)
                        continue;
                    using (FileStream new_File = new FileStream(info.FullName, FileMode.Open))
                    {
                        ///Process Headers Before Content
                        HeaderReaderStream = new MemoryStream(128);
                        BinaryReader reader = new BinaryReader(new_File);
                        header_Length = reader.ReadInt32();
                        block = new byte[header_Length];
                        length = new_File.Read(block, 0, header_Length);
                        reader.Close();
                    }
                    ///EOF Stream marker;Check Error Conditions
                    if (length <= 0)
                    {
                        throw new FormatException("Invalid Configuration file format,invalid Header");
                    }
                    else
                    {
                        HeaderReaderStream = new MemoryStream(block);
                        HeaderReaderStream = (MemoryStream)DecryptStream.GetDecryptedStream(HeaderReaderStream);
                        HeaderReaderStream.Seek(0, SeekOrigin.Begin);
                        hInfo = (HeaderInfo)Load_Param(HeaderReaderStream);
                        ///Process/Validate File InfoHeader here
                        if (hInfo == null || hInfo.HashFunction == HashAlgorithmType.None || String.IsNullOrEmpty(hInfo.Signature))
                            throw new FormatException("Invalid Configuration file format,invalid Header");
                    }
                    hInfo.FileURL = info.FullName;
                    ///Add HeaderInfo as first Parameters
                    hInfo_Deserialize.Add(hInfo);
                }
                return hInfo_Deserialize;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error occurred while loading Load_HeaderInfo" + Ex.Message, Ex);
            }
        }

        #region Helper_Methods

        public static void Save_ParamToFile(Stream SerlizerStream, string fileURL)
        {
            try
            {
                using (BufferedStream OutStream_File = new BufferedStream(File.Open(fileURL,FileMode.OpenOrCreate,FileAccess.ReadWrite)))
                {
                    SerlizerStream.CopyTo(OutStream_File);
                    OutStream_File.Close();
                }

                //using (FileStream stream = File.Open(fileURL, FileMode.OpenOrCreate, FileAccess.ReadWrite)) 
                //{
                //    StreamReader Reader = 
                //}

                
            }
            catch
            {
                throw;
            }
        }

        public static void Load_ParamFromFile(Stream SerlizerStream, string fileURL)
        {
            try
            {
                //using (Stream strm = File.Open(fileURL, FileMode.Open, FileAccess.Read)) 
                //{
                //    StreamReader reader = new StreamReader(strm);
                //    reader.BaseStream.CopyTo(SerlizerStream);
                //}

                using (BufferedStream InputStream_File = new BufferedStream(File.Open(fileURL, FileMode.Open, FileAccess.Read)))
                {
                    InputStream_File.CopyTo(SerlizerStream);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void Save_ParamXML(Stream SerlizerStream, IParam Param_SerializeAble, Type Param_Type)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(Param_Type);
                x.Serialize(SerlizerStream, Param_SerializeAble);
            }
            catch
            {
                throw;
            }
        }

        public static void Save_ParamXML(Stream SerlizerStream, IParam Param_SerializeAble)
        {
            try
            {
                SoapFormatter formatter = new SoapFormatter();
                formatter.Serialize(SerlizerStream, Param_SerializeAble);
            }
            catch
            {
                throw;
            }
        }

        public static void Save_ParamXML(Stream SerlizerStream, IParam[] Param_SerializeAble)
        {
            try
            {
                SoapFormatter formatter = new SoapFormatter();
                formatter.Serialize(SerlizerStream, Param_SerializeAble);
            }
            catch
            {
                throw;
            }
        }

        public static void Save_Param(Stream SerlizerStream, IParam Param_SerializeAble)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(SerlizerStream, Param_SerializeAble);
            }
            catch
            {
                throw;
            }
        }

        public static void Save_Param(Stream SerlizerStream, IParam[] Param_SerializeAble)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(SerlizerStream, Param_SerializeAble);
            }
            catch
            {
                throw;
            }
        }

        public static void Save_ParamXML(Stream SerlizerStream, IParam[] Param_SerializeAble, Type Param_Type)
        {
            try
            {
                Type ArrayType = Array.CreateInstance(Param_Type, 0).GetType();
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(ArrayType);
                x.Serialize(SerlizerStream, Param_SerializeAble);
            }
            catch (Exception Ex)
            {
                throw new Exception(String.Format("Error Saving Parameter {0} {1}", Param_Type.ToString(), Ex.Message), Ex);
            }
        }

        public static IParam Load_ParamXML(Stream DeserlizerStream, Type Param_Type)
        {
            IParam Param_Deserializeable;
            try
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(Param_Type);
                Param_Deserializeable = (IParam)x.Deserialize(DeserlizerStream);
            }
            catch
            {
                throw;
            }
            return Param_Deserializeable;
        }

        public static IParam Load_ParamXML(Stream DeserlizerStream)
        {
            IParam Param_Deserializeable;
            try
            {
                SoapFormatter formatter = new SoapFormatter();
                Param_Deserializeable = (IParam)formatter.Deserialize(DeserlizerStream);
            }
            catch
            {
                throw;
            }
            return Param_Deserializeable;
        }

        public static object Load_Param(Stream DeserlizerStream)
        {
            object Param_Deserializeable = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Param_Deserializeable = formatter.Deserialize(DeserlizerStream);
            }
            catch (Exception Ex)
            {
                String ParamName = String.Empty;
                if (Param_Deserializeable != null)
                {
                    ParamName = Param_Deserializeable.ToString();
                    if (ParamName.Length >= 25)
                        ParamName = ParamName.Substring(0, 25);
                }
                //throw new Exception(String.Format("Error Loading Parameter {0} {1}", Ex.Message, ParamName), Ex);
            }
            return Param_Deserializeable;
        }

        public static object[] Load_Params(Stream DeserlizerStream)
        {
            object[] Param_Deserializeable = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Param_Deserializeable = (object[])formatter.Deserialize(DeserlizerStream);
            }
            catch
            {
                throw;
            }
            return Param_Deserializeable;
        }

        public static IParam[] Load_ParamsXML(Stream DeserlizerStream, Type Param_Type)
        {
            IParam[] Params_Deserializeable;
            try
            {
                Type ArrayType = Array.CreateInstance(Param_Type, 0).GetType();
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(ArrayType);
                Params_Deserializeable = (IParam[])x.Deserialize(DeserlizerStream);
            }
            catch (Exception Ex)
            {
                throw new Exception(String.Format("Error Loading Parameters {0} {1}", Param_Type.ToString(), Ex.Message), Ex);
            }
            return Params_Deserializeable;
        }

        public static IParam[] Load_ParamsXML(Stream DeserlizerStream)
        {
            IParam[] Params_Deserializeable;
            try
            {
                SoapFormatter formatter = new SoapFormatter();
                Params_Deserializeable = (IParam[])formatter.Deserialize(DeserlizerStream);
            }
            catch
            {
                throw;
            }
            return Params_Deserializeable;
        }

        #endregion

        #region Import_AllParams

        public static ParamConfigurationSet Import_AllParams(string fileURL, string currentMeterInfo,
            ParamConfigurationSet paramterConfigurationSet = null,
            List<Params> SelectedParameters = null,
            List<ParamsCategory> SelectedParamsCategory = null)
        {
            List<IParam> ParamList = null;
            try
            {
                #region Commented_CodeSection

                ///FolderBrowserDialog Folder = new FolderBrowserDialog();
                ///Folder.RootFolder = Environment.SpecialFolder.Desktop;
                ///Folder.ShowDialog();
                ///string Dir = Folder.SelectedPath;
                ///Create directory if not Exists
                //if (!new DirectoryInfo(Dir).Exists)
                // 
                //if (!String.IsNullOrEmpty(Dir) || !String.IsNullOrWhiteSpace(Dir))
                //{
                ///Load All Parameters From Single XML Source File
                ///ParamList = XMLParamsProcessor.Load_AllParameters(Dir + "\\ParameterExport\\Parameters.conf"); 

                #endregion

                if (paramterConfigurationSet == null)
                    paramterConfigurationSet = new ParamConfigurationSet();

                #region Handle_Null_ArgumentsException

                if ((SelectedParameters == null || SelectedParameters.Count <= 0) &&
                   (SelectedParamsCategory == null || SelectedParamsCategory.Count <= 0))
                {
                    var AllCategories = Enum.GetValues(typeof(ParamsCategory));
                    SelectedParamsCategory = AllCategories.Cast<ParamsCategory>().ToList();
                }

                #endregion

                if (SelectedParameters == null)
                    SelectedParameters = new List<Params>();

                #region ///Process ParamsCategory
                foreach (var category in SelectedParamsCategory)
                {
                    Params[] paramList = ConfigFileHelper.GetParamsByCategory(category);
                    foreach (var selParam in paramList)
                    {
                        if (!SelectedParameters.Contains(selParam))
                            SelectedParameters.Add(selParam);
                    }
                }
                SelectedParameters.Sort();
                #endregion

                Object loadedObj = null;
                Object[] loadedObjs = null;
                if (SelectedParameters.Contains(Params.ParamTariffication))
                {
                    loadedObj = XMLParamsProcessor.Load_ActivityCalendarShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamTariffication = (Param_ActivityCalendar)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamCustomerReferenceCode))
                {
                    loadedObj = XMLParamsProcessor.load_CustomerReferenceShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamCustomerReferenceCode = (Param_Customer_Code)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamPassword))
                {
                    loadedObj = XMLParamsProcessor.load_ParamPasswordsShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamPassword = (Param_Password)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamCTPTRatio))
                {
                    loadedObj = XMLParamsProcessor.load_CTPT_RatiosShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamCTPTRatio = (Param_CTPT_Ratio)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamEnergy))
                {
                    loadedObj = XMLParamsProcessor.load_EnergyParamShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamEnergy = (Param_Energy_Parameter)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamDecimalPoint))
                {
                    loadedObj = XMLParamsProcessor.load_DecimalPointShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamDecimalPoint = (Param_Decimal_Point)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamStatausWordMap))
                {
                    loadedObj = XMLParamsProcessor.load_StatusWordMap(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamStatusWordMap = (Param_StatusWordMap)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamClockCalib))
                {
                    loadedObj = XMLParamsProcessor.load_ClockShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamClockCalib = (Param_Clock_Caliberation)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamContactor))
                {
                    loadedObj = XMLParamsProcessor.load_ContactorShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamContactor = (Param_Contactor)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamDisplayWindowsNormal) ||
                    SelectedParameters.Contains(Params.ParamDisplayWindowsAlternate) ||
                    SelectedParameters.Contains(Params.ParamDisplayWindowsTestMode))
                {
                    loadedObjs = XMLParamsProcessor.load_DisplayWindowsShowError(fileURL);
                    if (loadedObjs != null)
                    {
                        try
                        {
                            ///Save ParamDisplayWindowsNormal
                            if (SelectedParameters.Contains(Params.ParamDisplayWindowsNormal))
                                paramterConfigurationSet.ParamDisplayWindowsNormal = (DisplayWindows)loadedObjs[XMLParamsProcessor.Param_DisplayWindowsNormal];
                            ///Save ParamDisplayWindowsAlternate
                            if (SelectedParameters.Contains(Params.ParamDisplayWindowsAlternate))
                                paramterConfigurationSet.ParamDisplayWindowsAlternate = (DisplayWindows)loadedObjs
                                    [XMLParamsProcessor.Param_DisplayWindowsAlternate];
                            ///Save ParamDisplayWindowsTestMode
                            if (SelectedParameters.Contains(Params.ParamDisplayWindowsTestMode))
                                paramterConfigurationSet.ParamDisplayWindowsTestMode = (DisplayWindows)loadedObjs[XMLParamsProcessor.Param_DisplayWindowstest];
                        }
                        catch (Exception) { }
                    }
                }
                ///if (SelectedParameters.Contains(Params.ParamIPV4))
                ///{ 
                ///loadedObj = XMLParamsProcessor.load_IPV4ShowError(fileURL);
                ///if (loadedObj != null)
                ///    paramterConfigurationSet.ParamIPV4 = (Param_IPV4)loadedObj;
                ///    }  
                if (SelectedParameters.Contains(Params.ParamKeepAliveIP))
                {
                    loadedObj = XMLParamsProcessor.load_KeepAliveShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamKeepAliveIP = (Param_Keep_Alive_IP)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamLimits))
                {
                    loadedObj = XMLParamsProcessor.load_LimitsShowError(fileURL,
                    currentMeterInfo);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamLimits = (Param_Limits)loadedObj;

                    ///loadedObjs = XMLParamsProcessor.loadAll_Limits_Param_Limit_Demand_OverLoadShowError(fileURL,
                    ///    application_Controller.CurrentMeterName);
                    ///if (loadedObjs != null)
                    ///{
                    ///    try
                    ///    {
                    ///        Param_Limit_Demand_OverLoad_T1 = (Param_Limit_Demand_OverLoad)loadedObjs[0];
                    ///        Param_Limit_Demand_OverLoad_T2 = (Param_Limit_Demand_OverLoad)loadedObjs[1];
                    ///        Param_Limit_Demand_OverLoad_T3 = (Param_Limit_Demand_OverLoad)loadedObjs[2];
                    ///        Param_Limit_Demand_OverLoad_T4 = (Param_Limit_Demand_OverLoad)loadedObjs[3];
                    ///    }
                    ///    catch (Exception) { }
                    ///}

                }
                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo))
                {
                    loadedObjs = XMLParamsProcessor.Load_LoadProfileShowError(fileURL, LoadProfileScheme.Scheme_1);
                    if (loadedObjs != null && loadedObjs.Length > 0)
                    {
                        try
                        {
                            List<LoadProfileChannelInfo> LoadProfileChannelsInfo = new List<LoadProfileChannelInfo>();
                            LoadProfileChannelsInfo.AddRange((LoadProfileChannelInfo[])loadedObjs);
                            paramterConfigurationSet.ParamLoadProfileChannelInfo = LoadProfileChannelsInfo;

                        }
                        catch (Exception) { }
                    }
                }
                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo_2))
                {
                    loadedObjs = XMLParamsProcessor.Load_LoadProfileShowError(fileURL, LoadProfileScheme.Scheme_2);
                    if (loadedObjs != null && loadedObjs.Length > 0)
                    {
                        try
                        {
                            List<LoadProfileChannelInfo> LoadProfileChannelsInfo = new List<LoadProfileChannelInfo>();
                            LoadProfileChannelsInfo.AddRange((LoadProfileChannelInfo[])loadedObjs);
                            paramterConfigurationSet.ParamLoadProfileChannelInfo_2 = LoadProfileChannelsInfo;
                        }
                        catch (Exception) { }
                    }
                }
                if (SelectedParameters.Contains(Params.ParamMDI))
                {
                    loadedObj = XMLParamsProcessor.load_MDIParamsShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamMDI = (Param_MDI_parameters)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamModemLimitsAndTime))
                {
                    loadedObj = XMLParamsProcessor.load_ModemLimitAndTimeShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamModemLimitsAndTime = (Param_ModemLimitsAndTime)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamModemInitialize))
                {
                    loadedObj = XMLParamsProcessor.load_ModemInitializeShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamModemInitialize = (Param_Modem_Initialize)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamModemBasicsNEW))
                {
                    loadedObj = XMLParamsProcessor.load_ModemInitializeNewShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamModemBasicsNEW = (Param_ModemBasics_NEW)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamMonitoringTime))
                {
                    loadedObj = XMLParamsProcessor.load_MonitoringTimeShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamMonitoringTime = (Param_Monitoring_Time)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamIPProfiles))
                { ///load_IP_profile(fileURL);
                    loadedObjs = XMLParamsProcessor.load_IP_profileShowError(fileURL);
                    if (loadedObjs != null)
                    {
                        paramterConfigurationSet.ParamIPProfiles = (Param_IP_Profiles[])loadedObjs;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamStandardIPProfile))
                { ///load_IP_profile(fileURL);
                    loadedObjs = XMLParamsProcessor.Load_Standard_IP_profileShowError(fileURL);
                    if (loadedObjs != null)
                    {
                        paramterConfigurationSet.ParamStandardIPProfiles = (Param_Standard_IP_Profile[])loadedObjs;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamNumberProfile))
                {
                    ///load_IP_profile(fileURL);
                    loadedObjs = XMLParamsProcessor.load_NumberProfileShowError(fileURL);
                    if (loadedObjs != null)
                    {
                        paramterConfigurationSet.ParamNumberProfile = (Param_Number_Profile[])loadedObjs;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamStandardNumberProfile))
                {
                    ///load_IP_profile(fileURL);
                    loadedObjs = XMLParamsProcessor.Load_Standard_NumberProfileShowError(fileURL);
                    if (loadedObjs != null)
                    {
                        paramterConfigurationSet.ParamStandardNumberProfile = (Param_Standard_Number_Profile[])loadedObjs;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamWakeUpProfile))
                {
                    loadedObjs = XMLParamsProcessor.load_WakeUpProfileShowError(fileURL);
                    if (loadedObjs != null)
                    {
                        paramterConfigurationSet.ParamWakeUpProfile = (Param_WakeUp_Profile[])loadedObjs;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamCommunicationProfile))
                {
                    loadedObj = XMLParamsProcessor.load_CommProfileShowError(fileURL);
                    if (loadedObj != null)
                        paramterConfigurationSet.ParamCommunicationProfile = (Param_Communication_Profile)loadedObj;
                }
                if (SelectedParameters.Contains(Params.ParamTimeBaseEvent))
                {
                    loadedObj = XMLParamsProcessor.Load_TBEsShowError(fileURL);
                    if (loadedObj != null)
                    {
                        TBE tbe_obj = (TBE)loadedObj;
                        Param_TimeBaseEvents TBE1 = new Param_TimeBaseEvents();
                        Param_TimeBaseEvents TBE2 = new Param_TimeBaseEvents();
                        ///Syn TimeBaseEvent Param Object
                        #region ///here sync ParamTimeBaseEvent Object

                        ///Sync to ParamTimeBaseEvent1
                        TBE1.Control_Enum = tbe_obj.ControlEnum_Tbe1;
                        TBE1.Interval = tbe_obj.Tbe1_interval;
                        TBE1.DateTime = tbe_obj.Tbe1_datetime;
                        ///Sync ParamTimeBaseEvent2
                        TBE2.Control_Enum = tbe_obj.ControlEnum_Tbe2;
                        TBE2.Interval = tbe_obj.Tbe2_interval;
                        TBE2.DateTime = tbe_obj.Tbe2_datetime;

                        #endregion
                        paramterConfigurationSet.ParamTimeBaseEvent_1 = TBE1;
                        paramterConfigurationSet.ParamTimeBaseEvent_2 = TBE2;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamMajorAlarmProfile))
                {
                    loadedObj = XMLParamsProcessor.Load_MajorAlarmShowError(fileURL, currentMeterInfo);

                    //temporary commented
                    //paramterConfigurationSet.ParamMajorAlarmProfile = (Param_MajorAlarmProfile)loadedObj;

                    if (loadedObj != null)
                    {
                        paramterConfigurationSet.ParamMajorAlarmProfile = (Param_MajorAlarmProfile)loadedObj;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamEventsCaution))
                {
                    loadedObjs = XMLParamsProcessor.Load_EventsCautionsShowError(fileURL);
                    if (loadedObjs != null && loadedObjs.Length > 0)
                    {
                        paramterConfigurationSet.ParamEventsCaution = new List<Param_EventsCaution>((Param_EventsCaution[])loadedObjs);
                    }
                }
                if (SelectedParameters.Contains(Params.ParamDisplayWindowPowerDown))
                {
                    loadedObj = XMLParamsProcessor.Load_DisplayPowerDownShowError(fileURL);
                    if (loadedObj != null)
                    {
                        paramterConfigurationSet.ParamDisplayPowerDown = (Param_Display_PowerDown)loadedObj;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamGeneralProcess))
                {
                    loadedObj = XMLParamsProcessor.Load_GeneralProcessShowError(fileURL);
                    if (loadedObj != null)
                    {
                        paramterConfigurationSet.ParamGeneralProcess = (Param_Generel_Process)loadedObj;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamLoadShedding))
                {
                    loadedObj = XMLParamsProcessor.Load_LoadSheddingShowError(fileURL);
                    if (loadedObj != null)
                    {
                        paramterConfigurationSet.ParamLoadShedding = (Param_Load_Scheduling)loadedObj;
                    }
                }
                //TODO: LoadParamsFromFile  -> check existance of param
                if (SelectedParameters.Contains(Params.ParamEnergyMizer))
                {
                    loadedObj = XMLParamsProcessor.Load_EnergyMizerShowError(fileURL);
                    if (loadedObj != null)
                    {
                        paramterConfigurationSet.ParamEnergyMizer = (Param_Energy_Mizer)loadedObj;
                    }
                }
                if (SelectedParameters.Contains(Params.ParamGeneratorStart))
                {
                    loadedObj = XMLParamsProcessor.Load_GeneratorStartShowError(fileURL);
                    if (loadedObj != null)
                    {
                        paramterConfigurationSet.ParamGeneratorStart = (Param_Generator_Start)loadedObj;
                    }
                }

                return paramterConfigurationSet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Exporting Parameterization Configuration file, {0}", ex.Message), ex);
            }
        }

        public static ParamConfigurationSet Import_AllParams(string fileURL,
            ParamConfigurationSet paramterConfigurationSet = null,
            List<Params> SelectedParameters = null,
            List<ParamsCategory> SelectedParamsCategory = null)
        {
            List<IParam> ParamList = null;
            try
            {

                ///FolderBrowserDialog Folder = new FolderBrowserDialog();
                ///Folder.RootFolder = Environment.SpecialFolder.Desktop;
                ///Folder.ShowDialog();
                ///string Dir = Folder.SelectedPath;
                ///Create fileURL if not Exists
                //if (!new DirectoryInfo(Dir).Exists)
                // 
                //if (!String.IsNullOrEmpty(Dir) || !String.IsNullOrWhiteSpace(Dir))
                //{
                ///Load All Parameters From Single XML Source File
                ///ParamList = XMLParamsProcessor.Load_AllParameters(Dir + "\\ParameterExport\\Parameters.conf");
                if (paramterConfigurationSet == null)
                    paramterConfigurationSet = new ParamConfigurationSet();

                HeaderInfo HInfo = null;
                #region Handle_Null_ArgumentsException
                if ((SelectedParameters == null || SelectedParameters.Count <= 0) &&
                            (SelectedParamsCategory == null || SelectedParamsCategory.Count <= 0))
                    SelectedParamsCategory = new List<ParamsCategory>();
                #endregion
                if (SelectedParameters == null)
                    SelectedParameters = new List<Params>();
                #region ///Process ParamsCategory
                foreach (var category in SelectedParamsCategory)
                {
                    Params[] paramList = ConfigFileHelper.GetParamsByCategory(category);
                    foreach (var selParam in paramList)
                    {
                        if (!SelectedParameters.Contains(selParam))
                            SelectedParameters.Add(selParam);
                    }
                }
                SelectedParameters.Sort();
                #endregion
                ParamList = XMLParamsProcessor.Load_AllParameters(fileURL);
                List<IParam> SubParamList = null;
                if (ParamList.Count > 0)
                    HInfo = (HeaderInfo)ParamList[0];
                ///Process SelectedParameters List here
                paramterConfigurationSet = Import_AllParams(HInfo.ParamList, ParamList, paramterConfigurationSet);
                return paramterConfigurationSet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Exporting Parameterization Configuration file,{0}", ex.Message), ex);
            }
        }

        public static ParamConfigurationSet Import_AllParams(MemoryStream InStream,
            ParamConfigurationSet paramterConfigurationSet = null,
            List<Params> SelectedParameters = null,
            List<ParamsCategory> SelectedParamsCategory = null)
        {
            List<IParam> ParamList = null;
            try
            {
                if (paramterConfigurationSet == null)
                    paramterConfigurationSet = new ParamConfigurationSet();

                HeaderInfo HInfo = null;
                #region Handle_Null_ArgumentsException
                if ((SelectedParameters == null || SelectedParameters.Count <= 0) &&
                            (SelectedParamsCategory == null || SelectedParamsCategory.Count <= 0))
                    SelectedParamsCategory = new List<ParamsCategory>();
                #endregion
                if (SelectedParameters == null)
                    SelectedParameters = new List<Params>();
                #region ///Process ParamsCategory
                foreach (var category in SelectedParamsCategory)
                {
                    Params[] paramList = ConfigFileHelper.GetParamsByCategory(category);
                    foreach (var selParam in paramList)
                    {
                        if (!SelectedParameters.Contains(selParam))
                            SelectedParameters.Add(selParam);
                    }
                }
                SelectedParameters.Sort();
                #endregion
                ParamList = XMLParamsProcessor.Load_AllParameters_Helper(InStream);
                List<IParam> SubParamList = null;
                if (ParamList.Count > 0)
                    HInfo = (HeaderInfo)ParamList[0];
                ///Process SelectedParameters List here
                paramterConfigurationSet = Import_AllParams(HInfo.ParamList, ParamList, paramterConfigurationSet);
                return paramterConfigurationSet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Import Parameterization Configuration Set,{0}", ex.Message), ex);
            }
        }

        public static ParamConfigurationSet Import_AllParams(List<Params> SelectedParameters,
            List<IParam> paramList,
            ParamConfigurationSet paramterConfigurationSet = null)
        {
            List<IParam> SubParamList = null;
            try
            {
                if (paramterConfigurationSet == null)
                    paramterConfigurationSet = new ParamConfigurationSet();
                if (SelectedParameters == null || SelectedParameters.Count <= 0)
                    throw new ArgumentNullException("SelectedParameters", "Invalid argument SelectedParameters");
                if (paramList == null || paramList.Count <= 0)
                    throw new ArgumentNullException("paramList", "Invalid argument paramList");

                #region Process_Meter_Parameters

                #region //ParamMonitoringTime

                if (SelectedParameters.Contains(Params.ParamMonitoringTime))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamMonitoringTime);
                        //Verify Param_Monitoring_Time here
                        paramterConfigurationSet.ParamMonitoringTime = (Param_Monitoring_Time)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import ParamMonitoringTime");
                    }
                }

                #endregion
                #region //ParamLimits

                if (SelectedParameters.Contains(Params.ParamLimits))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamLimits);
                        //Verify Param_Limits here
                        paramterConfigurationSet.ParamLimits = (Param_Limits)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Limits");
                    }
                }

                #endregion
                #region //ParamTariffication

                if (SelectedParameters.Contains(Params.ParamTariffication))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamTariffication);
                        //Verify Param_ActivityCalendar here
                        paramterConfigurationSet.ParamTariffication = (Param_ActivityCalendar)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_ActivityCalendar");
                    }
                }

                #endregion
                #region Param_MDI_parameters_object

                if (SelectedParameters.Contains(Params.ParamMDI))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamMDI);
                        //Verify Param_MDI_parameters_object here
                        paramterConfigurationSet.ParamMDI = (Param_MDI_parameters)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_MDI_parameters_object");
                    }
                }

                #endregion
                #region ParamLoadProfileChannelInfo

                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamLoadProfileChannelInfo);
                        //List<LoadProfileChannelInfo> chList = new List<LoadProfileChannelInfo>();

                        //foreach (LoadProfileChannelInfo item in SubParamList)
                        //{
                        //    if (SubParamList != null && item.Scheme == LoadProfileScheme.Scheme_1)
                        //    {
                        //        chList.Add(item);
                        //    }
                        //}
                        paramterConfigurationSet.ParamLoadProfileChannelInfo = SubParamList.Cast<LoadProfileChannelInfo>().ToList().FindAll(x=>x.Scheme==LoadProfileScheme.Scheme_1);
                    }
                    catch
                    {
                        throw new Exception("Unable to Import LoadProfileChannelsInfo");
                    }
                }

                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo_2))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamLoadProfileChannelInfo_2);

                        //foreach (LoadProfileChannelInfo item in SubParamList)
                        //{
                        //    if (SubParamList != null && item.Scheme == LoadProfileScheme.Scheme_2)
                        //    {
                        //        paramterConfigurationSet.ParamLoadProfileChannelInfo_2.Add(item);
                        //    }
                        //    //paramterConfigurationSet.ParamLoadProfileChannelInfo_2 = SubParamList.Cast<LoadProfileChannelInfo>().ToList();
                        //}
                        paramterConfigurationSet.ParamLoadProfileChannelInfo_2 = SubParamList.Cast<LoadProfileChannelInfo>().ToList().FindAll(x => x.Scheme == LoadProfileScheme.Scheme_2);
                    }
                    catch
                    {
                        throw new Exception("Unable to Import LoadProfileChannelsInfo_2");
                    }
                }

                #endregion
                #region ParamDisplayWindows

                if (SelectedParameters.Contains(Params.ParamDisplayWindowsNormal))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamDisplayWindowsNormal);
                        //Verify Param_DisplayWindowsNormal here
                        foreach (DisplayWindows dispWin in SubParamList)
                        {
                            if (dispWin != null && dispWin.WindowsMode == DispalyWindowsModes.Normal)
                            {
                                paramterConfigurationSet.ParamDisplayWindowsNormal = dispWin;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_DisplayWindowsNormal");
                    }
                }
                if (SelectedParameters.Contains(Params.ParamDisplayWindowsAlternate))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamDisplayWindowsAlternate);
                        //Verify Param_DisplayWindowsAlternate here
                        foreach (DisplayWindows dispWin in SubParamList)
                        {
                            if (dispWin != null && dispWin.WindowsMode == DispalyWindowsModes.Alternate)
                            {
                                paramterConfigurationSet.ParamDisplayWindowsAlternate = dispWin;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_DisplayWindowsAlternate");
                    }
                }
                if (SelectedParameters.Contains(Params.ParamDisplayWindowsTestMode))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamDisplayWindowsTestMode);
                        //Verify Param_DisplayWindowstest here
                        foreach (DisplayWindows dispWin in SubParamList)
                        {
                            if (dispWin != null && dispWin.WindowsMode == DispalyWindowsModes.Test)
                            {
                                paramterConfigurationSet.ParamDisplayWindowsTestMode = dispWin;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_DisplayWindowstest");
                    }
                }

                #endregion
                #region ParamCTPTRatio

                if (SelectedParameters.Contains(Params.ParamCTPTRatio))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamCTPTRatio);
                        //Verify Param_CTPT_ratio_object here
                        paramterConfigurationSet.ParamCTPTRatio = (Param_CTPT_Ratio)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_CTPT_ratio_object");
                    }
                }

                #endregion
                #region ParamDecimalPoint

                if (SelectedParameters.Contains(Params.ParamDecimalPoint))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamDecimalPoint);
                        //Verify Param_decimal_point_object here
                        paramterConfigurationSet.ParamDecimalPoint = (Param_Decimal_Point)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Decimal_Point");
                    }
                }

                #endregion
                #region Param_password_object

                if (SelectedParameters.Contains(Params.ParamPassword))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamPassword);
                        //Verify param_password_object here
                        paramterConfigurationSet.ParamPassword = (Param_Password)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_password_object");
                    }
                }

                #endregion
                #region ParamCustomerReferenceCode

                if (SelectedParameters.Contains(Params.ParamCustomerReferenceCode))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamCustomerReferenceCode);
                        //Verify param_password_object here
                        paramterConfigurationSet.ParamCustomerReferenceCode = (Param_Customer_Code)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Customer_Code");
                    }
                }

                #endregion
                #region ParamEnergy

                if (SelectedParameters.Contains(Params.ParamEnergy))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamEnergy);
                        //Verify Param_energy_parameters_object here
                        paramterConfigurationSet.ParamEnergy = (Param_Energy_Parameter)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_energy_parameters_object");
                    }
                }

                #endregion
                #region ParamClockCalib

                if (SelectedParameters.Contains(Params.ParamClockCalib))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamClockCalib);
                        //Verify Param_clock_caliberation_object here
                        paramterConfigurationSet.ParamClockCalib = (Param_Clock_Caliberation)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_clock_caliberation_object");
                    }
                }

                #endregion
                #region ParamContactor

                if (SelectedParameters.Contains(Params.ParamContactor))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamContactor);
                        //Verify Param_Contactor here
                        paramterConfigurationSet.ParamContactor = (Param_Contactor)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Contactor");
                    }
                }

                #endregion
                #region Param_TimeBaseEvents

                if (SelectedParameters.Contains(Params.ParamTimeBaseEvent))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamTimeBaseEvent);
                        //Verify ParamTimeBaseEvent here
                        paramterConfigurationSet.ParamTimeBaseEvent_1 = (Param_TimeBaseEvents)SubParamList[0];
                        paramterConfigurationSet.ParamTimeBaseEvent_2 = (Param_TimeBaseEvents)SubParamList[1];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import ParamTimeBaseEvent");
                    }
                }
                if (SelectedParameters.Contains(Params.ParamTBPowerFail))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamTBPowerFail);
                        //Verify ParamTBPowerFail here
                        paramterConfigurationSet.ParamTBPowerFail = (TBE_PowerFail)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import obj_TBE_PowerFail");
                    }
                }

                #endregion
                ///Param_MajorAlarm Parameterization
                #region ParamMajorAlarmProfile

                if (SelectedParameters.Contains(Params.ParamMajorAlarmProfile))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamMajorAlarmProfile);
                        //Verify Param_MajorAlarm here
                        paramterConfigurationSet.ParamMajorAlarmProfile = (Param_MajorAlarmProfile)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import ParamMajorAlarmProfile");
                    }
                }

                #endregion
                #region ParamEventsCaution

                if (SelectedParameters.Contains(Params.ParamEventsCaution))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamEventsCaution);
                        //Verify ParamTimeBaseEvent here
                        paramterConfigurationSet.ParamEventsCaution = SubParamList.Cast<Param_EventsCaution>().ToList();
                    }
                    catch
                    {
                        throw new Exception("Unable to Import ParamEventsCaution");
                    }
                }

                #endregion
                #region ParamDisplayPowerDown

                if (SelectedParameters.Contains(Params.ParamDisplayWindowPowerDown))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamDisplayWindowPowerDown);
                        //Verify ParamTimeBaseEvent here
                        paramterConfigurationSet.ParamDisplayPowerDown = (Param_Display_PowerDown)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Display_PowerDown");
                    }
                }

                #endregion
                #region ParamGeneralProcess

                if (SelectedParameters.Contains(Params.ParamGeneralProcess))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamGeneralProcess);
                        //Verify ParamTimeBaseEvent here
                        paramterConfigurationSet.ParamGeneralProcess = (Param_Generel_Process)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Generel_Process");
                    }
                }

                #endregion
                #endregion
                #region Process_ModemParameters

                //Param_IP_Profiles_object 
                Param_IP_Profiles[] Param_IP_Profiles_object =
                    Param_IP_ProfilesHelper.Param_IP_Profiles_initailze(Param_IP_ProfilesHelper.Max_IP_Profile);
                //Param_Wakeup_Profile_object
                Param_WakeUp_Profile[] Param_Wakeup_Profile_object =
                    Param_WakeUp_ProfileHelper.Param_Wakeup_Profile_object_initialze(Param_WakeUp_ProfileHelper.Max_WakeUp_Profile);
                //Param_Number_Profile_object
                Param_Number_Profile[] Param_Number_Profile_object =
                    Param_Number_ProfileHelper.Param_Number_Profile_object_initialze(Param_Number_ProfileHelper.Max_Number_Profile + 1);

                #region //ParamIPProfiles

                if (SelectedParameters.Contains(Params.ParamIPProfiles))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamIPProfiles);
                        //Verify Param_IP_Profiles here
                        if (SubParamList != null && SubParamList.Count > 0 &&
                            SubParamList[0].GetType() == typeof(Param_IP_Profiles))
                        {
                            //Param_IP_Profiles_object = new Param_IP_Profiles[SubParamList.Count];
                            int count = 0;
                            foreach (var prmIPProfile in SubParamList)
                            {
                                Param_IP_Profiles_object[count++] = (Param_IP_Profiles)prmIPProfile;
                            }
                        }
                        else
                            throw new Exception("Unable to Import Param_IP_Profiles_object");
                        paramterConfigurationSet.ParamIPProfiles = Param_IP_Profiles_object;
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_IP_Profiles_object");
                    }
                }
                else if (paramterConfigurationSet.ParamIPProfiles == null ||
                    paramterConfigurationSet.ParamIPProfiles.Length < Param_IP_ProfilesHelper.Max_IP_Profile)
                    paramterConfigurationSet.ParamIPProfiles = Param_IP_Profiles_object;

                #endregion
                #region //ParamWakeUpProfile

                if (SelectedParameters.Contains(Params.ParamWakeUpProfile))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamWakeUpProfile);
                        //Verify ParamWakeUpProfile here
                        if (SubParamList != null && SubParamList.Count > 0 &&
                            SubParamList[0].GetType() == typeof(Param_WakeUp_Profile))
                        {
                            //Param_Wakeup_Profile_object = new Param_WakeUp_Profile[SubParamList.Count];
                            int count = 0;
                            foreach (var prmWkProfile in SubParamList)
                            {
                                Param_Wakeup_Profile_object[count++] = (Param_WakeUp_Profile)prmWkProfile;
                            }
                        }
                        else
                            throw new Exception("Unable to Import Param_Wakeup_Profile_object");
                        paramterConfigurationSet.ParamWakeUpProfile = Param_Wakeup_Profile_object;
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Wakeup_Profile_object");
                    }
                }
                else if (paramterConfigurationSet.ParamWakeUpProfile == null ||
                    paramterConfigurationSet.ParamWakeUpProfile.Length < Param_WakeUp_ProfileHelper.Max_WakeUp_Profile)
                    paramterConfigurationSet.ParamWakeUpProfile = Param_Wakeup_Profile_object;

                #endregion
                #region //ParamNumberProfile

                if (SelectedParameters.Contains(Params.ParamNumberProfile))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamNumberProfile);
                        //Verify Param_Number_Profile here
                        if (SubParamList != null && SubParamList.Count > 0 &&
                            SubParamList[0].GetType() == typeof(Param_Number_Profile))
                        {
                            //Param_Number_Profile_object = new Param_Number_Profile[SubParamList.Count];
                            int count = 0;
                            foreach (var prmNumProfile in SubParamList)
                            {
                                Param_Number_Profile_object[count++] = (Param_Number_Profile)prmNumProfile;
                            }
                        }
                        else
                            throw new Exception("Unable to Import Param_Number_Profile_object");
                        paramterConfigurationSet.ParamNumberProfile = Param_Number_Profile_object;
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Number_Profile_object");
                    }
                }
                else if (paramterConfigurationSet.ParamNumberProfile == null ||
                    paramterConfigurationSet.ParamNumberProfile.Length < Param_Number_ProfileHelper.Max_Number_Profile + 1)
                    paramterConfigurationSet.ParamNumberProfile = Param_Number_Profile_object;

                #endregion
                #region //Param_Communication_Profile_object

                if (SelectedParameters.Contains(Params.ParamCommunicationProfile))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamCommunicationProfile);
                        //Verify Param_Communication_Profile here
                        paramterConfigurationSet.ParamCommunicationProfile = (Param_Communication_Profile)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Communication_Profile_object");
                    }
                }

                #endregion
                #region //ParamKeepAliveIP

                if (SelectedParameters.Contains(Params.ParamKeepAliveIP))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamKeepAliveIP);
                        //Verify Param_Keep_Alive_IP here
                        paramterConfigurationSet.ParamKeepAliveIP = (Param_Keep_Alive_IP)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Keep_Alive_IP");
                    }
                }

                #endregion
                #region //Param_ModemLimitsAndTime_Object

                if (SelectedParameters.Contains(Params.ParamModemLimitsAndTime))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamModemLimitsAndTime);
                        //Verify Param_ModemLimitsAndTime_Object here
                        paramterConfigurationSet.ParamModemLimitsAndTime = (Param_ModemLimitsAndTime)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_ModemLimitsAndTime");
                    }
                }

                #endregion
                #region //ParamModemInitialize

                if (SelectedParameters.Contains(Params.ParamModemInitialize))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamModemInitialize);
                        //Verify ParamModemInitialize here
                        paramterConfigurationSet.ParamModemInitialize = (Param_Modem_Initialize)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_Modem_Initialize_Object");
                    }
                }

                #endregion
                #region //ParamModemBasicsNEW

                if (SelectedParameters.Contains(Params.ParamModemBasicsNEW))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamModemBasicsNEW);
                        //Verify Param_ModemBasics_NEW_object here
                        paramterConfigurationSet.ParamModemBasicsNEW = (Param_ModemBasics_NEW)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_ModemBasics_NEW");
                    }
                }

                #endregion
                #region //ParamTCPUDP

                if (SelectedParameters.Contains(Params.ParamTCPUDP))
                {
                    try
                    {
                        SubParamList = ConfigFileHelper.SelectByParam(paramList, Params.ParamTCPUDP);
                        //Verify Param_TCP_UDP_object here
                        paramterConfigurationSet.ParamTCPUDP = (Param_TCP_UDP)SubParamList[0];
                    }
                    catch
                    {
                        throw new Exception("Unable to Import Param_TCP_UDP_object");
                    }
                }

                #endregion

                #endregion
                #region MISC

                /////Process Param_IPV4_Object
                //IParam tObject = null;
                //tObject = paramList.Find((x) => x.GetType() == typeof(Param_IPV4));
                //if (tObject != null)
                //    Param_IPV4_object = (Param_IPV4)tObject;
                /////Process Param_ErrorDetail
                //tObject = paramList.Find((x) => x.GetType() == typeof(Param_ErrorDetail));
                //if (tObject != null)
                //    Param_Error_Details = (Param_ErrorDetail)tObject;

                #endregion
                //}
                //else
                //    throw new Exception("Profile Loading Unsuccessful,Enter valid Path");
                return paramterConfigurationSet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Exporting Parameterization Configuration file,{0}", ex.Message), ex);
            }
        }

        #endregion

        #region Export_AllParams


        public static void Export_AllParams(string fileURL, string currentMeterInfo,
            ParamConfigurationSet paramterConfigurationSet = null,
            List<Params> SelectedParameters = null,
            List<ParamsCategory> SelectedParamsCategory = null)
        {
            try
            {
                #region Commented_CodeSection
                ///FolderBrowserDialog Folder = new FolderBrowserDialog();
                ///Folder.RootFolder = Environment.SpecialFolder.Desktop;
                ///Folder.ShowDialog();
                ///string Dir = Folder.SelectedPath;
                ///Create directory if not Exists
                //if (!new DirectoryInfo(Dir).Exists)
                // 
                //if (!String.IsNullOrEmpty(Dir) || !String.IsNullOrWhiteSpace(Dir))
                //{
                ///Load All Parameters From Single XML Source File
                ///ParamList = XMLParamsProcessor.Load_AllParameters(Dir + "\\ParameterExport\\Parameters.conf"); 
                #endregion
                ///Validate Function Arguments
                ///Validate ParamConfigurationSet
                if (paramterConfigurationSet == null)
                    new ArgumentNullException("paramterConfigurationSet", "Invalid paramterConfigurationSet argument");
                if (String.IsNullOrEmpty(fileURL) || String.IsNullOrWhiteSpace(fileURL))
                    new ArgumentNullException("fileURL", "Invalid Directory URL argument");
                ///Create directory if not Exists
                DirectoryInfo dir = new DirectoryInfo(fileURL);
                if (!dir.Exists)
                    dir.Create();
                #region Handle_Null_ArgumentsException

                if ((SelectedParameters == null || SelectedParameters.Count <= 0) &&
                   (SelectedParamsCategory == null || SelectedParamsCategory.Count <= 0))
                {
                    var AllCategories = Enum.GetValues(typeof(ParamsCategory));
                    SelectedParamsCategory = AllCategories.Cast<ParamsCategory>().ToList();
                }

                #endregion
                if (SelectedParameters == null)
                    SelectedParameters = new List<Params>();
                #region ///Process ParamsCategory
                foreach (var category in SelectedParamsCategory)
                {
                    Params[] paramList = ConfigFileHelper.GetParamsByCategory(category);
                    foreach (var selParam in paramList)
                    {
                        if (!SelectedParameters.Contains(selParam))
                            SelectedParameters.Add(selParam);
                    }
                }
                SelectedParameters.Sort();
                #endregion
                ///Append Notes
                string notes = "\r\n" + fileURL + "   Created ON :   " + DateTime.Now.ToString();
                File.AppendAllText(fileURL + "\\.AOS", notes); //.SEC7 replaced with .AOS for Accurate //by Azeem Inayat
                #region ParamMetersProfiles

                if (SelectedParameters.Contains(Params.ParamCTPTRatio))
                {
                    XMLParamsProcessor.save_CTPTShowError(fileURL, paramterConfigurationSet.ParamCTPTRatio);
                }
                if (SelectedParameters.Contains(Params.ParamDecimalPoint))
                {
                    XMLParamsProcessor.save_DecimalPointShowError(fileURL, paramterConfigurationSet.ParamDecimalPoint);
                }
                if (SelectedParameters.Contains(Params.ParamMonitoringTime))
                {
                    XMLParamsProcessor.Save_MonitoringTimeShowError(fileURL, paramterConfigurationSet.ParamMonitoringTime);
                }
                if (SelectedParameters.Contains(Params.ParamLimits))
                {
                    XMLParamsProcessor.Save_LimitsShowError(fileURL, currentMeterInfo, paramterConfigurationSet.ParamLimits);
                }
                if (SelectedParameters.Contains(Params.ParamLimits))
                {
                    XMLParamsProcessor.Save_LimitsShowError(fileURL, currentMeterInfo, paramterConfigurationSet.ParamLimits);
                    ///Save Params Limits_Param_Limit_Demand_OverLoad
                    ///Param_Limit_Demand_OverLoad[] paramloadedObjs = new Param_Limit_Demand_OverLoad[04];
                    ///paramloadedObjs[0] = Param_Limit_Demand_OverLoad_T1;
                    ///paramloadedObjs[1] = Param_Limit_Demand_OverLoad_T2;
                    ///paramloadedObjs[2] = Param_Limit_Demand_OverLoad_T3;
                    ///paramloadedObjs[3] = Param_Limit_Demand_OverLoad_T4;
                    ///XMLParamsProcessor.SaveAll_Limits_Param_Limit_Demand_OverLoadShowError(fileURL, currentMeterInfo,
                    ///(Param_Limit_Demand_OverLoad[])paramloadedObjs);
                }
                if (SelectedParameters.Contains(Params.ParamDisplayWindowsNormal) ||
                    SelectedParameters.Contains(Params.ParamDisplayWindowsAlternate) ||
                    SelectedParameters.Contains(Params.ParamDisplayWindowsTestMode))
                {
                    ///Save Display Windows
                    DisplayWindows[] paramloadedObjs = new DisplayWindows[03];
                    paramloadedObjs[XMLParamsProcessor.Param_DisplayWindowsNormal] = paramterConfigurationSet.ParamDisplayWindowsNormal;
                    paramloadedObjs[XMLParamsProcessor.Param_DisplayWindowsAlternate] = paramterConfigurationSet.ParamDisplayWindowsAlternate;
                    paramloadedObjs[XMLParamsProcessor.Param_DisplayWindowstest] = paramterConfigurationSet.ParamDisplayWindowsTestMode;
                    XMLParamsProcessor.Save_DisplayWindowsShowError(fileURL, (DisplayWindows[])paramloadedObjs);
                }
                if (SelectedParameters.Contains(Params.ParamCustomerReferenceCode))
                {
                    XMLParamsProcessor.Save_CustomerReferenceShowError(fileURL,
                        paramterConfigurationSet.ParamCustomerReferenceCode);
                }
                if (SelectedParameters.Contains(Params.ParamPassword))
                {
                    XMLParamsProcessor.Save_ParamPasswordsShowError(fileURL,
                        paramterConfigurationSet.ParamPassword);
                }
                if (SelectedParameters.Contains(Params.ParamClockCalib))
                {
                    XMLParamsProcessor.Save_ClockShowError(fileURL, paramterConfigurationSet.ParamClockCalib);
                }
                if (SelectedParameters.Contains(Params.ParamEnergy))
                {
                    XMLParamsProcessor.Save_EnergyParamsShowError(fileURL, paramterConfigurationSet.ParamEnergy);
                }
                if (SelectedParameters.Contains(Params.ParamMDI))
                {
                    XMLParamsProcessor.Save_MDIParamsShowError(fileURL, paramterConfigurationSet.ParamMDI);
                }
                if (SelectedParameters.Contains(Params.ParamContactor))
                {
                    XMLParamsProcessor.Save_ContactorShowError(fileURL, paramterConfigurationSet.ParamContactor);
                }
                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo))
                {
                    ///Save Load Profile Channels Info Scheme 1
                    LoadProfileChannelInfo[] paramloadedObjs = paramterConfigurationSet.ParamLoadProfileChannelInfo.ToArray();
                    if (paramloadedObjs != null && paramloadedObjs.Length > 0)
                    {
                        XMLParamsProcessor.Save_LoadProfileShowError(fileURL, paramloadedObjs, LoadProfileScheme.Scheme_1);
                    }
                }

                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo_2))
                {
                    ///Save Load Profile Channels Info Scheme 2
                    LoadProfileChannelInfo[] paramloadedObjs = paramterConfigurationSet.ParamLoadProfileChannelInfo_2.ToArray();
                    if (paramloadedObjs != null && paramloadedObjs.Length > 0)
                    {
                        XMLParamsProcessor.Save_LoadProfileShowError(fileURL, paramloadedObjs, LoadProfileScheme.Scheme_2);
                    }
                }

                if (SelectedParameters.Contains(Params.ParamTariffication))
                {
                    ///Save Param_Activity_Calendar
                    XMLParamsProcessor.Save_ActivityCalendarShowError(fileURL, paramterConfigurationSet.ParamTariffication);
                }


                #endregion
                if (SelectedParameters.Contains(Params.ParamTimeBaseEvent))
                {
                    #region ///here sync from ParamTimeBaseEvent Object
                    TBE tbe_obj = new TBE();
                    Param_TimeBaseEvents TBE1 = null, TBE2 = null;
                    TBE1 = paramterConfigurationSet.ParamTimeBaseEvent_1;
                    TBE2 = paramterConfigurationSet.ParamTimeBaseEvent_2;
                    ///Sync ParamTimeBaseEvent1
                    tbe_obj.ControlEnum_Tbe1 = TBE1.Control_Enum;
                    tbe_obj.Tbe1_interval = TBE1.Interval;
                    tbe_obj.Tbe1_datetime = TBE1.DateTime;
                    ///Sync ParamTimeBaseEvent2
                    tbe_obj.ControlEnum_Tbe2 = TBE2.Control_Enum;
                    tbe_obj.Tbe2_interval = TBE2.Interval;
                    tbe_obj.Tbe2_datetime = TBE2.DateTime;
                    #endregion
                    XMLParamsProcessor.Save_TOFILE_TBEsShowError(fileURL, tbe_obj);
                }
                #region ParamMajorAlarmProfile

                if (SelectedParameters.Contains(Params.ParamMajorAlarmProfile))
                {
                    XMLParamsProcessor.Save_MajorAlarmShowError(fileURL, currentMeterInfo, paramterConfigurationSet.ParamMajorAlarmProfile);
                }
                if (SelectedParameters.Contains(Params.ParamEventsCaution))
                {
                    XMLParamsProcessor.Save_EventsCautionsShowError(fileURL, paramterConfigurationSet.ParamEventsCaution.ToArray());
                }

                #endregion

                #region ParamLoadShedding

                if (SelectedParameters.Contains(Params.ParamLoadShedding))
                {
                    XMLParamsProcessor.Save_LoadSheddingShowError(fileURL, paramterConfigurationSet.ParamLoadShedding);
                }

                #endregion
                
                //TODO: SaveToFile 06 add Existing check
                #region ParamEnergyMizer

                if (SelectedParameters.Contains(Params.ParamEnergyMizer))
                {
                    XMLParamsProcessor.Save_EnergyMizerShowError(fileURL, paramterConfigurationSet.ParamEnergyMizer);
                }
                #endregion

                #region ParamGeneratorStart

                if (SelectedParameters.Contains(Params.ParamGeneratorStart))
                {
                    XMLParamsProcessor.Save_GeneratorStartShowError(fileURL, paramterConfigurationSet.ParamGeneratorStart);
                }

                #endregion

                #region ParamDisplayWindowPowerDown

                if (SelectedParameters.Contains(Params.ParamDisplayWindowPowerDown))
                {
                    XMLParamsProcessor.Save_DisplayPowerDownShowError(fileURL, paramterConfigurationSet.ParamDisplayPowerDown);
                }

                #endregion

                #region ParamGeneralProcess

                if (SelectedParameters.Contains(Params.ParamGeneralProcess))
                {
                    XMLParamsProcessor.Save_GeneralProcessShowError(fileURL, paramterConfigurationSet.ParamGeneralProcess);
                }

                #endregion

                #region ///Save Modem Parameters

                ///Save Param_IP_Profiles
                if (SelectedParameters.Contains(Params.ParamIPProfiles))
                {
                    XMLParamsProcessor.Save_IP_profileShowError(fileURL, paramterConfigurationSet.ParamIPProfiles);
                }
                if (SelectedParameters.Contains(Params.ParamStandardIPProfile))
                {
                    XMLParamsProcessor.Save_Standard_IP_profileShowError(fileURL, paramterConfigurationSet.ParamStandardIPProfiles);
                }
                if (SelectedParameters.Contains(Params.ParamNumberProfile))
                {
                    XMLParamsProcessor.Save_NumberProfileShowError(fileURL, paramterConfigurationSet.ParamNumberProfile);
                }
                if (SelectedParameters.Contains(Params.ParamStandardNumberProfile))
                {
                    XMLParamsProcessor.Save_Standard_NumberProfileShowError(fileURL, paramterConfigurationSet.ParamStandardNumberProfile);
                }
                if (SelectedParameters.Contains(Params.ParamCommunicationProfile))
                {
                    XMLParamsProcessor.Save_CommProfile(fileURL, paramterConfigurationSet.ParamCommunicationProfile);
                }
                if (SelectedParameters.Contains(Params.ParamModemInitialize))
                {
                    XMLParamsProcessor.Save_Modem_InitializeShowError(fileURL, paramterConfigurationSet.ParamModemInitialize);
                }
                if (SelectedParameters.Contains(Params.ParamModemBasicsNEW))
                {
                    XMLParamsProcessor.Save_Modem_InitializeShowError(fileURL, paramterConfigurationSet.ParamModemBasicsNEW);
                }
                if (SelectedParameters.Contains(Params.ParamWakeUpProfile))
                {
                    XMLParamsProcessor.Save_WakeUpProfileShowError(fileURL, paramterConfigurationSet.ParamWakeUpProfile);
                }
                if (SelectedParameters.Contains(Params.ParamModemLimitsAndTime))
                {
                    XMLParamsProcessor.Save_ModemLimitsAndTimeShowError(fileURL, paramterConfigurationSet.ParamModemLimitsAndTime);
                }
                if (SelectedParameters.Contains(Params.ParamKeepAliveIP))
                {
                    XMLParamsProcessor.Save_KeepAliveShowError(fileURL, paramterConfigurationSet.ParamKeepAliveIP);
                }
                //if (SelectedParameters.Contains(Params.ParamIPV4))
                //{
                //    XMLParamsProcessor.Save_IPV4ShowError(fileURL, Param_IPV4_object);
                //} 

                #endregion

                if (SelectedParameters.Contains(Params.ParamStatausWordMap))
                {
                    ///Save Param_Activity_Calendar
                    XMLParamsProcessor.save_StatusWordMapShowError(fileURL, paramterConfigurationSet.ParamStatusWordMap);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Exporting Parameterization Configuration file,{0}", ex.Message), ex);
            }
        }
        
        /// <summary>
        /// Export_AllParams to Serialize Parameter in File Stream
        /// </summary>
        /// <param name="fileURL"></param>
        /// <param name="paramterConfigurationSet"></param>
        /// <param name="SelectedParameters"></param>
        /// <param name="SelectedParamsCategory"></param>
        public static void Export_AllParams(string fileURL,
            ParamConfigurationSet paramterConfigurationSet = null,
            List<Params> SelectedParameters = null,
            List<ParamsCategory> SelectedParamsCategory = null)
        {
            try
            {
                List<IParam> ParamList = null;
                ParamList = Export_AllParams_Helper(paramterConfigurationSet, SelectedParameters, SelectedParamsCategory);
                ///Save All Parameters To Single XML Source File
                ///XMLParamsProcessor.Save_AllParameters(Dir + "\\ParameterExport\\Parameters.conf", ParamList);
                XMLParamsProcessor.Save_AllParameters(fileURL, ParamList);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while exec Export_Parameters {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Export_AllParams to Serialize Parameter in Single Memory Stream
        /// </summary>
        /// <param name="paramterConfigurationSet"></param>
        /// <param name="SelectedParameters"></param>
        /// <param name="SelectedParamsCategory"></param>
        /// <returns></returns>
        public static MemoryStream Export_AllParams(ParamConfigurationSet paramterConfigurationSet = null,
            List<Params> SelectedParameters = null,
            List<ParamsCategory> SelectedParamsCategory = null)
        {
            try
            {
                List<IParam> ParamList = null;
                ParamList = Export_AllParams_Helper(paramterConfigurationSet, SelectedParameters, SelectedParamsCategory);
                ///Save All Parameters To Single XML Source File
                ///XMLParamsProcessor.Save_AllParameters(Dir + "\\ParameterExport\\Parameters.conf", ParamList);
                MemoryStream SerializeOutStream = new MemoryStream(1024);//1K
                try
                {
                    XMLParamsProcessor.Save_AllParametersHelper(SerializeOutStream, ParamList);
                }
                finally
                {
                    SerializeOutStream.Seek(0, SeekOrigin.Begin);
                }
                return SerializeOutStream;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while exec Export_Parameters {0}", ex.Message), ex);
            }
        }

        public static List<IParam> Export_AllParams_Helper(ParamConfigurationSet paramterConfigurationSet = null,
           List<Params> SelectedParameters = null,
           List<ParamsCategory> SelectedParamsCategory = null)
        {
            try
            {
                List<IParam> ParamList = new List<IParam>();
                #region Handle_Null_ArgumentsException

                if ((SelectedParameters == null || SelectedParameters.Count <= 0) &&
                   (SelectedParamsCategory == null || SelectedParamsCategory.Count <= 0))
                {
                    var AllCategories = Enum.GetValues(typeof(ParamsCategory));
                    SelectedParamsCategory = AllCategories.Cast<ParamsCategory>().ToList();
                }

                #endregion
                if (SelectedParameters == null)
                    SelectedParameters = new List<Params>();
                #region ///Process ParamsCategory

                foreach (var category in SelectedParamsCategory)
                {
                    Params[] paramList = ConfigFileHelper.GetParamsByCategory(category);
                    foreach (var selParam in paramList)
                    {
                        if (!SelectedParameters.Contains(selParam))
                            SelectedParameters.Add(selParam);
                    }
                }
                SelectedParameters.Sort();

                #endregion
                #region Process_Meter_Parameters
                #region ///ParamMonitoringTime
                if (SelectedParameters.Contains(Params.ParamMonitoringTime))
                    ParamList.Add(paramterConfigurationSet.ParamMonitoringTime);
                #endregion
                #region ///ParamLimitsObject
                if (SelectedParameters.Contains(Params.ParamLimits))
                {
                    ParamList.Add(paramterConfigurationSet.ParamLimits);
                    //ParamList.Add(Param_Limit_Demand_OverLoad_T1);
                    //ParamList.Add(Param_Limit_Demand_OverLoad_T2);
                    //ParamList.Add(Param_Limit_Demand_OverLoad_T3);
                    //ParamList.Add(Param_Limit_Demand_OverLoad_T4);
                }
                #endregion
                #region ///ParamTariffication
                if (SelectedParameters.Contains(Params.ParamTariffication))
                    ParamList.Add(paramterConfigurationSet.ParamTariffication);
                #endregion
                #region Param_MDI_parameters_object
                if (SelectedParameters.Contains(Params.ParamMDI))
                    ParamList.Add(paramterConfigurationSet.ParamMDI);
                #endregion
                #region ParamLoadProfilePeriod

                //if (SelectedParameters.Contains(Params.ParamLoadProfilePeriod))
                //{
                //    foreach (var lpChInfo in LoadProfileChannelsInfo)
                //    {
                //        lpChInfo.CapturePeriod = LoadProfilePeriod;
                //    }
                //}

                #endregion
                #region ParamLoadProfileChannelInfo
                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo))
                {
                    ParamList.AddRange(paramterConfigurationSet.ParamLoadProfileChannelInfo);
                }
                if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo_2))
                {
                    ParamList.AddRange(paramterConfigurationSet.ParamLoadProfileChannelInfo_2);
                }
                #endregion
                #region ParamDisplayWindows

                if (SelectedParameters.Contains(Params.ParamDisplayWindowsNormal))
                {
                    paramterConfigurationSet.ParamDisplayWindowsNormal.WindowsMode = DispalyWindowsModes.Normal;
                    ParamList.Add(paramterConfigurationSet.ParamDisplayWindowsNormal);
                }
                if (SelectedParameters.Contains(Params.ParamDisplayWindowsAlternate))
                {
                    paramterConfigurationSet.ParamDisplayWindowsAlternate.WindowsMode = DispalyWindowsModes.Alternate;
                    ParamList.Add(paramterConfigurationSet.ParamDisplayWindowsAlternate);
                }
                if (SelectedParameters.Contains(Params.ParamDisplayWindowsTestMode))
                {
                    paramterConfigurationSet.ParamDisplayWindowsTestMode.WindowsMode = DispalyWindowsModes.Test;
                    ParamList.Add(paramterConfigurationSet.ParamDisplayWindowsTestMode);
                }

                #endregion
                #region ParamCTPTRatio

                if (SelectedParameters.Contains(Params.ParamCTPTRatio))
                {
                    ParamList.Add(paramterConfigurationSet.ParamCTPTRatio);
                }

                #endregion
                #region ParamDecimalPoint

                if (SelectedParameters.Contains(Params.ParamDecimalPoint))
                {
                    ParamList.Add(paramterConfigurationSet.ParamDecimalPoint);
                }

                #endregion
                #region ParamCustomerReferenceCode

                if (SelectedParameters.Contains(Params.ParamCustomerReferenceCode))
                {
                    ParamList.Add(paramterConfigurationSet.ParamCustomerReferenceCode);
                }

                #endregion
                #region ParamPassword

                if (SelectedParameters.Contains(Params.ParamPassword))
                {
                    ParamList.Add(paramterConfigurationSet.ParamPassword);
                }

                #endregion
                #region ParamEnergy

                if (SelectedParameters.Contains(Params.ParamEnergy))
                {
                    ParamList.Add(paramterConfigurationSet.ParamEnergy);
                }

                #endregion
                #region ParamClockCalib

                if (SelectedParameters.Contains(Params.ParamClockCalib))
                {
                    ParamList.Add(paramterConfigurationSet.ParamClockCalib);
                }

                #endregion
                #region ParamClock

                //if (SelectedParameters.Contains(Params.ParamClock))
                //{
                //    ParamList.Add(paramterConfigurationSet.ParamClockCalib);
                //}

                #endregion
                #region ParamContactor

                if (SelectedParameters.Contains(Params.ParamContactor))
                {
                    ParamList.Add(paramterConfigurationSet.ParamContactor);
                }

                #endregion
                #region ParamTimeBaseEvent

                if (SelectedParameters.Contains(Params.ParamTimeBaseEvent))
                {
                    ParamList.Add(paramterConfigurationSet.ParamTimeBaseEvent_1);
                    ParamList.Add(paramterConfigurationSet.ParamTimeBaseEvent_2);
                }
                if (SelectedParameters.Contains(Params.ParamTBPowerFail))
                {
                    ParamList.Add(paramterConfigurationSet.ParamTBPowerFail);
                }

                #endregion
                #endregion
                ///Param_MajorAlarm Parameterization
                //Disable_Save_ParamMajorAlarmProfile
                #region ParamMajorAlarmProfile

                if (SelectedParameters.Contains(Params.ParamMajorAlarmProfile))// && false)
                {
                    ParamList.Add(paramterConfigurationSet.ParamMajorAlarmProfile);
                }

                #endregion
                //Disable_Save_ParamEventsCaution
                #region ParamEventsCaution

                if (SelectedParameters.Contains(Params.ParamEventsCaution))// && false)
                {
                    var ev_Cautions = Param_EventCautionHelper.Init_ParamEventCaution(paramterConfigurationSet.ParamEventsCaution.ToList());
                    ParamList.AddRange(ev_Cautions);
                }

                #endregion
                #region Process_ModemParameters
                #region ///ParamIPProfiles

                if (SelectedParameters.Contains(Params.ParamIPProfiles))
                    ParamList.AddRange(paramterConfigurationSet.ParamIPProfiles);
                #endregion
                #region ///ParamStandardIPProfiles

                if (SelectedParameters.Contains(Params.ParamStandardIPProfile))
                    ParamList.AddRange(paramterConfigurationSet.ParamStandardIPProfiles);
                #endregion
                #region ///ParamWakeUpProfile

                if (SelectedParameters.Contains(Params.ParamWakeUpProfile))
                    ParamList.AddRange(paramterConfigurationSet.ParamWakeUpProfile);
                #endregion
                #region ///ParamNumberProfile

                if (SelectedParameters.Contains(Params.ParamNumberProfile))
                    ParamList.AddRange(paramterConfigurationSet.ParamNumberProfile);
                #endregion
                #region ///ParamStandardNumberProfile

                if (SelectedParameters.Contains(Params.ParamStandardNumberProfile))
                    ParamList.AddRange(paramterConfigurationSet.ParamStandardNumberProfile);
                #endregion
                #region ///ParamCommunicationProfile

                if (SelectedParameters.Contains(Params.ParamCommunicationProfile))
                    ParamList.Add(paramterConfigurationSet.ParamCommunicationProfile);
                #endregion
                #region ///ParamKeepAliveIP

                if (SelectedParameters.Contains(Params.ParamKeepAliveIP))
                    ParamList.Add(paramterConfigurationSet.ParamKeepAliveIP);
                #endregion
                #region ///ParamModemLimitsAndTime

                if (SelectedParameters.Contains(Params.ParamModemLimitsAndTime))
                    ParamList.Add(paramterConfigurationSet.ParamModemLimitsAndTime);
                #endregion
                #region ///ParamModemInitialize

                if (SelectedParameters.Contains(Params.ParamModemInitialize))
                    ParamList.Add(paramterConfigurationSet.ParamModemInitialize);
                #endregion
                #region ///ParamModemBasicsNEW

                if (SelectedParameters.Contains(Params.ParamModemBasicsNEW))
                    ParamList.Add(paramterConfigurationSet.ParamModemBasicsNEW);
                #endregion
                #region ///ParamTCPUDP

                if (SelectedParameters.Contains(Params.ParamTCPUDP))
                    ParamList.Add(paramterConfigurationSet.ParamTCPUDP);
                #endregion
                #endregion
                #region MISC

                //ParamList.Add(Param_IPV4_object);
                //ParamList.Add(Param_Error_Details);

                #endregion
                #region ParamDisplayPowerDown

                if (SelectedParameters.Contains(Params.ParamDisplayWindowPowerDown))// && false)
                {
                    ParamList.Add(paramterConfigurationSet.ParamDisplayPowerDown);
                }

                #endregion
                #region ParamGeneralProcess

                if (SelectedParameters.Contains(Params.ParamGeneralProcess))// && false)
                {
                    ParamList.Add(paramterConfigurationSet.ParamGeneralProcess);
                }

                #endregion
                ///Remove Nullable ParamList
                for (int index = 0; index < ParamList.Count; index++)
                {
                    if (ParamList[index] == null)
                    {
                        ParamList.Remove(ParamList[index]);
                        index--;
                    }
                }
                return ParamList;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while exec Export_Parameters {0}", ex.Message), ex);
            }

        }
        #endregion
    }
}