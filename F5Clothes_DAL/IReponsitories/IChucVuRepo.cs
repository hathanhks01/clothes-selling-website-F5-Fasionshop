using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IChucVuRepo
    {
        Task<List<ChucVu>> GetAllChucVu();
        Task<ChucVu> GetByChucVu(int id);
        Task AddCv(ChucVu cv);
        Task UpdateCv(ChucVu cv);
        Task DeleteCv(int Id);
    }
}
