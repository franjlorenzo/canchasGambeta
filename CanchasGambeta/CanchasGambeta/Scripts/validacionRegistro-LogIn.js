'use strict';

const validacionRegistro = () => {
    let txtNombreUsuario = document.getElementById("txtUsuario").value;
    let txtEmail = document.getElementById("txtEmail").value;
    let txtTelefono = document.getElementById("txtTelefono").value;
    let txtPassword = document.getElementById("txtPassword").value;
    let txtConfirmaPassword = document.getElementById("txtConfirmaPassword").value;

    if (!txtNombreUsuario | !txtEmail | !txtTelefono | !txtPassword | !txtConfirmaPassword) {
        alert("Faltan campos obligatorios para completar el registro.");
        return false;
    }
    else if (txtPassword != txtConfirmaPassword) {
        alert("Las contraseñas no coinciden.");
        return false;
    }
    else if (txtTelefono.length < 10) {
        alert("El teléfono que ingresó no es válido");
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

const validacionLogIn = () => {
    let txtEmail = document.getElementById("txtEmail").value;
    let txtPassword = document.getElementById("txtPassword").value;

    if (!txtEmail | !txtPassword) {
        alert("Faltan campos obligatorios.");
        return false;
    }
    return true;
}