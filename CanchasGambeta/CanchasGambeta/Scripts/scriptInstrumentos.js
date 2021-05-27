'use strict';

function validacionNuevoInstrumento() {
    let txtInstrumento = document.getElementById("txtInstrumento").value;
    let txtFechaCompra = document.getElementById("txtFechaCompra").value;

    if (!txtInstrumento | txtFechaCompra == "") {
        alert("Debe llenar todos los campos para cargar un nuevo instrumento.");
        return false;
    }
    return true;
}

function confirmacionNuevoInstrumentoRoto() {
    var confirmar = confirm("¿Está seguro de que desea mover este elemento a la lista de rotos?");
    if (confirmar) {
        return true;
    }
    return false;
}

function confirmacionEliminarInstrumentoRoto() {
    var confirmar = confirm("¿Está seguro de que desea eliminar este intrumento roto?");
    if (confirmar) {
        return true;
    }
    return false;
}