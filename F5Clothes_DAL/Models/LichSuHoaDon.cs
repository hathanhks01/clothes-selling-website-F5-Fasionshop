using System;
using System.Collections.Generic;

namespace F5Clothes_DAL.Models;

public partial class LichSuHoaDon
{
    public Guid Id { get; set; }

    public Guid? IdHd { get; set; }

    public string? NguoiThaoTac { get; set; }

    public string? GhiChu { get; set; }

    public int? TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual HoaDon? IdHdNavigation { get; set; }
}
