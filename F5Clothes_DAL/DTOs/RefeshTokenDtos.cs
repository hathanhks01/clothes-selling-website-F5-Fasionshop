using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class RefeshTokenDtos
    {
        public int Id { get; set; }

        public Guid? IdNv { get; set; }

        public string? ToKen { get; set; }

        public DateTime? ThoiGianHetHan { get; set; }

        public Guid? IdKh { get; set; }
    }
}
