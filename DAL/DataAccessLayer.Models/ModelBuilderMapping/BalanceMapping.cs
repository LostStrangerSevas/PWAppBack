using DataAccessLayer.Models.Classes;
using DataAccessLayer.Models.ModelBuilderHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Models.ModelBuilderMapping
{
    /// <summary>
    /// Класс мапинга полей (для миграции)
    /// </summary>
    public class BalanceMapping : IEntityMappingConfiguration<Balance>
    {
        public void Map(EntityTypeBuilder<Balance> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.UserId).IsRequired().ValueGeneratedNever();
            builder.Property(e => e.Value).IsRequired();
            builder.HasIndex(e => new { e.UserId }).IsUnique(true); //один баланс для одного пользователя
            builder.HasOne(e => e.User)
                .WithOne(e => e.Balance)
                .HasForeignKey<Balance>(e => e.UserId);
        }
    }
}
