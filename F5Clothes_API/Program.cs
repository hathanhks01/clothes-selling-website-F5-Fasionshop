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
using F5cvothes_DAL.Reponsitories;
using F5Clothes_DAL.DTOs;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Cấu hình dịch vụ DbContext với chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<DbduAnTnContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

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
builder.Services.AddScoped<IChatLieuRepo, ChatLieuRepo>();
builder.Services.AddScoped<IChucVuRepo, ChucVuRepo >();
builder.Services.AddScoped<IDanhMucRepo, DanhMucRepo>();
builder.Services.AddScoped<IDiaChiRepo, DiaChiRepo>();
builder.Services.AddScoped<IGiamGiaRepo, GiamGiaRepo>();
builder.Services.AddScoped<IGiohangChiTietRepo, GHCTRepo>();
builder.Services.AddScoped<IGioHangRepo, GiohangRepo>();
builder.Services.AddScoped<IHDCTRepo,HDCTRepo>();
builder.Services.AddScoped<IHoaDonRepo, HoaDonRepo >();
builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<IKhachhangRepo, KhachhangRepo>();
builder.Services.AddScoped<ILSHDRepo, LSHDRepo>();
builder.Services.AddScoped<IMauSacRepo, MauSacRepo>();
builder.Services.AddScoped<INhanVienRepo, NhanVienRepo>();
builder.Services.AddScoped<IRefshTokenRepo, RefeshTokenRepo>();
builder.Services.AddScoped<ISanPhamRepo, SanPhamRepo>();
builder.Services.AddScoped<ISizeRepo, SizeRepo>();
builder.Services.AddScoped<ISPCTRepo, SPCTRepo>();
builder.Services.AddScoped<IThuongHieuRepo, ThuongHieuRepo>();
builder.Services.AddScoped<IVoucherRepo, VoucherRepo>();
builder.Services.AddScoped<IXuatXuRepo, XuatXuRepo>();
builder.Services.AddScoped<IHinhThucThanhToanRepo, HinhThucThanhToanRepo>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IVNPayService,VNPayService>();
builder.Services.AddScoped<VNPay>(); // Register the VNPay service


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
