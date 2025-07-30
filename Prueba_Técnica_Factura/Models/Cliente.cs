using System.ComponentModel.DataAnnotations;

namespace Prueba_Técnica_Factura.Models
{
    public class Cliente
    {
        public int ClienteID {  get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string CedulaRnc {  get; set; }

        [Required]
        public int TipoDocumento { get; set; }

        public ICollection<Factura>? Facturas { get; set; }
    }
}
