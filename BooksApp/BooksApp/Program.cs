namespace BooksApp
{
    using BooksApp.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to your Books");

            // setup IoC
            var serviceProvider = ProgramModule.Build();

            // execute program
            var service = serviceProvider.GetService<IUserBooksService>();

            while (true)
            {
                await service.ProcessUserReadingList();

                Console.ReadLine();
            }
        }
    }
}
