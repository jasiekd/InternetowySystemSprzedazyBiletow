using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Dto
{
    public class OrganiserInfoDto
    {
        public long Id { get; set; }
        public UserInfoDto User { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; } 
        public string Description { get; set; }
    }
}
