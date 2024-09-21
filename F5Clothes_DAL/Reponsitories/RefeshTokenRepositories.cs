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
    public class RefeshTokenRepositories : IRefeshTokenRepositories
    {
        private readonly DbduAnTnContext _context;
        public RefeshTokenRepositories(DbduAnTnContext context) 
        {
            _context = context;
        }

        public async Task Create(RefeshToken refeshToken)
        {
            await _context.RefeshTokens.AddAsync(refeshToken);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idRefeshToken = await GetById(id);
            _context.RefeshTokens.Remove(idRefeshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RefeshToken>> GetAll()
        {
            return await _context.RefeshTokens.ToListAsync(); 
        }

        public async Task<RefeshToken> GetById(Guid id)
        {
            return await _context.RefeshTokens.FindAsync(id);
        }

        public async Task Update(RefeshToken refeshToken)
        {
            _context.Entry(refeshToken).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
