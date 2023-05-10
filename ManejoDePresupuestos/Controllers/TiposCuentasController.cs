using ManejoDePresupuestos.Models;
using Microsoft.AspNetCore.Mvc;
using ManejoDePresupuestos.Servicios;
using Microsoft.Data.SqlClient;

namespace ManejoDePresupuestos.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositoriTipoCuentas;

        public TiposCuentasController(IRepositorioTiposCuentas repositoriTipoCuentas)
        {
            this.repositoriTipoCuentas = repositoriTipoCuentas;
        }


        public async Task<IActionResult> Index()
        {
            var usuarioId = 1;
            var tiposCuentas = await repositoriTipoCuentas.Obtener(usuarioId);
            return View(tiposCuentas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult>Crear(TipoCuenta tipoCuenta)

         {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            tipoCuenta.UsuarioId = 1;

            var yaExisteTipoCuenta = await repositoriTipoCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre{tipoCuenta.Nombre} ya existe");

                return View(tipoCuenta);

            }

            await repositoriTipoCuentas.Crear(tipoCuenta);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = 1;
            var yaExisteTipoCuenta = await repositoriTipoCuentas.Existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"el nombre {nombre} ya existe");
            }
            return Json(true);
        } 



    }
}
