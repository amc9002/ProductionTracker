using Microsoft.EntityFrameworkCore;

using ProductionTracker.Api.Bootstrap;
using ProductionTracker.Application;
using ProductionTracker.Domain;
using ProductionTracker.Infrastructure;

using System;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Basic Swagger setup
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Production Tracker API", Version = "v1" });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var catalog = new InMemoryCatalog();
builder.Services.AddSingleton(catalog);
builder.Services.AddSingleton<Inventory>();

builder.Services.AddScoped<OrderApplicationService>();
builder.Services.AddScoped<CatalogApplicationService>();
builder.Services.AddScoped<InventoryApplicationService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


builder.Services.AddSingleton(catalog);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var bootstrapper = new DemoCatalogBootstrapper();
    bootstrapper.Initialize(catalog);
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
