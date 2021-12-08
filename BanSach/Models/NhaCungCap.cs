using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            PhieuNhap = new HashSet<PhieuNhap>();
        }

        [Key]
        public string Mancc { get; set; }
        public string Tenncc { get; set; }
        public string Diachi { get; set; }
        public string Dienthoai { get; set; }
        public string Mail { get; set; }

        public virtual ICollection<PhieuNhap> PhieuNhap { get; set; }
    }
}
