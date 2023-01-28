using System;
using System.Text.Json.Serialization;
using System.Xml;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using tbcpay.services.Dto.ProviderDto.Request;
using tbcpay.services.Extensions;
using tbcpay.services.Helpers;

namespace tbcpay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete("Obsolete")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add(new ProducesAttribute("application/xml"));
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter(new XmlWriterSettings
                    {
                        OmitXmlDeclaration = false
                    }));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddXmlSerializerFormatters()
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<BaseRequestValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "tbcpay", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();   

            });
            services.AddCustomFilters();
            services.AddInterfaces();
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tbcpay.api v1"));
            }

            app.UseMiddlewares();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}