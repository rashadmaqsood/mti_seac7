using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "loadprofile_group")]
    public class loadprofile_group
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "LoadProfile_Group_Name", DbType = "VARCHAR")]
        public string LoadProfile_Group_Name { get; set; }
    }
}
