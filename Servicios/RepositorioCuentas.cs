using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Servicios
{

    public interface IRepositorioCuentas
    {
        Task Crear(Cuenta cuenta);

        Task<IEnumerable<Cuenta>> Listar(int usuarioId);
    }

    public class RepositorioCuentas : IRepositorioCuentas
    {

        private readonly string connectString;
        public RepositorioCuentas(IConfiguration conn)
        {
            connectString = conn.GetConnectionString("DefaultConnection");
        }


       
        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(connectString);

            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Cuentas(Nombre, TipoCuentaId, Descripcion, Balance)" +
                "VALUES(@Nombre, @TipoCuentaId,@Descripcion,@Balance);" +
                "SELECT SCOPE_IDENTITY();", cuenta );

            cuenta.Id = id;
        }


        public async Task<IEnumerable<Cuenta>> Listar(int usuarioId)
        {
            using var connection = new SqlConnection(connectString);
            return await connection.QueryAsync<Cuenta>(@"select Cuentas.Id, Cuentas.Nombre, Balance, tc.Nombre as TipoCuenta, Cuentas.Descripcion
                                                            from Cuentas
                                                            inner join TiposCuentas as tc
                                                            on tc.Id = Cuentas.TipoCuentaId
                                                            WHERE tc.UsuarioId = @usuarioId
                                                            order by tc.Orden;", new { usuarioId });

        }


    }
}
