namespace QuickTickets.Api.Dto
{
    public class SearchEventDto
    {
        public string? searchPhrase { get; set; }
        public float? minPrice { get; set; }
        public float? maxPrice { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public long? locationId { get; set; }
        public long? typeId { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
