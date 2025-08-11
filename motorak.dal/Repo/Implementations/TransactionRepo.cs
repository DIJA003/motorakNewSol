using Microsoft.EntityFrameworkCore;
using motorak.dal.Entities;
using motorak.DAL.DataBase;
using Motorak.DAL.Entities;
using Motorak.DAL.Repo.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Motorak.DAL.Repo.Implementations
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly MotorakDbContext _context;

        public TransactionRepo(MotorakDbContext context)
        {
            _context = context;
        }

        public async Task<Transactions?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(a=>a.Id == id && a.IsDeleted == false);
        }

        public async Task<IEnumerable<Transactions>> GetAllAsync()
        {
            return await _context.Transactions.Where(a=>a.IsDeleted == false).ToListAsync();
        }

        public async Task AddAsync(Transactions transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transactions transaction)
        {
            var trans = await _context.Transactions.FirstOrDefaultAsync(a => a.Id == transaction.Id && a.IsDeleted == false);
            if(trans!=null)
            {
                trans.UpdateTransaction(
                    newPaymentMethod: transaction.PaymentMethod,
                    newPrice: transaction.TotalPrice,
                    newStatus: transaction.Status,
                    updatedBy: transaction.UpdatedBy ?? "System"
                );
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("I didn't find the element that you want to Update");

            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Transactions.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false); ;
            if (entity != null)
            {
                entity.Delete();
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("I didn't find the element that you want to delete");
            }
        }
    }
}
