namespace BooksApp.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class GoogleBookServiceApiTests
    {
        // this test is an integration test
        [TestMethod]
        public async Task should_return_books_when_query_provided()
        {
            // arrange
            string isbn = "0071807993";

            // act
            var service = new GoogleBookServiceApi("PersonalDevelopment", "AIzaSyBAVxiyeXcKjUQU303tdt6L9hHT9L_s2_o");

            var actual = await service.Get(isbn);

            // assert
            Assert.IsNotNull(actual);
        }
    }
}
