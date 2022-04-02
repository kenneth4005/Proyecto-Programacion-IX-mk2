using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto_Programacion_IX_mk2.Classes;
using Proyecto_Programacion_IX_mk2.Data;
using Proyecto_Programacion_IX_mk2.Services;

namespace Proyecto_Programacion_IX_mk2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly Proyecto_Programacion_IX_mk2Context _context;

        public ClientesController(Proyecto_Programacion_IX_mk2Context context)
        {
            _context = context;
        }

        //// GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        //// GET: api/Clientes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Clientes>> GetClientes(int id)
        //{
        //    var clientes = await _context.Clientes.FindAsync(id);

        //    if (clientes == null)
        //    {
        //        return NotFound();
        //    }

        //    return clientes;
        //}

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpGet]
        [Route("ASYNC")]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetVehiculosAsync()
        {
            var clientesServices = new ClientesServices();
            {
                var vehículos = await clientesServices.getClientesAsync();
                if (vehículos != null)
                {
                    return Ok(vehículos);
                }
                return NotFound("Mensaje: No hay Productos");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Clientes> GetVehiculosByID(int id)
        {
            var clientesServices = new ClientesServices();
            {
                var clientes = clientesServices.getClientesByID(id);
                if (clientes != null)
                {
                    return Ok(clientes);
                }
                return NotFound("Mensaje: No hay produtos con el ID:" + id);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            _context.Clientes.Add(clientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientes", new { id = clientes.Id }, clientes);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{json}")]
        public async Task<ActionResult<Clientes>> MassiveJsonLoad(string json)
        {

            List<Clientes> vehiculos = JsonConvert.DeserializeObject<List<Clientes>>(json);
            foreach (var item in vehiculos)
            {
                _context.Clientes.Add(item);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }



        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
