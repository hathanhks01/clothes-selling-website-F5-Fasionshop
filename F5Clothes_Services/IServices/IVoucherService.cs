using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IVoucherService
    {
        Task<List<VouCher>> GetAllVouCher();
        Task<VouCher> GetByVouCher(Guid id);
        Task<VouCher> AddVc(VouCher Vc);
        Task<VouCher> UpdateVc(VouCher Vc);
        Task<VouCher> GetMaVouCher(string Ma);
    }
}
