namespace BankManagement.Models.Dto
{
    public class RepaymentScheduleDto
    {
        public int Month { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime DueDate { get; set; }
    }
}
