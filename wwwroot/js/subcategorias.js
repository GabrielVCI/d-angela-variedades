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