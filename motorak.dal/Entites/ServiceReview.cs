
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using motorak.dal.Entites;

namespace Motorak.DAL.Entites
{
    public class ServiceReview
    {
        public int Id { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }

        public int CustomerId { get; private set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; private set; }

        public int ServiceId { get; private set; }
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public ServiceReview(int rating, string comment, int customerId, int serviceId)
        {
            Rating = rating;
            Comment = comment;
            CustomerId = customerId;
            ServiceId = serviceId;
            CreatedAt = DateTime.Now;
            IsDeleted = false;
        }

        public ServiceReview()
        {
        }

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
        public void UpdateRating(int newRating)
        {
            Rating = newRating;
            UpdatedAt = DateTime.Now;
        }
        public void UpdateComment(string newComment)
        {
            Comment = newComment;
            UpdatedAt = DateTime.Now;
        }
    }
}
