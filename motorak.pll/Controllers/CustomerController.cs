using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.Customer;
using Motorak.BLL.Services.Abstractions;
using Motorak.Utility;

namespace Motorak.PLL.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [Authorize(Roles=Seed.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (status, message, customers) = await _customerService.GetAllCustomersAsync();
            if (!status || customers == null)
            {
                ViewBag.Error = message;
                return View("No Customers Found");
            }
            return View(customers);
        }

        [Authorize(Roles = Seed.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> CustomerDetails(int id)
        {
            var (status, message, customer) = await _customerService.GetCustomerByIdAsync(id);
            if (!status || customer == null)
            {
                ViewBag.Error = message;
                return View("Customer Not Found");
            }
            return View(customer);
        }

        [Authorize(Roles = Seed.Role_Admin)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Seed.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _customerService.CreateCustomerAsync(model);
            if (!status)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = Seed.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var (status, message, existing) = await _customerService.GetCustomerByIdAsync(id);
            if (!status || existing == null)
            {
                ViewBag.Error = message;
                return NotFound(message);
            }

            if (existing.IsDeleted)
            {
                ViewBag.Error = "This customer has been deleted.";
                return View("Customer Deleted");
            }

            var model = new EditCustomerModel
            {
                Id = existing.Id,
                Name= existing.Name,
                PhoneNumber = existing.PhoneNumber
            };
            return View(model);

        }

        [Authorize(Roles = Seed.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCustomerModel model)
        {
            try
            {
                await _customerService.EditCustomerAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [Authorize(Roles = Seed.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var (status, message) = await _customerService.DeleteCustomerAsync(id);
            if (!status)
            {
                TempData["Error"] = message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Customer Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
