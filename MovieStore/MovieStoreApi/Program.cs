using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Model;
using MovieStore.Infrastructure;
using MovieStoreApi.Repositories;
using MovieStoreApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MovieStoreContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Movie>, MovieRepository>();
builder.Services.AddScoped<IRepository<PurchasedMovie>, PurchasedMovieRepository>();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run("http://localhost:8085");