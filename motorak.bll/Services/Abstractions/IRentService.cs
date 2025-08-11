

using Motorak.BLL.ModelVM.Rents;

namespace Motorak.BLL.Services.Abstractions
{
    public interface IRentService
    {
        Task<List<RentReadDto>> GetAllAsync();
        Task<RentReadDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(RentCreateDto dto);
        Task UpdateAsync(RentUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
