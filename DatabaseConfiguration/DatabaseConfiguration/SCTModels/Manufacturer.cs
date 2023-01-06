using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Manufacturer")]
    public class Manufacturer : CommonModels.Manufacturer
    {
    }
}
