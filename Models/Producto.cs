using System.Runtime.Serialization;

namespace SOAP_PRODUCTOS.Models
{
    [DataContract]
    public class Producto
    {
        [DataMember]
        public int IdProducto { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Categoria { get; set; }

        [DataMember]
        public DateTime FechaRegistro { get; set; }

        [DataMember]
        public List<ProductoDetalle> Detalles
        {
            get;
            set;
        } = new List<ProductoDetalle>();
    }
}   