using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "all_quantities")]
    public class all_quantities
    {
        [Column(Name = "id", IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "OBIS_Index", DbType = "BIGINT")]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public long OBIS_Index { get; set; }

        [Column(Name = "Label", DbType = "VARCHAR")]
        public string Label { get; set; }

        [Column(Name = "dp_name", DbType = "VARCHAR")]
        public string DP_Name { get; set; }

        [Column(Name = "unit", DbType = "VARCHAR")]
        public string Unit { get; set; }

        [Column(Name = "priority", DbType = "INTEGER")]
        public int? Priority { get; set; }
    }
}
