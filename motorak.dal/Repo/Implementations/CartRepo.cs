

using Microsoft.EntityFrameworkCore;
using motorak.dal.Entites;
using motorak.dal.Repo.Abstractions;
using motorak.DAL.DataBase;
using Motorak.DAL;
using Motorak.DAL.Entites;


namespace motorak.dal.Repo.Implementations
{
    public class CartRepo:ICartRepo
    {
        private readonly MotorakDbContext _context;
        public CartRepo(MotorakDbContext context)
        {
            _context = context;
        }
        public async Task<List<CartItem>> GetAllAsync()
        {
            return await _context.CartItems.ToListAsync();
        }
    }
}
