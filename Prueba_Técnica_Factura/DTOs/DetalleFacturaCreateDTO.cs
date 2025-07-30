namespace Prueba_Técnica_Factura.DTOs
{
    public class DetalleFacturaCreateDTO
    {
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
