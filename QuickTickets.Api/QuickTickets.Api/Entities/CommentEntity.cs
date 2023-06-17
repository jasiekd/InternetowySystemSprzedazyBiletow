using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class CommentEntity
    {
        [Key]
        public long CommentID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public Guid UserID { get; set; }
        public AccountEntity? User { get; set; }
        public long EventID { get; set; }
    }
}
