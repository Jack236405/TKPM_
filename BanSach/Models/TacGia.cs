using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class TacGia
    {
        public TacGia()
        {
            Sach = new HashSet<Sach>();
        }

        [Key]
       
        public string Matg { get; set; }
        [Display(Name = "Tên Tác Giả")]
        public string Tentg { get; set; }
        [Display(Name = "Thông tin khác")]
        public string Ghichu { get; set; }

        public virtual ICollection<Sach> Sach { get; set; }
    }
}
