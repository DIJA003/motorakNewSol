using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Email Address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}