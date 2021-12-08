using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDon = new HashSet<HoaDon>();
        }

        [Key]
        public string Makh { get; set; }
        public string Hoten { get; set; }
        public string Diachi { get; set; }
        public string Dienthoai { get; set; }

        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
