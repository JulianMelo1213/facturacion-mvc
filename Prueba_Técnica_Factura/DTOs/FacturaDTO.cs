namespace Prueba_Técnica_Factura.DTOs
{
    public class FacturaDTO
    {
        public int FacturaID { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleFacturaDTO> Detalles { get; set; } = new();
    }
}
