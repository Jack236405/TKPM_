using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class ChiTietHd
    {
        [Key]
        public string Macthd { get; set; }
        public int? Soluong { get; set; }
        public int? Dongia { get; set; }
        public int? Thanhtien { get; set; }
        public string Masach { get; set; }
        public string Mahd { get; set; }

        [ForeignKey("Mahd")]
        public virtual HoaDon MahdNavigation { get; set; }
        [ForeignKey("Masach")]
        public virtual Sach MasachNavigation { get; set; }
    }
}
