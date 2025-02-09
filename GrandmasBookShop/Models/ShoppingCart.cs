using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrandmasBookShop.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        // The user identifier (from ASP.NET Identity)
        public string UserId { get; set; }

        // Navigation property for cart items
        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
