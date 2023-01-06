using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "obis_details")]
    public class obis_details
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Obis_Code", DbType = "BIGINT")]
        public long Obis_Code { get; set; }

        [Column(Name = "Default_OBIS_Code", DbType = "BIGINT")]
        public long Default_OBIS_Code { get; set; }

        [Column(Name = "Device_Id", DbType = "INTEGER")]
        public int Device_Id { get; set; }
    }
}
