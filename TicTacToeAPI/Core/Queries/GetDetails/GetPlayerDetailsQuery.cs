using MediatR;

namespace TicTacToeAPI.Core.Queries.GetDetails
{
    public class GetPlayerDetailsQuery : IRequest<PlayerDetailsVm>
    {
        public Guid PlayerId { get; set; }

        public Guid Id { get; set; }

        public string? PlayerName { get; set; }

    }
}
