using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "billing_items")]
    public class BillingItems//: CommonModels.BillingItems
    {
        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public int? ConfigId { get; set; }

        [Column(Name = "Label", DbType = "VARCHAR")]
        public new string Label { get; set; }

        [Column(Name = "FormatSpecifier", DbType = "VARCHAR")]
        public new string FormatSpecifier { get; set; }

        [Column(Name = "Unit", DbType = "VARCHAR")]
        public new string Unit { get; set; }

        [Column(Name = "Multiplier", DbType = "TINYINT")]
        public new int? Multiplier { get; set; }

        [Column(Name = "SequenceId", DbType = "INTEGER")]
        public new int? SequenceId { get; set; }

        [Key]
        [Column(Name = "BillItemGroupId", DbType = "INTEGER")]
        public new int BillItemGroupId { get; set; }
    }
}
