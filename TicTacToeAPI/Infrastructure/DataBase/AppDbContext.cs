using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Core.Interfaces;
using TicTacToeAPI.Presentation.Models;
using TicTacToeAPI.Infrastructure.EntityTypeConfigurations.Fluent;

namespace TicTacToeAPI.Infrastructure.DataBase
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Player> Players { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlayerEntityConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
