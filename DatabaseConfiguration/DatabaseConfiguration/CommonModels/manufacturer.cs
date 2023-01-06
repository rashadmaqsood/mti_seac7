using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "manufacturer")]
    public class manufacturer
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Manufacturer_Name", DbType = "VARCHAR")]
        public string Manufacturer_Name { get; set; }

        [Column(Name = "Code", DbType = "VARCHAR")]
        public string Code { get; set; }
    }
}
