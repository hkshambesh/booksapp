namespace BooksApp
{
    using BooksApp.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class ProgramModule
    {
        public static ServiceProvider Build()
        {
            return new ServiceCollection()
                .AddTransient<IUserRepositoy, UserRepositoy>()
                .AddTransient<IBookServiceApi>(x => new GoogleBookServiceApi("PersonalDevelopment", "AIzaSyBAVxiyeXcKjUQU303tdt6L9hHT9L_s2_o")) // assume this is stored in app.config file
                .AddTransient<IUserBooksService, UserBooksService>()
                .AddTransient<IConsole, ConsoleWrapper>()
                .BuildServiceProvider();
        }
    }
}
