using Dapper;
using Proyecto_Programacion_IX_mk2.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Programacion_IX_mk2.Services
{
    public class ClientesServices
    {
        private SqlConnection _Conn = new();

        private static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=DESKTOP-8VOAJ1P;Initial Catalog=Proyecto Programacion IX;Integrated Security=True");


        }
        public async Task<IEnumerable<Clientes>> getClientesAsync()
        {
            _Conn = GetSqlConnection();
            _Conn.Open();

            var clientes = await _Conn.QueryAsync<Clientes>("SELECT [Id],[Nombre],[Apellido],[DPI],[Cumpleanios],[Edad] FROM [Proyecto Programacion IX].[dbo].[Clientes]");
            return clientes;
        }

        public Clientes getClientesByID(int id)
        {
            _Conn = GetSqlConnection();
            _Conn.Open();
            var clientes = _Conn.Query<Clientes>("SELECT [Id],[Nombre],[Apellido],[DPI],[Cumpleanios],[Edad] FROM [Proyecto Programacion IX].[dbo].[Clientes]").Where(f => f.Id == id).ToList();
            return clientes.Count != 0 ? clientes.First() : null;
        }
    }
}
