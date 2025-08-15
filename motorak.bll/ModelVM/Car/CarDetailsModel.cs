using motorak.dal.Enums.CarEnums;
using Motorak.DAL.Enums.CarEnums;

namespace Motorak.BLL.ModelVM.Car
{
    public class CarDetailsModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public CarType Type { get; set; }
        public CarTransmission Transmission { get; set; }
        public CarCondition Condition { get; set; }
        public CarStatus Status { get; set; }
        public CarCategory Category { get; set; }       // ForSale / ForRent
        public decimal Price { get; set; }             // Purchase price
        public decimal? DailyRentPrice { get; set; }   // Only for rental cars
        public string? ImagePath { get; set; }
        public DateTime DateAdded { get; set; }        // CreatedAt from entity
        public string? OwnerName { get; set; }         // Optional, if linked to customer
    }
}
