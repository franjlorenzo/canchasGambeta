'use strict';

function validacionNuevoInsumo() {
    let txtInsumo = document.getElementById("txtInsumo").value;
    let txtPrecio = document.getElementById("txtPrecio").value;
    let txtStock = document.getElementById("txtStock").value;

    if (!txtInsumo | !txtPrecio | !txtStock) {
        alert("Debe llenar todos los campos para cargar un nuevo insumo.");
        return false;
    }
    else if (txtPrecio <= 0 | txtStock < 0) {
        alert("El precio o stock no puede ser menor o igual que cero(0).");
        return false;
    }
    else if (/\s/.test(txtPrecio) | /\s/.test(txtStock)) {
        alert("Uno de los campos contiene espacios no permitidos.");
        return false;
    }
    else if (isNaN(txtPrecio) | isNaN(txtStock) | txtPrecio.includes(".") | txtStock.includes(".")) {
        alert("El precio o stock solo debe contener números enteros.")
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
    else if (txtPrecio <= 0 | txtStock < 0) {
        alert("El precio o stock no puede ser menor o igual que cero(0).");
        return false;
    }
    else if (/\s/.test(txtPrecio) | /\s/.test(txtStock)) {
        alert("Uno de los campos contiene espacios no permitidos.");
        return false;
    }
    else if (isNaN(txtPrecio) | isNaN(txtStock) | txtPrecio.includes(".") | txtStock.includes(".")) {
        alert("El precio o stock solo debe contener números.")
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