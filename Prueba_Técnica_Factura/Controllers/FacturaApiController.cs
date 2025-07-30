using Microsoft.AspNetCore.Mvc;
using Prueba_Técnica_Factura.Services;
using Prueba_Técnica_Factura.Models;

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
            return Ok(facturas);
        }

        // GET: api/facturaapi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var factura = await _facturaOps.GetByIdAsync(id);
            if (factura == null)
                return NotFound();

            return Ok(factura);
        }

        // POST: api/facturaapi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Factura nuevaFactura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creada = await _facturaOps.CreateAsync(nuevaFactura);
            return CreatedAtAction(nameof(GetById), new { id = creada.FacturaID }, creada);
        }

        // PUT: api/facturaapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Factura facturaActualizada)
        {
            if (id != facturaActualizada.FacturaID)
                return BadRequest("El ID no coincide.");

            var existe = await _facturaOps.GetByIdAsync(id);
            if (existe == null)
                return NotFound();

            await _facturaOps.UpdateAsync(facturaActualizada);
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
