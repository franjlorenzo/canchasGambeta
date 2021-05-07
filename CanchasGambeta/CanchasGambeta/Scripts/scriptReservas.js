function validacionNuevaReserva() {
    let cboCancha = document.getElementById("idCancha").value;
    let cboHorario = document.getElementById("idHorario").value;
    let fecha = document.getElementById("NuevaReservaVM_Fecha").value;

    if (cboCancha == "" | cboHorario == "") {
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