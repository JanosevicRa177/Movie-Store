using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using MovieStore.Core.Model;
using MovieStore.Infrastructure;
using MovieStoreApi;
using MovieStoreApi.Behaviors;
using MovieStoreApi.Repositories;
using MovieStoreApi.Repositories.Interfaces;
using MovieStoreApi.Service;
using MovieStoreApi.Service.Email;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<MovieStoreContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IRepository<PurchasedMovie>, PurchasedMovieRepository>();

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.Configure<TimeOptions>(builder.Configuration.GetSection(TimeOptions.SectionName));
builder.Services.AddSingleton(cfg => cfg.GetRequiredService<IOptions<TimeOptions>>().Value);
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.SectionName));
builder.Services.AddSingleton(cfg => cfg.GetRequiredService<IOptions<EmailOptions>>().Value);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddOpenApiDocument(cfg =>
{
    cfg.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom
        .Configuration(hostingContext.Configuration);
});

builder.Services.AddHostedService<HostedService>();

var app = builder.Build();

app.UseCors(x => x
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<MovieStoreContext>();

    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

// Configure the HTTP request pipeline.

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Logger.LogInformation("Starting Application");

app.Run("http://localhost:8085");

app.Logger.LogInformation("Application shutdown");