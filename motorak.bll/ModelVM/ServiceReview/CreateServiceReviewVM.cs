
namespace Motorak.BLL.ModelVM.ServiceReview
{
    public class CreateServiceReviewVM
    {
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
