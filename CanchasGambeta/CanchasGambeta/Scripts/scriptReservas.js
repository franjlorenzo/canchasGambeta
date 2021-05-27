'use strict';

function validacionNuevaReserva() {
    let cboCancha = document.getElementById("idCancha").value;
    let cboHorario = document.getElementById("idHorario").value;
    let fecha = document.getElementById("NuevaReservaConDropDownList_fecha").value;

    if (cboCancha == "" | cboHorario == "" | fecha == "") {
        if (cboCancha == "") {
            alert("Debe seleccionar una cancha para registrar la reserva");
            return false;
        }
        if (cboHorario == "") {
            alert("Debe seleccionar un horario para registrar la reserva");
            return false;
        }
    }
    return true;
}


const botonAcciones = () => {
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

const botonInsumos = () => {
    let tablaInsumos = document.getElementById("tablaInsumos");

    if (tablaInsumos.style.display == "none") {
        tablaInsumos.style.display = "inline-table";
    }
    else {
        tablaInsumos.style.display = "none"
    }
}

function confirmacionEliminar() {
    var eliminar = confirm("¿Esta seguro de que quiere eliminar esta reserva?");
    if (eliminar) return true;
    return false;
}

var idHorario = document.getElementById("idHorario");
if (idHorario.length > 1) {
    idHorario.removeAttribute("disabled");
}

var fechaNueva = document.getElementById("Fecha");
fechaNueva.removeAttribute("data-val-required");
