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
    public class VouCherController : ControllerBase
    {
        private readonly IVoucherRepo _VouCherRepo;
        private readonly IMapper _mapper;

        public VouCherController(IVoucherRepo VcRepo, IMapper mapper)
        {
            _VouCherRepo = VcRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VouCher>>> GetAllVc()
        {
            var VouCherList = await _VouCherRepo.GetAllVouCher();
            var mappeVc = _mapper.Map<List<VouCherDtos>>(VouCherList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeVc);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByVouCher(Guid id)
        {


            var mappeVc = _mapper.Map<VouCherDtos>(await _VouCherRepo.GetByVouCher(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeVc);
        }

        [HttpPost]
        public async Task GetAll(VouCher Vc)
        {
            await _VouCherRepo.AddVc(Vc);
        }

        [HttpPut]
        public async Task Update(VouCher Vc)
        {
            await _VouCherRepo.UpdateVc(Vc);
        }
    }
}
