using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "Device_Association")]
    public class Device_Association : CommonModels.Device_Association
    {
        [Column(Name = "Association_Index", DbType = "BYTE")]
        public byte Association_Index { get; set; }
    }
}
