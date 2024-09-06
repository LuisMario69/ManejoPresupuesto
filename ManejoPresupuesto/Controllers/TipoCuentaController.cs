using ManejoPresupuesto.Models;
using ManejoPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Controllers
{
    public class TipoCuentaController : Controller
    {
        private readonly IRepositorioTipoCuenta repositorioTipoCuenta;

        public TipoCuentaController(IRepositorioTipoCuenta repositorioTipoCuenta)
        {
            this.repositorioTipoCuenta = repositorioTipoCuenta;
        }

        public async Task<IActionResult> Index()
        {
            var tipoCuenta = await repositorioTipoCuenta.obtener(1); // Asegúrate de que el método `Obtener` esté bien definido en `IRepositorioTipoCuenta`.
            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> crear(TipoCuentaViewModel tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }

            tipoCuenta.GsIdUsuario = 1; // Establece el ID del usuario aquí.
            await repositorioTipoCuenta.crear(tipoCuenta); // Asegúrate de que el método `Crear` esté bien definido en `IRepositorioTipoCuenta`.

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> editar(int gsId)
        {
            var idUsuario = 1;
            var tipoCuenta = await repositorioTipoCuenta.obtenerPorId(gsId, idUsuario);

            if (tipoCuenta is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> editar(TipoCuentaViewModel tipoCuenta)
        {
            var idUsiario = 1;
            var tipoCuentaExiste = await repositorioTipoCuenta.obtenerPorId(tipoCuenta.GsIdTipoCuenta, idUsiario);

            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("Error", "Home");
            }
            await repositorioTipoCuenta.actualizar(tipoCuenta);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> borrar(int id)
        {
            var idUsuario = 1;
            var tipoCuenta = await repositorioTipoCuenta.obtenerPorId(id, idUsuario);

            if (tipoCuenta is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> borrarTipoCuenta(TipoCuentaViewModel tipoCuenta)
        {
            var idUsuario = 1;

            var tipoCuentaExiste = await repositorioTipoCuenta.obtenerPorId(tipoCuenta.GsIdTipoCuenta, idUsuario);

            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("Error", "Home");
            }

            await repositorioTipoCuenta.borrar(tipoCuenta.GsIdTipoCuenta);
            return RedirectToAction("Index");
        }
    }
}

