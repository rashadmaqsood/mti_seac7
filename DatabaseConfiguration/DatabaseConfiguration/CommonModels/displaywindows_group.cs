using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "displaywindows_group")]
    public class displaywindows_group
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Dw_Group_Name", DbType = "VARCHAR")]
        public string Dw_Group_Name { get; set; }
    }
}
