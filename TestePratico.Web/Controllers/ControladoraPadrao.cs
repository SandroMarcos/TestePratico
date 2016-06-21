using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestePratico.Web.Controllers
{
    public class ControladoraPadrao : Controller
    {
        #region Construtores

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ControladoraPadrao()
        {
            OkJson = true;
            AlertaJson = false;
            RegraNegocioJson = false;
            MensagemJson = string.Empty;
            TipoMensagemJson = "information";
        }

        #endregion

        #region Propriedades protegidas

        /// <summary>
        /// Será utilizado no retorno "ok" do Json
        /// </summary>
        protected bool OkJson { get; set; }

        /// <summary>
        /// Será utilizado no retorno "alerta" do Json
        /// </summary>
        protected bool AlertaJson { get; set; }

        /// <summary>
        /// Será utilizado no retorno "regra negócio" do Json
        /// </summary>
        protected bool RegraNegocioJson { get; set; }

        /// <summary>
        /// Será utilizado no retorno "mensagem" do Json
        /// </summary>
        protected string MensagemJson { get; set; }

        /// <summary>
        /// Indica erro em um campo específico
        /// </summary>
        protected string CampoRetorno { get; set; }

        /// <summary>
        /// Tipo de mensagem de retorno
        /// </summary>
        protected string TipoMensagemJson { get; set; }

        protected object Dados { get; set; }

        /// <summary>
        /// Exibição dos erros encontrados
        /// </summary>
        protected KeyValuePair<string, string[]>[] ErrosJson { get; set; }

        #endregion

        #region Métodos protegidos

        /// <summary>
        /// Realiza a validação dos campos obrigatórios do modelo
        /// </summary>
        /// <returns>Retorna verdadeiro caso os parâmetros dos modelos sejam válidos</returns>
        protected bool ValidarModelo()
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                    .Where(v => v.Value.Count() > 0);

                OkJson = false;
                AlertaJson = true;
                MensagemJson = "Dados inválidos.";
                ErrosJson = erros.ToArray();
                TipoMensagemJson = "error";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Monta o objeto Json que será retornado para a página solicitante
        /// </summary>
        /// <returns>Retorna um objeto Json</returns>
        protected object RetornoJson()
        {
            object retorno = new
            {
                ok = OkJson,
                alerta = AlertaJson,
                regraNegocio = RegraNegocioJson,
                message = MensagemJson,
                campo = CampoRetorno,
                erros = ErrosJson,
                tipoMsg = TipoMensagemJson,
                dados = Dados
            };

            return retorno;
        }

        #endregion


    }
}
