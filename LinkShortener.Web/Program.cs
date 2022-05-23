using FluentValidation;
using LinkShortener.Dal;
using LinkShortener.Dal.Repositories.Abstractions;
using LinkShortener.Dal.Repositories.Implementations;
using LinkShortener.Mediatr.Pipelines;
using LinkShortener.Services.Abstractions;
using LinkShortener.Services.Implementations;
using LinkShortener.Web.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.Newtonsoft;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration as IConfiguration;

//Redis
builder.Services.AddSingleton(x => new RedisConfiguration()
{
    ConnectionString = configuration.GetValue<string>("RedisConnectionString")
});
builder.Services.AddSingleton<ISerializer, NewtonsoftSerializer>();
builder.Services.AddSingleton<IRedisConnectionPoolManager, RedisConnectionPoolManager>();
builder.Services.AddScoped<IRedisClient, RedisClient>();

//DbContext
builder.Services.AddDbContext<DatabaseContext>((x) =>
{
    string connectionStr = configuration.GetValue<string>("MySqlConnectionString");

    x.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr));
});

//Validators
builder.Services.AddValidatorsFromAssembly(typeof(LinkShortener.Mediatr.IAssemblyMarker).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<ILinkShortenService, LinkShortenService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ILinksRepository, LinksRepository>();

builder.Services.AddAutoMapper(typeof(LinkShortener.Dal.IAssemblyMarker), typeof(LinkShortener.Mediatr.IAssemblyMarker));
builder.Services.AddMediatR(typeof(LinkShortener.Mediatr.IAssemblyMarker));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DatabaseContext>()
        .Database.Migrate();
}

app.UseRouting();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
