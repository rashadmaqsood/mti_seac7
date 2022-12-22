using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SharedCode.Common
{
    public static class App_Validation
    {
        #region Data_Members_Validation_Process

        #region ///RegEx To Match IP in Decimal Format

        internal static readonly string IP_RegEx = @"^((?<F_Oct>[0-9]{1,3})\.(?<S_Oct>[0-9]{1,3})\.(?<T_Oct>[0-9]{1,3})\.(?<F_Oct>[0-9]{1,3}))$";

        #endregion

        #region ///RegEx to Match APN/URI String

        internal static readonly string URI_RegEx = @"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}$";

        #endregion

        #region ///RegEx to Match Mobile Phone Number

        internal static readonly string Intl_PhoneNumberFormat_RegEx =
                    @"^((?<IntPrefix>\+?(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|" +
                    @"42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|" +
                    @"4[987654310]|3[9643210]|2[70]|7|1))?\W*\d\W*\d\W*\d\W*\d\W*\d\W*\d\W*\d\W*\d\W*" +
                    @"(\d{1,2}\W*)?(\d{1,2}\W*)?(\d{1,2}\W*)?(\d{1,2}))$$";


        internal static readonly string Generic_PhoneNumberFormat_RegEx = @"^(?:(\+|-|00)+\d{1,4}|-)|(?:-|\d{2,4})|(-\d{3,11})|(-\d{1,4})$";

        #endregion

        #region ///RegEx to Match National Security Number

        internal static readonly string NID_RegEx = @"^(?:\d{5,10}-\d{7,10}-\d{1,5})$";

        #endregion

        #region Data_Member

        internal static readonly Regex IpRegEx;
        internal static readonly Regex URIRegEx;
        internal static readonly Regex Intl_PhoneNumberFormatRegEx;
        internal static Regex Generic_PhoneNumberFormatRegEx;
        internal static readonly Regex Generic_NIDFormatRegEx;

        #endregion

        #endregion

        static App_Validation()
        {
            try
            {
                IpRegEx = new Regex(IP_RegEx, RegexOptions.Compiled);
                URIRegEx = new Regex(URI_RegEx, RegexOptions.Compiled);
                Intl_PhoneNumberFormatRegEx = new Regex(Intl_PhoneNumberFormat_RegEx, RegexOptions.Compiled);
                Generic_NIDFormatRegEx = new Regex(NID_RegEx, RegexOptions.Compiled);
                Generic_PhoneNumberFormatRegEx = new Regex(Generic_PhoneNumberFormat_RegEx, RegexOptions.Compiled);
            }
            catch
            { }
        }

        #region Validate_Range

        public static bool Validate_Range(TimeSpan minVal, TimeSpan maxVal, TimeSpan val)
        {
            try
            {
                if (val >= minVal && val <= maxVal)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        public static bool Validate_Range(DateTime minVal, DateTime maxVal, DateTime val)
        {
            try
            {
                if (val >= minVal && val <= maxVal)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        public static bool Validate_Range(long minVal, long maxVal, long val)
        {
            try
            {
                if (val >= minVal && val <= maxVal)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        public static bool Validate_Range(int minVal, int maxVal, int val)
        {
            try
            {
                if (val >= minVal && val <= maxVal)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        public static bool Validate_Range(ushort minVal, ushort maxVal, ushort val)
        {
            try
            {
                if (val >= minVal && val <= maxVal)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        public static bool Validate_Range(double minVal, double maxVal, double val)
        {
            try
            {
                if (val >= minVal && val <= maxVal)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        #endregion

        #region TextBox_RangeValidation

        public static bool TextBox_RangeValidation(ValueType minVal, ValueType maxVal, TextBox given_TextBox, ref ErrorProvider errPro)
        {
            try
            {
                if (errPro == null)
                    errPro = new ErrorProvider();
                long text_box_valueLong = long.MinValue;
                Double text_box_valueDouble = Double.MinValue;
                bool valout_Come = false;
                App_Validation_Info Validation_Info = Commons.AppValidationInfo;
                if (!String.IsNullOrEmpty(given_TextBox.Text) &&
                    !String.IsNullOrWhiteSpace(given_TextBox.Text))
                {
                    if (minVal is Double)
                    {
                        text_box_valueDouble = Convert.ToDouble(given_TextBox.Text);
                        valout_Come = Validate_Range(Convert.ToDouble(minVal), Convert.ToDouble(maxVal), text_box_valueDouble);
                    }
                    else
                    {
                        text_box_valueLong = Convert.ToInt64(given_TextBox.Text);
                        valout_Come = Validate_Range(Convert.ToInt64(minVal), Convert.ToInt64(maxVal), text_box_valueLong);
                    }
                }
                //Verify Validation Case
                if (!valout_Come)
                {
                    given_TextBox.ForeColor = Validation_Info.InValidatedColorScheme;
                    errPro.SetError(given_TextBox, String.Format("Data Validation failed,Value {0} is not in range ({1},{2})", given_TextBox.Text, minVal, maxVal));
                    return false;
                }
                else if (String.IsNullOrEmpty(given_TextBox.Text) ||
                         String.IsNullOrWhiteSpace(given_TextBox.Text))
                {
                    given_TextBox.ForeColor = Validation_Info.InValidatedColorScheme;
                    errPro.SetError(given_TextBox, String.Format("Data Validation failed,Empty Value Specified"));
                    return false;
                }
                else
                {
                    given_TextBox.ForeColor = Validation_Info.ValidatedColorScheme;
                    if (errPro != null)
                        errPro.SetError(given_TextBox, String.Empty);
                    return true;
                }
            }
            #region Catch_ErrorHandler_Cases
            catch (FormatException)
            {
                given_TextBox.ForeColor = Color.Red;
                errPro.SetError(given_TextBox, String.Format("Data Validation failed,unable to parse value {0}", given_TextBox.Text));
            }
            catch (Exception)
            {
                given_TextBox.ForeColor = Color.Red;
                errPro.SetError(given_TextBox, String.Format("Data Validation failed,error occured", given_TextBox.Text));
            }
            #endregion
            return false;
        }

        public static bool TextBox_RangeValidation(long minVal, long maxVal, TextBox given_TextBox, ref ErrorProvider errPro)
        {
            ValueType _MinVal = minVal;
            ValueType _MaxVal = maxVal;
            return TextBox_RangeValidation(_MinVal, _MaxVal, given_TextBox, ref errPro);
        }

        public static bool TextBox_RangeValidation(int minVal, int maxVal, TextBox given_TextBox, ref ErrorProvider errPro)
        {
            ValueType _MinVal = minVal;
            ValueType _MaxVal = maxVal;
            return TextBox_RangeValidation(_MinVal, _MaxVal, given_TextBox, ref errPro);
        }

        public static bool TextBox_RangeValidation(ushort minVal, ushort maxVal, TextBox given_TextBox, ref ErrorProvider errPro)
        {
            ValueType _MinVal = minVal;
            ValueType _MaxVal = maxVal;
            return TextBox_RangeValidation(_MinVal, _MaxVal, given_TextBox, ref errPro);
        }

        public static bool TextBox_RangeValidation(double minVal, double maxVal, TextBox given_TextBox, ref ErrorProvider errPro)
        {
            ValueType _MinVal = minVal;
            ValueType _MaxVal = maxVal;
            return TextBox_RangeValidation(_MinVal, _MaxVal, given_TextBox, ref errPro);
        }

        public static void Apply_ValidationResult(bool IsValidated, String ErrorMessage)
        {
            try
            {
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                if (IsValidated)
                {
                    ///Reset,Clear Error
                    //errPro.SetError(given_Ctrl, String.Empty);
                    //given_Ctrl.ForeColor = ValidationInfo.ValidatedColorScheme;
                }
                else
                {
                    //errPro.SetError(given_Ctrl, ErrorMessage);
                    //given_Ctrl.ForeColor = ValidationInfo.InValidatedColorScheme;
                }
            }
            catch { }
        }

        public static void Apply_ValidationResult(bool IsValidated, String ErrorMessage, Control given_Ctrl, ref ErrorProvider errPro)
        {
            try
            {
                App_Validation_Info ValidationInfo = Commons.AppValidationInfo;
                if (IsValidated)
                {
                    ///Reset,Clear Error
                    errPro.SetError(given_Ctrl, String.Empty);
                    given_Ctrl.ForeColor = ValidationInfo.ValidatedColorScheme;
                }
                else
                {
                    errPro.SetError(given_Ctrl, ErrorMessage);
                    given_Ctrl.ForeColor = ValidationInfo.InValidatedColorScheme;
                }
            }
            catch { }
        }

        public static bool IsControlValidated(Control given_Ctrl, ErrorProvider errorProvider)
        {
            String ErrorMessage = String.Empty;
            bool IsValidated = false;
            try
            {
                ErrorMessage = errorProvider.GetError(given_Ctrl);
                IsValidated = !String.IsNullOrEmpty(ErrorMessage) && !String.IsNullOrWhiteSpace(ErrorMessage);
                return IsValidated;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while Validating {0}", (given_Ctrl != null) ? given_Ctrl.Name : null));
            }
        }

        /// ////////////////////////////////////////////////////////////////

        //public static void Apply_ValidationResult(bool IsValidated, String ErrorMessage, Control given_Ctrl, ErrorProvider errPro)
        //{
        //    try
        //    {
        //        App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
        //        if (IsValidated)
        //        {
        //            //Reset,Clear Error
        //            errPro.SetError(given_Ctrl, String.Empty);
        //            //given_Ctrl.ForeColor = ValidationInfo.ValidatedColorScheme;
        //        }
        //        else
        //        {
        //            errPro.SetError(given_Ctrl, ErrorMessage);
        //            //given_Ctrl.ForeColor = ValidationInfo.InValidatedColorScheme;
        //        }
        //    }
        //    catch { }
        //}

        //public static bool IsControlValidated(Control given_Ctrl, ErrorProvider errorProvider)
        //{
        //    String ErrorMessage = String.Empty;
        //    bool IsValidated = false;
        //    try
        //    {
        //        ErrorMessage = errorProvider.GetError(given_Ctrl);
        //        IsValidated = !String.IsNullOrEmpty(ErrorMessage) && !String.IsNullOrWhiteSpace(ErrorMessage);
        //        return IsValidated;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(String.Format("Error occurred while Validating {0}", (given_Ctrl != null) ? given_Ctrl.Name : null));
        //    }
        //}

        #endregion

        #region Validate_TxtExactLength_WithPaddingChar

        public static bool Validate_TxtExactLength_WithPaddingChar(int minChar, int Length, string txt, string PaddingChar)
        {
            try
            {
                if (String.IsNullOrEmpty(txt) || String.IsNullOrWhiteSpace(txt))
                    return false;
                String OrigTxt = txt.TrimEnd(PaddingChar.ToCharArray());
                if (txt.Length == Length && OrigTxt.Length >= minChar && OrigTxt.Length <= Length)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        #endregion

        #region Validate_TxtLength_WithPaddingChar

        public static bool Validate_TxtLength_WithPaddingChar(int minChar, int Length, string txt, string PaddingChar = "\0")
        {
            try
            {
                if (String.IsNullOrEmpty(txt) || String.IsNullOrWhiteSpace(txt))
                    return false;
                String OrigTxt = txt.TrimEnd(PaddingChar.ToCharArray());
                if (OrigTxt.Length >= minChar && OrigTxt.Length <= Length)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        #endregion

        #region Validate_RegExFormat

        internal static bool Validate_RegExFormat(Regex RegEx, string txt)
        {
            try
            {
                Match matches = RegEx.Match(txt);
                if (matches.Success)
                    return true;
                else
                    return false;
            }
            catch { }
            return false;
        }

        public static bool Validate_IPFormat(string txt)
        {
            try
            {
                return Validate_RegExFormat(IpRegEx, txt);
            }
            catch { }
            return false;
        }

        public static bool Validate_URIFormat(String txt)
        {
            try
            {
                return Validate_RegExFormat(URIRegEx, txt);
            }
            catch { }
            return false;
        }

        public static bool Validate_IntlPhoneNumberFormat(String txt)
        {
            try
            {
                return Validate_RegExFormat(Intl_PhoneNumberFormatRegEx, txt);
            }
            catch { }
            return false;
        }

        public static bool Validate_GenericPhoneNumberFormat(String txt)
        {
            try
            {
                if (Generic_PhoneNumberFormatRegEx == null)
                    Generic_PhoneNumberFormatRegEx = new Regex(Generic_PhoneNumberFormat_RegEx, RegexOptions.Compiled);
                return Validate_RegExFormat(Generic_PhoneNumberFormatRegEx, txt + ' ');
            }
            catch { }
            return false;
        }

        public static bool Validate_NIDFormat(String txt)
        {
            try
            {
                return Validate_RegExFormat(Generic_NIDFormatRegEx, txt + ' ');
            }
            catch { }
            return false;
        }

        #endregion

        #region ID_Lookup_HelperMethods

        public static bool Validate_LookupId(FieldInfo fInfo, Object Instance, byte idVal)
        {
            byte matchId = 0;
            try
            {
                matchId = Convert.ToByte(fInfo.GetValue(Instance));
                if (idVal != 0 && idVal == matchId)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            { }
            return false;
        }

        public static bool Validate_LookupId(FieldInfo fInfo, Object[] Instances, byte idVal)
        {
            try
            {
                bool isMatch = false;
                foreach (var item in Instances)
                {
                    if (item != null)
                        isMatch = Validate_LookupId(fInfo, item, idVal);
                    if (isMatch)
                        return isMatch;
                }
                return isMatch;
            }
            catch (Exception)
            { }
            return false;
        }

        public static bool Validate_LookupId(byte[] Instances, byte idVal)
        {
            try
            {
                bool isMatch = false;
                foreach (var item in Instances)
                {
                    if (item != null && Convert.ToByte(item) == idVal)
                    {
                        isMatch = true;
                        break;
                    }
                    if (isMatch)
                        return isMatch;
                }
                return isMatch;
            }
            catch (Exception)
            { }
            return false;
        }

        public static bool Validate_LookupId(FieldInfo fInfo, List<Object> Instances, byte idVal)
        {
            try
            {
                return Validate_LookupId(fInfo, Instances.ToArray(), idVal);
            }
            catch (Exception)
            { }
            return false;
        }

        public static bool Validate_LookupId(FieldInfo LKP_fInfo, Object[] LKP_Instances, FieldInfo M_fInfo, Object[] M_Instances,
            ref String ErrorMessage, bool allowNullable = true)
        {
            try
            {
                bool isMatch = false;
                byte matchId = 0;
                #region Arguemnt_Validation

                if (M_Instances == null || M_Instances.Length <= 0)
                    throw new ArgumentNullException("M_Instances");
                if (LKP_Instances == null || LKP_Instances.Length <= 0)
                    throw new ArgumentNullException("LKP_Instances");
                if (LKP_fInfo == null || M_fInfo == null)
                    throw new ArgumentNullException("fInfo");

                #endregion
                foreach (var item in M_Instances)
                {
                    if (item == null)
                        throw new Exception("Invalid Object to match id");
                    matchId = Convert.ToByte(M_fInfo.GetValue(item));
                    if (matchId == 0 && allowNullable)
                    {
                        isMatch = true;
                        continue;
                    }
                    else
                        isMatch = Validate_LookupId(LKP_fInfo, LKP_Instances, matchId);
                    if (!isMatch)
                    {
                        ErrorMessage = String.Format("Validation failed,{0} id not matched", matchId);
                        return false;
                    }
                }
                return isMatch;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        #endregion

        public static bool Validate_PINCode(ushort Codeval)
        {
            try
            {
                return Validate_Range((ushort)0, (ushort)Common_PCL.AppValidationInfo.PinCode_Length, Codeval);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while Validating PIN Code,{0}", ex.Message), ex);
            }
        }

        #region Validate_Modem_Parametrs

        public static bool Validate_Param_Keep_Alive(Param_WakeUp_Profile[] Param_WakeupProfile_LKP,
            Param_Keep_Alive_IP Param_KeepAlive, ref String ErrorMessage)
        {
            bool isLookup = false;
            try
            {
                if (Param_WakeupProfile_LKP == null || Param_WakeupProfile_LKP.Length <= 0)
                    throw new ArgumentNullException("Param_WakeUp_Profile");
                if (Param_KeepAlive == null)
                    throw new ArgumentNullException("Param_KeepAlive");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Wake_Up_Profile_ID");
                ///Validate  Param_KeepAlive.IP_Profile_ID Lookup
                isLookup = Validate_LookupId(fInfo, Param_WakeupProfile_LKP, Param_KeepAlive.IP_Profile_ID);
                if (!isLookup || Param_KeepAlive.IP_Profile_ID == 0)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Wake_Up_Profile_ID {0}", Param_KeepAlive.IP_Profile_ID);
                    return false;
                }
                ///Validate Ping_time data range validation
                isLookup = Validate_Range(ValidationInfo.Param_KA_MinPingtime, ValidationInfo.Param_KA_MaxPingtime, Param_KeepAlive.Ping_time);
                if (!isLookup)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_KeepAlive.Ping_time {0}", Param_KeepAlive.Ping_time);
                    return false;
                }
                return isLookup;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_Modem_Initialize(Param_Modem_Initialize Param_ModemInit, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_ModemInit == null)
                    throw new ArgumentNullException("Param_Modem_Initialize");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate Username
                isValidated = Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1, ValidationInfo.ModemBasics_Max_PasswordLength1,
                    Param_ModemInit.Username);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Modem_Initialize.Username {0}", Param_ModemInit.Username);
                    return false;
                }
                ///Validate Password
                isValidated = Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1, ValidationInfo.ModemBasics_Max_PasswordLength1,
                    Param_ModemInit.Password);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Modem_Initialize.Password {0}", Param_ModemInit.Password);
                    return false;
                }
                ///Validate APN
                isValidated = Validate_URIFormat(Param_ModemInit.APN);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Modem_Initialize.APN {0}", Param_ModemInit.APN);
                    return false;
                }
                ///Validate PIN Code
                isValidated = Validate_PINCode(Param_ModemInit.PIN_code);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Modem_Initialize.PIN_code {0}", Param_ModemInit.PIN_code);
                    return false;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_Param_Modem_Initialize,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_ModemBasic(Param_ModemBasics_NEW Param_ModemBasicNew, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_ModemBasicNew == null)
                    throw new ArgumentNullException("Param_ModemBasics_NEW");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate Username
                isValidated = Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1, ValidationInfo.ModemBasics_Max_PasswordLength1,
                    Param_ModemBasicNew.UserName);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_ModemBasics_NEW.Username {0}", Param_ModemBasicNew.UserName);
                    return false;
                }
                ///Validate Password
                isValidated = Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1, ValidationInfo.ModemBasics_Max_PasswordLength1,
                    Param_ModemBasicNew.Password);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_ModemBasics_NEW.Password {0}", Param_ModemBasicNew.Password);
                    return false;
                }
                ///Validate WakeupPassword
                isValidated = Validate_TxtLength_WithPaddingChar(ValidationInfo.ModemBasics_Min_PasswordLength1, ValidationInfo.ModemBasics_Max_PasswordLength1,
                    Param_ModemBasicNew.WakeupPassword);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_ModemBasicNew.WakeupPassword {0}", Param_ModemBasicNew.WakeupPassword);
                    return false;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_Param_ModemBasic,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Communication_Profile(
            Param_Communication_Profile _Param_Communication_Profile_object,
            Param_Number_Profile[] _Param_Number_Profiles,
            Param_WakeUp_Profile[] _Param_WakeupProfile_LKP,
            ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (_Param_Communication_Profile_object == null)
                    throw new ArgumentNullException("_Param_Communication_Profile_object");
                if (_Param_Number_Profiles == null || _Param_Number_Profiles.Length <= 0)
                    throw new ArgumentNullException("_Param_Number_Profiles");
                if (_Param_WakeupProfile_LKP == null || _Param_WakeupProfile_LKP.Length <= 0)
                    throw new ArgumentNullException("_Param_WakeupProfile_LKP");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate SelectedMode
                isValidated = Validate_Range((ushort)ValidationInfo.Param_Comm_Profile_MinSelectedMode,
                    (ushort)ValidationInfo.Param_Comm_Profile_MaxSelectedMode,
                    (ushort)_Param_Communication_Profile_object.SelectedMode);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Communication_Profile.SelectedMode {0}",
                        _Param_Communication_Profile_object.SelectedMode);
                    return false;
                }
                FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Wake_Up_Profile_ID");
                ///Validate _Param_Communication_Profile_object.WakeUpProfileID Lookup
                isValidated = Validate_LookupId(fInfo, _Param_WakeupProfile_LKP,
                    _Param_Communication_Profile_object.WakeUpProfileID);
                if (!isValidated ||
                    _Param_Communication_Profile_object.WakeUpProfileID == 0)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent WakeUpProfileID {0}",
                        _Param_Communication_Profile_object.WakeUpProfileID);
                    return false;
                }
                fInfo = typeof(Param_Number_Profile).GetField("Unique_ID");
                ///Validate _Param_Communication_Profile_object.NumberProfileID Lookup
                foreach (var numProfileId in _Param_Communication_Profile_object.NumberProfileID)
                {
                    isValidated = false;
                    isValidated = Validate_LookupId(fInfo, _Param_Number_Profiles, numProfileId);
                    if (!isValidated &&
                        numProfileId != 0)
                    {
                        ErrorMessage = String.Format("Validation failed,InConsistent NumberProfileID {0}",
                            numProfileId);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_Communication_Profile,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_Number_Profile(Param_WakeUp_Profile[] Param_WakeupProfile_LKP,
            Param_Number_Profile[] Param_NumberProfiles, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_WakeupProfile_LKP == null || Param_WakeupProfile_LKP.Length <= 0)
                    throw new ArgumentNullException("Param_WakeUp_Profile");
                if (Param_NumberProfiles == null || Param_NumberProfiles.Length <= 0)
                    throw new ArgumentNullException("Param_NumberProfile");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                isValidated = false;
                foreach (var numProf
                    in Param_NumberProfiles)
                {
                    if (numProf != null)
                    {
                        ///Validate Each Number Profile Object ID here
                        isValidated = Validate_Range((ushort)ValidationInfo._Param_Number_Profile_MinId,
                                                    (ushort)ValidationInfo._Param_Number_Profile_MaxId, numProf.Unique_ID);
                        if (!isValidated && numProf.Unique_ID == 0)
                            continue;
                        else if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Param_NumberProfiles Ids {0}", numProf.Unique_ID);
                            return false;
                        }
                        ///Validate Flag 2(WakeupType)
                        isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo._Param_Number_Profile_WakeUpType, numProf.FLAG2);
                        if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Param_NumberProfile.Flag2,WakeupType");
                            return false;
                        }
                        ///Validate Number
                        String num = Common_PCL.ConvertToValidString(numProf.Number);
                        isValidated = Validate_IntlPhoneNumberFormat(num);
                        if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Param_NumberProfiles.Number {0}", numProf.Number);
                            return false;
                        }
                        #region ///Disable DataCallNumber Validation
                        ///validate Data Call Number
                        //num = Common_PCL.ConvertToValidString(numProf.Datacall_Number);
                        //isValidated = Validate_IntlPhoneNumberFormat(num);
                        //if (!isValidated)
                        //{
                        //    ErrorMessage = String.Format("Validation failed,InConsistent Param_NumberProfiles.Datacall_Number {0}",
                        //        num);
                        //    return false;
                        //}
                        ///Validate ID Lookup On Wake_Up_On_VoiceCall 
                        #endregion
                        FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Wake_Up_Profile_ID");
                        ///Validate  numProf.Wake_Up_On_Voice_Call_ID Lookup
                        isValidated = Validate_LookupId(fInfo, Param_WakeupProfile_LKP, numProf.Wake_Up_On_Voice_Call);
                        if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Number Profile.Wake_Up_On_Voice_Call ID {0}"
                                , numProf.Wake_Up_On_Voice_Call);
                            return false;
                        }
                        ///Validate numProf.Wake_Up_On_SMS_ID Lookup
                        isValidated = Validate_LookupId(fInfo, Param_WakeupProfile_LKP, numProf.Wake_Up_On_SMS);
                        if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Number Profile.Wake_Up_On_SMS ID {0}", numProf.Wake_Up_On_SMS);
                            return false;
                        }
                    }
                    else
                    {
                        ErrorMessage = String.Format("Validation failed,InConsistent Null Param Number Profile");
                        return false;
                    }
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_WakeUp_Profile(Param_IP_Profiles[] Param_IPProfile_LKP,
            Param_WakeUp_Profile[] Param_WakeupProfile, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_IPProfile_LKP == null || Param_IPProfile_LKP.Length <= 0)
                    throw new ArgumentNullException("Param_IPProfile_LKP");
                if (Param_WakeupProfile == null || Param_WakeupProfile.Length <= 0)
                    throw new ArgumentNullException("Param_WakeupProfile");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                foreach (var WkProfile in Param_WakeupProfile)
                {
                    if (WkProfile != null)
                    {
                        ///Validate Each Number Profile Object ID here
                        isValidated = Validate_Range((ushort)ValidationInfo._Param_WakeUp_Profile_MinId,
                            (ushort)ValidationInfo._Param_WakeUp_Profile_MaxId, WkProfile.Wake_Up_Profile_ID);
                        if (!isValidated && WkProfile.Wake_Up_Profile_ID == 0)
                            continue;
                        else if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Param_Number Profile Ids {0}"
                                , WkProfile.Wake_Up_Profile_ID);
                            return false;
                        }
                        FieldInfo fInfo = typeof(Param_WakeUp_Profile).GetField("Unique_ID");
                        ///Validate WkProfile.IP_Profile_ID_1 Lookup
                        isValidated = Validate_LookupId(fInfo, Param_IPProfile_LKP, WkProfile.IP_Profile_ID_1);
                        if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Wakeup WkProfile.IP_Profile_ID_1 {0}"
                                , WkProfile.IP_Profile_ID_1);
                            return false;
                        }
                        ///Validate WkProfile.IP_Profile_ID_2 Lookup
                        isValidated = Validate_LookupId(fInfo, Param_IPProfile_LKP, WkProfile.IP_Profile_ID_2);
                        if (!isValidated && WkProfile.IP_Profile_ID_2 != 0)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Wakeup WkProfile.IP_Profile_ID_2 {0}"
                                , WkProfile.IP_Profile_ID_2);
                            return false;
                        }
                        ///Validate WkProfile.IP_Profile_ID_3 Lookup
                        isValidated = Validate_LookupId(fInfo, Param_IPProfile_LKP, WkProfile.IP_Profile_ID_3);
                        if (!isValidated && WkProfile.IP_Profile_ID_3 != 0)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Wakeup WkProfile.IP_Profile_ID_3 {0}"
                                , WkProfile.IP_Profile_ID_3);
                            return false;
                        }
                        ///Validate WkProfile.IP_Profile_ID_4 Lookup
                        isValidated = Validate_LookupId(fInfo, Param_IPProfile_LKP, WkProfile.IP_Profile_ID_4);
                        if (!isValidated && WkProfile.IP_Profile_ID_4 != 0)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Wakeup WkProfile.IP_Profile_ID_4 {0}"
                                , WkProfile.IP_Profile_ID_4);
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        ErrorMessage = String.Format("Validation failed,InConsistent Null Param Number Profile");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_IP_Profile(Param_IP_Profiles[] Param_IPProfile, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_IPProfile == null || Param_IPProfile.Length <= 0)
                    throw new ArgumentNullException("Param_IPProfile");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                foreach (var IPProfile in Param_IPProfile)
                {
                    if (IPProfile != null)
                    {
                        ///Validate Each Number Profile Object ID here
                        isValidated = Validate_Range((ushort)ValidationInfo._Param_IP_Profile_MinID,
                            (ushort)ValidationInfo._Param_IP_Profile_MaxID, IPProfile.Unique_ID);
                        if (!isValidated && IPProfile.Unique_ID == 0)
                            continue;
                        else if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent IP Profile Ids {0}"
                                , IPProfile.Unique_ID);
                            return false;
                        }
                        ///Validate IP Profile
                        String IPTxt = Common_PCL.LongToIPAddressString(IPProfile.IP);
                        isValidated = Validate_IPFormat(IPTxt);
                        if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent IP Profile Str {0}"
                                , IPTxt);
                            return false;
                        }
                        ///Validate Wrapper_Over_TCP_port
                        isValidated = Validate_Range((ushort)0, ushort.MaxValue, IPProfile.Wrapper_Over_TCP_port);
                        if (!isValidated)
                        {
                            ErrorMessage = String.Format("Validation failed,InConsistent Wrapper_Over_TCP_port {0}", IPProfile.Wrapper_Over_TCP_port);
                            return false;
                        }
                        /////Validate Wrapper_Over_UDP_port
                        //isValidated = Validate_Range((ushort)0, ushort.MaxValue, IPProfile.Wrapper_Over_UDP_port);
                        //if (!isValidated)
                        //{
                        //    ErrorMessage = String.Format("Validation failed,InConsistent Wrapper_Over_UDP_port {0}", IPProfile.Wrapper_Over_UDP_port);
                        //    return false;
                        //}
                        /////Validate HDLC_Over_TCP_Port
                        //isValidated = Validate_Range((ushort)0, ushort.MaxValue, IPProfile.HDLC_Over_TCP_Port);
                        //if (!isValidated)
                        //{
                        //    ErrorMessage = String.Format("Validation failed,InConsistent HDLC_Over_TCP_Port {0}", IPProfile.HDLC_Over_TCP_Port);
                        //    return false;
                        //}
                        /////Validate HDLC_Over_UDP_Port
                        //isValidated = Validate_Range((ushort)0, ushort.MaxValue, IPProfile.HDLC_Over_UDP_Port);
                        //if (!isValidated)
                        //{
                        //    ErrorMessage = String.Format("Validation failed,InConsistent HDLC_Over_UDP_Port {0}", IPProfile.HDLC_Over_UDP_Port);
                        //    return false;
                        //}
                        return true;
                    }
                    else
                    {
                        ErrorMessage = String.Format("Validation failed,InConsistent Null Param IP Profile");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_ModemLimitsAndTime(Param_ModemLimitsAndTime Param_ModemLimitsAndTime, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_ModemLimitsAndTime == null)
                    throw new ArgumentNullException("Param_ModemLimitsAndTime");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                #region Retry__
                ///Validate Retry_SMS
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Retry_SMS_Max,
                    Param_ModemLimitsAndTime.Retry_SMS);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Retry_SMS {0}"
                        , Param_ModemLimitsAndTime.Retry_SMS);
                    return false;
                }
                ///Validate Retry
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Retry_Max,
                    Param_ModemLimitsAndTime.Retry);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Retry {0}"
                        , Param_ModemLimitsAndTime.Retry_SMS);
                    return false;
                }
                ///Validate Retry_IP_connection_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Retry_IP_connection_Max,
                    Param_ModemLimitsAndTime.Retry_IP_connection);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Retry_IP_connection {0}"
                        , Param_ModemLimitsAndTime.Retry_IP_connection);
                    return false;
                }
                ///Validate Retry_TCP_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Retry_TCP_Max,
                    Param_ModemLimitsAndTime.Retry_TCP);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Retry_TCP {0}"
                        , Param_ModemLimitsAndTime.Retry_TCP);
                    return false;
                }
                ///Validate Retry_UDP_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Retry_UDP_Max,
                    Param_ModemLimitsAndTime.Retry_UDP);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Retry_UDP {0}"
                        , Param_ModemLimitsAndTime.Retry_UDP);
                    return false;
                }
                #endregion
                #region RSSI_LEVEL___
                ///Validate RSSI_LEVEL_TCP_UDP_Connection_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.RSSI_LEVEL_TCP_UDP_Connection_Max,
                    Param_ModemLimitsAndTime.RSSI_LEVEL_TCP_UDP_Connection);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.RSSI_LEVEL_TCP_UDP_Connection {0}"
                        , Param_ModemLimitsAndTime.RSSI_LEVEL_TCP_UDP_Connection);
                    return false;
                }
                ///Validate RSSI_LEVEL_SMS_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.RSSI_LEVEL_SMS_Max,
                    Param_ModemLimitsAndTime.RSSI_LEVEL_SMS);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.RSSI_LEVEL_SMS {0}"
                        , Param_ModemLimitsAndTime.RSSI_LEVEL_SMS);
                    return false;
                }
                ///Validate RSSI_LEVEL_Data_Call_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.RSSI_LEVEL_Data_Call_Max,
                    Param_ModemLimitsAndTime.RSSI_LEVEL_Data_Call);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.RSSI_LEVEL_Data_Call_Max {0}"
                        , Param_ModemLimitsAndTime.RSSI_LEVEL_Data_Call);
                    return false;
                }
                #endregion
                #region Time_between_Retries__
                ///Validate Time_between_Retries_SMS_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Time_between_Retries_SMS_Max,
                    Param_ModemLimitsAndTime.Time_between_Retries_SMS);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Time_between_Retries_SMS {0}"
                        , Param_ModemLimitsAndTime.Time_between_Retries_SMS);
                    return false;
                }
                ///Validate Time_between_Retries_TCP_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Time_between_Retries_TCP_Max,
                    Param_ModemLimitsAndTime.Time_between_Retries_TCP);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Time_between_Retries_TCP {0}"
                        , Param_ModemLimitsAndTime.Time_between_Retries_TCP);
                    return false;
                }
                ///Validate Time_between_Retries_IP_connection_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Time_between_Retries_IP_connection_Max,
                    Param_ModemLimitsAndTime.Time_between_Retries_IP_connection);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Time_between_Retries_IP_connection {0}"
                        , Param_ModemLimitsAndTime.Time_between_Retries_IP_connection);
                    return false;
                }
                ///Validate Time_between_Retries_UDP_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Time_between_Retries_UDP_Max,
                    Param_ModemLimitsAndTime.Time_between_Retries_UDP);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Time_between_Retries_UDP {0}"
                        , Param_ModemLimitsAndTime.Time_between_Retries_UDP);
                    return false;
                }
                ///Validate Time_between_Retries_Data_Call_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.Time_between_Retries_Data_Call_Max,
                    Param_ModemLimitsAndTime.Time_between_Retries_Data_Call);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.Time_between_Retries_Data_Call {0}"
                        , Param_ModemLimitsAndTime.Time_between_Retries_Data_Call);
                    return false;
                }
                ///Validate TimeRetriesAlwaysOnCycle_Max
                isValidated = Validate_Range((ushort)0, (ushort)ValidationInfo.TimeRetriesAlwaysOnCycle_Max,
                    Param_ModemLimitsAndTime.TimeRetriesAlwaysOnCycle);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.TimeRetriesAlwaysOnCycle {0}"
                        , Param_ModemLimitsAndTime.TimeRetriesAlwaysOnCycle);
                    return false;
                }
                #endregion
                ///Validate TCP_Inactivity
                isValidated = Validate_Range(ValidationInfo.TCP_Inactivity_Min, ValidationInfo.TCP_Inactivity_Max,
                    Param_ModemLimitsAndTime.TCP_Inactivity);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.TCP_Inactivity {0}"
                        , Param_ModemLimitsAndTime.TCP_Inactivity);
                    return false;
                }
                ///Validate TimeOut_CipSend
                isValidated = Validate_Range(ValidationInfo.TimeOut_CipSend_Min, ValidationInfo.TimeOut_CipSend_Max,
                    Param_ModemLimitsAndTime.TimeOut_CipSend);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,InConsistent Param_ModemLimitsAndTime.TimeOut_CipSend {0}"
                        , Param_ModemLimitsAndTime.TimeOut_CipSend);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        #endregion

        public static bool Validate_Param_TimeBase_Event(Param_TimeBaseEvents Param_TBE, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_TBE == null)
                    throw new ArgumentNullException("Param_TimeBaseEvents");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate Control_Enum data range validation
                isValidated = Validate_Range((byte)Param_TimeBaseEvents.Tb_Disable, (byte)Param_TimeBaseEvents.Tb_Fixed, Param_TBE.Control_Enum);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_TBE.Control_Enum {0}", Param_TBE.Control_Enum);
                    return false;
                }
                ///Validate Interval data range validation
                if (Param_TBE.Control_Enum == Param_TimeBaseEvents.Tb_Interval)
                    isValidated = Validate_Range(ValidationInfo._Param_TBE_MinInterval, ValidationInfo._Param_TBE_MaxInterval, Param_TBE.Interval);
                else
                    isValidated = Validate_Range(ValidationInfo._Param_TBE_MinInterval, ValidationInfo._Param_TBE_MaxIntervalMisc, Param_TBE.Interval);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_TBE.Interval {0}", Param_TBE.Interval);
                    return false;
                }
                //Validate StDateTime Validation
                if (Param_TBE.DateTime == null || !Param_TBE.DateTime.IsValid)
                    isValidated = false;
                else
                    isValidated = true;
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occurred while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        #region Validate_Meter_Parameters

        public static bool Validate_Param_Contactor(Param_Contactor Param_Contactor, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_Contactor == null)
                    throw new ArgumentNullException("Param_Contactor");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate Contactor_ON_Pulse_Time data range validation
                isValidated = Validate_Range((ushort)0, ValidationInfo.Contactor_ON_Max_Pulse_Time, Param_Contactor.Contactor_ON_Pulse_Time);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.Contactor_ON_Max_Pulse_Time {0}", Param_Contactor.Contactor_ON_Pulse_Time);
                    return false;
                }
                ///Validate Contactor_Off_Pulse_Time data range validation
                isValidated = Validate_Range((ushort)0, ValidationInfo.Contactor_OFF_Max_Pulse_Time, Param_Contactor.Contactor_OFF_Pulse_Time);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.Contactor_OFF_Pulse_Time {0}", Param_Contactor.Contactor_OFF_Pulse_Time);
                    return false;
                }
                ///Validate Minimum_Interval_Bw_Contactor_State_Change_Max data range validation
                isValidated = Validate_Range((ushort)0, ValidationInfo.Minimum_Interval_Bw_Contactor_State_Change_Max, Param_Contactor.Minimum_Interval_Bw_Contactor_State_Change);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.Minimum_Interval_Bw_Contactor_State_Change {0}",
                        Param_Contactor.Minimum_Interval_Bw_Contactor_State_Change);
                    return false;
                }
                ///Validate Power_Up_Delay_To_State_Change data range validation
                isValidated = Validate_Range((ushort)0, ValidationInfo.Power_Up_Delay_To_State_Change_Max, Param_Contactor.Power_Up_Delay_To_State_Change);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.Power_Up_Delay_To_State_Change {0}",
                        Param_Contactor.Power_Up_Delay_To_State_Change);
                    return false;
                }
                ///Validate Interval_Between_Retries data range validation
                isValidated = Validate_Range((uint)0, ValidationInfo.Interval_Between_Retries_Max, Param_Contactor.Interval_Between_Retries);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.Interval_Between_Retries {0}",
                        Param_Contactor.Interval_Between_Retries);
                    return false;
                }
                ///Validate RetryCount data range validation
                isValidated = Validate_Range((byte)0, ValidationInfo.RetryCount_Max, Param_Contactor.RetryCount);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.RetryCount {0}",
                        Param_Contactor.RetryCount);
                    return false;
                }
                ///Validate Control_Mode data range validation
                isValidated = Validate_Range((byte)0, ValidationInfo.Control_Mode_Max, Param_Contactor.Control_Mode);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Param_Contactor.Control_Mode {0}",
                        Param_Contactor.Control_Mode);
                    return false;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occurred while exec Validate_Param_Contactor,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_clock_Calib(Param_Clock_Caliberation Param_Clock_Calib, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_Clock_Calib == null)
                    throw new ArgumentNullException("Param_Clock_Calib");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                if (Param_Clock_Calib.Enable_Day_Light_Saving_FLAG)
                {
                    ///Validate Begin_Date Struct validation
                    isValidated = App_Validation.Validate_Range(App_Validation_Info.Param_Clock_Calib_DateMin,
                        App_Validation_Info.Param_Clock_Calib_DateMax, Param_Clock_Calib.Begin_Date);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Validation failed,Not in Valid Range:Param_Clock_Calib.Begin_Date {0}", Param_Clock_Calib.Begin_Date);
                        return false;
                    }
                    ///Validate End_Date Struct validation
                    isValidated = App_Validation.Validate_Range(App_Validation_Info.Param_Clock_Calib_DateMin,
                        App_Validation_Info.Param_Clock_Calib_DateMax, Param_Clock_Calib.End_Date);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Validation failed,Not in Valid Range:Param_Clock_Calib.End_Date {0}", Param_Clock_Calib.End_Date);
                        return false;
                    }
                }
                ///Validate Set_DateTime Struct validation
                isValidated = App_Validation.Validate_Range(App_Validation_Info.Param_Clock_Calib_DateMin,
                    App_Validation_Info.Param_Clock_Calib_DateMax, Param_Clock_Calib.Set_Time);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Not in Valid Range:Param_Clock_Calib.Set_Time {0}", Param_Clock_Calib.Set_Time);
                    return false;
                }
                ///Validate Clock_Caliberation_PPM data range validation
                isValidated = Validate_Range((ushort)0, ValidationInfo.PPM_Max, Param_Clock_Calib.Clock_Caliberation_PPM);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid Range Param_Clock_Calib.Clock_Caliberation_PPM {0}",
                        Param_Clock_Calib.Clock_Caliberation_PPM);
                    return false;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        internal static bool Validate_Param_DecimalPoint(byte[] limitValues, byte value)
        {
            try
            {
                int upper;
                int lower;

                byte left = limitValues[0];
                byte right = limitValues[1];
                byte total = limitValues[2];

                upper = value / 16;
                lower = value % 16;

                if (upper <= left && lower <= right && (upper + lower) <= total)
                    return true;
                else
                    return false;
            }
            catch
            { }
            return false;
        }

        public static bool Validate_Param_DecimalPoint(Param_Decimal_Point Param_Decimal_Point, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_Decimal_Point == null)
                    throw new ArgumentNullException("Param_Decimal_Point");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate  Param_DecimalPoint_Billing_Energy
                isValidated = App_Validation.Validate_Param_DecimalPoint(ValidationInfo.DecimalPoint_BillingKWH, Param_Decimal_Point.Billing_Energy);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:DecimalPoint_BillingKWH ");
                    return false;
                }
                ///Validate  Param_DecimalPoint_Billing_MDI
                isValidated = App_Validation.Validate_Param_DecimalPoint(ValidationInfo.DecimalPoint_BillingKWHMDI, Param_Decimal_Point.Billing_MDI);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:DecimalPoint_BillingKWHMDI ");
                    return false;
                }
                ///Validate  Param_DecimalPoint_InstantaneousVoltage
                isValidated = App_Validation.Validate_Param_DecimalPoint(ValidationInfo.DecimalPoint_InstantaneousVoltage, Param_Decimal_Point.Instataneous_Voltage);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:DecimalPoint_InstantaneousVoltage ");
                    return false;
                }
                ///Validate  Param_DecimalPoint_InstataneousCurrent
                isValidated = App_Validation.Validate_Param_DecimalPoint(ValidationInfo.DecimalPoint_InstantaneousCurrent, Param_Decimal_Point.Instataneous_Current);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:DecimalPoint_InstantaneousCurrent ");
                    return false;
                }
                ///Validate  Param_DecimalPoint_Instataneous_Power
                isValidated = App_Validation.Validate_Param_DecimalPoint(ValidationInfo.DecimalPoint_InstantaneousPower, Param_Decimal_Point.Instataneous_Power);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:DecimalPoint_InstantaneousPower ");
                    return false;
                }
                ///Validate  Param_DecimalPoint_Instataneous_MDI
                isValidated = App_Validation.Validate_Param_DecimalPoint(ValidationInfo.DecimalPoint_InstantaneousMDI, Param_Decimal_Point.Instataneous_MDI);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:DecimalPoint_InstantaneousMDI ");
                    return false;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while exec Validate_LookupId,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_CTPT_Ratio(Param_CTPT_Ratio Param_CTPT_Ratio, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (Param_CTPT_Ratio == null)
                    throw new ArgumentNullException("Param_CTPT_Ratio");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate Param_CTPT_Ratio.CTratio_Numerator
                isValidated = Validate_Range((ushort)0, ValidationInfo.CTratio_Numerator_Max, Param_CTPT_Ratio.CTratio_Numerator);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_CTPT_Ratio.CTratio_Numerator");
                    return false;
                }
                ///Validate Param_CTPT_Ratio.CTratio_Denominator
                isValidated = Validate_Range((ushort)0, ValidationInfo.CTratio_Denominator_Max, Param_CTPT_Ratio.CTratio_Denominator);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_CTPT_Ratio.CTratio_Denominator");
                    return false;
                }
                ///Validate Param_CTPT_Ratio.PTratio_Numerator
                isValidated = Validate_Range((ushort)0, ValidationInfo.PTratio_Numerator_Max, Param_CTPT_Ratio.PTratio_Numerator);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_CTPT_Ratio.PTratio_Numerator");
                    return false;
                }
                ///Validate Param_CTPT_Ratio.PTratio_Denominator
                isValidated = Validate_Range((ushort)0, ValidationInfo.PTratio_Denominator_Max, Param_CTPT_Ratio.PTratio_Denominator);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_CTPT_Ratio.PTratio_Denominator");
                    return false;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while Validate_Param_CTPT_Ratio,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_Param_Password(Param_Password Param_Password, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate _Management_Device
                isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.PasswordLength_Min,
                    ValidationInfo.PasswordLength_Max, Param_Password._Management_Device, "\r");
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_Password");
                    ///Special Case For PasswordLength_Min == 0
                    if (ValidationInfo.PasswordLength_Min <= 0 &&
                        (String.IsNullOrEmpty(Param_Password._Management_Device) ||
                        String.IsNullOrWhiteSpace(Param_Password._Management_Device)))
                        isValidated = true;
                    else
                        return false;
                }
                ///Validate _Electrical_Device
                isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.PasswordLength_Min,
                    ValidationInfo.PasswordLength_Max, Param_Password._Electrical_Device, "\r");
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_Password");
                    ///Special Case For PasswordLength_Min == 0
                    if (ValidationInfo.PasswordLength_Min <= 0 &&
                        (String.IsNullOrEmpty(Param_Password._Electrical_Device) ||
                        String.IsNullOrWhiteSpace(Param_Password._Electrical_Device)))
                        isValidated = true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while Validate_Param_Password,{0}", ex.Message);
            }
            return isValidated;
        }

        public static bool Validate_Param_Customer_Code(Param_Customer_Code Param_CustomerCode, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate _Customer_Code_String
                isValidated = App_Validation.Validate_TxtLength_WithPaddingChar(ValidationInfo.CustomerCodeLength_Min,
                    ValidationInfo.CustomerCodeLength_Max, Param_CustomerCode._Customer_Code_String, "\0");
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid format:Param_CustomerCode");
                    ///Special Case For PasswordLength_Min == 0
                    if (ValidationInfo.CustomerCodeLength_Min <= 0 &&
                        (String.IsNullOrEmpty(Param_CustomerCode._Customer_Code_String) ||
                        String.IsNullOrWhiteSpace(Param_CustomerCode._Customer_Code_String)))
                        isValidated = true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while Validate_Param_Password,{0}", ex.Message);
            }
            return isValidated;
        }

        public static bool Validate_Param_MDI_Parameters(Param_MDI_parameters Param_MDIParameters, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate Param_MDI_Parameters.Min_Interval_ManualReset
                isValidated = Validate_Range((ushort)0,
                    ValidationInfo.Interval_ManualReset_Max,
                    Param_MDIParameters.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid data range:Param_MDIParameters.Interval_ManualReset_Max");
                    return false;
                }
                ///Validate Param_MDI_Parameters.Min_Time_Unit
                isValidated = Validate_Range((ushort)0,
                    ValidationInfo.Interval_ManualReset_Max,
                    Param_MDIParameters.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid data range:Param_MDIParameters.Interval_ManualReset_Max");
                    return false;
                }
                ///Validate Param_MDI_Parameters.MDI_Interval
                isValidated = Validate_Range(ValidationInfo.MDI_Interval_Min,
                    ValidationInfo.MDI_Interval_Max,
                    Param_MDIParameters.MDI_Interval);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid data range:Param_MDIParameters.MDI_Interval");
                    return false;
                }
                ///Validate Param_MDI_Parameters.MDI_Interval
                isValidated = Validate_LookupId(ValidationInfo.MDI_Intervals,
                    (byte)Param_MDIParameters.MDI_Interval);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid data range:Param_MDIParameters.MDI_Interval");
                    return false;
                }
                ///Validate Param_MDI_Parameters.Auto_reset_date
                if (Param_MDIParameters.FLAG_Auto_Reset_0)
                {
                    isValidated = (Param_MDIParameters.Auto_reset_date != null) &&
                                   Param_MDIParameters.Auto_reset_date.IsValid;
                }
                else
                    isValidated = true;
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid StDateTime:Param_MDIParameters.Auto_reset_date");
                    return false;
                }
                ///Validate Param_MDI_Parameters.Roll_slide_count
                isValidated = Validate_Range(ValidationInfo.Roll_slide_count_Min,
                    ValidationInfo.Roll_slide_count_Max,
                    Param_MDIParameters.Roll_slide_count);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation failed,Invalid data range:Param_MDIParameters.Roll_slide_count");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occurred while Validate_Param_MDI_parameters,{0}", ex.Message);
            }
            return isValidated;
        }

        #region ParamDisplayWindows

        public static int Validate_LookupDisplayWindowItem(List<DisplayWindowItem> Instances, DisplayWindowItem winItem)
        {
            try
            {
                if (winItem == null)
                    throw new ArgumentNullException("winItem");
                if (Instances == null || Instances.Count <= 0)
                    throw new ArgumentNullException("Instances");

                List<DisplayWindowItem> winItems = Instances.FindAll((x) =>
                    x.Obis_Index == winItem.Obis_Index ||
                    x.Window_Name.Equals(winItem.Window_Name));
                return winItems.Count;
            }
            catch
            { }
            return -1;
        }

        public static bool Validate_LookupDisplayWindowItem(List<DisplayWindowItem> Instances, DisplayWindowItem winItem, ref String ErrorMessage)
        {
            int lookup = -1;
            try
            {
                lookup = Validate_LookupDisplayWindowItem(Instances, winItem);
                if (lookup <= 0)
                {
                    ErrorMessage = String.Format("Validation Error,unable to Lookup item {0}", winItem);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Validation Error,unable to Lookup item {0}.{1}", winItem, ex.Message);
            }
            return false;
        }

        public static bool Validate_LookupDisplayWindowItem(List<DisplayWindowItem> InstancesLKP, List<DisplayWindowItem> winItems, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                ///Iterate through InstancesLKP
                foreach (DisplayWindowItem winItem in winItems)
                {
                    isValidated = Validate_LookupDisplayWindowItem(InstancesLKP, winItem, ref ErrorMessage);
                    if (!isValidated)
                        break;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Validation Error,unable to Lookup item {1}", ex.Message);
            }
            return false;
        }

        public static bool Validate_LookupDuplicate_DisplayWindowItem(List<DisplayWindowItem> Instances, DisplayWindowItem winItem, ref String ErrorMessage)
        {
            int lookup = -1;
            try
            {
                lookup = Validate_LookupDisplayWindowItem(Instances, winItem);
                if (lookup <= 1)
                {
                    return true;
                }
                else if (lookup > 1)
                {
                    ErrorMessage = String.Format("Validation Error,Duplicate Display Window item {0}", winItem);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Validation Error,unable to Lookup item {0}.{1}", winItem, ex.Message);
            }
            return false;
        }

        public static bool Validate_LookupDuplicate_DisplayWindowItem(List<DisplayWindowItem> Instances, ref String ErrorMessage)
        {
            try
            {
                bool isValidated = false;
                foreach (DisplayWindowItem winItem in Instances)
                {
                    isValidated = Validate_LookupDuplicate_DisplayWindowItem(Instances, winItem, ref ErrorMessage);
                    if (!isValidated)
                        break;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return false;
        }

        public static bool Validate_WindowNumber(WindowNumber WindowNumber, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                if (WindowNumber.OBIS_Code_Display_Mode)
                {
                    isValidated = WindowNumber.Display_OBIS_Field_C ||
                                  WindowNumber.Display_OBIS_Field_D ||
                                  WindowNumber.Display_OBIS_Field_E;
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Invalid Window Number in OBIS Code Format");
                        return false;
                    }
                }
                if (WindowNumber.DisplayWindowNumber < 0 || WindowNumber.DisplayWindowNumber > WindowNumber.MaxWindowNumber)
                {
                    isValidated = false;
                    ErrorMessage = String.Format("Invalid Window Number not in valid range {0}", WindowNumber.DisplayWindowNumber);
                }
                isValidated = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Invalid Window Number Format" + ex.Message);
            }
            return isValidated;
        }

        public static int Validate_LookupWindowNumber(List<DisplayWindowItem> WindowNumbers, WindowNumber WinNumber)
        {
            int WinNumCount = 0;
            try
            {
                if (WindowNumbers == null || WindowNumbers.Count <= 0)
                    throw new ArgumentNullException("WindowNumbers");
                List<DisplayWindowItem> winItems = new List<DisplayWindowItem>();
                if (!WinNumber.OBIS_Code_Display_Mode)
                    winItems = WindowNumbers.FindAll((x) =>
                        !x.WindowNumberToDisplay.OBIS_Code_Display_Mode &&
                        x.WindowNumberToDisplay.DisplayWindowNumber == WinNumber.DisplayWindowNumber);
                WinNumCount = winItems.Count;
                return WinNumCount;
            }
            catch (Exception ex)
            {
                String ErrorMessage = String.Format("Invalid Window Number Format" + ex.Message);
                throw new Exception(ErrorMessage, ex);
            }
            //return WinNumCount;
        }

        public static bool Validate_LookupDuplicate_WindowNumber(List<DisplayWindowItem> WindowNumbers, WindowNumber WinNumber, ref String ErrorMessage)
        {
            int duplicate_Count = 0;
            try
            {
                duplicate_Count = Validate_LookupWindowNumber(WindowNumbers, WinNumber);
                if (duplicate_Count > 1)
                {
                    ErrorMessage = String.Format("Duplicate Window Number Found {0}", WinNumber);
                    return false;
                }
                else
                {
                    ErrorMessage = String.Format("No Duplicate Window Number Found"); //Na-Jaiz Error
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return false;
        }

        public static bool Validate_DisplayWindowItem(DisplayWindowItem WinItem, ref String ErrorMessage)
        {
            try
            {
                bool isValidated = false;
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate window_Name
                if (String.IsNullOrEmpty(WinItem.Window_Name) ||
                   String.IsNullOrWhiteSpace(WinItem.Window_Name))
                {
                    ErrorMessage = String.Format("Validation Failed,Incorrect Display Window Name");
                    return false;
                }
                ///Validate Attribute Selected
                isValidated = WinItem.AttributeSelected >= 0 && WinItem.AttributeSelected <= ValidationInfo.ClassAttribute_Max;
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation Failed,Incorrect Display Window Data Attribute");
                    return false;
                }
                ///Validate DisplayWindowCategory
                isValidated = WinItem.Category != null && WinItem.Category.Count > 0;
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation Failed,Incorrect Display Window Category");
                    return false;
                }
                ///Validate WindowNumberToDispay
                isValidated = App_Validation.Validate_WindowNumber(WinItem.WindowNumberToDisplay, ref ErrorMessage);
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured Validate Display Window Item,{0}", ex.Message);
            }
            return false;
        }

        public static bool Validate_DisplayWindows(DisplayWindows DisplayWindowObj, List<DisplayWindowItem> InstancesLKP, ref String ErrorMessage)
        {
            try
            {
                if (DisplayWindowObj == null)
                    throw new ArgumentNullException("DisplayWindowObj");
                if (InstancesLKP == null || InstancesLKP.Count <= 0)
                    throw new ArgumentNullException("InstancesLKP");
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                bool isValidated = false;
                
                if (DisplayWindowObj.WindowsMode == DispalyWindowsModes.Normal) //v10.0.21
                {
                    ///Validate DisplayWindows ScrollTime
                    isValidated = Validate_Range(ValidationInfo.scrollTime_Min, ValidationInfo.scrollTime_Max, DisplayWindowObj.ScrollTime.TotalSeconds);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format(DisplayWindowObj.WindowsMode.ToString() + ": Scroll Time range error {0}", DisplayWindowObj.ScrollTime.TotalSeconds);
                        return isValidated;
                    }
                }

                if (DisplayWindowObj.WindowsMode != DispalyWindowsModes.Test) //v10.0.21
                {
                    ///Validate DisplayWindowsItems
                    isValidated = Validate_Range(1, ValidationInfo.WidowCount_Max, DisplayWindowObj.Windows.Count);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format(DisplayWindowObj.WindowsMode.ToString() + ": Display WindowsItems Count {0}", DisplayWindowObj.Windows.Count);
                        return isValidated;
                    } 
                }

                if (DisplayWindowObj.WindowsMode == DispalyWindowsModes.Test && DisplayWindowObj.Windows.Count == 0) //v10.0.21
                {
                    isValidated = true; 
                }
                else
                {
                    ///Validate Allowed DisplayWindowsItem
                    isValidated = Validate_LookupDisplayWindowItem(InstancesLKP, DisplayWindowObj.Windows, ref ErrorMessage);
                    if (!isValidated)
                        return isValidated;
                    ///Validate DisplayWindowsItem
                    foreach (var dispWinItem in DisplayWindowObj.Windows)
                    {
                        isValidated = Validate_DisplayWindowItem(dispWinItem, ref ErrorMessage);
                        if (!isValidated)
                            return isValidated;
                        ///Validate Duplicate WindowNumber
                        isValidated = Validate_LookupDuplicate_WindowNumber(DisplayWindowObj.Windows,
                                        dispWinItem.WindowNumberToDisplay, ref ErrorMessage);
                        if (!isValidated)
                            return isValidated;
                    }
                    ///Validate Duplicate DisplayWindowsItems
                    isValidated = Validate_LookupDuplicate_DisplayWindowItem(DisplayWindowObj.Windows, ref ErrorMessage); 
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured Validate Display Window {0}", ex.Message);
            }
            return false;
        }

        #endregion

        #region LoadProfileChannelInfo

        public static int Validate_LookupLoadProfileItem(List<LoadProfileChannelInfo> Instances, LoadProfileChannelInfo lpInfo)
        {
            try
            {
                if (lpInfo == null)
                    throw new ArgumentNullException("LoadProfile Channel Info");
                if (Instances == null || Instances.Count <= 0)
                    throw new ArgumentNullException("Instances");

                List<LoadProfileChannelInfo> winItems = Instances.FindAll((x) =>
                            x == lpInfo ||
                            (x.OBIS_Index == lpInfo.OBIS_Index &&
                             x.Quantity_Name.Equals(lpInfo.Quantity_Name)));
                return winItems.Count;
            }
            catch
            { }
            return -1;
        }

        public static bool Validate_LookupLoadProfileItem(List<LoadProfileChannelInfo> Instances, LoadProfileChannelInfo lpInfo, ref String ErrorMessage)
        {
            int lookup = -1;
            try
            {
                lookup = Validate_LookupLoadProfileItem(Instances, lpInfo);
                if (lookup <= 0)
                {
                    ErrorMessage = String.Format("Validation Error,unable to Lookup item {0}", lpInfo);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Validation Error,unable to Lookup item {0}.{1}", lpInfo, ex.Message);
            }
            return false;
        }

        public static bool Validate_LookupLoadProfileItem(List<LoadProfileChannelInfo> InstancesLKP, List<LoadProfileChannelInfo> lpInfos, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                ///Iterate through InstancesLKP
                foreach (LoadProfileChannelInfo lpInfo in lpInfos)
                {
                    isValidated = Validate_LookupLoadProfileItem(InstancesLKP, lpInfo, ref ErrorMessage);
                    if (!isValidated)
                        break;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Validation Error,unable to Lookup item {1}", ex.Message);
            }
            return false;
        }

        public static bool Validate_LookupDuplicate_LoadProfileItem(List<LoadProfileChannelInfo> Instances, LoadProfileChannelInfo lpInfo, ref String ErrorMessage)
        {
            int lookup = -1;
            try
            {
                lookup = Validate_LookupLoadProfileItem(Instances, lpInfo);
                if (lookup <= 1)
                {
                    return true;
                }
                else if (lookup > 1)
                {
                    ErrorMessage = String.Format("Validation Error,duplicate Load Profile Channel Info {0}", lpInfo);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Validation Error, to Lookup item {0}.{1}", lpInfo, ex.Message);
            }
            return false;
        }

        public static bool Validate_LookupDuplicate_LoadProfileItem(List<LoadProfileChannelInfo> Instances, ref String ErrorMessage)
        {
            try
            {
                bool isValidated = false;
                foreach (LoadProfileChannelInfo lpInfo in Instances)
                {
                    isValidated = Validate_LookupDuplicate_LoadProfileItem(Instances, lpInfo, ref ErrorMessage);
                    if (!isValidated)
                        break;
                }
                return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return false;
        }

        public static bool Validate_LoadProfile_CapturePeriod(TimeSpan CapturePeriod, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate LP_Range
                isValidated = Validate_Range(ValidationInfo.LP_IntervalMin.TotalMinutes,
                    ValidationInfo.LP_IntervalMax.TotalMinutes, CapturePeriod.TotalMinutes);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Load Profile Period out of range {0}", CapturePeriod);
                    return isValidated;
                }
                isValidated = false;
                ///Lookup LP Step Range
                foreach (var lpCaptureInfo in ValidationInfo.LP_Intervals)
                {
                    if (lpCaptureInfo == CapturePeriod.TotalMinutes)
                    {
                        isValidated = true;
                        break;
                    }
                }
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Load Profile Period not matched {0}", CapturePeriod);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured Validate Load Profile Capture Period {0},{1}", CapturePeriod, ex.Message);
            }
            return isValidated;
        }

        public static bool Validate_LoadProfile_ChannelInfo(List<LoadProfileChannelInfo> Instances, ref String ErrorMessage)
        {
            bool isValidated = false;
            try
            {
                App_Validation_Info ValidationInfo = Common_PCL.AppValidationInfo;
                ///Validate LoadProfile_ChannelInfo
                if (Instances == null || Instances.Count != ValidationInfo.MaxLPChannelCount || Instances.Contains(null))
                {
                    ErrorMessage = String.Format("Error occurred Validate LoadProfile Channel Info");
                    isValidated = false;
                    return isValidated;
                }
                ///Validate LoadProfile Capture Period
                foreach (var lpInfo in Instances)
                {
                    isValidated = Validate_LoadProfile_CapturePeriod(lpInfo.CapturePeriod, ref ErrorMessage);
                    if (!isValidated)
                        return isValidated;
                }
                ///Validate Lookup LoadProfile Channels Info
                isValidated = Validate_LookupDuplicate_LoadProfileItem(Instances, ref ErrorMessage);
                if (!isValidated)
                    return isValidated;
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occurred Validate_LoadProfile_ChannelInfo {0}", ex.Message);
            }
            return isValidated;
        }

        #endregion

        #region Param_TOD(Tariff Of Day)

        public static bool IsTimeSlicesOverlapping(List<TimeSlot> timeSlices, out TimeSlot OverlappingTimeSlot,
            ushort Slide_Count = TimeSlot.MAX_TimeSlot)
        {
            try
            {
                timeSlices.Sort(Common_Comparable.CompareTimeSlotById);   ///Sort List Based On TimeSliceId
                for (int index = 0; (index < timeSlices.Count - 1) &&
                                    (index < Slide_Count - 1); index++)
                {
                    ///If TWO timeSlices equals
                    if (timeSlices[index] == null || timeSlices[index + 1] == null ||
                        timeSlices[index].StartTime >= timeSlices[index + 1].StartTime)
                    {
                        OverlappingTimeSlot = timeSlices[index + 1];
                        return true;
                    }
                    else if (timeSlices[index].TimeSlotId + 1 != timeSlices[index + 1].TimeSlotId)
                    {
                        OverlappingTimeSlot = timeSlices[index + 1];
                        return true;
                    }
                }
                OverlappingTimeSlot = null;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Validate_TimeSchedule(TimeSlot TimeSlot, ref String ErrorMessage)
        {
            bool IsValidated = false;
            try
            {
                ///Validate TimeSlotId Range
                IsValidated = Validate_Range((byte)1, TimeSlot.MAX_TimeSlot, TimeSlot.TimeSlotId);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid TimeSlotId {0}", TimeSlot.TimeSlotId);
                    return IsValidated;
                }
                ///Validate TimeSchedule StartTime 
                IsValidated = Validate_Range(TimeSlot.StartTime_Min.TotalMinutes, TimeSlot.StartTime_Max.TotalMinutes, TimeSlot.StartTime.TotalMinutes);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid TimeSlot StartTime {0}", TimeSlot.StartTime);
                    return IsValidated;
                }
            }
            catch { IsValidated = false; }
            return IsValidated;
        }

        public static bool Validate_DayProfile(DayProfile DayProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            int schedule_Count = 0;
            try
            {
                ///Validate DayProfile Id Range
                IsValidated = Validate_Range((byte)1, DayProfile.MAX_Day_Profile, DayProfile.Day_Profile_ID);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid DayProfile ID {0}", DayProfile.Day_Profile_ID);
                    return IsValidated;
                }
                schedule_Count = DayProfile.dayProfile_Schedule.Count;
                ///Validate dayProfile_Schedule Counts
                IsValidated = Validate_Range((byte)1, TimeSlot.MAX_TimeSlot, schedule_Count);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Zero or Max dayProfile_Schedule Count {0}", schedule_Count);
                    return IsValidated;
                }
                ///Validate dayProfile_Schedule
                foreach (var dayProfile_Sch in DayProfile.dayProfile_Schedule)
                {
                    IsValidated = Validate_TimeSchedule(dayProfile_Sch, ref ErrorMessage);
                    if (!IsValidated)
                    {
                        ErrorMessage = String.Format("Invalid dayProfile_Schedule {0},{1}", dayProfile_Sch, ErrorMessage);
                        return IsValidated;
                    }
                }
                ///Validate First Slot Should be Zero Hour
                IsValidated = (DayProfile.dayProfile_Schedule[0].StartTime == TimeSlot.StartTime_Min);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("First dayProfile_Schedule StartTime Should be Zero Hour");
                    return IsValidated;
                }
                ///Validate dayProfile_Schedule StartTime Overlapping
                TimeSlot OverLapSlot = null;
                IsValidated = !IsTimeSlicesOverlapping(DayProfile.dayProfile_Schedule, out OverLapSlot);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("DayProfile_Schedule TimeSlot Overlapping {0}", OverLapSlot);
                    return IsValidated;
                }
            }
            catch
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error occured while exec Validate_DayProfile");
            }
            return IsValidated;
        }

        public static bool Validate_DayProfile(Param_DayProfile Param_DayProfiles, ref String ErrorMessage)
        {
            bool IsValidated = false;
            try
            {
                ///Validate ParamDayProfile
                IsValidated = Validate_Range((byte)1, DayProfile.MAX_Day_Profile, Param_DayProfiles.DayProfileCount);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Validation Error,Invalid DayProfile Count {0}", Param_DayProfiles.Count<DayProfile>());
                    return IsValidated;
                }
                ///Validate DayProfile
                foreach (var d_Profile in Param_DayProfiles)
                {
                    IsValidated = Validate_DayProfile(d_Profile, ref ErrorMessage);
                    if (!IsValidated)
                    {
                        ErrorMessage = String.Format("Validation Error,Invalid DayProfile {0}.\r\n{1}", d_Profile, ErrorMessage);
                        return IsValidated;
                    }
                }
            }
            catch
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error occured while exec Validate_DayProfile");
            }
            return IsValidated;
        }

        public static bool Validate_WeekProfile(WeekProfile WeekProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                #region ///Validate WeekProfile Name

                IsValidated = !(String.IsNullOrEmpty(WeekProfile.Week_Profile_Name) ||
                            String.IsNullOrWhiteSpace(WeekProfile.Week_Profile_Name));
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid WeekProfile Name");
                    return IsValidated;
                }

                #endregion
                #region ///Validate day_Profile_MON

                IsValidated = !(WeekProfile.Day_Profile_MON == null) ||
                            Validate_DayProfile(WeekProfile.Day_Profile_MON, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                #region ///Validate day_Profile_TUE

                IsValidated = !(WeekProfile.Day_Profile_TUE == null) ||
                            Validate_DayProfile(WeekProfile.Day_Profile_TUE, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                #region ///Validate day_Profile_WED

                IsValidated = !(WeekProfile.Day_Profile_WED == null) ||
                            Validate_DayProfile(WeekProfile.Day_Profile_WED, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                #region ///Validate day_Profile_THRU

                IsValidated = !(WeekProfile.Day_Profile_THRU == null) ||
                            Validate_DayProfile(WeekProfile.Day_Profile_THRU, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                #region ///Validate day_Profile_FRI

                IsValidated = !(WeekProfile.Day_Profile_FRI == null) ||
                            Validate_DayProfile(WeekProfile.Day_Profile_FRI, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                #region ///Validate day_Profile_SAT

                IsValidated = !(WeekProfile.Day_Profile_SAT == null) ||
                            Validate_DayProfile(WeekProfile.Day_Profile_SAT, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                #region ///Validate Day_Profile_SUN

                IsValidated = !(WeekProfile.Day_Profile_SUN == null) ||
                            Validate_DayProfile(WeekProfile.Day_Profile_SUN, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
            }
            catch
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error occurred while exec Validate_WeekProfile");
            }
            return IsValidated;
        }

        public static bool Validate_WeekProfile(Param_WeeKProfile Param_WeekProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            try
            {
                ///Validate Param_WeekProfile
                IsValidated = Validate_Range((byte)1, WeekProfile.MAX_Week_Profile_Count, Param_WeekProfile.Count<WeekProfile>());
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Validation Error,Invalid ParamWeekProfile Count {0}", Param_WeekProfile.Count<WeekProfile>());
                    return IsValidated;
                }
                ///Validate Param_WeekProfile
                foreach (var w_Profile in Param_WeekProfile)
                {
                    IsValidated = Validate_WeekProfile(w_Profile, ref ErrorMessage);
                    if (!IsValidated)
                    {
                        ErrorMessage = String.Format("Validation Error,Invalid WeekProfile {0}\r\n{1}", w_Profile, ErrorMessage);
                        return IsValidated;
                    }
                }
            }
            catch (Exception ex)
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error occurred while exec Validate_WeekProfile,{0}", ex.Message);
            }
            return IsValidated;
        }

        public static bool IsSeasonProfilesOverlapping(List<SeasonProfile> SeasonProfiles, out SeasonProfile OverlappingSeasonProfile)
        {
            OverlappingSeasonProfile = null;
            try
            {
                //Sort SeasonProfile Based On Start Time Values //ahmed
                //SeasonProfiles.Sort((x, y) => (((x == null) ? -1 : (y == null) ? 1 : x.Start_Date.CompareTo(y.Start_Date))));
                for (int index = 0; index < SeasonProfiles.Count - 1; index++)
                {
                    // If two timeSlices equals
                    if (SeasonProfiles[index] == null ||
                        SeasonProfiles[index + 1] == null ||
                        SeasonProfiles[index].Start_Date.CompareTo(SeasonProfiles[index + 1].Start_Date) >= 0)
                    {
                        OverlappingSeasonProfile = SeasonProfiles[index];
                        return true;
                    }
                    else
                    {
                        //(SeasonProfile_1 < SesasonProfile_2) ==>True 
                        //if (SeasonProfiles[index].Profile_Name.CompareTo(SeasonProfiles[index + 1].Profile_Name) == -1)
                        continue;
                        //else
                        //{
                        //    OverlappingSeasonProfile = SeasonProfiles[index];
                        //    return true;
                        //}
                    }
                }
                OverlappingSeasonProfile = null;
                return false;
            }
            catch
            {
                OverlappingSeasonProfile = null;
                return false;
                ///throw ex;
            }
            finally
            {
                //SeasonProfiles.Sort((x, y) => x.Profile_Name.CompareTo(y.Profile_Name));
                //Sort List Based On Season ProfileName
            }
        }

        public static bool Validate_SeasonProfile(SeasonProfile SeasonProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            try
            {
                #region ///Validate SeasonProfile Name

                IsValidated = !(String.IsNullOrEmpty(SeasonProfile.Profile_Name) ||
                            String.IsNullOrWhiteSpace(SeasonProfile.Profile_Name));
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid SeasonProfile Name");
                    return IsValidated;
                }

                #endregion
                #region ///Validate Start_Date

                IsValidated = SeasonProfile.Start_Date != null && SeasonProfile.Start_Date.IsDateValid;
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid SeasonProfile Start Date {0}", SeasonProfile.Start_Date);
                    return IsValidated;
                }

                #endregion
                #region ///Validate SeasonProfile.Week_Profile

                IsValidated = Validate_WeekProfile(SeasonProfile.Week_Profile, ref ErrorMessage);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid WeekProfile {0}\r\n{1}", SeasonProfile.Week_Profile, ErrorMessage);
                    return IsValidated;
                }

                #endregion
                return IsValidated;
            }
            catch
            {
                IsValidated = false;
            }
            return IsValidated;
        }

        public static bool Validate_SeasonProfile(Param_SeasonProfile SeasonProfile_Objs, ref String ErrorMessage)
        {
            bool IsValidated = false;
            try
            {
                #region ///Validate Param_SeasonProfile

                IsValidated = Validate_Range((byte)1, SeasonProfile.MAX_Season_Profiles, SeasonProfile_Objs.seasonProfile_Table.Count);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Validation Error,Invalid Param_SeasonProfile Count {0}", SeasonProfile_Objs.seasonProfile_Table.Count);
                    return IsValidated;
                }

                #endregion
                #region ///Validate SeasonProfile_Objs

                foreach (var season_Obj in SeasonProfile_Objs)
                {
                    IsValidated = Validate_SeasonProfile(season_Obj, ref ErrorMessage);
                    if (!IsValidated)
                    {
                        ErrorMessage = String.Format("Validation Error,Invalid SeasonProfile {0}\r\n{1}", season_Obj, ErrorMessage);
                        return IsValidated;
                    }
                }

                #endregion
                #region ///Validate SeasonProfile_Objs Overlapping

                SeasonProfile Season_Obj = null;
                IsValidated = !IsSeasonProfilesOverlapping(SeasonProfile_Objs.seasonProfile_Table, out Season_Obj);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Validation Error,Invalid SeasonProfile Overlapping {0}", Season_Obj);
                    return IsValidated;
                }

                #endregion
            }
            catch (Exception ex)
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error Occurred while exec Validate_SeasonProfile,{0}", ex.Message);
            }
            return IsValidated;
        }

        public static bool Validate_SpecialDayProfile(SpecialDay SpecialDayProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                #region ///Validate SpecialDayId

                IsValidated = Validate_Range((uint)1, SpecialDay.MAX_Special_Days, SpecialDayProfile.SpecialDayID);
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid SpecialDayProfile Id {0}", SpecialDayProfile.SpecialDayID);
                    return IsValidated;
                }

                #endregion
                #region ///Validate SpecialDayProfile Start_Date

                IsValidated = SpecialDayProfile.StartDate != null && SpecialDayProfile.StartDate.IsDateValid;
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Invalid SeasonProfile Start Date {0}", SpecialDayProfile.StartDate);
                    return IsValidated;
                }

                #endregion
                #region ///Validate DayProfile

                IsValidated = Validate_DayProfile(SpecialDayProfile.DayProfile, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                return IsValidated;
            }
            catch
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Validation Error,error occured while exec Validate_DayProfile");
            }
            return IsValidated;
        }

        public static bool IsSpecialDayProfileOverlapping(List<SpecialDay> SpecialDayProfiles, out SpecialDay OverlappingSpecialDayProfile,
            uint SpecialDayMaxCount = SpecialDay.MAX_Special_Days)
        {
            try
            {
                ///Sort SeasonProfile Based On StartTime Value
                ///SpecialDayProfiles.Sort((x, y) => ((x == null) ? -1 : x.StartDate.CompareTo(y.StartDate)));
                for (int index = 0; index < SpecialDayProfiles.Count - 1
                                    && index < SpecialDayMaxCount - 1; index++)
                {
                    ///If two SeasonProfile are equals
                    if (SpecialDayProfiles[index] == null ||
                        SpecialDayProfiles[index + 1] == null ||
                        SpecialDayProfiles[index].StartDate.CompareTo(SpecialDayProfiles[index + 1].StartDate) >= 0)
                    {
                        OverlappingSpecialDayProfile = SpecialDayProfiles[index + 1];
                        return true;
                    }
                    ///Optional Check Condition(For Consecutive Special Day Profile Id Non-Empty )
                    else if (SpecialDayProfiles[index].SpecialDayID + 1 != SpecialDayProfiles[index + 1].SpecialDayID)
                    {
                        OverlappingSpecialDayProfile = SpecialDayProfiles[index];
                        return true;
                    }
                }
                OverlappingSpecialDayProfile = null;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Validate_SpecialDayProfile(Param_SpecialDay SpecialDayProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                #region ///Validate_SpecialDay

                IsValidated = Validate_Range((uint)0, SpecialDay.MAX_Special_Days, SpecialDayProfile.Count<SpecialDay>());
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Validation Error,Invalid SpecialDayProfiles Count {0}", SpecialDayProfile.Count<SpecialDay>());
                    return IsValidated;
                }

                #endregion
                #region ///Validate SpecialDayProfile Table

                foreach (var spDayProfile in SpecialDayProfile)
                {
                    IsValidated = Validate_SpecialDayProfile(spDayProfile, ref ErrorMessage);
                    if (!IsValidated)
                    {
                        ErrorMessage = String.Format("Validation Error,Invalid SpecialDayProfile {0}\r\n{1}", spDayProfile, ErrorMessage);
                        return IsValidated;
                    }
                }
                //return IsValidated;

                #endregion
                #region ///Validate SpecialDayProfile Overlapping

                //SpecialDay Sp_DayProfile = null;
                //IsValidated = IsSpecialDayProfileOverlapping(SpecialDayProfile.specialDay_Table, out Sp_DayProfile);
                //if (!IsValidated)
                //{
                //    ErrorMessage = String.Format("Validation Error,SpecialDayProfile Overlapping {0}", Sp_DayProfile);
                //    return IsValidated;
                //}

                #endregion
                return IsValidated;
            }
            catch
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Validation Error,error occurred while Exec Validate_SpecialDayProfile");
            }
            return IsValidated;
        }

        public static bool Validate_Lookup_SeasonProfile_WeekProfileTable(Param_SeasonProfile ParamSeasonProfile,
            Param_WeeKProfile ParamWeekProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                IsValidated = true;
                ///Either Assigned Week Profile Exists in WeekProfile Table  
                foreach (var seasonProfile in ParamSeasonProfile)
                {
                    IsValidated = false;
                    foreach (WeekProfile wkProfile in ParamWeekProfile)
                    {
                        if (wkProfile != null &&
                            wkProfile == seasonProfile.Week_Profile)
                        {
                            IsValidated = true;
                            break;
                        }
                    }
                    if (!IsValidated)
                    {
                        ErrorMessage = String.Format("Validation Error,Unable Lookup Table SeasonProfile.Week_Profile {0}",
                            seasonProfile.Week_Profile);
                        return IsValidated;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occurred while Exec Validate_Lookup_SeasonProfile_WeekProfileTable" + ex.Message);
            }
            return IsValidated;
        }

        public static bool Validate_Lookup_SpecialDay_DayProfileTable(Param_SpecialDay ParamSpecialDay, Param_DayProfile ParamDayProfile,
            ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                IsValidated = true;
                ///Either assigned SpecialProfileTable DayProfile Exists DayProfileTable 
                foreach (var specialDayProfile in ParamSpecialDay)
                {
                    IsValidated = false;
                    foreach (var dayProfile in ParamDayProfile)
                    {
                        if (specialDayProfile.DayProfile == dayProfile)
                        {
                            IsValidated = true;
                            break;
                        }
                    }
                    if (!IsValidated)
                    {
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", specialDayProfile);
                        return IsValidated;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = String.Format("Error occured while Exec Validate_Lookup_SpecialDay_DayProfileTable" + ex.Message);
            }
            return IsValidated;
        }

        public static bool Validate_Lookup_WeekProfile_DayProfileTable(Param_WeeKProfile ParamWeekProfile,
            Param_DayProfile ParamDayProfile, ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                IsValidated = true;
                ///Either assigned WeekProfile Day Profile Exists in DayProfile Table 
                foreach (var weekProfile in ParamWeekProfile)
                {
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_MON) == null)         ///Monday Day Profile Never exists
                    {
                        IsValidated = false;
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", weekProfile.Day_Profile_MON);
                    }
                    else if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_TUE) == null)     ///Tuesday Day Profile Never exists
                    {
                        IsValidated = false;
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", weekProfile.Day_Profile_TUE);
                    }
                    else if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_WED) == null)     ///Wednesday Day Profile Never exists
                    {
                        IsValidated = false;
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", weekProfile.Day_Profile_WED);
                    }
                    else if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_THRU) == null)     ///Thursday Day Profile Never exists
                    {
                        IsValidated = false;
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", weekProfile.Day_Profile_THRU);
                    }
                    else if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_FRI) == null)     ///Friday Day Profile Never exists
                    {
                        IsValidated = false;
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", weekProfile.Day_Profile_FRI);
                    }
                    else if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_SAT) == null)     ///Saturday Day Profile Never exists
                    {
                        IsValidated = false;
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", weekProfile.Day_Profile_SAT);
                    }
                    else if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_SUN) == null)     ///Sunday Day Profile Never exists
                    {
                        IsValidated = false;
                        ErrorMessage = String.Format("Validation Error,Unable to Lookup Table dayProfile {0}", weekProfile.Day_Profile_SUN);
                    }
                    else
                        IsValidated = true;
                    if (!IsValidated)
                        return IsValidated;
                }
                return IsValidated;
            }
            catch (Exception ex)
            {
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error occured while Exec Validate_Lookup_WeekProfile_DayProfileTable" + ex.Message);
            }
            return IsValidated;
        }

        public static bool Validate_Param_TOD_CalendarName(String Calendar_Name, ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                IsValidated = !(String.IsNullOrEmpty(Calendar_Name) ||
                                String.IsNullOrWhiteSpace(Calendar_Name));
                if (!IsValidated)
                {
                    ErrorMessage = String.Format("Validation Error,Invalid CalendarName Active {0}", Calendar_Name);
                }
            }
            catch
            {
                ErrorMessage = String.Format("Validation Error,Invalid CalendarName Active {0}", Calendar_Name);
                IsValidated = false;
            }
            return IsValidated;
        }

        public static bool Validate_Param_TariffOfDay(Param_ActivityCalendar Param_ActivityCalendar, ref String ErrorMessage)
        {
            bool IsValidated = false;
            ErrorMessage = String.Empty;
            try
            {
                #region ///Validate CalendarName

                IsValidated = Validate_Param_TOD_CalendarName(Param_ActivityCalendar.CalendarName, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }
                IsValidated = Validate_Param_TOD_CalendarName(Param_ActivityCalendar.CalendarNamePassive, ref ErrorMessage);
                //if (!IsValidated)
                //{
                //    return IsValidated;
                //}

                #endregion

                #region Validate_Lookup

                IsValidated = Validate_Lookup_SeasonProfile_WeekProfileTable(Param_ActivityCalendar.ParamSeasonProfile, Param_ActivityCalendar.ParamWeekProfile, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }
                IsValidated = Validate_Lookup_WeekProfile_DayProfileTable(Param_ActivityCalendar.ParamWeekProfile, Param_ActivityCalendar.ParamDayProfile, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }
                IsValidated = Validate_Lookup_SpecialDay_DayProfileTable(Param_ActivityCalendar.ParamSpecialDay, Param_ActivityCalendar.ParamDayProfile, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }

                #endregion
                ///Validate ActivityCalendar
                IsValidated = Validate_DayProfile(Param_ActivityCalendar.ParamDayProfile, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }
                IsValidated = Validate_WeekProfile(Param_ActivityCalendar.ParamWeekProfile, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }
                IsValidated = Validate_SeasonProfile(Param_ActivityCalendar.ParamSeasonProfile, ref ErrorMessage);
                if (!IsValidated)
                {
                    return IsValidated;
                }
                IsValidated = Validate_SpecialDayProfile(Param_ActivityCalendar.ParamSpecialDay, ref ErrorMessage);
                return IsValidated;
            }
            catch (Exception ex)
            {
                IsValidated = false;
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error occurred while Exec Validate_TariffOfDay,{0}", ex.Message);
            }
            return IsValidated;
        }

        #endregion

        #endregion

        #region Validate_LimitValues

        public static bool Validate_Param_Limits(Param_Limits paramLimitsObj, ThreshouldItem limitItem, ref String errorMessage,
            LimitValues limitValues = null)
        {
            bool isValidated = false;
            double val = double.MinValue;
            double min_Val = double.MinValue;
            double max_Val = double.MinValue;
            errorMessage = String.Empty;
            try
            {
                try
                {
                    if (limitValues == null)
                        limitValues = Common_PCL.AppValidationInfo._LimitValues;
                    val = paramLimitsObj.GetLimit(limitItem);
                    min_Val = limitValues.GetLimitMin(limitItem);
                    max_Val = limitValues.GetLimitMax(limitItem);
                    errorMessage = String.Format("Error Validating {0},Value {1}:Range[{2}-{3}]", limitItem, val, min_Val, max_Val);
                }
                catch (Exception ex)
                {
                    errorMessage = String.Format("Error validating {0}_Limit,{1}", limitItem, ex.Message);
                    isValidated = false;
                    //throw new InvalidOperationException(errorMessage, ex);
                }
                isValidated = App_Validation.Validate_Range(min_Val, max_Val, val);
                return isValidated;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while exec Validate_Param_Limits,{0}", ex.Message), ex);
            }
        }
        //
        public static bool Validate_Param_Limits(Param_Limits paramLimitsObj, ref String errorMessage,
            LimitValues limitValues = null, List<ThreshouldItem> limitItems = null)
        {
            bool isValidated = false;
            errorMessage = String.Empty;
            try
            {
                if (limitItems == null || limitItems.Count <= 0)
                {
                    if (limitItems == null)
                        limitItems = new List<ThreshouldItem>();
                    var enum_Vals = Enum.GetValues(typeof(ThreshouldItem));
                    foreach (ThreshouldItem MthItem in enum_Vals)
                    {
                        limitItems.Add(MthItem);
                    }
                }
                if (limitValues == null)
                    limitValues = Common_PCL.AppValidationInfo._LimitValues;

                foreach (ThreshouldItem MThItem in limitItems)
                {
                    if (Commons.IsQc && MThItem >= ThreshouldItem.OverCurrent_Phase) //Ignore 1P Limits
                        continue;
                    isValidated = Validate_Param_Limits(paramLimitsObj, MThItem, ref errorMessage);
                    if (!isValidated)
                        return isValidated;
                }
                return isValidated;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while exec Validate_Param_Limits,{0}", ex.Message), ex);
            }
        }

        //Added by Azeem with AccessRights Parameter, Commented PCL
    //    public static bool Validate_Param_Limits(Param_Limits paramLimitsObj, ref String errorMessage, List<AccessRights> _accessRightsList,
    //LimitValues limitValues = null, List<ThreshouldItem> limitItems = null)
    //    {
    //        bool isValidated = false;
    //        errorMessage = String.Empty;
    //        try
    //        {
    //            if (limitItems == null || limitItems.Count <= 0)
    //            {
    //                if (limitItems == null)
    //                    limitItems = new List<ThreshouldItem>();
    //                var enum_Vals = Enum.GetValues(typeof(ThreshouldItem));
    //                foreach (ThreshouldItem MthItem in enum_Vals)
    //                {
    //                    limitItems.Add(MthItem);
    //                }
    //            }
    //            if (limitValues == null)
    //                limitValues = Common_PCL.AppValidationInfo._LimitValues;

    //            foreach (ThreshouldItem MThItem in limitItems)
    //            {
    //                //foreach (AccessRights ar in _accessRightsList)
    //                //{
    //                //    if(ar.QuantityName==MThItem)
    //                //}

    //                isValidated = Validate_Param_Limits(paramLimitsObj, MThItem, ref errorMessage);
    //                if (!isValidated)
    //                    return isValidated;
    //            }
    //            return isValidated;
    //        }
    //        catch (InvalidOperationException)
    //        {
    //            throw;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(String.Format("Error occurred while exec Validate_Param_Limits,{0}", ex.Message), ex);
    //        }
    //    }

        public static bool Validate_Param_Limits(Param_Limit_Demand_OverLoad paramLimitDemandOverLoad
            , ThreshouldItem limitItem, ref String errorMessage, LimitValues limitValues = null)
        {
            bool isValidated = false;
            double val = double.MinValue;
            double min_Val = double.MinValue;
            double max_Val = double.MinValue;
            errorMessage = String.Empty;
            try
            {
                try
                {
                    if (limitValues == null)
                        limitValues = Common_PCL.AppValidationInfo._LimitValues;

                    val = paramLimitDemandOverLoad.Threshold;
                    min_Val = limitValues.GetLimitMin(limitItem);
                    max_Val = limitValues.GetLimitMax(limitItem);
                    errorMessage = String.Format("Error Validating {0},Value {1}:Range[{2}-{3}]", limitItem, val, min_Val, max_Val);
                }
                catch (Exception ex)
                {
                    errorMessage = String.Format("Error validating {0}_Limit,{1}", limitItem, ex.Message);
                    throw new InvalidOperationException(errorMessage, ex);
                }
                isValidated = App_Validation.Validate_Range(min_Val, max_Val, val);
                return isValidated;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while exec Validate_Param_Limits,{0}", ex.Message), ex);
            }
        }

        public static bool Validate_Param_Limits(Param_Limit_Demand_OverLoad[] paramLimitDemandOverLoads
            , ref String errorMessage, LimitValues limitValues = null)
        {
            bool isValidated = false;
            errorMessage = String.Empty;
            try
            {
                if (limitValues == null)
                    limitValues = Common_PCL.AppValidationInfo._LimitValues;

                ThreshouldItem[] MDIExceed_Items = new ThreshouldItem[] { ThreshouldItem.MDIExceed_T1, 
                    ThreshouldItem.MDIExceed_T2,
                    ThreshouldItem.MDIExceed_T3, 
                    ThreshouldItem.MDIExceed_T4 };

                for (int index = 0; (index < paramLimitDemandOverLoads.Length &&
                                     index < MDIExceed_Items.Length); index++)
                {
                    isValidated = App_Validation.Validate_Param_Limits(paramLimitDemandOverLoads[index], MDIExceed_Items[index],
                                            ref errorMessage, limitValues);
                    if (!isValidated)
                        return isValidated;
                }
                return isValidated;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while exec Validate_Param_Limits,{0}", ex.Message), ex);
            }
        }

        #endregion

        #region Validate_MonitoringTimeValues

        public static bool Validate_Param_MonitoringTime(Param_Monitoring_Time paramMonitoringTimeObj, MonitoringTimeItem monitorItem,
            ref String errorMessage, MonitoringTimeValues monitorTimeValue = null)
        {
            bool isValidated = false;
            TimeSpan val = new TimeSpan(0);
            TimeSpan min_Val = TimeSpan.MinValue;
            TimeSpan max_Val = TimeSpan.MinValue;
            errorMessage = String.Empty;
            try
            {
                try
                {
                    if (monitorTimeValue == null)
                        monitorTimeValue = Common_PCL.AppValidationInfo._MonitorTimeValues;

                    val = paramMonitoringTimeObj.GetMonitorTime(monitorItem);
                    min_Val = monitorTimeValue.GetMonitorTime_Min(monitorItem);
                    max_Val = monitorTimeValue.GetMonitorTime_Max(monitorItem);
                    errorMessage = String.Format("Error Validating {0},Value {1}:Range[{2}-{3}]", monitorItem, val, min_Val, max_Val);
                }
                catch (Exception ex)
                {
                    errorMessage = String.Format("Error validating {0}_MonitoringTime,{1}", monitorItem, ex.Message);
                    throw new InvalidOperationException(errorMessage, ex);
                }
                isValidated = App_Validation.Validate_Range(min_Val, max_Val, val);
                return isValidated;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while exec Validate_Param_MonitoringTime,{0}", ex.Message), ex);
            }
        }

        public static bool Validate_Param_MonitoringTime(Param_Monitoring_Time paramMonitoringTimeObj, ref String errorMessage,
            MonitoringTimeValues monitorTimeValues = null, List<MonitoringTimeItem> monitorItems = null)
        {
            bool isValidated = false;
            errorMessage = String.Empty;
            try
            {
                if (monitorItems == null || monitorItems.Count <= 0)
                {
                    if (monitorItems == null)
                        monitorItems = new List<MonitoringTimeItem>();
                    var enum_Vals = Enum.GetValues(typeof(MonitoringTimeItem));
                    foreach (MonitoringTimeItem MthItem in enum_Vals)
                    {
                        monitorItems.Add(MthItem);
                    }
                }
                if (monitorItems == null)
                    monitorTimeValues = Common_PCL.AppValidationInfo._MonitorTimeValues;

                foreach (MonitoringTimeItem MThItem in monitorItems)
                {
                    isValidated = Validate_Param_MonitoringTime(paramMonitoringTimeObj, MThItem, ref errorMessage);
                    if (!isValidated)
                        return isValidated;
                }
                return isValidated;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while exec Validate_Param_MonitoringTime,{0}", ex.Message), ex);
            }
        }

        public static void Apply_ValidationResult(bool IsValidated, String ErrorMessage, Control given_Ctrl, ErrorProvider errPro)
        {
            try
            {
                App_Validation_Info ValidationInfo = Commons.AppValidationInfo;
                if (IsValidated)
                {
                    //Reset,Clear Error
                    errPro.SetError(given_Ctrl, String.Empty);
                    given_Ctrl.ForeColor = ValidationInfo.ValidatedColorScheme;
                }
                else
                {
                    errPro.SetError(given_Ctrl, ErrorMessage);
                    given_Ctrl.ForeColor = ValidationInfo.InValidatedColorScheme;
                }
            }
            catch { }
        }

        #endregion
    }
}
