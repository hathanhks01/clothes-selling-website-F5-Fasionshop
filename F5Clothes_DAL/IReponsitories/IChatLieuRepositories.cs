using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IChatLieuRepositories
    {
        Task<List<ChatLieu>> GetAll();
        Task<ChatLieu> GetById(Guid id);
        Task Create(ChatLieu chatLieu);
        Task Update(ChatLieu chatLieu);
        Task Delete(Guid id);

    }
}
