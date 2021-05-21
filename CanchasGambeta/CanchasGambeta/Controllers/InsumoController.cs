﻿using CanchasGambeta.Models;
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

            List<Insumo> listaInsumo = AccesoBD.AD_Insumo.obtenerInsumos();
            return View(listaInsumo);
        }

        [HttpPost]
        public ActionResult MisInsumos(string insumo, decimal precio, int stock)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Insumo.nuevoInsumo(insumo, precio, stock);
                if (resultado) return RedirectToAction("MisInsumos", "Insumo");
                else
                {
                    ViewBag.ErrorInsertInsumo = "Ocurrió un error al cargar el nuevo insumo. Inténtelo nuevamente.";
                    List<Insumo> listaInsumo = AccesoBD.AD_Insumo.obtenerInsumos();
                    return View(listaInsumo);
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
        public ActionResult ModificarInsumo(Insumo insumo)
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null) return RedirectToAction("LogIn", "LogIn");

            if (ModelState.IsValid)
            {
                bool resultado = AccesoBD.AD_Insumo.modificarInsumo(insumo);
                if (resultado) return RedirectToAction("MisInsumos", "Insumo");
                else
                {
                    ViewBag.ErrorModificarInsumo = "Ocurrió un error al modificar el insumo. Inténtelo nuevamente.";
                    return View(insumo);
                }
            }
            return View(insumo);
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
                    ViewBag.ErrorEliminarInsumo = "Ocurrió un error al eliminar el insumo. Inténtelo nuevamente.";
                    List<Insumo> listaInsumo = AccesoBD.AD_Insumo.obtenerInsumos();
                    return View(listaInsumo);
                }
            }           
            return View();
        }
    }
}