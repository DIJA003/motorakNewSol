using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.ModelVM.ServiceReview;
using Motorak.BLL.Services.Abstractions;
using Motorak.Utility;

namespace Motorak.PLL.Controllers
{
    public class ServiceReviewController : Controller
    {
        private readonly IServiceReviewService _serviceReviewService;

        public ServiceReviewController(IServiceReviewService serviceReviewService)
        {
            _serviceReviewService = serviceReviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (status, message, reviews) = await _serviceReviewService.GetAllAsync();
            if (!status) ViewBag.Error = message;
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[Authorize(Roles = Seed.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceReviewVM model)
        {
            if (ModelState.IsValid)
            {
                var (status, message) = await _serviceReviewService.CreateAsync(model);
                if (status) return RedirectToAction(nameof(Index));
                ViewBag.Error = message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var (status, message, review) = await _serviceReviewService.GetByIdAsync(id);
            if (!status || review == null) return NotFound(message);

            var model = new UpdateServiceReviewVM
            {
                ReviewId = review.ReviewId,
                Rating = review.Rating,
                Comments = review.Comments
            };
            return View(model);
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
            if (!status || review == null) return NotFound(message);
            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int reviewId)
        {
            var (status, message) = await _serviceReviewService.DeleteAsync(reviewId);
            if (status) return RedirectToAction(nameof(Index));
            ViewBag.Error = message;
            return RedirectToAction(nameof(Delete), new { id = reviewId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var (status, message, review) = await _serviceReviewService.GetByIdAsync(id);
            if (!status || review == null) return NotFound(message);
            return View(review);
        }
    }
}
