'use strict';

let cboCancha = document.getElementById("idCancha").value;
let cboHorario = document.getElementById("idHorario").value;
let fechaOriginal = document.getElementById("fechaReserva").value;
let servicioAsador = document.getElementById("ServicioAsador").value;
let servicioInstrumento = document.getElementById("ServicioInstrumento").value;

function validacionModificarReserva() {
    let cboCanchaCambiado = document.getElementById("idCancha").value;
    let cboHorarioCambiado = document.getElementById("idHorario").value;
    let fechaCambiada = document.getElementById("fechaNuevaReserva").value;
    let servicioAsadorCambiado = document.getElementById("ServicioAsador").value;
    let servicioInstrumentoCambiado = document.getElementById("ServicioInstrumento").value;

    if (cboCancha == cboCanchaCambiado && cboHorario == cboHorarioCambiado && fechaOriginal == fechaCambiada && servicioAsador == servicioAsadorCambiado && servicioInstrumento == servicioInstrumentoCambiado) {
        var ningunCambio = confirm("No realizó ningún cambio en la reserva, ¿Desea continuar?");
        if (ningunCambio) return true;
        return false;
    }
}