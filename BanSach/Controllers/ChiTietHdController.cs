using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanSach.Data;
using BanSach.Models;

namespace BanSach.Controllers
{
    public class ChiTietHdController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChiTietHdController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietHd
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ChiTietHd.Include(c => c.MahdNavigation).Include(c => c.MasachNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ChiTietHd/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHd = await _context.ChiTietHd
                .Include(c => c.MahdNavigation)
                .Include(c => c.MasachNavigation)
                .FirstOrDefaultAsync(m => m.Macthd == id);
            if (chiTietHd == null)
            {
                return NotFound();
            }

            return View(chiTietHd);
        }

        // GET: ChiTietHd/Create
        public IActionResult Create()
        {
            ViewData["Mahd"] = new SelectList(_context.Set<HoaDon>(), "Mahd", "Mahd");
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach");
            return View();
        }

        // POST: ChiTietHd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Macthd,Soluong,Dongia,Thanhtien,Masach,Mahd")] ChiTietHd chiTietHd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mahd"] = new SelectList(_context.Set<HoaDon>(), "Mahd", "Mahd", chiTietHd.Mahd);
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach", chiTietHd.Masach);
            return View(chiTietHd);
        }

        // GET: ChiTietHd/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHd = await _context.ChiTietHd.FindAsync(id);
            if (chiTietHd == null)
            {
                return NotFound();
            }
            ViewData["Mahd"] = new SelectList(_context.Set<HoaDon>(), "Mahd", "Mahd", chiTietHd.Mahd);
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach", chiTietHd.Masach);
            return View(chiTietHd);
        }

        // POST: ChiTietHd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Macthd,Soluong,Dongia,Thanhtien,Masach,Mahd")] ChiTietHd chiTietHd)
        {
            if (id != chiTietHd.Macthd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHdExists(chiTietHd.Macthd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mahd"] = new SelectList(_context.Set<HoaDon>(), "Mahd", "Mahd", chiTietHd.Mahd);
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach", chiTietHd.Masach);
            return View(chiTietHd);
        }

        // GET: ChiTietHd/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHd = await _context.ChiTietHd
                .Include(c => c.MahdNavigation)
                .Include(c => c.MasachNavigation)
                .FirstOrDefaultAsync(m => m.Macthd == id);
            if (chiTietHd == null)
            {
                return NotFound();
            }

            return View(chiTietHd);
        }

        // POST: ChiTietHd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chiTietHd = await _context.ChiTietHd.FindAsync(id);
            _context.ChiTietHd.Remove(chiTietHd);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHdExists(string id)
        {
            return _context.ChiTietHd.Any(e => e.Macthd == id);
        }

        public ActionResult BanHang()
        {

            ViewData["Makh"] = new SelectList(_context.KhachHang, "Makh", "Makh");
            ViewData["Hoten"] = new SelectList(_context.KhachHang, "Hoten", "Hoten");
            ViewData["Manv"] = new SelectList(_context.NhanVien, "Manv", "Manv");
            ViewData["Masach"] = new SelectList(_context.Sach, "Masach", "Masach");
            ViewData["Tuasach"] = new SelectList(_context.Sach, "Masach", "Tuasach");
            //  return View();

            // This is only for show by default one row for insert data to the database
            List<ChiTietHd> ci = new List<ChiTietHd> { new ChiTietHd { Mahd = "", Soluong = 0, Dongia = 0 } };
            return View(ci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanHang([Bind("Mactpn,Soluong,Dongia,Thanhtien,Masach,Mapn")] CartItem ChiTietHd, List<ChiTietHd> ci, string MaNV, string MaNCC)
        {
            var a = HomeController.ci2;
         //   string s = a[0].Sach.Masach;
        //    int le = a.Count;
            using (var connection = _context.Database.GetDbConnection())
            {
                //await connection.CloseAsync();
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string addPn = string.Format("INSERT INTO dbo.HoaDon VALUES(GETDATE(), 0, '{0}', '{1}')", "NV001"/*MaNV*/, MaNCC);
                    command.CommandText = addPn;
                    var result0 = await command.ExecuteNonQueryAsync();

                    command.CommandText = "SELECT Mahd FROM dbo.HoaDon WHERE ID = (SELECT MAX(ID) FROM dbo.HoaDon)";
                    var MaHD = command.ExecuteScalar();

                    int TongTien = 0;
                    for (int i = 0; i < a.Count; i++)
                    {
                        TongTien += (int)a[i].Soluong * (int)a[i].Sach.Dongia;
                        string S = string.Format("INSERT INTO dbo.ChiTietHd VALUES({0}, {1}, {2}, '{3}', '{4}')", a[i].Soluong, a[i].Sach.Dongia, a[i].Soluong * a[i].Sach.Dongia, a[i].Sach.Masach, MaHD);
                        command.CommandText = S;
                        var result = await command.ExecuteNonQueryAsync();

                        string S3 = string.Format("UPDATE dbo.Sach SET Soluong=Soluong-{0} WHERE Masach='{1}'", a[i].Soluong, a[i].Sach.Masach);
                        command.CommandText = S3;
                        var result3 = await command.ExecuteNonQueryAsync();
                    }
                    //foreach (var i in a)
                    //{
                    //   // ChiTietHd = i;
                    //    TongTien += (int)ChiTietHd.Soluong * (int)ChiTietHd.Sach.Dongia;
                    //    string S = string.Format("INSERT INTO dbo.ChiTietHd VALUES({0}, {1}, {2}, '{3}', '{4}')", i.Soluong, ChiTietHd.Sach.Dongia, i.Soluong*i.Dongia, ChiTietHd.Sach.Masach, MaHD);
                    //    command.CommandText = S;
                    //    var result = await command.ExecuteNonQueryAsync();

                    //    string S3 = string.Format("UPDATE dbo.Sach SET Soluong=Soluong-{0} WHERE Masach='{1}'", ChiTietHd.Soluong, ChiTietHd.Sach.Masach);
                    //    command.CommandText = S3;
                    //    var result3 = await command.ExecuteNonQueryAsync();
                    //}


                    string S2 = string.Format("UPDATE dbo.HoaDon SET Tongtien={0} WHERE MaHD='{1}'", TongTien, MaHD);
                    command.CommandText = S2;
                    var result2 = await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
