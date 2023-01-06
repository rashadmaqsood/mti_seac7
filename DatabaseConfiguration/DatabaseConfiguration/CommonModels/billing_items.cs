using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "billing_items")]
    public class billing_items
    {
        [Column(Name = "id",IsPrimaryKey = true, DbType = "INTEGER")]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column(Name = "Label", DbType = "VARCHAR")]
        public string Label { get; set; }

        [Column(Name = "FormatSpecifier", DbType = "VARCHAR")]
        public string FormatSpecifier { get; set; }

        [Column(Name = "Unit", DbType = "VARCHAR")]
        public string Unit { get; set; }

        [Column(Name = "Multiplier", DbType = "TINYINT")]
        public int? Multiplier { get; set; }

        [Column(Name = "SequenceId", DbType = "INTEGER")]
        public int? SequenceId { get; set; }

        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public long? ConfigId { get; set; }

        [Column(Name = "BillItemGroupId", DbType = "INTEGER")]
        public int BillItemGroupId { get; set; }

    }
}
