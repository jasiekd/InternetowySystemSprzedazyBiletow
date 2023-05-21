﻿using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Dto
{
    public class EventInfoDto
    {
        public long EventID { get; set; }
        public string Title { get; set; }
        public int Seats { get; set; }
        public int OccupiedSeats { get; set; }
        public float TicketPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public bool AdultsOnly { get; set; }
        public TypesOfEventsEntity Type { get; set; }
        public LocationsEntity Location { get; set; }
        public string ImgURL { get; set; }
        public Guid OwnerID { get; set; }
    }
}