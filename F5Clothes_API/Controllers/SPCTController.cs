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
    public class SPCTController : ControllerBase
    {
        private readonly ISPCTRepo _SanPhamChiTietRepo;
        private readonly IMapper _mapper;

        public SPCTController(ISPCTRepo SPCTRepo, IMapper mapper)
        {
            _SanPhamChiTietRepo = SPCTRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPhamChiTiet>>> GetAllSPCT()
        {
            var SanPhamChiTietList = await _SanPhamChiTietRepo.GetAllSanPhamChiTiet();
            var mappeSPCT = _mapper.Map<List<SanPhamChiTietDtos>>(SanPhamChiTietList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeSPCT);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySanPhamChiTiet(Guid id)
        {


            var mappeSPCT = _mapper.Map<SanPhamChiTietDtos>(await _SanPhamChiTietRepo.GetBySanPhamChiTiet(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeSPCT);
        }

        [HttpPost]
        public async Task GetAll(SanPhamChiTiet SPCT)
        {
            await _SanPhamChiTietRepo.AddSPCT(SPCT);
        }

        [HttpPut]
        public async Task Update(SanPhamChiTiet SPCT)
        {
            await _SanPhamChiTietRepo.UpdateSPCT(SPCT);
        }
    }
}
