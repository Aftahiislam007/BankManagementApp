using BankManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("monthly-statement/{accountId}")]
        public async Task<IActionResult> GetMonthlyStatement(int accountId, [FromQuery] int month, [FromQuery] int year)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null) return NotFound("Account not found.");

            var transactions = await _context.Transactions
                .Where(t => t.AccountId == accountId &&
                            t.Date.Month == month &&
                            t.Date.Year == year)
                .OrderBy(t => t.Date)
                .ToListAsync();

            var statement = new
            {
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                Balance = account.Balance,
                Transactions = transactions
            };

            return Ok(statement);
        }

        [HttpGet("summary-report")]
        public async Task<IActionResult> GetSummaryReport()
        {
            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .ToListAsync();

            var loans = await _context.Loans
                .Include(l => l.Customer)
                .ToListAsync();

            var report = new
            {
                TotalAccounts = accounts.Count,
                TotalBalance = accounts.Sum(a => a.Balance),
                TotalLoans = loans.Count,
                ApprovedLoans = loans.Count(l => l.IsApproved),
                PendingLoans = loans.Count(l => !l.IsApproved),
            };

            return Ok(report);
        }
    }
}
