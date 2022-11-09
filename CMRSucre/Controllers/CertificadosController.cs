using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMRSucre.Data;
using CMRSucre.Models;

namespace CMRSucre.Controllers
{
    public class CertificadosController : Controller
    {
        private readonly CMRSucreContext _context;

        public CertificadosController(CMRSucreContext context)
        {
            _context = context;
        }

        // GET: Certificados
        public async Task<IActionResult> Index()
        {
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            var cMRSucreContext = _context.Certificados.Include(c => c.Socio);
            return View(await cMRSucreContext.ToListAsync());
        }

        // GET: Certificados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Certificados == null)
            {
                return NotFound();
            }

            var certificado = await _context.Certificados
                .Include(c => c.Socio)
                .FirstOrDefaultAsync(m => m.CertificadoId == id);
            if (certificado == null)
            {
                return NotFound();
            }

            return View(certificado);
        }

        // GET: Certificados/Create
        public IActionResult Create()
        {
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            ViewData["SocioId"] = new SelectList(_context.Socios, "SocioId", "Nombre");
            return View();
        }

        // POST: Certificados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CertificadoId,Emision,Cantidad,SocioId")] Certificado certificado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certificado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SocioId"] = new SelectList(_context.Socios, "SocioId", "Nombre", certificado.SocioId);
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            return View(certificado);
        }

        // GET: Certificados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Certificados == null)
            {
                return NotFound();
            }

            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return NotFound();
            }
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            ViewData["SocioId"] = new SelectList(_context.Socios, "SocioId", "Nombre", certificado.SocioId);
            return View(certificado);
        }

        // POST: Certificados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CertificadoId,Emision,Cantidad,SocioId")] Certificado certificado)
        {
            if (id != certificado.CertificadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificadoExists(certificado.CertificadoId))
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
            ViewData["SocioId"] = new SelectList(_context.Socios, "SocioId", "Nombre", certificado.SocioId);
            return View(certificado);
        }

        // GET: Certificados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Certificados == null)
            {
                return NotFound();
            }

            var certificado = await _context.Certificados
                .Include(c => c.Socio)
                .FirstOrDefaultAsync(m => m.CertificadoId == id);
            if (certificado == null)
            {
                return NotFound();
            }

            return View(certificado);
        }

        // POST: Certificados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Certificados == null)
            {
                return Problem("Entity set 'CMRSucreContext.Certificados'  is null.");
            }
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado != null)
            {
                _context.Certificados.Remove(certificado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificadoExists(int id)
        {
          return _context.Certificados.Any(e => e.CertificadoId == id);
        }
    }
}
