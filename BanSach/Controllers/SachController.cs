using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanSach.Data;
using BanSach.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BanSach.Controllers
{
    public class SachController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SachController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sach
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sach.Include(s => s.ManxbNavigation).Include(s => s.MatgNavigation).Include(s => s.MatlNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sach/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach
                .Include(s => s.ManxbNavigation)
                .Include(s => s.MatgNavigation)
                .Include(s => s.MatlNavigation)
                .FirstOrDefaultAsync(m => m.Masach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Sach/Create
        public IActionResult Create()
        {
            ViewData["Manxb"] = new SelectList(_context.NhaXuatBan, "Manxb", "Manxb");
            ViewData["Matg"] = new SelectList(_context.TacGia, "Matg", "Matg");
            ViewData["Matl"] = new SelectList(_context.TheLoai, "Matl", "Matl");
            return View();
        }

        // POST: Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Masach,Tuasach,Namxb,Soluong,Manxb,Matl,Matg,Ghichu,Dongia")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                sach.Ghichu = Upload(file);
                string S = string.Format("INSERT INTO dbo.Sach(Tuasach, Namxb, Soluong, Manxb, Matl, Matg, Ghichu, Dongia)VALUES(N'{0}', {1}, {2}, '{3}', '{4}', '{5}', N'{6}', {7})", sach.Tuasach, sach.Namxb,
                    sach.Soluong, sach.Manxb, sach.Matl, sach.Matg, sach.Ghichu, sach.Dongia);

                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = S;
                        var result = await command.ExecuteNonQueryAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sach);
        }

        // GET: Sach/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            ViewData["Manxb"] = new SelectList(_context.NhaXuatBan, "Manxb", "Manxb", sach.Manxb);
            ViewData["Matg"] = new SelectList(_context.TacGia, "Matg", "Matg", sach.Matg);
            ViewData["Matl"] = new SelectList(_context.TheLoai, "Matl", "Matl", sach.Matl);
            return View(sach);
        }

        // POST: Sach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, string id, [Bind("Masach,Tuasach,Namxb,Soluong,Manxb,Matl,Matg,Ghichu")] Sach sach)
        {
            if (id != sach.Masach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null) sach.Ghichu = Upload(file);
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.Masach))
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
            ViewData["Manxb"] = new SelectList(_context.NhaXuatBan, "Manxb", "Manxb", sach.Manxb);
            ViewData["Matg"] = new SelectList(_context.TacGia, "Matg", "Matg", sach.Matg);
            ViewData["Matl"] = new SelectList(_context.TheLoai, "Matl", "Matl", sach.Matl);
            return View(sach);
        }

        // GET: Sach/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach
                .Include(s => s.ManxbNavigation)
                .Include(s => s.MatgNavigation)
                .Include(s => s.MatlNavigation)
                .FirstOrDefaultAsync(m => m.Masach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sach = await _context.Sach.FindAsync(id);
            _context.Sach.Remove(sach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(string id)
        {
            return _context.Sach.Any(e => e.Masach == id);
        }

        public string Upload(IFormFile file)
        {
            string uploadFileName = null;
            if (file != null)
            {
                // phát sinh tên
                uploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\images\\{uploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return uploadFileName;
        }
    }
}
