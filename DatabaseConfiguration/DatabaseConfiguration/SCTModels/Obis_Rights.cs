using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "OBIS_Rights")]
    public class Obis_Rights : CommonModels.Obis_Rights
    {
        [Column(Name = "ServerSapId", DbType = "INTEGER")]
        public int ServerSapId { get; set; }

        [Column(Name = "ClientSapId", DbType = "INTEGER")]
        public int ClientSapId { get; set; }
    }
}
