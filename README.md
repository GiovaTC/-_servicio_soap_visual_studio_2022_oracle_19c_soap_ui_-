# -_servicio_soap_visual_studio_2022_oracle_19c_soap_ui_- :.
Servicio SOAP en Visual Studio 2022 + Oracle 19c + SOAP UI:

<img width="1254" height="1254" alt="image" src="https://github.com/user-attachments/assets/f696fb66-bec2-49ef-a99d-a4fe06ea12a9" />    

<img width="1365" height="767" alt="image" src="https://github.com/user-attachments/assets/8aed62f9-0b49-46de-9337-0a8b84e6c766" />    

<img width="2553" height="1075" alt="image" src="https://github.com/user-attachments/assets/326fa60b-001b-4dab-8f0e-7edff32d4a30" />    

```
Tecnologías Utilizadas
Visual Studio 2022
ASP.NET Core
CoreWCF (SOAP)
Oracle 19c
SOAP UI
.NET 8
Oracle Managed Data Access
Arquitectura del Proyecto .

SOAP_PRODUCTOS
│
├── Contracts
│   └── IProductoService.cs
│
├── Models
│   ├── Producto.cs
│   └── ProductoDetalle.cs
│
├── Services
│   └── ProductoService.cs
│
├── Data
│   └── ConexionOracle.cs
│
├── Program.cs
├── appsettings.json
└── SOAP_PRODUCTOS.sln

1. Crear Proyecto en Visual Studio 2022
Crear Proyecto
Seleccionar:
ASP.NET Core Empty
Nombre:
SOAP_PRODUCTOS
Framework:
.NET 8

2. Instalar Paquetes NuGet
Abrir:
Herramientas
→ Administrador de paquetes NuGet
→ Consola del Administrador

Instalar:
Install-Package CoreWCF.Http
Install-Package CoreWCF.Primitives
Install-Package Oracle.ManagedDataAccess.Core

3. Script Oracle 19c
Tabla Principal
CREATE TABLE PRODUCTOS (
    ID_PRODUCTO NUMBER PRIMARY KEY,
    NOMBRE VARCHAR2(100),
    CATEGORIA VARCHAR2(100),
    FECHA_REGISTRO DATE
);

Tabla Detalle
CREATE TABLE PRODUCTOS_DETALLE (
    ID_DETALLE NUMBER PRIMARY KEY,
    ID_PRODUCTO NUMBER,
    CODIGO VARCHAR2(50),
    NOMBRE VARCHAR2(100),
    DESCRIPCION VARCHAR2(200),
    PRECIO NUMBER(10,2),
    STOCK NUMBER,
    MARCA VARCHAR2(100),
    ESTADO VARCHAR2(50),

    CONSTRAINT FK_PRODUCTO
    FOREIGN KEY(ID_PRODUCTO)
    REFERENCES PRODUCTOS(ID_PRODUCTO)
);

4. JSON Solicitado.
{
  "producto": {
    "idProducto": 1,
    "nombre": "Laptop Gamer",
    "categoria": "Tecnologia",
    "fechaRegistro": "2026-05-12",
    "detalles": [
      {
        "codigo": "P001",
        "nombre": "Mouse Gamer",
        "descripcion": "RGB Profesional",
        "precio": 150000,
        "stock": 5,
        "marca": "Logitech",
        "estado": "Activo"
      },
      {
        "codigo": "P002",
        "nombre": "Teclado",
        "descripcion": "Teclado Mecanico",
        "precio": 250000,
        "stock": 3,
        "marca": "Redragon",
        "estado": "Activo"
      },
      {
        "codigo": "P003",
        "nombre": "Monitor",
        "descripcion": "27 Pulgadas",
        "precio": 950000,
        "stock": 2,
        "marca": "Samsung",
        "estado": "Activo"
      },
      {
        "codigo": "P004",
        "nombre": "Audifonos",
        "descripcion": "Audio 7.1",
        "precio": 180000,
        "stock": 4,
        "marca": "HyperX",
        "estado": "Activo"
      },
      {
        "codigo": "P005",
        "nombre": "Camara",
        "descripcion": "HD Streaming",
        "precio": 350000,
        "stock": 6,
        "marca": "Logitech",
        "estado": "Activo"
      }
    ]
  }
}

5. Modelo Producto
Models/Producto.cs
namespace SOAP_PRODUCTOS.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public string Categoria { get; set; }

        public DateTime FechaRegistro { get; set; }

        public List<ProductoDetalle> Detalles { get; set; }
    }
}

6. Modelo ProductoDetalle
Models/ProductoDetalle.cs
namespace SOAP_PRODUCTOS.Models
{
    public class ProductoDetalle
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string Marca { get; set; }

        public string Estado { get; set; }
    }
}

7. Contrato SOAP
Contracts/IProductoService.cs
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

8. Servicio SOAP
Services/ProductoService.cs
using Oracle.ManagedDataAccess.Client;
using SOAP_PRODUCTOS.Contracts;
using SOAP_PRODUCTOS.Models;

namespace SOAP_PRODUCTOS.Services
{
    public class ProductoService : IProductoService
    {
        private readonly string conexion =
            "User Id=SYSTEM;Password=123456;Data Source=localhost:1521/XEPDB1";

        public string RegistrarProducto(Producto producto)
        {
            using OracleConnection cn =
                new OracleConnection(conexion);

            cn.Open();

            string sqlProducto =
            @"INSERT INTO PRODUCTOS
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

            cmd.Parameters.Add(":ID",
                producto.IdProducto);

            cmd.Parameters.Add(":NOMBRE",
                producto.Nombre);

            cmd.Parameters.Add(":CATEGORIA",
                producto.Categoria);

            cmd.Parameters.Add(":FECHA",
                producto.FechaRegistro);

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

9. Configuración Program.cs
using CoreWCF;
using CoreWCF.Configuration;
using SOAP_PRODUCTOS.Contracts;
using SOAP_PRODUCTOS.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();

builder.Services.AddSingleton<IProductoService,
    ProductoService>();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<ProductoService>();

    serviceBuilder.AddServiceEndpoint
    <
        ProductoService,
        IProductoService
    >
    (
        new BasicHttpBinding(),
        "/ProductoService.svc"
    );
});

app.Run();

10. Ejecutar Proyecto
Ejecutar
CTRL + F5
URL del Servicio SOAP
http://localhost:5000/ProductoService.svc

11. Probar con SOAP UI
Abrir SOAP UI
Seleccionar:
File
→ New SOAP Project
WSDL
Ingresar:
http://localhost:5000/ProductoService.svc?wsdl

12. Request XML SOAP UI
<soapenv:Envelope
xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"
xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:RegistrarProducto>
         <tem:producto>
            <tem:IdProducto>1</tem:IdProducto>
            <tem:Nombre>
                Laptop Gamer
            </tem:Nombre>
            <tem:Categoria>
                Tecnologia
            </tem:Categoria>
            <tem:FechaRegistro>
                2026-05-12T00:00:00
            </tem:FechaRegistro>
         </tem:producto>
      </tem:RegistrarProducto>
   </soapenv:Body>
</soapenv:Envelope>

13. Respuesta Esperada SOAP UI
<s:Envelope
xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
   <s:Body>
      <RegistrarProductoResponse
      xmlns="http://tempuri.org/">
         <RegistrarProductoResult>
            PRODUCTO REGISTRADO CORRECTAMENTE
         </RegistrarProductoResult>
      </RegistrarProductoResponse>
   </s:Body>
</s:Envelope>

14. Consultar Oracle
Tabla Principal
SELECT * FROM PRODUCTOS;

Tabla Detalle
SELECT * FROM PRODUCTOS_DETALLE;

15. Resultado Esperado Oracle
PRODUCTOS
ID	NOMBRE	CATEGORIA
1	Laptop Gamer	Tecnologia
PRODUCTOS_DETALLE
CODIGO	NOMBRE	PRECIO
P001	Mouse Gamer	150000
P002	Teclado	250000
P003	Monitor	950000
P004	Audifonos	180000
P005	Camara	350000

16. Mejoras Empresariales
✔ Repository Pattern
✔ DTO
✔ AutoMapper
✔ Logs
✔ Stored Procedures
✔ JWT
✔ Swagger REST
✔ HTTPS
✔ Docker
✔ IIS
✔ XML Validation
✔ Oracle Packages
✔ Manejo Global de Excepciones
✔ Arquitectura Hexagonal .
:. . / .
