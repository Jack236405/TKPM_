using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHd = new HashSet<ChiTietHd>();
        }

        [Key]
        public string Mahd { get; set; }
        public DateTime? Ngaylap { get; set; }
        public int? Tongtien { get; set; }
        public string Manv { get; set; }
        public string Makh { get; set; }

        [ForeignKey("Makh")]
        public virtual KhachHang MakhNavigation { get; set; }
        [ForeignKey("Manv")]
        public virtual NhanVien ManvNavigation { get; set; }
        public virtual ICollection<ChiTietHd> ChiTietHd { get; set; }
    }
}
