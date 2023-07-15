using Application.Common.Interfaces;
using Infrastructure.Identity.DbContext;
using Infrastructure.Persistence.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Identity.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Infrastructure.Identity.Mapper;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient(provider => new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new DataProfile());
            //}).CreateMapper());
            services.AddAutoMapper(Assembly.Load(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ToDoDbContext).Assembly.FullName)));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ToDoDbContext).Assembly.FullName)));

            services.AddScoped<IToDoDbContext>(provider => provider.GetRequiredService<ToDoDbContext>());
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            return services;
        }
    }
}
