using System.ComponentModel.DataAnnotations;

namespace QuickTickets.Api.Entities
{
    public class EventsEntity
    {
        [Key]
        public long EventID { get; set; }
        public string Title { get; set; }
        public int Seats { get; set; }
        public float TicketPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public string Status { get; set; } = StatusEnum.Pending.ToString();
        public bool IsActive { get; set; }
        public bool AdultsOnly { get; set; }
        public long TypeID { get; set; }
        public virtual TypesOfEventsEntity? Type { get; set; }
        public long LocationID { get; set; }
        public virtual LocationsEntity? Location { get; set; }
        public string ImgURL { get; set; }
        public Guid OwnerID { get; set; }
        public virtual AccountEntity? Owner { get; set; }
    }
}
