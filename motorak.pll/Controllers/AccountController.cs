//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using motorak.dal.Entites;
//using motorak.DAL.DataBase;
//using Motorak.DAL.Entities;

//namespace Motorak.PLL.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly MotorakDbContext _context;

//        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, MotorakDbContext context)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            var user = await _userManager.FindByEmailAsync(model.Email);
//            if (user == null)
//            {
//                ModelState.AddModelError("", "Invalid login attempt.");
//                return View(model);
//            }

//            var result = await _signInManager.PasswordSignInAsync(
//                user.UserName,
//                model.Password,
//                model.RememberMe,
//                lockoutOnFailure: false
//            );

//            if (result.Succeeded)
//                return RedirectToAction("Index", "Home");

//            ModelState.AddModelError("", "Invalid login attempt.");
//            return View(model);
//        }


//        [HttpGet]
//        public IActionResult SignUp() => View();

//        [HttpPost]
//        public async Task<IActionResult> SignUp(SignUpViewModel model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            var user = new User
//            {
//                UserName = model.UserName,  
//                Email = model.Email,        
//                Name = model.FullName,      
//                CreatedAt = DateTime.UtcNow
//            };

//            var result = await _userManager.CreateAsync(user, model.Password);

//            if (result.Succeeded)
//            {
//                await _userManager.AddToRoleAsync(user, model.Role);

//                if (model.Role == "Customer")
//                    _context.Customers.Add(new Customer { UserId = user.Id });
//                else if (model.Role == "Mechanic")
//                    _context.Mechanics.Add(new Mechanic { UserId = user.Id });

//                await _context.SaveChangesAsync();
//                await _signInManager.SignInAsync(user, false);

//                return RedirectToAction("Index", "Home");
//            }

//            foreach (var error in result.Errors)
//                ModelState.AddModelError("", error.Description);

//            return View(model);
//        }

//        //[Authorize]
//        //public IActionResult Profile()
//        //{
//        //    var model = new ProfileViewModel
//        //    {
//        //        Name = User.Identity.Name,
//        //        Email = User.FindFirst("Email")?.Value,
//        //        Role = User.IsInRole("Admin") ? "Administrator" :
//        //               User.IsInRole("Mechanic") ? "Mechanic" : "Customer",

//        //        TotalPurchases = GetUserPurchases(User.Identity.Name).Count,
//        //        TotalRentals = GetUserRentals(User.Identity.Name).Count,
//        //        TotalServices = GetUserServices(User.Identity.Name).Count,
//        //        MemberSince = GetMembershipDate(User.Identity.Name).Year
//        //    };

//        //    return View(model);
//        //}

//    }
//}
