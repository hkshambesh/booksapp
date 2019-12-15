using BooksApp.Models;
using System;

namespace BooksApp.Interfaces
{
    public interface IUserRepositoy
    {
        User GetById(Guid id);
        void Add(User user);
        void AddToReadingList(Guid id, Book book);
    }
}
