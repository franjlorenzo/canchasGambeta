using CanchasGambeta.Models;
using CanchasGambeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class InsumoController : Controller
    {
        // GET: Insumo
        public ActionResult MisInsumos()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            List<Insumo> listaInsumo = AccesoBD.AD_Insumo.obtenerTop10InsumosMenosStock();
            if (TempData["nombreInsumo"] != null) ViewBag.nombreInsumo = TempData["nombreInsumo"].ToString();
            if (TempData["buscarInsumoAnterior"] != null) ViewBag.nombreInsumo = TempData["buscarInsumoAnterior"].ToString();
            if (TempData["ErrorEliminarInsumo"] != null) ViewBag.ErrorEliminarInsumo = TempData["ErrorEliminarInsumo"].ToString();
            if (TempData["ErrorBuscarInsumo"] != null) ViewBag.ErrorBuscarInsumo = TempData["ErrorBuscarInsumo"].ToString();
            if (TempData["listaInsumos"] != null)
            {
                listaInsumo = new List<Insumo>();
                foreach (var item in (List<BuscarInsumos>)TempData["listaInsumos"])
                {
                    Insumo insumo = new Insumo();
                    insumo.insumo1 = item.Insumo;
                    insumo.stock = item.Stock;
                    insumo.precio = item.Precio;
                    listaInsumo.Add(insumo);
                }
            }
            return View(listaInsumo);
        }

        [HttpPost]
        public ActionResult BuscarInsumo(string buscarInsumo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (buscarInsumo == "" || buscarInsumo == " ") buscarInsumo = null;

            if (buscarInsumo != null)
            {
                TempData["listaInsumos"] = AccesoBD.AD_Insumo.obtenerInsumosPorNombre(buscarInsumo);
                if (buscarInsumo == "MostrarTodosLosInsumos") return RedirectToAction("MisInsumos");
                else
                {
                    TempData["nombreInsumo"] = buscarInsumo;
                    return RedirectToAction("MisInsumos");
                }
            }
            if (Request["buscarInsumoAnterior"] == "")
            {
                TempData["ErrorBuscarInsumo"] = "Debe proporcionar una letra o palabra para buscar un insumo";
                return RedirectToAction("MisInsumos");
            }
            else
            {
                TempData["buscarInsumoAnterior"] = Request["buscarInsumoAnterior"];
                TempData["ErrorBuscarInsumo"] = "Debe proporcionar una letra o palabra para buscar un insumo";
                TempData["listaInsumos"] = AccesoBD.AD_Insumo.obtenerInsumosPorNombre(Request["buscarInsumoAnterior"]);
                return RedirectToAction("MisInsumos");
            }
        }

        public ActionResult AgregarInsumo()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            return View();
        }

        [HttpPost]
        public ActionResult AgregarInsumo(Insumo nuevoInsumo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Insumo.nuevoInsumo(nuevoInsumo);
                if (resultado) return RedirectToAction("MisInsumos", "Insumo");
                else
                {
                    ViewBag.ErrorInsertInsumo = "Ocurrió un error al cargar el nuevo insumo, inténtelo nuevamente.";
                    return View(nuevoInsumo);
                }
            }
            return View();
        }

        public ActionResult ModificarInsumo(int idInsumo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            Insumo insumo = AccesoBD.AD_Insumo.obtenerInsumoPorId(idInsumo);
            return View(insumo);
        }

        [HttpPost]
        public ActionResult ModificarInsumo(Insumo insumoModificado)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Insumo.modificarInsumo(insumoModificado);
                if (resultado) return RedirectToAction("MisInsumos", "Insumo");
                else
                {
                    ViewBag.ErrorModificarInsumo = "Ocurrió un error al modificar el insumo. Inténtelo nuevamente.";
                    Insumo insumo = AccesoBD.AD_Insumo.obtenerInsumoPorId(insumoModificado.idInsumo);
                    return View(insumo);
                }
            }
            return View(insumoModificado);
        }

        [HttpPost]
        public ActionResult EliminarInsumo()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            int idInsumo = int.Parse(Request["idInsumo"]);

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Insumo.eliminarInsumo(idInsumo);
                if (resultado) return RedirectToAction("MisInsumos", "Insumo");
                else
                {
                    TempData["ErrorEliminarInsumo"] = "Ocurrió un error al eliminar el insumo, inténtelo nuevamente.";
                    return RedirectToAction("MisInsumos", "Insumo");
                }
            }           
            return View();
        }
    }
}