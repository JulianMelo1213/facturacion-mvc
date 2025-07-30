namespace Prueba_Técnica_Factura.DTOs
{
    public class DetalleFacturaDTO
    {
        public string ProductoNombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
