using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IXuatXuRepo
    {
        Task<List<XuatXu>> GetAllXuatXu();
        Task<XuatXu> GetByIdXuatXu(Guid id);
        Task<XuatXu> AddXuatXu(XuatXuDtos xuatXuDto);
        Task<XuatXu> UpdateXuatXu(XuatXuDtos xuatXuDto);
        Task DeleteXuatXu(Guid id);
    }
}
