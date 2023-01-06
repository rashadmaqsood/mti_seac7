using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "rights")]
    public class rights
    {
        [Column(Name = "id", DbType = "BIGINT")]
        public long id { get; set; }

        [Column(Name = "type", DbType = "TINYINT")]
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
        public byte type { get; set; }

        [Column(Name = "SubId", DbType = "TINYINT")]
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        public byte SubId { get; set; }

        [Column(Name = "value", DbType = "TINYINT")]
        public byte value { get; set; }

        [Column(Name = "OBIS_Right_Id", DbType = "BIGINT")]
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
        public long OBIS_Right_Id { get; set; }
    }
}
