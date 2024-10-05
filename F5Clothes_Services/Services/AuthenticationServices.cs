using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Models.system;
using F5Clothes_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IAuthenticationRepo _repo;
        public AuthenticationServices(IAuthenticationRepo repo)
        {
            _repo= repo;
        }
        public async Task<object> Login(string username, string password)
        {
           return await _repo.Login(username, password);
        }

        public async Task<(KhachHang, string)> Register(Customer customer)
        {
           return await _repo.Register(customer);
        }
    }
}
