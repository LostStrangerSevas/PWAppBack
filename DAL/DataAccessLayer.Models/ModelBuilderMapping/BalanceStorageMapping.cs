using DataAccessLayer.Models.Classes;
using DataAccessLayer.Models.ModelBuilderHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Models.ModelBuilderMapping
{
    /// <summary>
    /// Класс мапинга полей (для миграции)
    /// </summary>
    public class BalanceStorageMapping : IEntityMappingConfiguration<BalanceStorage>
    {
        public void Map(EntityTypeBuilder<BalanceStorage> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.TransactionId).IsRequired().ValueGeneratedNever();
            builder.Property(e => e.UserId).IsRequired().ValueGeneratedNever();
            builder.Property(e => e.Value).IsRequired();
            builder.HasOne(e => e.Transaction)
                .WithMany(e => e.BalanceStorages)
                .HasForeignKey(e => e.TransactionId);
            builder.HasOne(e => e.User)
                .WithMany(e => e.BalanceStorages)
                .HasForeignKey(e => e.UserId);
        }
    }
}
