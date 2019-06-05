using System;
using PWApp.ComponentRegistrar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Classes;

namespace WebAPITest
{
    public class Startup
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Конфигурация сервиса
        /// </summary>
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //inject appsettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.RegisterDependecy(Configuration.GetConnectionString("DBConnection"));
            services.AddMvc()
                .AddMvcOptions(opts =>
                {
                    opts.RespectBrowserAcceptHeader = true;
                    opts.ReturnHttpNotAcceptable = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            //для работы сессии            
            //services.AddDistributedMemoryCache(); //добавляет дефолтную реализацию IDistributedCache.
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1); //сессия на 1 день
                //options.Cookie.HttpOnly = true;
            });

            //JWT Authentication            
            var opt = new AuthOptions() { Key = Configuration["ApplicationSettings:JWT_Secret"].ToString() };
            services.AddAuthentication(i =>
            {
                i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(i =>
            {
                i.RequireHttpsMetadata = false;
                i.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = opt.GetSymmetricSecurityKey(), 
                    ValidateIssuer = true, // укзывает, будет ли валидироваться издатель при валидации токена 
                    ValidIssuer = opt.Issuer, // строка, представляющая издателя
                    ValidateAudience = true, // будет ли валидироваться потребитель токена
                    ValidAudience = opt.Audience,// установка потребителя токена
                    ValidateLifetime = true //будет ли валидироваться время существования
                };
            });

            /*
            var keyFromConf = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(i =>
            {
                i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(i =>
            {
                i.RequireHttpsMetadata = false;
                i.SaveToken = false;
                i.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyFromConf),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });*/



            //разрешает вернуть результат по запросу
            services.AddCors(o => o.AddPolicy("FreePolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*app.UseCors(builder =>
                    builder.WithOrigins(Configuration["ApplicationSettings:ClientUrl"].ToString())
            .AllowAnyHeader()
            .AllowAnyMethod());*/

            app.UseAuthentication();
            app.UseSession(); //юзать UseSession до UseMvc
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}

