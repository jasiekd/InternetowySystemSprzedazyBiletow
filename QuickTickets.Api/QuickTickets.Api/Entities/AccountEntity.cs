using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTickets.Api.Entities
{
    public class AccountEntity: BaseDbItem
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string? Login { get; set; }

        [StringLength(50)]
        public string? Password { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int RoleID { get; set; }

        public string? GoogleSubject { get; set; }
        [Required]
        public string ModelID { get; set; }
    }
}
