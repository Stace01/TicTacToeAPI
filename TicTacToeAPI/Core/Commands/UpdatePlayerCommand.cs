using MediatR;

namespace TicTacToeAPI.Core.Commands
{
    public class UpdatePlayerCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public string? PlayerName { get; set; }
    }
}
