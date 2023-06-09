namespace QuickTickets.Api.Entities
{
    public class TransactionEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string Status { get; set; } = StatusEnum.Pending.ToString();
    }
}
