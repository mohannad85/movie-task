using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using MoviePortal.API.Extensions;
using MoviePortal.ApplicationCore.Interfaces.Infrastructure;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model;
using MoviePortal.ApplicationCore.Service;
using MoviePortal.Common.AzureStorageServices.Interfaces;
using MoviePortal.Common.AzureStorageServices.Services;
using MoviePortal.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MovieDatabaseConnectionString")));
builder.Services.ConfigureCors();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
builder.Services.AddSingleton<ITableStorageService, TableStorageService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IWatchedMovieService, WatchedMovieService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureClients(builder =>
{
    builder.AddServiceBusClient(configuration.GetSection("ServiceBusOptions:ServiceBusConnectionString"));
});

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
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        dataContext.Database.Migrate();
    }
}

app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
