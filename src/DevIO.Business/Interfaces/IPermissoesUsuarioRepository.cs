using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IPermissoesUsuarioRepository : IRepository<PermissoesUsuario>
    {
        Task<int> VerificarLogin(string login);
        Task<int> VerificaUsuarioSistema(int id_usuario, int id_sistema);
        Task<PermissoesUsuario> ConsultarUsuario(string login);
        Task<PermissoesUsuario> RetornarUsuarioId(int id_usuario, int id_sistema);
        Task<List<PermissoesUsuario>> RetornarTodos(int id_sistema);
        Task<List<PermissoesUsuario>> RetornarUsuarioSemTermo(int id_sistema);
        Task<PermissoesUsuario> RetornarUsuario(int id_usuario);
        Task<PermissoesUsuario> RetornarTodosSistema(int id_sistema);
        Task<List<PermissoesUsuario>> RetornarUsuarioAcessoPagina(int id_menu);
        Task<PermissoesUsuario> ValidarLogin(string login, string senha, int id_sistema);
        Task AtivarUsuario(int id_usuario_acao, int id_usuario, string ip, int id_sistema);
        Task<int> VerificarTrocaSenha(int id_usuario);
        Task<int> VerificarUsuarioSistema(int id_usuario, int id_sistema);
        Task<int> IncluirPOC(PermissoesUsuario valor);
        Task<int> AlterarPOC(PermissoesUsuario valor);
        Task<int> AlterarSenha(PermissoesUsuario valor);
        Task<int> ResetarSenha(PermissoesUsuario valor);
        Task GravarAceiteTermo(int id_usuario, int id_sistema);
    }
}

