using System.ComponentModel.DataAnnotations;

namespace TicTacToeAPI.Presentation.Models
{
    public class Player : BaseEntity
    {
        public Guid PlayerId { get; set; }

        public string? PlayerName { get; set; }
    }
}
