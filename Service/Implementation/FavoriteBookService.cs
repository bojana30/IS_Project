using Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class FavoriteBookService : IFavoriteBookService
    {
        private readonly IRepository<FavoriteBook> _favoriteBooksRepository;

        public FavoriteBookService(IRepository<FavoriteBook> favoriteBooksRepository)
        {
            _favoriteBooksRepository = favoriteBooksRepository;
        }

        public IEnumerable<FavoriteBook> GetFavoritesForUser(string userId)
        {
            return _favoriteBooksRepository.GetAll(
                selector: f => f,
                predicate: f => f.UserId == userId,
                include: f => f.Include(x => x.Book)

            );
        }

        public FavoriteBook? AddToFavorites(string userId, Guid bookId)
        {
            // Already favorite?
            var existing = _favoriteBooksRepository.Get(
                selector: f => f,
                predicate: f => f.UserId == userId && f.BookId == bookId
            );

            if (existing != null)
                return null; // already exists

            var favorite = new FavoriteBook
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BookId = bookId,
                //CreatedAt = DateTime.UtcNow
            };

            return _favoriteBooksRepository.Insert(favorite);
        }

        public FavoriteBook? RemoveFromFavorites(string userId, Guid bookId)
        {
            var favorite = _favoriteBooksRepository.Get(
                selector: f => f,
                predicate: f => f.UserId == userId && f.BookId == bookId
            );

            if (favorite == null) return null;

            _favoriteBooksRepository.Delete(favorite);
            return favorite;
        }

        public bool IsFavorite(string userId, Guid bookId)
        {
            var fav = _favoriteBooksRepository.Get(
                selector: f => f,
                predicate: f => f.UserId == userId && f.BookId == bookId
            );

            return fav != null;
        }
    }
}
