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
    public class HinhThucThanhToanController : ControllerBase
    {

        private readonly IHinhThucThanhToanRepo _HinhThucThanhToanRepo;
        private readonly IMapper _mapper;

        public HinhThucThanhToanController(IHinhThucThanhToanRepo ghRepo, IMapper mapper)
        {
            _HinhThucThanhToanRepo = ghRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HinhThucThanhToan>>> GetAll()
        {
            var HinhThucThanhToanList = await _HinhThucThanhToanRepo.GetAllHinhThucThanhToan();
            var mappeHtt = _mapper.Map<List<HinhThucThanhToanDtos>>(HinhThucThanhToanList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeHtt);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappedHtt = _mapper.Map<HinhThucThanhToanDtos>(await _HinhThucThanhToanRepo.GetByHinhThucThanhToan(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedHtt);
        }

        [HttpPost]
        public async Task GetAll(HinhThucThanhToan HTTT)
        {
            await _HinhThucThanhToanRepo.AddHTt(HTTT);
        }

        [HttpPut]
        public async Task Update(HinhThucThanhToan HTTT)
        {
            await _HinhThucThanhToanRepo.UpdateHTt(HTTT);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _HinhThucThanhToanRepo.DeleteHTt(id);
        }

    }
}
