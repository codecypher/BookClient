using System;
using System.Collections.Generic;

namespace BookClient.Models
{
    public class BookModel
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        //public IList<string> Authors { get; set; }
    }
}
