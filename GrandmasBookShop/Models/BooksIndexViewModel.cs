namespace GrandmasBookShop.Models
{
    public class BooksIndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public string SearchString { get; set; }
        public string GenreFilter { get; set; }  // Current genre filter

        // List of available genres to populate the dropdown.
        public IEnumerable<string> Genres { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
