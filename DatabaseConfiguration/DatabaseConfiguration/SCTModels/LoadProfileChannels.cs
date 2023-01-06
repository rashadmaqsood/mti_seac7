using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SCTModels
{
    [Table(Name = "Load_Profile_Channels")]
    public class LoadProfileChannels : CommonModels.LoadProfileChannels
    {
        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public int ConfigId { get; set; }
    }
}
