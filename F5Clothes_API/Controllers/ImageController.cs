using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepo _ImageRepo;
        private readonly IMapper _mapper;

        public ImageController(IImageRepo ImRepo, IMapper mapper)
        {
            _ImageRepo = ImRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetAll()
        {
            var ImageList = await _ImageRepo.GetAllImage();
            var mappeIm = _mapper.Map<List<ImageDtos>>(ImageList);
            return Ok(mappeIm);
        }

        [HttpPost]
        public async Task<ActionResult> AddImage(Image image)
        {
            await _ImageRepo.AddImage(image);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateImage(Guid id, Image image)
        {
            var existingImage = await _ImageRepo.GetByImage(id);
            if (existingImage == null)
            {
                return NotFound();
            }

            await _ImageRepo.UpdateImage(image);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            var image = await _ImageRepo.GetByImage(id);
            if (image == null)
            {
                return NotFound();
            }

            await _ImageRepo.DeleteImage(id);
            return NoContent();
        }
    }
}
