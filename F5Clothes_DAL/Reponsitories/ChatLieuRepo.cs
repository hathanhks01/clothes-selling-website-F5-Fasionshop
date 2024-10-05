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
        public async Task AddCl(ChatLieu cl)
        {
           _context.Add(cl);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCl(Guid Id)
        {
            var cl = await GetByChatLieu(Id);
            _context.Remove(cl);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatLieu>> GetAllChatLieu()
        {
            return await _context.ChatLieus.ToListAsync();
        }

        public async Task<ChatLieu> GetByChatLieu(Guid id)
        {
            return await _context.ChatLieus.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task UpdateCl(ChatLieu cl)
        {
           _context.Entry(cl).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
