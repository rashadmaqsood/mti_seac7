using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Event_Logs")]
    public class Event_Logs : CommonModels.Event_Logs
    {
        [Column(Name = "ConfigId", DbType = "INTEGER")]
        public int? ConfigId { get; set; }
    }
}
