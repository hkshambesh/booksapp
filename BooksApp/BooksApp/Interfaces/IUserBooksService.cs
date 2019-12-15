namespace BooksApp.Interfaces
{
    using System.Threading.Tasks;

    public interface IUserBooksService
    {
        Task ProcessUserReadingList();
    }
}
