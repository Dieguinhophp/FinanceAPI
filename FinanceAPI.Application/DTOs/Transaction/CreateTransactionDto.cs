using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Application.DTOs.Transaction;

public class CreateTransactionDto
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
}