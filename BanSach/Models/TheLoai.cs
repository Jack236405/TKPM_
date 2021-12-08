using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            Sach = new HashSet<Sach>();
        }

        [Key]
        public string Matl { get; set; }
        [Display(Name = "Tên Thể Loại")]
        public string Tentl { get; set; }
        [Display(Name = "Tên Tác Giả")]
        public string Ghichu { get; set; }

        public virtual ICollection<Sach> Sach { get; set; }
    }
}
