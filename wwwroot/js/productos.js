
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
        const response = await fetch(`${urlSubcategorias}/${categoriaId}`, {

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