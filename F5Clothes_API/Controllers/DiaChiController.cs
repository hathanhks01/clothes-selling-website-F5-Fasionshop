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
    public class DiaChiController : ControllerBase
    {
        private readonly IDiaChiRepo _DiaChiRepo;
        private readonly IMapper _mapper;

        public DiaChiController(IDiaChiRepo dcRepo, IMapper mapper)
        {
            _DiaChiRepo = dcRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaChi>>> GetAll()
        {
            var DiaChiList = await _DiaChiRepo.GetAllDiaChi();
            var mappeddc = _mapper.Map<List<DiaChiDtos>>(DiaChiList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeddc);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappeddc = _mapper.Map<DiaChiDtos>(await _DiaChiRepo.GetByDiaChi(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeddc);
        }

        [HttpPost]
        public async Task GetAll(DiaChi dc)
        {
            await _DiaChiRepo.Adddc(dc);
        }

        [HttpPut]
        public async Task Update(DiaChi dc)
        {
            await _DiaChiRepo.Updatedc(dc);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _DiaChiRepo.Deletedc(id);
        }
    }
}
