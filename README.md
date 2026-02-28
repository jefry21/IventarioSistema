Sistema de Gestión de Producto y Ventas – ASP.NET Core (.NET 8)

Este proyecto implementa un sistema básico de gestión de ventas utilizando una arquitectura desacoplada compuesta por un Frontend en ASP.NET Core MVC y un Backend en ASP.NET Core Web API, ambos desarrollados en .NET 8. El sistema incluye autenticación mediante JWT, control de acceso basado en roles, validaciones con DataAnnotations y manejo de base de datos mediante Entity Framework Core con migraciones.

La solución está dividida en dos proyectos principales: Backend y Frontend. El Backend expone endpoints REST protegidos con autenticación JWT, implementa validaciones de datos, manejo estructurado de códigos HTTP y control de acceso por roles (Administrador y Caja). El Frontend está desarrollado con Razor Views y C#, consumiendo la API mediante servicios desacoplados para mantener los controladores limpios y organizados, respetando la separación de responsabilidades del patrón MVC.

Requisitos previos: es necesario contar con .NET SDK 8 instalado, SQL Server configurado y las herramientas de Entity Framework Core. Para instalar las herramientas globales de EF se debe ejecutar el siguiente comando en consola: dotnet tool install --global dotnet-ef. El Backend requiere los paquetes Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools y Microsoft.AspNetCore.Authentication.JwtBearer.

Para configurar el proyecto, primero se debe clonar el repositorio y dirigirse al proyecto Backend. En el archivo appsettings.json se debe modificar la cadena de conexión en la sección ConnectionStrings, reemplazando el servidor y credenciales según el entorno local. En el mismo archivo se encuentran los parámetros de configuración del JWT (Key, Issuer, Audience y duración del token), los cuales pueden ajustarse según necesidad.

Antes de ejecutar la API, se deben aplicar las migraciones de base de datos. Ubicándose en la carpeta raíz del Backend, se ejecutan los siguientes comandos: dotnet ef migrations add InitialCreate y posteriormente dotnet ef database update. Esto generará y actualizará la base de datos según el modelo definido en el proyecto.

Para poder autenticarse en el sistema, es necesario crear manualmente un usuario inicial en la tabla Usuario. Los campos requeridos son: Id (autoincremental), Nombre, Rol (1 = Administrador, 2 = Caja), Activo (1 = Activo, 0 = Inactivo) y Contrasena. Para efectos demostrativos la contraseña se encuentra sin cifrado y el campo Activo no tiene logica implementada.

El flujo de autenticación funciona de la siguiente manera: primero se ejecuta el endpoint Login desde Swagger o Postman, se obtiene el token JWT generado y posteriormente se ingresa en la opción Authorize de Swagger o se envía como Bearer Token en las peticiones HTTP. El token incluye claims con el rol del usuario, lo que permite habilitar o restringir acciones según permisos.

El Frontend incluye las siguientes pantallas: Login (inicio de sesión), Dashboard (panel principal), Productos (visualización y control de productos, resaltando bajo stock), Ventas (registro de ventas simples) y Reportes (visualización de productos con stock mínimo). Las vistas y acciones disponibles se habilitan dinámicamente según el rol del usuario autenticado. El consumo de la API se realiza mediante clases de servicio dedicadas, evitando llamadas directas desde los controladores y manteniendo un código más limpio y mantenible.

El sistema implementa validaciones utilizando DataAnnotations en los modelos, control de datos obligatorios, manejo estructurado de errores y respuestas HTTP adecuadas (200, 400, 500). Se incluyen operaciones POST, PUT y DELETE para demostración del manejo completo de endpoints REST un que no todas estan implementadas en el Frontend.

Entre las principales decisiones técnicas destacan: la separación clara entre frontend y backend para escalabilidad y mantenimiento, uso de DTOs para evitar exponer entidades directamente, implementación de patrón DAO para acceso a datos, autenticación basada en JWT con control de roles mediante Claims, uso de async/await para operaciones no bloqueantes y pruebas unitarias enfocadas en validación de entrada y respuestas de endpoints.

Desarrollado por Jefry Omar Lagos Carranza.


------------------------------Fin del documento------------------------------------------------------------





