using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IPermissoesUsuarioService : IDisposable
    {
        Task<bool> Incluir(PermissoesUsuario permissoesUsuario);
        Task<bool> Alterar(PermissoesUsuario permissoesUsuario);

        Task<PermissoesUsuario> RetornarUsuario(int id_usuario);
      //  Task Excluir(int id);

    }
}
