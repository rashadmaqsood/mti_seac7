using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.CommonModels
{
     [Table(Name = "device_association")]
    public class device_association
    {
        [Column(IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int id { get; set; }

        [Column(Name = "Association_Name", DbType = "VARCHAR")]
        public string Association_Name { get; set; }

        [Column(Name = "Authentication_Type_Id", DbType = "INTEGER")]
        public int Authentication_Type_Id { get; set; }

        [Column(Name = "Client_Sap", DbType = "INTEGER")]
        public int Client_Sap { get; set; }

        [Column(Name = "Meter_Sap", DbType = "INTEGER")]
        public int Meter_Sap { get; set; }

        [Column(Name = "Device_Id", DbType = "INTEGER")]
        public int Device_Id { get; set; }

        [Column(Name = "Configuration_Id", DbType = "INTEGER")]
        public int Configuration_Id { get; set; }

        [Column(Name = "ObisRightGroupId", DbType = "INTEGER")]
        public int ObisRightGroupId { get; set; }

        [Column(Name = "Reload_Config", DbType = "TINTINT")]
        public byte? Reload_Config { get; set; }

        [Column(Name = "Association_Index", DbType = "TINTINT")]
        public byte Association_Index { get; set; }
    }
}
