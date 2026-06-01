using FinanceAPI.Application.DTOs.Transaction;
using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Entities;
using FinanceAPI.Domain.Interfaces;

namespace FinanceAPI.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync(int userId)
    {
        var transactions = await _repository.GetAllByUserIdAsync(userId);
        return transactions.Select(t => new TransactionResponseDto
        {
            Id = t.Id,
            Description = t.Description,
            Amount = t.Amount,
            Type = t.Type,
            Date = t.Date,
            CategoryName = t.Category.Name
        });
    }

    public async Task<TransactionResponseDto?> GetByIdAsync(int id)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return null;

        return new TransactionResponseDto
        {
            Id = t.Id,
            Description = t.Description,
            Amount = t.Amount,
            Type = t.Type,
            Date = t.Date,
            CategoryName = t.Category.Name
        };
    }

    public async Task<IEnumerable<TransactionResponseDto>> CreateAsync(int userId, CreateTransactionDto dto)
    {
        var installments = dto.Installments < 1 ? 1 : dto.Installments;
        var results = new List<TransactionResponseDto>();

        for (int i = 0; i < installments; i++)
        {
            var description = installments > 1
                ? $"{dto.Description} ({i + 1}/{installments})"
                : dto.Description;

            var transaction = new Transaction
            {
                Description = description,
                Amount = dto.Amount,
                Type = dto.Type,
                Date = dto.Date.AddMonths(i), // ← incrementa o mês a cada parcela
                CategoryId = dto.CategoryId,
                UserId = userId
            };

            var created = await _repository.CreateAsync(transaction);

            results.Add(new TransactionResponseDto
            {
                Id = created.Id,
                Description = created.Description,
                Amount = created.Amount,
                Type = created.Type,
                Date = created.Date,
                CategoryName = created.Category?.Name ?? string.Empty
            });
        }

        return results;
    }

    public async Task<TransactionResponseDto> UpdateAsync(int id, CreateTransactionDto dto)
    {
        var transaction = await _repository.GetByIdAsync(id);
        if (transaction == null) throw new Exception("Transação não encontrada.");

        transaction.Description = dto.Description;
        transaction.Amount = dto.Amount;
        transaction.Type = dto.Type;
        transaction.Date = dto.Date;
        transaction.CategoryId = dto.CategoryId;

        var updated = await _repository.UpdateAsync(transaction);

        return new TransactionResponseDto
        {
            Id = updated.Id,
            Description = updated.Description,
            Amount = updated.Amount,
            Type = updated.Type,
            Date = updated.Date,
            CategoryName = updated.Category?.Name ?? string.Empty
        };
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}