using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS
{
    public enum dtpCustomExtensions
    {
        dtpLong = 0,
        dtpShort = 1,
        dtpTime = 2,
        dtpShortDateShortTimeAMPM = 3,
        dtpShortDateLongTimeAMPM = 4,
        dtpShortDateShortTime24Hour = 5,
        dtpShortDateLongTime24Hour = 6,
        dtpLongDateShortTimeAMPM = 7,
        dtpLongDateLongTimeAMPM = 8,
        dtpLongDateShortTime24Hour = 9,
        dtpLongDateLongTime24Hour = 10,
        dtpSortableDateAndTimeLocalTime = 11,
        dtpUTFLocalDateAndShortTimeAMPM = 12,
        dtpUTFLocalDateAndLongTimeAMPM = 13,
        dtpUTFLocalDateAndShortTime24Hour = 14,
        dtpUTFLocalDateAndLongTime24Hour = 15,
        dtpShortTimeAMPM = 16,
        dtpShortTime24Hour = 17,
        dtpLongTime24Hour = 18,
        dtpYearAndMonthName = 19,
        dtpMonthNameAndDay = 20,
        dtpYear4Digit = 21,
        dtpMonthFullName = 22,
        dtpMonthShortName = 23,
        dtpDayFullName = 24,
        dtpDayShortName = 25,
        dtpDayOfMonth = 37,
        dtpShortDateAMPM = 26,
        dtpShortDateMorningAfternoon = 27,
        dtpCustom = 28,
        dtpLongDateTimeWildCard = 29,
        dtpLongDateWildCard = 30,
        dtpLongDateTime = 31,
        dtpShortDate = 32,
        dtpLongTime = 33,
        dtpShortInterval = 34,
        dtpShortIntervalFixed = 35,
        dtpShortIntervalTimeSink = 36
    }
}
