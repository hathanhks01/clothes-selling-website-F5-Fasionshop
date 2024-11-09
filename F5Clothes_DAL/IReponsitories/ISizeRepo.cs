using F5Clothes_DAL.DTOs;
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
        Task<Size> GetByIdSize(Guid id);
        Task<Size> AddSize(SizeDtos sizeDto);
        Task<Size> UpdateSize(SizeDtos sizeDto);
        Task DeleteSize(Guid id);
    }
}
