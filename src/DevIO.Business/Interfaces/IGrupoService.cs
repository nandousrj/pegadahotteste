using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IGrupoService : IDisposable
    {
        Task<bool> Incluir(Grupo grupo);
        Task<bool> Alterar(Grupo grupo);
        Task Excluir(int id);

    }
}
