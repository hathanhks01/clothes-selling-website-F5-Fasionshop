using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface ISizeRepo
    {
        Task<List<Size>> GetAllSize();
        Task<Size> GetBySize(Guid id);
        Task AddSz(Size Sz);
        Task UpdateSz(Size Sz);
        Task DeleteSz(Guid Id);
    }
}
