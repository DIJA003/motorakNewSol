
using AutoMapper;
using motorak.dal.Entites;
using Motorak.BLL.ModelVM.Customer;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAl.Repo.Abstractions;
using Motorak.DAl.Repo.Implementations;
using Motorak.DAL.Entites;

namespace Motorak.BLL.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRebo _customerRepo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRebo _customerRepo, IMapper _mapper)
        {
            this._customerRepo = _customerRepo;
            this._mapper = _mapper;
        }


        public async Task<(bool status, string message, List<CustomerListModel>)> GetAllCustomersAsync()
        {
            try
            {
                var result = await _customerRepo.GetAllAsync();
                var resultList = _mapper.Map<List<CustomerListModel>>(result);
                return (true, "Customer retrieved successfully!", resultList);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to retrieve customers : {ex.Message}", new List<CustomerListModel>());
            }
        }

        public async Task<(bool status, string message, CustomerDetailsModel?)> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _customerRepo.GetByIdAsync(id);
                if (customer == null) return (false, "Customer Not Found!!", null);
                var result = _mapper.Map<CustomerDetailsModel>(customer);
                return (true, "Customer retrieved successfully!", result);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to retrieve car : {ex.Message}", null);
            }
        }

        public async Task<(bool status, string message, CustomerDetailsModel?)> GetCustomerByUsernameAsync(string username)
        {
            try
            {
                var customer = await _customerRepo.GetByUsernameAsync(username);
                if (customer == null) return (false, "Customer Not Found!!", null);
                var result = _mapper.Map<CustomerDetailsModel>(customer);
                return (true, "Customer retrieved successfully!", result);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to retrieve customer : {ex.Message}", null);
            }
        }
        public async Task<(bool status, string message)> CreateCustomerAsync(CreateCustomerModel customerModel)
        {
            try
            {
                if (customerModel == null) 
                    return (false, "Customer Null");
                var result = _mapper.Map<Customer>(customerModel);
                await _customerRepo.CreateAsync(result);
                return (true, "Customer Created Successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Error Ocurred: {ex.Message}");
            }
        }


        public async Task<(bool status, string message)> EditCustomerAsync(EditCustomerModel customerModel)
        {
            try
            {
                var existingCustomer = await _customerRepo.GetByUserIdAsync(customerModel.Id);
                if (existingCustomer == null)
                {
                    return (false, "Customer Not Found!!");
                }
                _mapper.Map(customerModel, existingCustomer);
                _customerRepo.Update(existingCustomer);
                await _customerRepo.SaveChangesAsync();
                return (true, "Customer Updated Successfully!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string message)> DeleteCustomerAsync(int id)
        {
            try
            {
                var result = await _customerRepo.GetByIdAsync(id);
                if (result == null) return (false, "Customer Not Found!!");
                _customerRepo.Delete(result);
                await _customerRepo.SaveChangesAsync();
                return (true, "Customer Deleted Successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete : {ex.Message}");
            }
        }
        

        public async Task<(bool status, string message,CustomerDetailsModel?)> IsEmailExistsAsync(string email)
        {
            try
            {

                var customer = await _customerRepo.GetByEmailAsync(email);
                if (customer == null) return (false, "Customer Not Found!!", null);
                var result = _mapper.Map<CustomerDetailsModel>(customer);
                return (true, "Customer retrieved successfully!", result);
            }
            catch (Exception ex)
            {

                return (false, $"Failed to find Email : {ex.Message}",null);
            }
        }

        public async Task<(bool status, string message, CustomerDetailsModel?)> IsPhoneNumberExistsAsync(string phoneNumber)
        {
            try
            {

                var customer = await _customerRepo.GetByPhoneAsync(phoneNumber);
                if (customer == null) return (false, "Phone Number Not Found!!", null);
                var result = _mapper.Map<CustomerDetailsModel>(customer);
                return (true, "Customer retrieved successfully!", result);
            }
            catch (Exception ex)
            {

                return (false, $"Failed to find Phone Number : {ex.Message}", null);
            }
        }
    }
}
