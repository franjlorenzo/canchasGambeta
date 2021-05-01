const validacionModificarCliente = () => {
    let txtNombreCompleto = document.getElementById("txtNombre").value;
    let txtEmail = document.getElementById("txtEmail").value;
    let txtTelefono = document.getElementById("txtTelefono").value;
    let txtPassword = document.getElementById("txtPassword").value;

    if (!txtNombreCompleto | !txtEmail | !txtTelefono | !txtPassword) {
        alert("No puede dejar uno de los campos vacios");
        return false;
    }

    return true
}