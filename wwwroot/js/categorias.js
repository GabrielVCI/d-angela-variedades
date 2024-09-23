
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

