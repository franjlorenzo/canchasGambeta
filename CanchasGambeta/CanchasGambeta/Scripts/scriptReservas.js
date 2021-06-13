'use strict';

let today = new Date();
let dd = today.getDate();
let mm = today.getMonth() + 1; //Enero es 0
let yyyy = today.getFullYear();
if (dd < 10) {
    dd = '0' + dd
}
if (mm < 10) {
    mm = '0' + mm
}

today = yyyy + '-' + mm + '-' + dd;
//document.getElementById("NuevaReservaVM_fecha").setAttribute("min", today);

function validacionNuevaReserva() {
    let cboCancha = document.getElementById("idCancha").value;
    let cboHorario = document.getElementById("idHorario").value;
    let fecha = document.getElementById("NuevaReservaVM_fecha").value;

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
    let acciones = document.getElementsByClassName("text-center acciones");

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
    let eliminar = confirm("¿Esta seguro de que quiere eliminar esta reserva?");
    if (eliminar) return true;
    return false;
}

var idHorario = document.getElementById("idHorario");
if (idHorario.length > 1) {
    idHorario.removeAttribute("disabled");
}
