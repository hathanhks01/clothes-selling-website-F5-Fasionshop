using F5Clothes_DAL.Models.system;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F5Clothes_DAL.DTOs;

namespace F5Clothes_Services.IServices
{
    public interface IAuthenticationServices
    {
        public Task<Object> Login(string username, string password);
		public Task<Object> LoginNhanVien(string username, string password);
		public Task<(KhachHang, String)> Register(Customer customer);
        public Task<(NhanVien, String)> ResisterNhanVien(NhanVienDtos nhanVienDtos);
    }
}
