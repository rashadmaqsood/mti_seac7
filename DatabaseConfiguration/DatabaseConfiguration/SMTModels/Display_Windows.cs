using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "DisplayWindows")]
    public class Display_Windows : CommonModels.Display_Windows
    {
        [Column(Name = "Label", DbType = "VARCHAR")]
        public string Label { get; set; }
    }
}
