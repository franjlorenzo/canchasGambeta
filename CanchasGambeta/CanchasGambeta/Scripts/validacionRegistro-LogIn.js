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
    return true;
};

const validacionLogIn = () => {
    let txtEmail = document.getElementById("txtEmail").value;
    let txtPassword = document.getElementById("txtPassword").value;

    if (!txtEmail | !txtPassword) {
        alert("Faltan campos obligatorios.");
        return false;
    }
    return true;
}

const validacionNuevoEquipo = () => {
    let txtNombreEquipo = document.getElementById("txtNombreEquipo").value;

    if (!txtNombreEquipo) {
        alert("El equipo debe tener un nombre!");
        return false;
    }
    return true;
}