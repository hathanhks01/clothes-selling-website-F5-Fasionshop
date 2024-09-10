using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class RefeshToken
{
    public int Id { get; set; }

    public Guid? IdNv { get; set; }

    public string? ToKen { get; set; }

    public DateTime? ThoiGianHetHan { get; set; }

    public Guid? IdKh { get; set; }

    public virtual NhanVien? IdNvNavigation { get; set; }
}
