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
        public Task<Object>Login(string username, string password);
        public Task<(KhachHang, String)> Register(Customer customer);
    }
}
