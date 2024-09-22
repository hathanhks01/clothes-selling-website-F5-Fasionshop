using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IMauSacServices
    {
        Task<List<MauSac>> GetAll();
        Task<MauSac> GetById(Guid id);
        Task Create(MauSac mauSac);
        Task Update(MauSac mauSac);
        Task Delete(Guid id);
    }
}
