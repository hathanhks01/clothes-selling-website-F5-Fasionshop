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
        Task<XuatXu> GetByXuatXu(Guid id);
        Task AddXx(XuatXu xx);
        Task UpdateXx(XuatXu xx);
        Task DeleteXx(Guid Id);
    }
}
