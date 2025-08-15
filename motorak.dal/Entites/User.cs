
using Microsoft.AspNetCore.Identity;

namespace motorak.dal.Entites
{
    
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ImagePath { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
    
}
