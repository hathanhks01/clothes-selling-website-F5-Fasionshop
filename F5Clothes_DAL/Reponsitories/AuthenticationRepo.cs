using Azure.Core;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Models.system;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly DbduAnTnContext _context;
        private readonly JWTSetting _jwtSettings;
        public AuthenticationRepo(DbduAnTnContext context, JWTSetting jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        // Hàm đăng nhập khách hàng
        public async Task<object> LoginCustomer(string username, string password)
        {
            var customer = await _context.KhachHangs.FirstOrDefaultAsync(u => u.TaiKhoan == username);
            if (customer != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, customer.MatKhau))
                {
                    var token = GenerateToken(customer, "Customer");
                    return new { user = customer, token };
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

            var KhachHangExist = _context.KhachHangs.FirstOrDefault(x => x.Id == customer.Id);
            KhachHang user;
            if (KhachHangExist == null)
            {
                user = new KhachHang
                {
                    Id = Guid.NewGuid(),
                    MaKh = await GetNewCustomerCode(),
                    HoVaTenKh = customer.HoVaTenKh,
                    NgaySinh = customer.NgaySinh,
                    GioiTinh = customer.GioiTinh,
                    Email = customer.Email,
                    TaiKhoan = customer.TaiKhoan,
                    MatKhau = BCrypt.Net.BCrypt.HashPassword(customer.MatKhau), // Mã hóa mật khẩu
                    SoDienThoai = customer.SoDienThoai
                };
                //if (await _context.KhachHangs.AnyAsync(u => u.HoVaTenKh == customer.HoVaTenKh))
                //    throw new Exception("Username already exists");
                //if (await _context.KhachHangs.AnyAsync(u => u.Email == customer.Email))
                //    throw new Exception("Email already exists");
                //if (await _context.KhachHangs.AnyAsync(u => u.SoDienThoai == customer.SoDienThoai))
                //    throw new Exception("Phone number already exists");
                _context.KhachHangs.Add(user);
            }
            else
            {
                KhachHangExist.MaKh = customer.MaKh;
                KhachHangExist.HoVaTenKh = customer.HoVaTenKh;
                KhachHangExist.NgaySinh = customer.NgaySinh;
                KhachHangExist.GioiTinh = customer.GioiTinh;
                KhachHangExist.Email = customer.Email;
                KhachHangExist.SoDienThoai = customer.SoDienThoai;
                KhachHangExist.TrangThai = customer.TrangThai;


                user = KhachHangExist;
            }
            await _context.SaveChangesAsync();
            var token = GenerateToken(user, "Customer");
            return (user, token);
        }

        // Hàm đăng ký nhân viên
        public async Task<(NhanVien, string)> RegsiterNhanVien(NhanVienDtos nhanVienDtos)
        {
            var ExitsItem = _context.NhanViens.FirstOrDefault(s => s.MaNv == nhanVienDtos.MaNv);
            NhanVien user;
            if (ExitsItem == null)
            {
                user = new NhanVien
                {
                    IdCv = nhanVienDtos.IdCv,
                    Id = Guid.NewGuid(),
                    MaNv = await GetNewNhanVienCode(),
                    HoVaTenNv = nhanVienDtos.HoVaTenNv,
                    GioiTinh = nhanVienDtos.GioiTinh,
                    NgaySinh = nhanVienDtos.NgaySinh,
                    TaiKhoan = nhanVienDtos.TaiKhoan,
                    MatKhau = BCrypt.Net.BCrypt.HashPassword(nhanVienDtos.MatKhau), // Mã hóa mật khẩu
                    SoDienThoai = nhanVienDtos.SoDienThoai,
                    Email = nhanVienDtos.Email,
                    Image = nhanVienDtos.Image,
                    DiaChi = nhanVienDtos.DiaChi,
                    MoTa = nhanVienDtos.MoTa,
                    TrangThai = nhanVienDtos.TrangThai
                };
                _context.NhanViens.Add(user); // Thêm đối tượng vào context
            }
            else
            {
                // Cập nhật thông tin của nhân viên đã tồn tại
                ExitsItem.IdCv = nhanVienDtos.IdCv;
                ExitsItem.HoVaTenNv = nhanVienDtos.HoVaTenNv;
                ExitsItem.GioiTinh = nhanVienDtos.GioiTinh;
                ExitsItem.NgaySinh = nhanVienDtos.NgaySinh;
                ExitsItem.SoDienThoai = nhanVienDtos.SoDienThoai;
                ExitsItem.Email = nhanVienDtos.Email;
                ExitsItem.Image = nhanVienDtos.Image;
                ExitsItem.DiaChi = nhanVienDtos.DiaChi;
                ExitsItem.MoTa = nhanVienDtos.MoTa;
                ExitsItem.TrangThai = nhanVienDtos.TrangThai;

                user = ExitsItem; // Sử dụng nhân viên đã cập nhật
            }
            await _context.SaveChangesAsync();
            var token = GenerateToken(user, "Employee");
            return (user, token);

        }

        // Hàm tạo token cho cả KhachHang và NhanVien
        private string GenerateToken(object user, string? role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            // Kiểm tra nếu user là NhanVien
            if (user is NhanVien nhanVien)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, nhanVien.TaiKhoan),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, nhanVien.Id.ToString()),
                    new Claim("MaNv", nhanVien.MaNv),
                    new Claim("HoVaTenNv", nhanVien.HoVaTenNv),
                    new Claim("TaiKhoan", nhanVien.TaiKhoan),
               
                });
            }
            // Kiểm tra nếu user là KhachHang
            else if (user is KhachHang khachHang)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, khachHang.TaiKhoan),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, khachHang.Id.ToString()),
                    new Claim("MaKh", khachHang.MaKh),
                    new Claim("HoVaTenKh", khachHang.HoVaTenKh),
                    new Claim("TaiKhoan", khachHang.TaiKhoan),
                    new Claim("IdKhachhang", khachHang.Id.ToString()),

                });
            }

            // Thêm role nếu có
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

        // Hàm lấy mã khách hàng mới
        public async Task<string> GetNewCustomerCode()
        {
            var lastCustomerCode = await _context.KhachHangs
                                                 .OrderByDescending(kh => kh.MaKh)
                                                 .Select(kh => kh.MaKh)
                                                 .FirstOrDefaultAsync();

            if (lastCustomerCode == null)
            {
                return "KH01";
            }

            string numberPart = lastCustomerCode.Substring(2);

            if (int.TryParse(numberPart, out int currentNumber))
            {
                int newNumber = currentNumber + 1;
                return "KH" + newNumber.ToString("D2");
            }
            else
            {
                throw new Exception("Invalid customer code format.");
            }
        }

        // Hàm lấy mã nhân viên mới
        public async Task<string> GetNewNhanVienCode()
        {
            var lastEmployeeCode = await _context.NhanViens
                                                 .OrderByDescending(nv => nv.MaNv)
                                                 .Select(nv => nv.MaNv)
                                                 .FirstOrDefaultAsync();

            if (lastEmployeeCode == null)
            {
                return "NV01";
            }

            string numberPart = lastEmployeeCode.Substring(2);

            if (int.TryParse(numberPart, out int currentNumber))
            {
                int newNumber = currentNumber + 1;
                return "NV" + newNumber.ToString("D2");
            }
            else
            {
                throw new Exception("Invalid employee code format.");
            }
        }

        // Hàm đăng nhập nhân viên
        public async Task<object> LoginNhanVien(string username, string password)
        {
            var employee = await _context.NhanViens.FirstOrDefaultAsync(u => u.TaiKhoan == username);
            if (employee != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, employee.MatKhau))
                {
                    var token = GenerateToken(employee, "Employee");
                    return new { user = employee, token };
                }
                else
                {
                    throw new Exception("Invalid password.");
                }
            }
            throw new Exception("User not found.");
        }
    }
}
