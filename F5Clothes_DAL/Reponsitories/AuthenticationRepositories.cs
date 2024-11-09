using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Models.system;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class AuthenticationRepositories:IAuthenticationRepo
    {
        private readonly DbduAnTnContext _context;
        private readonly JWTSetting _jwtSettings;

        public AuthenticationRepositories(DbduAnTnContext context, JWTSetting jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }
        public async Task<object> Login(string username, string password)
        {
            var customer = await _context.KhachHangs.SingleOrDefaultAsync(u => u.TaiKhoan == username);
            if (customer != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, customer.MatKhau))
                {
                    var token =  GenerateToken(customer.Id, customer.TaiKhoan, "Customer");
                    return new { user = customer, token };
                }
                else
                {
                    throw new Exception("Invalid password.");
                }
            }

            var employee = await _context.NhanViens.SingleOrDefaultAsync(u => u.TaiKhoan == username);
            if (employee != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, employee.MatKhau))
                {
                    var token = GenerateToken(employee.Id, employee.TaiKhoan, "Employee");
                    return new { user = employee, token };
                }
                else
                {
                    throw new Exception("Invalid password.");
                }
            }

            throw new Exception("User not found.");
        }

        public async Task<(KhachHang, string)> Register(Customer customer)
        {
            if (await _context.KhachHangs.AnyAsync(u => u.HoVaTenKh == customer.HoVaTenKh))
                throw new Exception("Username already exists");
            if (await _context.KhachHangs.AnyAsync(u => u.Email == customer.Email))
                throw new Exception("Email already exists");
            if (await _context.KhachHangs.AnyAsync(u => u.SoDienThoai == customer.SoDienThoai))
                throw new Exception("Phone number already exists");

            var user = new KhachHang
            {
                Id = Guid.NewGuid(),
                MaKh = await GetNewCustomerCode(),
                HoVaTenKh = customer.HoVaTenKh,
                Email = customer.Email,
                TaiKhoan=customer.TaiKhoan,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(customer.MatKhau), // Mã hóa mật khẩu
                SoDienThoai = customer.SoDienThoai
            };

            _context.KhachHangs.Add(user);
            await _context.SaveChangesAsync();
            var token = GenerateToken(user.Id, user.HoVaTenKh,"customer");
            return (user, token);
        }
        public string GenerateToken(Guid userId, string username,string? role)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> GetNewCustomerCode()
        {
            // Lấy mã khách hàng lớn nhất từ cơ sở dữ liệu
            var lastCustomerCode = await _context.KhachHangs
                                                 .OrderByDescending(kh => kh.MaKh)
                                                 .Select(kh => kh.MaKh)
                                                 .FirstOrDefaultAsync();

            // Nếu chưa có khách hàng nào, tạo mã đầu tiên
            if (lastCustomerCode == null)
            {
                return "KH01";
            }

            // Tách phần số từ mã khách hàng (bỏ phần 'KH')
            string numberPart = lastCustomerCode.Substring(2);

            // Chuyển phần số sang số nguyên và tăng lên 1
            if (int.TryParse(numberPart, out int currentNumber))
            {
                int newNumber = currentNumber + 1;

                // Đảm bảo định dạng mã mới có ít nhất 2 chữ số (ví dụ KH01, KH002)
                return "KH" + newNumber.ToString("D2");
            }
            else
            {
                throw new Exception("Invalid customer code format.");
            }
        }
    }
}
