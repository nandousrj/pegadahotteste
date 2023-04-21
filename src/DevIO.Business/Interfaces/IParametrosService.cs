using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IParametrosService : IDisposable
    {
        Task<bool> Incluir(Parametros parametros);
        Task<bool> Alterar(Parametros parametros);
        Task Excluir(int id);

    }
}
