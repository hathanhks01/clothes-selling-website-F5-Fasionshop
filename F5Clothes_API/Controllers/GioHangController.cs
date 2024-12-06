using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

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
        var cartItems = await _gioHangServices.GetAllGioHangAsync(idKh);
        if (cartItems == null || !cartItems.Any())
        {
            // Nếu giỏ hàng rỗng, trả về một mảng rỗng thay vì lỗi
            return Ok(new List<GioHangChiTiet>());
        }
        return Ok(cartItems);
    }

    // Get the entire cart for a specific customer by customer ID (idKh)
    [HttpGet("GetByGioHang/{idKh}")]
    public async Task<IActionResult> GetByGioHang(Guid idKh)
    {
        try
        {
            // Call the service method to retrieve the cart for the customer
            var gioHang = await _gioHangServices.GetByGioHang(idKh);

            // Check if the cart exists
            if (gioHang == null)
            {
                return NotFound(new { Message = "Giỏ hàng không tồn tại." });
            }

            // Return the cart details
            return Ok(gioHang);  // 200 OK with the cart details
        }
        catch (Exception ex)
        {
            // Log the error and return a bad request response

            return StatusCode(500, new { Message = "Đã xảy ra lỗi khi lấy thông tin giỏ hàng.", Error = ex.Message });
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
    [HttpPost]
    public async Task<IActionResult> AddGioHang([FromBody] AddGioHangDtos addDto)
    {
        // Kiểm tra xem addDto có null không
        if (addDto == null)
        {
            return BadRequest("Dữ liệu không hợp lệ.");
        }

        // Kiểm tra idGh có hợp lệ không
        if (addDto.IdGh == Guid.Empty)
        {
            return BadRequest("ID Giỏ hàng không hợp lệ.");
        }

        // Kiểm tra IdSpct có hợp lệ không
        if (addDto.IdSpct == Guid.Empty)
        {
            return BadRequest("ID Sản phẩm chi tiết không hợp lệ.");
        }

        // Thực hiện logic thêm giỏ hàng
        try
        {
            await _gioHangServices.AddGioHangAsync(addDto);
            return Ok("Thêm giỏ hàng thành công.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
        }
    }


    // Update an existing cart item
    [HttpPut("UpdateGioHang")]
    public async Task<IActionResult> UpdateGioHang([FromBody] GioHangUpdate updateDto)
    {
        if (updateDto == null || updateDto.id == Guid.Empty)
        {
            return BadRequest("Invalid input data.");
        }

        try
        {
            await _gioHangServices.UpdateGioHangAsync(updateDto);
            return Ok(new { Message = "Cập nhật số lượng thành công!" });
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
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
                Message = $"Đặt hàng thất bại.",
                idKh,
                Error = ex.Message,
                InnerException = ex.InnerException?.Message // Ghi lại chi tiết về lỗi (nếu có)
            });
        }
    }

}
