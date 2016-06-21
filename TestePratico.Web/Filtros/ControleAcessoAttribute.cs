using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePratico.Dominio.Classes;

namespace TestePratico.Web.Filtros
{
    public class ControleAcessoAttribute : ActionFilterAttribute
    {
        private bool admin;

        public ControleAcessoAttribute()
            : base()
        {
        }

        public ControleAcessoAttribute(bool administrador)
        {
            admin = administrador;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            bool actionAutorizada = false;

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string login = filterContext.HttpContext.User.Identity.Name.ToLower();
                var usuario = Usuario.Consultar(x => x.Login.ToLower() == login).FirstOrDefault();
                actionAutorizada = usuario.Administrador == admin ? true : false;
            }

            if (!actionAutorizada)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult result = new JsonResult();
                    result.Data = new { ok = false, message = "<div id='AcessoNaoPermi' style='text-align: center; padding-top:50px;'>Acesso permitido somente para Administradores.</div>" };
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    filterContext.Result = result;
                }
                else
                {
                    RedirectResult redirect = new RedirectResult("~/Home/AcessoNaoPermitido");
                    filterContext.Result = redirect;
                }
            }

        }
    }
}