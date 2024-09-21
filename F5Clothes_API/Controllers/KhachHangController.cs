using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachhangRepo _KhachHangRepo;
        private readonly IMapper _mapper;

        public KhachHangController(IKhachhangRepo khRepo, IMapper mapper)
        {
            _KhachHangRepo = khRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetAllKh()
        {
            var KhachHangList = await _KhachHangRepo.GetAllKhachHang();
            var mappeKh = _mapper.Map<List<KhachHangDtos>>(KhachHangList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeKh);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByKhachhang(Guid id)
        {


            var mappeKh = _mapper.Map<KhachHangDtos>(await _KhachHangRepo.GetByKhachHang(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeKh);
        }

        [HttpPost]
        public async Task GetAll(KhachHang Kh)
        {
            await _KhachHangRepo.AddKh(Kh);
        }

        [HttpPut]
        public async Task Update(KhachHang Kh)
        {
            await _KhachHangRepo.UpdateKh(Kh);
        }

        
    }
}
