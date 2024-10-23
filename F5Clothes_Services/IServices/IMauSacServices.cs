using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IMauSacServices
    {
        Task<List<MauSac>> GetAllMauSac();
        Task<MauSac> GetByIdMauSac(Guid id);
        Task<MauSac> AddMauSac(MauSacDtos mauSacDto);
        Task<MauSac> UpdateMauSac(MauSacDtos mauSacDto);
        Task DeleteMauSac(Guid id);
    }
}
