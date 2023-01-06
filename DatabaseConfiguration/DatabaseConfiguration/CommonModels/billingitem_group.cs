using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "billingitem_group")]
    public class billingitem_group
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "BillingItem_Group_Name", DbType = "VARCHAR")]
        public string BillingItem_Group_Name { get; set; }
    }
}
