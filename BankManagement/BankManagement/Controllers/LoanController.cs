using BankManagement.Models;
using BankManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForLoan([FromBody] Loan loan)
        {
            loan.IsApproved = false;
            loan.StartDate = DateTime.UtcNow;

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return Ok(loan);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingLoans()
        {
            var pendingLoans = await _context.Loans
                .Where(l => !l.IsApproved)
                .Include(l => l.Customer)
                .ToListAsync();

            return Ok(pendingLoans);
        }

        [HttpPost("approve/{loanId}")]
        public async Task<IActionResult> ApproveLoan(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null) return NotFound("Loan not found.");

            loan.IsApproved = true;
            await _context.SaveChangesAsync();

            return Ok("Loan approved successfully.");
        }

        [HttpGet("repayment-schedule/{loanId}")]
        public async Task<IActionResult> GetRepaymentSchedule(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null || !loan.IsApproved)
                return NotFound("Loan not found or not approved.");

            var schedule = new List<RepaymentScheduleDto>();
            var monthlyPayment = (loan.Amount * (1 + (loan.InterestRate / 100))) / loan.DurationInMonths;

            for (var i = 1; i <= loan.DurationInMonths; i++)
            {
                schedule.Add(new RepaymentScheduleDto
                {
                    Month = i,
                    AmountDue = monthlyPayment,
                    DueDate = loan.StartDate.AddMonths(i)
                });
            }

            return Ok(schedule);
        }
    }
}
