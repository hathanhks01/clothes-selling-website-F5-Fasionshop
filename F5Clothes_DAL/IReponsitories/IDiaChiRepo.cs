using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IDiaChiRepo
    {
        Task<List<DiaChi>> GetAllDiaChi();
        Task<DiaChi> GetByDiaChi(Guid id);
        Task<DiaChi> Adddc(DiaChiDtos dc);
        Task Updatedc(DiaChi dc);
        Task Deletedc(Guid Id);
    }
}
