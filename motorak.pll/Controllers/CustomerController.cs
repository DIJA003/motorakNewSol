using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Motorak.BLL.ModelVM.Customer;
using Motorak.BLL.Services.Abstractions;
using Motorak.BLL.Services.Implementations;
using motorak.DAL.DataBase;
using Motorak.DAL.Entites;
using Motorak.DAL.Enums.SeviceEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motorak.PLL.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        
        //[Authorize(Roles = "Admin")]
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

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var (status, message, customer) = await _customerService.GetCustomerByIdAsync(id);
            if (!status || customer == null)
            {
                ViewBag.Error = message;
                return View(message);
            }

            if (customer.IsDeleted)
            {
                ViewBag.Error = "This customer has been deleted.";
                return View("Customer Deleted");
            }

            var model = new EditCustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCustomerModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _customerService.EditCustomerAsync(model);
            if (!status)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var (status, message) = await _customerService.DeleteCustomerAsync(id);
            if (!status)
            {
                TempData["Error"] = "Could not delete Customer.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Customer Deleted Successfully";
            return RedirectToAction(nameof(Index));

        }

    }
}
