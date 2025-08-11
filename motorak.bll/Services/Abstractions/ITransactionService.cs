

using Motorak.BLL.ModelVM.Transactions;

namespace Motorak.BLL.Services.Abstractions
{
    public interface ITransactionService
    {
        Task<List<TransactionReadDto>> GetAllAsync();
        Task<TransactionReadDto?> GetByIdAsync(int id);
        Task<int>  AddAsync(TransactionCreateDto dto);
        Task UpdateAsync(TransactionUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
