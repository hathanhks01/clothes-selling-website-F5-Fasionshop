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
    public class DMService : IDMService
    {
        private readonly IDanhMucRepo _danhMucRepo;

        public DMService(IDanhMucRepo danhMucRepo)
        {
            _danhMucRepo = danhMucRepo;
        }
        public async Task<DanhMuc> AddDm(DanhMuc dm)
        {
           await _danhMucRepo.AddDm(dm);
            return dm;
        }

        public async Task<List<DanhMuc>> GetAllDanhMuc()
        {
            return await _danhMucRepo.GetAllDanhMuc();
        }

        public async Task<DanhMuc> GetByDanhMuc(Guid id)
        {
            return await _danhMucRepo.GetByDanhMuc(id);
        }

        public async Task<DanhMuc> UpdateDm(DanhMuc dm)
        {
            await _danhMucRepo.UpdateDm(dm);
            return dm;
        }
    }
}
