using MediatR;
using TicTacToeAPI.Core.Exceptions;
using TicTacToeAPI.Core.Interfaces;
using TicTacToeAPI.Presentation.Models;

namespace TicTacToeAPI.Core.Commands
{
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public DeletePlayerCommandHandler(IAppDbContext appDbContext) =>
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Players
                .FindAsync(new object[] {request.PlayerId}, cancellationToken);

            if (entity == null || entity.PlayerName != request.PlayerName)
            {
                throw new NotFoundException(nameof(Player), request.PlayerId);
            }

            _appDbContext.Players.Remove(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<DeletePlayerCommand>.Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
