using Domain.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: Orders
        public IActionResult Index()
        {
            var userId = GetUserId();
            var orders = _orderService.GetOrdersForUser(userId);
            return View(orders);
        }

        // GET: Orders/Details/5
        public IActionResult Details(Guid id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: Orders/Create
        // This is the "additional action" required by the project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            var userId = GetUserId();

            var order = _orderService.CreateOrder(userId);

            return RedirectToAction(nameof(Details), new { id = order.Id });
        }
    }
}
