using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "device")]
    public class Device//:CommonModels.Device
    {
        [Column(Name = "Product", DbType = "VARCHAR")]
        public string Product { get; set; }

        [Column(Name = "Accuracy", DbType = "VARCHAR")]
        public string Accuracy { get; set; }
    }
}
