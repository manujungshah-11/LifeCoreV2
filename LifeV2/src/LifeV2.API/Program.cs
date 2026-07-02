using LifeV2.API.Middleware;
using LifeV2.Application;
using LifeV2.Infrastructure;
using LifeV2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "mssql",
        tags: new[] { "ready" });

builder.Services.AddCors(options =>
    options.AddPolicy("Default", p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()));

var app = builder.Build();

// ---- Pipeline ----
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Auto-apply migrations in Development for a frictionless first run.
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseCors("Default");
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();

// Exposed so integration tests can reference the entry point.
public partial class Program { }
