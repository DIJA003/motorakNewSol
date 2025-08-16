using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using motorak.bll.ModelVM.ShoppingCart;
using motorak.bll.Services.Abstractions;
using motorak.dal.Entites;
using motorak.dal.Enums.CartEnums;
using Motorak.BLL.Helper;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAL.Entites;

namespace Motorak.PLL.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CartController> _logger;

        public CartController(
            ICartService cartService,
            UserManager<User> userManager,
            ILogger<CartController> logger)
        {
            _cartService = cartService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                var cartItems = await _cartService.GetCartItemsAsync(userId);
                return View(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cart items");
                TempData["Error"] = "Unable to load cart items. Please try again.";
                return View(new List<object>()); // Return empty list
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, message = "User not authenticated" });
                }

                var (success, message) = await _cartService.AddToCartAsync(
                    request.CarId,
                    userId,
                    request.ItemType,
                    request.StartDate,
                    request.EndDate
                );

                if (success)
                {
                    var count = await _cartService.GetCartItemCountAsync(userId);
                    return Json(new { success = true, message, count });
                }

                return Json(new { success = false, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCartSimple(int id, string itemType = "Buy")
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, message = "User not authenticated" });
                }

                CartItemType cartItemType = itemType.ToLower() == "rent" ? CartItemType.Rent : CartItemType.Buy;

                var (success, message) = await _cartService.AddToCartAsync(
                    id,
                    userId,
                    cartItemType,
                    cartItemType == CartItemType.Rent ? DateTime.Now : null,
                    cartItemType == CartItemType.Rent ? DateTime.Now.AddDays(1) : null
                );

                if (success)
                {
                    var count = await _cartService.GetCartItemCountAsync(userId);
                    return Json(new { success = true, message, count });
                }

                return Json(new { success = false, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartRequest request)
        {
            try
            {
                if (request == null)
                {
                    return Json(new { success = false, message = "Invalid request data" });
                }

                // Validate quantity
                if (request.Quantity <= 0)
                {
                    return Json(new { success = false, message = "Quantity must be greater than 0" });
                }

                // Validate dates if provided
                if (request.StartDate.HasValue && request.EndDate.HasValue)
                {
                    if (request.StartDate >= request.EndDate)
                    {
                        return Json(new { success = false, message = "End date must be after start date" });
                    }

                    if (request.StartDate < DateTime.Today)
                    {
                        return Json(new { success = false, message = "Start date cannot be in the past" });
                    }
                }

                var (success, message) = await _cartService.UpdateCartItemAsync(
                    request.CartItemId,
                    request.Quantity,
                    request.StartDate,
                    request.EndDate
                );

                if (success)
                {
                    var userId = _userManager.GetUserId(User);
                    var total = await _cartService.GetCartTotalAsync(userId);
                    return Json(new { success = true, message, total });
                }

                return Json(new { success = false, message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart item. CartItemId: {CartItemId}", request?.CartItemId);
                return Json(new { success = false, message = "Failed to update cart item. Please try again." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                if (cartItemId <= 0)
                {
                    return Json(new { success = false, message = "Invalid cart item ID" });
                }

                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, message = "User not authenticated" });
                }

                var (success, message) = await _cartService.RemoveFromCartAsync(cartItemId, userId);

                if (success)
                {
                    var count = await _cartService.GetCartItemCountAsync(userId);
                    var total = await _cartService.GetCartTotalAsync(userId);
                    return Json(new { success = true, message, count, total });
                }

                return Json(new { success = false, message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cart item. CartItemId: {CartItemId}", cartItemId);
                return Json(new { success = false, message = "Failed to remove item from cart. Please try again." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, message = "User not authenticated" });
                }

                var (success, message) = await _cartService.ClearCartAsync(userId);
                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart for user");
                return Json(new { success = false, message = "Failed to clear cart. Please try again." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { count = 0 });
                }

                var count = await _cartService.GetCartItemCountAsync(userId);
                return Json(new { count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart count");
                return Json(new { count = 0 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                var cartItems = await _cartService.GetCartItemsAsync(userId);

                if (!cartItems.Any())
                {
                    TempData["Error"] = "Your cart is empty. Add some items before checking out.";
                    return RedirectToAction("Index", "Car");
                }

                // Validate all cart items before checkout
                var invalidItems = cartItems.Where(item =>
                    item.ItemType == CartItemType.Rent &&
                    (!item.RentStartDate.HasValue || !item.RentEndDate.HasValue ||
                     item.RentStartDate < DateTime.Today ||
                     item.RentStartDate >= item.RentEndDate)).ToList();

                if (invalidItems.Any())
                {
                    TempData["Error"] = "Some items in your cart have invalid rental dates. Please update them before checkout.";
                    return RedirectToAction("Index");
                }

                var model = new CheckoutViewModel
                {
                    CartItems = cartItems,
                    TotalAmount = cartItems.Sum(c => c.TotalPrice)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during checkout process");
                TempData["Error"] = "Unable to process checkout. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartSummary()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { count = 0, total = 0 });
                }

                var count = await _cartService.GetCartItemCountAsync(userId);
                var total = await _cartService.GetCartTotalAsync(userId);

                return Json(new { count, total });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart summary");
                return Json(new { count = 0, total = 0 });
            }
        }
    }
}