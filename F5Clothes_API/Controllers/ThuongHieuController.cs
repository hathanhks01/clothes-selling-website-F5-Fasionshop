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
    public class ThuongHieuController : ControllerBase
    {
        private readonly IThuongHieuRepo _ThuongHieuRepo;
        private readonly IMapper _mapper;

        public ThuongHieuController(IThuongHieuRepo ThRepo, IMapper mapper)
        {
            _ThuongHieuRepo = ThRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThuongHieu>>> GetAllTh()
        {
            var ThuongHieuList = await _ThuongHieuRepo.GetAllThuongHieu();
            var mappeTh = _mapper.Map<List<ThuongHieuDtos>>(ThuongHieuList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeTh);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByThuongHieu(Guid id)
        {


            var mappeTh = _mapper.Map<ThuongHieuDtos>(await _ThuongHieuRepo.GetByThuongHieu(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeTh);
        }

        [HttpPost]
        public async Task GetAll(ThuongHieu Th)
        {
            await _ThuongHieuRepo.AddTh(Th);
        }

        [HttpPut]
        public async Task Update(ThuongHieu Th)
        {
            await _ThuongHieuRepo.UpdateTh(Th);
        }
    }
}
