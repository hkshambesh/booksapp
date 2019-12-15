namespace BooksApp
{
    using BooksApp.Interfaces;
    using BooksApp.Models;
    using Google.Apis.Books.v1;
    using Google.Apis.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GoogleBookServiceApi : IBookServiceApi
    {
        private readonly BooksService booksService;

        public GoogleBookServiceApi(string appName, string apiKey)
        {
            booksService = new BooksService(new BaseClientService.Initializer
            {
                ApplicationName = appName,
                ApiKey = apiKey,
            });
        }

        public async Task<IList<Book>> Get(string query)
        {
            var books = new List<Book>();

            try
            {
                var apiResult = await this.booksService.Volumes.List(query).ExecuteAsync();

                foreach(var item in apiResult.Items)
                {
                    books.Add(new Book
                    {
                        Title = item.VolumeInfo.Title,
                        Author = item.VolumeInfo.Authors?.FirstOrDefault(),
                        Publisher = item.VolumeInfo.Publisher
                    });
                }
            }
            catch(Exception ex)
            {
                // for now we will log to the console
                Console.WriteLine(ex.Message);
            }

            return books;
        }
    }
}
