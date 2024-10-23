using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IVouCherServices
    {
        Task<List<VouCher>> GetAll();
        Task<VouCher> GetById(Guid id);
        Task Create(VouCher vouCher);
        Task Update(VouCher vouCher);
        Task Delete(Guid id);
    }
}
