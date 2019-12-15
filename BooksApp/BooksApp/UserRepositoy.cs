namespace BooksApp
{
    using BooksApp.Interfaces;
    using BooksApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserRepositoy : IUserRepositoy
    {
        private readonly IList<User> users;

        public UserRepositoy()
        {
            // assume the users are stored in a database
            this.users = new List<User>();
        }

        public void Add(User user)
        {
            this.users.Add(user);
        }

        public void AddToReadingList(Guid id, Book book)
        {
            var user = this.users.FirstOrDefault(x => x.Id == id);
            user.ReadingList.Add(book);
        }

        public User GetById(Guid id)
        {
            return this.users.FirstOrDefault(x=> x.Id == id);
        }
    }
}
