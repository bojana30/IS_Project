using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.DomainModels;
using Repository;

namespace Web.Controllers
{
    public class FavoriteBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FavoriteBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FavoriteBooks.Include(f => f.Book).Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FavoriteBooks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteBook = await _context.FavoriteBooks
                .Include(f => f.Book)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteBook == null)
            {
                return NotFound();
            }

            return View(favoriteBook);
        }

        // GET: FavoriteBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: FavoriteBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BookId,AddedAt")] FavoriteBook favoriteBook)
        {
            if (ModelState.IsValid)
            {
                favoriteBook.Id = Guid.NewGuid();
                _context.Add(favoriteBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", favoriteBook.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favoriteBook.UserId);
            return View(favoriteBook);
        }

        // GET: FavoriteBooks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteBook = await _context.FavoriteBooks.FindAsync(id);
            if (favoriteBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", favoriteBook.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favoriteBook.UserId);
            return View(favoriteBook);
        }

        // POST: FavoriteBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,BookId,AddedAt")] FavoriteBook favoriteBook)
        {
            if (id != favoriteBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteBookExists(favoriteBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", favoriteBook.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favoriteBook.UserId);
            return View(favoriteBook);
        }

        // GET: FavoriteBooks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteBook = await _context.FavoriteBooks
                .Include(f => f.Book)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteBook == null)
            {
                return NotFound();
            }

            return View(favoriteBook);
        }

        // POST: FavoriteBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var favoriteBook = await _context.FavoriteBooks.FindAsync(id);
            if (favoriteBook != null)
            {
                _context.FavoriteBooks.Remove(favoriteBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteBookExists(Guid id)
        {
            return _context.FavoriteBooks.Any(e => e.Id == id);
        }
    }
}
