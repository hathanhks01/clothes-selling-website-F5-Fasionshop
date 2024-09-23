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
        public async Task<IActionResult> GetBySp(Guid id)
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
            // Kiểm tra xem model có hợp lệ hay không
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new SanPham
            {
                Id = Guid.NewGuid(), // Tạo Guid mới
                MaSp = sanPhamCreate.MaSp,
                TenSp = sanPhamCreate.TenSp,
                GiaBan = sanPhamCreate.GiaBan,
                GiaNhap = sanPhamCreate.GiaNhap,
                DonGiaKhiGiam = sanPhamCreate.DonGiaKhiGiam,
                MoTa = sanPhamCreate.MoTa,
                IdDm = sanPhamCreate.IdDm,
                IdTh = sanPhamCreate.IdTh,
                IdXx = sanPhamCreate.IdXx,
                IdCl = sanPhamCreate.IdCl,
                IdGg = sanPhamCreate.IdGg,
                TheLoai = sanPhamCreate.TheLoai,
                ImageDefaul = sanPhamCreate.ImageDefaul,
                NgayThem = DateTime.UtcNow, // Cập nhật ngày thêm
                NgayThemGiamGia = sanPhamCreate.NgayThemGiamGia,
                TrangThai = sanPhamCreate.TrangThai
            };

            // Gọi repository để thêm sản phẩm (phải dùng await)
            var createdProduct = await _spRepo.AddSanPham(product); // Đảm bảo sử dụng phương thức async

            if (createdProduct == null)
            {
                return StatusCode(422, "Unable to create the product. Please check the details."); // Trả về 422 nếu không tạo được sản phẩm
            }

            // Tạo DTO cho sản phẩm đã tạo
            var createdSanPhamDto = new SanPhamDtos
            {
                IdDm = createdProduct.IdDm,
                IdTh = createdProduct.IdTh,
                IdXx = createdProduct.IdXx,
                IdCl = createdProduct.IdCl,
                IdGg = createdProduct.IdGg,
                MaSp = createdProduct.MaSp,
                TenSp = createdProduct.TenSp,
                GiaNhap = createdProduct.GiaNhap,
                GiaBan = createdProduct.GiaBan,
                DonGiaKhiGiam = createdProduct.DonGiaKhiGiam,
                MoTa = createdProduct.MoTa,
                TheLoai = createdProduct.TheLoai,
                ImageDefaul = createdProduct.ImageDefaul,
                NgayThem = createdProduct.NgayThem,
                NgayThemGiamGia = createdProduct.NgayThemGiamGia,
                TrangThai = createdProduct.TrangThai,
            };

            return CreatedAtAction(nameof(GetBySp), new { id = createdProduct.Id }, createdSanPhamDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)] // Bad Request
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(422)] // Unprocessable Entity
        public async Task<IActionResult> UpdateSanPham(Guid id, [FromBody] SanPhamDtos sanPhamUpdate)
        {
            // Kiểm tra xem model có hợp lệ hay không
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy sản phẩm hiện có từ cơ sở dữ liệu
            var existingProduct = await _spRepo.GetBySanPham(id);
            if (existingProduct == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy sản phẩm
            }

            // Cập nhật các thuộc tính của sản phẩm
            existingProduct.MaSp = sanPhamUpdate.MaSp;
            existingProduct.TenSp = sanPhamUpdate.TenSp;
            existingProduct.GiaBan = sanPhamUpdate.GiaBan;
            existingProduct.GiaNhap = sanPhamUpdate.GiaNhap;
            existingProduct.DonGiaKhiGiam = sanPhamUpdate.DonGiaKhiGiam;
            existingProduct.MoTa = sanPhamUpdate.MoTa;
            existingProduct.IdDm = sanPhamUpdate.IdDm;
            existingProduct.IdTh = sanPhamUpdate.IdTh;
            existingProduct.IdXx = sanPhamUpdate.IdXx;
            existingProduct.IdCl = sanPhamUpdate.IdCl;
            existingProduct.IdGg = sanPhamUpdate.IdGg;
            existingProduct.TheLoai = sanPhamUpdate.TheLoai;
            existingProduct.ImageDefaul = sanPhamUpdate.ImageDefaul;
            existingProduct.NgayThemGiamGia = sanPhamUpdate.NgayThemGiamGia;
            existingProduct.TrangThai = sanPhamUpdate.TrangThai;

            // Gọi repository để cập nhật sản phẩm
            await _spRepo.UpdateSanPham(existingProduct);

            return NoContent(); // Trả về 204 No Content khi cập nhật thành công
        }


    }
}
