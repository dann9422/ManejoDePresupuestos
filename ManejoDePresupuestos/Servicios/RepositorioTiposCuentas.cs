using Dapper;
using ManejoDePresupuestos.Models;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace ManejoDePresupuestos.Servicios
{

    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string Nombre, int UsuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
    }



    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {

        private readonly string connectionString;

        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {

            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                ($@"INSERT INTO TiposCuentas (Nombre,UsuarioId,Orden)
                                                    VALUES(@Nombre,@UsuarioId,0)
                                                     SELECT SCOPE_IDENTITY();",tipoCuenta);
                                                     tipoCuenta.Id = id;
        }


        public async Task<bool> Existe(string Nombre, int UsuarioId)
        {
           var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstAsync<int>(
                                            @"SELECT 1 FROM TiposCuentas
                            WHERE Nombre =@Nombre AND UsuarioId =@UsuarioId;" 
                            ,new {Nombre,UsuarioId});

            return existe ==1;


        }


        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>("SELECT id,Nombre,Orden " +
                "FROM TiposCuentas WHERE UsuarioId = @UsuarioId", new {usuarioId}); 
        }
    }
}
