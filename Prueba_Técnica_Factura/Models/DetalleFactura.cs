using System.ComponentModel.DataAnnotations;

namespace Prueba_Técnica_Factura.Models
{
    public class DetalleFactura
    {
        public int DetalleFacturaID { get; set; }

        
        public int? FacturaID { get; set; }

        [Required]
        public int ProductoID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal PrecioUnitario { get; set; }

        public decimal Total { get; set; }

        public Factura? Factura { get; set; }
        public Producto? Producto { get; set; }
    }
}
