using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "load_profile_channels")]
    public class load_profile_channels
    {
        [Column(Name = "id", DbType = "INTEGER", IsPrimaryKey = true)]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        public int id { get; set; }

        [Column(Name = "QuantityIndex", DbType = "BIGINT")]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        public long QuantityIndex { get; set; }

        [Column(Name = "Label", DbType = "VARCHAR")]
        public string Label { get; set; }

        [Column(Name = "AttributeNo", DbType = "TINYINT")]
        public byte? AttributeNo { get; set; }

        [Column(Name = "Multiplier", DbType = "SMALLINT")]
        public short? Multiplier { get; set; }

        [Column(Name = "SequenceId", DbType = "SMALLINT")]
        public short? SequenceId { get; set; }

        [Column(Name = "FormatSpecifier", DbType = "VARCHAR")]
        public string FormatSpecifier { get; set; }

        [Column(Name = "Unit", DbType = "VARCHAR")]
        public string Unit { get; set; }

        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public long? ConfigId { get; set; }

        [Column(Name = "LoadProfileGroupId", DbType = "INTEGER")]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
        public int LoadProfileGroupId { get; set; }
    }
}
