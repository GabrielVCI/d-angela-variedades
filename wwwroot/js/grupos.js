

async function ObtenerGruposClientes() {
    
}

async function ObtenerGruposClientes() {

    try {

        const response = await fetch(`${urlGrupos}`, {
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

        console.log(json);

        return json;

    } catch (error) {
        manejarErrorApi(error);
        return;
    }
}