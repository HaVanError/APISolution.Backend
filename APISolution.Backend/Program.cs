using APISolution.Database.DatabaseContext;
using APISoluton.Application.Helper;
using APISoluton.Application.Interface.Login.IAuthentication;
using APISoluton.Application.Interface.Role.Commands;
using APISoluton.Application.Interface.Role.Queries;
using APISoluton.Application.Interface.User.Commands;
using APISoluton.Application.Interface.User.Queries;
using APISoluton.Application.MappeerConfiguration;
using APISoluton.Application.Service.Login;
using APISoluton.Application.Service.Role;
using APISoluton.Application.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("Jwt:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddDbContext<DdConnect>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("db")));
#region Add DI SERVICE
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IUsers, UserService>();
builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<IRoles, RoleService>();
builder.Services.AddScoped<ILogin, LoginService>();

#endregion
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.Configure<Appsetting>(builder.Configuration.GetSection("Jwt:Secret"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
