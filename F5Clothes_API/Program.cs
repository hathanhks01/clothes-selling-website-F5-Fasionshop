using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models.system;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Reponsitories;
using F5Clothes_Services.IServices;
using F5Clothes_Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ DbContext với chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<DbduAnTnContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình dịch vụ JWTSettings từ appsettings.json
builder.Services.Configure<JWTSetting>(builder.Configuration.GetSection("Jwt"));

// Đảm bảo bạn đang sử dụng cùng tên cấu hình
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<JWTSetting>>().Value);

// Cấu hình dịch vụ Authentication với JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JWTSetting>();
    if (jwtSettings == null)
    {
        throw new Exception("JWTSettings configuration section is missing or invalid.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthenticationRepo, AuthenticationRepo>();
builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
