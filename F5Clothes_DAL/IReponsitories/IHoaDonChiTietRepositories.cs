using F5Clothes_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IHoaDonChiTietRepositories
    {
        public  Task Create(HoaDonChiTiet hoaDonChiTiet);
        public Task Delete(Guid id);
        public Task<List<HoaDonChiTiet>> GetAll();
        public Task<HoaDonChiTiet> GetById(Guid id);
        public Task Update(HoaDonChiTiet hoaDonChiTiet);   
    }
}
