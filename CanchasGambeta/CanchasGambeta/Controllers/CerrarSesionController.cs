using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class CerrarSesionController : Controller
    {
        // GET: CerrarSesion
        //------------------------------------CLIENTE-----------------------------------------
        public ActionResult IndexCliente()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PerfilCliente()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarCliente()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //------------------------------------EQUIPO-----------------------------------------
        public ActionResult MiEquipo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CambiarNombreEquipo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarIntegrante()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult NuevoEquipo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //------------------------------------ADMINISTRADOR-----------------------------------------
        public ActionResult IndexAdministrador()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarPerfilAdministrador()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PerfilAdministrador()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //------------------------------------INSUMOS-----------------------------------------
        public ActionResult MisInsumos()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarInsumo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AgregarInsumo()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //------------------------------------RESERVA-----------------------------------------
        public ActionResult MisReservas()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarReserva()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ReservarInsumos()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarInsumosReserva()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //------------------------------------PEDIDOS-----------------------------------------
        public ActionResult MisPedidos()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarPedido()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConcretarPedido()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModificarProveedor()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AgregarInsumosAlPedido()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerDetallePedido()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MisProveedores()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //------------------------------------INFORMES-----------------------------------------
        public ActionResult CanchasMasUtilizadas()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ClientesConMasReservas()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ReservasActivas()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ReservasCanceladas()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult HorariosMasReservados()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult InstrumentosRotos()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult InsumosMasConsumidos()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //------------------------------------Términos y condiciones/Ayuda-----------------------------------------

        public ActionResult TerminosYCondiciones()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Ayuda()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}