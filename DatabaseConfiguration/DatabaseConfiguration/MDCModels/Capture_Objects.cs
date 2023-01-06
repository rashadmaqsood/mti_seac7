using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "capture_objects")]
    public class Capture_Objects//: CommonModels.Capture_Objects
    {
        [Column(Name = "DeviceId", DbType = "INTEGER")]
        public int DeviceId { get; set; }

        [Column(Name = "ConfigId", DbType = "INTEGER")]
        public new int? ConfigId { get; set; }

        [Column(Name = "GroupId", DbType = "INTEGER")]
        public new int? GroupId { get; set; }
    }
}
