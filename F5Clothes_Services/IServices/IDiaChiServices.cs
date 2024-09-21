using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IDiaChiServices
    {
        Task<List<DiaChi>> GetAll();
        Task<DiaChi> GetById(Guid id);
        Task Create(DiaChi diaChi);
        Task Update(DiaChi diaChi);
        Task Delete(Guid id);
    }
}
