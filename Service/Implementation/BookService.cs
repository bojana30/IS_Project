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
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book CreateBook(Book book)
        {
            return _bookRepository.Insert(book);
        }

        public Book DeleteBook(Guid id)
        {
            var book = GetBook(id);
            return _bookRepository.Delete(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll(b => b);
        }

        public Book? GetBook(Guid id)
        {
            return _bookRepository.Get(b => b, b => b.Id == id);
        }

        public IEnumerable<Book> GetBooksByGenre(string genre)
        {
            return _bookRepository.GetAll(b => b, b => b.Genre == genre);
        }

        public Book UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }

        public bool BookExists(Guid id)
        {
            return _bookRepository
                .GetAll(b => b)   // селекторот b => b го враќа целиот ентитет
                .Any(b => b.Id == id);
        }

    }
}

