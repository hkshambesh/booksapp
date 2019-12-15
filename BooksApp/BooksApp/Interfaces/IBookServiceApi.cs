namespace BooksApp.Interfaces
{
    using BooksApp.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookServiceApi
    {
        Task<IList<Book>> Get(string query);
    }
}
