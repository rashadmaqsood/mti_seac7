using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "users")]
    public class users
    {
        [Column(Name = "id", IsPrimaryKey = true, DbType = "INTEGER")]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column(Name = "user_name", DbType = "VARCHAR")]
        public string user_name { get; set; }

        [Column(Name = "user_password", DbType = "VARCHAR")]
        public string user_password { get; set; }

        [Column(Name = "user_type", DbType = "INTEGER")]
        public int user_type { get; set; }
    }
}