using BooksApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BooksApp.Tests
{
    [TestClass]
    public class UserRepositoyTests
    {
        [TestMethod]
        public void should_return_new_user_after_created()
        {
            // arrange
            var userId = Guid.NewGuid();
            var repository = new UserRepositoy();

            // act
            repository.Add(new Models.User
            {
                Id = userId
            });

            var actual = repository.GetById(userId);

            // assert
            Assert.AreEqual(actual.Id, userId);
        }

        [TestMethod]
        public void should_return_list_of_reading_list_once_added()
        {
            // arrange
            var userId = Guid.NewGuid();
            var repository = new UserRepositoy();

            repository.Add(new Models.User
            {
                Id = userId
            });

            var book = new Book
            {
                Author = "Test",
                Title = "Test Title",
                Publisher = "Test Company"
            };

            // act
            repository.AddToReadingList(userId, book);
            var actual = repository.GetById(userId);

            // assert
            Assert.IsNotNull(actual.ReadingList.FirstOrDefault(x=>x.Title.Equals(book.Title)));
        }
    }
}
