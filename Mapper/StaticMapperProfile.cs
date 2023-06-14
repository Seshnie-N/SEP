using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SEP.Mapper
{
    public static class StaticMapperProfile
    {
        public static void AddCustomConfiguredAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
