using System;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models.ModelBuilderHelpers
{
    /// <summary>
    /// Класс расширения для мапинга сущностей
    /// </summary>
    public static class EntityMappingExtensions
    {
        public static ModelBuilder BuildEntity<T, TMap>(this ModelBuilder builder) where TMap : IEntityMappingConfiguration<T>
                                                                                   where T : class
        {
            var mapper = (TMap)Activator.CreateInstance(typeof(TMap));
            mapper.Map(builder.Entity<T>());
            return builder;
        }
    }
}
