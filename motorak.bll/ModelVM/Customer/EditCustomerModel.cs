
using System.ComponentModel.DataAnnotations;

namespace Motorak.BLL.ModelVM.Customer
{
    public class EditCustomerModel
    {
        [Required]
        public string Id { get; set; }

        [Required, Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string? ImagePath { get; set; }
        public DateTime UpdatedAt { get; set; } //= DateTime.Now;
        public bool IsUpdated { get; set; } //= true;

    }
}
