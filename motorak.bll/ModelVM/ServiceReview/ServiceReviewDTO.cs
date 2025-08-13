

namespace Motorak.BLL.ModelVM.ServiceReview
{
    public class ServiceReviewDTO
    {
        public int ReviewId { get; set; }
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
