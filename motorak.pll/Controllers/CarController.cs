using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.Car;
using Motorak.BLL.Services.Abstractions;
using Motorak.BLL.Services.Implementations;
using Motorak.DAl.Repo.Abstractions;

namespace Motorak.PLL.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarServicecs _carService;
        private readonly ICustomerRebo _customerRebo;

        public CarController(ICarServicecs carService, ICustomerRebo customerRebo)
        {
            _carService = carService;
            _customerRebo = customerRebo;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (status, message, cars) = await _carService.GetAllCarsAsync();
            if (!status)
            {
                ViewBag.Error = message;
                return View("Error");
            }

            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var (status, message, car) = await _carService.GetCarByIdAsync(id);
            if (!status || car == null)
                return NotFound(message);

            return View(car);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCarModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _carService.CreateCarAsync(model);
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
            var (status, message, car) = await _carService.GetCarByIdAsync(id);
            if (!status || car == null) return NotFound();

            var editModel = new EditCarModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Mileage = car.Mileage,
                Condition = car.Condition,
                Transmission = car.Transmission,
                Price = car.Price,
                Type = car.Type
            };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCarModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _carService.UpdateCarAsync(model);
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
            var (status, message) = await _carService.DeleteCarAsync(id);
            if (!status)
            {
                TempData["Error"] = message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = message;
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Sell(int carId)
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
                return Unauthorized();

            var customer = await _customerRebo.GetByUsernameAsync(userName);
            if (customer == null)
                return BadRequest("Customer not found.");

            var (status, message) = await _carService.SellCarAsync(carId, customer.Id);
            TempData[status ? "Success" : "Error"] = message;
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rent(int carId)
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
                return Unauthorized();

            var customer = await _customerRebo.GetByUsernameAsync(userName);
            if (customer == null)
                return BadRequest("Customer not found.");

            var (status, message) = await _carService.RentCarAsync(carId, customer.Id);
            TempData[status ? "Success" : "Error"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}
