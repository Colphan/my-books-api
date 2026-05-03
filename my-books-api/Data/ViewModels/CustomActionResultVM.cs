using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_books_api.Data.Models;

namespace my_books_api.Data.ViewModels
{

    public class CustomActionResultVM
    {
        public Exception Exception { get; set; }

        public Publisher Publisher { get; set; }

    }
}