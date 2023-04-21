using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface ITipoContaRepository : IRepository<TipoConta>
    {
        Task<List<TipoConta>> RetornarTodosPOC();
        Task<List<TipoConta>> ConsultarPOC(string descricao);
        Task<TipoConta> ObterPorIdPOC(int id);
        Task<TipoConta> ObterPorNomePOC(string descricao);
        Task IncluirPOC(TipoConta valor);
        Task AlterarPOC(TipoConta valor);
        Task ExlcuirPOC(int Id);

    }
}

