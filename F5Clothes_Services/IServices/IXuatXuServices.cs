using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IXuatXuServices
    {
        Task<List<XuatXu>> GetAll();
        Task<XuatXu> GetById(Guid id);
        Task Create(XuatXu xuatXu);
        Task Update(XuatXu xuatXu);
        Task Delete(Guid id);
    }
}
