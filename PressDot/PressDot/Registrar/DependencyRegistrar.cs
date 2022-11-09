using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PressDot.Core.Configuration;
using PressDot.Core.Data;
using PressDot.Core.Infrastructure.Mapper;
using PressDot.Data;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;
using System;
using System.Linq;

namespace PressDot.Registrar
{
    public static class DependencyRegistrar
    {

        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var pressDotConfig = configuration.GetSection("PressDotConfigurations").Get<PressDotConfig>();
            services.AddSingleton(pressDotConfig);

            // Register database
            var optionsBuilder = new DbContextOptionsBuilder<PressDotContext>();
            optionsBuilder.UseSqlServer(pressDotConfig.DataConnectionString);
            services.AddScoped<IDbContext>(context => new PressDotContext(optionsBuilder.Options));

            // Register Repository
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Register Services
            services.Scan(scan => scan
                .FromAssemblyOf<IUsersService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Register Facade
            services.Scan(scan => scan
                .FromAssemblyOf<IUsersFacade>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Facade")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            //find mapper configurations provided by other assemblies
            var type = typeof(IOrderedMapperProfile);
            var mapperConfigurations = AppDomain.CurrentDomain.GetAssemblies()
                .Where(s => s.FullName.StartsWith("PressDot"))
                .SelectMany(s => s.GetTypes().Where(x => x.IsClass && !x.IsAbstract))
                .Where(p => type.IsAssignableFrom(p));
            //create and sort instances of mapper configurations
            var instances = mapperConfigurations
                .Select(mapperConfiguration => (IOrderedMapperProfile)Activator.CreateInstance(mapperConfiguration))
                .OrderBy(mapperConfiguration => mapperConfiguration.Order);

            //create AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }

            });

            //register
            AutoMapperConfiguration.Init(config);

            return services;
        }

    }
}
