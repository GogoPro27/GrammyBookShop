using System;
using System.Linq;
using System.Threading.Tasks;
using GrandmasBookShop.Data;
using GrandmasBookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GrandmasBookShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private const int PageSize = 9; // Change this to the desired number of items per page

        public BooksController(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchString, string genre, int page = 1)
        {
            var booksQuery = _context.Books.AsQueryable();

            // Filter by search string if provided.
            if (!string.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Name.Contains(searchString) || b.Author.Contains(searchString));
            }

            // Filter by genre if provided.
            if (!string.IsNullOrEmpty(genre))
            {
                booksQuery = booksQuery.Where(b => b.Genre == genre);
            }

            // Get total count for pagination.
            var count = await booksQuery.CountAsync();

            // Retrieve the books for the requested page.
            var books = await booksQuery
                .OrderBy(b => b.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            // Retrieve the distinct genres for the filter dropdown.
            var genres = await _context.Books.Select(b => b.Genre).Distinct().ToListAsync();

            var viewModel = new BooksIndexViewModel
            {
                Books = books,
                SearchString = searchString,
                GenreFilter = genre,
                Genres = genres,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)PageSize)
            };

            return View(viewModel);
        }



        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Author,PhotoUrl,Description,Price,CopiesAvailable,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }



        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,PhotoUrl,Description,Price,CopiesAvailable,Genre")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }




        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a book exists
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
