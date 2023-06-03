using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{

    public class OrganiserApplicationEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public DateTime DateModified { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = StatusEnum.Pending.ToString();

        public string Description { get; set; }
    }
}
