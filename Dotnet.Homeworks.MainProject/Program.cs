using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.DataAccess;
using Dotnet.Homeworks.Infrastructure;
using Dotnet.Homeworks.MainProject.Configuration;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;
using Dotnet.Homeworks.MainProject.ServicesExtensions.MediatR;
using Dotnet.Homeworks.MainProject.ServicesExtensions.Migrator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddMasstransitRabbitMq(builder.Configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>()!);

builder.Services.AddMediator();

builder.Services.AddDataAccessLayer();
builder.Services.AddInfrastructure();

builder.Services.AddSingleton<IRegistrationService, RegistrationService>();
builder.Services.AddSingleton<ICommunicationService, CommunicationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.DbMigrate();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();