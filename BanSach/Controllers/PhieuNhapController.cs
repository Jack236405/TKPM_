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
    public class PhieuNhapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhieuNhapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhieuNhap
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PhieuNhap.Include(p => p.ManccNavigation).Include(p => p.ManvNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PhieuNhap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuNhap = await _context.PhieuNhap
                .Include(p => p.ManccNavigation)
                .Include(p => p.ManvNavigation)
                .FirstOrDefaultAsync(m => m.Mapn == id);
            if (phieuNhap == null)
            {
                return NotFound();
            }

            return View(phieuNhap);
        }

        // GET: PhieuNhap/Create
        public IActionResult Create()
        {
            ViewData["Mancc"] = new SelectList(_context.NhaCungCap, "Mancc", "Mancc");
            ViewData["Manv"] = new SelectList(_context.NhanVien, "Manv", "Manv");
            return View();
        }

        // POST: PhieuNhap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mapn,Ngaynhap,Tongtien,Mancc,Manv")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mancc"] = new SelectList(_context.NhaCungCap, "Mancc", "Mancc", phieuNhap.Mancc);
            ViewData["Manv"] = new SelectList(_context.NhanVien, "Manv", "Manv", phieuNhap.Manv);
            return View(phieuNhap);
        }

        // GET: PhieuNhap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuNhap = await _context.PhieuNhap.FindAsync(id);
            if (phieuNhap == null)
            {
                return NotFound();
            }
            ViewData["Mancc"] = new SelectList(_context.NhaCungCap, "Mancc", "Mancc", phieuNhap.Mancc);
            ViewData["Manv"] = new SelectList(_context.NhanVien, "Manv", "Manv", phieuNhap.Manv);
            return View(phieuNhap);
        }

        // POST: PhieuNhap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mapn,Ngaynhap,Tongtien,Mancc,Manv")] PhieuNhap phieuNhap)
        {
            if (id != phieuNhap.Mapn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuNhap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuNhapExists(phieuNhap.Mapn))
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
            ViewData["Mancc"] = new SelectList(_context.NhaCungCap, "Mancc", "Mancc", phieuNhap.Mancc);
            ViewData["Manv"] = new SelectList(_context.NhanVien, "Manv", "Manv", phieuNhap.Manv);
            return View(phieuNhap);
        }

        // GET: PhieuNhap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuNhap = await _context.PhieuNhap
                .Include(p => p.ManccNavigation)
                .Include(p => p.ManvNavigation)
                .FirstOrDefaultAsync(m => m.Mapn == id);
            if (phieuNhap == null)
            {
                return NotFound();
            }

            return View(phieuNhap);
        }

        // POST: PhieuNhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var phieuNhap = await _context.PhieuNhap.FindAsync(id);
            _context.PhieuNhap.Remove(phieuNhap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuNhapExists(string id)
        {
            return _context.PhieuNhap.Any(e => e.Mapn == id);
        }
    }
}
