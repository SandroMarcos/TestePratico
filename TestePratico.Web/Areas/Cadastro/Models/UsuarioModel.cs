using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestePratico.Dominio.Classes;
using System.Web.Mvc;
using TestePratico.Dominio.Infra;

namespace TestePratico.Web.Areas.Cadastro.Models
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {
            CarregarTiposUsuario();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public int TipoUsuario { get; set; }

        public string DescricaoTipo { get; set; }

        public bool Administrador { get; set; }

        public string HeAdministrador { get; set; }

        public List<SelectListItem> TiposUsuario { get; set; }

        public IList<UsuarioModel> listagem { get; set; }

        public static UsuarioModel CarregarModel()
        {
            var model = new UsuarioModel();
            model.listagem = Usuario.Consultar()
                .Select(x => new UsuarioModel()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Login = x.Login,
                    TipoUsuario = (int)x.TipoUsuario,
                    DescricaoTipo = x.TipoUsuario == Dominio.Classes.TipoUsuario.Programador ? "Programador" : "Designer",
                    Administrador = x.Administrador,
                    HeAdministrador = x.Administrador ? "Sim" : "Não"
                }).ToList();

            return model;

        }

        public static UsuarioModel CarregarModel(int id)
        {
            var usuario = Usuario.ObterPorId(id);
            var model = new UsuarioModel()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = CriptografiaAES.Decrypt(usuario.Senha,CriptografiaAES.CHAVE_AES,CriptografiaAES.AESCryptographyLevel.AES_128),
                    TipoUsuario = (int)usuario.TipoUsuario,
                    Administrador = usuario.Administrador                    
                };

            model.listagem = Usuario.Consultar()
                .Select(x => new UsuarioModel()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Login = x.Login,
                    TipoUsuario = (int)x.TipoUsuario,
                    DescricaoTipo = x.TipoUsuario == Dominio.Classes.TipoUsuario.Programador ? "Programador" : "Designer",
                    Administrador = x.Administrador,
                    HeAdministrador = x.Administrador ? "Sim" : "Não"
                }).ToList();

            return model;

        }

        /// <summary>
        /// Carrega as situações do atendimento
        /// </summary>
        private void CarregarTiposUsuario()
        {
            TiposUsuario = new List<SelectListItem>();            
            TiposUsuario.Add(new SelectListItem() { Text = "Programador", Value = ((int)Dominio.Classes.TipoUsuario.Programador).ToString() });
            TiposUsuario.Add(new SelectListItem() { Text = "Designer", Value = ((int)Dominio.Classes.TipoUsuario.Designer).ToString()  });
            
        }
    }
}