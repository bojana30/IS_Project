using Domain.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ReviewsController : Controller
    {
        //        private readonly ApplicationDbContext _context;

        //        public ReviewsController(ApplicationDbContext context)
        //        {
        //            _context = context;
        //        }

        //        // GET: Reviews
        //        public async Task<IActionResult> Index()
        //        {
        //            var applicationDbContext = _context.Reviews.Include(r => r.Book).Include(r => r.User);
        //            return View(await applicationDbContext.ToListAsync());
        //        }

        //        // GET: Reviews/Details/5
        //        public async Task<IActionResult> Details(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var review = await _context.Reviews
        //                .Include(r => r.Book)
        //                .Include(r => r.User)
        //                .FirstOrDefaultAsync(m => m.Id == id);
        //            if (review == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(review);
        //        }

        //        // GET: Reviews/Create
        //        public IActionResult Create()
        //        {
        //            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author");
        //            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        //            return View();
        //        }

        //        // POST: Reviews/Create
        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Create([Bind("Id,Rating,Comment,BookId,UserId,CreatedAt")] Review review)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                review.Id = Guid.NewGuid();
        //                _context.Add(review);
        //                await _context.SaveChangesAsync();
        //                return RedirectToAction(nameof(Index));
        //            }
        //            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", review.BookId);
        //            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
        //            return View(review);
        //        }

        //        // GET: Reviews/Edit/5
        //        public async Task<IActionResult> Edit(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var review = await _context.Reviews.FindAsync(id);
        //            if (review == null)
        //            {
        //                return NotFound();
        //            }
        //            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", review.BookId);
        //            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
        //            return View(review);
        //        }

        //        // POST: Reviews/Edit/5
        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Rating,Comment,BookId,UserId,CreatedAt")] Review review)
        //        {
        //            if (id != review.Id)
        //            {
        //                return NotFound();
        //            }

        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    _context.Update(review);
        //                    await _context.SaveChangesAsync();
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {
        //                    if (!ReviewExists(review.Id))
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                return RedirectToAction(nameof(Index));
        //            }
        //            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", review.BookId);
        //            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
        //            return View(review);
        //        }

        //        // GET: Reviews/Delete/5
        //        public async Task<IActionResult> Delete(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var review = await _context.Reviews
        //                .Include(r => r.Book)
        //                .Include(r => r.User)
        //                .FirstOrDefaultAsync(m => m.Id == id);
        //            if (review == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(review);
        //        }

        //        // POST: Reviews/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> DeleteConfirmed(Guid id)
        //        {
        //            var review = await _context.Reviews.FindAsync(id);
        //            if (review != null)
        //            {
        //                _context.Reviews.Remove(review);
        //            }

        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }

        //        private bool ReviewExists(Guid id)
        //        {
        //            return _context.Reviews.Any(e => e.Id == id);
        //        }
        //    }
        //}

        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: Reviews?bookId=...
        // Usually reviews are shown per book
        public IActionResult Index(Guid bookId)
        {
            var reviews = _reviewService.GetReviewsForBook(bookId);
            ViewBag.BookId = bookId;
            return View(reviews);
        }

        // GET: Reviews/Create?bookId=...
        public IActionResult Create(Guid bookId)
        {
            var review = new Review
            {
                BookId = bookId
            };

            return View(review);
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (!ModelState.IsValid)
                return View(review);

            review.Id = Guid.NewGuid();
            review.UserId = GetUserId();
            review.CreatedAt = DateTime.Now;

            _reviewService.AddReview(review);

            return RedirectToAction(nameof(Index), new { bookId = review.BookId });
        }

        // GET: Reviews/Delete/5
        public IActionResult Delete(Guid id)
        {
            var review = _reviewService
                .GetReviewsForBook(Guid.Empty) // dummy call avoided in View
                .FirstOrDefault(r => r.Id == id);

            if (review == null)
                return NotFound();

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _reviewService.DeleteReview(id);
            return RedirectToAction("Index", "Books");
        }
    }
}
