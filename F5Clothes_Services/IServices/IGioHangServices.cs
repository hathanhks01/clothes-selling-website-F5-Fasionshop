using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IGioHangServices
    {
        Task<GioHangChiTiet> AddItem(GioHangChiTietDtos itemToAdd);

        
        Task<IEnumerable<GioHangChiTiet>> GetAll(Guid userId);

        
        Task<GioHangChiTiet> GetItem(Guid id);


        Task<bool> RemoveItem(Guid id);

        
        Task<GioHangChiTiet> UpdateItem(Guid cartItemId, GioHangUpdate itemToUpdate);
    }
}
