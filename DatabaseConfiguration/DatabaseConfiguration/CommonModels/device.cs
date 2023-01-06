using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
    [Table(Name = "device")]
    public class device
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Device_Name", DbType = "VARCHAR")]
        public string Device_Name { get; set; }

        [Column(Name = "Model", DbType = "VARCHAR")]
        public string Model { get; set; }

        [Column(Name = "Manufacturer_Id", DbType = "INTEGER")]
        public int Manufacturer_Id { get; set; }

        [Column(Name = "IsSinglePhase", DbType = "BIT")]
        public bool IsSinglePhase { get; set; }

        [Column(Name = "Product", DbType = "VARCHAR")]
        public string Product { get; set; }

        [Column(Name = "Accuracy", DbType = "VARCHAR")]
        public string Accuracy { get; set; }
    }
}
