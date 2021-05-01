using CanchasGambeta.Controllers;
using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanchasGambeta.Filters
{
    public class VerificarSesion : ActionFilterAttribute
    {
        private Usuario oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (Usuario)HttpContext.Current.Session["User"];

                if (oUsuario == null)
                {
                    if (filterContext.Controller is LogInController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/Index");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/LogIn/LogIn");
            }
        }
    }
}