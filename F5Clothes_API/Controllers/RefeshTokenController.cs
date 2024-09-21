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
    public class RefeshTokenController : ControllerBase
    {
        private readonly IRefshTokenRepo _RefeshTokenRepo;
        private readonly IMapper _mapper;

        public RefeshTokenController(IRefshTokenRepo RtRepo, IMapper mapper)
        {
            _RefeshTokenRepo = RtRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefeshToken>>> GetAllRt()
        {
            var RefeshTokenList = await _RefeshTokenRepo.GetAllRefshToken();
            var mappeRt = _mapper.Map<List<RefeshTokenDtos>>(RefeshTokenList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeRt);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByRefeshToken(int id)
        {


            var mappeRt = _mapper.Map<RefeshTokenDtos>(await _RefeshTokenRepo.GetByRefeshToken(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeRt);
        }

        [HttpPost]
        public async Task GetAll(RefeshToken Rt)
        {
            await _RefeshTokenRepo.AddRt(Rt);
        }

        [HttpPut]
        public async Task Update(RefeshToken Rt)
        {
            await _RefeshTokenRepo.UpdateRt(Rt);
        }
    }
}
