 
function agregarNuevoProducto() {
    productosListadoViewModel.productos.push(
        new productoElementoListadoViewModel(
            {
                idProducto: 0,
                nombreProducto: '',
                precio: '',
                stock: '',
                descripcion: '',
                categoria: '',
                subcategoria: '',
                categoriaId: '',
                subcategoriaId: ''
            })); 
}; 

async function ObtenerCategorias() {

    try {
        const response = await fetch(`${urlCategorias}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        const json = await response.json();
        return json;
    }

    catch (error) {
       
        manejarErrorApi(error);
    }
}


async function ObtenerSubcategorias(categoriaId) {
     
    try {
        const response = await fetch(`${urlSubategorias}/${categoriaId}/subcategorias`, {

            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        
        if (!response.ok) { 
            manejarErrorApi(response);
            return;
        }

        const json = await response.json();
        return json;

    } catch (error) {
        manejarErrorApi(error);
        return;
    }
}

async function guardarProducto(producto) {

    try {
        let categoriaId = producto.categoria.idCategoria;
        let subcategoriaId = producto.subcategoria.idSubCategoria;
        completandoAccionTimer();
        const object = {
            "Nombre": producto.nombreProducto(),
            "Stock": producto.stock(),
            "Descripcion": producto.descripcion(),
            "Precio": producto.precio(),
            "IdCategoria": categoriaId,
            "IdSubcategoria": subcategoriaId
        }
  
        const data = JSON.stringify(object);
  
        const response = await fetch(`${urlProductos}`, {
            method: 'POST',
            body: data,
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        await ObtenerProductos();
        MensajeDeExito("El producto ha sido agregado");

    } catch (error) {

        manejarErrorApi(error);
        return;
    }
}

async function ObtenerProductos() {

    productosListadoViewModel.cargando(true);

    const respuesta = await fetch(urlProductos, {

        method: 'GET',
        headers: {
            'Content-Type': "application/json"
        }
    });

    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
        return;
    }

    const json = await respuesta.json();
    productosListadoViewModel.productos([]);
    
    json.forEach(producto => {
        const viewModel = new productoElementoListadoViewModel(producto);
        productosListadoViewModel.productos.push(viewModel);
    });

    productosListadoViewModel.cargando(false);
}

function focusOutProductos() {

    productosListadoViewModel.productos.pop();
 
}

async function obtenerCategoria(categoriaId) {

    try {
        const response = await fetch(`${urlCategorias}/${categoriaId}`, {
            method: 'GET',

            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        const json = await response.json();
         
        return json.nombre;
    }

    catch (error) {
        manejarErrorApi(error);
        return;
    }
}

async function obtenerSubcategoria(subcategoriaId) {

    try {
        const response = await fetch(`${urlSubategorias}/${subcategoriaId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        const json = await response.json(); 
        return json.name;
    }

    catch (error) {
        manejarErrorApi(error);
        return;
    }
}


async function obtenerProductoAEditar(producto) {
   
    try {

        const response = await fetch(`${urlProductos}/${producto.idProducto()}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            manejarErrorApi(response);
             
            return;
        }
        const json = await response.json();

        productoEditarViewModel.idProducto = json.idProducto;
        productoEditarViewModel.nombre(json.nombre);
        productoEditarViewModel.descripcion(json.descripcion);
        productoEditarViewModel.stock(json.stock);
        productoEditarViewModel.precio(json.precio);
        productoEditarViewModel.idCategoria(json.idCategoria);
        productoEditarViewModel.idSubCategoria(json.idSubCategoria);

        //This is important, we need to set the category first
        // Set category
        productoEditarViewModel.categoria(json.idCategoria);

        //And then load the municipios
        // Load the municipalities for the selected province
        await productoEditarViewModel.loadSubcategorias(json.idCategoria);

        //AND THEN set the municipio
        //Set municipio after the municipalities have been loaded
        productoEditarViewModel.subcategoria(json.idSubCategoria); 
        modalEditarProductoBSTP.show();

    } catch (error) {
        manejarErrorApi(error);
        return;
    }
    
}


async function editarProducto(producto) {

    try {

        completandoAccionTimer();

        const object = {
            "Nombre": producto.nombreProducto,
            "Stock": producto.stock,
            "Precio": producto.precio,
            "Descripcion": producto.descripcion,
            "IdSubCategoria": producto.subcategoria,
            "idCategoria": producto.categoria
        }

 
        const data = JSON.stringify(object);
   
        const response = await fetch(`${urlProductos}/${producto.id}`, {
            method: "PUT",
            body: data,
            headers: {
                'Content-Type': "application/json"
            }

         });

        
        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        const json = response.json();
        mensajeExitoAccionCompletada("¡El producto ha sido editado correctamente!");
        await ObtenerProductos();
        modalEditarProductoBSTP.hide();
        return json;


    } catch (error) {
         
        manejarErrorApi(error);
        return;
    }
}

async function eliminarProducto(producto) {

    try {
        completandoAccionTimer();

        const response = await fetch(`${urlProductos}/${producto.idProducto()}`, {

            method: 'DELETE'
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        productosListadoViewModel.productos.remove(function (prod) { return prod.id == producto.idProducto });
        MensajeDeExito();
        ObtenerProductos();
    } catch (error) {
        manejarErrorApi(error);
        return;
    }
}
function confirmarEliminacionDelProducto(producto) {

     
    confirmarAction({
        callbackAceptar: () => {
            eliminarProducto(producto);
        },
        callbackCancelar: () => {
            return;
        },

        titulo: `¿Desea borrar el producto ${producto.nombre()}?`,

        text: "Se eliminará de su lista de productos."
    });
}

async function obtenerProductosConElNombre(nombreProducto) {

    try {
        if (nombreProducto.trim().length == 0) {
            return;
        }

        productosListadoViewModel.cargando(true);

        const parametros = new URLSearchParams({
            nombre: nombreProducto || ''
        });


        const response = await fetch(`${urlProductos}/obtenerProductosPorElNombre?${parametros}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            response.status == 404 ? mostrarMensajeError("No tienes productos con este nombre") : manejarErrorApi(response);
            productosListadoViewModel.cargando(false);
            return;
        } 

        const json = await response.json();
        productosListadoViewModel.productos([]);
         
        json.forEach(producto => {
            const viewModel = new productoElementoListadoViewModel(producto);
            productosListadoViewModel.productos.push(viewModel);
        });
         
        productosListadoViewModel.cargando(false);

    } catch (error) {
        manejarErrorApi(error);
        return;
    }
}


async function filtrarProductos(objetoFiltro) {
      
    try {
        productosListadoViewModel.cargando(true); 
        const params = new URLSearchParams({

            nombreProducto: objetoFiltro.nombre || "",
            precio: objetoFiltro.precio || 0,
            stock: objetoFiltro.stock || 0,
            categoriaId: objetoFiltro.categoriaId || 0,
            subcategoriaId: objetoFiltro.subcategoriaId || 0

        });

        const response = await fetch(`${urlProductos}/filtrarProductos?${params}`, {

            method: "GET",
            headers: {
                'Content-Type': "application/json"
            }
        });

        if (!response.ok) {
            productosListadoViewModel.cargando(false);
            if (response.status === 404) {
                mostrarMensajeError("No tienes productos guardados con estas características");
                return;
            }
            manejarErrorApi(response); 
            return;
        }

         
        const json = await response.json();
 
        productosListadoViewModel.productos([]);

        json.forEach(producto => {
            const viewModel = new productoElementoListadoViewModel(producto); 
            productosListadoViewModel.productos.push(viewModel);
        });

        productosListadoViewModel.cargando(false);
        modalFiltrarProductoBSTP.hide();

    } catch (error) {
        manejarErrorApi(error);
    }
}