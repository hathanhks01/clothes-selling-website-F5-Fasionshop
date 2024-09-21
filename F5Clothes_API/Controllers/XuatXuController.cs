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
    public class XuatXuController : ControllerBase
    {
        private readonly IXuatXuRepo _XuatXuRepo;
        private readonly IMapper _mapper;

        public XuatXuController(IXuatXuRepo XxRepo, IMapper mapper)
        {
            _XuatXuRepo = XxRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<XuatXu>>> GetAllXx()
        {
            var XuatXuList = await _XuatXuRepo.GetAllXuatXu();
            var mappeXx = _mapper.Map<List<XuatXuDtos>>(XuatXuList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeXx);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByXuatXu(Guid id)
        {


            var mappeXx = _mapper.Map<XuatXuDtos>(await _XuatXuRepo.GetByXuatXu(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeXx);
        }

        [HttpPost]
        public async Task GetAll(XuatXu Xx)
        {
            await _XuatXuRepo.AddXx(Xx);
        }

        [HttpPut]
        public async Task Update(XuatXu Xx)
        {
            await _XuatXuRepo.UpdateXx(Xx);
        }
    }
}
