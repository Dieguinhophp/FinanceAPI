using FinanceAPI.Application.DTOs.Report;
using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Enums;
using FinanceAPI.Domain.Interfaces;

namespace FinanceAPI.Application.Services;

public class ReportService : IReportService
{
    private readonly ITransactionRepository _repository;

    public ReportService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<MonthlyReportDto> GetMonthlyReportAsync(int userId, int month, int year)
    {
        var transactions = await _repository.GetAllByUserIdAsync(userId);

        var filtered = transactions
            .Where(t => t.Date.Month == month && t.Date.Year == year)
            .ToList();

        var totalIncome = filtered
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        var totalExpense = filtered
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        var expensesByCategory = filtered
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.Category.Name)
            .Select(g => new CategorySummaryDto
            {
                CategoryName = g.Key,
                Total = g.Sum(t => t.Amount),
                Count = g.Count()
            })
            .OrderByDescending(c => c.Total)
            .ToList();

        return new MonthlyReportDto
        {
            Month = month,
            Year = year,
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            Balance = totalIncome - totalExpense,
            ExpensesByCategory = expensesByCategory
        };
    }
}