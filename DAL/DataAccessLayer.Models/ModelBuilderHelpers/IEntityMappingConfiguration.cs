using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Models.ModelBuilderHelpers
{
    /// <summary>
    /// Интерфейс конфигурации маппинга билдера
    /// </summary>
    /// <typeparam name="T">Ссылочный тип</typeparam>
    public interface IEntityMappingConfiguration<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}
