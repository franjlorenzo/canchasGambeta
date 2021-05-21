'use strict';

function validacionNuevaReserva() {
    let cboCancha = document.getElementById("idCancha").value;
    let cboHorario = document.getElementById("NuevaReservaConDropDownList_Horarios").value;
    let fecha = document.getElementById("fechaReservaElegida").value;

    if (cboCancha == "" | cboHorario == "" | fecha == "") {
        alert("Debe seleccionar un horario para registrar la reserva.");
        return false;
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
    var eliminar = confirm("¿Esta seguro de que quiere eliminar este pedido?");
    if (eliminar) return true;
    return false;
}

/*function actualizarFechaHidden() {
    var inputFecha = document.getElementById("idFechaReserva").value;
    var fechaVisible = new Date(inputFecha.value);
    fechaVisible.setMinutes(fechaVisible.getMinutes() + fechaVisible.getTimezoneOffset());
    fechaVisible = fechaVisible.getTime();

    let dia = String(fechaVisible.getDate());
    let mes = String(fechaVisible.getMonth() + 1);
    let anio = String(fechaVisible.getFullYear());

    if (mes.length < 2) mes = '0' + mes;
    if (dia.length < 2) dia = '0' + dia;

    document.getElementById("fechaReservaElegida").value = `${dia}/${mes}/${anio}`;
}*/

var idHorario = document.getElementById("NuevaReservaConDropDownList_Horarios");
if (idHorario.length > 1) {
    idHorario.removeAttribute("disabled");
}

var fechaNueva = document.getElementById("Fecha");
fechaNueva.removeAttribute("data-val-required");
