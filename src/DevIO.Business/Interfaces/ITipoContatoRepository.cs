using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoContatoRepository : IRepository<TipoContato>
    {
        Task<List<TipoContato>> RetornarTodosPOC();
        Task<List<TipoContato>> ConsultarPOC(string descricao);
        Task<TipoContato> ObterPorIdPOC(int id);
        Task<TipoContato> ObterPorNomePOC(string descricao);
        Task IncluirPOC(TipoContato valor);
        Task AlterarPOC(TipoContato valor);
        Task ExlcuirPOC(int Id);

    }
}
