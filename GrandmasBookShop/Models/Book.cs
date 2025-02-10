using System.ComponentModel.DataAnnotations;

namespace GrandmasBookShop.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Author { get; set; }

        [Display(Name = "Cover Photo URL")]
        public string PhotoUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }

        [Display(Name = "Copies Available")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Copies available must be 0 or more.")]
        public int CopiesAvailable { get; set; }

        // New: Genre property
        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; }
    }
}
