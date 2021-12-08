using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public partial class ChiTietPn
    {
        [Key]
        public string Mactpn { get; set; }
        public int? Soluong { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int? Dongia { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int? Thanhtien { get; set; }
        public string Masach { get; set; }
        public string Mapn { get; set; }

        [ForeignKey("Mapn")]
        public virtual PhieuNhap MapnNavigation { get; set; }
        [ForeignKey("Masach")]
        public virtual Sach MasachNavigation { get; set; }
    }
}
