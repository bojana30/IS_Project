using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.DomainModels;
using Web.Data;

namespace Web.Controllers
{
    public class BookInShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookInShoppingCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookInShoppingCarts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InShoppingCarts.Include(b => b.Book).Include(b => b.ShoppingCart);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookInShoppingCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInShoppingCart = await _context.InShoppingCarts
                .Include(b => b.Book)
                .Include(b => b.ShoppingCart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookInShoppingCart == null)
            {
                return NotFound();
            }

            return View(bookInShoppingCart);
        }

        // GET: BookInShoppingCarts/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author");
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "UserId");
            return View();
        }

        // POST: BookInShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,ShoppingCartId,Quantity")] BookInShoppingCart bookInShoppingCart)
        {
            if (ModelState.IsValid)
            {
                bookInShoppingCart.Id = Guid.NewGuid();
                _context.Add(bookInShoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookInShoppingCart.BookId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "UserId", bookInShoppingCart.ShoppingCartId);
            return View(bookInShoppingCart);
        }

        // GET: BookInShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInShoppingCart = await _context.InShoppingCarts.FindAsync(id);
            if (bookInShoppingCart == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookInShoppingCart.BookId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "UserId", bookInShoppingCart.ShoppingCartId);
            return View(bookInShoppingCart);
        }

        // POST: BookInShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BookId,ShoppingCartId,Quantity")] BookInShoppingCart bookInShoppingCart)
        {
            if (id != bookInShoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookInShoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookInShoppingCartExists(bookInShoppingCart.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", bookInShoppingCart.BookId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "UserId", bookInShoppingCart.ShoppingCartId);
            return View(bookInShoppingCart);
        }

        // GET: BookInShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInShoppingCart = await _context.InShoppingCarts
                .Include(b => b.Book)
                .Include(b => b.ShoppingCart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookInShoppingCart == null)
            {
                return NotFound();
            }

            return View(bookInShoppingCart);
        }

        // POST: BookInShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bookInShoppingCart = await _context.InShoppingCarts.FindAsync(id);
            if (bookInShoppingCart != null)
            {
                _context.InShoppingCarts.Remove(bookInShoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookInShoppingCartExists(Guid id)
        {
            return _context.InShoppingCarts.Any(e => e.Id == id);
        }
    }
}
