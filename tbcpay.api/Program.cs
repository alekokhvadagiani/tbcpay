using System.Text.Json.Serialization;
using System.Xml;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using tbcpay.services.Extensions;
using tbcpay.services.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/xml"));
        options.OutputFormatters.Add(new XmlSerializerOutputFormatter(new XmlWriterSettings
        {
            OmitXmlDeclaration = false
        }));
    })
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
    .AddXmlSerializerFormatters();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "tbcpay", Version = "v1" });
    c.SchemaFilter<EnumSchemaFilter>();
});
builder.Services.AddCustomFilters();
builder.Services.AddInterfaces();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });


var app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.Run();
