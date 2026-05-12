using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;

using SOAP_PRODUCTOS.Contracts;
using SOAP_PRODUCTOS.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();

builder.Services.AddServiceModelMetadata();

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

    var smb =
        app.Services.GetRequiredService<ServiceMetadataBehavior>();

    smb.HttpGetEnabled = true;
});

app.Run();  