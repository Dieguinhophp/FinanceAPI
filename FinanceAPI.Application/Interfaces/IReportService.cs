using FinanceAPI.Application.DTOs.Report;

namespace FinanceAPI.Application.Interfaces;

public interface IReportService
{
    Task<MonthlyReportDto> GetMonthlyReportAsync(int userId, int month, int year);
}