using Domain.DomainModels;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<BookInShoppingCart> _cartItemsRepository;
        private readonly IRepository<ShoppingCart> _cartRepository;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<BookInShoppingCart> cartItemsRepository,
            IRepository<ShoppingCart> cartRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartItemsRepository = cartItemsRepository;
            _cartRepository = cartRepository;
        }

        public Order CreateOrder(string userId)
        {
            // Get shopping cart
            var cart = _cartRepository.Get(c => c, c => c.UserId == userId);

            if (cart == null)
                throw new Exception("Shopping cart not found");

            // Get cart items WITH Book included
            var cartItems = _cartItemsRepository
                .GetAll(i => i, i => i.ShoppingCartId == cart.Id)
                .ToList();

            if (!cartItems.Any())
                throw new Exception("Shopping cart is empty");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Pending
            };

            double totalAmount = 0;

            // Map cart items → order items
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Book.Price
                };

                totalAmount += orderItem.TotalPrice;

                _orderItemRepository.Insert(orderItem);
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;
            _orderRepository.Insert(order);

            // Clear shopping cart
            foreach (var item in cartItems)
            {
                _cartItemsRepository.Delete(item);
            }

            return order;
        }

        public IEnumerable<Order> GetOrdersForUser(string userId)
        {
            return _orderRepository.GetAll(o => o, o => o.UserId == userId);
        }

        public Order? GetOrder(Guid id)
        {
            return _orderRepository.Get(o => o, o => o.Id == id);
        }
    }
}