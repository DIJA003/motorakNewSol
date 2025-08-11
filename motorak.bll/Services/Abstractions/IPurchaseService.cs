using Motorak.BLL.ModelVM.Purchases;

namespace Motorak.BLL.Services.Abstractions
{
    public interface IPurchaseService
    {
        Task<List<PurchaseReadDto>> GetAllAsync();
        Task<PurchaseReadDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(PurchaseCreateDto dto);
        Task UpdateAsync(PurchaseUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
