using Motorak.DAL.Entites;
using motorak.dal.Entites;
using Motorak.DAL.Enums.CarEnums;

namespace Motorak.BLL.ModelVM.Car
{
    public class CarDetailsModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public CarType Type { get; set; }
        public CarTransmission Transmission { get; set; }
        public CarCondition Condition { get; set; }
        public CarStatus Status { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DateAdded { get; set; }

        public string? OwnerName { get; set; }
    }
}
