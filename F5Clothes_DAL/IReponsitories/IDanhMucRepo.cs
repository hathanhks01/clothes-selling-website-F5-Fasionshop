using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IDanhMucRepo
    {
        Task<List<DanhMuc>> GetAllDanhMuc();
        Task<DanhMuc> GetByDanhMuc(Guid id);
        Task AddDm(DanhMuc dm);
        Task UpdateDm(DanhMuc dm);
        Task DeleteDm(Guid Id);
    }
}
