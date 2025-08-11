using AutoMapper;
using Motorak.BLL.ModelVM.Purchases;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAL.Entities;
using Motorak.DAL.Repo.Abstractions;

namespace Motorak.BLL.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepo _repo;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PurchaseReadDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(p => _mapper.Map<PurchaseReadDto>(p)).ToList();
        }

        public async Task<PurchaseReadDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PurchaseReadDto>(entity);
        }

        public async Task<int> CreateAsync(PurchaseCreateDto dto)
        {
            var purchase = new Purchase(dto.SellerName, dto.PaymentMethod, dto.TotalPrice, dto.CustomerId, dto.CarId);
            await _repo.AddAsync(purchase);
            return purchase.Id;
        }

        public async Task UpdateAsync(PurchaseUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new InvalidOperationException("Purchase not found");

            existing.UpdateTransaction(dto.PaymentMethod, dto.TotalPrice, dto.Status);
            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}