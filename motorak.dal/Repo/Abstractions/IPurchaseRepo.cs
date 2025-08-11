using Motorak.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorak.DAL.Repo.Abstractions
{
    public interface IPurchaseRepo
    {
        Task<Purchase?> GetByIdAsync(int id);
        Task<List<Purchase>> GetAllAsync();
        Task AddAsync(Purchase purchase);
        Task UpdateAsync(Purchase purchase);
        Task DeleteAsync(int id);
    }
}
