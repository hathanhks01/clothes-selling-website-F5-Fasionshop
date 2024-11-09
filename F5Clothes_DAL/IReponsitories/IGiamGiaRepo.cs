using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGiamGiaRepo
    {
        Task<List<GiamGium>> GetAllGiamGia();
        Task<GiamGium> GetByIdGiamGia(Guid id);
        Task<GiamGium> AddGiamGia(GiamGiaDtos giamGiaDto);
        Task<GiamGium> UpdateGiamGia(GiamGiaDtos giamGiaDto);
        Task DeleteGiamGia(Guid id);
    }
}
