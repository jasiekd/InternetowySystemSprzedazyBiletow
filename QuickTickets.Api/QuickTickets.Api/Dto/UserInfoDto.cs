using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Dto
{
    public class UserInfoDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
