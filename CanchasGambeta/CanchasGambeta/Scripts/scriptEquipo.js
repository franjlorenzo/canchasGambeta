'use strict';

let btnNuevoIntegrante = document.getElementById("btnNuevoIntegrante");
let divNuevoIntegrante = document.getElementById("divNuevoIntegrante");
let cantmiembros = document.getElementsByClassName("miembro");

if (cantmiembros.length == 13) {
    btnNuevoIntegrante.style.display = "none";
    divNuevoIntegrante.style.display = "none";
}

function validacionNuevoEquipo() {
    let txtNombreEquipo = document.getElementById("txtNombreEquipo").value;

    if (!txtNombreEquipo) {
        alert("El equipo debe tener un nombre!");
        return false;
    }
    return true;
}

function validacionNuevoIntegrante() {
    let txtEmail = document.getElementById("txtEmail").value;
    if (!txtEmail) {
        alert("Debe ingresar un correo electrónico para agregar un nuevo miembro al equipo!")
        return false;
    }
    if (!validarEmail(txtEmail)) {
        alert("El email que ingresó no es válido");
        return false;
    }
    return true;
}

function validacionModificarIntegrante() {
    let txtEmail = document.getElementById("txtEmail").value;
    let txtEmailOriginal = document.getElementById("txtEmailOriginal").value;
    if (!txtEmail) {
        alert("No puede dejar el campo vacio.");
        return false;
    }
    if (txtEmailOriginal == txtEmail) {
        alert("El email que ingresó es igual al anterior.");
        return false;
    }
    if (!validarEmail(txtEmail)) {
        alert("El email que ingresó no es válido.");
        return false;
    }
    return true;
}

const botones = () => {
    let acciones = document.getElementsByClassName("text-center acciones thfix");
    let botonesAcciones = document.getElementsByClassName("text-center acciones");

    if (acciones[0].style.display == "none") {
        for (let i = 0; i < acciones.length; i++) {
            acciones[i].style.display = "revert";
        }
        for (let i = 0; i < botonesAcciones.length; i++) {
            botonesAcciones[i].style.display = "revert";
        }
    }
    else {
        for (let i = 0; i < acciones.length; i++) {
            acciones[i].style.display = "none";
        }
        for (let i = 0; i < botonesAcciones.length; i++) {
            botonesAcciones[i].style.display = "none";
        }
    }
}

function confirmarEliminarIntegrante() {
    let confirmar = confirm("¿Está seguro de que desea eliminar al integrante?");
    if (confirmar) {
        return true;
    }
    return false;
}

function confirmarEliminarEquipo() {
    let confirmar = confirm("¿Está seguro de que desea eliminar su equipo?");
    if (confirmar) {
        return true;
    }
    return false;
}

function validarEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

