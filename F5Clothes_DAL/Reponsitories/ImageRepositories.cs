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
    public class ImageRepositories : IImageRepositories
    {
        private readonly DbduAnTnContext _context;
        public ImageRepositories(DbduAnTnContext context)
        {
            _context = context;
        }

        public async Task Create(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var idImage = await GetById(id);
            _context.Images.Remove(idImage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Image>> GetAll()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image> GetById(Guid id)
        {
            return await _context.Images.FindAsync(id);
        }

        public async Task Update(Image image)
        {
            _context.Entry(image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
