using GreenPipes;
using GreenPipes.Util;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using UrbanClap.BookingService.Consumers;
using UrbanClap.BookingService.Repository;
using UrbanClap.BookingService.Services;

namespace UrbanClap.BookingService
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UrbanClap.Booking", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IMessageBus, MessageBus>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddScoped<BookingConfirmationConsumer>();

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<BookingConfirmationConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri("amqps://hehosepe:9HSOCfXSR7gUiuqrhtLnfLQhJGtTGpMI@lionfish.rmq.cloudamqp.com/hehosepe"));

                    cfg.ReceiveEndpoint("booking-confirmation", c =>
                    {
                        c.ConfigureConsumer<BookingConfirmationConsumer>(ctx);
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrbanClap.Booking v1"));
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