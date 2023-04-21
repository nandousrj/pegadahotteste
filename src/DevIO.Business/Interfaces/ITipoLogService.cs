using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoLogService : IDisposable
    {
        Task<bool> Incluir(TipoLog TipoLog);
        Task<bool> Alterar(TipoLog TipoLog);
        Task Excluir(int id);

    }
}
