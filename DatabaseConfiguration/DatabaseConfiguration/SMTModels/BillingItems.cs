using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "BillingItems")]
    public class BillingItems : CommonModels.BillingItems
    {
    }
}
