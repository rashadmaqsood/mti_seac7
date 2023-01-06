using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SEACModels
{
    [Table(Name = "BillingItems")]
    public class BillingItems : CommonModels.BillingItems
    {
    }
}
