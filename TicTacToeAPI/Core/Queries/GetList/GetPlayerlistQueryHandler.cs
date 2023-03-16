using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using TicTacToeAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TicTacToeAPI.Core.Queries.GetList
{
    public class GetPlayerlistQueryHandler : IRequestHandler<GetPlayerlistQuery, PlayerlistVm>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPlayerlistQueryHandler(IAppDbContext appDbContext, IMapper mapper) =>
            (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<PlayerlistVm> Handle(GetPlayerlistQuery request, CancellationToken cancellationToken)
        {
            var playerQuery = await _appDbContext.Players
                .Where(player => player.PlayerId == request.PlayerId)
                .ProjectTo<PlayerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PlayerlistVm { Players = playerQuery };
        }
    }
}
