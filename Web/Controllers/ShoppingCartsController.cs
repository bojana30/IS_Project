using Domain.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Implementation;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: ShoppingCarts
        // Shows current user's cart
        public IActionResult Index()
        {
            var userId = GetUserId();
            var cart = _shoppingCartService.GetShoppingCart(userId);

            if (cart == null)
                return View(new ShoppingCart());

            return View(cart);
        }

        // POST: ShoppingCarts/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(Guid bookId, int quantity = 1)
        {
            var userId = GetUserId();

            var success = _shoppingCartService.AddToCart(userId, bookId, quantity);

            if (!success)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        // POST: ShoppingCarts/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(Guid bookId)
        {
            var userId = GetUserId();

            var success = _shoppingCartService.RemoveFromCart(userId, bookId);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // POST: ShoppingCarts/ClearCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearCart()
        {
            var userId = GetUserId();

            _shoppingCartService.ClearCart(userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order()
        {
            var userId = GetUserId();
            _orderService.CreateOrder(userId);
            return RedirectToAction("Index", "Orders");
        }

    }
}