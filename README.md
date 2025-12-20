# 🧾 API de Ventas — ASP.NET Core (.NET 8)

using API_de_Ventas.Models;
using Microsoft.AspNetCore.Mvc;

API REST desarrollada en **ASP.NET Core Web API (.NET 8)** que gestiona un sistema de ventas con **Pedidos y Detalles**, aplicando reglas de negocio reales y buenas prácticas backend.

## 🚀 Características

- Gestión de:
  -Clientes
  - Productos
  - Pedidos
  - Detalles de pedido
- Arquitectura en capas:
  -Controllers
  - Services
  - Repositories
- DTOs para entrada y salida
- Validaciones fuertes
- Cálculo de totales en backend
- Paginación y filtros
- EF Core + SQL Server
- Logging con Serilog
- Middleware global de errores
- JWT configurado

---

## 🧠 Reglas de negocio implementadas

- ❌ No se permiten pedidos sin productos
- ❌ No se permiten productos duplicados en un pedido
- ❌ No se permite usar productos inactivos
- ❌ Cantidades ≤ 0 no permitidas
- ✅ El total del pedido **se calcula en backend**
- ✅ Subtotales calculados por producto
- ✅ Validación de existencia de cliente y productos

---

## 📦 Entidades

- Cliente
- Producto
- Pedido
- PedidoDetalle

### Relaciones

- Cliente 1 → N Pedidos
- Pedido 1 → N Detalles
- Producto N → N Pedido (via PedidoDetalle)

---

## 🔗 Endpoints principales

### Pedidos

- `POST /api/pedidos/crear-pedido`
- `GET /api/pedidos/obtener-pedido-detalles/{pedidoId}`
- `GET / api / pedidos / obtener - pedido - detalles - cliente /{ clienteId}`

### Filtros disponibles

-Por cliente
- Por rango de fechas
- Paginación (`page`, `pageSize`)

---

## 🛠 Tecnologías

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Serilog
- LINQ

---

## 📌 Notas

Este proyecto fue desarrollado como parte de un plan de formación **Backend .NET**, enfocado en consolidar lógica de negocio, relaciones entre entidades y buenas prácticas reales de API REST.

---

## 👨‍💻 Autor

**Santiago**  
Backend .NET Trainee / Junior  
Ecuador 🇪🇨
