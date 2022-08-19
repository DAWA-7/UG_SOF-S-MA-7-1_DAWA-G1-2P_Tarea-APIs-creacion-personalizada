using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIClientes.Data;
using APIClientes.Models;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ClientesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("/clientbyedad")]
        //Retornar información de clientes que tenga edad entre 30 y 50 años
        public async Task<IEnumerable<Cliente>> Adults()
        {
            IQueryable<Cliente> query = _context.Cliente;
            query = query.Where(e => (e.Edad >= 30) && (e.Edad <= 50)).Include(e => e.Ciudad);
            if (query == null)
            {
                return null;
            }
            return await query.ToListAsync();
        }

        [HttpGet("{apellidos}/clientbyapellido")]
        //Retornar información de clientes cuyos apellidos comiencen según lo ingresado como parámetro
        public async Task<IEnumerable<Cliente>> SearchApellido(string apellidos)
        {
            IQueryable<Cliente> query = _context.Cliente;
            //string[] apellido = apellidos.Split(' ');
            query = query.Where(e => e.Apellidos.StartsWith(apellidos)).Include(e => e.Ciudad);
            if (query == null)
            {
                return null;
            }
            return await query.ToListAsync();
        }

        [HttpGet("{ciudad}/clientbyciudad")]
        //Retornar información de clientes que coincida con la CIUDAD ingresada por parámetros
        public async Task<IEnumerable<Cliente>> SearchCiudad(string ciudad)
        {
            IQueryable<Cliente> queryCl = _context.Cliente;
            IQueryable<Ciudad> queryCiu = _context.Ciudad;
            queryCiu = queryCiu.Where(e => e.Nombre_Ciudad.Contains(ciudad));
            var id = (from idc in queryCiu where idc.Nombre_Ciudad == ciudad select idc).First().Id;
            queryCl = queryCl.Where(e => e.Ciudad.Id == id).Include(e => e.Ciudad);
            if (queryCl == null)
            {
                return null;
            }
            return await queryCl.ToListAsync();
        }

        [HttpGet("{cedula}/clientbycedula")]

        public async Task<IEnumerable<Cliente>> Search(string cedula)
        {
            IQueryable<Cliente> query = _context.Cliente;
            //query = query.Where(e => e.Cedula == cedula);
            query = query.Where(e => e.Cedula.Contains(cedula)).Include(e => e.Ciudad);
            if (query == null)
            {
                return null;
            }
            return await query.ToListAsync();
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
          if (_context.Cliente == null)
          {
              return NotFound();
          }
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
          if (_context.Cliente == null)
          {
              return NotFound();
          }
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Cliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
          if (_context.Cliente == null)
          {
              return Problem("Entity set 'ApplicationDBContext.Clientes'  is null.");
          }
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
