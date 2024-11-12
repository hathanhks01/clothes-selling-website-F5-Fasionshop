using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HDCTController : ControllerBase
    {
        private readonly IHoaDonChiTietServices _HoaDonChiTietSv;
        private readonly IMapper _mapper;

        public HDCTController(IHoaDonChiTietServices HDCTSv, IMapper mapper)
        {
            _HoaDonChiTietSv = HDCTSv;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDonChiTiet>>> GetAll()
        {
            var HoaDonChiTietList = await _HoaDonChiTietSv.GetAll();
            var mappeHDCT = _mapper.Map<List<HoaDonChiTietDtos>>(HoaDonChiTietList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeHDCT);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {
            var mappedHDCT = _mapper.Map<HoaDonChiTietDtos>(await _HoaDonChiTietSv.GetById(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedHDCT);
        }

        [HttpPost]
        public async Task GetAll(HoaDonChiTiet HDCT)
        {
            await _HoaDonChiTietSv.Create(HDCT);
        }

        [HttpPut]
        public async Task Update(HoaDonChiTiet HDCT)
        {
            await _HoaDonChiTietSv.Update(HDCT);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _HoaDonChiTietSv.Delete(id);
        }
    }
}
