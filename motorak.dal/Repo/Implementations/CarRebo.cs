
using Microsoft.EntityFrameworkCore;
using motorak.dal.Entites;
using motorak.DAL.DataBase;
using Motorak.DAl.Repo.Abstractions;

using Motorak.DAL.Entites;

namespace Motorak.DAl.Repo.Implementations
{
    public class CarRebo : ICarRebo
    {
        private readonly MotorakDbContext MDB;

        public CarRebo(MotorakDbContext MDB)
        {
            this.MDB = MDB;
        }

        public async Task CreateAsync(Car car)
        {
            await MDB.Cars.AddAsync(car);
        }

        public void Delete(Car car)
        {
            car.Delete();
            MDB.Cars.Update(car);
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await MDB.Cars.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await MDB.Cars.Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await MDB.SaveChangesAsync();   
        }

        public void Update(Car car)
        {
            MDB.Cars.Update(car);
        }
    }
}
