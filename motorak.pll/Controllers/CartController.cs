using Microsoft.AspNetCore.Mvc;
using Motorak.BLL.Helper;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAL.Entites;

namespace Motorak.PLL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICarServicecs _carService;

        public CartController(ICarServicecs carService)
        {
            _carService = carService;
        }

        private List<CartItem> GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.CarId == id);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                var (status, message, car) = await _carService.GetCarByIdAsync(id);
                if (!status || car == null)
                    return NotFound(message);

                cart.Add(new CartItem
                {
                    CarId = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Price = car.Price,
                    ImagePath = car.ImagePath ?? "/images/default.jpg",
                    Quantity = 1
                });
            }

            SaveCart(cart);
            return Json(new { count = cart.Sum(c => c.Quantity) });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int carId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.CarId == carId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
    }
}
