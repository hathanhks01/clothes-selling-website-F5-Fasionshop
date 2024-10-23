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
    public class ChatLieuRepositories : IChatLieuRepositories
    {
        private readonly DbduAnTnContext _context;
        public ChatLieuRepositories(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task Create(ChatLieu chatLieu)
        {
            await _context.ChatLieus.AddAsync(chatLieu);    
            _context.SaveChanges();
        }
        public async Task Delete(Guid id)
        {
            var idChatLieu = await GetById(id); 
             _context.ChatLieus.Remove(idChatLieu);
            await _context.SaveChangesAsync();

        }
        public async Task<List<ChatLieu>> GetAll()
        {
            return await _context.ChatLieus.ToListAsync();    
        }
        public async Task<ChatLieu> GetById(Guid id)
        {
            return await _context.ChatLieus.FindAsync(id);
        }
        public async Task Update(ChatLieu chatLieu)
        {
            _context.Entry(chatLieu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
