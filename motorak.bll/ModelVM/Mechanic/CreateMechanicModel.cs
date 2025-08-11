
using System.ComponentModel.DataAnnotations;

namespace Motorak.BLL.ModelVM.Mechanic
{
    public class CreateMechanicModel
    {
        [Required]
        public string Name { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),Display(Name="Confirm Password"),Compare("Password",ErrorMessage="Not matched with Password")]
        public string ConfirmPassword { get; set; }

        [Required,Display(Name ="Working Hours")]
        public string WorkHours { get; set; }

        [Required]
        public string Status { get; set; }
        public bool IsDeleted { get; set; } //= false;
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; private set; } //= DateTime.Now;


    }
}
