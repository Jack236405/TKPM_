using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class NhaXuatBan
    {
        public NhaXuatBan()
        {
            Sach = new HashSet<Sach>();
        }

        [Key]
        public string Manxb { get; set; }
        [Display(Name = "Tên Nhà Xuất Bản")]
        public string Tennxb { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Diachi { get; set; }
        [Display(Name = "Điện Thoại")]
        public string Dienthoai { get; set; }
        public string Mail { get; set; }

        public virtual ICollection<Sach> Sach { get; set; }
    }
}
