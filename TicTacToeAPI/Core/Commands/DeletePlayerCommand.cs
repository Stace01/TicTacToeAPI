using MediatR;

namespace TicTacToeAPI.Core.Commands
{
    public class DeletePlayerCommand : IRequest
    {
        public Guid PlayerId { get; set; }

        public string? PlayerName { get; set; }
    }
}
