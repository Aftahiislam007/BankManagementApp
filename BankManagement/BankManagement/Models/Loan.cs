namespace BankManagement.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string LoanType { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInMonths { get; set; }
        public bool IsApproved { get; set; }
    }
}
