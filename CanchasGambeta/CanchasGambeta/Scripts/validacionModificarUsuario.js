'use strict';

const validacionModificarUsuario = () => {
    let txtNombreCompleto = document.getElementById("txtNombre").value;
    let txtEmail = document.getElementById("txtEmail").value;
    let txtTelefono = document.getElementById("txtTelefono").value;
    let txtPassword = document.getElementById("txtPassword").value;
    let txtConfirmarPassword = document.getElementById("txtConfirmarPassword").value;

    if (!txtNombreCompleto | !txtEmail | !txtTelefono | !txtPassword | !txtConfirmarPassword) {
        alert("No puede dejar uno de los campos vacios");
        return false;
    }
    else if (!validarEmail(txtEmail)) {
        alert("El email que ingresó no es válido");
        return false;
    }
    else if (txtTelefono.length < 10) {
        alert("El teléfono que ingresó no es válido");
        return false;
    }
    else if (isNaN(txtTelefono) | txtTelefono.includes(".")) {
        alert("El campo teléfono solo debe contener números.");
        return false;
    }
    else if (txtPassword != txtConfirmarPassword) {
        alert("Las contraseñas no coinciden");
        return false;
    }

    return true;
}

function validarEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}