using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "Device")]
    public class Device : CommonModels.Device
    { }
}
