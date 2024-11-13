using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F5Clothes_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatLieu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TenChatLieu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiChucVu = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiamGia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MaGiamGia = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TenGiamGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: true),
                    GiaTriGiam = table.Column<long>(type: "bigint", nullable: true),
                    HinhThucGiam = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiamGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MaKH = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    HoVaTenKH = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    TaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TenMauSac = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TenSize = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VouCher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MaVouCher = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TenVouCher = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: true),
                    SoLuongMa = table.Column<int>(type: "int", nullable: true),
                    SoLuongDung = table.Column<int>(type: "int", nullable: true),
                    GiaTriGiam = table.Column<long>(type: "bigint", nullable: true),
                    DieuKienToiThieuHoaDon = table.Column<long>(type: "bigint", nullable: true),
                    HinhThucGiam = table.Column<int>(type: "int", nullable: true),
                    LoaiVouCher = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VouCher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XuatXu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TenXuatXu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatXu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdCV = table.Column<int>(type: "int", nullable: true),
                    MaNV = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    HoVaTenNV = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    TaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanVien_ChucVu",
                        column: x => x.IdCV,
                        principalTable: "ChucVu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiaChi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiaChiChiTiet = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhuongXa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    QuanHuyen = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TinhThanh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    QuocGia = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaChi_KhachHang",
                        column: x => x.IdKH,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHang_KhachHang",
                        column: x => x.IdKH,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdDM = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdTH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdXX = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCL = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdGG = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaSP = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TenSP = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TheLoai = table.Column<int>(type: "int", nullable: true),
                    ImageDefaul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaNhap = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    GiaBan = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    DonGiaKhiGiam = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    NgayThem = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayThemGiamGia = table.Column<DateTime>(type: "datetime", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPham_ChatLieu",
                        column: x => x.IdCL,
                        principalTable: "ChatLieu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc",
                        column: x => x.IdDM,
                        principalTable: "DanhMuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPham_GiamGia",
                        column: x => x.IdGG,
                        principalTable: "GiamGia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPham_ThuongHieu",
                        column: x => x.IdTH,
                        principalTable: "ThuongHieu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPham_XuatXu",
                        column: x => x.IdXX,
                        principalTable: "XuatXu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdNV = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdVouCher = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaHoaDon = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayXacNhan = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayChoGiaoHang = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayGiaoHang = table.Column<DateTime>(type: "datetime", nullable: true),
                    DonViGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNguoiGiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDTNguoiGiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TienGiaoHang = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    NgayNhanHang = table.Column<DateTime>(type: "datetime", nullable: true),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDTNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiNhanHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayHuy = table.Column<DateTime>(type: "datetime", nullable: true),
                    GiaTriGiam = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    TienKhachTra = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    TienThua = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    ThanhTien = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiHoaDon = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang",
                        column: x => x.IdKH,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien",
                        column: x => x.IdNV,
                        principalTable: "NhanVien",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDon_VouCher",
                        column: x => x.IdVouCher,
                        principalTable: "VouCher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefeshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNV = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToKen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefeshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefeshToken_NhanVien",
                        column: x => x.IdNV,
                        principalTable: "NhanVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdSP = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_SanPham",
                        column: x => x.IdSP,
                        principalTable: "SanPham",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SanPhamChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdSP = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdMS = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdSize = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuongTon = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_MauSac",
                        column: x => x.IdMS,
                        principalTable: "MauSac",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_SanPham",
                        column: x => x.IdSP,
                        principalTable: "SanPham",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_Size",
                        column: x => x.IdSize,
                        principalTable: "Size",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HinhThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdHD = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdNV = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaGiaoDich = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime", nullable: true),
                    SoTienTra = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    HinhThucThanhToan = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhThucThanhToan_HoaDon",
                        column: x => x.IdHD,
                        principalTable: "HoaDon",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HinhThucThanhToan_KhachHang",
                        column: x => x.IdKH,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HinhThucThanhToan_NhanVien",
                        column: x => x.IdNV,
                        principalTable: "NhanVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LichSuHoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHD = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NguoiThaoTac = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichSuHo__3214EC07D30E6227", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSuHoaDon_HoaDon",
                        column: x => x.IdHD,
                        principalTable: "HoaDon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdGH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdSPCT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    DonGia = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    DonGiaKhiGiam = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    SoTienGiam = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_GioHang",
                        column: x => x.IdGH,
                        principalTable: "GioHang",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_SanPhamChiTiet",
                        column: x => x.IdSPCT,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdHD = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdSPCT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    DonGia = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    DonGiaKhiGiam = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_HoaDon",
                        column: x => x.IdHD,
                        principalTable: "HoaDon",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_SanPhamChiTiet",
                        column: x => x.IdSPCT,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_IdKH",
                table: "DiaChi",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_IdKH",
                table: "GioHang",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IdGH",
                table: "GioHangChiTiet",
                column: "IdGH");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IdSPCT",
                table: "GioHangChiTiet",
                column: "IdSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_HinhThucThanhToan_IdHD",
                table: "HinhThucThanhToan",
                column: "IdHD");

            migrationBuilder.CreateIndex(
                name: "IX_HinhThucThanhToan_IdKH",
                table: "HinhThucThanhToan",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_HinhThucThanhToan_IdNV",
                table: "HinhThucThanhToan",
                column: "IdNV");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdKH",
                table: "HoaDon",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNV",
                table: "HoaDon",
                column: "IdNV");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdVouCher",
                table: "HoaDon",
                column: "IdVouCher");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IdHD",
                table: "HoaDonChiTiet",
                column: "IdHD");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IdSPCT",
                table: "HoaDonChiTiet",
                column: "IdSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_Image_IdSP",
                table: "Image",
                column: "IdSP");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuHoaDon_IdHD",
                table: "LichSuHoaDon",
                column: "IdHD");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdCV",
                table: "NhanVien",
                column: "IdCV");

            migrationBuilder.CreateIndex(
                name: "IX_RefeshToken_IdNV",
                table: "RefeshToken",
                column: "IdNV");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdCL",
                table: "SanPham",
                column: "IdCL");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdDM",
                table: "SanPham",
                column: "IdDM");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdGG",
                table: "SanPham",
                column: "IdGG");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdTH",
                table: "SanPham",
                column: "IdTH");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdXX",
                table: "SanPham",
                column: "IdXX");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_IdMS",
                table: "SanPhamChiTiet",
                column: "IdMS");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_IdSize",
                table: "SanPhamChiTiet",
                column: "IdSize");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_IdSP",
                table: "SanPhamChiTiet",
                column: "IdSP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaChi");

            migrationBuilder.DropTable(
                name: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "HinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiet");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "LichSuHoaDon");

            migrationBuilder.DropTable(
                name: "RefeshToken");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiet");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "VouCher");

            migrationBuilder.DropTable(
                name: "ChatLieu");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "GiamGia");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "XuatXu");

            migrationBuilder.DropTable(
                name: "ChucVu");
        }
    }
}
