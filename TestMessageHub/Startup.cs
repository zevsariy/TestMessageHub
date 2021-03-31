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
                    }
                )
                .AddControllersAsServices()
                .AddFluentValidation(fv => fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false);

            // Add MessageBase validator
            services.AddTransient<IValidator<MessageBase>, MessageBaseValidator>();

            // Add messages DB service
            services.AddTransient<IDBMessagesService, DBMessagesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
