using AutoMapper;
using TicTacToeAPI.Core.Mappings;
using TicTacToeAPI.Presentation.Models;

namespace TicTacToeAPI.Core.Queries.GetDetails
{
    /// <summary>
    /// Класс, описывающий, что будет возвращено
    /// пользователю, когда он запросит детали игрока.
    /// </summary>
    public class PlayerDetailsVm : IMapWith<Player>
    {
        public Guid Id { get; set; }

        //public Guid PlayerId { get; set; }

        public string? PlayerName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, PlayerDetailsVm>()
                .ForMember(playerVm => playerVm.PlayerName, opt => opt.MapFrom(playerVm => playerVm.PlayerName))
                .ForMember(playerVm => playerVm.Id, opt => opt.MapFrom(playerVm => playerVm.Id))
                .ForMember(playerVm => playerVm.CreatedDateTime, opt => opt.MapFrom(playerVm => playerVm.CreatedDateTime))
                .ForMember(playerVm => playerVm.UpdatedDateTime, opt => opt.MapFrom(playerVm => playerVm.UpdatedDateTime));
        }
    }
}
