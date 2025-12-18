using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart GetShoppingCart(string userId);
        bool AddToCart(string userId, Guid bookId, int quantity);
        bool RemoveFromCart(string userId, Guid bookId);
        bool ClearCart(string userId);
    }
}
