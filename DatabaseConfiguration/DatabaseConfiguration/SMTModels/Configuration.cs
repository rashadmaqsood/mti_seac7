using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "Configuration")]
    public class Configuration : CommonModels.Configuration
    {
        [Column(Name = "DisplayWindowGroupId", DbType = "INTEGER")]
        public int DisplayWindowGroupId { get; set; }
    }
}
