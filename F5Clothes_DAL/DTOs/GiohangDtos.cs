﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class GiohangDtos
    {
       
        public Guid IdGh { get; set; }
        public string TenSp { get; set; }
       
        public string HinhAnh { get; set; }
        public Guid IdSpct { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }
        public decimal TongTien { get; set; }


    }
}
