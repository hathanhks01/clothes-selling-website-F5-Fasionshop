using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        private readonly IChatLieuServices _services;

        public ChatLieuController(IChatLieuServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<List<ChatLieu>> GetAll()
        {
            return await _services.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ChatLieu> GetById(Guid id)
        {
           return await _services.GetById(id);           
        }
        [HttpPut("{id}")]
        public async Task PutChatLieu(ChatLieu chatLieu)
        {
             await _services.Update(chatLieu);
        }
        [HttpPost]
        public async Task PostChatLieu(ChatLieu chatLieu)
        {
            await _services.Create(chatLieu);
        }
        [HttpDelete("{id}")]
        public async Task DeleteChatLieu(Guid id)
        {
            await _services.Delete(id);
        }

    }
}
