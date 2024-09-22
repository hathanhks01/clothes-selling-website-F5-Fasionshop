using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
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
        private readonly IChatLieuRepositories _response;
        public ChatLieuServices(IChatLieuRepositories response)
        {
            _response = response;
        }
        public async Task Create(ChatLieu chatLieu)
        {
            await _response.Create(chatLieu);
        }

        public async Task Delete(Guid id)
        {
            await _response.Delete(id);
        }

        public async Task<List<ChatLieu>> GetAll()
        {
           return await _response.GetAll();    
        }

        public async Task<ChatLieu> GetById(Guid id)
        {
            return await _response.GetById(id);
        }

        public async Task Update(ChatLieu chatLieu)
        {
            await _response.Update(chatLieu);
        }
    }
}
