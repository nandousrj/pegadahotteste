using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITrabalhoService : IDisposable
    {
        Task<bool> Incluir(Trabalho trabalho);
        Task<bool> Alterar(Trabalho trabalho);
        Task Excluir(int id);

    }
}
