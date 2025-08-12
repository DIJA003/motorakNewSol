using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.ServiceReview;
using Motorak.BLL.Services.Abstractions;

namespace Motorak.PLL.Controllers
{
    public class ServiceReviewController : Controller
    {
        private readonly IServiceReviewService _serviceReviewService;
        private readonly IMapper _mapper;

        public ServiceReviewController(IServiceReviewService serviceReviewService, IMapper mapper)
        {
            _serviceReviewService = serviceReviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (status, message, reviews) = await _serviceReviewService.GetAllAsync();
            if (!status)
            {
                ViewBag.Error = message;
                return View(new List<ServiceReviewDTO>());
            }
            return View(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var (status, message, review) = await _serviceReviewService.GetByIdAsync(id);
            if (!status || review == null)
            {
                return NotFound(message);
            }
            return View(review);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceReviewVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _serviceReviewService.CreateAsync(model);
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
            var (status, message, review) = await _serviceReviewService.GetByIdAsync(id);
            if (!status || review == null)
            {
                return NotFound(message);
            }

            var updateVM = _mapper.Map<UpdateServiceReviewVM>(review);
            return View(updateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateServiceReviewVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var (status, message) = await _serviceReviewService.UpdateAsync(model);
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
            var (status, message, review) = await _serviceReviewService.GetByIdAsync(id);
            if (!status || review == null)
            {
                return NotFound(message);
            }
            return View(review);
        }

        [HttpPost] 
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (status, message) = await _serviceReviewService.DeleteAsync(id);
            if (!status)
            {
                ViewBag.Error = message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ByService(int serviceId)
        {
            var (status, message, reviews) = await _serviceReviewService.GetByServiceIdAsync(serviceId);
            if (!status)
            {
                ViewBag.Error = message;
                return View(new List<ServiceReviewDTO>());
            }
            return View("Index", reviews);
        }

        [HttpGet]
        public async Task<IActionResult> ByCustomer(int customerId)
        {
            var (status, message, reviews) = await _serviceReviewService.GetByCustomerIdAsync(customerId);
            if (!status)
            {
                ViewBag.Error = message;
                return View(new List<ServiceReviewDTO>());
            }
            return View("Index", reviews);
        }

        [HttpGet]
        public async Task<IActionResult> ByRating(int rating)
        {
            var (status, message, reviews) = await _serviceReviewService.GetByRatingAsync(rating);
            if (!status)
            {
                ViewBag.Error = message;
                return View(new List<ServiceReviewDTO>());
            }
            return View("Index", reviews);
        }
    }
}
