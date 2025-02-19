using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Lego.Api;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/legoApi.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<LegosDataStore>();

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.ReportApiVersions = true;
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
}).AddMvc().AddApiExplorer(setupAction =>
{
    setupAction.SubstituteApiVersionInUrl = true;
});

var apiVersionDescriptionProvider =
    builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen(setupAction =>
{
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        setupAction.SwaggerDoc(
            $"{description.GroupName}",
            new()
            {
                Title = "Lego API",
                Version = description.ApiVersion.ToString(),
                Description = "Through this API you can access my Lego collection and the information of the individual Lego sets."
            });
    }

    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            setupAction.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
