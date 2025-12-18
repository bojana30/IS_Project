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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _cartRepository;
        private readonly IRepository<BookInShoppingCart> _cartItemRepository;

        public ShoppingCartService(
            IRepository<ShoppingCart> cartRepository,
            IRepository<BookInShoppingCart> cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public ShoppingCart GetShoppingCart(string userId)
        {
            return _cartRepository.Get(c => c, c => c.UserId == userId);
        }

        public bool AddToCart(string userId, Guid bookId, int quantity)
        {
            var cart = GetShoppingCart(userId);

            var item = new BookInShoppingCart
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                ShoppingCartId = cart.Id,
                Quantity = quantity
            };

            _cartItemRepository.Insert(item);
            return true;
        }

        public bool RemoveFromCart(string userId, Guid bookId)
        {
            var cart = GetShoppingCart(userId);

            var item = _cartItemRepository.Get(i => i,
                i => i.BookId == bookId && i.ShoppingCartId == cart.Id);

            if (item == null) return false;

            _cartItemRepository.Delete(item);
            return true;
        }

        public bool ClearCart(string userId)
        {
            var cart = GetShoppingCart(userId);

            var items = _cartItemRepository.GetAll(i => i, i => i.ShoppingCartId == cart.Id);

            foreach (var item in items)
                _cartItemRepository.Delete(item);

            return true;
        }
    }
}
