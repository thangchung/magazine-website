using System;
using System.Reflection;
using Autofac;
using Cik.CoreLibs;
using Cik.CoreLibs.Domain;
using Cik.CoreLibs.Extensions;
using Cik.Services.Magazine.MagazineService.Api.Category;
using Cik.Services.Magazine.MagazineService.Api.Category.Commands;
using Cik.Services.Magazine.MagazineService.Api.Category.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.Services.Magazine.MagazineService.Infrastruture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer AddWebHost(this IServiceCollection services, IConfigurationRoot configuration)
        {
            Guard.NotNull(services);
            Guard.NotNull(configuration);

            return services
                .AddAuthorization()
                .AddCoreServiceCollection(configuration, s => s.AddServiceCollection(configuration))
                .AddCoreAutofacDependencies(s => s.AddAutofacDependencies(configuration))
                .Build();
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            var guestPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("scope", "data_category_records")
                .Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("data_category_records_admin",
                    policyAdmin => { policyAdmin.RequireClaim("role", "data_category_records_admin"); });
                options.AddPolicy("data_category_records_user",
                    policyUser => { policyUser.RequireClaim("role", "data_category_records_user"); });
            });

            // services.AddMvc(options => { options.Filters.Add(new AuthorizeFilter(guestPolicy)); });
            return services;
        }

        private static void AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            if (!configuration["RunInMemory"].ToBoolean())
            {
                // Use a PostgreSQL database
                var sqlConnectionString = configuration["DataAccessPostgreSqlProvider:ConnectionString"];
                services.AddDbContext<MagazineDbContext>(options =>
                    options.UseNpgsql(
                        sqlConnectionString,
                        b => b.MigrationsAssembly("Cik.Services.Magazine.MagazineService")
                        ));
            }
            else
            {
                // Use a InMemory database
                services.AddDbContext<MagazineDbContext>(options =>
                    options.UseInMemoryDatabase()
                    );
            }
        }

        private static void AddAutofacDependencies(this ContainerBuilder builder, IConfiguration configuration)
        {
            // Autofac container
            builder.RegisterType<MagazineDbContext>().AsSelf().SingleInstance();
            builder.Register(x => x.Resolve<MagazineDbContext>()).As<DbContext>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<CategoryRepository>().As<IRepository<Category, Guid>>().InstancePerLifetimeScope();
            builder.AddCqrsBus(
                typeof (CreateCategoryCommand).GetTypeInfo().Assembly, 
                configuration, 
                "magazine.command.exchange", 
                "magazine.event.exchange", 
                "magazine.command.queue", 
                "magazine.event.queue");
        }
    }
}