using AutoMapper;
using motorak.dal.Entites;
using Motorak.BLL.ModelVM.Customer;
using Motorak.BLL.ModelVM.Mechanic;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAl.Repo.Abstractions;
using Motorak.DAl.Repo.Implementations;
using Motorak.DAL.Entites;
using Motorak.DAL.Enums.MechaincEnums;
using Motorak.DAL.Repo.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorak.BLL.Services.Implementations
{
    public class MechanicService : IMechanicService
    {
        private readonly IMechanicRebo _mechanicRepo;
        private readonly IMapper _mapper;

        public MechanicService(IMechanicRebo mechanicRepo, IMapper mapper)
        {
            _mechanicRepo = mechanicRepo;
            _mapper = mapper;
        }

        public async Task<(bool status, string message, List<MechanicListModel>)> GetAllMechanicsAsync()
        {
            try
            {
                var result = await _mechanicRepo.GetAllAsync();
                var resultList = _mapper.Map<List<MechanicListModel>>(result);
                return (true, "Mechanics retrieved successfully!", resultList);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to retrieve mechanics: {ex.Message}", new List<MechanicListModel>());
            }
        }

        public async Task<(bool status, string message, MechanicDetailsModel?)> GetMechanicByIdAsync(int id)
        {
            try
            {
                var mechanic = await _mechanicRepo.GetByIdAsync(id);
                if (mechanic == null) return (false, "Mechanic Not Found!!", null);

                var result = _mapper.Map<MechanicDetailsModel>(mechanic);
                return (true, "Mechanic retrieved successfully!", result);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to retrieve mechanic: {ex.Message}", null);
            }
        }

        public async Task<(bool status, string message)> CreateMechanicAsync(CreateMechanicModel mechanicModel)
        {
            try
            {
                if (mechanicModel == null)
                    return (false, "Mechanic model is null");

                var mechanic = _mapper.Map<Mechanic>(mechanicModel);
                var user = _mapper.Map<User>(mechanicModel);

                mechanic.UserId = user.Id;
                mechanic.User = user;

                await _mechanicRepo.AddAsync(mechanic);
                await _mechanicRepo.SaveChangesAsync();
                return (true, "Mechanic Created Successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Error Occurred: {ex.Message}");
            }
        }

        public async Task<(bool status, string message)> EditMechanicAsync(EditMechanicModel mechanicModel)
        {
            try
            {
                var existingMechanic = await _mechanicRepo.GetByIdAsync(mechanicModel.Id);
                if (existingMechanic == null)
                {
                    return (false, "Mechanic Not Found!!");
                }

                _mapper.Map(mechanicModel, existingMechanic);
                _mapper.Map(mechanicModel, existingMechanic.User);

                existingMechanic.UpdateWorkHours(mechanicModel.WorkHours);
                existingMechanic.UpdateRating(mechanicModel.Rating);
                existingMechanic.UpdateStatus(mechanicModel.Status);

                _mechanicRepo.Update(existingMechanic);
                await _mechanicRepo.SaveChangesAsync();
                return (true, "Mechanic Updated Successfully!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string message)> DeleteMechanicAsync(int id)
        {
            try
            {
                var result = await _mechanicRepo.GetByIdAsync(id);
                if (result == null) return (false, "Mechanic Not Found!!");

                _mechanicRepo.Delete(result);
                await _mechanicRepo.SaveChangesAsync();
                return (true, "Mechanic Deleted Successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete: {ex.Message}");
            }
        }
    }
}