using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class ChatLieuRepo : IChatLieuRepo
    {
        private readonly DbduAnTnContext _context;
        public ChatLieuRepo(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task<ChatLieu> AddChatLieu(ChatLieuDtos chatLieuDtos)
        {
            var chatLieu = new ChatLieu
            {
                Id = Guid.NewGuid(),
                TenChatLieu = chatLieuDtos.TenChatLieu,
                MoTa = chatLieuDtos.MoTa,
                TrangThai = chatLieuDtos.TrangThai
            };
            await _context.ChatLieus.AddAsync(chatLieu);
            _context.SaveChanges();
            return chatLieu;

        }

        public async Task DeleteChatLieu(Guid id)
        {
            var chatLieu = await GetByIdChatLieu(id);
            _context.ChatLieus.Remove(chatLieu);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatLieu>> GetAllChatLieu()
        {
            return await _context.ChatLieus.ToListAsync();
        }

        public async Task<ChatLieu> GetByIdChatLieu(Guid id)
        {
            return await _context.ChatLieus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ChatLieu> UpdateChatLieu(ChatLieuDtos chatLieuDtos)
        {
            var existingChatLieu = await _context.ChatLieus
                .Where(cl => cl.Id == chatLieuDtos.Id)
                .FirstOrDefaultAsync();
            if(existingChatLieu != null)
            {
                existingChatLieu.TenChatLieu = chatLieuDtos.TenChatLieu;
                existingChatLieu.MoTa = chatLieuDtos.MoTa;
                existingChatLieu.TrangThai = chatLieuDtos.TrangThai;

                await _context.SaveChangesAsync();
            }
            return existingChatLieu ?? new ChatLieu();
        }

    }
}
