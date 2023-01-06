using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "bill_tariff_quantity")]
    public class bill_tariff_quantity
    {
        [Column(Name = "id", IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "BillItemId", DbType = "INTEGER")]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.Schema.Index(Order = 0)]
        public int BillItemId { get; set; }

        [Column(Name = "OBIS_Index", DbType = "BIGINT")]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.Schema.Index(Order = 1)]
        public long OBIS_Index { get; set; }

        [Column(Name = "SequenceId", DbType = "INTEGER")]
        public int? SequenceId { get; set; }

        [Column(Name = "DatabaseField", DbType = "VARCHAR")]
        public string DatabaseField { get; set; }
    }
}
