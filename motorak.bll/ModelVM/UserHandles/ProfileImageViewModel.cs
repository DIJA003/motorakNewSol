using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Motorak.BLL.ModelVM.NewFolder
{
    public class ProfileImageViewModel
    {
        [Required,Display(Name="Profile Image")]
        public IFormFile ImageFile { get; set; }
    }
}
