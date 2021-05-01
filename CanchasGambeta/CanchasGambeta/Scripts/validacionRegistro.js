function validacionRegistro() {
    let txtNombreUsuario = document.getElementById("txtUsuario").value;
    let txtEmail = document.getElementById("txtEmail").value;
    let txtTelefono = document.getElementById("txtTelefono").value;
    let txtPassword = document.getElementById("txtPassword").value;
    let txtConfirmaPassword = document.getElementById("txtConfirmaPassword").value;

    if (!txtNombreUsuario | !txtEmail | !txtTelefono | !txtPassword | !txtConfirmaPassword) {
        alert("Faltan campos obligatorios.");
        return false;
    }
    else if (!txtPassword === txtConfirmaPassword) {
        alert("Las contraseñas no coinciden.");
        return false;
    }
    return true;
};