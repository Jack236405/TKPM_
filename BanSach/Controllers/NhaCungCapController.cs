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
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhaCungCapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NhaCungCap
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhaCungCap.ToListAsync());
        }

        // GET: NhaCungCap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCap
                .FirstOrDefaultAsync(m => m.Mancc == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        // GET: NhaCungCap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhaCungCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mancc,Tenncc,Diachi,Dienthoai,Mail")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                string S = string.Format("INSERT INTO dbo.NhaCungCap(Tenncc, Diachi, Dienthoai, Mail) VALUES (N'Thang Long', N'HN', '', '')",
                    nhaCungCap.Tenncc, nhaCungCap.Diachi, nhaCungCap.Dienthoai, nhaCungCap.Mail);

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
            return View(nhaCungCap);
        }

        // GET: NhaCungCap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCap.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mancc,Tenncc,Diachi,Dienthoai,Mail")] NhaCungCap nhaCungCap)
        {
            if (id != nhaCungCap.Mancc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaCungCap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaCungCapExists(nhaCungCap.Mancc))
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
            return View(nhaCungCap);
        }

        // GET: NhaCungCap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCap
                .FirstOrDefaultAsync(m => m.Mancc == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhaCungCap = await _context.NhaCungCap.FindAsync(id);
            _context.NhaCungCap.Remove(nhaCungCap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhaCungCapExists(string id)
        {
            return _context.NhaCungCap.Any(e => e.Mancc == id);
        }
    }
}
