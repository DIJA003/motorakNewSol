
using System.ComponentModel.DataAnnotations;

namespace Motorak.BLL.ModelVM.Customer
{
    public class CreateCustomerModel
    {
        [Required,Display(Name="Full Name")]
        public string Name { get; set; }

        [Required, EmailAddress,MinLength(8)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.PhoneNumber),Display(Name="Phone number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirm Password"), Compare("Password", ErrorMessage = "Not matched with Password")]
        public string ConfirmPassword { get; set; }

        public bool IsDeleted { get; set; } //= false;
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; private set; } //= DateTime.Now;
    }
}
