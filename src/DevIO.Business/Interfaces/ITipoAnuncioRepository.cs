using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface ITipoAnuncioRepository : IRepository<TipoAnuncio>
    {
        Task<List<TipoAnuncio>> RetornarTodosPOC(int status);
        Task<List<TipoAnuncio>> ConsultarPOC(string descricao);
        Task<TipoAnuncio> ObterPorIdPOC(int id);
        Task<TipoAnuncio> ObterPorNomePOC(string descricao);
        Task IncluirPOC(TipoAnuncio valor);
        Task AlterarPOC(TipoAnuncio valor);
        Task ExlcuirPOC(int Id);

    }
}

