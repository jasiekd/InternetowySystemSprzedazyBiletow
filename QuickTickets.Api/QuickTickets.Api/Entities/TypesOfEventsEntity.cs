using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class TypesOfEventsEntity
    {
        [Key]
        public long TypeID { get; set; }
        public string Description { get; set; }
    }
}
