using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IMauSacRepo
    {
        Task<List<MauSac>> GetAllMauSac();
        Task<MauSac> GetByMauSac(Guid id);
        Task AddMs(MauSac Ms);
        Task UpdateMs(MauSac Ms);
        Task DeleteMs(Guid Id);
    }
}
