using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Event_Info")]
    public class Event_Info : CommonModels.Event_Info
    {
        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public int ConfigId { get; set; }

        [Column(Name = "CautionNumber", DbType = "INTEGER")]
        public int CautionNumber { get; set; }
    }
}
