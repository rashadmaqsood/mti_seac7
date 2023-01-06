using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "manufacturer")]
    public class Manufacturer:CommonModels.Manufacturer
    {
    }
}
