using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IRefeshTokenServices
    {
        Task<List<RefeshToken>> GetAll();
        Task<RefeshToken> GetById(Guid id);
        Task Create(RefeshToken refeshToken);
        Task Update(RefeshToken refeshToken);
        Task Delete(Guid id);
    }
}
