using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IMauSacRepositories
    {
        Task<List<MauSac>> GetAll();
        Task<MauSac> GetById(Guid id);
        Task Create(MauSac mauSac);
        Task Update(MauSac mauSac);
        Task Delete(Guid id);
    }
}
