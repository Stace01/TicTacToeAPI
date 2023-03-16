using TicTacToeAPI.Presentation.Models;

namespace TicTacToeAPI.Infrastructure.DataBase
{
    /// <summary>
    /// Используется при старте приложения и проверяет,
    /// создана ли база данных. Если нет, то она будет
    /// создана на основе имеющегося контекста.
    /// </summary>
    public class DbInitializer
    {
        public static void Initialize(AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreatedAsync();
            dbContext.SaveChanges();
        }
    }
}
