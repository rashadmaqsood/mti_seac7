using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "event_info")]
    public class event_info
    {
        [Column(Name = "id", DbType = "INTEGER", IsPrimaryKey = true)]
        public int id { get; set; }

        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        [Column(Name = "EventGroupId", DbType = "INTEGER")]
        public int EventCode { get; set; }

        [Column(Name = "Label", DbType = "VARCHAR")]
        public string Label { get; set; }

        [Column(Name = "MaxEventCount", DbType = "INTEGER")]
        public int MaxEventCount { get; set; }

        [Column(Name = "EventNo", DbType = "INTEGER")]
        public int? EventNo { get; set; }

        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        [Column(Name = "EventGroupId", DbType = "INTEGER")]
        public int EventGroupId { get; set; }

        //[Column(Name = "CautionNumber", DbType = "INTEGER")]
        //public int CautionNumber { get; set; }

        //[Column(Name = "FlashTime", DbType = "INTEGER")]
        //public int FlashTime { get; set; }

        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public long? ConfigId { get; set; }
    }
}
