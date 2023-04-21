using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoContaService : IDisposable
    {
        Task<bool> Incluir(TipoConta tipoconta);
        Task<bool> Alterar(TipoConta tipoconta);
        Task Excluir(int id);

    }
}
