using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;


namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "LoadProfileChannels")]
    public class LoadProfileChannels : CommonModels.LoadProfileChannels
    {
        [Column(Name = "Label", DbType = "VARCHAR")]
        public string Label { get; set; }
    }
}
