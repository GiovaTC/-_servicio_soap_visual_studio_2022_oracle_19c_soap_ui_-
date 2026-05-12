using CoreWCF;
using SOAP_PRODUCTOS.Models;    

namespace SOAP_PRODUCTOS.Contracts
{
    [ServiceContract]
    public interface IProductoService
    {
        [OperationContract]
        string RegistrarProducto(Producto producto);
    }
}   
