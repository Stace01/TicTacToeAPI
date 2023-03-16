using MediatR;
using TicTacToeAPI.Core.Interfaces;
using TicTacToeAPI.Presentation.Models;
using TicTacToeAPI.Infrastructure.DataBase;

namespace TicTacToeAPI.Core.Commands
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Guid>
    {
        private readonly IAppDbContext _appDbContext;

        public CreatePlayerCommandHandler(IAppDbContext appDbContext) =>
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player
            {
                PlayerId = request.PlayerId,
                PlayerName = request.PlayerName,
                Id = Guid.NewGuid(),
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = null
            };

            await _appDbContext.Players.AddAsync(player, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return player.Id;
        }
    }
}
