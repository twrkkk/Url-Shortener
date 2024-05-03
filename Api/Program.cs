using Microsoft.EntityFrameworkCore;
using NetSchool.Api.Configuration;
using Url_Shortener.Data.Context;
using Url_Shortener.Data.DTO;
using Url_Shortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

//app.MapFallback(async (IDbContextFactory<MainDbContext> IDbContextFactory, HttpContext context, IUrlService urlService) =>
//{
//    var path = context.Request.Path.ToUriComponent().Trim('/');
//    using var db = IDbContextFactory.CreateDbContext();
//    var result = await db.Urls.FirstOrDefaultAsync(x => x.ShortUrl == path);

//    if (string.IsNullOrEmpty(result.Url))
//        return Results.BadRequest();

//    return Results.Redirect(result.Url);
//});

app.Run();
