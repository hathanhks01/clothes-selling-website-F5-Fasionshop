using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Models.system;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IAuthenticationRepo
    {
        public Task<Object>LoginCustomer(string username, string password);
		public Task<Object> LoginNhanVien(string username, string password);
		public Task<(KhachHang, String)> Register(Customer customer);
        public Task<(NhanVien, String)> RegsiterNhanVien(NhanVienDtos nhanVienDtos);
    }
}
