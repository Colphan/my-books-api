using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books_api.Exceptions
{
    [Serializable]

    public class PublisherNameException : Exception
    {
        public string PublisherName { get; set; }

        public PublisherNameException()
        {

        }

        public PublisherNameException(string massage) : base(massage)

        {

        }


        public PublisherNameException(string massage, Exception inner) : base(massage, inner)
        {

        }

        public PublisherNameException(string massage, string PublisherName) : this(massage)
        {
            PublisherName = PublisherName;
        }



    }

}

