namespace GrandmasBookShop.Models
{
    public class BooksIndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public string SearchString { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
