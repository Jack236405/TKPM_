using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BanSach.Models
{
    public class CartItem
    {
        public Sach Sach { get; set; }
        [Display(Name = "Số Lượng")]
        public int Soluong { get; set; }
    }

    public partial class Sach
    {
        public Sach()
        {
            ChiTietHd = new HashSet<ChiTietHd>();
            ChiTietPn = new HashSet<ChiTietPn>();
        }
        
        [Key]
        [Display(Name = "Mã sách")]
        public string Masach { get; set; }
        [Display(Name = "Tựu Sách")]
        public string Tuasach { get; set; }
        [Display(Name = "Năm Xuất Bản")]
        public int? Namxb { get; set; }
        [Display(Name = "Số Lượng")]
        public int Soluong { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int Dongia { get; set; }
        [Display(Name = "Tên NXB")]
        public string Manxb { get; set; }
        [Display(Name = "Tên TL")]
        public string Matl { get; set; }
        [Display(Name = "Mã TG")]
        public string Matg { get; set; }
        [Display(Name = "Ảnh Bìa")]
        public string Ghichu { get; set; }

       
        [ForeignKey("Manxb")]
        public virtual NhaXuatBan ManxbNavigation { get; set; }
        [ForeignKey("Matg")]
        public virtual TacGia MatgNavigation { get; set; }
        [ForeignKey("Matl")]
        public virtual TheLoai MatlNavigation { get; set; }

        public virtual ICollection<ChiTietHd> ChiTietHd { get; set; }
        public virtual ICollection<ChiTietPn> ChiTietPn { get; set; }

       
    }
}
