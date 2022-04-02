using Dapper;
using Proyecto_Programacion_IX_mk2.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Programacion_IX_mk2.Services
{
    public class VehiculosServices
    {
        private SqlConnection _Conn = new();

        private static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=DESKTOP-8VOAJ1P;Initial Catalog=Proyecto Programacion IX;Integrated Security=True");


        }
        public async Task<IEnumerable<Vehículos>> getVehiculosAsync()
        {
            _Conn = GetSqlConnection();
            _Conn.Open();

            var vehículos = await _Conn.QueryAsync<Vehículos>("SELECT [Id],[Marca],[Ancho],[Largo],[Color],[Placas] FROM [Proyecto Programacion IX].[dbo].[Vehículos]");
            return vehículos;
        }

        public Vehículos getVehiculoByID(int id)
        {
            _Conn = GetSqlConnection();
            _Conn.Open();
            var vehículos = _Conn.Query<Vehículos>("SELECT [Id],[Marca],[Ancho],[Largo],[Color],[Placas] FROM [Proyecto Programacion IX].[dbo].[Vehículos]").Where(f => f.Id == id).ToList();
            return vehículos.Count != 0 ? vehículos.First() : null;
        }
    }
}
