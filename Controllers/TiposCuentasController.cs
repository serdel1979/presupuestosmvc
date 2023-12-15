using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TiposCuentasController: Controller
    {

        public IActionResult Crear()
        {

           
            return View();
        }


        [HttpPost]
        public ActionResult Crear(TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            return View();
        }


    }
}
