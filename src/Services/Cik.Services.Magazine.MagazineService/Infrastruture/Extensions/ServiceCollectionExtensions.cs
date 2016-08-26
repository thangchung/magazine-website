using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Cik.CoreLibs;
using Cik.CoreLibs.Bus;
using Cik.CoreLibs.Bus.Amqp;
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
                .AddCoreAutofacDependencies(s => s.AddAutofacDependencies())
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

        private static void AddAutofacDependencies(this ContainerBuilder builder)
        {
            // Autofac container
            builder.RegisterType<MagazineDbContext>().AsSelf().SingleInstance();
            builder.RegisterType<CategoryRepository>().As<IRepository<Category, Guid>>().InstancePerLifetimeScope();

            // TODO: will refactor later
            var uri = "amqp://root:root@192.168.99.100:5672/%2Fmagazine";
            builder.RegisterInstance(new RabbitMqPublisher(uri, "magazine.command.exchange")).As<ICommandBus>().SingleInstance();
            builder.RegisterAssemblyTypes(typeof (CreateCategoryCommand).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterInstance(new RabbitMqSubscriber(uri, "magazine.command.exchange", "command.queue")).As<IMessageSubscriber>(); 
            builder.Register(x =>
                new CommandConsumer(
                    x.Resolve<IMessageSubscriber>(),
                    (IEnumerable<ICommandHandler>)x.Resolve(typeof(IEnumerable<ICommandHandler>))
                )
            ).As<ICommandConsumer>();
        }
    }
}