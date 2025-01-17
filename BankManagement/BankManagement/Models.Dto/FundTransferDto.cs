namespace BankManagement.Models.Dto
{
    public class FundTransferDto
    {
        public int SourceAccountId { get; set; }
        public int TargetAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
