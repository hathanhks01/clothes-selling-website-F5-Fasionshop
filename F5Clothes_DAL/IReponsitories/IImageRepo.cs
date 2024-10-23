using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IImageRepo
    {
        Task<List<Image>> GetAllImage();
        Task<Image> GetByImage(Guid id);
        Task AddImage(Image Image);
        Task UpdateImage(Image Image);
        Task DeleteImage(Guid Id);
    }
}
