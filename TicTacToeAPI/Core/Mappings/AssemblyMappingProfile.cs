using AutoMapper;
using System.Reflection;

namespace TicTacToeAPI.Core.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingsfromAssembly(assembly);

        /// <summary>
        /// Сканирует сборку и ищет любые типы
        /// которые реализуют интерфейс IMapWith.
        /// </summary>
        /// <param name="assembly"></param>
        private void ApplyMappingsfromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMapWith<>))).
                ToList();

            // Вызывает метод "Mapping".
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
