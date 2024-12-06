using F5Clothes_DAL.Models;
using F5Clothes_DAL.Reponsitories;
using F5Clothes_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class LichSuHoaDonServices : ILichSuHoaDonServices
    {
        private readonly LSHDRepo _repo;
        public LichSuHoaDonServices(LSHDRepo repo)
        {
            _repo = repo;   
        }
        public async Task Create(LichSuHoaDon LsHoaDon)
        {
            await _repo.AddLs(LsHoaDon);
        }

        public async Task Delete(Guid id)
        {
             await _repo.DeleteLs(id);
        }

        public async Task<List<LichSuHoaDon>> GetAll()
        {
            return await _repo.GetAllLichSuHoaDon();
        }

        public async Task<LichSuHoaDon> GetById(Guid id)
        {
            return await _repo.GetByLichSuHoaDon(id);
        }

        public async Task Update(LichSuHoaDon LsHoaDon)
        {
            await _repo.UpdateLs(LsHoaDon);
        }
    }
}
