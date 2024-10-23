using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGiamGiaRepositories
    {
        Task<List<GiamGia>> GetAll();
        Task<GiamGia> GetById(Guid id);
        Task Create(GiamGia giamGia);
        Task Update(GiamGia giamGia);
        Task Delete(Guid id);
    }
}
