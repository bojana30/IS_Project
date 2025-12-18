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
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BooksController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: Books
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
            //return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid id)
        {
            var book = _bookService.GetBook(id);
            if (book == null) return NotFound();
            return View(book);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var book = await _context.Books
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (book == null)
            //{
            //    return NotFound();
            //}

            //return View(book);

        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            book.Id = Guid.NewGuid();

            _bookService.CreateBook(book);

            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    book.Id = Guid.NewGuid();
            //    _context.Add(book);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(Guid id)
        {
            var book = _bookService.GetBook(id);
            if (book == null) return NotFound();
            return View(book);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var book = await _context.Books.FindAsync(id);
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Book book)
        {
            if (id != book.Id) 
                return NotFound();

            if (!ModelState.IsValid) 
                return View(book);

            if (!_bookService.BookExists(id)) 
                return NotFound();

            _bookService.UpdateBook(book);
            return RedirectToAction(nameof(Index));
            //if (id != book.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(book);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!BookExists(book.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(Guid id)
        {
            var book = _bookService.GetBook(id);
            if (book == null) return NotFound();
            return View(book);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var book = await _context.Books
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (book == null)
            //{
            //    return NotFound();
            //}

            //return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
            //var book = await _context.Books.FindAsync(id);
            //if (book != null)
            //{
            //    _context.Books.Remove(book);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

       
    }
}
