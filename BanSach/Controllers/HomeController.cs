using BanSach.Data;
using BanSach.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BanSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public static List<CartItem> ci2 = new List<CartItem>();

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
      
        
        //// GET: Sach
        public async Task<IActionResult> Index()
        {
            var doanContext = _context.Sach;

         //   var doanContext2 = _context.ChiTietPn.Include(s => s.sach);
            return View(await doanContext.ToListAsync());
        }

        // Lưu danh sách CartItem trong giỏ hàng vào session
        void SaveCartSession(List<CartItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("shopcart", jsoncart);
        }



        // Đọc danh sách CartItem từ session
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("shopcart");
            if (jsoncart != null)
            {
                ci2= JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
           
            return new List<CartItem>();
        }

        // Cho hàng vào giỏ
        public async Task<IActionResult> AddToCart(string id)
        {
            var product = await _context.Sach
                .FirstOrDefaultAsync(m => m.Masach == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }
            var cart = GetCartItems();
            var item = cart.Find(p => p.Sach.Masach == id);
            if (item != null)
            {
                item.Soluong++;
            }
            else
            {
                cart.Add(new CartItem() { Sach = product, Soluong = 1 });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        // Chuyển đến view xem giỏ hàng
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        // Xóa một mặt hàng khỏi giỏ
        public IActionResult RemoveItem(string id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Sach.Masach == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        // Cập nhật số lượng một mặt hàng trong giỏ
        public IActionResult UpdateItem(string id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Sach.Masach == id);
            if (item != null)
            {
                item.Soluong = quantity;
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

             

        // Chuyển đến view thanh toán
        public IActionResult CheckOut()
        {
            ViewData["Makh"] = new SelectList(_context.KhachHang, "Makh", "Makh");
            ViewData["Hoten"] = new SelectList(_context.KhachHang, "Hoten", "Hoten");
            ViewData["Manv"] = new SelectList(_context.NhanVien, "Manv", "Manv");
            ViewData["Masach"] = new SelectList(_context.Sach, "Masach", "Masach");
            ViewData["Tuasach"] = new SelectList(_context.Sach, "Masach", "Tuasach");
            //  return View();

            //List<ChiTietHd> ci = new List<ChiTietHd> { new ChiTietHd { Mahd = "", Soluong = 0, Dongia = 0 } };

            return View(GetCartItems());
        }
    }
}
