using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"Connection string is: {builder.Configuration.GetConnectionString("Psql")}");

builder.Services.AddDbContextFactory<HospitalContext>(options =>
    options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("Psql"),
            x => x.MigrationsAssembly("DAL.Psql.Migrations"))
        //.UseLazyLoadingProxies()
        .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
