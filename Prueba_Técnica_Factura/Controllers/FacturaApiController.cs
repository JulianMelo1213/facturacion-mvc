using Microsoft.AspNetCore.Mvc;
using Prueba_Técnica_Factura.Services;
using Prueba_Técnica_Factura.Models;
using Prueba_Técnica_Factura.DTOs;

namespace Prueba_Técnica_Factura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaApiController : ControllerBase
    {
        private readonly FacturaOperations _facturaOps;

        public FacturaApiController(FacturaOperations facturaOps)
        {
            _facturaOps = facturaOps;
        }

        // GET: api/facturaapi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var facturas = await _facturaOps.GetAllAsync();
            var dtoList = facturas.Select(f => _facturaOps.MapToDTO(f)).ToList();
            return Ok(dtoList);
        }

        // GET: api/facturaapi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var factura = await _facturaOps.GetByIdAsync(id);
            if (factura == null)
                return NotFound();

            var dto = _facturaOps.MapToDTO(factura);
            return Ok(dto);
        }

        // POST: api/facturaapi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacturaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var factura = _facturaOps.MapFromCreateDTO(dto);
            var creada = await _facturaOps.CreateAsync(factura);
            return CreatedAtAction(nameof(GetById), new { id = creada.FacturaID }, _facturaOps.MapToDTO(creada));
        }

        // PUT: api/facturaapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FacturaCreateDTO dto)
        {
            var existe = await _facturaOps.GetByIdAsync(id);
            if (existe == null)
                return NotFound();

            // Creamos una nueva instancia con los datos del DTO
            var factura = _facturaOps.MapFromCreateDTO(dto);
            factura.FacturaID = id;

            await _facturaOps.UpdateAsync(factura);
            return NoContent();
        }

        // DELETE: api/facturaapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await _facturaOps.GetByIdAsync(id);
            if (existe == null)
                return NotFound();

            await _facturaOps.DeleteAsync(id);
            return NoContent();
        }
    }
}
