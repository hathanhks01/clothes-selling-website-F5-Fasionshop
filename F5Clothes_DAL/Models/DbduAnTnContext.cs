using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace F5Clothes_DAL.Models;

public partial class DbduAnTnContext : DbContext
{
    public DbduAnTnContext()
    {
    }

    public DbduAnTnContext(DbContextOptions<DbduAnTnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatLieu> ChatLieus { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DiaChi> DiaChis { get; set; }

    public virtual DbSet<GiamGium> GiamGia { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }

    public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LichSuHoaDon> LichSuHoaDons { get; set; }

    public virtual DbSet<MauSac> MauSacs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<RefeshToken> RefeshTokens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<SanPhamChiTiet> SanPhamChiTiets { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<ThuongHieu> ThuongHieus { get; set; }

    public virtual DbSet<VouCher> VouChers { get; set; }

    public virtual DbSet<XuatXu> XuatXus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=DESKTOP-NDACBFQ\\SQLEXPRESS01;Database=DBDuAnTN;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatLieu>(entity =>
        {
            entity.ToTable("ChatLieu");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenChatLieu).HasMaxLength(250);
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.ToTable("ChucVu");

            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenChucVu).HasMaxLength(250);
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.ToTable("DanhMuc");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(250);
        });

        modelBuilder.Entity<DiaChi>(entity =>
        {
            entity.ToTable("DiaChi");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DiaChiChiTiet).HasMaxLength(250);
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.IdKh).HasColumnName("IdKH");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.PhuongXa).HasMaxLength(250);
            entity.Property(e => e.QuanHuyen).HasMaxLength(250);
            entity.Property(e => e.QuocGia).HasMaxLength(250);
            entity.Property(e => e.TinhThanh).HasMaxLength(250);

            entity.HasOne(d => d.IdKhNavigation).WithMany(p => p.DiaChis)
                .HasForeignKey(d => d.IdKh)
                .HasConstraintName("FK_DiaChi_KhachHang");
        });

        modelBuilder.Entity<GiamGium>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.MaGiamGia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.ToTable("GioHang");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.IdKh).HasColumnName("IdKH");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");

            entity.HasOne(d => d.IdKhNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.IdKh)
                .HasConstraintName("FK_GioHang_KhachHang");
        });

        modelBuilder.Entity<GioHangChiTiet>(entity =>
        {
            entity.ToTable("GioHangChiTiet");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DonGia).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.DonGiaKhiGiam).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.IdGh).HasColumnName("IdGH");
            entity.Property(e => e.IdSpct).HasColumnName("IdSPCT");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.SoTienGiam).HasColumnType("decimal(20, 0)");

            entity.HasOne(d => d.IdGhNavigation).WithMany(p => p.GioHangChiTiets)
                .HasForeignKey(d => d.IdGh)
                .HasConstraintName("FK_GioHangChiTiet_GioHang");

            entity.HasOne(d => d.IdSpctNavigation).WithMany(p => p.GioHangChiTiets)
                .HasForeignKey(d => d.IdSpct)
                .HasConstraintName("FK_GioHangChiTiet_SanPhamChiTiet");
        });

        modelBuilder.Entity<HinhThucThanhToan>(entity =>
        {
            entity.ToTable("HinhThucThanhToan");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.HinhThucThanhToan1).HasColumnName("HinhThucThanhToan");
            entity.Property(e => e.IdHd).HasColumnName("IdHD");
            entity.Property(e => e.IdKh).HasColumnName("IdKH");
            entity.Property(e => e.IdNv).HasColumnName("IdNV");
            entity.Property(e => e.MaGiaoDich).HasMaxLength(250);
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayThanhToan).HasColumnType("datetime");
            entity.Property(e => e.SoTienTra).HasColumnType("decimal(20, 0)");

            entity.HasOne(d => d.IdHdNavigation).WithMany(p => p.HinhThucThanhToans)
                .HasForeignKey(d => d.IdHd)
                .HasConstraintName("FK_HinhThucThanhToan_HoaDon");

            entity.HasOne(d => d.IdKhNavigation).WithMany(p => p.HinhThucThanhToans)
                .HasForeignKey(d => d.IdKh)
                .HasConstraintName("FK_HinhThucThanhToan_KhachHang");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.HinhThucThanhToans)
                .HasForeignKey(d => d.IdNv)
                .HasConstraintName("FK_HinhThucThanhToan_NhanVien");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK");

            entity.ToTable("HoaDon");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.GiaTriGiam).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.IdKh).HasColumnName("IdKH");
            entity.Property(e => e.IdNv).HasColumnName("IdNV");
            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayChoGiaoHang).HasColumnType("datetime");
            entity.Property(e => e.NgayGiaoHang).HasColumnType("datetime");
            entity.Property(e => e.NgayHuy).HasColumnType("datetime");
            entity.Property(e => e.NgayNhanHang).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayThanhToan).HasColumnType("datetime");
            entity.Property(e => e.NgayXacNhan).HasColumnType("datetime");
            entity.Property(e => e.SdtnguoiGiao).HasColumnName("SDTNguoiGiao");
            entity.Property(e => e.SdtnguoiNhan).HasColumnName("SDTNguoiNhan");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.TienGiaoHang).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.TienKhachTra).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.TienThua).HasColumnType("decimal(20, 0)");

            entity.HasOne(d => d.IdKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdKh)
                .HasConstraintName("FK_HoaDon_KhachHang");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdNv)
                .HasConstraintName("FK_HoaDon_NhanVien");

            entity.HasOne(d => d.IdVouCherNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdVouCher)
                .HasConstraintName("FK_HoaDon_VouCher");
        });

        modelBuilder.Entity<HoaDonChiTiet>(entity =>
        {
            entity.ToTable("HoaDonChiTiet");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DonGia).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.DonGiaKhiGiam).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.IdHd).HasColumnName("IdHD");
            entity.Property(e => e.IdSpct).HasColumnName("IdSPCT");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");

            entity.HasOne(d => d.IdHdNavigation).WithMany(p => p.HoaDonChiTiets)
                .HasForeignKey(d => d.IdHd)
                .HasConstraintName("FK_HoaDonChiTiet_HoaDon");

            entity.HasOne(d => d.IdSpctNavigation).WithMany(p => p.HoaDonChiTiets)
                .HasForeignKey(d => d.IdSpct)
                .HasConstraintName("FK_HoaDonChiTiet_SanPhamChiTiet");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IdSp).HasColumnName("IdSP");
            entity.Property(e => e.MoTa).HasMaxLength(250);

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.Images)
                .HasForeignKey(d => d.IdSp)
                .HasConstraintName("FK_Image_SanPham");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.ToTable("KhachHang");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.HoVaTenKh)
                .HasMaxLength(500)
                .HasColumnName("HoVaTenKH");
            entity.Property(e => e.MaKh)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MaKH");
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
        });

        modelBuilder.Entity<LichSuHoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichSuHo__3214EC07D30E6227");

            entity.ToTable("LichSuHoaDon");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IdHd).HasColumnName("IdHD");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NguoiThaoTac)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdHdNavigation).WithMany(p => p.LichSuHoaDons)
                .HasForeignKey(d => d.IdHd)
                .HasConstraintName("FK_LichSuHoaDon_HoaDon");
        });

        modelBuilder.Entity<MauSac>(entity =>
        {
            entity.ToTable("MauSac");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenMauSac).HasMaxLength(250);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.ToTable("NhanVien");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).HasMaxLength(550);
            entity.Property(e => e.HoVaTenNv)
                .HasMaxLength(500)
                .HasColumnName("HoVaTenNV");
            entity.Property(e => e.IdCv).HasColumnName("IdCV");
            entity.Property(e => e.MaNv)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);

            entity.HasOne(d => d.IdCvNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.IdCv)
                .HasConstraintName("FK_NhanVien_ChucVu");
        });

        modelBuilder.Entity<RefeshToken>(entity =>
        {
            entity.ToTable("RefeshToken");

            entity.Property(e => e.IdKh).HasColumnName("IdKH");
            entity.Property(e => e.IdNv).HasColumnName("IdNV");
            entity.Property(e => e.ThoiGianHetHan).HasColumnType("datetime");
            entity.Property(e => e.ToKen).HasMaxLength(500);

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.RefeshTokens)
                .HasForeignKey(d => d.IdNv)
                .HasConstraintName("FK_RefeshToken_NhanVien");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.ToTable("SanPham");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DonGiaKhiGiam).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.GiaBan).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.GiaNhap).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.IdCl).HasColumnName("IdCL");
            entity.Property(e => e.IdDm).HasColumnName("IdDM");
            entity.Property(e => e.IdGg).HasColumnName("IdGG");
            entity.Property(e => e.IdTh).HasColumnName("IdTH");
            entity.Property(e => e.IdXx).HasColumnName("IdXX");
            entity.Property(e => e.MaSp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaSP");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.NgayThem).HasColumnType("datetime");
            entity.Property(e => e.NgayThemGiamGia).HasColumnType("datetime");
            entity.Property(e => e.TenSp)
                .HasMaxLength(250)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.IdClNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.IdCl)
                .HasConstraintName("FK_SanPham_ChatLieu");

            entity.HasOne(d => d.IdDmNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.IdDm)
                .HasConstraintName("FK_SanPham_DanhMuc");

            entity.HasOne(d => d.IdGgNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.IdGg)
                .HasConstraintName("FK_SanPham_GiamGia");

            entity.HasOne(d => d.IdThNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.IdTh)
                .HasConstraintName("FK_SanPham_ThuongHieu");

            entity.HasOne(d => d.IdXxNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.IdXx)
                .HasConstraintName("FK_SanPham_XuatXu");
        });

        modelBuilder.Entity<SanPhamChiTiet>(entity =>
        {
            entity.ToTable("SanPhamChiTiet");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IdMs).HasColumnName("IdMS");
            entity.Property(e => e.IdSp).HasColumnName("IdSP");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.NgayTao).HasColumnType("datetime");

            entity.HasOne(d => d.IdMsNavigation).WithMany(p => p.SanPhamChiTiets)
                .HasForeignKey(d => d.IdMs)
                .HasConstraintName("FK_SanPhamChiTiet_MauSac");

            entity.HasOne(d => d.IdSizeNavigation).WithMany(p => p.SanPhamChiTiets)
                .HasForeignKey(d => d.IdSize)
                .HasConstraintName("FK_SanPhamChiTiet_Size");

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.SanPhamChiTiets)
                .HasForeignKey(d => d.IdSp)
                .HasConstraintName("FK_SanPhamChiTiet_SanPham");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.ToTable("Size");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenSize).HasMaxLength(250);
        });

        modelBuilder.Entity<ThuongHieu>(entity =>
        {
            entity.ToTable("ThuongHieu");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenThuongHieu).HasMaxLength(250);
        });

        modelBuilder.Entity<VouCher>(entity =>
        {
            entity.ToTable("VouCher");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.MaVouCher).HasMaxLength(100);
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.TenVouCher).HasMaxLength(100);
        });

        modelBuilder.Entity<XuatXu>(entity =>
        {
            entity.ToTable("XuatXu");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenXuatXu).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
