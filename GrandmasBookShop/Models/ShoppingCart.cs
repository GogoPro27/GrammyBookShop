using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrandmasBookShop.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
