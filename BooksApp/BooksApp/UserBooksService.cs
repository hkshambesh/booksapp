namespace BooksApp
{
    using BooksApp.Interfaces;
    using BooksApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserBooksService : IUserBooksService
    {
        private readonly IUserRepositoy userRepositoy;
        private readonly IBookServiceApi bookServiceApi;
        private readonly IConsole console;


        private Guid UserId = Guid.NewGuid();

        public UserBooksService(IUserRepositoy userRepositoy, IBookServiceApi bookServiceApi, IConsole console)
        {
            this.userRepositoy = userRepositoy;
            this.bookServiceApi = bookServiceApi;
            this.console = console;
        }

        public async Task ProcessUserReadingList()
        {
            // assume this user created in database by default
            this.userRepositoy.Add(new Models.User
            {
                Id = UserId
            });

            this.console.WriteLine("Enter your search query:");

            string query = this.console.ReadLine();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var books = await bookServiceApi.Get(query);

                if (!books.Any())
                {
                    this.console.WriteLine($"No books found. query: {query}");
                }

                // only the top 5 books as requirement
                var adjustedBooks = books.Count() > 5 ? books.Take(5).ToList() : books.ToList();

                while (adjustedBooks.Any())
                {
                    var selectedBookNumber = SelectBook(adjustedBooks);

                    if (selectedBookNumber != 0)
                    {
                        var selectedBook = adjustedBooks[selectedBookNumber - 1];

                        AddBookToReadingList(selectedBook);

                        // remove book from original list
                        adjustedBooks.Remove(selectedBook);

                        this.console.WriteLine(string.Empty);
                    }
                }
            }
        }

        private int SelectBook(IEnumerable<Book> books)
        {
            this.console.WriteLine($"The following books found:");

            int count = 1;

            foreach (var book in books) 
            {
                this.console.WriteLine($"Number: ({count}), Title: {book.Title}, Author: {book.Author}, Publisher: {book.Publisher}");
                count++;
            }

            this.console.WriteLine("To Add to your reading list, select one of the above list by entering the number");

            var userSelectedNumber = this.console.ReadLine();

            return Convert.ToInt32(userSelectedNumber);
        }

        private void AddBookToReadingList(Book selectedBook)
        {
            // add book to reading list
            userRepositoy.AddToReadingList(UserId, selectedBook);

            this.console.WriteLine($"Book have been added to your reading list. Your List:");

            var user = userRepositoy.GetById(UserId);

            foreach (var book in user.ReadingList)
            {
                this.console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Publisher: {book.Publisher}");
            }
        }
    }
}
