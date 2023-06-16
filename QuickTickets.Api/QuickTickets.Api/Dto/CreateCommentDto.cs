namespace QuickTickets.Api.Dto
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public long EventID { get; set; }
    }
}
