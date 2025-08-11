

using Motorak.DAL.Enums.SeviceEnums;

namespace Motorak.BLL.ModelVM.Service
{
    public class CreateServiceVM
    {
        public DateTime RequestDate { get; set; }
        public ServiceType ServiceType { get; set; }
        public Status Status { get; set; }
        public int CustomerId { get; set; }
        public int MechanicId { get; set; }
        public int CarId { get; set; }
    }
}
