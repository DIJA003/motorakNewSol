using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.Service;
using Motorak.BLL.Services.Abstractions;
using Motorak.BLL.Services.Implementations;
using Motorak.DAL.Enums.SeviceEnums;

namespace Motorak.PLL.Controllers
{
    public class ServiveController : Controller
    {
        private readonly IServiceServicecs servicecs;
        private readonly IMapper _mapper;
        public ServiveController (IServiceServicecs servicecs,IMapper mapper)
        {
            this.servicecs = servicecs;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(
        Status? status, int? carId, int? customerId, DateTime? from, DateTime? to, int? mechanicId)
        {
            var (success, message, services) = await servicecs.GetServicesFilteredAsync(status, carId, customerId, from, to, mechanicId);

            if (!success)
                ViewBag.Error = message;

            return View(services);
        }

        public async Task<IActionResult> Details(int id)
        {
            var (status, message, service) = await servicecs.GetServiceByIdAsync(id);
            if (!status || service == null)
                return NotFound(message);

            return View(service);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceVM model)
        {
            if(!ModelState.IsValid) return View(model);
            var (status, message) = await servicecs.CreateServiceAsync(model);
            if (!status)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditStatus(int id)
        {
            var (status, message, service) = await servicecs.GetServiceByIdAsync(id);
            if (!status || service == null)
                return NotFound(message);

            var updateModel = _mapper.Map<UpdateServiceVM>(service);
            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(UpdateServiceVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var (status, message) = await servicecs.UpdateServiceStatusAsync(model);
            if (!status)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var (status, message) = await servicecs.DeleteServiceAsync(id);
            if (!status)
            {
                TempData["Error"] = message;
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
