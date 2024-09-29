function AgregarSubcategoria(subcategoria) { 
 
    categoriasListadoViewModel.categorias.push(
        new categoriaElementoListadoViewModel(
            {
                idCategoria: subcategoria.idCategoria(),
                nombre: subcategoria.nombre(),
                Subcategorias: '',
                subcategoriaId: 0
            }));

}

async function guardarSubCategoria(subcategoria) {

    completandoAccionTimer();
    const object = {
        "Name": subcategoria.nombreSubCategoria()
    };

    const data = JSON.stringify(object);
    const response = await fetch(`${urlSubcategorias}/${subcategoria.categoriaId()}`, {

        method: "POST",
        body: data,
        headers: {
            'Content-Type': "application/json"
        }
    });

    if (!response.ok) {

        manejarErrorApi(response);

        if (response.status == 422) {
            subcategoriaExiste = true; 
        }
        return;
    }

    MensajeDeExito("La subcategoría ha sido creada");
    ObtenerListadoCategorias();
} 


async function ObtenerSubCategoriaParaEditar(subcategoria) {

     try {
        const response = await fetch(`${urlSubcategorias}/${subcategoria.idSubCategoria}`, {
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

        subcategoriaEditarViewModel.id = json.idSubCategoria;
        subcategoriaEditarViewModel.nombre(json.name);
        modalEditarSubCategoriaBTSP.show();
         
    } catch (error) {
        manejarErrorApi(error);
        return;
    }

} 

async function enviarSubcategoriaAlBackEnd(subcategoria) {

    completandoAccionTimer();

    let nombreSubCategoria = subcategoria.Nombre;

    const object = {
        "Name": nombreSubCategoria
    }

    const data = JSON.stringify(object);

    const response = await fetch(`${urlSubcategorias}/${subcategoria.Id}`, {
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
     
 
    mensajeExitoAccionCompletada("Subcategoría editada correctamente");
    await ObtenerListadoCategorias();

    modalEditarSubCategoriaBTSP.hide(); 
}


async function eliminarSubCategoria(subcategoria) {

    completandoAccionTimer();
    const response = await fetch(`${urlSubcategorias}/${subcategoria.idSubCategoria}`, {
        method: 'DELETE'
    });

    if (!response.ok) {
        manejarErrorApi(response);
        return;
    }

    ObtenerListadoCategorias();
    mensajeExitoAccionCompletada("Subcategoría eliminada correctamente");
}

function confirmarElimininacionSubcategoria(subcategoria) {
    console.log(subcategoria)
    confirmarAction({
        callbackAceptar: () => {
            eliminarSubCategoria(subcategoria);
        },
        callbackCancelar: () => {
            return;
        },

        titulo: `¿Desea borrar la subcategoría ${subcategoria.name}?`,

        text: "Se eliminará de su lista de subcategorías, también todos los productos relacionados a esta"
    });
}