using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Core.Interfaces;

namespace TicTacToeAPI.Infrastructure.DataBase
{
    public static class DependencyInjection
    {
        // Метод расширения для добавления контекста
        // базы данных в веб приложении.
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            return services;
        }
    }
}
