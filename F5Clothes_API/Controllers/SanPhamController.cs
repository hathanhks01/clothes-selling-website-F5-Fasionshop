using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamRepo _spRepo;
        private readonly IMapper _mapper;

        public SanPhamController(ISanPhamRepo spRepo, IMapper mapper)
        {
            _spRepo = spRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SanPham>))]
        public async Task<ActionResult<IEnumerable<SanPham>>> GetAllSp()
        {
           
            var mappedSanPhams = _mapper.Map<List<SanPhamDtos>>(await _spRepo.GetAllSanPham());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            return Ok(mappedSanPhams);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SanPham))]  // Fixed the type to a single SanPham
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBySp(string id)
        {
            

            var mappedSanPham = _mapper.Map<SanPhamDtos>(await _spRepo.GetBySanPham(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedSanPham);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateSanPham([FromBody] SanPhamDtos sanPhamCreate)
        {
            if (sanPhamCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the product already exists
            var products = await _spRepo.GetAllSanPham();
            if (products != null)
            {
                ModelState.AddModelError("SanPham", "SanPham already exists");
                return StatusCode(422, ModelState);
            }

            // Map the DTO to the entity
            var mappedProduct = _mapper.Map<SanPham>(sanPhamCreate);

            // Add the mapped product to the database here
            await _spRepo.AddSanPham(mappedProduct);

            return CreatedAtAction(nameof(CreateSanPham), new { id = mappedProduct.Id }, mappedProduct);
        }

    }
}
