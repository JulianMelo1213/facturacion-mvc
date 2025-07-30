\# 🧾 Sistema de Facturación MVC

Este es un sistema de facturación desarrollado como parte de una prueba
técnica, utilizando ASP.NET Core MVC, Entity Framework Core y SQL
Server.

Permite crear, editar y eliminar facturas, con cálculo automático de
totales por producto y factura.

\-\--

\## Características

\- CRUD de Facturas - Relación entre Cliente → Factura → Detalles →
Productos - Cálculo dinámico de precios (precio mayorista si cantidad \>
12) - Validaciones de formulario (cliente requerido, al menos un
detalle) - Precio unitario autocompletado y protegido según reglas -
Confirmación de eliminación

\-\--

\## Tecnologías Usadas

\- ASP.NET Core  - MVC Architecture - Entity Framework Core - SQL
Server - HTML, CSS y Javascript

\-\--

\## Estructura del Proyecto

Prueba_Técnica_Factura/ -Controllers/ -Models/ -Services/ -Views/
-wwwroot/ -Program.cs -appsettings.json -script_facturacionDB.sql

\-\--

##Instalación y Configuración

1\. Clona el repositorio

git clone https://github.com/JulianMelo1213/facturacion-mvc.git cd
facturacion-mvc

2\. Restaura paquetes y compila -dotnet restore -dotnet build

3\. Configura la base de datos Crea una base de datos en SQL Server:
facturacionDB

Ejecuta el script script_facturacionDB.sql para crear tablas y datos de
prueba

4\. Configura la cadena de conexión Abre appsettings.json y pon:

\"ConnectionStrings\": { \"DefaultConnection\":
\"Server=.;Database=facturacionDB;Trusted_Connection=True;MultipleActiveResultSets=true\"
} 5. Ejecuta la aplicación
