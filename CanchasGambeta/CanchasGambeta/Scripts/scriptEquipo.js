'use strict';

var btnNuevoIntegrante = document.getElementById("btnNuevoIntegrante");
var divNuevoIntegrante = document.getElementById("divNuevoIntegrante");
var cantmiembros = document.getElementsByClassName("miembro");

if (cantmiembros.length == 13) {
    btnNuevoIntegrante.style.display = "none";
    divNuevoIntegrante.style.display = "none";
}

function validacionNuevoIntegrante() {
    var txtEmail = document.getElementById("txtEmail").value;
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
    var txtEmail = document.getElementById("txtEmail").value;
    if (!txtEmail) {
        alert("No puede dejar el campo vacio.");
        return false;
    }
    if (!validarEmail(txtEmail)) {
        alert("El email que ingresó no es válido");
        return false;
    }
    return true;
}

const botones = () => {
    var acciones = document.getElementsByClassName("text-center acciones");

    if (acciones[0].style.display == "none") {
        for (var i = 0; i < acciones.length; i++) {
            acciones[i].style.display = "block"; 
        }
    }
    else {
        for (var i = 0; i < acciones.length; i++) {
            acciones[i].style.display = "none";
        }
    }
}

function confirmarEliminarIntegrante() {
    var confirmar = confirm("¿Está seguro de que desea eliminar al integrante?");
    if (confirmar) {
        return true;
    }
    return false;
}

function confirmarEliminarEquipo() {
    var confirmar = confirm("¿Está seguro de que desea eliminar su equipo?");
    if (confirmar) {
        return true;
    }
    return false;
}

function validarEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

