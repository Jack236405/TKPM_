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
    public class TacGiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TacGiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TacGia
        public async Task<IActionResult> Index()
        {
            return View(await _context.TacGia.ToListAsync());
        }

        // GET: TacGia/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGia
                .FirstOrDefaultAsync(m => m.Matg == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // GET: TacGia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TacGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matg,Tentg,Ghichu")] TacGia tacGia)
        {
            if (ModelState.IsValid)
            {
                string S = string.Format("INSERT INTO dbo.TacGia VALUES(N'{0}', N'{0}')", tacGia.Tentg, tacGia.Ghichu);

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
            return View(tacGia);
        }

        // GET: TacGia/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGia.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }
            return View(tacGia);
        }

        // POST: TacGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Matg,Tentg,Ghichu")] TacGia tacGia)
        {
            if (id != tacGia.Matg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tacGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacGiaExists(tacGia.Matg))
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
            return View(tacGia);
        }

        // GET: TacGia/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGia
                .FirstOrDefaultAsync(m => m.Matg == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // POST: TacGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tacGia = await _context.TacGia.FindAsync(id);
            _context.TacGia.Remove(tacGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacGiaExists(string id)
        {
            return _context.TacGia.Any(e => e.Matg == id);
        }
    }
}
