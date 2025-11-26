using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_books_api.Data.Models; // 👈 cambia según el namespace donde esté tu clase Book
using Microsoft.EntityFrameworkCore;



namespace my_books_api.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

               var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {

                            Title = "The Hobbit",
                            Description = "Fantasy novel by J.R.R. Tolkien",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-10),
                            Rate = 9,
                            Genre = "Fantasy",
                            Author = "J.R.R. Tolkien",
                            CoverUrl = "https://example.com/hobbit.jpg",
                            DateAdded = DateTime.Now
                        },

                         new Book()
                         {
                             Title = "Clean Code",
                             Description = "A Handbook of Agile Software Craftsmanship",
                             IsRead = false,
                             Genre = "Programming",
                             Author = "Robert C. Martin",
                             CoverUrl = "https://example.com/cleancode.jpg",
                             DateAdded = DateTime.Now
                         }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}