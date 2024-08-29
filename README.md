# d-angela-variedades
Aplicación web para administrar la venta de productos a mediana escala, realizada con tecnologías como ASP.NET Core, SQL Server, JS y demás.

La idea es crear una herramienta en donde se puedan administrar productos de algún vendedor. Los productos se pueden dividir por categoría y cada producto tiene diferentes características.
El administrador es capaz de ver sus productos, registrarlos, editarlos y/o eliminarlos a su antojo. También puede registrar un objeto como vendido.
# Descripción general
La aplicación tendrá las siguientes secciones:
-	Login y registro: Aquí se podrá registrar una empresa o producto, junto con su administrador/a. Luego el administrador tendrá la opción de registrar nuevos miembros que puedan ingresar a la aplicación con permisos limitados.

-	Home Page: la página principal del sitio donde se describe lo que se puede realizar en la aplicación y donde se le da la bienvenida al administrador y al miembro de la empresa.

-	Inventario: este es un listado de todos los productos de la empresa. Estos productos estarán divididos en categorías de productos. Es decir, el administrador será capaz de agregar una categoría de producto (ej. cosméticos) y desde ahí podrá agregar los diferentes productos relacionados a la categoría. También podrá crear una subcategoría, como lo sería una marca de un cosmético, y de ahí registrar productos dependiendo de otras características como el color o modelo.

-	Ventas: este apartado es para que el administrador lleve un control de las ventas que ha realizado. Este se ordenará de la siguiente forma: 

1.	Listado de opciones para ver las ventas, en este listado estarán las categorías de la empresa, el administrador puede seleccionar una categoría y ver un listado de las ventas de esa categoría.
2.	Buscador de productos, para que el administrador pueda buscar un producto en específico utilizando un código único brindado por la aplicación.
3.	Una opción para ver los clientes que han comprado.

-	Ganancias: este apartado es para desglosar de manera detallada las ganancias que se han generado.

# Descripción específica
Apartados de la aplicación
-	Login y registro de empresas y usuarios
-	Home Page
-	Productos
-	Ganancias 
-	Usuarios
-	Ventas

Login y registro de empresas y usuarios:
La aplicación cuenta con un sistema de registro de empresas y usuarios, en donde los administradores pueden registrar sus empresas y los usuarios que tendrán acceso al contenido de esta.
Los administradores registran una empresa utilizando los siguientes campos:
1.	Nombre de la empresa
2.	Tipo de empresa
3.	Cantidad de empleados
4.	Nombre del administrador/a
5.	Correo electrónico del administrador/a
6.	Contraseña
7.	Confirmar contraseña
Para ingresar a la aplicación, los miembros de la empresa tienen que utilizar las credenciales registradas (en caso de ser administradores) o las otorgadas (en caso de ser miembros regulares).
Cuando el administrador registra una empresa, un mensaje de confirmación de correo le es enviado al correo personal que ha proporcionado en primer lugar, luego el administrador confirma su correo y es redirigido al home page de la aplicación.



# Usuarios

Para registrar a un miembro de la empresa, el administrador puede registrar sus datos personales, un nombre de usuario y una contraseña, los dos últimos son los utilizados por el trabajador de la empresa para ingresar a la aplicación.
De igual forma, el usuario puede crear diferentes roles para asignárselo a los trabajadores de la empresa. Para utilizar estos roles, el administrador es capaz de asignarle UN rol al trabajador cuando este le cree las credenciales.
Para crear el rol, el administrador debe agregar seleccionar la opción de crear rol, agregar el nombre del rol (ej. Usuario estándar, Inventario, Contable), luego elegir a cuáles partes de la aplicación ese usuario tiene acceso y seleccionar Crear. 
Estas acciones solo pueden ser realizadas por los usuarios administradores.
Home page
El home page es simple y va directo al punto, le da la bienvenida al usuario que haya ingresado en el momento, y le muestra varias opciones implicándole lo que puede hacer dependiendo de su rol, el usuario desde ahí puede decidir qué quiere hacer.
Las diferentes opciones que los usuarios pueden elegir:
-	Login y registro de empresas y usuarios
-	Home Page
-	Productos
-	Ganancias 
-	Usuarios
-	Clientes
La accesibilidad de estos apartados está definida por el administrador.

# Productos

Este apartado es para registrar todos los productos que la empresa vende, desde aquí, los usuarios pueden agregar productos, categorías y subcategorías de estos, editarlos y/o eliminarlos a su antojo.
Lo primero que el usuario puede ver es una descripción del apartado y de lo que puede hacer aquí, luego puede ver una opción para agregar un producto o una categoría, seguido de una lista de todas las categorías que ha registrado hasta ahora, en forma de acordeón, con los campos:
-	Nombre de la categoría
-	Listado de las subcategorías en una tabla (campos: nombre y acción) 
-	Código de la categoría
-	Acciones (editar, eliminar)
Agregar una categoría es básicamente registrar qué tipo de producto va a ser registrado (calzado, cosméticos, ropa, electrónicos, etc.) para que el administrador tenga los productos de una forma más organizada. Las categorías se pueden editar y/o eliminar.
Una subcategoría es una descripción más detallada del producto en sí (nike, cyzone, apolo, Samsung, etc.). De esta forma, el administrador puede dividir los productos que pertenecen a una misma categoría en bloques más específicos, haciendo que sea más fácil la organización de estos. Las subcategorías también se pueden editar y/o eliminar.
Para agregar un producto, el usuario debe proveer varios datos, como el nombre del producto, una descripción, el precio, el stock (cantidad), la categoría y la subcategoría. Los usuarios pueden ver los productos listados en una tabla con la información brindada. Los productos pueden ser editados y/o eliminados. Esta lista está dividida por categoría.

# Ventas
En este apartado están las ventas que se realizan, donde los usuarios pueden ver una pequeña descripción del apartado junto a un listado de las ventas en una tabla con los siguientes campos:
-	Fecha de venta
-	Código de venta
-	Nombre del producto
-	Tipo
-	Nombre del cliente
-	Cantidad
-	Precio por unidad
-	Cantidad total
También cuenta con un buscador de ventas, utilizando el código generado por la aplicación, para que los usuarios tengan la capacidad de buscar cualquier venta realizada solo usando el código. En la misma tabla, hay una opción para generar un recibo de la venta, esto se puede hacer siempre que el usuario desee hacerlo.

# Ganancias
Este apartado es un dashboard con toda la información relacionada con las ventas que se han realizado, desde todas las ventas, hasta la comparación mensual de ventas, este apartado le brinda al usuario un control general de las ganancias que va generando con la empresa.
Lo primero que el usuario es capaz de ver son varios cards que brindan información como:
-	Total generado hasta la fecha
-	Total de ventas
-	Total generado este mes
Seguido de dos gráficos, el primero despliega la cantidad de ventas realizadas conforme al tiempo (1 mes), el segundo es un gráfico de barras con la cantidad de dinero generada cada mes, con un rango de 6 meses.
Al final, un listado de todas las ventas realizadas hasta la fecha, con los mismos campos antes mencionados.
