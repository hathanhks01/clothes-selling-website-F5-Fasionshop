﻿using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class XuatXu
{
    public Guid Id { get; set; }

    public string? TenXuatXu { get; set; }

    public string? MoTa { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
