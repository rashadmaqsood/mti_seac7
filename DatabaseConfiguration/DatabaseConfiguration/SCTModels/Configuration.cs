using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Configuration_New")]
    public class Configuration : CommonModels.Configuration
    {
        [Column(Name = "DisplayWindowGroupId", DbType = "INTEGER")]
        public int DisplayWindowGroupId { get; set; }
    }
}
