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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeIm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBySp(Guid id)
        {


            var mappedImage = _mapper.Map<ImageDtos>(await _ImageRepo.GetByImage(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedImage);
        }

        [HttpPost]
        public async Task GetAll(Image Image)
        {
            await _ImageRepo.AddImage(Image);
        }

        [HttpPut]
        public async Task Update(Image Image)
        {
            await _ImageRepo.UpdateImage(Image);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _ImageRepo.DeleteImage(id);
        }
    }
}
