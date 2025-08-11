using Motorak.BLL.ModelVM.Customer;

namespace Motorak.BLL.Services.Abstractions
{
    public interface ICustomerService
    {
        Task<(bool status,string message,List<CustomerListModel>)> GetAllCustomersAsync();
        Task<(bool status, string message,CustomerDetailsModel?)> GetCustomerByIdAsync(int id);
        Task<(bool status, string message, CustomerDetailsModel?)> GetCustomerByUsernameAsync(string username);
        Task<(bool status, string message)> CreateCustomerAsync(CreateCustomerModel model);
        Task<(bool status, string message)> EditCustomerAsync(EditCustomerModel model);
        Task<(bool status, string message)> DeleteCustomerAsync(int id);
        Task<(bool status, string message, CustomerDetailsModel?)> IsEmailExistsAsync(string email);
        Task<(bool status, string message, CustomerDetailsModel?)> IsPhoneNumberExistsAsync(string phoneNumber);
    }
}
