using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface ITipoPagamentoRepository : IRepository<TipoPagamento>
    {
        Task<List<TipoPagamento>> RetornarTodosPOC(int status);
        Task<List<TipoPagamento>> ConsultarPOC(string descricao);
        Task<TipoPagamento> ObterPorIdPOC(int id);
        Task<TipoPagamento> ObterPorNomePOC(string descricao);
        Task IncluirPOC(TipoPagamento valor);
        Task AlterarPOC(TipoPagamento valor);
        Task ExlcuirPOC(int Id);

    }
}

