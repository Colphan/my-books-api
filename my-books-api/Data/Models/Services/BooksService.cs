
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
    }
}