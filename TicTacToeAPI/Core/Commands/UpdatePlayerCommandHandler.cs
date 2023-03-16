using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Core.Interfaces;
using TicTacToeAPI.Presentation.Models;
using TicTacToeAPI.Infrastructure.DataBase;
using TicTacToeAPI.Core.Exceptions;

namespace TicTacToeAPI.Core.Commands
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdatePlayerCommandHandler(IAppDbContext appDbContext) =>
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        // Unit - это тип, означающий пустой ответ.
        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Players.FirstOrDefaultAsync(player => player.Id == request.Id, cancellationToken);

            if (entity == null || entity.PlayerId != request.PlayerId)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }

            entity.PlayerName = request.PlayerName;
            entity.UpdatedDateTime = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<UpdatePlayerCommand>.Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
