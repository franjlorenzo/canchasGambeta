'use strict';

function validacionNuevoInsumo() {
    let txtInsumo = document.getElementById("txtInsumo").value;
    let txtPrecio = document.getElementById("txtPrecio").value;
    let txtStock = document.getElementById("txtStock").value;

    if (!txtInsumo | !txtPrecio | !txtStock) {
        alert("Debe llenar todos los campos para cargar un nuevo insumo.");
        return false;
    }
    return true;
}

function validacionModificarInsumo() {
    let txtInsumo = document.getElementById("txtInsumo").value;
    let txtPrecio = document.getElementById("txtPrecio").value;
    let txtStock = document.getElementById("txtStock").value;

    if (!txtInsumo | !txtPrecio | !txtStock) {
        alert("No puede dejar un campo vacio al realizar la modificación.");
        return false;
    }
    return true;
}

const botones = () => {
    let acciones = document.getElementsByClassName("text-center acciones");

    if (acciones[0].style.display == "none") {
        for (let i = 0; i < acciones.length; i++) {
            acciones[i].style.display = "block";
        }
    }
    else {
        for (let i = 0; i < acciones.length; i++) {
            acciones[i].style.display = "none";
        }
    }
}

function confirmacionEliminarInsumo(){
    let confirmar = confirm("¿Está seguro de que desea eliminar este insumo?");
    if (confirmar) {
        return true;
    }
    return false;
}