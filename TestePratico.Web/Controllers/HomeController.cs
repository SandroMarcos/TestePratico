using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestePratico.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Bloqueia acesso
        /// </summary>
        /// <returns>View de bloqueio</returns>
        public ActionResult AcessoNaoPermitido()
        {
            return View("AcessoNaoPermitido");
        }
    }
}
