using Application.Contracts.ServiceBus;
using Bus.Consumers;
using Domain.Constants;
using Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bus.Extensions
{
    public static class BusExtension
    {
        public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            services.AddScoped<IServiceBus, ServiceBus>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductAddedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url), h => { });

                    cfg.ReceiveEndpoint(ServiceBusConst.ProductAddedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<ProductAddedEventConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
