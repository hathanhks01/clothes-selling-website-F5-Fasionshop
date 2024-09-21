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
    public class MauSacController : ControllerBase
    {
        private readonly IMauSacRepo _MauSacRepo;
        private readonly IMapper _mapper;

        public MauSacController(IMauSacRepo MsRepo, IMapper mapper)
        {
            _MauSacRepo = MsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MauSac>>> GetAllMs()
        {
            var MauSacList = await _MauSacRepo.GetAllMauSac();
            var mappeMs = _mapper.Map<List<MauSacDtos>>(MauSacList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeMs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByMauSac(Guid id)
        {


            var mappeMs = _mapper.Map<MauSacDtos>(await _MauSacRepo.GetByMauSac(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeMs);
        }

        [HttpPost]
        public async Task GetAll(MauSac Ms)
        {
            await _MauSacRepo.AddMs(Ms);
        }

        [HttpPut]
        public async Task Update(MauSac Ms)
        {
            await _MauSacRepo.UpdateMs(Ms);
        }
    }
}
