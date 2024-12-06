using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using F5Clothes_Services.IServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepo _voucherRepo;

        public VoucherService(IVoucherRepo voucherRepo)
        {
            _voucherRepo = voucherRepo;
        }

        public async Task<VouCher> AddVc(VouCher Vc)
        {
            await _voucherRepo.AddVc(Vc);
            return Vc;
            
        }

       

        public async Task<List<VouCher>> GetAllVouCher()
        {
            return await _voucherRepo.GetAllVouCher();
        }

        public async Task<VouCher> GetByVouCher(Guid id)
        {
            return await _voucherRepo.GetByVouCher(id);
        }

        public async Task<VouCher> GetMaVouCher(string Ma)
        {
            return await _voucherRepo.GetMaVouCher(Ma);
        }

        public async Task<VouCher> UpdateVc(VouCher Vc)
        {
            await _voucherRepo.UpdateVc(Vc);
            return Vc;
        }
    }
}
