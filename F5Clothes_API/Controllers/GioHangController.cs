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



        [HttpGet("all/{idKh}")]
        public async Task<ActionResult<IEnumerable<GiohangDtos>>> GetAllGh(Guid idKh)
        {
            try
            {
                // Fetch the cart items
                var ghct = await _GioHangSer.GetAll(idKh);

                if (ghct == null || !ghct.Any())
                {
                    return NotFound("Không có sản phẩm nào.");
                }

                // Debugging: Log or inspect the content of ghct
                Console.WriteLine($"Found {ghct.Count()} items for User {idKh}");

                // Check each item for null navigation properties
                foreach (var item in ghct)
                {
                    if (item.IdSpctNavigation == null)
                    {
                        // Log the item or user information where the null navigation was found
                        Console.WriteLine($"Item with ID {item.IdGh} has null IdSpctNavigation.");
                        return BadRequest("Dữ liệu liên kết bị thiếu.");
                    }

                    // You can add more checks if needed, for example:
                    if (item.IdSpctNavigation.IdSpNavigation == null)
                    {
                        Console.WriteLine($"Item with ID {item.IdGh} has null IdSpNavigation.");
                        return BadRequest("Dữ liệu sản phẩm bị thiếu.");
                    }
                }

                // Map to DTOs
                var dtos = _mapper.Map<IEnumerable<GiohangDtos>>(ghct);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // Log the exception message for debugging purposes
                Console.WriteLine($"Exception occurred: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi xảy ra: {ex.Message}");
            }
        }


        [HttpGet("single/{cartItemId}")]
        public async Task<ActionResult<GiohangDtos>> GetItem(Guid cartItemId)
        {
            try
            {
                // Gọi service để lấy mục trong giỏ hàng
                var cartItem = await _GioHangSer.GetItem(cartItemId);

                // Kiểm tra nếu không tìm thấy dữ liệu
                if (cartItem == null)
                {
                    return NotFound("Không tìm thấy mục giỏ hàng.");
                }

                // Kiểm tra nếu _mapper chưa được khởi tạo
                if (_mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Cấu hình AutoMapper không hợp lệ.");
                }

                // Ánh xạ sang DTO
                var dto = _mapper.Map<GiohangDtos>(cartItem);

                // Trả về dữ liệu
                return Ok(dto);
            }
            catch (Exception ex)
            {
                // Trả về lỗi nội bộ với thông tin chi tiết
                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi xảy ra: {ex.Message}");
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
        [HttpDelete("{IDGH}")]
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
        [HttpPatch("{IDGH}")]
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
