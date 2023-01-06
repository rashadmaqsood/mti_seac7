using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace DatabaseConfiguration.SMTModels
{
    [Table(Name = "Event_Logs")]
    public class Event_Logs : CommonModels.Event_Logs
    {}
}
