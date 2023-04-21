using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoPagamentoService : IDisposable
    {
        Task<bool> Incluir(TipoPagamento zona);
        Task<bool> Alterar(TipoPagamento zona);
        Task Excluir(int id);

    }
}
