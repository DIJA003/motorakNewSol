
using motorak.dal.Entites;
using Motorak.DAL.Entites;

namespace Motorak.DAl.Repo.Abstractions
{
    public interface ICarRebo
    {
        Task<List<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task CreateAsync(Car car);
        void Update(Car car);
        void Delete(Car car);
        Task<int> SaveChangesAsync();
    }
}
