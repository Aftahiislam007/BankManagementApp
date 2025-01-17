using BankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BankManagement.Services
{
    public class InterestCalculationJob : IJob
    {
        private readonly ApplicationDbContext _context;

        public InterestCalculationJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var savingsAccounts = await _context.Accounts
                .Where(a => a.AccountType == "Savings")
                .ToListAsync();

            foreach (var account in savingsAccounts)
            {
                var monthlyInterestRate = 0.03m / 12;
                var interest = account.Balance * monthlyInterestRate;
                account.Balance += interest;

                var interestTransaction = new Transaction
                {
                    AccountId = account.Id,
                    Amount = interest,
                    TransactionType = "Interest Credit",
                    Date = DateTime.UtcNow,
                    Description = "Monthly interest credit"
                };

                _context.Transactions.Add(interestTransaction);
            }

            await _context.SaveChangesAsync();
        }
    }
}
