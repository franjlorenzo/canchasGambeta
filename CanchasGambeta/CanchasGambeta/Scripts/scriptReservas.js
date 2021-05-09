function validacionNuevaReserva() {
    let cboCancha = document.getElementById("idCancha").value;
    let cboHorario = document.getElementById("idHorario").value;
    let fecha = document.getElementById("NuevaReservaVM_Fecha").value;

    if (cboCancha == "" | cboHorario == "" | fecha == "") {
        alert("Debe seleccionar una fecha/cancha/horario para registrar la reserva.");
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

function buscarHorarios() {
    console.log("prueba");
}