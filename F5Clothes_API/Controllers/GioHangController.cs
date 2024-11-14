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
    public class GioHangController : ControllerBase
    {
        private readonly IGioHangServices _GioHangSer;
        private readonly IMapper _mapper;

        public GioHangController(IGioHangServices ghSev, IMapper mapper)
        {
            _GioHangSer = ghSev;
            _mapper = mapper;
        }



        [HttpGet("{IdKh:Guid}")]
        public async Task<ActionResult<GiohangDtos>> GetAllGh(Guid idKh)
        {
            try
            {
                // Get all the items from the service
                var ghct = await _GioHangSer.GetAll(idKh);

                // If no items are found, return NoContent status
                if (ghct == null || !ghct.Any())
                {
                    return NoContent();
                }

                // Map the data to DTOs
                var dtos = _mapper.Map<IEnumerable<GiohangDtos>>(ghct);

                // Return the mapped DTOs with an OK status
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // Return 500 error if something goes wrong
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{IDGH:guid}")]
        public async Task<ActionResult<GiohangDtos>> GetItem(Guid cartItemId)
        {
            try
            {
                var cartItem = await _GioHangSer.GetItem(cartItemId);

                if (cartItem == null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<GiohangDtos>(cartItem);
                return Ok(dto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<GiohangDtos>> AddItem([FromBody] GioHangChiTietDtos cartItemToAdd)
        {
            try
            {
                var cartItem = await _GioHangSer.AddItem(cartItemToAdd);

                if (cartItem == null)
                {
                    return NoContent();
                }

                var dto = _mapper.Map<GiohangDtos>(cartItem);
                return CreatedAtAction(nameof(GetItem), new { id = cartItem.Id }, cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{IDGH:guid}")]
        public async Task<ActionResult<GiohangDtos>> RemoveItem(Guid cartItemId)
        {
            try
            {
                var cartItem = await _GioHangSer.RemoveItem(cartItemId);
                if (cartItem == null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<GiohangDtos>(cartItem);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{IDGH:guid}")]
        public async Task<ActionResult<GiohangDtos>> UpdateItem(Guid cartItemId, GioHangUpdate cartItemToUpdate)
        {
            try
            {
                var cartItem = await _GioHangSer.UpdateItem(cartItemId, cartItemToUpdate);
                if (cartItem is null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<GiohangDtos>(cartItem);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
