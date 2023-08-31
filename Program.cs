using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "XML-Web-API API",
        Description = "An API to work with json/xml.",
        Contact = new OpenApiContact
        {
            Name = "Saran Periyasamy",
            Email = "srisaran246@gmail.com"
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    // Enable "Project properties -> Build -> Output -> Documentation file" check box.
    // Add Warning code: 1591 in "Project properties -> Build -> Errors & warnings -> Suppress specific warnings" text box.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
builder.Services.AddHttpContextAccessor();

// Show all log in console.
builder.Host.UseSerilog((_, config) =>
{
    config.WriteTo.Console().ReadFrom.Configuration(builder.Configuration);
});

//var logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
