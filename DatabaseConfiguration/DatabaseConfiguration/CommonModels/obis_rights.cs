using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
     [Table(Name = "obis_rights")]
    public class obis_rights
    {
        [Column(IsPrimaryKey = true, DbType = "BIGINT")]
        [Key]
        public long id { get; set; }

        [Column(Name = "ServerSapId", DbType = "BIGINT")]
        public long? ServerSapId { get; set; }

        [Column(Name = "ClientSapId", DbType = "BIGINT")]
        public long? ClientSapId { get; set; }

        [Column(Name = "OBIS_Index", DbType = "BIGINT")]
        public long OBIS_Index { get; set; }

        [Column(Name = "Version", DbType = "INTEGER")]
        public int? Version { get; set; }

        [Column(Name = "ObisRightGroupId", DbType = "INTEGER")]
        public int? ObisRightGroupId { get; set; }
    }
}
