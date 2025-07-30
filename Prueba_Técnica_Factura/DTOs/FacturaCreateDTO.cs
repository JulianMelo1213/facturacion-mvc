namespace Prueba_Técnica_Factura.DTOs
{
    public class FacturaCreateDTO
    {
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleFacturaCreateDTO> Detalles { get; set; } = new();
    }
}
