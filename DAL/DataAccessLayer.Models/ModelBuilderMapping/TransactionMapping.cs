using DataAccessLayer.Models.Classes;
using DataAccessLayer.Models.ModelBuilderHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Models.ModelBuilderMapping
{
    /// <summary>
    /// Класс мапинга полей (для миграции)
    /// </summary>
    public class TransactionMapping : IEntityMappingConfiguration<Transaction>
    {
        public void Map(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.ExecutionDate).IsRequired().HasDefaultValueSql("GETDATE()"); ;
            builder.Property(e => e.Value).IsRequired();
        }
    }
}
