

using Motorak.DAL.Entities;
using Motorak.DAL.Entites;

namespace motorak.dal.Entites
{
    public class Customer
    {
        public int Id { get; private set; }
        public virtual List<Purchase>? Purchases { get; private set; } = new List<Purchase>();
        public virtual List<Rent>? Rents { get; private set; } = new List<Rent>();

        public virtual List<Service>? Services { get; private set; }=new List<Service>();
        public virtual List<ServiceReview>? ServiceReviews { get; private set; }= new List<ServiceReview>();
        public virtual List<Car>? Cars { get; private set; }= new List<Car>();

        public string UserId { get;  set; }
        public virtual User User { get;  set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsUpdated { get; private set; }                                                                                                  

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
    }
}
