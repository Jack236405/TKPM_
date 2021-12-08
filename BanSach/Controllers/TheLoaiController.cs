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
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TheLoaiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TheLoai
        public async Task<IActionResult> Index()
        {
            return View(await _context.TheLoai.ToListAsync());
        }

        // GET: TheLoai/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoai = await _context.TheLoai
                .FirstOrDefaultAsync(m => m.Matl == id);
            if (theLoai == null)
            {
                return NotFound();
            }

            return View(theLoai);
        }

        // GET: TheLoai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matl,Tentl,Ghichu")] TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                string S = string.Format("INSERT INTO dbo.theLoai(Tentl, Ghichu) VALUES(N'{0}', N'{0}')", theLoai.Tentl, theLoai.Ghichu);

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
            return View(theLoai);
        }

        // GET: TheLoai/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoai = await _context.TheLoai.FindAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }

        // POST: TheLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Matl,Tentl,Ghichu")] TheLoai theLoai)
        {
            if (id != theLoai.Matl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theLoai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheLoaiExists(theLoai.Matl))
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
            return View(theLoai);
        }

        // GET: TheLoai/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoai = await _context.TheLoai
                .FirstOrDefaultAsync(m => m.Matl == id);
            if (theLoai == null)
            {
                return NotFound();
            }

            return View(theLoai);
        }

        // POST: TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var theLoai = await _context.TheLoai.FindAsync(id);
            _context.TheLoai.Remove(theLoai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheLoaiExists(string id)
        {
            return _context.TheLoai.Any(e => e.Matl == id);
        }
    }
}
