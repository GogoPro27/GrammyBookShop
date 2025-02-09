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
    }
}
