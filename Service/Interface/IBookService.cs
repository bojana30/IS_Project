using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBook(Guid id);
        Book CreateBook(Book book);
        Book UpdateBook(Book book);
        Book DeleteBook(Guid id);
        bool BookExists(Guid id);

        IEnumerable<Book> GetBooksByGenre(string genre);
    }
}
