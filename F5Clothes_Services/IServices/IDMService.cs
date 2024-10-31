using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IDMService
    {
        Task<List<DanhMuc>> GetAllDanhMuc();
        Task<DanhMuc> GetByDanhMuc(Guid id);
        Task<DanhMuc> AddDm(DanhMuc dm);
        Task<DanhMuc> UpdateDm(DanhMuc dm);
    }
}
