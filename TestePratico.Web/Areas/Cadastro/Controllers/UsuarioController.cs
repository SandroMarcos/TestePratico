using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePratico.Web.Controllers;
using TestePratico.Web.Areas.Cadastro.Models;
using TestePratico.Web.Filtros;
using TestePratico.Dominio.Classes;

namespace TestePratico.Web.Areas.Cadastro.Controllers
{
    [Filtros.ControleAcesso(true)]
    public class UsuarioController : ControladoraPadrao
    {
        private UsuarioModel usuarioModel = null;

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObterTodos()
        {
            try
            {
                usuarioModel = UsuarioModel.CarregarModel();
            }
            catch (Exception)
            {
            }

            return Json(usuarioModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obter(int id)
        {
            try
            {
                usuarioModel = UsuarioModel.CarregarModel(id);
            }
            catch (Exception)
            {
            }

            return Json(usuarioModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Novo()
        {
            try
            {
                usuarioModel = UsuarioModel.CarregarModel();
            }
            catch (Exception)
            {
            }

            return Json(usuarioModel, JsonRequestBehavior.AllowGet);
        }

        [ControleTransacao]
        public JsonResult Excluir(int id)
        {
            try
            {
                var usuario = Usuario.ObterPorId(id);
                usuario.Excluir();
            }
            catch (Exception e)
            {
                OkJson = false;
                MensagemJson = e.Message;
            }

            return Json(RetornoJson(), JsonRequestBehavior.AllowGet);
        }

        [ControleTransacao]
        public JsonResult Salvar(UsuarioModel model)
        {
            try
            {
                if (Usuario.IncluirUsuario(model.Nome, model.Login, model.Senha, model.Administrador, model.TipoUsuario, model.Id))
                {
                    MensagemJson = "Já existe este Login cadastrado";
                    OkJson = false;
                    AlertaJson = true;
                }
            }
            catch (Exception e)
            {
                MensagemJson = e.InnerException.ToString();
            }

            return Json(RetornoJson(), JsonRequestBehavior.AllowGet);
        }

    }
}
