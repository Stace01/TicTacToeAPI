using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeAPI.Presentation.Models;

namespace TicTacToeAPI.Infrastructure.EntityTypeConfigurations.Fluent
{
    internal class PlayerEntityConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player");

            builder.HasIndex(p => p.PlayerId).IsUnique();
            builder.Property(p => p.PlayerId).HasColumnName("PlayerId");
            builder.Property(p => p.PlayerName).IsRequired().HasMaxLength(40).HasColumnName("Name");
        }
    }
}
