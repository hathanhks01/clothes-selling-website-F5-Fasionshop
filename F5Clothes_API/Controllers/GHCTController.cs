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
    public class GHCTController : ControllerBase
    {
        private readonly IGiohangChiTietRepo _GioHangChiTietRepo;
        private readonly IMapper _mapper;

        public GHCTController(IGiohangChiTietRepo ghctRepo, IMapper mapper)
        {
            _GioHangChiTietRepo = ghctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GioHangChiTiet>>> GetAll()
        {
            var GioHangChiTietList = await _GioHangChiTietRepo.GetAllGHCT();
            var mappeghct = _mapper.Map<List<GioHangChiTietDtos>>(GioHangChiTietList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeghct);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappedghct = _mapper.Map<GioHangChiTietDtos>(await _GioHangChiTietRepo.GetByGHCT(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedghct);
        }

        [HttpPost]
        public async Task GetAll(GioHangChiTiet dm)
        {
            await _GioHangChiTietRepo.AddGhct(dm);
        }

        [HttpPut]
        public async Task Update(GioHangChiTiet dm)
        {
            await _GioHangChiTietRepo.UpdateGhct(dm);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _GioHangChiTietRepo.DeleteGhct(id);
        }
    }
}
