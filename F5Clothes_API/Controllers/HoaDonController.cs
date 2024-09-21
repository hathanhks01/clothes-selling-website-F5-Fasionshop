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
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonRepo _HoaDonRepo;
        private readonly IMapper _mapper;

        public HoaDonController(IHoaDonRepo hdRepo, IMapper mapper)
        {
            _HoaDonRepo = hdRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDon>>> GetAll()
        {
            var HoaDonList = await _HoaDonRepo.GetAllHoaDon();
            var mappehd = _mapper.Map<List<HoaDonDtos>>(HoaDonList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappehd);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappeHdd = _mapper.Map<HoaDonDtos>(await _HoaDonRepo.GetByHoaDon(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeHdd);
        }

        [HttpPost]
        public async Task GetAll(HoaDon Hd)
        {
            await _HoaDonRepo.AddHd(Hd);
        }

        [HttpPut]
        public async Task Update(HoaDon Hd)
        {
            await _HoaDonRepo.UpdateHd(Hd);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _HoaDonRepo.DeleteHd(id);
        }
    }
}
