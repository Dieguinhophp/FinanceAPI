using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Application.DTOs.Transaction;

public class TransactionResponseDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}