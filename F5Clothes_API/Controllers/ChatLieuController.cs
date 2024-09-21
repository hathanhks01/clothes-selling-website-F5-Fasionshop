using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F5Clothes_API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        private readonly IChatLieuRepo _chatLieuRepo;
        private readonly IMapper _mapper;

        public ChatLieuController(IChatLieuRepo clRepo, IMapper mapper)
        {
            _chatLieuRepo = clRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatLieu>>> GetAll()
        {
            var chatLieuList = await _chatLieuRepo.GetAllChatLieu();
            var mappedCL = _mapper.Map<List<ChatLieuDtos>>(chatLieuList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappedCL);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappedNhanVien = _mapper.Map<ChatLieuDtos>(await _chatLieuRepo.GetByChatLieu(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedNhanVien);
        }

        [HttpPost]
        public async Task GetAll(ChatLieu sv)
        {
            await _chatLieuRepo.AddCl(sv);
        }

        [HttpPut]
        public async Task Update(ChatLieu sv)
        {
            await _chatLieuRepo.UpdateCl(sv);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _chatLieuRepo.DeleteCl(id);
        }
    }
}
