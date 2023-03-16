using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Presentation.Models;

namespace TicTacToeAPI.Core.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Player> Players { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
