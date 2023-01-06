using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    //[Table(Name = "capture_objects")]
    //public class capture_objects
    //{
    //    [Column(Name = "SequenceId", DbType = "INTEGER")]
    //    public int SequenceId { get; set; }

    //    [Column(Name = "OBIS_Index", DbType = "decimal")]
    //    [Key]
    //    [System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
    //    public decimal OBIS_Index { get; set; }

    //    [Column(Name = "AttributeNo", DbType = "TINTINT")]
    //    public byte AttributeNo { get; set; }

    //    [Column(Name = "DataIndex", DbType = "INTEGER")]
    //    public int DataIndex { get; set; }

    //    [Column(Name = "ConfigId", DbType = "INTEGER")]
    //    public int? ConfigId { get; set; }

    //    [Column(Name = "GroupId", DbType = "INTEGER")]
    //    public int? GroupId { get; set; }

    //    [Column(Name = "Target_OBIS_Index", DbType = "decimal")]
    //    [Key]
    //    [System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
    //    public decimal Target_OBIS_Index { get; set; }

    //    [Column(Name = "DeviceId", DbType = "INTEGER")]
    //    [Key]
    //    [System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
    //    public int? DeviceId { get; set; }

    //    //[Column(Name = "DatabaseField", DbType = "VARCHAR")]
    //    //public string DatabaseField { get; set; }
    //}

    [Table(Name = "capture_objects")]
    public class capture_objects
    {
        [Key]
        [Column(Name = "id", IsPrimaryKey = true, DbType = "INTEGER")] //???
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column(Name = "SequenceId", DbType = "INTEGER")]
        public int SequenceId { get; set; }

        [Column(Name = "OBIS_Index", DbType = "BIGINT")]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        public long OBIS_Index { get; set; }

        [Column(Name = "AttributeNo", DbType = "TINYINT")]
        public byte AttributeNo { get; set; }

        [Column(Name = "DataIndex", DbType = "INTEGER")]
        public int DataIndex { get; set; }

        [Column(Name = "ConfigId", DbType = "INTEGER")]
        public int? ConfigId { get; set; }

        [Column(Name = "GroupId", DbType = "INTEGER")]
        public int? GroupId { get; set; }

        [Column(Name = "Target_OBIS_Index", DbType = "BIGINT")]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        public long Target_OBIS_Index { get; set; }

        [Column(Name = "DeviceId", DbType = "INTEGER")]
        //[Key]
        //[System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
        public int? DeviceId { get; set; }

        [Column(Name = "databasefield", DbType = "VARCHAR")]
        public string databasefield { get; set; }

        [Column(Name = "Multiplier", DbType = "INTEGER")]
        public short? Multiplier { get; set; }
    }


}
