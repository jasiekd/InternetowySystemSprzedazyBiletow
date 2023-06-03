namespace QuickTickets.Api.Dto
{
    public class PaginationDto
    {
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
