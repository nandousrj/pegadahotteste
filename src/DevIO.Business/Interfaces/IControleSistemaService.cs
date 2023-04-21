using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IControleSistemaService : IDisposable
    {
        Task<bool> Incluir(ControleSistema controlesistema);
        Task<bool> Alterar(ControleSistema controlesistema);
        Task Excluir(int id);

    }
}
