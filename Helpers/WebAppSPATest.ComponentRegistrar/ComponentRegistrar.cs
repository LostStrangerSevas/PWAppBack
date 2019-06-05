using BusinessLogic.Implementation.Managers;
using BusinessLogic.Interfaces.IManagers;
using DataAccessLayer.Context;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Implementation.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PWApp.ComponentRegistrar
{
    /// <summary>
    /// Класс регистратора в пределах IoC
    /// </summary>
    public static class ComponentRegistrar
    {
        public static IServiceCollection RegisterDependecy(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MSSQLContext>(options =>
                options.UseSqlServer(connectionString));
            //services.AddIdentityCore<User>
            services.AddIdentity<User, Role>(opts =>
                       {
                           opts.Password.RequiredLength = 5;   // минимальная длина
                            opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                            opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                            opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                            opts.Password.RequireDigit = false; // требуются ли цифры
                        })
                    .AddEntityFrameworkStores<MSSQLContext>(); //добавляет EF-реализацию для хранилища системы идентификации

            services.AddTransient<UserManager<User>>() //представляет API для управления пользователями в постоянном хранилище
                    .AddTransient<SignInManager<User>>() //представляет API для авторизации пользователей

                    .AddTransient<DbContext, MSSQLContext>()

                    .AddScoped<IUserDtoManager, UserDtoManager>()
                    .AddTransient<IBalanceDtoManager, BalanceDtoManager>()
                    .AddTransient<IBalanceStorageDtoManager, BalanceStorageDtoManager>()
                    .AddTransient<ITransactionDtoManager, TransactionDtoManager>()

                    .AddScoped<IUow, Uow>();
            return services;
        }
    }
}
