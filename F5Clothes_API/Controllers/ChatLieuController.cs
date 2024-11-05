using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Reponsitories;
using F5Clothes_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F5Clothes_API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        private readonly IChatLieuServices _chatLieuSer;
        public ChatLieuController(IChatLieuServices chatLieuSer, IChatLieuRepo chatLieuRepo)
        {
            _chatLieuSer = chatLieuSer;
        }

        [HttpGet]
        public async Task<List<ChatLieu>> GetAll()
        {
            return await _chatLieuSer.GetAllChatLieu();
        }

        [HttpGet("{id}")]
        public async Task<ChatLieu> GetById(Guid id)
        {
            return await _chatLieuSer.GetByIdChatLieu(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ChatLieuDtos chatLieuDto)
        {
            await _chatLieuSer.AddChatLieu(chatLieuDto);
            return CreatedAtAction(nameof(GetById), new { id = chatLieuDto.Id }, chatLieuDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ChatLieuDtos chatLieuDto)
        {
            if (id != chatLieuDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _chatLieuSer.UpdateChatLieu(chatLieuDto);
                return Ok(chatLieuDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _chatLieuSer.DeleteChatLieu(id);
        }
    }
}
