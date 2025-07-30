using System.ComponentModel.DataAnnotations;


namespace Prueba_Técnica_Factura.Models
{
    public class Factura
    {
        public int FacturaID { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        public int? ClienteID { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public decimal Total {  get; set; }

        public Cliente? Cliente { get; set; }

        [MinLength(1, ErrorMessage = "Debe agregar al menos un detalle.")]
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();

    }
}
