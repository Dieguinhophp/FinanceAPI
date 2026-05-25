using FinanceAPI.Application.DTOs.Transaction;

namespace FinanceAPI.Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponseDto>> GetAllAsync(int userId);
    Task<TransactionResponseDto?> GetByIdAsync(int id);
    Task<TransactionResponseDto> CreateAsync(int userId, CreateTransactionDto dto);
    Task<TransactionResponseDto> UpdateAsync(int id, CreateTransactionDto dto);
    Task DeleteAsync(int id);
}