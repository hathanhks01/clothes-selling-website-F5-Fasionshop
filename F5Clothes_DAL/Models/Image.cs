using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class Image
{
    public Guid Id { get; set; }

    public Guid? IdSp { get; set; }

    public string? TenImage { get; set; }

    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public virtual SanPham? IdSpNavigation { get; set; }
}
