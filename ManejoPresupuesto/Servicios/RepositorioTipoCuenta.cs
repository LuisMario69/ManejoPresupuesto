using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Services
{
    public interface IRepositorioTipoCuenta
    {
        
        Task crear(TipoCuentaViewModel tipoCuenta);
        Task<IEnumerable<TipoCuentaViewModel>> obtener(int idUsuario);
        Task<TipoCuentaViewModel> obtenerPorId(int id, int idUsuario);
        Task actualizar(TipoCuentaViewModel tipoCuenta);
        Task borrar(int idCuenta);
    }

    public class RepositorioTipoCuenta : IRepositorioTipoCuenta
    {
        private readonly string connectionString;
        private object usuarioId;

        public RepositorioTipoCuenta(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task crear(TipoCuentaViewModel tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                "sp_insertar_tipo_cuenta",
                new
                {
                    nombre_cuenta = tipoCuenta.GsNombreTipoCuenta,
                    fk_id_usuario = tipoCuenta.GsIdUsuario
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
            tipoCuenta.GsIdTipoCuenta = id;
        }

        public async Task<IEnumerable<TipoCuentaViewModel>> obtener(int idUsuario)
        {
            using var connection = new SqlConnection(connectionString);
            var tiposDeCuenta = await connection.QueryAsync<TipoCuentaViewModel>(
                @"SELECT id_tipo_cuenta AS GsIdTipoCuenta, nombre_cuenta AS GsNombreTipoCuenta " +
                "FROM tbl_tipo_cuenta WHERE fk_id_usuario = @idUsuario;",
                new { idUsuario }
            );
            return tiposDeCuenta;
        }
        public async Task<TipoCuentaViewModel> obtenerPorId(int id, int idUsuario)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuentaViewModel>(
                @"SELECT id_tipo_cuenta AS GsIdTipoCuenta, nombre_cuenta AS GsNombreTipoCuenta " +
                "FROM tbl_tipo_cuenta WHERE fk_id_usuario = @idUsuario AND id_tipo_cuenta = @id;",
                new { idUsuario, id });
        }

        public async Task actualizar(TipoCuentaViewModel tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE tbl_tipo_cuenta
                                         SET nombre_cuenta = @GsNombreTipoCuenta
                                         WHERE id_tipo_cuenta = @GsIdTipoCuenta;", tipoCuenta);
        }

        public async Task borrar(int idCuenta)
        {
            using var connetion = new SqlConnection(connectionString);
            await connetion.ExecuteAsync(@"DELETE tbl_tipo_cuenta WHERE id_tipo_cuenta =
                                          @idCuenta;", new {idCuenta});
        }
    }
}

