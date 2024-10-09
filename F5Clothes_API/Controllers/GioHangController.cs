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
    public class GioHangController : ControllerBase
    {
        private readonly IGioHangRepo _GioHangRepo;
        private readonly IMapper _mapper;

        public GioHangController(IGioHangRepo ghRepo, IMapper mapper)
        {
            _GioHangRepo = ghRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GioHang>>> GetAll()
        {
            var GioHangList = await _GioHangRepo.GetAllGioHang();
            var mappeddm = _mapper.Map<List<GiohangDtos>>(GioHangList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeddm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappedDm = _mapper.Map<GiohangDtos>(await _GioHangRepo.GetByGioHang(id));  
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedDm);
        }

        [HttpPost]
        public async Task GetAll(GioHang dm)
        {
            await _GioHangRepo.AddGh(dm);
        }

        [HttpPut]
        public async Task Update(GioHang dm)
        {
            await _GioHangRepo.UpdateGh(dm);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _GioHangRepo.DeleteGh(id);
        }
    }
}
