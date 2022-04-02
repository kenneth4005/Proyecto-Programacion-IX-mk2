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
    public class VehículosController : ControllerBase
    {
        private readonly Proyecto_Programacion_IX_mk2Context _context;

        public VehículosController(Proyecto_Programacion_IX_mk2Context context)
        {
            _context = context;
        }

        //GET: api/Vehículos
       [HttpGet]
       [Route("GetAsync")]
        public async Task<ActionResult<IEnumerable<Vehículos>>> GetVehículos()
        {
            return await _context.Vehículos.ToListAsync();
        }

        // GET: api/Vehículos/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Vehículos>> GetVehículos(int id)
        //{
        //    var vehículos = await _context.Vehículos.FindAsync(id);

        //    if (vehículos == null)
        //    {
        //        return NotFound();
        //    }

        //    return vehículos;
        //}

        // PUT: api/Vehículos/5

        [HttpGet]
        [Route("ASYNC")]
        public async Task<ActionResult<IEnumerable<Vehículos>>> GetVehiculosAsync()
        {
            var vehiculosServices = new VehiculosServices();
            {
                var vehículos = await vehiculosServices.getVehiculosAsync();
                if (vehículos != null)
                {
                    return Ok(vehículos);
                }
                return NotFound("Mensaje: No hay Productos");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Vehículos> GetVehiculosByID(int id)
        {
            var vehiculosServices = new VehiculosServices();
            {
                var vehículos = vehiculosServices.getVehiculoByID(id);
                if (vehículos != null)
                {
                    return Ok(vehículos);
                }
                return NotFound("Mensaje: No hay produtos con el ID:" + id);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehículos(int id, Vehículos vehículos)
        {
            if (id != vehículos.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehículos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehículosExists(id))
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

        // POST: api/Vehículos
        [HttpPost]
        public async Task<ActionResult<Vehículos>> PostVehículos(Vehículos vehículos)
        {
            _context.Vehículos.Add(vehículos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehículos", new { id = vehículos.Id }, vehículos);
        }

        // DELETE: api/Vehículos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehículos(int id)
        {
            var vehículos = await _context.Vehículos.FindAsync(id);
            if (vehículos == null)
            {
                return NotFound();
            }

            _context.Vehículos.Remove(vehículos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{json}")]
        public async Task<ActionResult<Vehículos>> MassiveJsonLoad(string json)
        {

            List<Vehículos> vehiculos = JsonConvert.DeserializeObject<List<Vehículos>>(json);
            foreach (var item in vehiculos)
            {
                _context.Vehículos.Add(item);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool VehículosExists(int id)
        {
            return _context.Vehículos.Any(e => e.Id == id);
        }
    }
}
