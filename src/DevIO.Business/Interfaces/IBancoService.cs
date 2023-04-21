using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IBancoService : IDisposable
    {
        Task<bool> Incluir(Banco Banco);
        Task<bool> Alterar(Banco Banco);
        Task Excluir(int id);

    }
}
