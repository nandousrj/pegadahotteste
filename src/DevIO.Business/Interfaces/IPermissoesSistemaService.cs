using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IPermissoesSistemaService : IDisposable
    {
        Task<bool> Incluir(PermissoesSistema permissoesSistena);
        Task<bool> Alterar(PermissoesSistema permissoesSistena);
        Task Excluir(int id);

    }
}
