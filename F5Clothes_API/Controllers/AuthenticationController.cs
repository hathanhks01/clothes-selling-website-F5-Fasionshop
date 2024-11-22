using F5Clothes_DAL.Models.system;
using F5Clothes_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using F5Clothes_Services.IServices;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Reponsitories;
using Microsoft.AspNetCore.Identity.Data;
using F5Clothes_DAL.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _auth;
        public AuthenticationController(IAuthenticationServices auth)
        {
            _auth = auth;
        }
        [HttpPost("register")]
       public async Task<IActionResult> Register([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var (registeredUser, token) = await _auth.Register(customer);
                return Ok(new
                {
                    User = registeredUser,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
		[HttpPost("register/NhanVien")]     
		public async Task<IActionResult> RegisterNhanVien([FromBody] NhanVienDtos nhanVienDtos)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var (registeredNhanVien, token) = await _auth.ResisterNhanVien(nhanVienDtos);
				return Ok(new
				{
					User = registeredNhanVien,
					Token = token
				});
			}
			catch (Exception ex)
			{
				return Conflict(ex.Message);
			}
		}
		[HttpPost("login")]
        public async Task<IActionResult> Login( string Username, string Password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _auth.Login(Username, Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[HttpPost("nhanvien/login")]
		public async Task<IActionResult> LoginNhanVien(string Username, string Password)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var result = await _auth.LoginNhanVien(Username, Password);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
