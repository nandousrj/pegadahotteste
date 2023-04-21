using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface INovidadeService : IDisposable
    {
        Task<bool> Incluir(Novidade novidade);
        Task<bool> Alterar(Novidade novidade);
        Task Excluir(int id);

    }
}
