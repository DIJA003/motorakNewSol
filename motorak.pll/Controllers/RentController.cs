using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.Rents;
using Motorak.BLL.Services.Abstractions;
using System.Security.Claims;

namespace Motorak.PLL.Controllers
{
    [Authorize]  // Require authentication for all actions
    public class RentController : Controller
    {
        private readonly IRentService _service;

        public RentController(IRentService service)
        {
            _service = service;
        }

        [AllowAnonymous] // Anyone can view the list
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        [AllowAnonymous] // Anyone can view details, but we restrict in view if needed
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            // Optional: hide details if not the owner or admin
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (item.CustomerId.ToString() != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(item);
        }

        [HttpGet]
        public IActionResult Create(int? carId = null, decimal? price = null)
        {
            var model = new RentCreateDto();
            if (carId.HasValue) model.CarId = carId.Value;
            if (price.HasValue) model.TotalPrice = price.Value;

            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today.AddDays(1);

            // Automatically set CustomerId to the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int customerId))
            {
                model.CustomerId = customerId;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentCreateDto dto)
        {
            // Ensure the CustomerId in the form matches the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userId, out int currentUserId))
            {
                ModelState.AddModelError("", "User not authenticated.");
                return View(dto);
            }

            if (dto.CustomerId != currentUserId && !User.IsInRole("Admin"))
            {
                ModelState.AddModelError("CustomerId", "You can only create rentals for yourself.");
                return View(dto);
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
        [Authorize(Roles = "Admin")] // Only admins can edit
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();
            return View(existing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
    }
}