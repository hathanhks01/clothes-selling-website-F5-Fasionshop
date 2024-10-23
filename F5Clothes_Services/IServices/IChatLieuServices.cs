using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IChatLieuServices
    {
        Task<List<ChatLieu>> GetAllChatLieu();
        Task<ChatLieu?> GetByIdChatLieu(Guid id);
        Task<ChatLieu> AddChatLieu(ChatLieuDtos chatLieuDto);
        Task<ChatLieu> UpdateChatLieu(ChatLieuDtos chatLieuDto);
        Task DeleteChatLieu(Guid id);
    }
}
