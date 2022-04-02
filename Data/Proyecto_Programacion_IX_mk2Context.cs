using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto_Programacion_IX_mk2.Classes;

namespace Proyecto_Programacion_IX_mk2.Data
{
    public class Proyecto_Programacion_IX_mk2Context : DbContext
    {
        public Proyecto_Programacion_IX_mk2Context (DbContextOptions<Proyecto_Programacion_IX_mk2Context> options)
            : base(options)
        {
        }

        public DbSet<Proyecto_Programacion_IX_mk2.Classes.Clientes> Clientes { get; set; }

        public DbSet<Proyecto_Programacion_IX_mk2.Classes.Viajes> Viajes { get; set; }

        public DbSet<Proyecto_Programacion_IX_mk2.Classes.Vehículos> Vehículos { get; set; }
    }
}
