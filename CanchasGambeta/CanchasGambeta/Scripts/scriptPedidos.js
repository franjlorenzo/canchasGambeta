'use strict';

const mostrarBotonesPedidos = () => {
    let acciones = document.getElementsByClassName("text-center accionesPedidos");

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

const mostrarBotonesProveedores = () => {
    let acciones = document.getElementsByClassName("text-center accionesProveedores");

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
    let cboProveedor = document.getElementById("idProveedor").value;
    let txtDescripcion = document.getElementById("NuevoPedido_Descripcion").value;

    if (cboProveedor == "" | !txtDescripcion) {
        alert("Faltan campos obligatorios para realizar un pedido.");
        return false;
    }

    return true;
}

function validacionNuevoProveedor() {
    let txtNombreProveedor = document.getElementById("txtNombreProveedor").value;
    let txtTelefono = document.getElementById("txtTelefono").value;
    let txtEmail = document.getElementById("txtEmail").value;
    let txtEmpresa = document.getElementById("txtEmpresa").value;

    if (!txtNombreProveedor | !txtTelefono | txtEmail | txtEmpresa) {
        alert("Faltan campos obligatorios para el registro.");
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

    let cboProveedorAtributos = document.getElementById("proveedor");
    cboProveedorAtributos.removeAttribute("disabled");
    return true;
}

function validacionModificarProveedor() {
    let txtNombreProveedor = document.getElementById("txtNombreProveedor").value;
    let txtTelefono = document.getElementById("txtTelefono").value;
    let txtEmail = document.getElementById("txtEmail").value;
    let txtEmpresa = document.getElementById("txtEmpresa").value;

    if (!txtNombreProveedor | !txtTelefono | txtEmail | txtEmpresa) {
        alert("Faltan campos obligatorios para modificar al proveedor.");
        return false;
    }

    return true;
}

function confirmacionEliminarProveedor() {
    let eliminar = confirm("¿Esta seguro de que quiere eliminar este proveedor?");
    if (eliminar) return true;
    return false;
}

function confirmacionEliminarPedido() {
    let eliminar = confirm("¿Esta seguro de que quiere eliminar este pedido?");
    if (eliminar) return true;
    return false;
}

function validacionConcretarPedido() {

    let inputs = document.getElementsByName('cantidadRecibida');
    let cantidades = [];
    let hayInsumoIgualCero = false;


    for (var i = 0; i < inputs.length; i++) {
        let valor = inputs[i].value;
        cantidades.push(valor);
    }

    for (var i = 0; i < cantidades.length; i++) {
        if (cantidades[i] == "") {
            hayInsumoIgualCero = true;
            break;
        }
    }

    if (hayInsumoIgualCero) {
        let confirmarConcretar = confirm("Algunas cantidades son iguales a cero(0). ¿Está seguro de que quiere concretar el pedido?")
        if (confirmarConcretar) return true;
        return false;
    }
    
    return true;
}