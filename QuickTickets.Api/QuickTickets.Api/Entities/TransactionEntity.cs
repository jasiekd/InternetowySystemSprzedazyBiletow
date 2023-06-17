using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class TransactionEntity
    {
        [Key]
        public Guid TransactionID { get; set; }
        public Guid UserId { get; set; }
        public virtual AccountEntity? User { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public DateTime DateDeadline { get; set; } = DateTime.Now.AddMinutes(3);
        public string Status { get; set; } = StatusEnum.Pending.ToString();
        public double Price { get; set; }
        public string? DotPayID { get; set; }
    }
}
