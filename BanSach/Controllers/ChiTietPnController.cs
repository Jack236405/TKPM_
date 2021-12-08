using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanSach.Data;
using BanSach.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace BanSach.Controllers
{
    public class ChiTietPnController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChiTietPnController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietPn
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ChiTietPn.Include(c => c.MapnNavigation).Include(c => c.MasachNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ChiTietPn/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPn = await _context.ChiTietPn
                .Include(c => c.MapnNavigation)
                .Include(c => c.MasachNavigation)
                .FirstOrDefaultAsync(m => m.Mactpn == id);
            if (chiTietPn == null)
            {
                return NotFound();
            }

            return View(chiTietPn);
        }

        // GET: ChiTietPn/Create
        public IActionResult Create()
        {
            ViewData["Mapn"] = new SelectList(_context.Set<PhieuNhap>(), "Mapn", "Mapn");
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach");
            return View();
        }

        // POST: ChiTietPn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mactpn,Soluong,Dongia,Thanhtien,Masach,Mapn")] ChiTietPn chiTietPn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietPn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mapn"] = new SelectList(_context.Set<PhieuNhap>(), "Mapn", "Mapn", chiTietPn.Mapn);
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach", chiTietPn.Masach);
            return View(chiTietPn);
        }

        // GET: ChiTietPn/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPn = await _context.ChiTietPn.FindAsync(id);
            if (chiTietPn == null)
            {
                return NotFound();
            }
            ViewData["Mapn"] = new SelectList(_context.Set<PhieuNhap>(), "Mapn", "Mapn", chiTietPn.Mapn);
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach", chiTietPn.Masach);
            return View(chiTietPn);
        }

        // POST: ChiTietPn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mactpn,Soluong,Dongia,Thanhtien,Masach,Mapn")] ChiTietPn chiTietPn)
        {
            if (id != chiTietPn.Mactpn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietPn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietPnExists(chiTietPn.Mactpn))
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
            ViewData["Mapn"] = new SelectList(_context.Set<PhieuNhap>(), "Mapn", "Mapn", chiTietPn.Mapn);
            ViewData["Masach"] = new SelectList(_context.Set<Sach>(), "Masach", "Masach", chiTietPn.Masach);
            return View(chiTietPn);
        }

        // GET: ChiTietPn/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPn = await _context.ChiTietPn
                .Include(c => c.MapnNavigation)
                .Include(c => c.MasachNavigation)
                .FirstOrDefaultAsync(m => m.Mactpn == id);
            if (chiTietPn == null)
            {
                return NotFound();
            }

            return View(chiTietPn);
        }

        // POST: ChiTietPn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chiTietPn = await _context.ChiTietPn.FindAsync(id);
            _context.ChiTietPn.Remove(chiTietPn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietPnExists(string id)
        {
            return _context.ChiTietPn.Any(e => e.Mactpn == id);
        }

        //new
        public ActionResult NhapHang()
        {

            ViewData["Mancc"] = new SelectList(_context.NhaCungCap, "Mancc", "Tenncc");
            ViewData["Manv"] = new SelectList(_context.NhanVien, "Manv", "Manv");
            ViewData["Masach"] = new SelectList(_context.Sach, "Masach", "Masach");
            ViewData["Tuasach"] = new SelectList(_context.Sach, "Masach", "Tuasach");
            //  return View();

            // This is only for show by default one row for insert data to the database
            List<ChiTietPn> ci = new List<ChiTietPn> { new ChiTietPn { Mapn = "", Soluong = 0, Dongia = 0 } };
            return View(ci);
        }


     
        // POST: ChiTietPn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NhapHang([Bind("Mactpn,Soluong,Dongia,Thanhtien,Masach,Mapn")] ChiTietPn chiTietPn, List<ChiTietPn> ci, string MaNV, string MaNCC)
        {
            
            using (var connection = _context.Database.GetDbConnection())
            {
                //await connection.CloseAsync();
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    string addPn = string.Format("INSERT INTO dbo.PhieuNhap VALUES(GETDATE(), 0, '{0}', '{1}')", MaNCC, "NV001"/*MaNV*/);
                    command.CommandText = addPn;
                    var result0 = await command.ExecuteNonQueryAsync();

                    command.CommandText = "SELECT Mapn FROM dbo.PhieuNhap WHERE ID = (SELECT MAX(ID) FROM dbo.PhieuNhap)";
                    var MaPN = command.ExecuteScalar();

                    int TongTien = 0;
                    foreach (var i in ci)
                    {
                        chiTietPn = i;
                        TongTien += (int)chiTietPn.Soluong * (int)chiTietPn.Dongia;
                        string S = string.Format("INSERT INTO dbo.ChiTietPN VALUES({0}, {1}, {2}, '{3}', '{4}')", chiTietPn.Soluong, chiTietPn.Dongia, chiTietPn.Thanhtien, chiTietPn.Masach, MaPN);
                        command.CommandText = S;
                        var result = await command.ExecuteNonQueryAsync();

                        string S3 = string.Format("UPDATE dbo.Sach SET Soluong=Soluong+{0} WHERE Masach='{1}'", chiTietPn.Soluong, chiTietPn.Masach);
                        command.CommandText = S3;
                        var result3 = await command.ExecuteNonQueryAsync();
                    }


                    string S2 = string.Format("UPDATE dbo.PhieuNhap SET Tongtien={0} WHERE Mapn='{1}'", TongTien, MaPN);
                    command.CommandText = S2;
                    var result2 = await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
