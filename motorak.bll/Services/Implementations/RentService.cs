using AutoMapper;
using Motorak.BLL.ModelVM.Rents;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAL.Entities;
using Motorak.DAL.Repo.Abstractions;

namespace Motorak.BLL.Services.Implementations
{
    public class RentService : IRentService
    {
        private readonly IRentRepo _repo;
        private readonly IMapper _mapper;

        public RentService(IRentRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<RentReadDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(r => _mapper.Map<RentReadDto>(r)).ToList();
        }

        public async Task<RentReadDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<RentReadDto>(entity);
        }

        public async Task<int> CreateAsync(RentCreateDto dto)
        {
            var rent = new Rent(dto.StartDate, dto.EndDate, dto.PaymentMethod, dto.TotalPrice, dto.CustomerId, dto.CarId);
            await _repo.AddAsync(rent);
            return rent.Id;
        }

        public async Task UpdateAsync(RentUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new InvalidOperationException("Rent not found");

            existing.UpdateTransaction(dto.PaymentMethod, dto.TotalPrice, dto.Status);
            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
