namespace QuickTickets.Api.Dto
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        public int Seats { get; set; }
        public float TicketPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set;}
        public bool IsActive { get; set; }
        public bool AdultsOnly { get; set; }
        public long TypeID { get; set; }
        public long LocationID { get; set; }
        public string ImgURL { get; set; }
    }
}
