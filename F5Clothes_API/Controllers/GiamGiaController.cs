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
    public class GiamGiaController : ControllerBase
    {
        private readonly IGiamGiaRepo _GiamGiaRepo;
        private readonly IMapper _mapper;

        public GiamGiaController(IGiamGiaRepo ggRepo, IMapper mapper)
        {
            _GiamGiaRepo = ggRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiamGium>>> GetAll()
        {
            var GiamGiaList = await _GiamGiaRepo.GetAllGg();
            var mappedgg = _mapper.Map<List<GiamGiaDtos>>(GiamGiaList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappedgg);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappedgg = _mapper.Map<GiamGiaDtos>(await _GiamGiaRepo.GetByGg(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedgg);
        }

        [HttpPost]
        public async Task GetAll(GiamGium gg)
        {
            await _GiamGiaRepo.AddGg(gg);
        }

        [HttpPut]
        public async Task Update(GiamGium gg)
        {
            await _GiamGiaRepo.UpdateGg(gg);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _GiamGiaRepo.DeleteGg(id);
        }
    }
}
