namespace BooksApp.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public Guid Id { get; set; }
        public IList<Book> ReadingList { get; set; } = new List<Book>();
    }
}
