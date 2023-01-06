using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "event_info")]
    public class Event_Info : CommonModels.event_info
    {
        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public int ConfigId { get; set; }
    }
}
