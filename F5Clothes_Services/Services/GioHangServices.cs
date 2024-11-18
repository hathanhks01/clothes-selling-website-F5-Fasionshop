using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class GioHangServices : IGioHangServices
    {
        private readonly IGioHangRepo _gioHangRepo;
        private readonly IMapper _mapper;

        public GioHangServices(IGioHangRepo gioHangRepo, IMapper mapper)
        {
            _gioHangRepo = gioHangRepo;
            _mapper = mapper;
        }

        // Retrieve all cart items for a customer by ID
        public async Task<List<GiohangDtos>> GetAllGioHangAsync(Guid idKh)
        {

            var gioHangChiTiets = await _gioHangRepo.GetAllGioHangAsync(idKh);
            return _mapper.Map<List<GiohangDtos>>(gioHangChiTiets);
        }

        // Retrieve a specific cart item by its ID
        public async Task<GiohangDtos> GetGioHangByIdAsync(Guid id)
        {
            var gioHangChiTiet = await _gioHangRepo.GetGioHangByIdAsync(id);
            if (gioHangChiTiet == null) throw new Exception("Cart item not found.");
            return _mapper.Map<GiohangDtos>(gioHangChiTiet);
        }

        // Add a new cart item

        public async Task AddGioHangAsync(AddGioHangDtos addDto)
        {
            var existingCartItem = await _gioHangRepo.GetCartItemByIdsAsync(addDto.IdGh, addDto.IdSpct);
            if (existingCartItem != null)
            {
                throw new Exception("Sản phẩm đã có trong giỏ hàng.");
            }
            // Map DTO to the entity
            var newCartItem = _mapper.Map<GioHangChiTiet>(addDto);

            // Calculate the unit price and validate
            var DonGia = await _gioHangRepo.GetProductPriceAsync(addDto.IdSpct);
            if (DonGia == 0) throw new Exception("The product does not exist or has no price set.");
           

            // Set additional properties on the new cart item
            newCartItem.Id = addDto.Id;
            newCartItem.IdGh = addDto.IdGh;
            newCartItem.IdSpct = addDto.IdSpct;
            newCartItem.SoLuong = addDto.SoLuong;
            newCartItem.DonGia = DonGia;
            newCartItem.DonGiaKhiGiam = null; // Apply discount logic if needed
            newCartItem.NgayTao = DateTime.Now;
            newCartItem.TrangThai = 0; // Active

            // Add the new cart item to the repository
            await _gioHangRepo.AddGioHangAsync(newCartItem);
        }

        // Update an existing cart item
       public async Task UpdateGioHangAsync(GioHangUpdate updateDto)
        {
            /*  var existingCartItem = await _gioHangRepo.GetGioHangByIdAsync(updateDto.id);
             if (existingCartItem == null) throw new Exception("Cart item not found.");

             // Use AutoMapper to map the updateDto to the existing entity
             _mapper.Map(updateDto, existingCartItem);  // This will update all properties from the DTO

             // Ensure that other fields like SoLuong are set explicitly if needed
             existingCartItem.SoLuong = updateDto.SoLuong;

             // Update the cart item in the repository
             await _gioHangRepo.UpdateGioHangAsync(existingCartItem);*/
        }


        // Delete a cart item
        public async Task DeleteGioHangAsync(Guid id)
        {
            var existingCartItem = await _gioHangRepo.GetGioHangByIdAsync(id);
            if (existingCartItem == null) throw new Exception("Cart item not found.");

            // Delete the cart item from the repository
            await _gioHangRepo.DeleteGioHangAsync(id);
        }
    }
}
