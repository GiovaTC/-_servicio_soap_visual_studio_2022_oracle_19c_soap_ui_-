using Oracle.ManagedDataAccess.Client;
using SOAP_PRODUCTOS.Contracts;
using SOAP_PRODUCTOS.Models;

namespace SOAP_PRODUCTOS.Services
{
    public class ProductoService : IProductoService
    {
        private readonly string conexion =
            "User Id=system;Password=Tapiero123;Data Source=localhost:1521/orcl";

        public string RegistrarProducto(Producto producto)
        {
            using OracleConnection cn =
                new OracleConnection(conexion);

            cn.Open();

            string sqlProducto =
                @"INSERT INTO PRODUCTOS_I
                (
                    ID_PRODUCTO, 
                    NOMBRE, 
                    CATEGORIA, 
                    FECHA_REGISTRO
                ) 
                VALUES 
                (
                    :ID, 
                    :NOMBRE, 
                    :CATEGORIA, 
                    :FECHA
                )";

            OracleCommand cmd = 
                new OracleCommand(sqlProducto, cn);

            cmd.Parameters.Add(":ID", producto.IdProducto);
            cmd.Parameters.Add(":NOMBRE", producto.Nombre);
            cmd.Parameters.Add(":CATEGORIA", producto.Categoria);
            cmd.Parameters.Add(":FECHA",
                producto.FechaRegistro);
            
            // ejecuta en bd .
            cmd.ExecuteNonQuery();

            int idDetalle = 1;

            foreach (var d in producto.Detalles)
            {
                string sqlDetalle =
                @"INSERT INTO PRODUCTOS_DETALLE
                (
                    ID_DETALLE,
                    ID_PRODUCTO,
                    CODIGO,
                    NOMBRE,
                    DESCRIPCION,
                    PRECIO,
                    STOCK,
                    MARCA,
                    ESTADO
                )
                VALUES
                (
                    :IDDETALLE,
                    :IDPRODUCTO,
                    :CODIGO,
                    :NOMBRE,
                    :DESCRIPCION,
                    :PRECIO,
                    :STOCK,
                    :MARCA,
                    :ESTADO
                )";

                OracleCommand detalleCmd =
                    new OracleCommand(sqlDetalle, cn);

                detalleCmd.Parameters.Add(":IDDETALLE",
                    idDetalle++);

                detalleCmd.Parameters.Add(":IDPRODUCTO",
                    producto.IdProducto);

                detalleCmd.Parameters.Add(":CODIGO",
                    d.Codigo);

                detalleCmd.Parameters.Add(":NOMBRE",
                    d.Nombre);

                detalleCmd.Parameters.Add(":DESCRIPCION",
                    d.Descripcion);

                detalleCmd.Parameters.Add(":PRECIO",
                    d.Precio);

                detalleCmd.Parameters.Add(":STOCK",
                    d.Stock);

                detalleCmd.Parameters.Add(":MARCA",
                    d.Marca);

                detalleCmd.Parameters.Add(":ESTADO",
                    d.Estado);

                detalleCmd.ExecuteNonQuery();
            }

            cn.Close();

            return "PRODUCTO REGISTRADO CORRECTAMENTE";
        }
    }
}   
