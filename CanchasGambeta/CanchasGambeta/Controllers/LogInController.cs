﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string Email, string Password)
        {
            try
            {
                using (Models.Canchas_GambetaEntities3 db = new Models.Canchas_GambetaEntities3())
                {
                    var oUser = (from data in db.Usuario
                                 where data.email == Email.Trim() && data.password == Password.Trim()
                                 select data).FirstOrDefault(); //FirstOrDefault devuelve el elemento o devuelve null

                    if(oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }

                    Session["User"] = oUser;
                    Session.Timeout = 10;

                    if(oUser.rol == 2)
                    {
                        return RedirectToAction("IndexCliente", "IndexCliente");
                    }
                    else
                    {
                        return RedirectToAction("IndexAdministrador", "IndexAdministrador");
                    }
                } 
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}