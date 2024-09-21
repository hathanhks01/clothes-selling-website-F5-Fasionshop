using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IDanhMucRepositories
    {
        Task<List<DanhMuc>> GetAll();
        Task<DanhMuc> GetById(Guid id);
        Task Create(DanhMuc danhMuc);
        Task Update(DanhMuc danhMuc);
        Task Delete(Guid id);
    }
}
