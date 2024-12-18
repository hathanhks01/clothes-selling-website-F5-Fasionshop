﻿using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LSHDController : ControllerBase
    {
        private readonly ILichSuHoaDonServices _LichSuHoaDonSV;
        private readonly IMapper _mapper;

        public LSHDController(ILichSuHoaDonServices lsSv, IMapper mapper)
        {
            _LichSuHoaDonSV = lsSv;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichSuHoaDon>>> GetAllLs()
        {
            var LichSuHoaDonList = await _LichSuHoaDonSV.GetAll();
            var mappeLs = _mapper.Map<List<LichSuHoaDonDtos>>(LichSuHoaDonList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeLs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByLichSuHoaDon(Guid id)
        {


            var mappeLs = _mapper.Map<LichSuHoaDonDtos>(await _LichSuHoaDonSV.GetById(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeLs);
        }

        [HttpPost]
        public async Task GetAll(LichSuHoaDon Ls)
        {
            await _LichSuHoaDonSV.Create(Ls);
        }

        [HttpPut]
        public async Task Update(LichSuHoaDon Ls)
        {
            await _LichSuHoaDonSV.Update(Ls);
        }
    }
}
