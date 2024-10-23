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
    public class ImageRepo: IImageRepo
    {
        private readonly DbduAnTnContext _context;
        public ImageRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddImage(Image Image)
        {
            _context.Add(Image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(Guid Id)
        {
            var Image = await GetByImage(Id);
            _context.Remove(Image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Image>> GetAllImage()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image> GetByImage(Guid id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateImage(Image Image)
        {
            _context.Entry(Image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
