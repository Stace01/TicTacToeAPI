using AutoMapper;

namespace TicTacToeAPI.Core.Mappings
{
    public interface IMapWith<T>
    {
        /// <summary>
        /// Создаём конфигурацию из исходного типа Т
        /// и назначения.
        /// </summary>
        /// <param name="profile"></param>
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
