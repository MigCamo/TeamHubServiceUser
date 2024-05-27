using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeamHubServiceUser.Gateways.Interfaces;
using TeamHubServiceUser.Gateways.Providers;
using TeamHubServiceUser.Entities;
using TeamHubServiceUser.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<TeamHubContext>(options => {
    var connectionString = builder.Configuration
                           .GetConnectionString("MySQLCursos")?? "DefaultConnectionString";
    options.UseMySQL(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/TeamHub/Users/", (IUserService userService, StudentDTO newStudent) => 
{
    return userService.AddStudent(newStudent);
})
.WithName("AddUser")
.WithOpenApi();


app.MapPost("/TeamHub/Users/Delete", (IUserService userService, int idDeleteStudent) => 
{
    return userService.DeleteStudent(idDeleteStudent);
})
.WithName("DeleteUser")
.WithOpenApi();

app.MapPost("/TeamHub/Users/Edit", (IUserService userService, StudentDTO editStudent) => 
{
    return userService.EditStudent(editStudent);
})
.WithName("EditeUser")
.WithOpenApi();

app.Run();
