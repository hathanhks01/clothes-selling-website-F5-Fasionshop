using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using F5Clothes_Services.IServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDMService _DanhMucSer;
        private readonly IMapper _mapper;

        public DanhMucController(IDMService DanhMucSer, IMapper mapper)
        {
            _DanhMucSer = DanhMucSer;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhMuc>>> GetAll()
        {
            var DanhMucList = await _DanhMucSer.GetAllDanhMuc();
            var mappeddm = _mapper.Map<List<DanhMucDtos>>(DanhMucList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeddm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappedDm = _mapper.Map<DanhMucDtos>(await _DanhMucSer.GetByDanhMuc(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedDm);
        }

        [HttpPost]
        public async Task GetAll(DanhMuc dm)
        {
            await _DanhMucSer.AddDm(dm);
        }

        [HttpPut]
        public async Task Update(DanhMuc dm)
        {
            await _DanhMucSer.UpdateDm(dm);
        }

        
    }
}
