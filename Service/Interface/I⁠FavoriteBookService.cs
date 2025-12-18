using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFavoriteBookService
    {
        IEnumerable<FavoriteBook> GetFavoritesForUser(string userId);
        FavoriteBook? AddToFavorites(string userId, Guid bookId);
        FavoriteBook? RemoveFromFavorites(string userId, Guid bookId);
        bool IsFavorite(string userId, Guid bookId);
    }
    }
