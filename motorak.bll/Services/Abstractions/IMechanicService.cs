using Motorak.BLL.ModelVM.Mechanic;
using Motorak.DAL.Enums.MechaincEnums;

namespace Motorak.BLL.Services.Abstractions
{
    public interface IMechanicService
    {
        Task<(bool status, string message, List<MechanicListModel>)> GetAllMechanicsAsync();
        Task<(bool status, string message, MechanicDetailsModel?)> GetMechanicByIdAsync(int id);
        Task<(bool status, string message)> CreateMechanicAsync(CreateMechanicModel model);
        Task<(bool status, string message)> EditMechanicAsync(EditMechanicModel model);
        Task<(bool status, string message)> DeleteMechanicAsync(int id);
    }
}
