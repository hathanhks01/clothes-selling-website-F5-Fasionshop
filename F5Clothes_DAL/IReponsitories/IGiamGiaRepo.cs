using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IGiamGiaRepo
    {
        Task<List<GiamGium>> GetAllGg();
        Task<GiamGium> GetByGg(Guid id);
        Task AddGg(GiamGium gg);
        Task UpdateGg(GiamGium gg);
        Task DeleteGg(Guid id);
    }
}
