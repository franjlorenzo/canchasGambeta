
var btnNuevoIntegrante = document.getElementById("btnNuevoIntegrante");
var divNuevoIntegrante = document.getElementById("divNuevoIntegrante");
var cantmiembros = document.getElementsByClassName("miembro");
var txtEmail = document.getElementById("txtEmail").value;

if (cantmiembros.length == 13) {
    btnNuevoIntegrante.style.display = "none";
    divNuevoIntegrante.style.display = "none";
}

const validacionNuevoIntegrante = () => {
    if (!txtEmail) {
        alert("Debe ingresar un correo electrónico para agregar un nuevo miembro al equipo!")
        return false;
    }
    return true;
}

const botones = () => {
    var acciones = document.getElementsByClassName("text-center acciones");
    console.log(acciones);

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

