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
            // Get the current user's Id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the user's shopping cart including items and related book details
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                    .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // If the cart doesn't exist, create an empty one
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return View(cart);
        }

        // POST: ShoppingCart/AddToCart?bookId=5&quantity=1
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, int quantity = 1)
        {
            // Get the current user's Id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve or create the user's shopping cart
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Check if the book is already in the cart
            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                // Increase quantity if the item exists
                item.Quantity += quantity;
            }
            else
            {
                // Otherwise, add a new item
                item = new ShoppingCartItem
                {
                    BookId = bookId,
                    Quantity = quantity,
                    ShoppingCartId = cart.Id
                };
                _context.ShoppingCartItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: ShoppingCart/RemoveFromCart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            // Find the cart item by its Id
            var item = await _context.ShoppingCartItems.FindAsync(itemId);
            if (item != null)
            {
                _context.ShoppingCartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
