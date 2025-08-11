using Motorak.DAL.Entities;
using Motorak.DAL.Enums.CarEnums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace motorak.dal.Entites
{
    public class Car
    {
        public int Id { get; private set; }
        public decimal Price { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        public int Mileage { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; } 
        public DateTime? DeletedAt { get; private set; }
        public CarType Type { get; private set; }
        public CarTransmission Transmission { get; private set; }
        public CarCondition Condition { get; private set; }
        public CarStatus Status { get; private set; }
        public string? ImagePath { get; private set; }

        //public virtual List<Service>? Services { get;private set; }



        public virtual List<Purchase>? Purchases { get; private set; } = new List<Purchase>();
        public virtual List<Rent>? Rents { get; private set; } = new List<Rent>();



        public int? CustomerId { get; private set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; private set; }


        public Car() { }

        public Car(
            string brand,
            string model,
            int year,
            string color,
            int mileage,
            CarType type,
            CarTransmission transmission,
            CarCondition condition,
            decimal price,
            string? imagePath
    )
        {
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Mileage = mileage;
            Type = type;
            Transmission = transmission;
            Condition = condition;
            Status = CarStatus.Available;
            ImagePath = imagePath;
            CreatedAt = DateTime.Now;
            IsDeleted = false;
            Price = price;
        }
        

        public void SellToCustomer(int customerId)
        {
            CustomerId = customerId;
            Status = CarStatus.Sold;
        }
        public void RentToCustomer(int customerId)
        {
            CustomerId = customerId;
            Status = CarStatus.Rented;
        }

        public void Edit(
           string brand,
           string model,
           int year,
           string color,
           int mileage,
           CarType type,
           CarTransmission transmission,
           CarCondition condition,
           CarStatus status,
           string? imagePath
       )
        {
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Mileage = mileage;
            Type = type;
            Transmission = transmission;
            Condition = condition;
            Status = status;
            ImagePath = imagePath;
        }

        public void UpdateMileage(int newMileage)
        {
            if (newMileage < Mileage) throw new InvalidOperationException("Mileage can not be decreased");
            Mileage = newMileage;
        }
        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
        public void Update()
        {
            UpdatedAt = DateTime.Now;
        }
        public void MarkAsSold()
        {
            Status = CarStatus.Sold;
        }

        public void MarkAsRented()
        {
            Status = CarStatus.Rented;
        }
    }
}
