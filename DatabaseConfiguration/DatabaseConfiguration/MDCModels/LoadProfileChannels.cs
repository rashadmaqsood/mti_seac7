using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "load_profile_channels")]
    public class LoadProfileChannels : CommonModels.LoadProfileChannels
    {
        [Column(Name = "ConfigId", DbType = "BIGINT")]
        public int ConfigId { get; set; }
    }
}
