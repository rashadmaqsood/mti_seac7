using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SharedCode.Comm.DataContainer
{
    public enum MDCEvents : ushort
    {
        high_ev_counter = 0,
        low_ev_counter = 1,
        high_lp_counter = 2,
        low_lp_counter = 3,
        cs_valid_sync = 4,
        pwd_change = 5,
        pwd_error = 6,
        mdi_date_change = 7,
        tbe1_change = 8,
        tbe2_change = 9,
        type_change = 10,
        param_m_limit_time = 11,
        contactor_param = 12,
        lp_counter_mismatch = 13,
        cs_invalid_sync = 14,
        exception_occur = 15,
        ev_counter_mismatch = 16,
        mb_counter_mismatch = 17,
        contactor_status_on = 18,
        contactor_status_off = 19,
        param_limit =20,
        Param_Monitoring_Time =21,
        param_ct_pt =22,
        Param_Decimal_Point=23,
        param_energy =24,
        rtc_failed_battery=25,

        Invalid_SecurityData = 26,
        SecurityKey_Change = 27,
        Invalid_AuthenticationTAG = 28,

         high_lp2_counter = 29,
         low_lp2_counter = 30,
         lp2_counter_mismatch = 31,
    };

    public class MDCEventsClass
    {
        public BitArray MDCCombineEvents { get; set; }

        public bool IsMDCEventOuccer { get; set; }

        public MDCEventsClass()
        {
            MDCCombineEvents = new BitArray(48);
        }

    }
}


