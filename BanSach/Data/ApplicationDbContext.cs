using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BanSach.Models;

namespace BanSach.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BanSach.Models.NhanVien> NhanVien { get; set; }
        public DbSet<BanSach.Models.TacGia> TacGia { get; set; }
        public DbSet<BanSach.Models.TheLoai> TheLoai { get; set; }
        public DbSet<BanSach.Models.NhaXuatBan> NhaXuatBan { get; set; }
        public DbSet<BanSach.Models.ChiTietHd> ChiTietHd { get; set; }
        public DbSet<BanSach.Models.ChiTietPn> ChiTietPn { get; set; }
        public DbSet<BanSach.Models.HoaDon> HoaDon { get; set; }
        public DbSet<BanSach.Models.KhachHang> KhachHang { get; set; }
        public DbSet<BanSach.Models.NhaCungCap> NhaCungCap { get; set; }
        public DbSet<BanSach.Models.PhieuNhap> PhieuNhap { get; set; }
        public DbSet<BanSach.Models.Sach> Sach { get; set; }
    }
}
