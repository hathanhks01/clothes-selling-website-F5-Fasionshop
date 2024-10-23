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
    public class RefeshTokenRepo: IRefshTokenRepo
    {
        private readonly DbduAnTnContext _context;
        public RefeshTokenRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddRt(RefeshToken Rt)
        {
            _context.Add(Rt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRt(int Id)
        {
            var Rt = await GetByRefeshToken(Id);
            _context.Remove(Rt);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RefeshToken>> GetAllRefshToken()
        {
            return await _context.RefeshTokens.ToListAsync();
        }

        public async Task<RefeshToken> GetByRefeshToken(int id)
        {
            return await _context.RefeshTokens.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateRt(RefeshToken Rt)
        {
            _context.Entry(Rt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
