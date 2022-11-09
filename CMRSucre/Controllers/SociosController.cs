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
    public class SociosController : Controller
    {
        private readonly CMRSucreContext _context;

        public SociosController(CMRSucreContext context)
        {
            _context = context;
        }

        // GET: Socios
        public async Task<IActionResult> Index()
        {
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            return View(await _context.Socios.ToListAsync());
        }

        // GET: Socios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Socios == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .FirstOrDefaultAsync(m => m.SocioId == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // GET: Socios/Create
        public IActionResult Create()
        {
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            return View();
        }

        // POST: Socios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SocioId,Nombre")] Socio socio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socio);
        }

        // GET: Socios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Socios == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            return View(socio);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SocioId,Nombre")] Socio socio)
        {
            if (id != socio.SocioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocioExists(socio.SocioId))
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
            return View(socio);
        }

        // GET: Socios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Socios == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .FirstOrDefaultAsync(m => m.SocioId == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Socios == null)
            {
                return Problem("Entity set 'CMRSucreContext.Socios'  is null.");
            }
            var socio = await _context.Socios.FindAsync(id);
            if (socio != null)
            {
                _context.Socios.Remove(socio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocioExists(int id)
        {
          return _context.Socios.Any(e => e.SocioId == id);
        }

        public async Task<IActionResult> Reporte()
        {
            //dynamic[,] arreglo = new dynamic[13, 12];
            string FD = ""; //Certificados con Antigüedad Minima (90 días)
            decimal DD; //Días Válidos sobre el Total de Días (360)
            decimal RA = 0; //Relación de Aportaciones y Dias Válidos
            decimal SumaRA = 0; //Suma de Relación de Aportaciones y Dias Válidos
            decimal DE; //Destribución Excedentes de Percepción
            decimal SumaDE = 0; //Suma de Destribución Excedentes de Percepción
            DateTime FC = _context.Configs.First().Fecha_Cierre; //Fecha de Cierre (o Retiro)
            decimal U = _context.Configs.First().Utilidad; //Utilidad

            var Socio_Certificado = (from s in _context.Socios
                                     join c in _context.Certificados on s.SocioId equals c.SocioId
                                     select new
                                     {
                                         Nro = c.CertificadoId,
                                         NroSocio = s.SocioId,
                                         Nombre = s.Nombre,
                                         NroCetificado = c.CertificadoId,
                                         FC = FC,
                                         F = c.Emision,
                                         D = ((FC-c.Emision).Days >= 360) ? 360 : (FC-c.Emision).Days, 
                                         C = c.Cantidad
                                     }).ToList();
            ViewBag.Socio_Certificado = Socio_Certificado;
            ViewBag.Utilidad = _context.Configs.First().Utilidad;

            foreach (var s_c in Socio_Certificado)
            {
                FD = (s_c.D >= 90) ? "CUMPLE" : "NO CUMPLE";
                //DD = (FD == "CUMPLE") ? Math.Round((s_c.D / 360), 2) : 0;
                if (FD == "CUMPLE")
                {
                    DD = Math.Round((Convert.ToDecimal(s_c.D) / 360), 2);
                }
                else
                    DD = 0;
                RA = (FD == "CUMPLE") ? (DD * s_c.C) : 0;
                SumaRA = SumaRA + RA;

                
                DE = (FD == "CUMPLE") ? (RA * U) / SumaRA : 0;
                SumaDE = SumaDE + DE;                
            }
            ViewBag.SumaRA = SumaRA;
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            //return View(await _context.Certificados.Include(s => s.Socio).ToListAsync());
            return View();
        }
    }
}
