using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Capture_Objects")]
    public class Capture_Objects : CommonModels.Capture_Objects
    {
        [Column(Name = "OBIS_Index", DbType = "DOUBLE")]
        [Key]
        public new decimal OBIS_Index { get; set; }

        [Column(Name = "Target_OBIS_Index", DbType = "DOUBLE")]
        [Key]
        public new decimal Target_OBIS_Index { get; set; }

        [Column(Name = "ConfigId", DbType = "INTEGER")]
        [Key]
        public new int? ConfigId { get; set; }
    }
}
