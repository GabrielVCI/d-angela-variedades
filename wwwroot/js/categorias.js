
function agregarNuevaCategoria() {

    categoriasListadoViewModel.categorias.push(
        new categoriaElementoListadoViewModel(
            { idCategoria: 0, NombreCategoria: '', Subcategorias: '' }));

    
}; 

async function guardarCategoria(categoria) {
     
     
    try {
        const nombreCategoria = categoria.nombre();

        const object = {
            "Nombre": nombreCategoria
        };

        const data = JSON.stringify(object);

        const respuesta = await fetch(urlCategorias, {
            method: 'POST',
            body: data,
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!respuesta.ok) { 
            manejarErrorApi(respuesta);
            return;
        } 

        await ObtenerListadoCategorias()
        MensajeDeExito("La categoria ha sido creada");
    }

    catch (error) {
        manejarErrorApi(error);
    }
}

async function ObtenerListadoCategorias() {

    categoriasListadoViewModel.cargando(true);
    const respuesta = await fetch(urlCategorias, {

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
    categoriasListadoViewModel.categorias([]);

    json.forEach(categoria => {
        const viewModel = new categoriaElementoListadoViewModel(categoria);
        categoriasListadoViewModel.categorias.push(viewModel);
    });

    categoriasListadoViewModel.cargando(false);
}

async function focusOutCategoria(){
    categoriasListadoViewModel.categorias.pop();
}


async function enviarCategoriaAlBackEnd(categoria) { 

    completandoAccionTimer();
    console.log(categoria);
    let nombreCategoria = categoria.Nombre;

    const object = {
        "Nombre": nombreCategoria
    }

    const data = JSON.stringify(object);

    const response = await fetch(`${urlCategorias}/${categoria.Id}`, {
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
    const json = await response.json();
    mensajeExitoAccionCompletada("Categoría editada correctamente");
    await ObtenerListadoCategorias();

    modalEditarCategoriaBTSP.hide();

    return json;
}

async function ObtenerCategoriaParaEditar(categoria) {
     
    if (categoria.esNuevo()) {  
        return;
    }
  
    const response = await fetch(`${urlCategorias}/${categoria.idCategoria()}`, {
        method: 'GET',
        headers: {
            'Content-Type': "application/json"
        }
    });

    if (!response.ok) {
        manejarErrorApi(response);
        return;
    }

    const json = await response.json();
    console.log(json)
    categoriaEditarViewModel.id = json.idCategoria;
    categoriaEditarViewModel.nombre(json.nombre);

    modalEditarCategoriaBTSP.show();
}


async function eliminarCategoria(categoria) {

    completandoAccionTimer();
 
    const response = await fetch(`${urlCategorias}/${categoria.idCategoria()}`, {
        method: 'DELETE'
    });

    if (!response.ok) {
        manejarErrorApi(response);
        return;
    }
    
    categoriasListadoViewModel.categorias.remove(function (cat) { return cat.id == categoria.idCategoria });
    MensajeDeExito();
    ObtenerListadoCategorias();

}

function confirmarElimininacionCategoria(categoria) {

    confirmarAction({
        callbackAceptar: () => {
            eliminarCategoria(categoria);
        },
        callbackCancelar: () => {
            return;
        },

        titulo: '¿Desea borrar esta categoría?',

        text: "Se eliminará de su lista de categorías"
    });
}




