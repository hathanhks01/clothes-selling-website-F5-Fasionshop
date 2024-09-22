using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IThuongHieuServices
    {
        Task<List<ThuongHieu>> GetAll();
        Task<ThuongHieu> GetById(Guid id);
        Task Create(ThuongHieu thuongHieu);
        Task Update(ThuongHieu thuongHieu);
        Task Delete(Guid id);
    }
}
