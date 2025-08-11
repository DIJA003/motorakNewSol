

using Motorak.DAL.Enums.SeviceEnums;

namespace Motorak.BLL.ModelVM.Service
{
    public class UpdateServiceVM
    {
        public int ServiceId { get; set; }
        public DateTime RequestDate { get; set; }
        public Status Status { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
