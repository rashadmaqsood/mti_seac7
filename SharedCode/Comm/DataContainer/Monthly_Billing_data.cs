using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedCode.Comm.DataContainer
{
    public class Monthly_Billing_data
    {
        public List<m_data> monthly_billing_data;
        public StringBuilder DBFields { get; set; } = new StringBuilder();

        //public int CustomerStatusCode;

        //public DateTime CustomerStatusChangeDate;

        public int MeterTariffCode;

        public Monthly_Billing_data()
        {
            monthly_billing_data = new List<m_data>();
        }

        //public Monthly_Billing_data GetValidatedMonthlyBillingData()
        //{
        //    var dataOfStatusChangeDate = monthly_billing_data.
        //        Find(x => x.billData_obj.date.Year == CustomerStatusChangeDate.Year && x.billData_obj.date.Month == CustomerStatusChangeDate.Month);
        //    if (dataOfStatusChangeDate != null)
        //    {
        //        dataOfStatusChangeDate.CustomerStatusCode = this.CustomerStatusCode;
        //        dataOfStatusChangeDate.MeterTariffCode = this.MeterTariffCode;
        //        dataOfStatusChangeDate.CustomerStatusChangeDate = this.CustomerStatusChangeDate;
        //    }
          
        //    return this;
        //}
    }

    public class m_data
    {
        public Cumulative_billing_data billData_obj;
        public StringBuilder Values { get; set; } = new StringBuilder();

        public uint Counter;

        //public int CustomerStatusCode;

        //public DateTime CustomerStatusChangeDate;

        //public int MeterTariffCode;
        public m_data()
        {
            billData_obj = new Cumulative_billing_data();
        }
    }

    public class Monthly_Billing_data_SinglePhase
    {
        public List<m_data_singlePhase> monthly_billing_data;
        public Monthly_Billing_data_SinglePhase()
        {
            this.monthly_billing_data = new List<m_data_singlePhase>();
        }
    }

    public class m_data_singlePhase
    {
        public cumulativeBilling_SinglePhase billData_obj;
        public uint Counter;

        public m_data_singlePhase()
        {
            billData_obj = new cumulativeBilling_SinglePhase();
        }
    }
}
