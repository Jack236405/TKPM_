using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class PhieuNhap
    {
        public PhieuNhap()
        {
            ChiTietPn = new HashSet<ChiTietPn>();
        }

        [Key]
        public string Mapn { get; set; }
        public DateTime? Ngaynhap { get; set; }
        public int? Tongtien { get; set; }
        public string Mancc { get; set; }
        public string Manv { get; set; }

        [ForeignKey("Mancc")]
        public virtual NhaCungCap ManccNavigation { get; set; }
        [ForeignKey("Manv")]
        public virtual NhanVien ManvNavigation { get; set; }
        public virtual ICollection<ChiTietPn> ChiTietPn { get; set; }
    }
}
