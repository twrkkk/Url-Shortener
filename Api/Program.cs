using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSchool.Services.Logger;
using Url_Shortener.Configuration;
using Url_Shortener.Data.Context;
using Url_Shortener.Services.Logger;
using Url_Shortener.Services.Settings;
using Url_Shortener.Services.UrlService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logSettings = Settings.Load<LogSettings>("Log");
builder.Services.AddSingleton(logSettings);
builder.AddAppLogger(logSettings);

builder.Services.AddSingleton<IAppLogger, AppLogger>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppCors();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextFactory<MainDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<IUrlService, UrlService>();


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
app.UseAppCors();


//Migrate Db
using var scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();
using var context = dbContextFactory.CreateDbContext();
context.Database.Migrate();

var logger = app.Services.GetRequiredService<IAppLogger>();

logger.Information("The Api has started");

app.Run();

logger.Information("The Api has stopped");
