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
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucRepo _DanhMucRepo;
        private readonly IMapper _mapper;

        public DanhMucController(IDanhMucRepo dmRepo, IMapper mapper)
        {
            _DanhMucRepo = dmRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhMuc>>> GetAll()
        {
            var DanhMucList = await _DanhMucRepo.GetAllDanhMuc();
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


            var mappedDm = _mapper.Map<DanhMucDtos>(await _DanhMucRepo.GetByDanhMuc(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedDm);
        }

        [HttpPost]
        public async Task GetAll(DanhMuc dm)
        {
            await _DanhMucRepo.AddDm(dm);
        }

        [HttpPut]
        public async Task Update(DanhMuc dm)
        {
            await _DanhMucRepo.UpdateDm(dm);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _DanhMucRepo.DeleteDm(id);
        }
    }
}
