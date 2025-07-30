using Microsoft.EntityFrameworkCore;
using Prueba_Técnica_Factura.Data;
using Prueba_Técnica_Factura.DTOs;
using Prueba_Técnica_Factura.Models;

namespace Prueba_Técnica_Factura.Services
{
    public class FacturaOperations
    {
        private readonly ApplicationDbContext _context;

        public FacturaOperations(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener facturas con cliente y detalles
        public async Task<List<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(f => f.FacturaID == id);
        }

        public async Task<Factura> CreateAsync(Factura factura)
        {
            // Cálculo automático del total
            foreach (var detalle in factura.Detalles)
            {
                detalle.Total = detalle.Cantidad * detalle.PrecioUnitario;
            }

            factura.Total = factura.Detalles.Sum(d => d.Total);

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return factura; // Devuelve la entidad creada
        }

        public async Task UpdateAsync(Factura factura)
        {
            var existingFactura = await _context.Facturas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.FacturaID == factura.FacturaID);

            if (existingFactura == null)
                throw new Exception("Factura no encontrada");

            // Actualizar datos principales
            existingFactura.ClienteID = factura.ClienteID;
            existingFactura.Fecha = factura.Fecha;

            // Eliminar detalles antiguos
            _context.DetalleFacturas.RemoveRange(existingFactura.Detalles);

            // Agregar nuevos detalles
            existingFactura.Detalles = factura.Detalles;

            // Calcular total
            existingFactura.Total = factura.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.FacturaID == id);

            if (factura != null)
            {
                _context.DetalleFacturas.RemoveRange(factura.Detalles);
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cliente?> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public FacturaDTO MapToDTO(Factura factura)
        {
            return new FacturaDTO
            {
                FacturaID = factura.FacturaID,
                ClienteNombre = factura.Cliente?.Nombre ?? "",
                Fecha = factura.Fecha,
                Total = factura.Total,
                Detalles = factura.Detalles.Select(d => new DetalleFacturaDTO
                {
                    ProductoNombre = d.Producto?.Nombre ?? "",
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Total = d.Total
                }).ToList()
            };
        }
        public Factura MapFromCreateDTO(FacturaCreateDTO dto)
        {
            return new Factura
            {
                ClienteID = dto.ClienteID,
                Fecha = dto.Fecha,
                Detalles = dto.Detalles.Select(d => new DetalleFactura
                {
                    ProductoID = d.ProductoID,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Total = d.Cantidad * d.PrecioUnitario
                }).ToList(),
                Total = dto.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario)
            };
        }
    }
}
