using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xo_Test.Interfaces;
using Xo_Test.Models;
using Xo_Test.Repositories;

//docker pull mcr.microsoft.com/mssql/server:2022-latest
//docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=xo_test1234" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest 

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

builder.Services.AddDbContext<Xo_TestContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
