using Microsoft.AspNetCore.Mvc;
using Prueba_Técnica_Factura.Models;
using Prueba_Técnica_Factura.Services;
using Microsoft.Extensions.Logging;

namespace Prueba_Técnica_Factura.Controllers
{
    public class FacturaController : Controller
    {
        private readonly FacturaOperations _facturaOps;
        private readonly ILogger<FacturaController> _logger;

        public FacturaController(FacturaOperations facturaOps, ILogger<FacturaController> logger)
        {
            _facturaOps = facturaOps;
            _logger = logger;
        }

        // GET: /Factura
        public async Task<IActionResult> Index()
        {
            var facturas = await _facturaOps.GetAllAsync();
            return View(facturas);
        }

        // GET: /Factura/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var factura = await _facturaOps.GetByIdAsync(id);
            if (factura == null)
                return NotFound();

            return View(factura);
        }

        // GET: /Factura/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = await _facturaOps.GetClientesAsync();
            ViewBag.Productos = await _facturaOps.GetProductosAsync();

            return View(new Factura
            {
                Fecha = DateTime.Now,
                Detalles = new List<DetalleFactura> { new() }
            });
        }

        // POST: /Factura/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Factura factura)
        {
            _logger.LogInformation("POST /Factura/Create ejecutado.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Formulario no válido");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Error de validación: {error.ErrorMessage}");
                }

                ViewBag.Clientes = await _facturaOps.GetClientesAsync();
                ViewBag.Productos = await _facturaOps.GetProductosAsync();
                return View(factura);
            }

            if (factura.Detalles == null || !factura.Detalles.Any())
            {
                _logger.LogWarning("⚠️ No se recibieron detalles.");
            }
            else
            {
                _logger.LogInformation($"🟢 Se recibieron {factura.Detalles.Count} detalle(s).");
                foreach (var d in factura.Detalles)
                {
                    _logger.LogInformation($"- ProductoID: {d.ProductoID}, Cantidad: {d.Cantidad}, Precio: {d.PrecioUnitario}, Total: {d.Total}");
                }
            }

            _logger.LogInformation($"ClienteID: {factura.ClienteID}, Fecha: {factura.Fecha}");

            await _facturaOps.CreateAsync(factura);

            _logger.LogInformation("✅ Factura guardada exitosamente.");
            return RedirectToAction(nameof(Index));
        }

        // GET: /Factura/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var factura = await _facturaOps.GetByIdAsync(id);
            if (factura == null)
                return NotFound();

            ViewBag.Clientes = await _facturaOps.GetClientesAsync();
            ViewBag.Productos = await _facturaOps.GetProductosAsync();
            return View(factura);
        }

        // POST: /Factura/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Factura factura)
        {
            if (ModelState.IsValid)
            {
                await _facturaOps.UpdateAsync(factura);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = await _facturaOps.GetClientesAsync();
            ViewBag.Productos = await _facturaOps.GetProductosAsync();
            return View(factura);
        }

        // GET: /Factura/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var factura = await _facturaOps.GetByIdAsync(id);
            if (factura == null)
                return NotFound();

            return View(factura);
        }

        // POST: /Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _facturaOps.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // AJAX: /Factura/ClienteInfo/5
        public async Task<IActionResult> ClienteInfo(int id)
        {
            var cliente = await _facturaOps.GetClienteByIdAsync(id);
            if (cliente == null)
                return NotFound();

            return Json(cliente);
        }
    }
}
