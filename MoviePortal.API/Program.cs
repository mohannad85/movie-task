using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using MoviePortal.API.Extensions;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model;
using MoviePortal.ApplicationCore.Service;
using MoviePortal.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
//builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MovieDatabase")));

builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureClients(builder =>
{
    var connectionString = "**";
    builder.AddServiceBusClient(connectionString);
});

builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MovieDatabase")));

builder.Services.AddAuthentication()
    .AddGoogle("google", opt =>
    {
        var googleAuth = configuration.GetSection("Authentication:Google");
        opt.ClientId = googleAuth["ClientId"];
        opt.ClientSecret = googleAuth["ClientSecret"];
        opt.SignInScheme = IdentityConstants.ExternalScheme;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
