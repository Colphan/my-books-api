
using Microsoft.AspNetCore.Components.Web;
using my_books_api.Data.Models;
using my_books_api.Data.ViewModels;

namespace my_books_api.Data.Services
{
    public class BooksService
    {

        private AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;

        } 

        public void AddBook(BookVM book)

        {

            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now

            };

            _context.Books.Add(_book);
            _context.SaveChanges();
        }
   
   public List<Book> GetAllBooks() => _context.Books.ToList();
   public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n => n.Id == bookId);
   
   public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.Author = book.Author;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();

            }

            return _book;
        }
    }
}