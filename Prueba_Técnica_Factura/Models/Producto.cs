using System.ComponentModel.DataAnnotations;

namespace Prueba_Técnica_Factura.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }

        [Required]
        public string Nombre { get; set; }

        public decimal Precio { get; set; }
        public decimal PrecioMayor { get; set; }
    }
}
