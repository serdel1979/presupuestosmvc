using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TiposCuentasController: Controller
    {

        private readonly string connectionString;

        public TiposCuentasController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }


        public IActionResult Crear()
        {

            using (var conn = new SqlConnection(connectionString))
            {
                var query = conn.Query("SELECT 1").FirstOrDefault();
            }

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
