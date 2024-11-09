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
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuRepo _cvRepo;
        private readonly IMapper _mapper;

        public ChucVuController(IChucVuRepo cvRepo, IMapper mapper)
        {
            _cvRepo = cvRepo;
            _mapper = mapper;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChuVuDtos>>> GetAll()
        {
            var cVList = await _cvRepo.GetAllChucVu();
            var mappedCv = _mapper.Map<List<ChuVuDtos>>(cVList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappedCv);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCV(int id)
        {


            var mappedCV = _mapper.Map<ChuVuDtos>(await _cvRepo.GetByChucVu(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedCV);
        }

        [HttpPost]
        public async Task GetAll(ChucVu cv)
        {
            await _cvRepo.AddCv(cv);
        }

        [HttpPut]
        public async Task Update(ChucVu cv)
        {
            await _cvRepo.UpdateCv(cv);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cvRepo.DeleteCv(id);
        }
    }
}
