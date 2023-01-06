using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "obis_rights")]
    public class Obis_Rights : CommonModels.Obis_Rights
    {
        [Column(Name = "ServerSapId", DbType = "INTEGER")]
        public int ServerSapId { get; set; }

        [Column(Name = "ClientSapId", DbType = "INTEGER")]
        public int ClientSapId { get; set; }
    }
}
