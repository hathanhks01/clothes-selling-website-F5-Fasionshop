using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IChatLieuRepo
    {
        Task<List<ChatLieu>> GetAllChatLieu();
        Task<ChatLieu> GetByChatLieu(Guid id);
        Task AddCl(ChatLieu cl);
        Task UpdateCl(ChatLieu cl);
        Task DeleteCl(Guid Id);
    }
}
