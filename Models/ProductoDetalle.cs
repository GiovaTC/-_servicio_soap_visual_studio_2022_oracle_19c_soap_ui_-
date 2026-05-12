using System.Runtime.Serialization;

namespace SOAP_PRODUCTOS.Models
{
    [DataContract]
    public class ProductoDetalle
    {
        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public decimal Precio { get; set; }

        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public string Marca { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}   