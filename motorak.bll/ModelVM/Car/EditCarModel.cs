
using Microsoft.AspNetCore.Http;
using Motorak.DAL.Enums.CarEnums;
using System.ComponentModel.DataAnnotations;

namespace Motorak.BLL.ModelVM.Car
{
    public class EditCarModel
    {
        [Required]
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required,MaxLength(50)]
        public string Model { get; set; }

        [Required,Range(1900, 2100)]
        public int Year { get; set; }

        [Required,MaxLength(30)]
        public string Color { get; set; }

        [Required,Range(0, int.MaxValue)]
        public int Mileage { get; set; }

        [Required,Display(Name = "Car Type")]
        public CarType Type { get; set; }

        [Required]
        public CarTransmission Transmission { get; set; }

        [Required]
        public CarCondition Condition { get; set; }

        [Required]
        public CarStatus Status { get; set; }

        public string? CurrentImagePath { get; set; }

        [Display(Name = "New Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
