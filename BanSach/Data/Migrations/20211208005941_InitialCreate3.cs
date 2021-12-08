using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanSach.Data.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Makh = table.Column<string>(nullable: false),
                    Hoten = table.Column<string>(nullable: true),
                    Diachi = table.Column<string>(nullable: true),
                    Dienthoai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Makh);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    Mancc = table.Column<string>(nullable: false),
                    Tenncc = table.Column<string>(nullable: true),
                    Diachi = table.Column<string>(nullable: true),
                    Dienthoai = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.Mancc);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Manv = table.Column<string>(nullable: false),
                    Hoten = table.Column<string>(nullable: true),
                    Ngaysinh = table.Column<DateTime>(nullable: true),
                    Diachi = table.Column<string>(nullable: true),
                    Dienthoai = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Taikhoan = table.Column<string>(nullable: true),
                    Matkhau = table.Column<string>(nullable: true),
                    Quyen = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.Manv);
                });

            migrationBuilder.CreateTable(
                name: "NhaXuatBan",
                columns: table => new
                {
                    Manxb = table.Column<string>(nullable: false),
                    Tennxb = table.Column<string>(nullable: true),
                    Diachi = table.Column<string>(nullable: true),
                    Dienthoai = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaXuatBan", x => x.Manxb);
                });

            migrationBuilder.CreateTable(
                name: "TacGia",
                columns: table => new
                {
                    Matg = table.Column<string>(nullable: false),
                    Tentg = table.Column<string>(nullable: true),
                    Ghichu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGia", x => x.Matg);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    Matl = table.Column<string>(nullable: false),
                    Tentl = table.Column<string>(nullable: true),
                    Ghichu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.Matl);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Mahd = table.Column<string>(nullable: false),
                    Ngaylap = table.Column<DateTime>(nullable: true),
                    Tongtien = table.Column<int>(nullable: true),
                    Manv = table.Column<string>(nullable: true),
                    Makh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Mahd);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_Makh",
                        column: x => x.Makh,
                        principalTable: "KhachHang",
                        principalColumn: "Makh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien_Manv",
                        column: x => x.Manv,
                        principalTable: "NhanVien",
                        principalColumn: "Manv",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    Mapn = table.Column<string>(nullable: false),
                    Ngaynhap = table.Column<DateTime>(nullable: true),
                    Tongtien = table.Column<int>(nullable: true),
                    Mancc = table.Column<string>(nullable: true),
                    Manv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.Mapn);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_NhaCungCap_Mancc",
                        column: x => x.Mancc,
                        principalTable: "NhaCungCap",
                        principalColumn: "Mancc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_NhanVien_Manv",
                        column: x => x.Manv,
                        principalTable: "NhanVien",
                        principalColumn: "Manv",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    Masach = table.Column<string>(nullable: false),
                    Tuasach = table.Column<string>(nullable: true),
                    Namxb = table.Column<int>(nullable: true),
                    Soluong = table.Column<int>(nullable: true),
                    Manxb = table.Column<string>(nullable: true),
                    Matl = table.Column<string>(nullable: true),
                    Matg = table.Column<string>(nullable: true),
                    Ghichu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.Masach);
                    table.ForeignKey(
                        name: "FK_Sach_NhaXuatBan_Manxb",
                        column: x => x.Manxb,
                        principalTable: "NhaXuatBan",
                        principalColumn: "Manxb",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sach_TacGia_Matg",
                        column: x => x.Matg,
                        principalTable: "TacGia",
                        principalColumn: "Matg",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sach_TheLoai_Matl",
                        column: x => x.Matl,
                        principalTable: "TheLoai",
                        principalColumn: "Matl",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHd",
                columns: table => new
                {
                    Macthd = table.Column<string>(nullable: false),
                    Soluong = table.Column<int>(nullable: true),
                    Dongia = table.Column<int>(nullable: true),
                    Thanhtien = table.Column<int>(nullable: true),
                    Masach = table.Column<string>(nullable: true),
                    Mahd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHd", x => x.Macthd);
                    table.ForeignKey(
                        name: "FK_ChiTietHd_HoaDon_Mahd",
                        column: x => x.Mahd,
                        principalTable: "HoaDon",
                        principalColumn: "Mahd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietHd_Sach_Masach",
                        column: x => x.Masach,
                        principalTable: "Sach",
                        principalColumn: "Masach",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPn",
                columns: table => new
                {
                    Mactpn = table.Column<string>(nullable: false),
                    Soluong = table.Column<int>(nullable: true),
                    Dongia = table.Column<int>(nullable: true),
                    Thanhtien = table.Column<int>(nullable: true),
                    Masach = table.Column<string>(nullable: true),
                    Mapn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPn", x => x.Mactpn);
                    table.ForeignKey(
                        name: "FK_ChiTietPn_PhieuNhap_Mapn",
                        column: x => x.Mapn,
                        principalTable: "PhieuNhap",
                        principalColumn: "Mapn",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietPn_Sach_Masach",
                        column: x => x.Masach,
                        principalTable: "Sach",
                        principalColumn: "Masach",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHd_Mahd",
                table: "ChiTietHd",
                column: "Mahd");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHd_Masach",
                table: "ChiTietHd",
                column: "Masach");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPn_Mapn",
                table: "ChiTietPn",
                column: "Mapn");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPn_Masach",
                table: "ChiTietPn",
                column: "Masach");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Makh",
                table: "HoaDon",
                column: "Makh");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Manv",
                table: "HoaDon",
                column: "Manv");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_Mancc",
                table: "PhieuNhap",
                column: "Mancc");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_Manv",
                table: "PhieuNhap",
                column: "Manv");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_Manxb",
                table: "Sach",
                column: "Manxb");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_Matg",
                table: "Sach",
                column: "Matg");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_Matl",
                table: "Sach",
                column: "Matl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHd");

            migrationBuilder.DropTable(
                name: "ChiTietPn");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "NhaXuatBan");

            migrationBuilder.DropTable(
                name: "TacGia");

            migrationBuilder.DropTable(
                name: "TheLoai");
        }
    }
}
