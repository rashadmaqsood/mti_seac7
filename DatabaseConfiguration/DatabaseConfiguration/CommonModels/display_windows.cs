using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "display_windows")]
    public class display_windows
    {
        [Column(Name = "id", DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Category", DbType = "TINYINT")]
        public byte Category { get; set; }

        [Column(Name = "Label", DbType = "VARCHAR")]
        public string Label { get; set; }

        [Column(Name = "AttributeNo", DbType = "TINYINT")]
        public byte? AttributeNo { get; set; }

        [Column(Name = "WinNumberToDisplay", DbType = "SMALLINT")]
        public short? WinNumberToDisplay { get; set; }

        [Column(Name = "QuantityIndex", DbType = "BIGINT")]
        public long QuantityIndex { get; set; }

        [Column(Name = "SequenceId", DbType = "SMALLINT")]
        public short? SequenceId { get; set; }

        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public long? ConfigId { get; set; }

        [Column(Name = "DisplayWindowsGroupId", DbType = "INTEGER")]
        public int? DisplayWindowsGroupId { get; set; }
    }
}
