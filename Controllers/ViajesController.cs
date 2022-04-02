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
    public class ViajesController : ControllerBase
    {
        private readonly Proyecto_Programacion_IX_mk2Context _context;

        public ViajesController(Proyecto_Programacion_IX_mk2Context context)
        {
            _context = context;
        }

        // GET: api/Viajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viajes>>> GetViajes()
        {
            return await _context.Viajes.ToListAsync();
        }

        //// GET: api/Viajes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Viajes>> GetViajes(int id)
        //{
        //    var viajes = await _context.Viajes.FindAsync(id);

        //    if (viajes == null)
        //    {
        //        return NotFound();
        //    }

        //    return viajes;
        //}


        [HttpGet]
        [Route("ASYNC")]
        public async Task<ActionResult<IEnumerable<Viajes>>> GetFacturaAsync()
        {
            var viajesServices = new ViajesServices();
            {
                var viajes = await viajesServices.getVehiculosAsync();
                if (viajes != null)
                {
                    return Ok(viajes);
                }
                return NotFound("Mensaje: No hay Productos");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Viajes> GetFacturasByID(int id)
        {
            var viajesServices = new ViajesServices();
            {
                var viajes = viajesServices.getVehiculoByID(id);
                if (viajes != null)
                {
                    return Ok(viajes);
                }
                return NotFound("Mensaje: No hay produtos con el ID:" + id);
            }
        }





        // PUT: api/Viajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViajes(int id, Viajes viajes)
        {
            if (id != viajes.Id)
            {
                return BadRequest();
            }

            _context.Entry(viajes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViajesExists(id))
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

        // POST: api/Viajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Viajes>> PostViajes(Viajes viajes)
        {
            _context.Viajes.Add(viajes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViajes", new { id = viajes.Id }, viajes);
        }

        // DELETE: api/Viajes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViajes(int id)
        {
            var viajes = await _context.Viajes.FindAsync(id);
            if (viajes == null)
            {
                return NotFound();
            }

            _context.Viajes.Remove(viajes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{json}")]
        public async Task<ActionResult<Clientes>> MassiveJsonLoad(string json)
        {

            List<Viajes> vehiculos = JsonConvert.DeserializeObject<List<Viajes>>(json);
            foreach (var item in vehiculos)
            {
                _context.Viajes.Add(item);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }



        private bool ViajesExists(int id)
        {
            return _context.Viajes.Any(e => e.Id == id);
        }
    }
}
