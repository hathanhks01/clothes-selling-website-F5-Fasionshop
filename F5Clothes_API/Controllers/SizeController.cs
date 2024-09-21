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
    public class SizeController : ControllerBase
    {
        private readonly ISizeRepo _SizeRepo;
        private readonly IMapper _mapper;

        public SizeController(ISizeRepo SizeRepo, IMapper mapper)
        {
            _SizeRepo = SizeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size>>> GetAllSize()
        {
            var SizeList = await _SizeRepo.GetAllSize();
            var mappeSize = _mapper.Map<List<SizeDtos>>(SizeList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeSize);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySize(Guid id)
        {


            var mappeSize = _mapper.Map<SizeDtos>(await _SizeRepo.GetBySize(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeSize);
        }

        [HttpPost]
        public async Task GetAll(Size Size)
        {
            await _SizeRepo.AddSz(Size);
        }

        [HttpPut]
        public async Task Update(Size Size)
        {
            await _SizeRepo.UpdateSz(Size);
        }
    }
}
