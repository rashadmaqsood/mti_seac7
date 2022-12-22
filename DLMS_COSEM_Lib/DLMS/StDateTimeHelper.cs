using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DLMS.Comm
{
    #region HelperStDateTime

    public partial class StDateTime
    {
        /// <summary>
        /// HelperStDateTime
        /// </summary>
        public class StDateTimeHelper
        {
            #region Validation_String_Formats

            /// <summary>
            /// FullDateTimeRegEx,Compliant Format RFC 2822,To validate Long Full Date_Time With Date&Time_Wild cards
            /// Valid Date & Time Strings
            ///Any, LST ANY ANY 18:44:56 -0500
            ///Fri, 01 May 2014 11:05:00 +0500
            ///Fri, 01 May 2014 11:05:00 -0500
            ///Fri, 01 May 2014 11:05:00 (Null GMT)
            /// </summary>
            public const string FullDateTimeWildCardRegEx =
                @"^(?<DateTimeWildCard>\s*(?<DayOfWeek_>___|ANY|Sun|Mon|Tue|Wed|Thu|Fri|Sat),\s*)?(?<DayOfMonth>0?[1-9]|[1-2][0-9]|3[01]|ANY|LST|"
                + @"2LST|__|_)\s+(?<Month>ANY|DLSB|DLSE|Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|___)\s+"
                + @"(?<Year>19[0-9]{2}|[2-9][0-9]{3}|[0-9]{2}|ANY|____)\s+"
                + @"(?<Hour>2[0-3]|[0-1][0-9]|ANY|__):(?<Minute>[0-5][0-9]|ANY|__):(?<Second>(60|[0-5][0-9]|ANY|__))?\s+"
                + @"(?<GMT>[-\+][0-9]{2}[0-5][0-9]|(?:UT|GMT|(?:E|C|M|P)(?:ST|DT)|[A-IK-Z])|ANY|___||\s*)"
                + @"(\s*\((\\\(|\\\)|(?<=[^\\])\((?<C>)|(?<=[^\\])\)(?<-C>)|[^\(\)]*)*(?(C)(?!))\))*\s*$";

            /// <summary>
            /// FullDateTimeRegEx,To validate Long_Full Date_Time With No Date_Time Wild cards support
            /// Valid Date & Time Strings
            ///Fri, 01 May 2014 11:05:00 +0500
            ///Fri, 01 May 2014 11:05:00 -0500
            ///Fri, 01 May 2014 11:05:00 (Null GMT)
            /// </summary>
            public const string FullDateTimeRegEx =
                @"^(?<LongDateTime>\s*(?<DayOfWeek_>Sun|Mon|Tue|Wed|Thu|Fri|Sat),\s*)?(?<DayOfMonth>0?[1-9]|[1-2][0-9]|3[01])\s+"
                + @"(?<Month>Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s+(?<Year>19[0-9]{2}|[2-9][0-9]{3}|[0-9]{2})\s+"
                + @"(?<Hour>2[0-3]|[0-1][0-9]):(?<Minute>[0-5][0-9]):(?<Second>(60|[0-5][0-9]))?\s+"
                + @"(?<GMT>[-\+][0-9]{2}[0-5][0-9]|(?:UT|GMT|(?:E|C|M|P)(?:ST|DT)|[A-IK-Z])||\s*)(\s*\((\\\(|\\\)|(?<=[^\\])\((?<C>)|(?<=[^\\])\)(?<-C>)|[^\(\)]*)*(?(C)(?!))\))*\s*$";


            /// <summary>
            /// FullDateWildCardRegEx,Compliant Format RFC 2822,To validate Long Full Date With Date_Wild cards
            /// Valid Date Strings
            ///Any, LST ANY ANY
            ///___, 01 May 2014
            ///Fri, __ May 2014
            ///___, __ ___ 2014
            /// </summary>
            public const string FullDateWildCardRegEx =
                @"^(?<LongDateWildCard>\s*(?<DayOfWeek_>___|ANY|Sun|Mon|Tue|Wed|Thu|Fri|Sat),\s*)?" +
                @"(?<DayOfMonth>0?[1-9]|[1-2][0-9]|3[01]|ANY|LST|2LST|__|_)\s+" +
                @"(?<Month>ANY|DLSB|DLSE|Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|___)\s+" +
                @"(?<Year>19[0-9]{2}|[2-9][0-9]{3}|[0-9]{2}|ANY|____)\s+\s*$";


            /// <summary>
            /// ShortDateRegEx,To validate Short DateTime DayOfMonth,Month(Without_WildCard Support)
            /// Valid Date & Time Strings
            ///01 May
            ///28 FEB
            ///29 FEB
            /// </summary>
            public const string ShortDateRegEx =
                @"^((?<DayOfMonth31>31(?! (FEB|APR|JUN|SEP|NOV)))|(?<DayOfMonth30>30(?! FEB))|" +
                @"(?<DayOfMonth>0?[1-9]|1\d|2[0-9]))\s*(?<Month>JAN|FEB|MAR|MAY|APR|JUL|JUN|AUG|OCT|SEP|NOV|DEC)$";

            /// <summary>
            /// FullTimeRegEx,StDatetime(interval) To 24 Hour Time Format (With No WildCard Support)
            /// Valid Date & Time Strings
            ///23:59:00
            ///22:00:00
            ///01:59:59
            /// </summary>	
            public const string FullTimeRegEx =
            @"^(?<Hour>([0-1]?[0-9])|([2][0-3])):(?<Minute>[0-5]?[0-9]):?(?<Second>([0-5]?[0-9]))?$";

            /// <summary>
            /// ShortIntervalRegEx,StDateTime(Interval)(Format)mm:ss
            ///Valid Strings
            ///14:59
            ///9:00
            ///10:59
            ///59:59
            /// </summary>	    
            public const string ShortIntervalRegEx =
                @"^(?<Minute>[0-5]?[0-9]):(?<Second>([0-5]?[0-9]))$";

            /// <summary>
            /// ShortFixedIntervalRegEx,StDateTime(Fixed_Interval)(Format)mm:ss
            ///Valid Strings
            ///14:59
            ///9:00
            ///10:59
            /// </summary>	    
            public const string ShortFixedIntervalRegEx =
                @"^(?<Minute>([0-1]?[0-4]|[0]?[1-9])):(?<Second>([0-5]?[0-9]))$";

            /// <summary>
            /// ShortIntervalTimeSinkRegEx,StDateTime(Interval Time Sink)(Format)mm:ss
            ///Valid Strings
            ///06:59
            ///5:59
            ///10:10
            ///20:50
            ///Invalid Strings
            ///11:59
            ///07:00
            /// </summary>	    
            public const string ShortIntervalTimeSinkRegEx =
                @"^(?<Minute>([0]?[1-6]|10|12|15|20)):(?<Second>([0-5]?[0-9]))$";

            #endregion

            #region Date_Time_Formats

            /// <summary>
            /// (RFC 2822 Format)ddd, dd MMM YYYY HH:mm:ss UTC
            /// </summary>
            public const string FullDateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss";

            /// <summary>
            /// (RFC 2822 Format)ddd, dd MMM YYYY
            /// </summary>
            public const string FullDateFormat = "ddd, dd MMM yyyy";

            /// <summary>
            /// (Format)dd MMM
            /// </summary>
            public const string ShortDateFormat = "dd MMM";

            // (Format)dd MMM
            /// </summary>
            public const string FullTimeFormat = "HH:mm:ss";

            // (Format)mm:ss
            /// </summary>
            public const string ShortTimeIntervalFormat = "mm:ss";

            #endregion

            public static void GetCustomFormatMessage(dtpCustomExtensions mvarFormatEx,
                                                      ref string CustomFormat,
                                                      ref string mvarCustomFormatMessage)
            {
                CultureInfo CurrentCulture = new CultureInfo("en-us");
                switch (mvarFormatEx)
                {
                    #region Switch_Cases

                    case dtpCustomExtensions.dtpCustom:
                        mvarCustomFormatMessage = CustomFormat;
                        break;
                    case dtpCustomExtensions.dtpLong:
                        mvarCustomFormatMessage = "Long Date (" +
                            DateTime.Now.ToLongDateString() + ")";
                        CustomFormat = CurrentCulture.DateTimeFormat.LongDatePattern;
                        break;
                    case dtpCustomExtensions.dtpShort:
                        mvarCustomFormatMessage = "Short Date (" +
                            DateTime.Now.ToShortDateString() + ")";
                        CustomFormat = CurrentCulture.DateTimeFormat.ShortDatePattern;
                        break;
                    case dtpCustomExtensions.dtpTime:
                        mvarCustomFormatMessage = "Long Time AM/PM (" +
                            DateTime.Now.ToLongTimeString() + ")";
                        CustomFormat = CurrentCulture.DateTimeFormat.LongTimePattern;
                        break;
                    case dtpCustomExtensions.dtpDayFullName:
                        mvarCustomFormatMessage = "Day of the Week Full Name (" +
                            DateTime.Now.ToString("dddd", CurrentCulture) + ")";
                        CustomFormat = "dddd";
                        break;
                    case dtpCustomExtensions.dtpDayShortName:
                        mvarCustomFormatMessage = "Day of the Week Short Name (" +
                            DateTime.Now.ToString("ddd", CurrentCulture) + ")";
                        CustomFormat = "ddd";
                        break;
                    case dtpCustomExtensions.dtpDayOfMonth:
                        mvarCustomFormatMessage = "Day of Month (" +
                            DateTime.Now.ToString("dd", CurrentCulture) + ")";
                        CustomFormat = "dd";
                        break;
                    case dtpCustomExtensions.dtpLongDateLongTime24Hour:
                        mvarCustomFormatMessage = "Long Date Long Time 24 Hour (" +
                            DateTime.Now.ToString("D", CurrentCulture) + " " +
                            DateTime.Now.ToString("HH:mm:ss", CurrentCulture) + ")";
                        CustomFormat = String.Format("{0} HH:mm:ss", CurrentCulture.DateTimeFormat.LongDatePattern);
                        break;
                    case dtpCustomExtensions.dtpLongDateLongTimeAMPM:
                        mvarCustomFormatMessage = "Long Date Long Time AM/PM (" +
                            DateTime.Now.ToString("D", CurrentCulture) + " " +
                            DateTime.Now.ToString("hh:mm:ss tt", CurrentCulture) + ")";
                        CustomFormat = String.Format("{0} hh:mm tt ", CurrentCulture.DateTimeFormat.LongDatePattern);
                        break;
                    case dtpCustomExtensions.dtpLongDateShortTime24Hour:
                        mvarCustomFormatMessage = "Long Date Short Time 24 Hour (" +
                            DateTime.Now.ToString("D", CurrentCulture) + " " +
                            DateTime.Now.ToString("HH:mm", CurrentCulture) + ")";
                        CustomFormat = String.Format("{0} HH:mm", CurrentCulture.DateTimeFormat.LongDatePattern);
                        break;
                    case dtpCustomExtensions.dtpLongDateShortTimeAMPM:
                        mvarCustomFormatMessage = "Long Date Short Time AM/PM (" +
                            DateTime.Now.ToString("D", CurrentCulture) + " " +
                            DateTime.Now.ToString("hh:mm tt", CurrentCulture) + ")";
                        CustomFormat = String.Format("{0} hh:mm tt", CurrentCulture.DateTimeFormat.LongDatePattern);
                        break;
                    case dtpCustomExtensions.dtpLongTime24Hour:
                        mvarCustomFormatMessage = "Long Time 24 Hour (" +
                            DateTime.Now.ToString("HH:mm:ss", CurrentCulture) + ")";
                        CustomFormat = "HH:mm:ss";
                        break;
                    case dtpCustomExtensions.dtpMonthFullName:
                        mvarCustomFormatMessage = "Month Full Name (" +
                            DateTime.Now.ToString("MMMM", CurrentCulture) + ")";
                        CustomFormat = "MMMM";
                        break;
                    case dtpCustomExtensions.dtpMonthShortName:
                        mvarCustomFormatMessage = "Month Short Name (" +
                            DateTime.Now.ToString("MMM", CurrentCulture) + ")";
                        CustomFormat = "MMM";
                        break;
                    case dtpCustomExtensions.dtpMonthNameAndDay:
                        mvarCustomFormatMessage = "Month Name and Day (" +
                            DateTime.Now.ToString("MMMM dd", CurrentCulture) + ")";
                        CustomFormat = "MMMM dd";
                        break;
                    case dtpCustomExtensions.dtpShortDateLongTime24Hour:
                        mvarCustomFormatMessage = "Short Date Long Time 24 Hour (" +
                            DateTime.Now.ToString("d", CurrentCulture) + " " +
                            DateTime.Now.ToString("HH:mm:ss", CurrentCulture) + ")";
                        CustomFormat = String.Format("{0} HH:mm:ss", CurrentCulture.DateTimeFormat.ShortDatePattern);
                        break;
                    case dtpCustomExtensions.dtpShortDateLongTimeAMPM:
                        mvarCustomFormatMessage = "Short Date Long Time AM/PM (" +
                            DateTime.Now.ToString("G", CurrentCulture) + ")";
                        CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
                        break;
                    case dtpCustomExtensions.dtpShortDateShortTime24Hour:
                        mvarCustomFormatMessage = " Short Date Short Time 24 Hour (" +
                            DateTime.Now.ToString("d", CurrentCulture) + " " +
                            DateTime.Now.ToString("HH:mm", CurrentCulture) + ")";
                        CustomFormat = "yyyy-MM-dd HH:mm";
                        break;
                    case dtpCustomExtensions.dtpShortDateShortTimeAMPM:
                        mvarCustomFormatMessage = " Short Date Short Time AM/PM (" +
                            DateTime.Now.ToString("d", CurrentCulture) + " " +
                            DateTime.Now.ToString("hh:mm tt", CurrentCulture) + ")";
                        CustomFormat = "yyyy-MM-dd hh:mm tt";
                        break;
                    case dtpCustomExtensions.dtpShortTime24Hour:
                        mvarCustomFormatMessage = "Short Time 24 Hour (" +
                            DateTime.Now.ToString("HH:mm", CurrentCulture) + ")";
                        CustomFormat = "HH:mm";
                        break;
                    case dtpCustomExtensions.dtpShortTimeAMPM:
                        mvarCustomFormatMessage = "Short Time AM/PM (" +
                            DateTime.Now.ToString("hh:mm tt", CurrentCulture) + ")";
                        CustomFormat = "hh:mm tt";
                        break;
                    case dtpCustomExtensions.dtpSortableDateAndTimeLocalTime:
                        mvarCustomFormatMessage = "Sortable Date and Local Time (" +
                            DateTime.Now.ToString("s", CurrentCulture) + ")";
                        CustomFormat = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss";
                        break;
                    case dtpCustomExtensions.dtpUTFLocalDateAndLongTime24Hour:
                        mvarCustomFormatMessage = "UTF Local Date and Long Time 24 Hour (" +
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CurrentCulture) + ")";
                        CustomFormat = "yyyy-MM-dd HH:mm:ss";
                        break;
                    case dtpCustomExtensions.dtpUTFLocalDateAndLongTimeAMPM:
                        mvarCustomFormatMessage = "UTF Local Date and Long Time AM/PM (" +
                            DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt", CurrentCulture) + ")";
                        CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
                        break;
                    case dtpCustomExtensions.dtpUTFLocalDateAndShortTime24Hour:
                        mvarCustomFormatMessage = "UTF Local Date and Short Time 24 Hour (" +
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm", CurrentCulture) + ")";
                        CustomFormat = "yyyy-MM-dd HH:mm";
                        break;
                    case dtpCustomExtensions.dtpUTFLocalDateAndShortTimeAMPM:
                        mvarCustomFormatMessage = "UTF Local Date and Short Time AM/PM (" +
                            DateTime.Now.ToString("yyyy-MM-dd hh:mm tt", CurrentCulture) + ")";
                        CustomFormat = "yyyy-MM-dd hh:mm tt";
                        break;
                    case dtpCustomExtensions.dtpYear4Digit:
                        mvarCustomFormatMessage = "4 Digit Year (" +
                            DateTime.Now.ToString("yyyy", CurrentCulture);
                        CustomFormat = "yyyy";
                        break;
                    case dtpCustomExtensions.dtpYearAndMonthName:
                        mvarCustomFormatMessage = "Year and Month Name (" +
                            DateTime.Now.ToString("Y", CurrentCulture) + ")";
                        CustomFormat = "MMM,yyyy";
                        break;
                    case dtpCustomExtensions.dtpShortDateAMPM:
                        mvarCustomFormatMessage = "Short Date AM/PM (" +
                            DateTime.Now.ToString("d", CurrentCulture) + " " +
                            DateTime.Now.ToString("tt", CurrentCulture) + ")";
                        CustomFormat = "MM-dd-yyyy tt";
                        break;
                    case dtpCustomExtensions.dtpShortDateMorningAfternoon:
                        string AMPM = "AM";
                        if (DateTime.Now.Hour >= 12)
                        {
                            AMPM = "Afternoon";
                        }
                        mvarCustomFormatMessage = "Short Date Morning/Afternoon (" +
                            DateTime.Now.ToString("d", CurrentCulture) + " " + AMPM + ")";
                        CustomFormat = "MM-dd-yyyy tt";
                        break;
                    case dtpCustomExtensions.dtpLongDateTimeWildCard:
                        {
                            mvarCustomFormatMessage = "UTF Full DateTime With Custom WildCard Support \r\n(" +
                                DateTime.Now.ToString(FullDateTimeFormat, CurrentCulture)
                                + ")";
                            CustomFormat = FullDateTimeFormat;
                            break;
                        }
                    case dtpCustomExtensions.dtpLongDateWildCard:
                        {
                            mvarCustomFormatMessage = "UTF Long Date With Custom WildCard Support \r\n(" +
                                DateTime.Now.ToString(FullDateFormat, CurrentCulture)
                                + ")";
                            CustomFormat = FullDateFormat;
                            break;
                        }
                    case dtpCustomExtensions.dtpLongDateTime:
                        {
                            mvarCustomFormatMessage = "UTF Full DateTime(" +
                                DateTime.Now.ToString(FullDateTimeFormat, CurrentCulture) + ")";
                            CustomFormat = FullDateTimeFormat;
                            break;
                        }
                    case dtpCustomExtensions.dtpShortDate:
                        {
                            mvarCustomFormatMessage = "Short Date (" +
                                DateTime.Now.ToString(ShortDateFormat, CurrentCulture) + ")";
                            CustomFormat = ShortDateFormat;
                            break;
                        }
                    case dtpCustomExtensions.dtpLongTime:
                        {
                            mvarCustomFormatMessage = "24 Hour Long Time(Interval) (" +
                                DateTime.Now.ToString(FullTimeFormat, CurrentCulture) + ")";
                            CustomFormat = FullTimeFormat;
                            break;
                        }
                    case dtpCustomExtensions.dtpShortInterval:
                        {
                            mvarCustomFormatMessage = "Short Time Interval Minute:Second(00:00 upto 59:59)";
                            CustomFormat = ShortTimeIntervalFormat;
                            break;
                        }
                    case dtpCustomExtensions.dtpShortIntervalFixed:
                        {
                            mvarCustomFormatMessage = "Short Time Interval[Fixed] Minute:Second(00:00 upto 14:59)";
                            CustomFormat = ShortTimeIntervalFormat;
                            break;
                        }
                    case dtpCustomExtensions.dtpShortIntervalTimeSink:
                        {
                            mvarCustomFormatMessage = "Short Time Interval[Sink] Minute:Second(00:00 upto 20:59,Minute(1-6,10,12,15,20))";
                            CustomFormat = ShortTimeIntervalFormat;
                            break;
                        }
                    #endregion
                }
            }

            #region ToString_StringConversion Methods

            public static string ToStringCustomLongDateTime(StDateTime localDateTimeObj)
            {
                DateTime exact_dt1 = DateTime.Now;
                CultureInfo _current_Culture = new CultureInfo("en-us");
                string txtDateTime = exact_dt1.ToString(FullDateTimeFormat, _current_Culture) + ' ';
                //String localStrYear, localStrMonth, localStrDayOfMonth, localStrDayOfWeek, localStrHour, localStrMinute, localStrSecond, localStrGMT = null;

                try
                {
                    Regex _regex = new Regex(FullDateTimeWildCardRegEx, RegexOptions.IgnoreCase);
                    var match = _regex.Match(txtDateTime);
                    if (match.Success)
                    {
                        ///(RFC 2822 Format)ddd, dd MMM YYYY HH:mm:ss UTC
                        PrintLongDateCustom(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                        ///Printing Time Value
                        PrintTimeCustom(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                    }
                    else
                        throw new FormatException("Unable to verify CustomFormat StoreCustomDate_FromText");
                    ///Concat Final Conversion in Format
                    /// (RFC 2822 Format)ddd, dd MMM YYYY HH:mm:ss UTC
                    //txtDateTime = String.Format("{0}, {1} {2} {3} {4}:{5}:{6} {7}", localStrDayOfWeek, localStrDayOfMonth, localStrMonth, localStrYear,
                    //    localStrHour, localStrMinute, localStrSecond, localStrGMT);
                    txtDateTime = txtDateTime.Trim();
                }
                catch (FormatException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new FormatException("Unable to Validate StDateTime Format,ToStringCustomLongDateTime", ex);
                }
                return txtDateTime;
            }

            public static string ToStringCustomLongDate(StDateTime localDateTimeObj)
            {
                DateTime exact_dt1 = DateTime.Now;
                CultureInfo _current_Culture = new CultureInfo("en-us");
                string txtDateTime = exact_dt1.ToString(FullDateFormat, _current_Culture) + ' ';
                //String localStrYear, localStrMonth, localStrDayOfMonth, localStrDayOfWeek, localStrHour, localStrMinute, localStrSecond, localStrGMT = null;
                try
                {
                    Regex _regex = new Regex(FullDateWildCardRegEx, RegexOptions.IgnoreCase);
                    var match = _regex.Match(txtDateTime);
                    if (match.Success)
                    {
                        ///(RFC 2822 Format)ddd, dd MMM YYYY
                        PrintLongDateCustom(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                    }
                    else
                        throw new FormatException("Unable to verify CustomFormat StoreCustomDate_FromText");
                    ///Concat Final Conversion in Format
                    /// (RFC 2822 Format)ddd, dd MMM YYYY
                    //txtDateTime = String.Format("{0}, {1} {2} {3} {4}:{5}:{6} {7}", localStrDayOfWeek, localStrDayOfMonth, localStrMonth, localStrYear,
                    //localStrHour, localStrMinute, localStrSecond, localStrGMT);
                    txtDateTime = txtDateTime.Trim();
                }
                catch (FormatException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new FormatException("Unable to Validate StDateTime Format,ToStringCustomLongDateTime", ex);
                }
                return txtDateTime;
            }

            #region PrintLongDateCustom

            private static void PrintLongDateCustom(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                ///(RFC 2822 Format)ddd, dd MMM YYYY
                PrintYearCustomYYYY(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                PrintMonthCustomMMM(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                PrintDayOfMonthCustomdd(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                PrintDayOfWeekCustomddd(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
            }

            private static void PrintYearCustomYYYY(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)YYYY
                #region ///Print Year

                ///(?<Year>19[0-9]{2}|[2-9][0-9]{3}|[0-9]{2}|ANY|____)
                if (localDateTimeObj.Year == StDateTime.NullYear)
                {
                    localStrMatch = "____";
                }
                else
                    localStrMatch = localDateTimeObj.Year.ToString("D4");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "Year", localStrMatch);
                //localStrYear = localStrMatch;

                #endregion
            }

            private static void PrintMonthCustomMMM(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)MMM
                #region ///Print Month

                ///Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|___|ANY
                localStrMatch = null;
                if (localDateTimeObj.Month == StDateTime.Null)
                {
                    localStrMatch = "___";
                }
                else if (localDateTimeObj.Month == StDateTime.DaylightSavingBegin)
                {
                    localStrMatch = "DLSB";
                }
                else if (localDateTimeObj.Month == StDateTime.DaylightSavingEnd)
                {
                    localStrMatch = "DLSE";
                }
                else if (localDateTimeObj.Month > 0 && localDateTimeObj.Month <= 12)
                {
                    localStrMatch = _current_Culture.DateTimeFormat.GetMonthName(localDateTimeObj.Month);
                    localStrMatch = localStrMatch.Substring(0, 3);
                }
                else
                    throw new FormatException("Invalid Object Month Format");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "Month", localStrMatch);
                ///localStrMonth = localStrMatch;

                #endregion
            }

            private static void PrintDayOfMonthCustomdd(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)dd
                #region ///Print DayOfMonth

                ///?<DayOfMonth>0?[1-9]|[1-2][0-9]|3[01]|ANY|LST|2LST|__|_
                localStrMatch = null;
                if (localDateTimeObj.DayOfMonth == StDateTime.Null)
                {
                    localStrMatch = "__";
                }
                else if (localDateTimeObj.DayOfMonth == StDateTime.LastDayOfMonth)
                {
                    localStrMatch = "LST";
                }
                else if (localDateTimeObj.DayOfMonth == StDateTime.SecondLastDayOfMonth)
                {
                    localStrMatch = "2LST";
                }
                else if (localDateTimeObj.DayOfMonth > 0 && localDateTimeObj.DayOfMonth <= 31)
                {
                    localStrMatch = localDateTimeObj.DayOfMonth.ToString("D2");
                }
                else
                    throw new FormatException("Invalid Object DayOfMonth Format");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "DayOfMonth", localStrMatch);
                //localStrDayOfMonth = localStrMatch;

                #endregion
            }

            private static void PrintDayOfWeekCustomddd(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)ddd
                #region ///Parse DayOfWeek
                ///(?<DayOfWeek_>___|ANY|Sun|Mon|Tue|Wed|Thu|Fri|Sat)
                localStrMatch = null;
                if (localDateTimeObj.DayOfWeek == StDateTime.Null)
                {
                    localStrMatch = "___";
                }
                else if (localDateTimeObj.DayOfWeek > 0 && localDateTimeObj.DayOfWeek <= 07)
                {
                    localStrMatch = _current_Culture.DateTimeFormat.GetDayName((DayOfWeek)
                        ((localDateTimeObj.DayOfWeek >= 7) ? 0 : localDateTimeObj.DayOfWeek));
                    localStrMatch = localStrMatch.Substring(0, 3);
                }
                else
                    throw new FormatException("Invalid Object DayOfWeek Format");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "DayOfWeek_", localStrMatch);
                //localStrDayOfWeek = localStrMatch;
                #endregion
            }

            #endregion

            private static void PrintTimeCustom(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                ///(RFC 2822 Format)HH:mm:ss UTC 
                ///Printing Time Value
                PrintHourCustomHH(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                PrintMinuteCustom_mm(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                PrintSecondCustom_ss(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
                PrintGMTCustom(ref txtDateTime, _regex, localDateTimeObj, _current_Culture);
            }

            #region PrintHourCustomHH

            private static void PrintHourCustomHH(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)HH
                ///Printing Time Value
                #region ///Print Hour
                ///(?<Hour>2[0-3]|[0-1][0-9]|ANY|__)
                localStrMatch = null;
                if (localDateTimeObj.Hour == StDateTime.Null)
                {
                    localStrMatch = "__";
                }
                else if (localDateTimeObj.Hour >= 0 && localDateTimeObj.Hour <= 24)
                {
                    localStrMatch = localDateTimeObj.Hour.ToString("D2");
                }
                else
                    throw new FormatException("Invalid Object Hour HH Format");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "Hour", localStrMatch);
                //localStrHour = localStrMatch;
                #endregion
            }

            private static void PrintMinuteCustom_mm(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)mm
                ///Printing Time Value
                #region ///Print Minute
                ///(?<Minute>[0-5][0-9]|ANY|__)
                localStrMatch = null;
                if (localDateTimeObj.Minute == StDateTime.Null)
                {
                    localStrMatch = "__";
                }
                else if (localDateTimeObj.Minute >= 0 && localDateTimeObj.Minute <= 59)
                {
                    localStrMatch = localDateTimeObj.Minute.ToString("D2");
                }
                else
                    throw new FormatException("Invalid Object Minute Format");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "Minute", localStrMatch);
                //localStrMinute = localStrMatch;
                #endregion
            }

            private static void PrintSecondCustom_ss(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)ss
                ///Printing Time Value
                #region ///Parse Second
                ///(?<Second>(60|[0-5][0-9]|ANY|__))
                localStrMatch = null;
                if (localDateTimeObj.Second == StDateTime.Null)
                {
                    localStrMatch = "__";
                }
                else if (localDateTimeObj.Second >= 0 && localDateTimeObj.Second <= 59)
                {
                    localStrMatch = localDateTimeObj.Second.ToString("D2");
                }
                else
                    throw new FormatException("Invalid Object Second ss Format");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "Second", localStrMatch);
                //localStrSecond = localStrMatch;
                #endregion
            }

            private static void PrintGMTCustom(ref string txtDateTime, Regex _regex, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                ///(Format)ss
                ///Printing Time Value
                #region ///Print UTC_OffSet
                ///(?<GMT>[-\+][0-9]{2}[0-5][0-9]|ANY|___))
                localStrMatch = null;
                if (localDateTimeObj.UTCOffset == StDateTime.NullUTCOffset)
                {
                    localStrMatch = "";
                }
                else if (localDateTimeObj.UTCOffset >= -720 && localDateTimeObj.UTCOffset <= 720)
                {
                    int AbsUTCOFFSET = Math.Abs(localDateTimeObj.UTCOffset);
                    int hour = AbsUTCOFFSET / 60;
                    int minute = AbsUTCOFFSET % 60;
                    if (localDateTimeObj.UTCOffset < 0)
                    {
                        localStrMatch = String.Format("-{0:D2}{1:D2}", hour, minute);
                    }
                    else
                        localStrMatch = String.Format("+{0:D2}{1:D2}", hour, minute);
                }
                else
                    throw new FormatException("Invalid Object GMT +/-0000 Format");
                txtDateTime = RegexExtensions.Replace(txtDateTime, _regex, "GMT", localStrMatch);
                //localStrGMT = localStrMatch;

                #endregion
            }

            #endregion

            ///Display dates Times etc, based on Format selected
            public static String ToStringStDateTime(StDateTime localDateTimeObj,
                dtpCustomExtensions mvarFormatEx)
            {
                CultureInfo currentCulture = new CultureInfo("en-us");
                String CustomFormat = null, mvarFormatExStr = null, txtDateTime = null;
                GetCustomFormatMessage(mvarFormatEx, ref CustomFormat, ref mvarFormatExStr);
                DateTime Value = DateTime.MinValue;
                try
                {
                    #region ///Convert stDateTime to Sytem.DateTime

                    try
                    {
                        if (mvarFormatEx == dtpCustomExtensions.dtpCustom ||
                            mvarFormatEx == dtpCustomExtensions.dtpLongDateTime ||
                            mvarFormatEx == dtpCustomExtensions.dtpLongDateLongTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpLongDateLongTimeAMPM ||
                            mvarFormatEx == dtpCustomExtensions.dtpLongDateShortTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpLongDateShortTimeAMPM ||
                            mvarFormatEx == dtpCustomExtensions.dtpShortDateLongTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpShortDateLongTimeAMPM ||
                            mvarFormatEx == dtpCustomExtensions.dtpShortDateShortTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpShortDateShortTimeAMPM ||
                            mvarFormatEx == dtpCustomExtensions.dtpSortableDateAndTimeLocalTime ||
                            mvarFormatEx == dtpCustomExtensions.dtpUTFLocalDateAndLongTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpUTFLocalDateAndLongTimeAMPM ||
                            mvarFormatEx == dtpCustomExtensions.dtpShortDateAMPM ||
                            mvarFormatEx == dtpCustomExtensions.dtpShortDateMorningAfternoon ||
                            mvarFormatEx == dtpCustomExtensions.dtpUTFLocalDateAndShortTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpUTFLocalDateAndShortTimeAMPM)
                        {
                            if (localDateTimeObj.IsDateTimeConvertible)
                                Value = localDateTimeObj.GetDateTime();
                            else
                                throw new FormatException("Unable to Convert StDateTime to System.DateTime Object");
                        }
                        //Convert stDateTime to Sytem.DateTime;Only TimeConversion
                        else if (mvarFormatEx == dtpCustomExtensions.dtpShortTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpLongTime24Hour ||
                            mvarFormatEx == dtpCustomExtensions.dtpShortTimeAMPM ||
                           mvarFormatEx == dtpCustomExtensions.dtpTime ||
                           mvarFormatEx == dtpCustomExtensions.dtpLongTime)
                        {
                            if (localDateTimeObj.IsTimeConvertible)
                                Value = DateTime.Now.Date.Add(localDateTimeObj.GetTime());
                            else
                                throw new FormatException("Unable to Convert StDateTime to System.DateTime Object");
                        }
                        //Convert stDateTime to Sytem.DateTime;Only DateConversion
                        else if (mvarFormatEx == dtpCustomExtensions.dtpLong ||
                             mvarFormatEx == dtpCustomExtensions.dtpShort)
                        {
                            if (localDateTimeObj.IsDateConvertible)
                                Value = localDateTimeObj.GetDate();
                            else
                                throw new FormatException("Unable to Convert StDateTime to System.DateTime Object");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException("Unable to Print StDateTime in " + mvarFormatEx, ex);
                    }
                    #endregion
                    #region Partial_StDateTimeConversion

                    string monthStrLong = null, monthStr = null; string yearStr = null; string dayStr = null;
                    if (localDateTimeObj.Month > 0 && localDateTimeObj.Month <= 12)
                    {
                        monthStrLong = currentCulture.DateTimeFormat.GetMonthName(localDateTimeObj.Month);
                        //Abbreviated Month_Name
                        monthStr = monthStrLong.Substring(0, 03);
                    }

                    if (localDateTimeObj.Year != StDateTime.NullYear)
                        yearStr = localDateTimeObj.Year.ToString("D4");

                    if (localDateTimeObj.DayOfMonth > 0 && localDateTimeObj.DayOfMonth <= 31)
                        dayStr = localDateTimeObj.DayOfMonth.ToString("D2");

                    #endregion
                    switch (mvarFormatEx)
                    {
                        #region case dtpCustomExtensions.dtpCustom
                        case dtpCustomExtensions.dtpCustom:
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpDayFullName

                        case dtpCustomExtensions.dtpDayShortName:
                        case dtpCustomExtensions.dtpDayFullName:

                            txtDateTime = ((localDateTimeObj.DayOfWeek == 07) ? System.DayOfWeek.Sunday :
                                (System.DayOfWeek)localDateTimeObj.DayOfWeek) + "";
                            if (mvarFormatEx == dtpCustomExtensions.dtpDayShortName)
                                txtDateTime = txtDateTime.Substring(0, 03);

                            break;

                        #endregion
                        #region case dtpCustomExtensions.dtpDayOfMonth

                        case dtpCustomExtensions.dtpDayOfMonth:

                            if (localDateTimeObj.DayOfMonth > 0 && localDateTimeObj.DayOfMonth <= 31)
                            {
                                txtDateTime = localDateTimeObj.DayOfMonth.ToString("D2");
                            }
                            else
                                throw new FormatException("Unable to print StDateTime in dtpDayFullName");

                            break;

                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateLongTime24Hour
                        
                        case dtpCustomExtensions.dtpLongDateLongTime24Hour:
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;

                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateLongTimeAMPM
                        case dtpCustomExtensions.dtpLongDateLongTimeAMPM:
                            //txtDateTime = Value.ToString("D", currentCulture) +
                            //    " " + Value.ToString("hh:mm:ss tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateShortTime24Hour
                        case dtpCustomExtensions.dtpLongDateShortTime24Hour:
                            //txtDateTime = Value.ToString("D", currentCulture) + " " +
                            //    Value.ToString("HH:mm", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateShortTimeAMPM
                        case dtpCustomExtensions.dtpLongDateShortTimeAMPM:
                            //txtDateTime = Value.ToString("D", currentCulture) + " " +
                            //    Value.ToString("hh:mm tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongTime24Hour
                        case dtpCustomExtensions.dtpLongTime24Hour:
                            //txtDateTime = Value.ToString("HH:mm:ss", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpMonthFullName
                        case dtpCustomExtensions.dtpMonthShortName:
                        case dtpCustomExtensions.dtpMonthFullName:
                            if (localDateTimeObj.Month > 0 && localDateTimeObj.Month <= 12)
                            {
                                txtDateTime = currentCulture.DateTimeFormat.GetMonthName(localDateTimeObj.Month);
                                ///Abbreviated Month_Name
                                if (mvarFormatEx == dtpCustomExtensions.dtpMonthShortName)
                                    txtDateTime = txtDateTime.Substring(0, 03);
                            }
                            else
                                throw new FormatException("Invalid Object Month Format");
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpMonthNameAndDay
                        case dtpCustomExtensions.dtpMonthNameAndDay:
                            String month = null, dayOfMonth = null;
                            if (localDateTimeObj.Month > 0 && localDateTimeObj.Month <= 12)
                            {
                                month = currentCulture.DateTimeFormat.GetMonthName(localDateTimeObj.Month);
                            }
                            else
                                throw new FormatException("Invalid Object Month Format");
                            if (localDateTimeObj.DayOfMonth > 0 && localDateTimeObj.DayOfMonth <= 31)
                            {
                                dayOfMonth = localDateTimeObj.DayOfMonth.ToString("D2");
                                ///Abbreviated Month_Name
                                //if (mvarFormatEx == dtpCustomExtensions.dtpMonthShortName)
                                //    txtDateTime = txtDateTime.Substring(0, 03);
                            }
                            else
                                throw new FormatException("Invalid Object DayOfMonth Format");
                            txtDateTime = String.Format("{0} {1}", month, dayOfMonth);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpShortDateLongTime24Hour
                        case dtpCustomExtensions.dtpShortDateLongTime24Hour:
                            ///txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("HH:mms:ss", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpShortDateLongTimeAMPM
                        case dtpCustomExtensions.dtpShortDateLongTimeAMPM:
                            ///txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("hh:mms:ss tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateShortTime24Hour
                        case dtpCustomExtensions.dtpShortDateShortTime24Hour:
                            ///txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("HH:mm", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateShortTimeAMPM
                        case dtpCustomExtensions.dtpShortDateShortTimeAMPM:
                            ///txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("hh:mms tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortTime24Hour
                        case dtpCustomExtensions.dtpShortTime24Hour:
                            ///txtDateTime = String.Format("{0:D2}:{1:D2}", localDateTimeObj.Hour, localDateTimeObj.Minute);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortTimeAMPM
                        case dtpCustomExtensions.dtpShortTimeAMPM:
                            ///txtDateTime = Value.ToString("hh:mm tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpSortableDateAndTimeLocalTime
                        case dtpCustomExtensions.dtpSortableDateAndTimeLocalTime:
                            ///txtDateTime = Value.ToString("s", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndLongTime24Hour
                        case dtpCustomExtensions.dtpUTFLocalDateAndLongTime24Hour:
                            ///txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("HH:mm:ss", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndLongTimeAMPM
                        case dtpCustomExtensions.dtpUTFLocalDateAndLongTimeAMPM:
                            //txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("hh:mm:ss tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndShortTime24Hour
                        case dtpCustomExtensions.dtpUTFLocalDateAndShortTime24Hour:
                            //txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("HH:mm", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndShortTimeAMPM
                        case dtpCustomExtensions.dtpUTFLocalDateAndShortTimeAMPM:
                            //txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("hh:mm tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpYear4Digit
                        case dtpCustomExtensions.dtpYear4Digit:
                            if (localDateTimeObj.Year != StDateTime.NullYear)
                                txtDateTime = localDateTimeObj.Year.ToString("D4");
                            else
                                throw new FormatException("Invalid Object Year Format");
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpYearAndMonthName
                        case dtpCustomExtensions.dtpYearAndMonthName:
                            {
                                if (String.IsNullOrEmpty(yearStr))
                                    throw new FormatException("Invalid Object Year Format");
                                if (String.IsNullOrEmpty(monthStrLong))
                                    throw new FormatException("Invalid Object Month Format");
                                txtDateTime = String.Format("{0},{1}", monthStr, yearStr);
                                break;
                            }
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateAMPM
                        case dtpCustomExtensions.dtpShortDateAMPM:
                            //txtDateTime = Value.ToString("d", currentCulture) +
                            //    " " + Value.ToString("tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateMorningAfternoon
                        case dtpCustomExtensions.dtpShortDateMorningAfternoon:
                            string AMPM = "Morning";
                            if (Value.Hour >= 12)
                            {
                                AMPM = "Afternoon";
                            }
                            txtDateTime = Value.ToString("d", currentCulture) + " " + AMPM;
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLong
                        case dtpCustomExtensions.dtpLong:
                            txtDateTime = Value.ToLongDateString();
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShort
                        case dtpCustomExtensions.dtpShort:
                            txtDateTime = Value.ToShortDateString();
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpTime
                        case dtpCustomExtensions.dtpTime:
                            txtDateTime = Value.ToLongTimeString();
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongDateTimeWildCard
                        case dtpCustomExtensions.dtpLongDateTimeWildCard:
                            try
                            {
                                ///Restore Selected Date_From Value
                                txtDateTime = ToStringCustomLongDateTime(localDateTimeObj);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing DateTime Format dtpLongDateTime", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongDateWildCard
                        case dtpCustomExtensions.dtpLongDateWildCard:
                            try
                            {
                                ///Restore Selected Date_From Value
                                txtDateTime = ToStringCustomLongDate(localDateTimeObj);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing DateTime Format dtpLongDate", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongDateTime

                        case dtpCustomExtensions.dtpLongDateTime:
                            try
                            {
                                txtDateTime = Value.ToString(FullDateTimeFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing DateTime Format dtpLongDateTime", ex);
                            }
                            break;

                        #endregion
                        #region dtpCustomExtensions.dtpShortDate

                        case dtpCustomExtensions.dtpShortDate:
                            try
                            {
                                if (monthStr == null)
                                    throw new FormatException("Invalid Object Month Format");
                                if (dayStr == null)
                                    throw new FormatException("Invalid Object DayOfMonth Format");

                                txtDateTime = String.Format("{0} {1}", dayStr, monthStr);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Date Format dtpShortDate", ex);
                            }
                            break;

                        #endregion
                        #region dtpCustomExtensions.dtpLongTime

                        case dtpCustomExtensions.dtpLongTime:
                            try
                            {
                                txtDateTime = Value.ToString(FullTimeFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Time Format dtpLongTime", ex);
                            }
                            break;

                        #endregion
                        #region dtpCustomExtensions.dtpShortIntervalFixed

                        case dtpCustomExtensions.dtpShortInterval:
                        case dtpCustomExtensions.dtpShortIntervalFixed:
                        case dtpCustomExtensions.dtpShortIntervalTimeSink:
                            try
                            {
                                string minuteStr = null, secondStr = null;
                                if (localDateTimeObj.Minute != StDateTime.Null &&
                                    localDateTimeObj.Minute >= 0 && localDateTimeObj.Minute < 60)
                                    minuteStr = localDateTimeObj.Minute.ToString("D2");
                                else
                                    throw new FormatException("Invalid Object Minute Format");
                                if (localDateTimeObj.Second != StDateTime.Null &&
                                    localDateTimeObj.Second >= 0 && localDateTimeObj.Second < 60)
                                    secondStr = localDateTimeObj.Second.ToString("D2");
                                else
                                    throw new FormatException("Invalid Object Second Format");

                                txtDateTime = String.Format("{0}:{1}", minuteStr, secondStr);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Short Time Interval Format dtpShortFixedInterval", ex);
                            }
                            break;

                        #endregion
                        default:
                            txtDateTime = localDateTimeObj.ToString();
                            break;
                    }
                    return txtDateTime;
                }
                catch (FormatException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new FormatException("Unable to Validate StDateTime Format," + mvarFormatEx, ex);
                }
            }

            public static string ToString(StDateTime localDateTimeObj)
            {
                String dateTime = null;

                if (localDateTimeObj.Kind == Comm.StDateTime.DateTimeType.DateTime)
                    dateTime = String.Format("{0}/{1}/{2}/{3} {4}:{5}:{6}:{7} GMT+ 0X{8:X2} CLKS 0X{9:X2}",
                                                                      (localDateTimeObj.Year == NullYear) ? localDateTimeObj.Year.ToString("X4") :
                                                                      localDateTimeObj.Year.ToString("D4"),
                                                                      (localDateTimeObj.Month >= 1 && localDateTimeObj.Month <= 12) ?
                                                                      localDateTimeObj.Month.ToString("D1") : localDateTimeObj.Month.ToString("X2"),
                                                                      (localDateTimeObj.DayOfMonth >= 1 && localDateTimeObj.DayOfMonth <= 31) ?
                                                                      localDateTimeObj.DayOfMonth.ToString("D2") : localDateTimeObj.DayOfMonth.ToString("X2"),
                                                                      (localDateTimeObj.DayOfWeek >= 1 && localDateTimeObj.DayOfWeek <= 7) ?
                                                                      ((DayOfWeek)localDateTimeObj.DayOfWeek).ToString() :
                                                                      localDateTimeObj.DayOfWeek.ToString("X2"),

                                                                      (localDateTimeObj.Hour != Null) ? localDateTimeObj.Hour.ToString("D2") :
                                                                      localDateTimeObj.Hour.ToString("X2"),
                                                                      (localDateTimeObj.Minute != Null) ? localDateTimeObj.Minute.ToString("D2") :
                                                                      localDateTimeObj.Minute.ToString("X2"),
                                                                      (localDateTimeObj.Second != Null) ? localDateTimeObj.Second.ToString("D2") :
                                                                      localDateTimeObj.Second.ToString("X2"),
                                                                      (localDateTimeObj.Hundred != Null) ? localDateTimeObj.Hundred.ToString("D2") :
                                                                      localDateTimeObj.Hundred.ToString("X2"),
                                                                       localDateTimeObj.UTCOffset, localDateTimeObj.ClockStatus);

                else if (localDateTimeObj.Kind == DateTimeType.Date)

                    dateTime = String.Format("{0}/{1}/{2}/{3}",
                    (localDateTimeObj.Year == NullYear) ? localDateTimeObj.Year.ToString("X4") :
                    localDateTimeObj.Year.ToString("D4"),
                    (localDateTimeObj.Month >= 1 && localDateTimeObj.Month <= 12) ?
                    localDateTimeObj.Month.ToString("D1") : localDateTimeObj.Month.ToString("X2"),
                    (localDateTimeObj.DayOfMonth >= 1 && localDateTimeObj.DayOfMonth <= 31) ?
                    localDateTimeObj.DayOfMonth.ToString("D2") : localDateTimeObj.DayOfMonth.ToString("X2"),
                    (localDateTimeObj.DayOfWeek >= 1 && localDateTimeObj.DayOfWeek <= 7) ? ((DayOfWeek)localDateTimeObj.DayOfWeek).ToString() :
                    localDateTimeObj.DayOfWeek.ToString("X2"));

                else if (localDateTimeObj.Kind == DateTimeType.Time)

                    dateTime = String.Format("{0}:{1}:{2}:{3}", (localDateTimeObj.Hour != Null) ?
                        localDateTimeObj.Hour.ToString("D2") : localDateTimeObj.Hour.ToString("X2"),
                    (localDateTimeObj.Minute != Null) ? localDateTimeObj.Minute.ToString("D2") :
                    localDateTimeObj.Minute.ToString("X2"),
                    (localDateTimeObj.Second != Null) ? localDateTimeObj.Second.ToString("D2") :
                    localDateTimeObj.Second.ToString("X2"),
                    (localDateTimeObj.Hundred != Null) ? localDateTimeObj.Hundred.ToString("D2") :
                    localDateTimeObj.Hundred.ToString("X2"));

                else
                    dateTime = String.Format("{0}/{1}/{2}/{3} {4}:{5}:{6}:{7}",
                   (localDateTimeObj.Year == NullYear) ? localDateTimeObj.Year.ToString("X4")
                   : localDateTimeObj.Year.ToString("D4"),
                    (localDateTimeObj.Month >= 1 && localDateTimeObj.Month <= 12) ? localDateTimeObj.Month.
                    ToString("D1") : localDateTimeObj.Month.ToString("X2"),
                    (localDateTimeObj.DayOfMonth >= 1 && localDateTimeObj.DayOfMonth <= 31) ?
                    localDateTimeObj.DayOfMonth.ToString("D2") : localDateTimeObj.DayOfMonth.ToString("X2"),
                    (localDateTimeObj.DayOfWeek >= 1 && localDateTimeObj.DayOfWeek <= 7) ?
                    ((DayOfWeek)localDateTimeObj.DayOfWeek).ToString() : localDateTimeObj.DayOfWeek.ToString("X2"),
                              (localDateTimeObj.Hour != Null) ? localDateTimeObj.Hour.ToString("D2") :
                              localDateTimeObj.Hour.ToString("X2"),
                              (localDateTimeObj.Minute != Null) ? localDateTimeObj.Minute.ToString("D2") :
                              localDateTimeObj.Minute.ToString("X2"),
                              (localDateTimeObj.Second != Null) ? localDateTimeObj.Second.ToString("D2") :
                              localDateTimeObj.Second.ToString("X2"),
                              (localDateTimeObj.Hundred != Null) ? localDateTimeObj.Hundred.ToString("D2") :
                              localDateTimeObj.Hundred.ToString("X2"));
                return dateTime;
            }

            #endregion

            /// <summary>
            ///Display dates Times etc, based on Format selected
            /// </summary>
            /// <param name="txtDateTime"></param>
            /// <param name="localDateTimeObj"></param>
            /// <param name="Value"></param>
            /// <param name="mvarFormatEx"></param>
            public static void FormatStDateTime(ref String txtDateTime, ref StDateTime localDateTimeObj,
                ref DateTime Value, dtpCustomExtensions mvarFormatEx)
            {
                CultureInfo currentCulture = new CultureInfo("en-us");
                String CustomFormat = null, mvarFormatExStr = null;
                GetCustomFormatMessage(mvarFormatEx, ref CustomFormat, ref mvarFormatExStr);
                List<StDateTimeWildCards> WildCardListCards = null;
                try
                {
                    switch (mvarFormatEx)
                    {
                        #region case dtpCustomExtensions.dtpCustom
                        case dtpCustomExtensions.dtpCustom:
                            {
                                Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);
                                StoreCustomDate_DateTime(Value, localDateTimeObj);
                                StoreCustomTime_DateTime(Value, localDateTimeObj);
                            }
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpDayFullName

                        case dtpCustomExtensions.dtpDayFullName:
                        case dtpCustomExtensions.dtpDayShortName:
                            {
                                DateTime tmpVal = DateTime.Today;
                                DayOfWeek? DayOfWeekMatch = null;
                                String DayOfWeek_Str = String.Empty;
                                foreach (DayOfWeek dyOfWeek in Enum.GetValues(typeof(DayOfWeek)))
                                {
                                    if (mvarFormatEx == dtpCustomExtensions.dtpDayFullName)
                                        DayOfWeek_Str = currentCulture.DateTimeFormat.GetDayName(dyOfWeek);
                                    else
                                        DayOfWeek_Str = currentCulture.DateTimeFormat.GetAbbreviatedDayName(dyOfWeek);
                                    //matched DayOfWeek
                                    if (String.Equals(DayOfWeek_Str, txtDateTime, StringComparison.OrdinalIgnoreCase))
                                    {
                                        DayOfWeekMatch = dyOfWeek;
                                        break;
                                    }
                                }
                                if (DayOfWeekMatch == null)
                                    throw new FormatException("Error Parsing DateTime in Format dtpCustomExtensions.dtpDayFullName");
                                while (tmpVal.DayOfWeek != DayOfWeekMatch) // i.e. "Monday"
                                    tmpVal = tmpVal.AddDays(1);
                                Value = tmpVal;
                                //Store Custom DayOfWeek here
                                localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                                WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                WildCardListCards.Add(StDateTimeWildCards.NullYear);
                                WildCardListCards.Add(StDateTimeWildCards.NullMonth);
                                WildCardListCards.Add(StDateTimeWildCards.NullDay);
                                WildCardListCards.Remove(StDateTimeWildCards.NullDayOfWeek);
                                ///here Reset All WildCards
                                Save_WildCards(WildCardListCards, localDateTimeObj);
                                StoreCustomDate_DateTime(Value, localDateTimeObj);
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);
                                break;
                            }
                        #endregion
                        #region case dtpCustomExtensions.dtpDayOfMonth

                        case dtpCustomExtensions.dtpDayOfMonth:
                            {
                                DateTime tmpVal = DateTime.Today;
                                int DayOfMonth = 0;
                                if (!int.TryParse(txtDateTime, out DayOfMonth))
                                    throw new FormatException("Error Parsing DateTime in Format dtpCustomExtensions.dtpDayOfMonth");
                                while (tmpVal.Day != DayOfMonth) // i.e. "0,1,28,29,31"
                                    tmpVal = tmpVal.AddDays(1);
                                Value = tmpVal;
                                //Store Custom DayOfWeek here
                                localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                                WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                WildCardListCards.Add(StDateTimeWildCards.NullYear);
                                WildCardListCards.Add(StDateTimeWildCards.NullMonth);
                                WildCardListCards.Remove(StDateTimeWildCards.NullDay);
                                WildCardListCards.Remove(StDateTimeWildCards.LSTDayOfMonth);
                                WildCardListCards.Remove(StDateTimeWildCards._2LSTDayOfMonth);
                                WildCardListCards.Add(StDateTimeWildCards.NullDayOfWeek);
                                ///here Reset All WildCards
                                Save_WildCards(WildCardListCards, localDateTimeObj);
                                StoreCustomDate_DateTime(Value, localDateTimeObj);
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);
                                break;
                            }
                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateLongTime24Hour
                        case dtpCustomExtensions.dtpLongDateLongTime24Hour:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("D", currentCulture) + " " +
                            //    Value.ToString("HH:mm:ss", currentCulture);

                            ///Store Custom FullDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            ///here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateLongTimeAMPM
                        case dtpCustomExtensions.dtpLongDateLongTimeAMPM:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("D", currentCulture) +
                            //    " " + Value.ToString("hh:mm:ss tt", currentCulture);

                            ///Store Custom FullDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            ///here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateShortTime24Hour
                        case dtpCustomExtensions.dtpLongDateShortTime24Hour:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("D", currentCulture) + " " +
                            //    Value.ToString("HH:mm", currentCulture);

                            ///Store Custom FullDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            ///here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpLongDateShortTimeAMPM
                        case dtpCustomExtensions.dtpLongDateShortTimeAMPM:
                            //txtDateTime = Value.ToString("D", currentCulture) + " " +
                            //    Value.ToString("hh:mm tt", currentCulture);
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);

                            //Store Custom FullDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            ///here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongTime24Hour
                        case dtpCustomExtensions.dtpLongTime24Hour:
                            {
                                Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                                //txtDateTime = Value.ToString("HH:mm:ss", currentCulture);
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);

                                ////Store Custom Time here
                                localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                                WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                WildCardListCards.Remove(StDateTimeWildCards.NullGMT);
                                WildCardListCards.Remove(StDateTimeWildCards.NullHour);
                                WildCardListCards.Remove(StDateTimeWildCards.NullMinute);
                                WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                                ///here Reset All WildCards
                                Save_WildCards(WildCardListCards, localDateTimeObj);
                                StoreCustomTime_DateTime(Value, localDateTimeObj);
                                break;
                            }
                        #endregion
                        #region case dtpCustomExtensions.dtpMonthFullName
                        case dtpCustomExtensions.dtpMonthShortName:
                        case dtpCustomExtensions.dtpMonthFullName:
                            {
                                DateTime tmpVal = DateTime.Today;
                                int? Month = null;
                                String Month_Str = String.Empty;

                                for (int Month_Count = 1; Month_Count <= 12; Month_Count++)
                                {
                                    if (mvarFormatEx == dtpCustomExtensions.dtpMonthShortName)
                                        Month_Str = currentCulture.DateTimeFormat.GetAbbreviatedMonthName(Month_Count);
                                    else if (mvarFormatEx == dtpCustomExtensions.dtpMonthFullName)
                                        Month_Str = currentCulture.DateTimeFormat.GetMonthName(Month_Count);
                                    //match Month_Str
                                    if (String.Equals(Month_Str, txtDateTime, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Month = Month_Count;
                                        break;
                                    }
                                }
                                if (Month == null)
                                    throw new FormatException("Error Parsing DateTime in Format dtpCustomExtensions.dtpMonthShortName");
                                while (tmpVal.Month != Month) // i.e. "February"
                                    tmpVal = tmpVal.AddMonths(1);
                                Value = tmpVal;
                                //Value = DateTime.Parse(txtDateTime);
                                //Store Custom Date here
                                localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                                WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                WildCardListCards.Add(StDateTimeWildCards.NullYear);
                                WildCardListCards.Add(StDateTimeWildCards.NullDay);
                                WildCardListCards.Add(StDateTimeWildCards.NullDayOfWeek);
                                WildCardListCards.Remove(StDateTimeWildCards.NullMonth);
                                WildCardListCards.Remove(StDateTimeWildCards.DLSBEGIN);
                                WildCardListCards.Remove(StDateTimeWildCards.DLSEND);
                                ///here Reset All WildCards
                                Save_WildCards(WildCardListCards, localDateTimeObj);
                                StoreCustomDate_DateTime(Value, localDateTimeObj);
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);
                                break;
                            }
                        #endregion
                        #region dtpCustomExtensions.dtpMonthNameAndDay

                        case dtpCustomExtensions.dtpMonthNameAndDay:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString("M", currentCulture);
                            //Save Custom Date here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                            WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                            WildCardListCards.Add(StDateTimeWildCards.NullYear);
                            WildCardListCards.Add(StDateTimeWildCards.NullDayOfWeek);
                            WildCardListCards.Remove(StDateTimeWildCards.NullDay);
                            WildCardListCards.Remove(StDateTimeWildCards.LSTDayOfMonth);
                            WildCardListCards.Remove(StDateTimeWildCards._2LSTDayOfMonth);
                            WildCardListCards.Remove(StDateTimeWildCards.NullMonth);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSBEGIN);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSEND);
                            //here Reset All WildCards
                            Save_WildCards(WildCardListCards, localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            break;

                        #endregion
                        #region case dtpCustomExtensions.dtpShortDateLongTime24Hour
                        case dtpCustomExtensions.dtpShortDateLongTime24Hour:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("HH:mms:ss", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //Store Custom ShortDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            ///here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region case dtpCustomExtensions.dtpShortDateLongTimeAMPM
                        case dtpCustomExtensions.dtpShortDateLongTimeAMPM:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("hh:mms:ss tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //Store Custom ShortDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateShortTime24Hour
                        case dtpCustomExtensions.dtpShortDateShortTime24Hour:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("HH:mm", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //Store Custom ShortDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateShortTimeAMPM
                        case dtpCustomExtensions.dtpShortDateShortTimeAMPM:
                            {
                                Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);
                                //Store Custom ShortDateTime here
                                localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                                //here Reset All WildCards
                                Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                                StoreCustomDate_DateTime(Value, localDateTimeObj);
                                StoreCustomTime_DateTime(Value, localDateTimeObj);
                                break;
                            }
                        #endregion
                        #region dtpCustomExtensions.dtpShortTime24Hour
                        case dtpCustomExtensions.dtpShortTime24Hour:
                            {
                                DateTime exact_dt1 = DateTime.MinValue;
                                //Valid Time Parsed from txtBox
                                if (DateTime.TryParseExact(txtDateTime, CustomFormat, currentCulture,
                                       DateTimeStyles.None, out exact_dt1))
                                {
                                    Value = exact_dt1;
                                }
                                else
                                    Value = DateTime.MinValue;
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);
                                //Store Custom Time here
                                localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                                WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                WildCardListCards.Add(StDateTimeWildCards.NullGMT);
                                WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                                WildCardListCards.Remove(StDateTimeWildCards.NullHour);
                                WildCardListCards.Remove(StDateTimeWildCards.NullMinute);

                                //here Reset All WildCards
                                Save_WildCards(WildCardListCards, localDateTimeObj);
                                StoreCustomTime_DateTime(Value, localDateTimeObj);

                                break;
                            }
                        #endregion
                        #region dtpCustomExtensions.dtpShortTimeAMPM
                        case dtpCustomExtensions.dtpShortTimeAMPM:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            ///txtDateTime = Value.ToString("hh:mm tt", currentCulture);

                            //Store Custom Time here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                            WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                            WildCardListCards.Add(StDateTimeWildCards.NullGMT);
                            WildCardListCards.Remove(StDateTimeWildCards.NullHour);
                            WildCardListCards.Remove(StDateTimeWildCards.NullMinute);
                            WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                            //here Reset All WildCards
                            Save_WildCards(WildCardListCards, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpSortableDateAndTimeLocalTime
                        case dtpCustomExtensions.dtpSortableDateAndTimeLocalTime:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString("s", currentCulture);

                            //Store Custom ShortDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndLongTime24Hour
                        case dtpCustomExtensions.dtpUTFLocalDateAndLongTime24Hour:
                            {
                                Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                                //txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("HH:mm:ss", currentCulture);
                                txtDateTime = Value.ToString(CustomFormat, currentCulture);
                                //Store Custom ShortDateTime here
                                localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                                //here Reset All WildCards
                                Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                                StoreCustomDate_DateTime(Value, localDateTimeObj);
                                StoreCustomTime_DateTime(Value, localDateTimeObj);
                                break;
                            }
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndLongTimeAMPM
                        case dtpCustomExtensions.dtpUTFLocalDateAndLongTimeAMPM:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("hh:mm:ss tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //Store Custom LongDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndShortTime24Hour
                        case dtpCustomExtensions.dtpUTFLocalDateAndShortTime24Hour:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("HH:mm", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //Store Custom LongDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpUTFLocalDateAndShortTimeAMPM
                        case dtpCustomExtensions.dtpUTFLocalDateAndShortTimeAMPM:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            //txtDateTime = Value.ToString("yyyy-MM-dd", currentCulture) + " " + Value.ToString("hh:mm tt", currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //Store Custom LongDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpYear4Digit
                        case dtpCustomExtensions.dtpYear4Digit:
                            try
                            {
                                Value = DateTime.Parse(txtDateTime);
                            }
                            catch
                            {
                                Value = DateTime.Parse("01 01 " + txtDateTime);
                            }
                            txtDateTime = Value.ToString("yyyy", currentCulture);

                            //Save Custom Date here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                            WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                            WildCardListCards.Remove(StDateTimeWildCards.NullYear);
                            WildCardListCards.Add(StDateTimeWildCards.NullDayOfWeek);
                            WildCardListCards.Add(StDateTimeWildCards.NullDay);
                            WildCardListCards.Add(StDateTimeWildCards.NullMonth);
                            //here Reset All WildCards
                            Save_WildCards(WildCardListCards, localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpYearAndMonthName
                        case dtpCustomExtensions.dtpYearAndMonthName:
                            try
                            {
                                Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            }
                            catch
                            {
                                try
                                {
                                    txtDateTime = DateTime.Now.Year.ToString() + " " + int.Parse(txtDateTime, currentCulture).ToString();
                                }
                                catch
                                {
                                    Value = DateTime.Parse(txtDateTime + " 01");
                                }
                            }
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);

                            //Save Custom Date here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                            WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                            //Remove CustomWild Cards
                            WildCardListCards.Remove(StDateTimeWildCards.NullYear);
                            WildCardListCards.Remove(StDateTimeWildCards.NullMonth);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSBEGIN);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSEND);
                            //Apply CustomWild Cards
                            WildCardListCards.Add(StDateTimeWildCards.NullDayOfWeek);
                            WildCardListCards.Add(StDateTimeWildCards.NullDay);
                            //here Reset All WildCards
                            Save_WildCards(WildCardListCards, localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateAMPM
                        case dtpCustomExtensions.dtpShortDateAMPM:
                            if (txtDateTime.Substring(txtDateTime.Length - 2, 2).ToLower() == "pm")
                            {
                                txtDateTime = txtDateTime.Substring(0, txtDateTime.Length - 2);
                                txtDateTime = txtDateTime + " 13:00";
                            }
                            else
                            {
                                if (txtDateTime.Substring(txtDateTime.Length - 2, 2).ToLower() == "am")
                                {
                                    txtDateTime = txtDateTime.Substring(0, txtDateTime.Length - 2);
                                }
                                txtDateTime = txtDateTime + " 01:00";
                            }
                            Value = DateTime.Parse(txtDateTime);
                            txtDateTime = Value.ToString("d", currentCulture) + " " + Value.ToString("tt", currentCulture);

                            //Store Custom ShortDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDateMorningAfternoon

                        case dtpCustomExtensions.dtpShortDateMorningAfternoon:
                            string AMPM = "Morning";
                            if (txtDateTime.Substring(txtDateTime.Length - 2, 2).ToLower() == "pm")
                            {
                                txtDateTime = txtDateTime.Substring(0, txtDateTime.Length - 2);
                                txtDateTime = txtDateTime + " 13:00";
                            }
                            else
                            {
                                if (txtDateTime.Substring(txtDateTime.Length - 2, 2).ToLower() == "am")
                                {
                                    txtDateTime = txtDateTime.Substring(0, txtDateTime.Length - 2);
                                }
                                txtDateTime = txtDateTime + " 01:00";
                            }
                            Value = DateTime.Parse(txtDateTime);
                            if (Value.Hour >= 12)
                            {
                                AMPM = "Afternoon";
                            }
                            txtDateTime = Value.ToString("d", currentCulture) + " " + AMPM;

                            //Store Custom ShortDateTime here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            //here Reset All WildCards
                            Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;

                        #endregion
                        #region dtpCustomExtensions.dtpLong

                        case dtpCustomExtensions.dtpLong:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //txtDateTime = Value.ToLongDateString();

                            //Store Custom Date here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                            WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                            WildCardListCards.Remove(StDateTimeWildCards.NullYear);
                            WildCardListCards.Remove(StDateTimeWildCards.NullMonth);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSBEGIN);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSEND);
                            WildCardListCards.Remove(StDateTimeWildCards.NullDay);
                            WildCardListCards.Remove(StDateTimeWildCards.LSTDayOfMonth);
                            WildCardListCards.Remove(StDateTimeWildCards._2LSTDayOfMonth);
                            WildCardListCards.Remove(StDateTimeWildCards.NullDayOfWeek);
                            //here Reset All WildCards
                            Save_WildCards(WildCardListCards, localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);
                            break;

                        #endregion
                        #region dtpCustomExtensions.dtpShort
                        case dtpCustomExtensions.dtpShort:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //txtDateTime = Value.ToShortDateString();

                            //Store Custom Date here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                            WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                            WildCardListCards.Remove(StDateTimeWildCards.NullYear);
                            WildCardListCards.Remove(StDateTimeWildCards.NullMonth);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSBEGIN);
                            WildCardListCards.Remove(StDateTimeWildCards.DLSEND);
                            WildCardListCards.Remove(StDateTimeWildCards.NullDay);
                            WildCardListCards.Remove(StDateTimeWildCards.LSTDayOfMonth);
                            WildCardListCards.Remove(StDateTimeWildCards._2LSTDayOfMonth);
                            WildCardListCards.Remove(StDateTimeWildCards.NullDayOfWeek);
                            //here Reset All WildCards
                            Save_WildCards(WildCardListCards, localDateTimeObj);
                            StoreCustomDate_DateTime(Value, localDateTimeObj);

                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpTime
                        case dtpCustomExtensions.dtpTime:
                            Value = DateTime.ParseExact(txtDateTime, CustomFormat, currentCulture);
                            txtDateTime = Value.ToString(CustomFormat, currentCulture);
                            //txtDateTime = Value.ToLongTimeString();

                            //Store Custom Date here
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                            WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                            WildCardListCards.Remove(StDateTimeWildCards.NullHour);
                            WildCardListCards.Remove(StDateTimeWildCards.NullMinute);
                            WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                            WildCardListCards.Add(StDateTimeWildCards.NullGMT);
                            //here Reset All WildCards
                            Save_WildCards(WildCardListCards, localDateTimeObj);
                            StoreCustomTime_DateTime(Value, localDateTimeObj);
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongDateTimeWildCard
                        case dtpCustomExtensions.dtpLongDateTimeWildCard:
                            try
                            {
                                DateTime exact_dt1 = DateTime.Now;
                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt + ' '), FullDateTimeWildCardRegEx, RegexOptions.IgnoreCase);
                                //Valid Date_Time Parsed from txtBox
                                if (match.Success)
                                {
                                    exact_dt1 = StoreCustomLongDateTime_FromText(inputTxt, localDateTimeObj);
                                    if (exact_dt1 != DateTime.MinValue)
                                        Value = exact_dt1;
                                }
                                else
                                    throw new FormatException("Invalid Long DateTime Format");
                                //Restore Selected Date_From Value
                                txtDateTime = ToStringCustomLongDateTime(localDateTimeObj);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing DateTime Format dtpLongDateTime", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongDateWildCard
                        case dtpCustomExtensions.dtpLongDateWildCard:
                            try
                            {
                                DateTime exact_dt1 = DateTime.Now;
                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt + ' '), FullDateWildCardRegEx, RegexOptions.IgnoreCase);
                                //Valid Date_Time Parsed from txtBox
                                if (match.Success)
                                {
                                    exact_dt1 = StoreCustomLongDate_FromText(inputTxt, localDateTimeObj);
                                    if (exact_dt1 != DateTime.MinValue)
                                        Value = exact_dt1;
                                }
                                else
                                    throw new FormatException("Invalid LongDate Format");
                                //Restore Selected Date_From Value
                                txtDateTime = ToStringCustomLongDate(localDateTimeObj);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing DateTime Format dtpLongDate", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongDateTime
                        case dtpCustomExtensions.dtpLongDateTime:
                            try
                            {
                                DateTime exact_dt1 = DateTime.Now;
                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt + ' '), FullDateTimeRegEx, RegexOptions.IgnoreCase);
                                //Valid Date_Time Parsed from txtBox
                                if (DateTime.TryParseExact(inputTxt, CustomFormat, currentCulture,
                                   DateTimeStyles.None, out exact_dt1)
                                   && match.Success)
                                {
                                    Value = exact_dt1;
                                    localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                                    ///here Reset All WildCards
                                    Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                                    StoreCustomDate_DateTime(exact_dt1, localDateTimeObj);
                                    StoreCustomTime_DateTime(exact_dt1, localDateTimeObj);
                                }
                                else
                                    throw new FormatException("Invalid Long DateTime Format");
                                //Restore Selected Date_From Value
                                txtDateTime = Value.ToString(FullDateTimeFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing DateTime Format dtpLongDateTime", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortDate
                        case dtpCustomExtensions.dtpShortDate:
                            try
                            {
                                DateTime exact_dt1 = DateTime.Now;
                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt), ShortDateRegEx, RegexOptions.IgnoreCase);
                                //Valid Date Parsed from txtBox
                                if (DateTime.TryParseExact(inputTxt, CustomFormat, currentCulture,
                                   DateTimeStyles.None, out exact_dt1)
                                   && match.Success)
                                {
                                    Value = exact_dt1;
                                    localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                                    WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                    WildCardListCards.Add(StDateTimeWildCards.NullYear);
                                    WildCardListCards.Add(StDateTimeWildCards.NullDayOfWeek);

                                    WildCardListCards.Remove(StDateTimeWildCards.NullMonth);
                                    WildCardListCards.Remove(StDateTimeWildCards.DLSBEGIN);
                                    WildCardListCards.Remove(StDateTimeWildCards.DLSEND);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullDay);
                                    WildCardListCards.Remove(StDateTimeWildCards.LSTDayOfMonth);
                                    WildCardListCards.Remove(StDateTimeWildCards._2LSTDayOfMonth);
                                    //here Reset All WildCards
                                    Save_WildCards(WildCardListCards, localDateTimeObj);
                                    StoreCustomDate_DateTime(exact_dt1, localDateTimeObj);
                                }
                                else
                                    throw new FormatException("Invalid Short Date Format");
                                //Restore Selected Date_From Value
                                txtDateTime = Value.ToString(ShortDateFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Date Format dtpShortDate", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpLongTime
                        case dtpCustomExtensions.dtpLongTime:
                            try
                            {
                                DateTime exact_dt1 = DateTime.Now;
                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt), FullTimeRegEx, RegexOptions.IgnoreCase);
                                ///Valid Time Parsed from txtBox
                                if (DateTime.TryParseExact(inputTxt, CustomFormat, currentCulture,
                                    DateTimeStyles.None, out exact_dt1)
                                    && match.Success)
                                {
                                    Value = exact_dt1;
                                    localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                                    ///here Reset All WildCards
                                    WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullHour);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullMinute);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                                    WildCardListCards.Add(StDateTimeWildCards.NullGMT);
                                    Save_WildCards(WildCardListCards, localDateTimeObj);
                                    StoreCustomTime_DateTime(exact_dt1, localDateTimeObj);
                                }
                                else
                                    throw new FormatException("Invalid Long Time Format");
                                ///Restore Selected Date_From Value
                                txtDateTime = Value.ToString(FullTimeFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Time Format dtpLongTime", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortInterval
                        case dtpCustomExtensions.dtpShortInterval:
                            ///mvarCustomFormatMessage = "Short Interval Minute:Second(00:00 upto 59:59)";
                            try
                            {
                                DateTime exact_dt1 = DateTime.Now;
                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt), ShortIntervalRegEx, RegexOptions.IgnoreCase);
                                ///Valid Time Parsed from txtBox
                                if (DateTime.TryParseExact(inputTxt, CustomFormat, currentCulture,
                                    DateTimeStyles.None, out exact_dt1)
                                    && match.Success)
                                {
                                    Value = exact_dt1;
                                    localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                                    ///here Reset All WildCards
                                    WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullMinute);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                                    WildCardListCards.AddRange(new List<StDateTimeWildCards>() 
                                { StDateTimeWildCards.NullHour,
                                  StDateTimeWildCards.NullGMT });
                                    Save_WildCards(WildCardListCards, localDateTimeObj);
                                    StoreCustomTime_DateTime(exact_dt1, localDateTimeObj);
                                }
                                else
                                    throw new FormatException("Invalid Short Time Interval Format");
                                ///Restore Selected Date_From Value
                                txtDateTime = Value.ToString(ShortTimeIntervalFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Short Time Interval Format dtpShortInterval", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortIntervalFixed
                        case dtpCustomExtensions.dtpShortIntervalFixed:
                            ///mvarCustomFormatMessage = "Short Fixed Interval Minute:Second(00:00 upto 14:59)";
                            try
                            {
                                DateTime exact_dt1 = DateTime.Now;
                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt), ShortFixedIntervalRegEx, RegexOptions.IgnoreCase);
                                ///Valid Time Parsed from txtBox
                                if (DateTime.TryParseExact(inputTxt, CustomFormat, currentCulture,
                                    DateTimeStyles.None, out exact_dt1)
                                    && match.Success)
                                {
                                    Value = exact_dt1;
                                    localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                                    ///here Reset All WildCards
                                    WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullMinute);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                                    WildCardListCards.AddRange(new List<StDateTimeWildCards>() 
                                { StDateTimeWildCards.NullHour,
                                  StDateTimeWildCards.NullGMT });
                                    Save_WildCards(WildCardListCards, localDateTimeObj);
                                    StoreCustomTime_DateTime(exact_dt1, localDateTimeObj);
                                }
                                else
                                    throw new FormatException("Invalid Short Time Interval Format");
                                ///Restore Selected Date_From Value
                                txtDateTime = Value.ToString(ShortTimeIntervalFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Short Time Interval Format dtpShortFixedInterval", ex);
                            }
                            break;
                        #endregion
                        #region dtpCustomExtensions.dtpShortIntervalTimeSink
                        case dtpCustomExtensions.dtpShortIntervalTimeSink:
                            try
                            {

                                String inputTxt = txtDateTime;
                                var match = Regex.Match((inputTxt), ShortIntervalTimeSinkRegEx, RegexOptions.IgnoreCase);
                                DateTime exact_dt1 = DateTime.Now;
                                ///Valid Time Parsed from txtBox
                                if (DateTime.TryParseExact(inputTxt, CustomFormat, currentCulture,
                                   DateTimeStyles.None, out exact_dt1)
                                   && match.Success)
                                {
                                    Value = exact_dt1;
                                    localDateTimeObj.Kind = StDateTime.DateTimeType.Time;
                                    ///here Reset All WildCards
                                    WildCardListCards = (List<StDateTimeWildCards>)Get_WildCardsChecked(localDateTimeObj);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullMinute);
                                    WildCardListCards.Remove(StDateTimeWildCards.NullSecond);
                                    WildCardListCards.AddRange(new List<StDateTimeWildCards>() {StDateTimeWildCards.NullHour,
                                        StDateTimeWildCards.NullGMT});
                                    Save_WildCards(WildCardListCards, localDateTimeObj);
                                    StoreCustomTime_DateTime(exact_dt1, localDateTimeObj);
                                }
                                else
                                    throw new FormatException("Invalid Short Time Interval TimeSink Format");
                                ///Restore Selected Date_From Value
                                txtDateTime = Value.ToString(ShortTimeIntervalFormat, currentCulture);
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException("Error occurred while Parsing Short Time Interval Format dtpShortIntervalTimeSink", ex);
                            }
                            break;
                        #endregion
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //Raise Error
                    throw ex;
                }
            }

            public static void Save_WildCards(IList<StDateTimeWildCards> WildCardsChecked, StDateTime localDateTimeObj)
            {
                DateTime Current_DateTime = DateTime.Now;
                try
                {
                    if (WildCardsChecked == null)
                        return;
                    //Save Time WildCards
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullHour))
                        localDateTimeObj.Hour = StDateTime.Null;
                    else if (localDateTimeObj.Hour == StDateTime.Null)//Reset Hour WildCard here
                    {
                        localDateTimeObj.Hour = 00; //Zero hour to Reset Hour    
                    }
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullMinute))
                        localDateTimeObj.Minute = StDateTime.Null;
                    else if (localDateTimeObj.Minute == StDateTime.Null)
                    {
                        localDateTimeObj.Minute = 00; //Zero minute to Reset Minute
                    }
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullSecond))
                        localDateTimeObj.Second = StDateTime.Null;
                    else if (localDateTimeObj.Second == StDateTime.Null)
                    {
                        localDateTimeObj.Second = 00; //Zero Second to Reset
                    }
                    //Save Monthly WildCards
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullMonth))
                        localDateTimeObj.Month = StDateTime.Null;
                    else if (WildCardsChecked.Contains(StDateTimeWildCards.DLSBEGIN))
                        localDateTimeObj.Month = StDateTime.DaylightSavingBegin;
                    else if (WildCardsChecked.Contains(StDateTimeWildCards.DLSEND))
                        localDateTimeObj.Month = StDateTime.DaylightSavingEnd;
                    else if (!(localDateTimeObj.Month >= 1 && localDateTimeObj.Month <= 12))//Reset Month WildCard here
                    {
                        localDateTimeObj.Month = (byte)Current_DateTime.Month;  //Reset Value For Month
                    }
                    //Display Miscellaneous WildCards
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullYear))
                        localDateTimeObj.Year = StDateTime.NullYear;
                    else if (localDateTimeObj.Year == StDateTime.NullYear)
                        localDateTimeObj.Year = (ushort)Current_DateTime.Year;
                    //Save DayOfMonth WildCards
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullDay))
                        localDateTimeObj.DayOfMonth = StDateTime.Null;
                    else if (WildCardsChecked.Contains(StDateTimeWildCards.LSTDayOfMonth))
                        localDateTimeObj.DayOfMonth = StDateTime.LastDayOfMonth;
                    else if (WildCardsChecked.Contains(StDateTimeWildCards._2LSTDayOfMonth))
                        localDateTimeObj.DayOfMonth = StDateTime.SecondLastDayOfMonth;
                    else if (!(localDateTimeObj.DayOfMonth >= 1 && localDateTimeObj.DayOfMonth <= 31))  //Reset DayOfMonth Wild Cards
                    {
                        localDateTimeObj.DayOfMonth = (byte)Current_DateTime.Day;  //Reset Value For DayOfMonth
                    }
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullDayOfWeek))
                        localDateTimeObj.DayOfWeek = StDateTime.Null;
                    else if (localDateTimeObj.DayOfWeek == StDateTime.Null)    //Reset DayOfMonth WildCard here
                        localDateTimeObj.DayOfWeek = (Current_DateTime.DayOfWeek == System.DayOfWeek.Sunday) ? (byte)7 : (byte)Current_DateTime.DayOfWeek;
                    if (WildCardsChecked.Contains(StDateTimeWildCards.NullGMT))
                        localDateTimeObj.UTCOffset = StDateTime.NullUTCOffset;
                    else if (localDateTimeObj.UTCOffset == StDateTime.NullUTCOffset)    //Reset Null UTC OffSet here
                    {
                        try
                        {
                            TimeZone tInfo = TimeZone.CurrentTimeZone;
                            TimeSpan UTCoffSet = tInfo.GetUtcOffset(Current_DateTime);
                            localDateTimeObj.UTCOffset = Convert.ToInt16(UTCoffSet.TotalMinutes);
                        }
                        catch
                        {
                            localDateTimeObj.UTCOffset = StDateTime.NullUTCOffset;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving WildCards", ex);
                }
            }

            public static IList<StDateTimeWildCards> Get_WildCardsChecked(StDateTime localDateTimeObj)
            {
                IList<StDateTimeWildCards> WildCardsChecked = new List<StDateTimeWildCards>();
                try
                {
                    if (WildCardsChecked == null)
                        return WildCardsChecked;
                    ///Display Time WildCards
                    if (localDateTimeObj.Hour == StDateTime.Null)
                        WildCardsChecked.Add(StDateTimeWildCards.NullHour);
                    if (localDateTimeObj.Minute == StDateTime.Null)
                        WildCardsChecked.Add(StDateTimeWildCards.NullMinute);
                    if (localDateTimeObj.Second == StDateTime.Null)
                        WildCardsChecked.Add(StDateTimeWildCards.NullSecond);

                    ///Display Monthly WildCards
                    if (localDateTimeObj.Month == StDateTime.Null)
                        WildCardsChecked.Add(StDateTimeWildCards.NullMonth);
                    if (localDateTimeObj.Month == StDateTime.DaylightSavingBegin)
                        WildCardsChecked.Add(StDateTimeWildCards.DLSBEGIN);
                    if (localDateTimeObj.Month == StDateTime.DaylightSavingEnd)
                        WildCardsChecked.Add(StDateTimeWildCards.DLSEND);

                    if (localDateTimeObj.DayOfMonth == StDateTime.Null)
                        WildCardsChecked.Add(StDateTimeWildCards.NullDay);
                    if (localDateTimeObj.DayOfMonth == StDateTime.LastDayOfMonth)
                        WildCardsChecked.Add(StDateTimeWildCards.LSTDayOfMonth);
                    if (localDateTimeObj.DayOfMonth == StDateTime.SecondLastDayOfMonth)
                        WildCardsChecked.Add(StDateTimeWildCards._2LSTDayOfMonth);

                    ///Display Miscellaneous WildCards
                    if (localDateTimeObj.Year == StDateTime.NullYear)
                        WildCardsChecked.Add(StDateTimeWildCards.NullYear);
                    if (localDateTimeObj.UTCOffset == StDateTime.NullUTCOffset)
                        WildCardsChecked.Add(StDateTimeWildCards.NullGMT);
                    if (localDateTimeObj.DayOfWeek == StDateTime.Null)
                        WildCardsChecked.Add(StDateTimeWildCards.NullDayOfWeek);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while reading WildCards", ex);
                }
                return WildCardsChecked;
            }

            public static void StoreCustomDate_DateTime(DateTime val, StDateTime localDateTimeObj)
            {
                if (localDateTimeObj != null &&
                        (localDateTimeObj.Kind == StDateTime.DateTimeType.Date ||
                        localDateTimeObj.Kind == StDateTime.DateTimeType.DateTime))
                {
                    ///Store DateTime Value
                    if (localDateTimeObj.Year != StDateTime.NullYear)
                        localDateTimeObj.Year = (ushort)val.Year;
                    ///Save If Month field not wild carded
                    if (localDateTimeObj.Month != StDateTime.Null &&
                       localDateTimeObj.Month != StDateTime.DaylightSavingBegin &&
                       localDateTimeObj.Month != StDateTime.DaylightSavingEnd)
                        localDateTimeObj.Month = (byte)val.Month;
                    ///Save If DayOfMonth field not wild carded
                    if (localDateTimeObj.DayOfMonth != StDateTime.Null &&
                       localDateTimeObj.DayOfMonth != StDateTime.LastDayOfMonth &&
                       localDateTimeObj.DayOfMonth != StDateTime.SecondLastDayOfMonth)
                        localDateTimeObj.DayOfMonth = (byte)val.Day;
                    ///Save If DayOfWeek field not wild carded
                    if (localDateTimeObj.DayOfWeek != StDateTime.Null)
                        localDateTimeObj.DayOfWeek = (val.DayOfWeek == System.DayOfWeek.Sunday) ? (byte)7 : (byte)val.DayOfWeek;
                }
            }

            public static void StoreCustomTime_DateTime(DateTime val, StDateTime localDateTimeObj)
            {
                if (localDateTimeObj != null &&
                        (localDateTimeObj.Kind == StDateTime.DateTimeType.Time ||
                        localDateTimeObj.Kind == StDateTime.DateTimeType.DateTime))
                {
                    ///Store Time Value
                    if (localDateTimeObj.Hour != StDateTime.Null)
                        localDateTimeObj.Hour = (byte)val.TimeOfDay.Hours;
                    ///Save If Minute field not wild carded
                    if (localDateTimeObj.Minute != StDateTime.Null)
                        localDateTimeObj.Minute = (byte)val.TimeOfDay.Minutes;
                    ///Save If Seconds field not wild carded
                    if (localDateTimeObj.Second != StDateTime.Null)
                        localDateTimeObj.Second = (byte)val.TimeOfDay.Seconds;
                    ///Save If Hundred-Seconds field not wild carded
                    if (localDateTimeObj.Hundred != StDateTime.Null)
                        localDateTimeObj.Hundred = Convert.ToByte(val.TimeOfDay.Milliseconds / 10);
                    ///Save If UTC-OffSet field not wild carded
                    if (localDateTimeObj.UTCOffset != StDateTime.NullUTCOffset)
                    {
                        try
                        {
                            TimeZone tInfo = TimeZone.CurrentTimeZone;
                            TimeSpan UTCoffSet = tInfo.GetUtcOffset(val);
                            localDateTimeObj.UTCOffset = Convert.ToInt16(UTCoffSet.TotalMinutes);
                        }
                        catch
                        {
                            localDateTimeObj.UTCOffset = StDateTime.NullUTCOffset;
                        }
                    }

                }
            }

            public static DateTime StoreCustomLongDateTime_FromText(String txtDateTime,
                StDateTime localDateTimeObj,
                dtpCustomExtensions FormatEx = dtpCustomExtensions.dtpLongDateTimeWildCard)
            {
                DateTime exact_dt1 = DateTime.Now;
                CultureInfo _current_Culture = new CultureInfo("en-us");
                String CustomFormat = String.Empty, mvarCustomFormatMessage = String.Empty;
                try
                {
                    var match = Regex.Match((txtDateTime + ' '), FullDateTimeWildCardRegEx, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        GetCustomFormatMessage(dtpCustomExtensions.dtpLongDateTimeWildCard,
                            ref CustomFormat, ref mvarCustomFormatMessage);
                        ///Valid Date_Time Parsed from textInput
                        if (DateTime.TryParseExact(txtDateTime, CustomFormat
                            , _current_Culture,
                           DateTimeStyles.None, out exact_dt1)
                           && match.Success)
                        {
                            ///DateTime String Completely Parsed
                            ///Value = exact_dt1;
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                            ///here Reset All WildCards
                            if (FormatEx == dtpCustomExtensions.dtpLongDateTime)
                                Save_WildCards(new List<StDateTimeWildCards>(), localDateTimeObj);
                            else
                                Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT },
                                               localDateTimeObj);
                            StoreCustomDate_DateTime(exact_dt1, localDateTimeObj);
                            StoreCustomTime_DateTime(exact_dt1, localDateTimeObj);
                        }
                        ///DateTime String Is WildCarded
                        else if (match.Success)
                        {
                            localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;

                            ParseYearCustomYYYY(match, localDateTimeObj, _current_Culture);
                            ParseMonthCustomMMM(match, localDateTimeObj, _current_Culture);
                            ParseDayOfMonthCustomdd(match, localDateTimeObj, _current_Culture);
                            ParseDayOfWeekCustomddd(match, localDateTimeObj, _current_Culture);
                            ///Parsing Time Value
                            ParseHourCustomHH(match, localDateTimeObj, _current_Culture);
                            ParseMinuteCustom_mm(match, localDateTimeObj, _current_Culture);
                            ParseSecondCustom_ss(match, localDateTimeObj, _current_Culture);
                            ///Parse Hundred-Seconds
                            localDateTimeObj.Hundred = StDateTime.Null;
                            ParseGMTCustom(match, localDateTimeObj, _current_Culture);
                        }
                    }
                    else
                        throw new FormatException("Unable to verify CustomFormat StoreCustomDate_FromText");

                }
                catch (FormatException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new FormatException("Unable to verify CustomFormat StoreCustomDate_FromText", ex);
                }

                try
                {
                    exact_dt1 = localDateTimeObj.GetDateTime();
                }
                catch
                {
                    exact_dt1 = DateTime.MinValue;
                }
                return exact_dt1;
            }

            public static DateTime StoreCustomLongDate_FromText(String txtDateTime,
               StDateTime localDateTimeObj,
               dtpCustomExtensions FormatEx = dtpCustomExtensions.dtpLongDateWildCard)
            {
                DateTime exact_dt1 = DateTime.Now;
                CultureInfo _current_Culture = new CultureInfo("en-us");
                String CustomFormat = String.Empty, mvarCustomFormatMessage = String.Empty;
                try
                {
                    var match = Regex.Match((txtDateTime + ' '), FullDateWildCardRegEx, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        GetCustomFormatMessage(dtpCustomExtensions.dtpLongDateWildCard,
                            ref CustomFormat, ref mvarCustomFormatMessage);
                        //Valid Date_Time Parsed from textInput
                        if (DateTime.TryParseExact(txtDateTime, CustomFormat
                            , _current_Culture,
                           DateTimeStyles.None, out exact_dt1)
                           && match.Success)
                        {
                            //DateTime String Completely Parsed
                            //Value = exact_dt1;
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                            //here Reset All WildCards
                            if (FormatEx == dtpCustomExtensions.dtpLong || FormatEx == dtpCustomExtensions.dtpLongDateWildCard)
                                Save_WildCards(new List<StDateTimeWildCards>() 
                                { 
                                  StDateTimeWildCards.NullHour,
                                  StDateTimeWildCards.NullMinute, 
                                  StDateTimeWildCards.NullSecond, 
                                  StDateTimeWildCards.NullGMT
                                }, localDateTimeObj);
                            //else
                            //    Save_WildCards(new List<StDateTimeWildCards>(),
                            //                   localDateTimeObj);
                            StoreCustomDate_DateTime(exact_dt1, localDateTimeObj);
                        }
                        //DateTime String Is WildCarded
                        else if (match.Success)
                        {
                            localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                            //Parsing Date Value
                            ParseYearCustomYYYY(match, localDateTimeObj, _current_Culture);
                            ParseMonthCustomMMM(match, localDateTimeObj, _current_Culture);
                            ParseDayOfMonthCustomdd(match, localDateTimeObj, _current_Culture);
                            ParseDayOfWeekCustomddd(match, localDateTimeObj, _current_Culture);
                            //Parsing Time Value
                            localDateTimeObj.Hour = StDateTime.Null;
                            localDateTimeObj.Minute = StDateTime.Null;
                            localDateTimeObj.Second = StDateTime.Null;
                            localDateTimeObj.Hundred = StDateTime.Null;
                            localDateTimeObj.UTCOffset = StDateTime.NullUTCOffset;
                        }
                    }
                    else
                        throw new FormatException("Unable to verify CustomFormat StoreCustomDate_FromText");

                }
                catch (FormatException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new FormatException("Unable to verify CustomFormat StoreCustomDate_FromText", ex);
                }
                try
                {
                    exact_dt1 = localDateTimeObj.GetDate();
                }
                catch
                {
                    exact_dt1 = DateTime.MinValue;
                }
                return exact_dt1;
            }

            #region ParsexxxCustomFormat

            private static void ParseYearCustomYYYY(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                int NumVar = 0;
                #region ///Parse Year
                localStrMatch = _regExMatch.Groups["Year"].Value;
                if (int.TryParse(localStrMatch, out NumVar))
                {
                    localDateTimeObj.Year = (ushort)NumVar;
                }
                else
                    localDateTimeObj.Year = StDateTime.NullYear;
                #endregion
            }

            private static void ParseMonthCustomMMM(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                #region ///Parse Month

                ///Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|___
                localStrMatch = _regExMatch.Groups["Month"].Value;

                if (localStrMatch.Equals("ANY", StringComparison.OrdinalIgnoreCase) ||
                    localStrMatch.Equals("___", StringComparison.OrdinalIgnoreCase))
                {
                    localDateTimeObj.Month = StDateTime.Null;
                }
                else if (localStrMatch.Equals("DLSB", StringComparison.OrdinalIgnoreCase))
                {
                    localDateTimeObj.Month = StDateTime.DaylightSavingBegin;
                }
                else if (localStrMatch.Equals("DLSE", StringComparison.OrdinalIgnoreCase))
                {
                    localDateTimeObj.Month = StDateTime.DaylightSavingEnd;
                }
                else if (!String.IsNullOrEmpty(localStrMatch))
                {
                    List<string> AbvMonth = new List<string>();
                    for (int monthNo = 1; monthNo <= 12; monthNo++)
                    {
                        string TlocalStrMatch = _current_Culture.DateTimeFormat.GetMonthName(monthNo);
                        AbvMonth.Add(TlocalStrMatch.Substring(0, 3));
                    }
                    int indexOfStrMatch = AbvMonth.IndexOf(localStrMatch);
                    localDateTimeObj.Month = (byte)(indexOfStrMatch + 1);
                }
                else
                    throw new FormatException("Invalid Object Month Format");

                #endregion
            }

            private static void ParseDayOfMonthCustomdd(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                int NumVar = 0;
                #region ///Parse DayOfMonth
                ///?<DayOfMonth>0?[1-9]|[1-2][0-9]|3[01]|ANY|LST|2LST|__|_
                localStrMatch = _regExMatch.Groups["DayOfMonth"].Value;
                if (!String.IsNullOrEmpty(localStrMatch))
                {
                    if (localStrMatch.Equals("ANY", StringComparison.OrdinalIgnoreCase) ||
                        localStrMatch.Equals("__", StringComparison.OrdinalIgnoreCase) ||
                        localStrMatch.Equals("_", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.DayOfMonth = StDateTime.Null;
                    }
                    else if (localStrMatch.Equals("LST", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.DayOfMonth = StDateTime.LastDayOfMonth;
                    }
                    else if (localStrMatch.Equals("2LST", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.DayOfMonth = StDateTime.SecondLastDayOfMonth;
                    }
                    else if (int.TryParse(localStrMatch, out NumVar))
                    {
                        localDateTimeObj.DayOfMonth = (byte)NumVar;
                    }
                    else
                        throw new FormatException("Invalid Object DayOfMonth Format");
                }
                #endregion
            }

            private static void ParseDayOfWeekCustomddd(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                int NumVar = 0;
                #region ///Parse DayOfWeek
                ///(?<DayOfWeek_>___|ANY|Sun|Mon|Tue|Wed|Thu|Fri|Sat)
                localStrMatch = _regExMatch.Groups["DayOfWeek_"].Value;
                if (!String.IsNullOrEmpty(localStrMatch))
                {
                    if (localStrMatch.Equals("ANY", StringComparison.OrdinalIgnoreCase) ||
                        localStrMatch.Equals("___", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.DayOfWeek = StDateTime.Null;
                    }
                    else if (!String.IsNullOrEmpty(localStrMatch))
                    {
                        string[] names = Enum.GetNames(typeof(DayOfWeek));
                        List<string> DayOfWeekNames = new List<string>(names);
                        for (int index = 0; index < DayOfWeekNames.Count; index++)
                        {
                            DayOfWeekNames[index] = DayOfWeekNames[index].Substring(0, 3);
                        }
                        DayOfWeekNames.RemoveAt(0);
                        DayOfWeekNames.Add("Sun");
                        int indexOfMatch = DayOfWeekNames.IndexOf(localStrMatch);
                        localDateTimeObj.DayOfWeek = (byte)(1 + indexOfMatch);
                    }
                    else
                        throw new FormatException("Invalid Object DayOfWeek Format");
                }
                #endregion
            }

            private static void ParseHourCustomHH(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                int NumVar = 0;
                #region ///Parse Hour
                ///(?<Hour>2[0-3]|[0-1][0-9]|ANY|__)
                localStrMatch = _regExMatch.Groups["Hour"].Value;
                if (!String.IsNullOrEmpty(localStrMatch))
                {
                    if (localStrMatch.Equals("ANY", StringComparison.OrdinalIgnoreCase) ||
                        localStrMatch.Equals("__", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.Hour = StDateTime.Null;
                    }
                    else if (int.TryParse(localStrMatch, out NumVar))
                    {
                        localDateTimeObj.Hour = (byte)NumVar;
                    }
                    else
                        throw new FormatException("Invalid Object Hour hh Format");
                }

                #endregion
            }

            private static void ParseMinuteCustom_mm(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                int NumVar = 0;
                #region ///Parse Minute
                ///(?<Minute>[0-5][0-9]|ANY|__)
                localStrMatch = _regExMatch.Groups["Minute"].Value;
                if (!String.IsNullOrEmpty(localStrMatch))
                {
                    if (localStrMatch.Equals("ANY", StringComparison.OrdinalIgnoreCase) ||
                        localStrMatch.Equals("__", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.Minute = StDateTime.Null;
                    }
                    else if (int.TryParse(localStrMatch, out NumVar))
                    {
                        localDateTimeObj.Minute = (byte)NumVar;
                    }
                    else
                        throw new FormatException("Invalid Object Minute Format");
                }
                #endregion
            }

            private static void ParseSecondCustom_ss(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                int NumVar = 0;
                #region ///Parse Second
                ///(?<Second>:(60|[0-5][0-9]|ANY|__))
                localStrMatch = _regExMatch.Groups["Second"].Value;
                if (!String.IsNullOrEmpty(localStrMatch))
                {
                    if (localStrMatch.Equals("ANY", StringComparison.OrdinalIgnoreCase) ||
                        localStrMatch.Equals("__", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.Second = StDateTime.Null;
                    }
                    else if (int.TryParse(localStrMatch, out NumVar))
                    {
                        localDateTimeObj.Second = (byte)NumVar;
                    }
                    else
                        throw new FormatException("Invalid Object Second ss Format");
                }
                #endregion
            }

            private static void ParseGMTCustom(Match _regExMatch, StDateTime localDateTimeObj, CultureInfo _current_Culture)
            {
                String localStrMatch = null;
                int NumVar = 0;
                #region ///Parse UTC_OffSet
                ///(?<GMT>[-\+][0-9]{2}[0-5][0-9]|ANY|___))
                localStrMatch = _regExMatch.Groups["GMT"].Value;
                if (!String.IsNullOrEmpty(localStrMatch))
                {
                    if (localStrMatch.Equals("ANY", StringComparison.OrdinalIgnoreCase) ||
                        localStrMatch.Equals("___", StringComparison.OrdinalIgnoreCase))
                    {
                        localDateTimeObj.UTCOffset = StDateTime.NullUTCOffset;
                    }
                    else if (int.TryParse(localStrMatch, out NumVar))
                    {
                        if (NumVar > 0 || NumVar < 0)
                        {
                            ///Convert +/-0959 format to minutes
                            int minute = NumVar % 100;
                            NumVar = Convert.ToInt32(NumVar / 100);
                            minute = minute + (NumVar * 60);
                            NumVar = minute;
                        }
                        localDateTimeObj.UTCOffset = (short)NumVar;
                    }
                    else
                        throw new FormatException("Invalid Object GMT +/-0000 Format");
                }
                #endregion
            }

            #endregion

            #region Member_Methods

            /// <summary>
            /// Convert Explicit Specified Complete stDateTime Object into DateTime Stamp Object
            /// </summary>
            /// <returns></returns>
            public static DateTime GetDateTime(StDateTime localDateTimeObj)
            {
                try
                {
                    DateTime dt;
                    if (localDateTimeObj.IsDateTimeConvertible)    ///Could be converted 
                        dt = new DateTime(localDateTimeObj.Year, localDateTimeObj.Month, localDateTimeObj.DayOfMonth, localDateTimeObj.Hour,
                            localDateTimeObj.Minute, localDateTimeObj.Second, (localDateTimeObj.Hundred == Null) ? 0 : localDateTimeObj.Hundred * 10);
                    else
                    {
                        //throw new Exception("Unable to convert stDateTime object into valid DateTime Stamp");
                        dt = DefaultDateTime;
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static void SetDateTime(DateTime objDateTime, StDateTime localDateTimeObj)
            {
                try
                {
                    localDateTimeObj.kind = DateTimeType.DateTime;
                    localDateTimeObj.Year = (ushort)objDateTime.Year;
                    localDateTimeObj.Month = (byte)objDateTime.Month;
                    localDateTimeObj.DayOfMonth = (byte)objDateTime.Day;
                    localDateTimeObj.DayOfWeek = (byte)((objDateTime.DayOfWeek != System.DayOfWeek.Sunday) ? (byte)objDateTime.DayOfWeek : 7);

                    localDateTimeObj.Hour = (byte)objDateTime.Hour;
                    localDateTimeObj.Minute = (byte)objDateTime.Minute;
                    localDateTimeObj.Second = (byte)objDateTime.Second;
                    localDateTimeObj.Hundred = (byte)(objDateTime.Millisecond / 10);

                    localDateTimeObj.UTCOffset = NullUTCOffset;
                    localDateTimeObj.ClockStatus = Null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static TimeSpan GetTime(StDateTime localDateTimeObj)
            {
                try
                {
                    TimeSpan dt;
                    if (localDateTimeObj.IsTimeConvertible)    ///Could be converted 
                        dt = new TimeSpan(0, localDateTimeObj.Hour, localDateTimeObj.Minute, localDateTimeObj.Second,
                            (localDateTimeObj.Hundred == Null) ? 0 : localDateTimeObj.Hundred * 10);
                    else
                    {
                        ///throw new Exception("Unable to convert stDateTime object into valid TimeSpan");
                        dt = DefaultDateTime.TimeOfDay;
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static void SetTime(TimeSpan objTime, StDateTime localDateTimeObj)
            {
                try
                {
                    localDateTimeObj.kind = DateTimeType.Time;
                    localDateTimeObj.Year = NullYear;
                    localDateTimeObj.Month = Null;
                    localDateTimeObj.DayOfMonth = Null;
                    localDateTimeObj.DayOfWeek = Null;

                    localDateTimeObj.Hour = (byte)objTime.Hours;
                    localDateTimeObj.Minute = (byte)objTime.Minutes;
                    localDateTimeObj.Second = (byte)objTime.Seconds;
                    localDateTimeObj.Hundred = (byte)(objTime.Milliseconds / 10);

                    localDateTimeObj.UTCOffset = NullUTCOffset;
                    localDateTimeObj.ClockStatus = Null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static void SetDate(DateTime objDate, StDateTime localDateTimeObj)
            {
                try
                {
                    SetDateTime(objDate, localDateTimeObj);

                    localDateTimeObj.kind = DateTimeType.Date;
                    localDateTimeObj.Hour = Null;
                    localDateTimeObj.Minute = Null;
                    localDateTimeObj.Second = Null;
                    localDateTimeObj.Hundred = Null;

                    localDateTimeObj.UTCOffset = NullUTCOffset;
                    localDateTimeObj.ClockStatus = Null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static DateTime GetDate(StDateTime localDateTimeObj)
            {
                try
                {
                    DateTime dt = new DateTime();
                    if (localDateTimeObj.IsDateConvertible)          ///Could be converted safe
                        dt = new DateTime(localDateTimeObj.Year, localDateTimeObj.Month, localDateTimeObj.DayOfMonth);
                    else
                    {
                        ///throw new Exception("Unable to convert stDateTime object into valid DateTime(Date)");
                        dt = DefaultDateTime;
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            public static bool IsRepeatDailyFormat(StDateTime localDateTime)
            {
                try
                {
                    var WildCards = Get_WildCardsChecked(localDateTime);
                    if (WildCards.Contains(StDateTimeWildCards.NullYear) &&
                       WildCards.Contains(StDateTimeWildCards.NullMonth) &&
                       WildCards.Contains(StDateTimeWildCards.NullDay) &&
                        WildCards.Contains(StDateTimeWildCards.NullDayOfWeek) &&
                        //Time WildCard Check
                        !(WildCards.Contains(StDateTimeWildCards.NullHour) ||
                       WildCards.Contains(StDateTimeWildCards.NullMinute) ||
                        WildCards.Contains(StDateTimeWildCards.NullSecond)))
                        return true;
                    else
                        return false;
                }
                catch
                { }
                return false;
            }
        }
    }

    #endregion

    public enum StDateTimeWildCards : ushort
    {
        NullYear = 0x00,
        NullMonth = 0x01,
        DLSBEGIN = 0x02,
        DLSEND = 0x03,
        NullDay = 0x04,
        LSTDayOfMonth = 0x05,
        _2LSTDayOfMonth = 0x06,
        NullDayOfWeek = 0x0b,
        NullHour = 0x07,
        NullMinute = 0x08,
        NullSecond = 0x09,
        NullGMT = 0x0a
    }
}
