using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.Rents;
using Motorak.BLL.Services.Abstractions;
using motorak.DAL.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Motorak.PLL.Controllers
{
    //[Authorize]  // Require authentication for all actions
    public class RentController : Controller
    {
        private readonly IRentService _service;
        private readonly MotorakDbContext _context;

        public RentController(IRentService service, MotorakDbContext context)
        {
            _service = service;
            _context = context;
        }

        [AllowAnonymous] // Anyone can view the list
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        [AllowAnonymous] // Anyone can view details, but we restrict if needed
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            // Get the current logged-in user's Customer record
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.UserId == userId);
                
                // Check if the rent belongs to the current customer OR user is admin
                if (customer != null && item.CustomerId != customer.Id && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }
                else if (customer == null && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }
            }
            else if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? carId = null, decimal? price = null)
        {
            var model = new RentCreateDto();
            if (carId.HasValue) model.CarId = carId.Value;
            if (price.HasValue) model.TotalPrice = price.Value;

            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today.AddDays(1);

            // Get the current logged-in user's Customer record
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.UserId == userId);
                
                if (customer != null)
                {
                    model.CustomerId = customer.Id; // This is the int CustomerId
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentCreateDto dto)
        {
            // Get the current logged-in user's Customer record
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // Verify the CustomerId belongs to the current user
            if (customer == null && !User.IsInRole("Admin"))
            {
                ModelState.AddModelError("", "You must be a registered customer to rent a car.");
                return View(dto);
            }

            // For non-admin users, force CustomerId to their own customer ID
            if (!User.IsInRole("Admin"))
            {
                dto.CustomerId = customer.Id;
            }

            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _service.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")] // Only admins can edit
        public async Task<IActionResult> Edit(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var dto = new RentUpdateDto
            {
                Id = existing.Id,
                PaymentMethod = existing.PaymentMethod,
                TotalPrice = existing.TotalPrice,
                Status = existing.Status
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, RentUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _service.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();
            return View(existing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // Optional: View current user's rentals
        [HttpGet]
        public async Task<IActionResult> MyRentals()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
            {
                return NotFound("Customer profile not found.");
            }

            var allRentals = await _service.GetAllAsync();
            var myRentals = allRentals.Where(r => r.CustomerId == customer.Id).ToList();
            
            return View(myRentals);
        }
    }
}