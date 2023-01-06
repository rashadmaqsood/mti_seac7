using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "obis_rights_group")]
    public class obis_rights_group
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Group_Name", DbType = "VARCHAR")]
        public string Group_Name { get; set; }

        [Column(Name = "Update_Rights", DbType = "BIT")]
        public bool Update_Rights { get; set; }
    }
}
