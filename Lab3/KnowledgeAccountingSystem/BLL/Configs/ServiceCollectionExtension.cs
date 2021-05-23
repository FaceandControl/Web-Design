using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Configs
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDALConfig(this IServiceCollection services, string connectionString)
        {
            services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AccountingSystemDbContext>();

            services.AddDbContext<AccountingSystemDbContext>(options =>
                    options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, b => b.MigrationsAssembly("DAL")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagDescriptionRepository, TagDescriptionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
