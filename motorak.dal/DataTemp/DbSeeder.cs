using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using motorak.dal.Entites;
using motorak.DAL.DataBase;
using Motorak.DAL.Enums.CarEnums;

namespace motorak.dal.DataTemp
{
    public class DbSeeder
    {
        public static async Task SeedAsync(MotorakDbContext context)
        {
            context.Database.EnsureCreated();
            if (!await context.Cars.AnyAsync())
            {
                var carsToSeed = new List<Car>
                {
                    new Car("Tesla", "Model S", 2023, "Red", 15000, CarType.Sedan, CarTransmission.Automatic, CarCondition.New, 75000m, "assets/img/featured1.png"),
                    new Car("Porsche", "911", 2022, "Yellow", 22000, CarType.Coupe, CarTransmission.Manual, CarCondition.Used, 115000m, "assets/img/featured2.png"),
                    new Car("Audi", "A8", 2023, "Black", 18000, CarType.Sedan, CarTransmission.Automatic, CarCondition.New, 95000m, "assets/img/featured3.png"),
                    new Car("BMW", "X5", 2021, "White", 45000, CarType.SUV, CarTransmission.Automatic, CarCondition.Used, 65000m, "assets/img/featured4.png"),
                    new Car("Mercedes", "G-Class", 2022, "Gray", 31000, CarType.SUV, CarTransmission.Automatic, CarCondition.Used, 135000m, "assets/img/featured5.png")
                };
                await context.Cars.AddRangeAsync(carsToSeed);
                await context.SaveChangesAsync();
            }
        }
    }
}
