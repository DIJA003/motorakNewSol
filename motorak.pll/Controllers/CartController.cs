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

        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = _userManager.GetUserId(User);

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

        [HttpPost]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartRequest request)
        {
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

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = _userManager.GetUserId(User);
            var (success, message) = await _cartService.RemoveFromCartAsync(cartItemId, userId);

            if (success)
            {
                var count = await _cartService.GetCartItemCountAsync(userId);
                var total = await _cartService.GetCartTotalAsync(userId);
                return Json(new { success = true, message, count, total });
            }

            return Json(new { success = false, message });
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = _userManager.GetUserId(User);
            var (success, message) = await _cartService.ClearCartAsync(userId);
            return Json(new { success, message });
        }

        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var userId = _userManager.GetUserId(User);
            var count = await _cartService.GetCartItemCountAsync(userId);
            return Json(new { count });
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = await _cartService.GetCartItemsAsync(userId);

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty";
                return RedirectToAction("Index");
            }

            var model = new CheckoutViewModel
            {
                CartItems = cartItems,
                TotalAmount = cartItems.Sum(c => c.TotalPrice)
            };

            return View(model);
        }
    }

    

    

   
}

