using AutoMapper;

using F5Clothes_DAL.DTOs;

using F5Clothes_Services.IServices;
using F5Clothes_Services.Services;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GioHangController : ControllerBase
{
    private readonly IGioHangServices _gioHangServices;


    public GioHangController(IGioHangServices ghSev)
    {
        _gioHangServices = ghSev;

    }

    [HttpGet("GetAllGioHang/{idKh}")]
    public async Task<IActionResult> GetAllGioHang(Guid idKh)
    {
        try
        {
            var cartItems = await _gioHangServices.GetAllGioHangAsync(idKh);
            return Ok(cartItems); // 200 OK with list of cart items
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Get a specific cart item by ID
    [HttpGet("GetGioHangById/{id}")]
    public async Task<IActionResult> GetGioHangById(Guid id)
    {
        try
        {
            var cartItem = await _gioHangServices.GetGioHangByIdAsync(id);
            return Ok(cartItem); // 200 OK with cart item details
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Add a new cart item
    [HttpPost("AddGioHang")]
    public async Task<IActionResult> AddGioHang([FromBody] AddGioHangDtos addDto)
    {
        try
        {
            await _gioHangServices.AddGioHangAsync(addDto);
            return CreatedAtAction(nameof(GetGioHangById), new { id = addDto.Id }, addDto); // 201 Created
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Update an existing cart item
    [HttpPut("UpdateGioHang")]
    public async Task<IActionResult> UpdateGioHang([FromBody] GioHangUpdate updateDto)
    {
        try
        {
            await _gioHangServices.UpdateGioHangAsync(updateDto);
            return Ok(new { message = "Cart item updated successfully." }); // 200 OK
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Delete a cart item
    [HttpDelete("DeleteGioHang/{id}")]
    public async Task<IActionResult> DeleteGioHang(Guid id)
    {
        try
        {
            await _gioHangServices.DeleteGioHangAsync(id);
            return NoContent(); // 204 No Content
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    [HttpPost("place-order")]
    public async Task<IActionResult> PlaceOrder([FromQuery] Guid idKh, [FromBody] OrderInfoDto orderInfo)
    {
        try
        {
            if (orderInfo == null)
            {
                return BadRequest("Thông tin đơn hàng không được để trống.");
            }

            await _gioHangServices.PlaceOrderAsync(idKh, orderInfo);

            return Ok(new
            {
                Message = "Đặt hàng thành công!",
                CustomerId = idKh,
                OrderDate = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            

            return BadRequest(new
            {
                Message = $"Đặt hàng thất bại.", idKh,
                Error = ex.Message,
                InnerException = ex.InnerException?.Message // Ghi lại chi tiết về lỗi (nếu có)
            });
        }
    }

}
