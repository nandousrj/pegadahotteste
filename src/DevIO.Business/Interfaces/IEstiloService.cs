using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IEstiloService : IDisposable
    {
        Task<bool> Incluir(Estilo estilo);
        Task<bool> Alterar(Estilo estilo);
        Task Excluir(int id);

    }
}
