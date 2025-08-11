using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.Rents;
using Motorak.BLL.Services.Abstractions;

namespace Motorak.PLL.Controllers
{
    public class RentsController : Controller
    {
        private readonly IRentService _service;

        public RentsController(IRentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RentCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RentCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var id = await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
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
        public async Task<IActionResult> Edit(int id, RentUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dto);
            try
            {
                await _service.UpdateAsync(dto);
                return RedirectToAction(nameof(Details), new { id = dto.Id });
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();
            return View(existing);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
