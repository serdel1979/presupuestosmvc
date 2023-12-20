using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Servicios
{

    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Listar(int usuarioId);

        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Eliminar(int id);
    }

    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        private readonly string connectString;
        public RepositorioTiposCuentas(IConfiguration conn )
        {
            connectString = conn.GetConnectionString("DefaultConnection");
        }


        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectString);

            var id = await connection.QuerySingleAsync<int>(@"
                        INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden) 
                        VALUES (@Nombre, @UsuarioId, 0);
                        SELECT SCOPE_IDENTITY();", tipoCuenta);

            tipoCuenta.Id = id;
        }


        public async Task<bool> Existe(string nombre, int idUsuario)
        {
            using var connection = new SqlConnection(connectString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM TiposCuentas WHERE (Nombre = @nombre AND UsuarioId = @idUsuario);",
                new {nombre, idUsuario}
                );

            return (existe == 1);

        }

        public async Task<IEnumerable<TipoCuenta>> Listar(int usuarioId)
        {
            using var connection = new SqlConnection(connectString);
            return await connection.QueryAsync<TipoCuenta>(@"SELECT * FROM TiposCuentas WHERE UsuarioId = @usuarioId;",
                new { usuarioId });

        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectString);

            var query = @"
                    UPDATE TiposCuentas
                    SET Nombre = @Nombre
                    WHERE Id = @Id;
    ";

            await connection.ExecuteAsync(query,tipoCuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectString);

            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden FROM TiposCuentas WHERE Id = @Id and UsuarioId = @UsuarioId",
                new { id, usuarioId});

        }


        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectString);

            await connection.ExecuteAsync(@"DELETE TiposCuentas WHERE Id = @Id",
                new { id });

        }

    }
}
