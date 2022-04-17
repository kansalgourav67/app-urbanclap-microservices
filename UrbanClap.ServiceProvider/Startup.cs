using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanClap.ServiceProvider.Consumers;
using UrbanClap.ServiceProvider.Repositories;
using UrbanClap.ServiceProvider.Services;

namespace UrbanClap.ServiceProvider
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UrbanClap.ServiceProvider", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IServiceProviderRepository, ServiceProviderRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IMessageBus, Services.MessageBus>();
            services.AddScoped<AssignServiceBookingConsumer>();

            services.AddDiscoveryClient(Configuration);
            services.AddHealthChecks();
            services.AddSingleton<IHealthCheckHandler, ScopedEurekaHealthCheckHandler>();

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<AssignServiceBookingConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    //cfg.Host(new Uri("amqps://hehosepe:9HSOCfXSR7gUiuqrhtLnfLQhJGtTGpMI@lionfish.rmq.cloudamqp.com/hehosepe"));
                    cfg.Host(new Uri($"rabbitmq://{Configuration["RabbitMQHostName"]}"), hostConfig => {
                        hostConfig.Username("guest");
                        hostConfig.Password("guest");
                    });

                    cfg.ReceiveEndpoint("booking-assignment", c =>
                    {
                        c.ConfigureConsumer<AssignServiceBookingConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrbanClap.ServiceProvider v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDiscoveryClient();
        }
    }
}
