using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "status_word")]
    public class status_word
    {
        [Column(Name = "Code", IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int Code { get; set; }

        [Column(Name = "Name", DbType = "VARCHAR")]
        public string Name { get; set; }
    }
}
