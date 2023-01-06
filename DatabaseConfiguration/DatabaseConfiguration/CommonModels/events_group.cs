using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;


namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "events_group")]
    public class events_group
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Events_group_Name", DbType = "VARCHAR")]
        public string Events_Group_Name { get; set; }
    }
}
