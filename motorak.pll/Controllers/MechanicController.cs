using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.Customer;
using Motorak.BLL.ModelVM.Mechanic;
using Motorak.BLL.Services.Abstractions;

namespace Motorak.PLL.Controllers
{
    public class MechanicController : Controller
    {
        private readonly IMechanicService _mechanicService;
        public MechanicController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (status, message, mechanics) = await _mechanicService.GetAllMechanicsAsync();
            if (!status || mechanics == null)
            {
                ViewBag.Error = message;
                return View("No Mechanics Found");
            }
            return View(mechanics);
        }


        public async Task<IActionResult> MechanicDetails(int id)
        {
            var (status, message, mechanic) = await _mechanicService.GetMechanicByIdAsync(id);
            if (!status || mechanic == null)
            {
                ViewBag.Error = message;
                return View("Mechanic Not Found");
            }
            return View(mechanic);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMechanicModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _mechanicService.CreateMechanicAsync(model);
            if (!status)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var (status, message, mechanic) = await _mechanicService.GetMechanicByIdAsync(id);
            if (!status || mechanic == null)
            {
                ViewBag.Error = message;
                return View("Mechanic Not Found");
            }

            if (mechanic.IsDeleted)
            {
                ViewBag.Error = "This mechanic has been deleted.";
                return View("Mechanic Deleted");
            }

            var model = new EditMechanicModel
            {
                Id = mechanic.Id,
                Name = mechanic.Name,
                WorkHours = mechanic.WorkHours,
                Rating = mechanic.Rating,
                Status = mechanic.Status,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMechanicModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _mechanicService.EditMechanicAsync(model);
            if (!status)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var (status, message) = await _mechanicService.DeleteMechanicAsync(id);
            if (!status)
            {
                TempData["Error"] = "Could not delete Mechanic.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Mechanic Deleted Successfully";
            return RedirectToAction(nameof(Index));

        }

    }
}

