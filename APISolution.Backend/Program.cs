using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Helper;
using APISoluton.Application.Interface.IUsers.Queries;
using APISoluton.Application.Interface.Login.IAuthentication;
using APISoluton.Application.Interface.IUsers.Commands;
using APISoluton.Application.MappeerConfiguration;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Application.Service.LoaiPhongServices;
using APISoluton.Application.Service.Login;
using APISoluton.Application.Service.PhieuDatPhongServices;
using APISoluton.Application.Service.PhongServices;
using APISoluton.Application.Service.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Windows.Input;
using APISolution.Database.Stored_Procedure;
using APISoluton.Database.Stored_Procedure;
using APISoluton.Application.Interface.IRole.Commands;
using APISoluton.Application.Interface.IRole.Queries;
using APISoluton.Application.Service.RoleServices;
using APISoluton.Application.Service.DichVuServices;
using APISoluton.Application.Interface.IPhieuDatPhong.Queries;
using APISoluton.Application.Interface.IPhieuDatPhong.Commands;
using APISoluton.Application.Interface.IPhieuDatDichVu.Commands;
using APISoluton.Application.Interface.IPhieuDatDichVu.Queries;
using APISoluton.Application.Service.PhieuDatDichVuServices;
using Microsoft.Extensions.Options;
using APISoluton.Application.Interface.IDichVu.Commands;
using APISoluton.Application.Interface.IDichVu.Queries;
using APISoluton.Application.Interface.IPhong.Commands;
using APISoluton.Application.Interface.IPhong.Queries;
using APISoluton.Application.Interface.ILoaiPhong.Queries;
using APISoluton.Application.Interface.ILoaiPhong.Commands;
using APISoluton.Application.Interface.IThanhToan.Commands;
using APISoluton.Application.Service.ThanhToanServices;
using APISoluton.Application.Interface.IThanhToan.Queries;


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
//builder.Services.AddDbContext<DdConnect>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddDbContext<DdConnect>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("db")));
#region Add DI SERVICE
builder.Services.AddScoped<IUserCommaind, UserService>();
builder.Services.AddScoped<IUsersQueries, UserService>();
builder.Services.AddScoped<IRoleCommand, RoleService>();
builder.Services.AddScoped<IRolesQueries, RoleService>();
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IPhongCommand, PhongService>();
builder.Services.AddScoped<IPhongQueries, PhongService>();
builder.Services.AddScoped<ILoaiPhongQueries, LoaiPhongService>();
builder.Services.AddScoped<ILoaiPhongCommand, LoaiPhongService>();
builder.Services.AddScoped<IPhieuDatPhongQueries, PhieuDatPhongService>();
builder.Services.AddScoped<IPhieuDatPhongCommand, PhieuDatPhongService>();
builder.Services.AddScoped<IDichVuCommand, DichVuService>();
builder.Services.AddScoped<IDichVuQueries, DichVuService>();
builder.Services.AddScoped<IPhieuDatDichVuCommand, PhieuDatDichVuService>();
builder.Services.AddScoped<IPhieuDatDichVuQueries, PhieuDatDichVuService>();
builder.Services.AddScoped<IThanhToanCommand, ThanhToanService>();
builder.Services.AddScoped<IThanhToanQueries, ThanhToanService>();
//Cache
builder.Services.AddMemoryCache(); // thêm cache 
builder.Services.AddSingleton<CacheServices>();
builder.Services.AddScoped<ProcedureUser>();
builder.Services.AddScoped<ProcedurePhong>();
builder.Services.AddScoped<ProcedureLoaiPhong>();
builder.Services.AddScoped<ProcedureDichVu>();
builder.Services.AddScoped<ProcedureRole>();
builder.Services.AddScoped<ProcedurePhieuDatDichVu>();
builder.Services.AddScoped<ProcedurePhieuDatPhong>();
builder.Services.AddScoped<ProcedureThanhToan>();

#endregion
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.Configure<Appsetting>(builder.Configuration.GetSection("Jwt:Secret"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("api",
        builder =>
        {
            builder
                .AllowAnyOrigin() // Cho phép tất cả các origin (domain)
                .AllowAnyMethod() // Cho phép tất cả các phương thức HTTP
                .AllowAnyHeader(); // Cho phép tất cả các header
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("api");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
