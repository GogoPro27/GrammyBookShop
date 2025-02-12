using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GrandmasBookShop.Data;
using GrandmasBookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrandmasBookShop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                    .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, LastUpdated = DateTime.UtcNow };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (cart.LastUpdated < DateTime.UtcNow.AddMinutes(-10))
                {
                    foreach (var item in cart.Items)
                    {
                        if (item.Book != null)
                        {
                            item.Book.CopiesAvailable += item.Quantity;
                        }
                    }
                    _context.ShoppingCartItems.RemoveRange(cart.Items);
                    cart.LastUpdated = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Your cart has been cleared due to inactivity.";
                }
            }

            return View(cart);
        }

        // POST: ShoppingCart/AddToCart?bookId=5&quantity=1
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            if (book.CopiesAvailable < quantity)
            {
                TempData["Error"] = "Not enough copies available.";
                return RedirectToAction("Details", "Books", new { id = bookId });
            }

            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, LastUpdated = DateTime.UtcNow };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            book.CopiesAvailable -= quantity;

            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new ShoppingCartItem
                {
                    BookId = bookId,
                    Quantity = quantity,
                    ShoppingCartId = cart.Id
                };
                _context.ShoppingCartItems.Add(item);
            }

            cart.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // POST: ShoppingCart/RemoveFromCart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var item = await _context.ShoppingCartItems.FindAsync(itemId);
            if (item != null)
            {
                var book = await _context.Books.FindAsync(item.BookId);
                if (book != null)
                {
                    book.CopiesAvailable += item.Quantity;
                }

                _context.ShoppingCartItems.Remove(item);

                var cart = await _context.ShoppingCarts.FindAsync(item.ShoppingCartId);
                if (cart != null)
                {
                    cart.LastUpdated = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
