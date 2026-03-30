# Sistema de Gestión de Pedidos

API REST para la gestión interna de pedidos de un comercio. Permite administrar clientes, productos y pedidos con sus detalles.

Desarrollado en C# con ASP.NET Core Web API y Entity Framework Core.

## Tecnologías
- C# / ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- Swagger / OpenAPI

## Arquitectura
Proyecto estructurado en capas simples:
- **Controllers** — reciben las requests y devuelven responses HTTP
- **Services** — contienen la lógica de negocio (PedidoService)
- **Models** — entidades de la base de datos
- **DTOs** — objetos de transferencia de datos para desacoplar la API de los modelos
- **Data** — DbContext, configuración de EF Core y Base de datos.

## Funcionalidades
- CRUD completo de Clientes y Productos
- Creación de pedidos con múltiples productos
- Descuento automático de stock al confirmar un pedido
- Precio unitario guardado al momento del pedido
- Cálculo automático del total del pedido
- Validaciones de negocio: cliente existente, producto existente, stock suficiente
- Validación de formato en DNI (solo números)

## Cómo correr el proyecto
1. Clonar el repositorio
2. Configurar la connection string en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SistemaGestionPedidos;Trusted_Connection=True;"
}
```
3. Ejecutar migraciones en Package Manager Console:
```
Update-Database
```
4. Correr el proyecto y acceder a Swagger en `http://localhost:{puerto}/swagger`

## Endpoints

### Clientes
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/clientes | Obtener todos los clientes |
| GET | /api/clientes/{id} | Obtener cliente por id |
| POST | /api/clientes | Crear cliente |
| PUT | /api/clientes/{id} | Actualizar cliente |
| DELETE | /api/clientes/{id} | Eliminar cliente |

### Productos
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/producto | Obtener todos los productos |
| GET | /api/producto/{id} | Obtener producto por id |
| POST | /api/producto | Crear producto |
| PUT | /api/producto/{id} | Actualizar producto |
| DELETE | /api/producto/{id} | Eliminar producto |

### Pedidos
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/pedido | Obtener todos los pedidos con cliente y productos |
| GET | /api/pedido/{id} | Obtener pedido por id |
| POST | /api/pedido | Crear pedido con detalles |
| PUT | /api/pedido/{id} | Actualizar pedido |
| DELETE | /api/pedido/{id} | Eliminar pedido |

## Ejemplo de creación de pedido

Request `POST /api/pedido`:
```json
{
  "idCliente": 1,
  "detalle": [
    { "productoId": 1, "cantidad": 2 },
    { "productoId": 2, "cantidad": 3 }
  ]
}
```

Response `201 Created`:
```json
{
  "idPedido": 1,
  "fechaPedido": "2026-03-30T20:20:15",
  "totalPedido": 310000,
  "cliente": { "nombre": "Juan", "apellido": "Pérez" },
  "detallesPedido": [
    { "cantidad": 2, "precioUnitario": 80000 },
    { "cantidad": 3, "precioUnitario": 50000 }
  ]
}
```

## Aclaraciones
Este proyecto es el backend de un sistema de gestión interno.
No incluye frontend momentáneamente — los endpoints están documentados y probados via Swagger UI (`/swagger`).

Un frontend consumiendo esta API mostraría formularios para cargar clientes,
productos y pedidos sin necesidad de interactuar con JSON directamente.