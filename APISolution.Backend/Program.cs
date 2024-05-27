using APISolution.Database.DatabaseContext;
using APISoluton.Application.Interface.Role.Commands;
using APISoluton.Application.Interface.Role.Queries;
using APISoluton.Application.Interface.User.Commands;
using APISoluton.Application.Interface.User.Queries;
using APISoluton.Application.MappeerConfiguration;
using APISoluton.Application.Service.Role;
using APISoluton.Application.Service.User;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DdConnect>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("db")));
#region Add DI SERVICE
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IUsers, UserService>();
builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<IRoles, RoleService>();
#endregion
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
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

app.Run();
