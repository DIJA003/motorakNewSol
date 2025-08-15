using Motorak.DAL.Entities;
using Motorak.DAL.Entites;

namespace motorak.dal.Entites
{
    public class Customer
    {
        public int Id { get; set; }
        public virtual List<Purchase>? Purchases { get; set; } = new List<Purchase>();
        public virtual List<Rent>? Rents { get; set; } = new List<Rent>();
        public virtual List<Service>? Services { get; set; } = new List<Service>();
        public virtual List<ServiceReview>? ServiceReviews { get; set; } = new List<ServiceReview>();
        public virtual List<Car>? Cars { get; set; } = new List<Car>();

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsUpdated { get; set; } = false;

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }

        public void Update()
        {
            IsUpdated = true;
            UpdatedAt = DateTime.Now;
        }
    }
}