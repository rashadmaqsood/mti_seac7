using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SEACModels
{
    [Table(Name = "EventInfo")]
    public class Event_Info : CommonModels.Event_Info
    {
        [Column(Name = "CautionNumber", DbType = "INTEGER")]
        public int CautionNumber { get; set; }

        [Column(Name = "FlashTime", DbType = "INTEGER")]
        public int FlashTime { get; set; }
    }
}
