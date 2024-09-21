using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IVoucherRepo
    {
        Task<List<VouCher>> GetAllVouCher();
        Task<VouCher> GetByVouCher(Guid id);
        Task AddVc(VouCher Vc);
        Task UpdateVc(VouCher Vc);
        Task DeleteVc(Guid Id);
    }
}
