
using Microsoft.AspNetCore.Http;
using motorak.dal.Enums.CarEnums;
using Motorak.DAL.Enums.CarEnums;
using System.ComponentModel.DataAnnotations;

namespace Motorak.BLL.ModelVM.Car
{
    public class EditCarModel
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Brand { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Model { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Required, MaxLength(30)]
        public string Color { get; set; } = null!;

        [Required]
        public int Mileage { get; set; }

        [Required]
        public CarType Type { get; set; }

        [Required]
        public CarTransmission Transmission { get; set; }

        [Required]
        public CarCondition Condition { get; set; }

        [Required]
        public CarStatus Status { get; set; }

        [Required]
        public CarCategory Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? DailyRentPrice { get; set; }

        public string? ImagePath { get; set; }
    }
}
