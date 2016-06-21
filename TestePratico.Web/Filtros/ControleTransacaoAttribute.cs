using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePratico.Dominio.Infra;
using TestePratico.Dominio.Persistencia;

namespace TestePratico.Web.Filtros
{
    public class ControleTransacaoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Fabrica.Instancia.Obter<IUnidadeTrabalho>().BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var ut = Fabrica.Instancia.Obter<IUnidadeTrabalho>();

            if (filterContext.Exception == null)
            {
                try
                {
                    ut.Commit();
                }
                catch (Exception e)
                {
                    filterContext.Exception = e;
                }
            }

            if (filterContext.Exception != null)
            {
                ut.Rollback();
            }
        }
    }
}