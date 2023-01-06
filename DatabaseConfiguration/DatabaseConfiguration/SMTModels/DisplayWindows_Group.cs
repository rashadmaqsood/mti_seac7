using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "DisplayWindows_Group")]
    public class DisplayWindows_Group:CommonModels.DisplayWindows_Group
    {
        [Column(Name = "Dw-Group_Name", DbType = "VARCHAR")]
        public new string Dw_Group_Name { get; set; }
    }
}
