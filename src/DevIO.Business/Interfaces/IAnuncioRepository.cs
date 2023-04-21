using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IAnuncioRepository : IRepository<Anuncio>
    {
        Task<List<Anuncio>> RetornarTodosPOC(int status);
        Task<List<Anuncio>> ConsultarPOC(string descricao);
        Task<Anuncio> ObterPorIdPOC(int id);       
        Task<Anuncio> ObterPorNomePOC(string descricao);

        Task<List<Anuncio>> RetornarTodosAtivosPOC();
        Task IncluirPOC(Anuncio valor);
        Task AlterarPOC(Anuncio valor);
        Task ExlcuirPOC(int Id);

    }
}

