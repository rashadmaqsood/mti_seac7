using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.MDCModels
{
    [Table(Name = "bill_tariff_quantity")]
    public class BillTariffQuantity//: CommonModels.BillTariffQuantity
    {
        [Column(Name = "DatabaseField", DbType = "VARCHAR")]
        public string DatabaseField { get; set; }
    }
}
