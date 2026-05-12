namespace SOAP_PRODUCTOS.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public string Categoria { get; set; }

        public DateTime FechaRegistro { get; set; }

        // INICIALIZAR LISTA

        public List<ProductoDetalle> Detalles
        {
            get;
            set;
        } = new List<ProductoDetalle>();
    }
}   