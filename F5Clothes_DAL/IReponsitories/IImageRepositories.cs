using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IImageRepositories
    {
        Task<List<Image>> GetAll();
        Task<Image> GetById(Guid id);
        Task Create(Image image);
        Task Update(Image image);
        Task Delete(Guid id);
    }
}
