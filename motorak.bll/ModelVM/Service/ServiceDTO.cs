
using Motorak.DAL.Enums.SeviceEnums;

namespace Motorak.BLL.ModelVM.Service
{
    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public int MechanicId { get; set; }
        public Status Status { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
