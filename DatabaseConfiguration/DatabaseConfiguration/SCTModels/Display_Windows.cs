using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Display_Windows")]
    public class Display_Windows : CommonModels.Display_Windows
    {
        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public int ConfigId { get; set; }
    }
}
