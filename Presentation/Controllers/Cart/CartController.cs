using DTO.Cart;
using DTO.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;

namespace Presentation.Controllers.Cart
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public IActionResult AddToCart(CartDTO dto)
        {
            bool isSuccess;
            string mes;
            try
            {
                dto.UserId = Convert.ToInt32(HttpContext.User.FindFirst(x => x.Type == "Id")?.Value);
                _cartService.AddToCart(dto);
                mes = "The book has been successfully added to your cart!";
                isSuccess = true;
            }
            catch (Exception exception)
            {
                mes = exception.Message;
                isSuccess = false;
            }

            return RedirectToAction("Get", "Book",
                new
                {
                    id = dto.BookId,
                    message = mes,
                    isSuccess
                });
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int cartId)
        {
            _cartService.DeleteFromCart(cartId);

            return RedirectToAction("GetCart", "Cart",
                new
                {
                    message = "The book has been successfully deleted from your cart!",
                    isSuccess = true
                });
        }

        [HttpGet]
        public IActionResult GetCart(string message = null, bool isSuccess = true)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (isSuccess)
                {
                    ViewBag.Success = message;
                }
                else
                {
                    ViewBag.Error = message;
                }
            }

            var userId = Convert.ToInt32(HttpContext.User.FindFirst(x => x.Type == "Id")?.Value);
            var result = _cartService.GetCart(userId);
            return View(result);
        }

        [HttpPost]
        public IActionResult Buy(PayDTO dto)
        {
            _cartService.Buy(dto.CartId);

            return RedirectToAction("GetCart",
                new
                {
                    message = "Success! You paid " + "$" + dto.Total + "!",
                    isSuccess = true
                });
        }
    }
}