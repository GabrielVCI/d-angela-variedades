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

function confirmarAction({ callbackAceptar, callbackCancelar, titulo, mensaje }) {

    Swal.fire({
        title: titulo || '¿Realmente deseas hacer esto?',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        confirmButtonText: 'Si',
        focusConfirm: true,
        text: mensaje
    }).then((resultado) => {
        if (resultado.isConfirmed) {
            callbackAceptar();
        }
        else if (callbackCancelar) {
            callbackCancelar();
        }
    });
}

function mensajeExitoAccionCompletada(mensaje) {
    Swal.fire({
        title: "Listo",
        text: mensaje,
        icon: "success"
    });
}


function completandoAccionTimer() {
    let timerInterval;
    Swal.fire({
        title: "Cargando..",
        html: "Completado en <b></b>",
        timer: 1000,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading();
            const b = document.createElement('b');
            const htmlContainer = Swal.getHtmlContainer();
            htmlContainer.appendChild(b);
            timerInterval = setInterval(() => {
                b.textContent = `${Swal.getTimerLeft()}`;
            }, 100);
        },
        willClose: () => {
            clearInterval(timerInterval);
        }
    }).then((result) => {
        if (result.dismiss === Swal.DismissReason.timer) {
            return;
        }
    });
}
