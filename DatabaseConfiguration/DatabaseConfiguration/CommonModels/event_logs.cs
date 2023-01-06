using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "event_logs")]
    public class event_logs
    {
        /// <summary>
        /// Added by Khalil
        /// </summary>
        [Column(Name = "id_pk", IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id_pk { get; set; }

        [Column(Name = "id", DbType = "INTEGER")]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        public int id { get; set; }

        [Column(Name = "EventLogIndex", DbType = "BIGINT")]
        public long? EventLogIndex { get; set; }

        [Column(Name = "EventCounterIndex", DbType = "BIGINT")]
        public long? EventCounterIndex { get; set; }

        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public long? ConfigId { get; set; }

        [Column(Name = "EventGroupId", DbType = "INTEGER")]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        public int EventGroupId { get; set; }
    }
}
