using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TestMessageHub.Converters;
using TestMessageHub.Interfaces;
using TestMessageHub.Models;
using TestMessageHub.Services;
using TestMessageHub.Validations;
using Microsoft.OpenApi.Models;
using System;

namespace TestMessageHub
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
            // Add automapper with assembly mappings search
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddControllers()
                .AddNewtonsoftJson(
                    o =>
                    {
                        o.SerializerSettings.DateParseHandling = DateParseHandling.None;
                        o.SerializerSettings.Converters.Add(new JSONMessageConverter());
                        o.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                        o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                        o.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ";
                    }
                )
                .AddControllersAsServices()
                .AddFluentValidation(fv => fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false);

            // Add MessageBase validator
            services.AddTransient<IValidator<MessageBase>, MessageBaseValidator>();
            services.AddTransient<IValidator<AdidasMessage>, MessageBaseValidator>();
            services.AddTransient<IValidator<NikeMessage>, MessageBaseValidator>();
            services.AddTransient<IValidator<PumaMessage>, MessageBaseValidator>();

            // Add messages DB service
            services.AddTransient<IDBMessagesService, DBMessagesService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test Message HUB",
                    Description = "Simple test message hub on ASP.NET Core Web API. It can be used for ADIDAS, PUMA and NIKE companies.",
                    Contact = new OpenApiContact
                    {
                        Name = "Tkachenko Sergey",
                        Email = "zevsariy.app@gmail.com",
                        Url = new Uri("https://vk.com/tkachenko_s_y")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "You can copy this project without any notification.",
                        Url = new Uri("https://github.com/zevsariy"),
                    }
                });
                c.UseOneOfForPolymorphism();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test Message HUB API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
