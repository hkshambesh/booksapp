using BooksApp.Interfaces;
using BooksApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Tests
{
    [TestClass]
    public class UserBooksServiceTests
    {
        private Mock<IUserRepositoy> userRepositoryMock;
        private Mock<IBookServiceApi> bookServiceApiMock;
        private Mock<IConsole> consoleMock;


        [TestInitialize]
        public void Init()
        {
            userRepositoryMock = new Mock<IUserRepositoy>();
            bookServiceApiMock = new Mock<IBookServiceApi>();
            consoleMock = new Mock<IConsole>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            userRepositoryMock = null;
            bookServiceApiMock = null;
            consoleMock = null;
        }

        [TestMethod]
        public async Task user_should_add_book_to_reading_list_from_book_colletion()
        {
            // arrange
            var books = new List<Book>
            {
                new Book { Title = "Test 1" }
            };

            var user = new User
            {
                Id = Guid.NewGuid()
            };

            userRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(user);
            bookServiceApiMock.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(books);
            consoleMock.Setup(x => x.ReadLine()).Returns("1");

            // act
            var service = new UserBooksService(userRepositoryMock.Object, bookServiceApiMock.Object, consoleMock.Object);
            await service.ProcessUserReadingList();

            // assert
            userRepositoryMock.Verify(x => x.AddToReadingList(It.IsAny<Guid>(), It.IsAny<Book>()), Times.Once);
        }
    }
}
