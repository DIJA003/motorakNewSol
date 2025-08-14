

using motorak.dal.Entites;
using Motorak.DAL;
using Motorak.DAL.Entites;

namespace motorak.dal.Repo.Abstractions
{
    public interface ICartRepo
    {
        Task<List<CartItem>> GetAllAsync();
    }
}
