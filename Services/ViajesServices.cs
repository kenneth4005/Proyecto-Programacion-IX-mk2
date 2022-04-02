using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Proyecto_Programacion_IX_mk2.Classes;

namespace Proyecto_Programacion_IX_mk2.Services
{
    public class ViajesServices
    {
        private SqlConnection _Conn = new();

        private static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=DESKTOP-8VOAJ1P;Initial Catalog=Proyecto Programacion IX;Integrated Security=True");

            
        }
        public async Task<IEnumerable<Viajes>> getVehiculosAsync()
        {
            _Conn = GetSqlConnection();
            _Conn.Open();
            
            var viajes = await _Conn.QueryAsync<Viajes>("SELECT [Id] ,[Destino] ,[TiempoViajeH] ,[Nombre] FROM [Proyecto Programacion IX].[dbo].[Viajes]");
            return viajes;
        }

        public Viajes getVehiculoByID(int id)
        {
            _Conn = GetSqlConnection();
            _Conn.Open();
            var viajes = _Conn.Query<Viajes>("SELECT [Id] ,[Destino] ,[TiempoViajeH] ,[Nombre] FROM [Proyecto Programacion IX].[dbo].[Viajes]").Where(f => f.Id == id).ToList();
            return viajes.Count != 0 ? viajes.First() : null;
        }



    }
}
