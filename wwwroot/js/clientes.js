function agregarNuevoCliente() {
    clientesListadoViewModel.clientes.push(
        new clienteElementoListadoViewModel(
            {
                idCliente: 0,
                nombre: '',
                telefono: '',
                nota: '',
                grupo: '',
                grupoId: '', 
            }));
};

async function guardarCliente(cliente) {
     
    try {
        let grupoId = cliente.grupo.grupoId;
        
        completandoAccionTimer();
        const object = {
            "NombreCliente": cliente.nombreCliente(),
            "Telefono": cliente.telefono(),
            "GrupoId": grupoId,
            "Nota": cliente.nota()  
        }

        const data = JSON.stringify(object);

        const response = await fetch(`${urlClientes}`, {
            method: 'POST',
            body: data,
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            if (response.status == 422) {
                mostrarMensajeError("Ya tienes un cliente con este nombre"); 
                return;
            }
             
            else if (response.status == 421) {
                mostrarMensajeError("Ya tienes un cliente con este telefono"); 
                return;
            } 
            manejarErrorApi(response);
            return;
        }

        await ObtenerClientes();
        MensajeDeExito("El cliente ha sido agregado");

    } catch (error) {
         
        manejarErrorApi(error);
        return;
    }
}

async function ObtenerClientes() {

    clientesListadoViewModel.cargando(true);

    const respuesta = await fetch(urlClientes, {

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
    clientesListadoViewModel.clientes([]);
     
    json.forEach(cliente => {
        const viewModel = new clienteElementoListadoViewModel(cliente);
        clientesListadoViewModel.clientes.push(viewModel);
    });

    clientesListadoViewModel.cargando(false);
}

function focusOutCliente() {

    clientesListadoViewModel.clientes.pop();

}
 

async function obtenerClienteAEditar(cliente) {

    try { 
        const response = await fetch(`${urlClientes}/${cliente.idCliente()}`, {
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
      
        clienteEditarViewModel.idCliente = json.idCliente;
        clienteEditarViewModel.nombre(json.nombre);
        clienteEditarViewModel.nota(json.nota);
        clienteEditarViewModel.telefono(json.telefono);  
        clienteEditarViewModel.idGrupo(json.grupoId);

        //This is important, we need to set the category first
        // Set category
        clienteEditarViewModel.grupo(json.grupoId); 
      
  
        modalEditarClienteBSTP.show();

    } catch (error) {
        manejarErrorApi(error);
        return;
    }

}


async function editarCliente(cliente) {

    try {
         
        completandoAccionTimer();

        const object = {
            "NombreCliente": cliente.nombreCliente,
            "Telefono": cliente.telefono,
            "GrupoId": cliente.grupo,
            "Nota": cliente.nota
        }
        const data = JSON.stringify(object);

        const response = await fetch(`${urlClientes}/${cliente.id}`, {
            method: "PUT",
            body: data,
            headers: {
                'Content-Type': "application/json"
            }
        });


        if (!response.ok) {
            if (response.status == 422) {
                mostrarMensajeError("Ya tienes un cliente con este nombre");
                return;
            }

            else if (response.status == 421) {
                mostrarMensajeError("Ya tienes un cliente con este telefono");
                return;
            } 
            manejarErrorApi(response);
            return;
        }

        const json = await response.json();
        mensajeExitoAccionCompletada("¡El cliente ha sido editado correctamente!");
        await ObtenerClientes();
        modalEditarClienteBSTP.hide();
        return json;


    } catch (error) {
        console.log(error)
        manejarErrorApi(error);
        return;
    }
}

async function eliminarCliente(cliente) {

    try {
        completandoAccionTimer();

        const response = await fetch(`${urlClientes}/${cliente.idCliente()}`, {

            method: 'DELETE'
        });

        if (!response.ok) {
            manejarErrorApi(response);
            return;
        }

        clientesListadoViewModel.clientes.remove(function (cli) { return cli.id == cliente.idCliente});
        mensajeExitoAccionCompletada("El cliente ha sido eliminado correctamente");
        ObtenerClientes();
    } catch (error) {
        manejarErrorApi(error);
        return;
    }
}
function confirmarEliminacionDelCliente(cliente) {


    confirmarAction({
        callbackAceptar: () => {
            eliminarCliente(cliente);
        },
        callbackCancelar: () => {
            return;
        },

        titulo: `¿Desea borrar el cliente ${cliente.nombre()}?`,

        text: "Se eliminará de su lista de clientes."
    });
}

async function obtenerClienteConElNombreOTelefono(nombre_telefono_cliente) {

    try {
        if (nombre_telefono_cliente.trim().length == 0) {
            return;
        }

        clientesListadoViewModel.cargando(true);

        const parametros = new URLSearchParams({
            nombre_telefono: nombre_telefono_cliente || ''
        });


        const response = await fetch(`${urlClientes}/obtenerClientesPorElNombreOTelefono?${parametros}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
             
            response.status == 404 ? mostrarMensajeError("No tienes clientes con este nombre o telefono") : manejarErrorApi(response);
            clientesListadoViewModel.cargando(false);
            return;
        }

        const json = await response.json();
        clientesListadoViewModel.clientes([]);

        json.forEach(cliente => {
            const viewModel = new clienteElementoListadoViewModel(cliente);
            clientesListadoViewModel.clientes.push(viewModel);
        });

        clientesListadoViewModel.cargando(false);

    } catch (error) {
        manejarErrorApi(error);
        return;
    }
} 