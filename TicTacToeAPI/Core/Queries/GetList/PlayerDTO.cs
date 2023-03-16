using AutoMapper;
using TicTacToeAPI.Core.Mappings;
using TicTacToeAPI.Presentation.Models;

namespace TicTacToeAPI.Core.Queries.GetList
{
    public class PlayerDTO : IMapWith<Player>
    {
        public Guid PlayerId { get; set; }

        public string? PlayerName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, PlayerDTO>()
                .ForMember(playerDTO => playerDTO.PlayerId, opt => opt.MapFrom(playerDTO => playerDTO.PlayerId))
                .ForMember(playerDTO => playerDTO.PlayerName, opt => opt.MapFrom(playerDTO => playerDTO.PlayerName));
        }
    }
}
