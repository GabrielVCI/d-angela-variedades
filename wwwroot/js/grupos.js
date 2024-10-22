 

function agregarNuevoGrupo() {
    gruposListadoViewModel.grupos.push(
        new grupoElementoListadoViewModel(
            {
                grupoId: 0,
                NombreGrupo: ''  
            }));
};

async function ObtenerGruposClientes() {

    try {

        const response = await fetch(`${urlGrupos}`, {
            method: "GET",
            headers: {
                'Content-Type': "application/json"
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

async function obtenerGrupo(grupoId) {
    console.log(grupoId)
    try {
        const response = await fetch(`${urlGrupos}/${grupoId}`, {

            method: "GET",
            headers: {
                'Content-Type': "application/json"
            }
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        const json = await response.json();

        return json.nombreGrupo;
    } catch (error) {
        manejarErrorApi(error);
        return;
    }
}

async function guardarGrupo(grupo) {
 
    try { 
        completandoAccionTimer();
        const object = {
            "NombreGrupo": grupo.nombreGrupo() 
        }

        const data = JSON.stringify(object);

        const response = await fetch(`${urlGrupos}`, {
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

        await ObtenerGrupos();
        MensajeDeExito("El grupo ha sido agregado");

    } catch (error) {
       
        manejarErrorApi(error);
        return;
    }
}

async function ObtenerGrupos() {

    gruposListadoViewModel.cargando(true);

    const respuesta = await fetch(urlGrupos, {

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
    gruposListadoViewModel.grupos([]);

    
    json.forEach(grupo => {
        const viewModel = new grupoElementoListadoViewModel(grupo);
        gruposListadoViewModel.grupos.push(viewModel);
    });

    gruposListadoViewModel.cargando(false);
}

function focusOutGrupo() {

    gruposListadoViewModel.grupos.pop();

}


async function obtenerGrupoAEditar(grupo) {

    try { 
        const response = await fetch(`${urlGrupos}/${grupo.grupoId()}`, {
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

        grupoEditarViewModel.grupoId = json.grupoId;
        grupoEditarViewModel.nombreGrupo(json.nombreGrupo);
        modalEditarGrupoBSTP.show();

    } catch (error) {
         
        manejarErrorApi(error);
        return;
    }

}


async function editarGrupo(grupo) {

    try {
        
        completandoAccionTimer();

        const object = {
            "NombreGrupo": grupo.nombreGrupo,
        };
        
        const data = JSON.stringify(object); 
        const response = await fetch(`${urlGrupos}/${grupo.id}`, {
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

         
        mensajeExitoAccionCompletada("¡El grupo ha sido editado correctamente!");
        await ObtenerGrupos();
        modalEditarGrupoBSTP.hide();
        return;


    } catch (error) {
         
        manejarErrorApi(error);
        return;
    }
}

async function eliminarGrupo(grupo) {

    try {
        completandoAccionTimer();

        const response = await fetch(`${urlGrupos}/${grupo.grupoId()}`, {

            method: 'DELETE'
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        gruposListadoViewModel.grupos.remove(function (gru) { return gru.id == grupo.idGrupo });
        mensajeExitoAccionCompletada("El grupo ha sido eliminado correctamente");
        ObtenerGrupos();
    } catch (error) {
        manejarErrorApi(error);
        return;
    }
}
function confirmarEliminacionDelGrupo(grupo) {


    confirmarAction({
        callbackAceptar: () => {
            eliminarGrupo(grupo);
        },
        callbackCancelar: () => {
            return;
        },

        titulo: `¿Desea borrar el grupo ${grupo.nombreGrupo()}?`,

        text: "Se eliminará de su lista de grupos."
    });
}

async function obtenerGruposConElNombre(nombre_grupo) {

    try {
        if (nombre_grupo.trim().length == 0) {
            return;
        }

        gruposListadoViewModel.cargando(true);

        const parametros = new URLSearchParams({
            nombreGrupo: nombre_grupo || ''
        });


        const response = await fetch(`${urlGrupos}/obtenerGrupoConElNombre?${parametros}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });
          
        if (!response.ok) {
            response.status == 404 ? mostrarMensajeError("No tienes grupos con este nombre") : manejarErrorApi(response);
            gruposListadoViewModel.cargando(false);
            return;
        }

        const json = await response.json();
        gruposListadoViewModel.grupos([]);
      
        json.forEach(grupo => {
            const viewModel = new grupoElementoListadoViewModel(grupo);
            gruposListadoViewModel.grupos.push(viewModel);
        });

        gruposListadoViewModel.cargando(false);

    } catch (error) {
        manejarErrorApi(error);
        
        return;
    }
} 