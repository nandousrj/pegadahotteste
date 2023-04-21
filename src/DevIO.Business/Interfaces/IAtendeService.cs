using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IAtendeService : IDisposable
    {
        Task<bool> Incluir(Atende dados);
        Task<bool> Alterar(Atende dados);
        Task Excluir(int id);

    }
}
