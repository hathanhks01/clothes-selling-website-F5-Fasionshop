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
    public class LSHDController : ControllerBase
    {
        private readonly ILSHDRepo _LichSuHoaDonRepo;
        private readonly IMapper _mapper;

        public LSHDController(ILSHDRepo lsRepo, IMapper mapper)
        {
            _LichSuHoaDonRepo = lsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichSuHoaDon>>> GetAllLs()
        {
            var LichSuHoaDonList = await _LichSuHoaDonRepo.GetAllLichSuHoaDon();
            var mappeLs = _mapper.Map<List<LichSuHoaDonDtos>>(LichSuHoaDonList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeLs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByLichSuHoaDon(Guid id)
        {


            var mappeLs = _mapper.Map<LichSuHoaDonDtos>(await _LichSuHoaDonRepo.GetByLichSuHoaDon(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeLs);
        }

        [HttpPost]
        public async Task GetAll(LichSuHoaDon Ls)
        {
            await _LichSuHoaDonRepo.AddLs(Ls);
        }

        [HttpPut]
        public async Task Update(LichSuHoaDon Ls)
        {
            await _LichSuHoaDonRepo.UpdateLs(Ls);
        }
    }
}
