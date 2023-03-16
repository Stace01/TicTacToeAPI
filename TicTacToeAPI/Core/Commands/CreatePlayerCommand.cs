using MediatR;

namespace TicTacToeAPI.Core.Commands
{
    public class CreatePlayerCommand : IRequest<Guid>
    {
        public Guid PlayerId { get; set; }

        public string? PlayerName { get; set; }
    }
}
