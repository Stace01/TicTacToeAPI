using System.ComponentModel.DataAnnotations;

namespace TicTacToeAPI.Presentation.Models
{
    /// <summary>
    /// Базовая модель для будущих сущностей.
    /// </summary>
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }
}
