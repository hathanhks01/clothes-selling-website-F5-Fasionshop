using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HDCTController : ControllerBase
    {
        private readonly IHDCTRepo _HoaDonChiTietRepo;
        private readonly IMapper _mapper;

        public HDCTController(IHDCTRepo HDCTRepo, IMapper mapper)
        {
            _HoaDonChiTietRepo = HDCTRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDonChiTiet>>> GetAll()
        {
            var HoaDonChiTietList = await _HoaDonChiTietRepo.GetAllHoaDonChiTiet();
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


            var mappedHDCT = _mapper.Map<HoaDonChiTietDtos>(await _HoaDonChiTietRepo.GetByHoaDonChiTiet(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedHDCT);
        }

        [HttpPost]
        public async Task GetAll(HoaDonChiTiet HDCT)
        {
            await _HoaDonChiTietRepo.AddHDCT(HDCT);
        }

        [HttpPut]
        public async Task Update(HoaDonChiTiet HDCT)
        {
            await _HoaDonChiTietRepo.UpdateHDCT(HDCT);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _HoaDonChiTietRepo.DeleteHDCT(id);
        }
    }
}
