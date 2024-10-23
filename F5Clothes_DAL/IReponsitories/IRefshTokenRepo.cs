using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IRefshTokenRepo
    {
        Task<List<RefeshToken>> GetAllRefshToken();
        Task<RefeshToken> GetByRefeshToken(int id);
        Task AddRt(RefeshToken Rt);
        Task UpdateRt(RefeshToken Rt);
        Task DeleteRt(int Id);
    }
}
