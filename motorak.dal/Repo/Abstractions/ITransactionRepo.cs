using motorak.dal.Entities;
using Motorak.DAL.Entities;


namespace Motorak.DAL.Repo.Abstractions
{
    public interface ITransactionRepo
    {
        Task<Transactions?> GetByIdAsync(int id);
        Task<IEnumerable<Transactions>> GetAllAsync();
        Task AddAsync(Transactions transaction);
        Task UpdateAsync(Transactions transaction);
        Task DeleteAsync(int id);
    }
}
