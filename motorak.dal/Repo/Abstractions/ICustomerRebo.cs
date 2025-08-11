
using motorak.dal.Entites;
using Motorak.DAL.Entites;

namespace Motorak.DAl.Repo.Abstractions
{
    public interface ICustomerRebo
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByUserIdAsync(string id);
        Task<Customer?> GetByUsernameAsync(string username);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer?> GetByPhoneAsync(string phone);
        Task CreateAsync(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<int> SaveChangesAsync();
    }
}
