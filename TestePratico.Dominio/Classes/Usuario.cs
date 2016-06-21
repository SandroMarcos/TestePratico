using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestePratico.Dominio.Persistencia;
using TestePratico.Dominio.Infra;

namespace TestePratico.Dominio.Classes
{
    /// <summary>
    /// Classe de Usuários do sistema
    /// </summary>
    public class Usuario : EntidadePersistente<Usuario, IRepositorio<Usuario>>
    {
        public Usuario()
            : base()
        {

        }

        public virtual string Nome { get; set; }

        public virtual string Login { get; set; }

        public virtual string Senha { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        public virtual bool Administrador { get; set; }

        public virtual IList<SkillsUsuarios> SkillsUsuarios { get; set; }

        /// <summary>
        /// Inseri um novo usuário
        /// Antes de inserir, verifica se já existe o login desejado
        /// </summary>
        /// <param name="nome">Nome do usuário</param>
        /// <param name="login">Login  do usuário</param>
        /// <param name="senha">Senha  do usuário</param>
        /// <param name="admin">Define se o usuário é administrador ou não</param>
        /// <param name="tipoUsuario">Tipo de Usuário</param>
        /// <param name="id">Idendificador do usuário no banco de dados</param>
        /// <returns>Verdade caso não possui o login desejado, caso contrário falso</returns>
        public static bool IncluirUsuario(string nome, string login, string senha, bool admin, int tipoUsuario, int id = 0)
        {
            bool existe = true;

            if (Usuario.Count(x => x.Login == login && x.Id != id) == 0)
            {
                var usuario = new Usuario();
                existe = false;

                if (id > 0)
                {
                    usuario = Usuario.ObterPorId(id);
                }

                usuario.Administrador = admin;
                usuario.Login = login;
                usuario.Nome = nome;
                //usuario.Senha = Criptografia.Instancia.CalcularMD5Hash(senha);
                usuario.Senha = CriptografiaAES.Encrypt(senha, CriptografiaAES.CHAVE_AES, CriptografiaAES.AESCryptographyLevel.AES_128);
                usuario.TipoUsuario = (TipoUsuario)Enum.Parse(typeof(TipoUsuario), tipoUsuario.ToString());
                usuario.Gravar();
            }

            return existe;
        }

        /// <summary>
        /// Método sobreposto
        /// </summary>
        public override void Excluir()
        {
            TestePratico.Dominio.Classes.SkillsUsuarios.ExcluirSkillUsuario(this.SkillsUsuarios);

            base.Excluir();
        }
    }

}
