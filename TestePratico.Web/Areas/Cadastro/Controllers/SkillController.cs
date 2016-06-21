using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePratico.Dominio.Classes;
using TestePratico.Web.Areas.Cadastro.Models;
using TestePratico.Web.Controllers;
using TestePratico.Web.Filtros;

namespace TestePratico.Web.Areas.Cadastro.Controllers
{
    [Filtros.ControleAcesso(true)]
    public class SkillController : ControladoraPadrao
    {
        private SkillsModel skillModel = null;

        //
        // GET: /Cadastro/Skill/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObterTodos()
        {
            try
            {
                skillModel = SkillsModel.CarregarModel();
            }
            catch (Exception)
            {
            }

            return Json(skillModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obter(int id)
        {
            try
            {
                skillModel = SkillsModel.CarregarModel(id);
            }
            catch (Exception)
            {
            }

            return Json(skillModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Novo()
        {
            try
            {
                skillModel = SkillsModel.CarregarModel();
            }
            catch (Exception)
            {
            }

            return Json(skillModel, JsonRequestBehavior.AllowGet);
        }

        [ControleTransacao]
        public JsonResult Excluir(int id)
        {
            try
            {
                Skills skill = Skills.ObterPorId(id); 

                if (skill.SkillsUsuarios.Count() > 0)
                {
                    AlertaJson = true;
                    throw new Exception("Skill possui usuário associado não pode ser exclúido.");
                }
                else
                {
                    skill.Excluir();                    
                }
            }
            catch (Exception e)
            {
                OkJson = false;
                MensagemJson = e.Message;
            }

            return Json(RetornoJson(), JsonRequestBehavior.AllowGet);
        }

        [ControleTransacao]
        public JsonResult Salvar(SkillsModel model)
        {
            try
            {
                Skills skills = new Skills(); 

                if (model.Id > 0)
                {
                    skills = Skills.ObterPorId(model.Id);
                }

                skills.Descricao = model.Descricao;
                skills.Gravar();                
            }
            catch (Exception e)
            {
                MensagemJson = e.InnerException.ToString();
            }

            return Json(RetornoJson(), JsonRequestBehavior.AllowGet);
        }
    }
}
