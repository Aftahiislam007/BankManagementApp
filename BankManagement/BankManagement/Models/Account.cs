using BankManagement.Utils;

namespace BankManagement.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber 
        {
            get => EncryptionHelper.Decrypt(AccountNumberEncrypted);
            set => AccountNumberEncrypted = EncryptionHelper.Encrypt(value);
        }
        public string AccountNumberEncrypted { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "USD";
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
