using System;
using FanOfTheVan.Services;
using FanOfTheVan.Services.Implementation.Repositories;
using FanOfTheVan.Services.Implementation.Repositories.Implementations;
using FanOfTheVan.Services.Implementation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FanOfTheVan.IOC
{
    public class ServicesRegistrar
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IMarketService, MarketService>();
            services.AddTransient<IMarketRepository, MongoRepository>();
        }
    }
}
