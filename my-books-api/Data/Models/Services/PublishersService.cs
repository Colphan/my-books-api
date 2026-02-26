using Microsoft.AspNetCore.Components.Web;
using my_books_api.Data.Models;
using my_books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books_api.Data.Services
{
    public class PublishersService
    {

        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;

        } 

        public void AddPublisher(PublisherVM publisher)

        {

            var _publisher = new Publisher()
            {
               
            Name = publisher.Name

            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }
    }
}
