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
    public class NhaXuatBanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhaXuatBanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NhaXuatBan
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhaXuatBan.ToListAsync());
        }

        // GET: NhaXuatBan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBan
                .FirstOrDefaultAsync(m => m.Manxb == id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        // GET: NhaXuatBan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhaXuatBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Manxb,Tennxb,Diachi,Dienthoai,Mail")] NhaXuatBan nhaXuatBan)
        {
            if (ModelState.IsValid)
            {
                string S = string.Format("INSERT INTO dbo.NhaXuatBan(Tennxb, Diachi, Dienthoai, Mail)VALUES(N'{0}', N'{1}', '{2}', '{3}')", nhaXuatBan.Tennxb, nhaXuatBan.Diachi, nhaXuatBan.Dienthoai, nhaXuatBan.Mail);

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
            return View(nhaXuatBan);
        }

        // GET: NhaXuatBan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBan.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }
            return View(nhaXuatBan);
        }

        // POST: NhaXuatBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Manxb,Tennxb,Diachi,Dienthoai,Mail")] NhaXuatBan nhaXuatBan)
        {
            if (id != nhaXuatBan.Manxb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaXuatBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaXuatBanExists(nhaXuatBan.Manxb))
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
            return View(nhaXuatBan);
        }

        // GET: NhaXuatBan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBan
                .FirstOrDefaultAsync(m => m.Manxb == id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        // POST: NhaXuatBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhaXuatBan = await _context.NhaXuatBan.FindAsync(id);
            _context.NhaXuatBan.Remove(nhaXuatBan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhaXuatBanExists(string id)
        {
            return _context.NhaXuatBan.Any(e => e.Manxb == id);
        }
    }
}
