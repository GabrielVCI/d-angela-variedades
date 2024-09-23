async function manejarErrorApi(respuesta) {

    let mensajeError = '';

    if (respuesta.status === 400) {
        mensajeError = await respuesta.text();
    } else if (respuesta.status === 404) {
        mensajeError = "Este recurso no ha sido encontrado.";
    } else if (respuesta.status == 422) {
        mensajeError = "Propiedad ya agregada en tus intereses"
    } else {
        mensajeError = "Ha surgido un error inesperado.";
    }

    mostrarMensajeError(mensajeError);
}

function mostrarMensajeError(mensaje) {

    Swal.fire({
        icon: 'error',
        title: 'Error...',
        text: mensaje
    });
}

function MensajeDeExito(mensaje) {

    Swal.fire({
        title: "Listo",
        text: mensaje,
        icon: "success"
    });
}