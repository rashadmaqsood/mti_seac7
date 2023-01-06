using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "event_logs")]
    public class Event_Logs : CommonModels.Event_Logs
    {
        [Column(Name = "ConfigId", DbType = "INTEGER")]
        public int? ConfigId { get; set; }
    }
}
