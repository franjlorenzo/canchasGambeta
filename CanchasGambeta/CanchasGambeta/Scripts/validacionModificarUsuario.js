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

    if (txtPassword != txtConfirmarPassword) {
        alert("Las contraseñas no coinciden");
        return false;
    }

    if (!validarEmail(txtEmail)) {
        alert("El email que ingresó no es válido");
        return false;
    }

    return true;
}

function validarEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}