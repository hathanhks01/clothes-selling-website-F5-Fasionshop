using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface ISizeServices
    {
        Task<List<Size>> GetAll();
        Task<Size> GetById(Guid id);
        Task Create(Size size);
        Task Update(Size size);
        Task Delete(Guid id);
    }
}
