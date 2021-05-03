﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanchasGambeta.Models;

namespace CanchasGambeta.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult IndexCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }

        public ActionResult PerfilCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            return View();
        }

        [HttpPost]
        public ActionResult PerfilCliente(Usuario usuario)
        {
            return View();
        }

        public ActionResult ModificarCliente()
        {
            var sesion = (Usuario)HttpContext.Session["User"];
            if (sesion == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            Usuario usuario = AccesoBD.AD_Usuario.obtenerUsuario(sesion.idUsuario);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult ModificarCliente(Usuario usuario, int idUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool resultado = AccesoBD.AD_Usuario.actualizarUsuario(usuario);
                    if (resultado)
                    {
                        var oUser = AccesoBD.AD_Usuario.obtenerUsuario(idUsuario);
                        Session["User"] = oUser;

                        return RedirectToAction("PerfilCliente", "Cliente");
                    }
                    else
                    {
                        return View(usuario);
                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

            return View(usuario);
        }
    }
}