using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using UrbanClap.AdministrationService.Consumers;
using UrbanClap.AdministrationService.Repositories;
using UrbanClap.AdministrationService.Services;

namespace UrbanClap.AdministrationService
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UrbanClap.Administration", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddTransient<IMessageBus, MessageBus>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IMessageBus, MessageBus>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ServiceBookingConfirmationConsumer>();
            services.AddScoped<ServiceBookingRequestConsumer>();

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<ServiceBookingRequestConsumer>();
                config.AddConsumer<ServiceBookingConfirmationConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri("amqps://hehosepe:9HSOCfXSR7gUiuqrhtLnfLQhJGtTGpMI@lionfish.rmq.cloudamqp.com/hehosepe"));

                    cfg.ReceiveEndpoint("booking", c =>
                    {
                        c.ConfigureConsumer<ServiceBookingRequestConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint("booking-confirmation", c =>
                    {
                        c.ConfigureConsumer<ServiceBookingConfirmationConsumer>(ctx);
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrbanClap.Administration v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
