
using Motorak.DAL.Enums.CarEnums;

namespace Motorak.BLL.ModelVM.Car
{
    public class CarListModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public CarStatus Status { get; set; }
        public string? ImagePath { get; set; }
    }
}
