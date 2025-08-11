
namespace Motorak.BLL.ModelVM.Service
{
    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public DateTime RequestDate { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public int MechanicId { get; set; }
        public int CarId { get; set; }

    }
}
