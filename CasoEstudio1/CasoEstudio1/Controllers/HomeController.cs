using System.Diagnostics;
using CasoEstudio1.Data;
using CasoEstudio1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasoEstudio1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEjerciciosRepository _repository;

        public HomeController(ILogger<HomeController> logger, IEjerciciosRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Acciones de Pagina Principal
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Acciones de Registro
        [HttpGet]
        public IActionResult Registrar()
        {
            return View(new EjercicioCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(EjercicioCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (Success, Message) = await _repository.CreateAsync(model);

            if (!Success)
            {
                ModelState.AddModelError(string.Empty, Message ?? "No se pudo registrar la solicitud.");
                return View(model);
            }

            TempData["Success"] = "Solicitud registrada correctamente.";
            return RedirectToAction(nameof(Consultar));
        }
        #endregion

        #region Acciones de Consulta
        public async Task<IActionResult> Consultar()
        {
            var items = await _repository.GetAllAsync();
            return View(items);
        }

        #endregion

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
