# KiriBlog Backend

Backend API para una plataforma de blog construida con **Clean Architecture** en .NET.

Este proyecto implementa:

- Arquitectura limpia (Domain, Application, Infrastructure, Api)
- JWT Authentication
- Entity Framework Core
- PostgreSQL
- Separación de casos de uso
- DTOs específicos por escenario (List vs Detail)
- Soporte para contenido en Markdown

---

## Arquitectura

El proyecto sigue principios de Clean Architecture:

- **Domain** → Entidades y reglas de negocio
- **Application** → Casos de uso y contratos
- **Infrastructure** → EF Core, persistencia y servicios externos
- **Api** → Controllers y configuración HTTP

La aplicación separa claramente:

- DTOs para listado (`PostListItemResponse`)
- DTOs para detalle (`PostDetailResponse`)
- Casos de uso independientes por acción

---

## Tecnologías

- .NET 10
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT Authentication

