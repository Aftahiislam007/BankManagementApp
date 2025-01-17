using BankManagement.Models;
using BankManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankManagement.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] Transaction transaction)
        {
            var account = await _context.Accounts.FindAsync(transaction.AccountId);
            if (account == null) return NotFound("Account not found.");

            account.Balance += transaction.Amount;
            transaction.TransactionType = "Deposit";
            transaction.Date = DateTime.UtcNow;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return Ok(transaction);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] Transaction transaction)
        {
            var account = await _context.Accounts.FindAsync(transaction.AccountId);
            if (account == null) return NotFound("Account not found.");
            if (account.Balance < transaction.Amount) return BadRequest("Insufficient funds.");

            account.Balance -= transaction.Amount;
            transaction.TransactionType = "Withdrawal";
            transaction.Date = DateTime.UtcNow;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return Ok(transaction);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] FundTransferDto transfer)
        {
            var sourceAccount = await _context.Accounts.FindAsync(transfer.SourceAccountId);
            var targetAccount = await _context.Accounts.FindAsync(transfer.TargetAccountId);

            if (sourceAccount == null || targetAccount == null)
                return NotFound("One or both accounts not found.");
            if (sourceAccount.Balance < transfer.Amount)
                return BadRequest("Insufficient funds in the source account.");

            sourceAccount.Balance -= transfer.Amount;
            targetAccount.Balance += transfer.Amount;

            var sourceTransaction = new Transaction
            {
                AccountId = sourceAccount.Id,
                Amount = transfer.Amount,
                TransactionType = "Transfer Out",
                Date = DateTime.UtcNow,
                Description = transfer.Description
            };

            var targetTransaction = new Transaction
            {
                AccountId = targetAccount.Id,
                Amount = transfer.Amount,
                TransactionType = "Transfer In",
                Date = DateTime.UtcNow,
                Description = transfer.Description
            };

            _context.Transactions.AddRange(sourceTransaction, targetTransaction);
            await _context.SaveChangesAsync();
            return Ok(new { sourceTransaction, targetTransaction });
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetTransactionHistory(int accountId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            return Ok(transactions);
        }
    }
}
