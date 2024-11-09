using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Reponsitories;
using F5Clothes_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class ChatLieuServices : IChatLieuServices
    {
        private readonly IChatLieuRepo _chatLieuRepo;       
        public ChatLieuServices(IChatLieuRepo chatLieuRepo, IMapper mapper)
        {
            _chatLieuRepo = chatLieuRepo;         
        }
        public async Task<ChatLieu> AddChatLieu(ChatLieuDtos chatLieuDto)
        {
           return await _chatLieuRepo.AddChatLieu(chatLieuDto);
        }

        public async Task DeleteChatLieu(Guid id)
        {
            await _chatLieuRepo.DeleteChatLieu(id);
        }

        public async Task<List<ChatLieu>> GetAllChatLieu()
        {
            return await _chatLieuRepo.GetAllChatLieu();
        }

        public async Task<ChatLieu?> GetByIdChatLieu(Guid id)
        {
            return await _chatLieuRepo.GetByIdChatLieu(id);
        }

        public async Task<ChatLieu> UpdateChatLieu(ChatLieuDtos chatLieuDto)
        {
            return await _chatLieuRepo.UpdateChatLieu(chatLieuDto);
        }
    }
}
