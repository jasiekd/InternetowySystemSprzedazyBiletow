using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class RoleEntity
    {
        [Key]
        public int RoleID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
