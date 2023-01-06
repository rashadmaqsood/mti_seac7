using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SEACModels
{
    [Table(Name = "configuration")]
    public class Configuration : CommonModels.Configuration
    {
        [Column(Name = "DisplayWindowGroupId", DbType = "INTEGER")]
        public int DisplayWindowGroupId { get; set; }
    }
}
