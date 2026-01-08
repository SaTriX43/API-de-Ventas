# API de Ventas y Facturación

API REST desarrollada en **ASP.NET Core (.NET 8)** para la gestión de ventas y facturación básica, con autenticación JWT, control de roles y reglas de negocio.

Este proyecto forma parte de mi portafolio como **Backend .NET Junior**, enfocado en buenas prácticas, seguridad y diseño limpio.

---

## 🚀 Tecnologías utilizadas

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role-based Authorization
- Serilog
- QuestPDF (exportación de facturas)
- Swagger / OpenAPI

---

## 🔐 Autenticación y roles

La API utiliza **JWT** para autenticación y autorización.

### Roles disponibles:
- **Admin**
  - Puede ver y gestionar todas las facturas
- **Vendedor**
  - Solo puede ver y gestionar las facturas que él creó

---

## 🧱 Entidades principales

- **Cliente**
- **Producto**
- **Pedido (Factura)**
- **PedidoDetalle**

---

## ⚙️ Funcionalidades

- Autenticación con JWT
- Creación de pedidos / facturas
- Visualización de facturas con detalle
- Listado de facturas por cliente
- Filtros por fecha
- Paginación
- Control de acceso por rol y ownership
- Exportación de facturas a **PDF**
- Manejo centralizado de errores

---

## 📏 Reglas de negocio

- El total de la factura se calcula automáticamente
- No se permiten pedidos sin productos
- No se permiten cantidades inválidas
- El vendedor solo puede acceder a sus propios pedidos
- El admin puede acceder a todos los pedidos

---

## 📄 Exportación de facturas

Se permite exportar una factura a PDF mediante el endpoint:

GET /api/pedidos/{id}/pdf


El archivo se genera dinámicamente y se devuelve como respuesta HTTP.

---

## 🧪 Arquitectura

- Controllers: Orquestan las solicitudes HTTP
- Services: Contienen la lógica de negocio
- Repositories: Acceso a datos (EF Core)
- Middleware: Manejo global de errores
- Servicios dedicados para infraestructura (PDF)

---

## 📌 Estado del proyecto

✔️ Proyecto funcional  
✔️ Enfocado en backend real  
✔️ Preparado para ser parte de un portafolio profesional  

---

## 👤 Autor

**Santiago Gonzáles**  
Backend .NET Junior  
