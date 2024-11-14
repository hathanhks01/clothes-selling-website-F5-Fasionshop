using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class GioHangChiTietDtos
    {


        public Guid IdGh { get; set; }

        public Guid IdSpct { get; set; }

        public int SoLuong { get; set; }

    }
    public class GioHangUpdate
    {
        public Guid id { get; set; }
        public int SoLuong { get; set; }
    }
}
