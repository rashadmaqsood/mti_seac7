using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm
{
    public class Monthly_Billing_data
    {
        public List<m_data> monthly_billing_data;
        public Monthly_Billing_data()
        {
            this.monthly_billing_data = new List<m_data>();
        }
    }

    public class m_data
    {
        public Cumulative_billing_data billData_obj;
        public uint Counter;

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
