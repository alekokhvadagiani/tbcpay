var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICheck, Check>();
builder.Services.AddScoped<IPay, Pay>();

builder.Services.AddTransient<CommandMiddleware>();
builder.Services.AddTransient<ExceptionMiddleware>();

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

builder.Services.AddValidatorsFromAssemblyContaining<BaseRequestValidator>().AddFluentValidationAutoValidation();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "tbcpay", Version = "v1" });
    c.SchemaFilter<EnumSchemaFilter>();
});


builder.Services.AddScoped<ModelStateFilter>();

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tbcpay.api v1"));
}

app.UseMiddleware<CommandMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();