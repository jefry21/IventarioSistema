Este proyecto esta construido bajo las siguientes caracteristicas:
FrontEnd:
.NET MVC CORE version 8
Backend
API CORE .NET version 8
Objetos de base de datos con migración
EF para el manejo de datos
Validación de datos y entrada de datos

Instrucciones backedn:
al clonar el repositorio en una maquina fisica tenemos que tener en cuenta lo siguiente:
1. Validación de que todo los entornos esten configurados como por ejemplo las herramientas de EF que son Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools y ejecución de comando dotnet tool install --global dotnet-ef para instalar las herramientas EF globalmente permitiendolas usar en consola
2. Microsoft.AspNetCore.Authentication.JwtBearer para autenticación

Una vez verificando esto antes de ejecutar el API tenemos que migrar la base de datos a nuestro SQL:
1. Modificaremos el archivo appsettings.json en donde tenemos la configuración de nuestra base de datos tenemos que sustituir el server por el de la maquina enla que trabajaremos (Instancia o Link Server) con sus respectivas credenciales, para efecto de esta practica contamos con Usuario: sa y clave: sa
2. En el mismo archivo esta la configuración de nuestro JWT en donde podemos cambiar nuestro Key de cifrado Issuer, audience y el tiempo de duración de nuestro JWT.

Ejecutaremos los comando de migración para la creación de nuestra base: 
1. Tenemos que estar seguro que estamos en nuestro arhcivo padre en donde se aloja nuestr backend
2. ejecutamos dotnet ef migrations add "Este espacio lo sustituimos por el nombre de nuestra migración", esto generara o modificara una carpeta migrations en donde tendremos los cambios a la base
3. aplicamos la migración con dotnet ef database update, esto creara y modificara nuestra base segun la tengamos configurada, antes de ejecutarlo tenemos que asegurarnos que nuestro appsettings.json tenga la cadena correcta

Antes de utilizr nuestra API crearemos manalmente un usuario en la tabla Usuario(El API utiliza autenticación si no hay un usuario para autenticar no podremos realizar acciones)
Campos de tabla Usuario:
1. Id-> autoincrementable
2. Nombre-> descripcion del usuario
3. Rol-> rol que le pondremos al usuario(1-> administrador, 2-> Caja)
4. Activo: Si ese usuario esta activo(1->Si, 0-> no)
5. Contrasena-> credencial(se dejo sin cifrado por efectos de demostracion)
Algunos campos se dejaron como demostración de que podemos hacer multiples selecciones para mejorar la seguridad como por ejemplo Activo, en este ejemplo no se tomo en cuenta
   
Una vez tengamos migrada nuestra base y creado el usuario podremos ejecutar nuestra API, antes de hacer cualquier movieminto debemos de tener en cuenta que usa autenticación por lo que primero tenemos que ejecutar el endpoint Login en donde nos devolvera un JWT que tenemos que ingresar en la parte superior derecha de nuestro swagger
una vez ingresado podremos hacer movimientos, tengamos en cuenta que el token se genera dependiendo de roles de usuario.
Cada metodo muestra los datos que espera y el tipo de dato siendo en su mayoria obligatorios

Los metodos utilizan estados de respuesta para controlar la respuesta con codigos tambien se validaron los datos que on se ingresaran en nulos u obligfatorios con DataAnnotations
Se realiza un Post, Put, Delete de algunos endpoint pero para esta practica no se utilizan todos pero se dejaron por demostración

Instricciones frontend:
El frontend esta elaborado en base codigo c# y plantillsa razor y con poco de JS para mayor fluides asi como la separaciónd de responsabilidades entre el modelo-vista-controlador asi mismo la creación de clases para consumo de API ya que si dejamos estos consumos en el controlador tendremos un codigo extenso y poco entendible
tambien cuenta con las pantallas de:
1. Login: Inicio de sesión
2. Index: tablero principal
3. Ventas: creación de ventas simples
4. Productos: visualización de productos resaltando los que estan bajo stock
5. Reportes: visualización de producto de bajo stock

Cabe mencioanr que se tienen que agregar productos y hacer movimientos para que validaciones como stock minimo funcione

Las vistas y componentes se habilitan dependiendo el rol del usuario asi como sus acciones.

1. Las prinicpales deciciones tecnicas de este proyecto fue la separacion de responsabilidades asi como DAO y el correcto uso de modelos para evitar consultas en cadenas asi como manejo de errores para evitar una caida de la pagina.
2. Pruebas unitarias en base a entrada de datos y respuestas de endpoint


------------------------------Fin del documento------------------------------------------------------------





