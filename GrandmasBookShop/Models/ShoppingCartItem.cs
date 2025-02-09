using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasBookShop.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        // Foreign key to the Book
        [ForeignKey("Book")]
        public int BookId { get; set; }

        // Foreign key to the ShoppingCart
        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
