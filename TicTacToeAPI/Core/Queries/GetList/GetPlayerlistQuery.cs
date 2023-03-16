using MediatR;

namespace TicTacToeAPI.Core.Queries.GetList
{
    public class GetPlayerlistQuery : IRequest<PlayerlistVm>
    {
        public Guid PlayerId { get; set; }

        public string? PlayerName { get; set; }
    }
}
