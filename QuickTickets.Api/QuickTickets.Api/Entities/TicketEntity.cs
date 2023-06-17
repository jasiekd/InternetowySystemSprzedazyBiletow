using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class TicketEntity 
    {
        [Key]
        public long TicketID { get; set; }
        public bool IsActive { get; set; } = false;
        public long EventID { get; set; }
        public virtual EventsEntity? Event { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int Amount { get; set; }
        public Guid TransactionID { get; set; }
        public virtual TransactionEntity? Transaction { get; set; }
    }
}
