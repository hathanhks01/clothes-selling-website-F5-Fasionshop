using AutoMapper;
using F5Clothes_DAL.DTOs;

using F5Clothes_DAL.Models;

using F5Clothes_Services.IServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouCherController : ControllerBase
    {
        private readonly IVoucherService _VouCherSev;
        private readonly IMapper _mapper;

        public VouCherController(IVoucherService VcSev, IMapper mapper)
        {
            _VouCherSev = VcSev;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VouCher>>> GetAllVc()
        {
            var VouCherList = await _VouCherSev.GetAllVouCher();
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


            var mappeVc = _mapper.Map<VouCherDtos>(await _VouCherSev.GetByVouCher(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeVc);
        }

        [HttpPost]
        [ProducesResponseType(201)] // Created
        [ProducesResponseType(400)] // Bad Request
        [ProducesResponseType(422)] // Unprocessable Entity
        public async Task<IActionResult> CreateVouCher( VouCherDtos voucherCreate)
        {
            // Kiểm tra tính hợp lệ của model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucher = new VouCher
            {
                Id = Guid.NewGuid(), // Tạo Guid mới
                MaVouCher = voucherCreate.MaVouCher,
                TenVouCher = voucherCreate.TenVouCher,
                NgayTao = DateTime.UtcNow, // Gán ngày tạo hiện tại
                NgayBatDau = voucherCreate.NgayBatDau,
                NgayCapNhat = voucherCreate.NgayCapNhat,
                NgayKetThuc = voucherCreate.NgayKetThuc,
                SoLuongMa = voucherCreate.SoLuongMa,
                SoLuongDung = voucherCreate.SoLuongDung,
                GiaTriGiam = voucherCreate.GiaTriGiam,
                DieuKienToiThieuHoaDon = voucherCreate.DieuKienToiThieuHoaDon,
                HinhThucGiam = voucherCreate.HinhThucGiam,
                LoaiVouCher = voucherCreate.LoaiVouCher,
                GhiChu = voucherCreate.GhiChu,
                TrangThai = voucherCreate.TrangThai
            };

            // Gọi service để thêm voucher vào cơ sở dữ liệu
            var createdVoucher = await _VouCherSev.AddVc(voucher);

            if (createdVoucher == null)
            {
                return StatusCode(422, "Unable to create the voucher. Please check the details."); // Trả về 422 nếu không tạo được voucher
            }

            // Tạo DTO để trả về sau khi tạo voucher thành công
            var createdVoucherDto = new VouCherDtos
            {
                Id = createdVoucher.Id,
                MaVouCher = createdVoucher.MaVouCher,
                TenVouCher = createdVoucher.TenVouCher,
                NgayTao = createdVoucher.NgayTao,
                NgayBatDau = createdVoucher.NgayBatDau,
                NgayCapNhat = createdVoucher.NgayCapNhat,
                NgayKetThuc = createdVoucher.NgayKetThuc,
                SoLuongMa = createdVoucher.SoLuongMa,
                SoLuongDung = createdVoucher.SoLuongDung,
                GiaTriGiam = createdVoucher.GiaTriGiam,
                DieuKienToiThieuHoaDon = createdVoucher.DieuKienToiThieuHoaDon,
                HinhThucGiam = createdVoucher.HinhThucGiam,
                LoaiVouCher = createdVoucher.LoaiVouCher,
                GhiChu = createdVoucher.GhiChu,
                TrangThai = createdVoucher.TrangThai
            };

            return CreatedAtAction(nameof(GetByVouCher), new { id = createdVoucher.Id }, createdVoucherDto);
        }


        [HttpPut("{id:guid}")]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(400)] // Bad Request
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(422)] // Unprocessable Entity
        public async Task<IActionResult> UpdateVouCher(Guid id, [FromBody] VouCherDtos voucherUpdate)
        {
            // Kiểm tra tính hợp lệ của model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tìm voucher theo id
            var existingVoucher = await _VouCherSev.GetByVouCher(id);
            if (existingVoucher == null)
            {
                return NotFound($"Voucher with ID {id} not found.");
            }

            // Cập nhật các thuộc tính của voucher từ DTO
            existingVoucher.MaVouCher = voucherUpdate.MaVouCher;
            existingVoucher.TenVouCher = voucherUpdate.TenVouCher;
            existingVoucher.NgayBatDau = voucherUpdate.NgayBatDau;
            existingVoucher.NgayCapNhat = DateTime.UtcNow; // Gán ngày cập nhật hiện tại
            existingVoucher.NgayKetThuc = voucherUpdate.NgayKetThuc;
            existingVoucher.SoLuongMa = voucherUpdate.SoLuongMa;
            existingVoucher.SoLuongDung = voucherUpdate.SoLuongDung;
            existingVoucher.GiaTriGiam = voucherUpdate.GiaTriGiam;
            existingVoucher.DieuKienToiThieuHoaDon = voucherUpdate.DieuKienToiThieuHoaDon;
            existingVoucher.HinhThucGiam = voucherUpdate.HinhThucGiam;
            existingVoucher.LoaiVouCher = voucherUpdate.LoaiVouCher;
            existingVoucher.GhiChu = voucherUpdate.GhiChu;
            existingVoucher.TrangThai = voucherUpdate.TrangThai;

            var result = await _VouCherSev.UpdateVc(existingVoucher);

            if (result == null) // Kiểm tra nếu việc cập nhật không thành công
            {
                return StatusCode(422, "Unable to update the voucher. Please check the details.");
            }


            // Trả về voucher DTO đã được cập nhật
            var updatedVoucherDto = new VouCherDtos
            {
                
                MaVouCher = existingVoucher.MaVouCher,
                TenVouCher = existingVoucher.TenVouCher,
                NgayBatDau = existingVoucher.NgayBatDau,
                NgayCapNhat = existingVoucher.NgayCapNhat,
                NgayKetThuc = existingVoucher.NgayKetThuc,
                SoLuongMa = existingVoucher.SoLuongMa,
                SoLuongDung = existingVoucher.SoLuongDung,
                GiaTriGiam = existingVoucher.GiaTriGiam,
                DieuKienToiThieuHoaDon = existingVoucher.DieuKienToiThieuHoaDon,
                HinhThucGiam = existingVoucher.HinhThucGiam,
                LoaiVouCher = existingVoucher.LoaiVouCher,
                GhiChu = existingVoucher.GhiChu,
                TrangThai = existingVoucher.TrangThai
            };

            return Ok(updatedVoucherDto);
        }

    }
}
