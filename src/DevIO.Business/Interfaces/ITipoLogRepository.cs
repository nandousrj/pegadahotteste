using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoLogRepository : IRepository<TipoLog>
    {
        Task<List<TipoLog>> RetornarTodosPOC();
        Task<List<TipoLog>> ConsultarPOC(string descricao);
        Task<TipoLog> ObterPorIdPOC(int id);
        Task<TipoLog> ObterPorNomePOC(string descricao);
        Task IncluirPOC(TipoLog valor);
        Task AlterarPOC(TipoLog valor);
        Task ExlcuirPOC(int Id);

    }
}
