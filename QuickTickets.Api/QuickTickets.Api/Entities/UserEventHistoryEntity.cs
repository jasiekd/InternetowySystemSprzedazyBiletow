using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class UserEventHistoryEntity
    {
        [Key]
        public long UserEventHistoryID { get; set; }
        public long UserID { get; set; }
        public long EventID { get; set; }
        public float Label { get; set; }
    }
}
