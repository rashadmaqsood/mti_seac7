using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "authentication_type")]
    public class authentication_type
    {
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        [Column(Name = "id", IsPrimaryKey = true, DbType = "INTEGER")]
        public int Id { get; set; }

        [Column(Name = "Authentication_Type_Name", DbType = "VARCHAR")]
        public string Authentication_Type_Name { get; set; }
    }
}
