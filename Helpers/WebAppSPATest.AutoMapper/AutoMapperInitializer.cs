using AutoMapper;
using PWApp.AutoMapper.MappingProfiles;

namespace PWApp.AutoMapper
{
    public static class AutoMapperInitializer
    {
        /// <summary>
        /// Инициализатор автомаппера по профилям сущностей
        /// </summary>
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile(new BalanceProfile());
                config.AddProfile(new BalanceStorageProfile());
                config.AddProfile(new TransactionProfile());
                config.AddProfile(new UserProfile());
            });
        }
    }
}
