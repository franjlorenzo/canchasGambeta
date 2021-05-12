'use strict';

const mostrarBotones = () => {
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

const mostrarPedidosConcretados = () => {
    var pedidosConcretados = document.getElementById("tablaPedidosConcretados");

    if (pedidosConcretados.style.display == "none") pedidosConcretados.style.display = "inline-table";
    else pedidosConcretados.style.display = "none";
}

function validacionNuevoPedido() {
    var txtDescripcion = document.getElementById("NuevoPedido_Descripcion").value;
    var cboProveedor = document.getElementById("idProveedor").value;

    if (cboProveedor == "" | !txtDescripcion) {
        alert("Faltan campos obligatorios para realizar un pedido.");
        return false;
    }

    return true;
}

function validacionModificarPedido() {
    var txtDescripcion = document.getElementById("descripcion").value;
    var cboProveedor = document.getElementById("proveedor").value;

    if (cboProveedor == "" | !txtDescripcion) {
        alert("Faltan campos obligatorios para modificar el pedido.");
        return false;
    }

    return true;
}

function confirmacionEliminar() {
    var eliminar = confirm("¿Esta seguro de que quiere eliminar este pedido?");
    if (eliminar) return true;
    return false;
}