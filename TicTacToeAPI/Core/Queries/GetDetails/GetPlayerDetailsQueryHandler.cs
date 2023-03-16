using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Core.Commands;
using TicTacToeAPI.Core.Interfaces;
using TicTacToeAPI.Core.Exceptions;
using TicTacToeAPI.Presentation.Models;

namespace TicTacToeAPI.Core.Queries.GetDetails
{
    public class GetPlayerDetailsQueryHandler : IRequestHandler<GetPlayerDetailsQuery, PlayerDetailsVm>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPlayerDetailsQueryHandler(IAppDbContext appDbContext, IMapper mapper) =>
            (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<PlayerDetailsVm> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Players
                .FirstOrDefaultAsync(player => player.PlayerName == request.PlayerName, cancellationToken);

            if (entity == null || entity.PlayerId != request.PlayerId)
            {
                throw new NotFoundException(nameof(Player), request.PlayerId);
            }

            return _mapper.Map<PlayerDetailsVm>(entity);
        }
    }
}
