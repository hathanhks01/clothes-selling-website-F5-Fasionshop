﻿using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienRepo _nvRepo;
        private readonly IMapper _mapper;

        public NhanVienController(INhanVienRepo spRepo, IMapper mapper)
        {
            _nvRepo = spRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NhanVien>))]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetAllSp()
        {

            var mappedNhanViens = _mapper.Map<List<NhanVienDtos>>(await _nvRepo.GetAllNhanVien());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            return Ok(mappedNhanViens);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(NhanVien))]  // Fixed the type to a single NhanVien
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBySp(string id)
        {


            var mappedNhanVien = _mapper.Map<NhanVienDtos>(await _nvRepo.GetByNhanVien(id));  // Mapping single object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappedNhanVien);
        }
    }
}
