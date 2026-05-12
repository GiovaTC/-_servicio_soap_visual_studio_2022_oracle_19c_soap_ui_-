namespace SOAP_PRODUCTOS.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Categoria { get; set; }
        public String FechaRegistro { get; set; }
        public List<ProductoDetalle> Detalles { get; set; }
    }
}   
