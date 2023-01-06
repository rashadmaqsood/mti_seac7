using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "configuration_new")]
    public class configuration_new
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Name", DbType = "VARCHAR")]
        public string Name { get; set; }

        [Column(Name = "lp_channels_group_id", DbType = "INTEGER")]
        public int LP_Channels_Group_ID { get; set; }

        [Column(Name = "BillItemsGroupId", DbType = "INTEGER")]
        public int BillItemsGroupId { get; set; }

        [Column(Name = "DisplayWindowGroupId", DbType = "INTEGER")]
        public int? DisplayWindowGroupId { get; set; }

        [Column(Name = "EventGroupId", DbType = "INTEGER")]
        public int EventGroupId { get; set; }
    }
}
