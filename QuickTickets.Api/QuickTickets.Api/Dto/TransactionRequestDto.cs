namespace QuickTickets.Api.Dto
{
    public class TransactionRequestDto
    {
        public double Cost { get; set; }
        public string Desc { get; set; }
        public long EventID { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
