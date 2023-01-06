using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Billing_Items")]
    public class BillingItems:CommonModels.BillingItems
    {
        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public int ConfigId { get; set; }
    }
}
