namespace FinanceAPI.Application.DTOs.Report;

public class MonthlyReportDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance { get; set; }
    public List<CategorySummaryDto> ExpensesByCategory { get; set; } = new();
}

public class CategorySummaryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public int Count { get; set; }
}