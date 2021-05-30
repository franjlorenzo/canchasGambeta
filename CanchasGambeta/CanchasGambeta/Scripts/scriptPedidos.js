'use strict';

const mostrarBotones = () => {
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

const mostrarPedidosConcretados = () => {
    let pedidosConcretados = document.getElementById("tablaPedidosConcretados");

    if (pedidosConcretados.style.display == "none") pedidosConcretados.style.display = "inline-table";
    else pedidosConcretados.style.display = "none";
}

function validacionNuevoPedido() {
    let txtDescripcion = document.getElementById("NuevoPedido_Descripcion").value;
    let cboProveedor = document.getElementById("idProveedor").value;

    if (cboProveedor == "" | !txtDescripcion) {
        alert("Faltan campos obligatorios para realizar un pedido.");
        return false;
    }

    return true;
}

function validacionModificarPedido() {
    let txtDescripcion = document.getElementById("descripcion").value;
    let cboProveedor = document.getElementById("proveedor").value;

    if (cboProveedor == "" | !txtDescripcion) {
        alert("Faltan campos obligatorios para modificar el pedido.");
        return false;
    }

    return true;
}

function confirmacionEliminar() {
    let eliminar = confirm("¿Esta seguro de que quiere eliminar este pedido?");
    if (eliminar) return true;
    return false;
}