using FluentValidation;
using Microsoft.EntityFrameworkCore;
using IMAS_API_Example.Shared.Middleware;
using Scalar.AspNetCore;
using IMAS.API.AkaunBelumTerima.Shared.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AkaunBelumTerimaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FinancialDBConnectionString")));


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy => policy.WithOrigins("https://localhost:9004", "http://localhost:5004")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        // Object initializer
        options.Title = "IMAS.LejarAm API";
        options.ShowSidebar = true;
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseCors("AllowBlazorApp");

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();


void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var _DbFinancial = scope.ServiceProvider.GetRequiredService<AkaunBelumTerimaDbContext>();

    _DbFinancial?.Database.Migrate();
}