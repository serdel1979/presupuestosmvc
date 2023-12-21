using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Servicios;

namespace WebApplication1.Controllers
{
    public class CuentasController: Controller
    {
        private IServicioUsuarios servicioUsuarios;
        private IRepositorioTiposCuentas repositorioTipoCuentas;

        public CuentasController(IRepositorioTiposCuentas repoTipoCuentas, IServicioUsuarios servicioUsuarios)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioTipoCuentas = repoTipoCuentas;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.GetUsuarioId();
            var tiposCuentas = await repositorioTipoCuentas.Listar(usuarioId);

            var modelo = new CuentaCreacionViewModel();
            modelo.Tipo = tiposCuentas.Select(x=>new SelectListItem(x.Nombre, x.Id.ToString()));

            return View(modelo);
        }


    }
}
