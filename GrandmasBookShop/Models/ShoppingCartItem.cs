using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasBookShop.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        public Book Book { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
