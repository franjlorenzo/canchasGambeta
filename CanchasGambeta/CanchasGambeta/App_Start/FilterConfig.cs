using System.Web.Mvc;

namespace CanchasGambeta
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Filters.VerificarSesion());
        }
    }
}