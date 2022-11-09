using CMRSucre.Data;
using CMRSucre.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMRSucre.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CMRSucreContext _context; 

        public HomeController(ILogger<HomeController> logger, CMRSucreContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Utilidad = _context.Configs.First().Utilidad;
            ViewBag.FechaCierre = _context.Configs.First().Fecha_Cierre;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}