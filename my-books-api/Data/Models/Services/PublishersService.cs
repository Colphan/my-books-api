using Microsoft.AspNetCore.Components.Web;
using my_books_api.Data.Models;
using my_books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using my_books_api.Exceptions;
using my_books_api.Data.Models;
using my_books_api.Data.Paging;


namespace my_books_api.Data.Services
{
    public class PublishersService
    {

        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;

        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers = _context.Publishers.OrderBy(n => n.Id).ToList();

            // sort
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "asc_name":
                        allPublishers = allPublishers.OrderBy(n => n.Id).ToList();
                        break;

                    case "desc_name":
                        allPublishers = allPublishers.OrderByDescending(n => n.Id).ToList();
                        break;
                }
            }

            // filter

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Paging
            int pageSize = 5;
            return PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);
            
        }

        public Publisher AddPublisher(PublisherVM publisher)

        {
            if (StringStartWithNumber(publisher.Name)) throw new PublisherNameException("Name starts with number", publisher.Name);

            var _publisher = new Publisher()
            {

                Name = publisher.Name

            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);


        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()

                }).ToList()
            }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {

            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} does no exist");
            }
        }

        private bool StringStartWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));


    }
}
