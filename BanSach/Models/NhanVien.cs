using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            HoaDon = new HashSet<HoaDon>();
            PhieuNhap = new HashSet<PhieuNhap>();
        }

        [Key]
        public string Manv { get; set; }
        public string Hoten { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Ngaysinh { get; set; }
        public string Diachi { get; set; }
        public string Dienthoai { get; set; }
        public string Mail { get; set; }
        public string Taikhoan { get; set; }
        public string Matkhau { get; set; }
        public int Quyen { get; set; }

        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhap { get; set; }
    }
}
